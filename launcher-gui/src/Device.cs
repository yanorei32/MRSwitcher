class Device {
	string name, id, status;

	public string Name {
		get { return this.name; }
	}

	public string Id {
		get { return this.id; }
	}

	public bool Status {
		get { return this.status == "OK"; }
	}

	public Device(string name, string id, string status) {
		this.name	= name;
		this.id		= id;
		this.status	= status;
	}
}

