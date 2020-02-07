using System;
using System.IO;
using System.Windows.Forms;

class Program {
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

		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(new MainForm(mrctlPath));
	}
}

