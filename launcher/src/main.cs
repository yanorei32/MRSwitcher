using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

#if (ENABLER && DISABLER)
#error ENABLER and DISABLER are defined.
#elif (!ENABLER && !DISABLER)
#error ENABLER or DISABLER is not defined.
#endif

class MRControlLauncher {
	const int ERROR_PATH_NOT_FOUND = 3;

	static string getFullPath(string filename) {
		// ref: https://stackoverflow.com/questions/3855956/

		if (File.Exists(filename))
			return Path.GetFullPath(filename);

		var values = Environment.GetEnvironmentVariable("PATH");

		foreach (var dir in values.Split(Path.PathSeparator)) {
			var fullpath = Path.Combine(dir, filename);

			if (File.Exists(fullpath))
				return fullpath;
		}

		return null;
	}

	public static void Main(string[] Args) {
		var mrctlPath = getFullPath("mr-ctl.exe");

		if (mrctlPath == null) {
			MessageBox.Show("mr-ctl.exe not found.");
			Environment.Exit(ERROR_PATH_NOT_FOUND);
		}

		var psi = new ProcessStartInfo();

		psi.CreateNoWindow			= true;
		psi.UseShellExecute			= false;
		psi.FileName				= mrctlPath;
		psi.RedirectStandardError	= true;

#if ENABLER
		psi.Arguments		= "enable";
#elif DISABLER
		psi.Arguments		= "disable";
#endif

		try {
			var p = Process.Start(psi);

			var stderr = p.StandardError.ReadToEnd();
			p.WaitForExit();

			if (p.ExitCode != 0) {
				MessageBox.Show(string.Format(
					stderr
				));
			}
		} catch (Win32Exception) {
		}
	}
}

