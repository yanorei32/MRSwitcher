using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Windows.Forms;

class MRControl {
	enum Operation {
		List,
		Enable,
		Disable,
	}

	static Operation str2op(string operationStr) {
		switch (operationStr) {
			case "ls":
				return Operation.List;

			case "enable":
				return Operation.Enable;

			case "disable":
				return Operation.Disable;

			default:
				throw new FormatException("illegal operation");
		}
	}

	static List<ManagementObject> getHolographicDevices(string id) {
		var holographicDevices = new List<ManagementObject>();

		foreach (ManagementObject device in (
			(new ManagementClass("Win32_PnPEntity")).GetInstances()
		) ) {
			var pnpClass = device.GetPropertyValue("PNPClass");

			if (pnpClass == null)
				continue;

			if (id != null && id != (string)device.GetPropertyValue("DeviceID"))
				continue;

			if (pnpClass.ToString() != "Holographic")
				continue;

			holographicDevices.Add(device);
		}

		return holographicDevices;
	}

	static void listupDevices(List<ManagementObject> devices) {
		foreach (var d in devices)
			Console.WriteLine(
				"{0}\t{1}\t{2}",
				d.GetPropertyValue("Status"),
				d.GetPropertyValue("DeviceID"),
				d.GetPropertyValue("Name")
			);
	}

	static void switchDevices(
		List<ManagementObject> devices,
		string validatedStatus
	) {
		foreach (var d in devices)
			d.InvokeMethod(
				validatedStatus,
				new Object[] {null}
			);
	}

	public static void Main(string[] Args) {
		/*\
		|*| Parse argument(s)
		\*/
		if (Args.Length == 0) {
			Console.Error.WriteLine("missing operand");
			Environment.Exit(-1);
		}

		Operation o = str2op(Args[0]);

		if ((o == Operation.List ? 1 : 2) < Args.Length) {
			Console.Error.WriteLine("too many/missing argument(s)");
			Environment.Exit(-1);
		}

		var id = (string) null;

		if (2 == Args.Length && o != Operation.List) {
			id = Args[1];
		}

		/*\
		|*| Listup holographic device(s)
		\*/
		var holographicDevices = getHolographicDevices(id);

		/*\
		|*| Operation
		\*/
		switch (o) {
			case Operation.List:
				listupDevices(holographicDevices);
				break;

			case Operation.Enable:
			case Operation.Disable:
				if (!holographicDevices.Any()) {
					Console.Error.WriteLine(
						"Holographic device was not found"
					);

					Environment.Exit(-1);
				}

				try {
					switchDevices(
						holographicDevices,
						Enum.GetName(typeof(Operation), o)
					);

				} catch (ManagementException e) {
					Console.Error.WriteLine(
						"ManagementException ({0})\n({1})",
						e.Message,
						"ex: permission denied, can't switch device"
					);

					Environment.Exit(-1);
				}

				break;
		}
	}
}

