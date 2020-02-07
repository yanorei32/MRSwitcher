using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

partial class MainForm : Form {
	void initializeComponent() {
		const int
			imgW	= 320,
			imgH	= 66,
			margin	= 13,
			padding	= 6;

		int curW = 0, curH = 0;
		Assembly execAsm = Assembly.GetExecutingAssembly();

		/*\
		|*| UI Initialization
		\*/
		this.logo		= new PictureBox();
		this.reload		= new Button();
		this.prev		= new Button();
		this.next		= new Button();
		this.enable		= new Button();
		this.disable	= new Button();
		this.deviceName	= new Label();
		this.deviceId	= new Label();
		this.status		= new Label();
		
		this.SuspendLayout();

		curH = curW = margin;

		/*\
		|*| Logo column
		\*/
		curH += padding;
		this.logo.Location			= new Point(curH, curW);
		this.logo.Size				= new Size(imgW, imgH);
		this.logo.BackgroundImage	= new Bitmap(
			execAsm.GetManifestResourceStream("logo")
		);
		curH += padding;

		curH += this.logo.Size.Height;
		curH += padding;

		/*\
		|*| Reload, Prev/Next button column
		\*/
		this.reload.Text		= "Reload (&R)";
		this.reload.UseMnemonic	= true;
		this.reload.Size		= new Size(75,23);
		this.reload.Location	= new Point(curW, curH);
		this.reload.Click		+= new EventHandler(reloadButtonClick);

		curW += this.reload.Size.Width;
		curW += padding;
		curW += padding;

		this.prev.Text			= "< Prev (&P)";
		this.prev.UseMnemonic	= true;
		this.prev.Size			= new Size(75, 23);
		this.prev.Location		= new Point(curW, curH);
		this.prev.Click			+= new EventHandler(prevButtonClick);

		curW += this.prev.Size.Width;
		curW += padding;

		this.next.Text			= "(&N) Next >";
		this.next.UseMnemonic	= true;
		this.next.Size			= new Size(75, 23);
		this.next.Location		= new Point(curW, curH);
		this.next.Click			+= new EventHandler(nextButtonClick);

		curW = margin;
		curH += this.next.Size.Height;
		curH += padding;

		/*\
		|*| User friendly device name column
		\*/
		this.deviceName.Text		= "Device: ";
		this.deviceName.AutoSize	= false;
		this.deviceName.Location	= new Point(curW, curH);
		this.deviceName.Size		= new Size(imgW, 22);
		this.deviceName.Font		= new Font("Consolas", 16F);

		curH += this.deviceName.Size.Height;
		curH += padding;

		/*\
		|*| Status coulumn
		\*/
		this.status.Text		= "Status: ";
		this.status.AutoSize	= false;
		this.status.Location	= new Point(curW, curH);
		this.status.Size		= new Size(imgW, 20);
		this.status.Font		= new Font("Consolas", 14F);

		curH += this.status.Size.Height;
		curH += padding;

		/*\
		|*| DeviceId column
		\*/
		this.deviceId.Text		= "DeivceID:\n";
		this.deviceId.AutoSize	= false;
		this.deviceId.Location	= new Point(curW, curH);
		this.deviceId.Size		= new Size(imgW, 50);
		this.deviceId.Font		= new Font("Consolas", 9F);

		curH += this.deviceId.Size.Height;
		curH += padding;

		/*\
		|*| Enable/Disable button column
		\*/
		this.disable.Text			= "Disable (&D)";
		this.disable.UseMnemonic	= true;
		this.disable.Size			= new Size(75, 23);
		this.disable.Location		= new Point(curW, curH);
		this.disable.Click			+= new EventHandler(disableButtonClick);

		curW += this.disable.Size.Width;
		curW += padding;

		this.enable.Text		= "Enable (&E)";
		this.enable.UseMnemonic	= true;
		this.enable.Size		= new Size(75, 23);
		this.enable.Location	= new Point(curW, curH);
		this.enable.Click		+= new EventHandler(enableButtonClick);

		curH += this.enable.Size.Height;
		curH += padding;

		curW = margin;

		this.Text				= "MRSwitcher";
		this.ClientSize			= new Size(imgW + (margin * 2), curH);
		this.MinimumSize		= this.Size;
		this.MaximumSize		= this.Size;
		this.FormBorderStyle	= FormBorderStyle.FixedSingle;
		this.Icon				= new Icon(execAsm.GetManifestResourceStream("icon"));
		// this.KeyDown			+= new EventHandler(formKeyDown);
		this.Controls.Add(this.logo);
		this.Controls.Add(this.reload);
		this.Controls.Add(this.prev);
		this.Controls.Add(this.next);
		this.Controls.Add(this.deviceName);
		this.Controls.Add(this.status);
		this.Controls.Add(this.deviceId);
		this.Controls.Add(this.enable);
		this.Controls.Add(this.disable);
		this.ResumeLayout(false);
	}
}

