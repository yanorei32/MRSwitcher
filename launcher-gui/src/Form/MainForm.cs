using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

partial class MainForm : Form {
	// UI Elements
	PictureBox	logo;

	Button	reload,
			disable,
			enable,
			next,
			prev;

	Label	deviceName,
			deviceId,
			status;

	string mrctlPath;
	int index = 0;

	List<Device> devices;

	void update() {
		deviceName.ForeColor	= deviceId.ForeColor
								= status.ForeColor
								= devices.Any() ?
									Color.Black : Color.Gray;
		
		if (devices.Count() <= index)
			index = devices.Count() == 0 ? 0 : devices.Count() - 1;

		next.Enabled = index < devices.Count() - 1;
		prev.Enabled = 0 < index;

		disable.Enabled = enable.Enabled = false;

		if (!devices.Any()) {
			deviceName.Text = "Name: Not found";
			return;
		}

		var d = devices[index];
		enable.Enabled = !(disable.Enabled = d.Status);

		deviceName.Text		= "Name: " + d.Name;
		deviceId.Text		= "DeviceID:\n" + d.Id;
		status.Text			= "Status: "
								+ (d.Status ? "Enabled" : "Unavailable");
		status.ForeColor	= d.Status ? Color.Green : Color.Red;
	}

	void reloadDevices() {
		devices = new List<Device>();

		var psi = new ProcessStartInfo();

		psi.CreateNoWindow			= true;
		psi.UseShellExecute			= false;
		psi.FileName				= mrctlPath;
		psi.RedirectStandardError	= true;
		psi.RedirectStandardOutput	= true;
		psi.Arguments				= "ls";

		var p = Process.Start(psi);

		var stderr = p.StandardError.ReadToEnd();

		p.WaitForExit();

		if (p.ExitCode != 0) {
			MessageBox.Show("Device load error: " + stderr);
			return;
		}

		while (p.StandardOutput.Peek() >= 0) {
			string[] deviceStr = p.StandardOutput.ReadLine().Split('\t');
			devices.Add(new Device(deviceStr[2], deviceStr[1], deviceStr[0]));
		}

		update();
	}

	void switchDevice(string operation, string id) {
		var psi = new ProcessStartInfo();

		psi.CreateNoWindow			= true;
		psi.UseShellExecute			= false;
		psi.FileName				= mrctlPath;
		psi.RedirectStandardError	= true;
		psi.RedirectStandardOutput	= true;
		psi.Arguments				= operation + " " + id;

		var p = Process.Start(psi);

		var stderr = p.StandardError.ReadToEnd();

		p.WaitForExit();

		if (p.ExitCode != 0) {
			MessageBox.Show("Device switch error: " + stderr);
		}
	}

	void reloadButtonClick(object sender, EventArgs e) {
		reloadDevices();
	}

	void enableButtonClick(object sender, EventArgs e) {
		switchDevice("enable", devices[index].Id);
		reloadDevices();
	}

	void disableButtonClick(object sender, EventArgs e) {
		switchDevice("disable", devices[index].Id);
		reloadDevices();
	}

	void prevButtonClick(object sender, EventArgs e) {
		index --;
		update();
	}

	void nextButtonClick(object sender, EventArgs e) {
		index ++;
		update();
	}

	public MainForm(string mrctlPath) {
		this.mrctlPath = mrctlPath;
		initializeComponent();
		reloadDevices();
	}
}

