using System;
using System.Management;
using System.Windows.Forms;

class MRSwitcher {
	public static void Main(string[] Args) {
		if (Args.Length != 1) {
			MessageBox.Show(
				String.Format(
					"Illegal argument count: {0}",
					Args.Length
				),
				"ArgumentError",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);

			return;
		}

		if (Array.IndexOf(new string[] {"Toggle", "Enable", "Disable"}, Args[0]) < 0) {
			MessageBox.Show(
				String.Format(
					"Undefined command: {0}",
					Args[0]
				),
				"ArgumentError",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);

			return;
		}

		foreach (ManagementObject mo in (new ManagementClass("Win32_PnPEntity")).GetInstances()) {
			{
				object pc = mo.GetPropertyValue("PNPClass");

				if (pc == null)
					continue;

				if (pc.ToString() != "Holographic")
					continue;
			}

			if (Args[0] == "Toggle")
				Args[0] = mo.GetPropertyValue("Status").ToString() == "ok" ? "Disable" : "Enable";

			if (
				MessageBox.Show(
					String.Format(
						"Found Device: {0}\nCallMethod: {1}\n\nIs it OK?",
						mo.GetPropertyValue("Name").ToString(),
						Args[0]
					),
					"Found the MixedReality Device",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question
				) == DialogResult.Yes
			) {
				mo.InvokeMethod(Args[0], new Object[] {null});
			}

			return;
		}

		MessageBox.Show(
			"Could not find MixedReality device.\n\nPlease check cable connection",
			"Could not find MixedReality device",
			MessageBoxButtons.OK,
			MessageBoxIcon.Asterisk
		);
	}
}

