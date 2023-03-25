namespace NetworkDiscovery
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pMain = new System.Windows.Forms.Panel();
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.lvDevices = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpenWithHttp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenWithHttps = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tsProgressBar2 = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslDiscoveryOn = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslIpAddress = new System.Windows.Forms.ToolStripStatusLabel();
            this.pMain.SuspendLayout();
            this.gbMain.SuspendLayout();
            this.cmsMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pMain
            // 
            this.pMain.Controls.Add(this.gbMain);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Location = new System.Drawing.Point(0, 0);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(800, 450);
            this.pMain.TabIndex = 1;
            // 
            // gbMain
            // 
            this.gbMain.Controls.Add(this.statusStrip1);
            this.gbMain.Controls.Add(this.lvDevices);
            this.gbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMain.Location = new System.Drawing.Point(0, 0);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(800, 450);
            this.gbMain.TabIndex = 0;
            this.gbMain.TabStop = false;
            // 
            // lvDevices
            // 
            this.lvDevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader9,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.lvDevices.ContextMenuStrip = this.cmsMenu;
            this.lvDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDevices.FullRowSelect = true;
            this.lvDevices.HideSelection = false;
            this.lvDevices.Location = new System.Drawing.Point(3, 16);
            this.lvDevices.Name = "lvDevices";
            this.lvDevices.Size = new System.Drawing.Size(794, 431);
            this.lvDevices.TabIndex = 0;
            this.lvDevices.UseCompatibleStateImageBehavior = false;
            this.lvDevices.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "IP Address";
            this.columnHeader1.Width = 91;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Hostname";
            this.columnHeader2.Width = 119;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "MAC address";
            this.columnHeader3.Width = 121;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Interface name";
            this.columnHeader4.Width = 99;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Interface type";
            this.columnHeader9.Width = 89;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Interface description";
            this.columnHeader5.Width = 124;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Intereface speed";
            this.columnHeader6.Width = 96;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Download";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Upload";
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenWithHttp,
            this.tsmiOpenWithHttps});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(167, 48);
            // 
            // tsmiOpenWithHttp
            // 
            this.tsmiOpenWithHttp.Name = "tsmiOpenWithHttp";
            this.tsmiOpenWithHttp.Size = new System.Drawing.Size(166, 22);
            this.tsmiOpenWithHttp.Text = "Open with HTTP";
            this.tsmiOpenWithHttp.Click += new System.EventHandler(this.TsmiOpenWithHttp_Click);
            // 
            // tsmiOpenWithHttps
            // 
            this.tsmiOpenWithHttps.Name = "tsmiOpenWithHttps";
            this.tsmiOpenWithHttps.Size = new System.Drawing.Size(166, 22);
            this.tsmiOpenWithHttps.Text = "Open with HTTPS";
            this.tsmiOpenWithHttps.Click += new System.EventHandler(this.TsmiOpenWithHttps_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslDiscoveryOn,
            this.tsProgressBar,
            this.tsslIpAddress,
            this.tsProgressBar2});
            this.statusStrip1.Location = new System.Drawing.Point(3, 425);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(794, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsProgressBar
            // 
            this.tsProgressBar.Name = "tsProgressBar";
            this.tsProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // tsProgressBar2
            // 
            this.tsProgressBar2.Name = "tsProgressBar2";
            this.tsProgressBar2.Size = new System.Drawing.Size(100, 16);
            // 
            // tsslDiscoveryOn
            // 
            this.tsslDiscoveryOn.Name = "tsslDiscoveryOn";
            this.tsslDiscoveryOn.Size = new System.Drawing.Size(16, 17);
            this.tsslDiscoveryOn.Text = "...";
            // 
            // tsslIpAddress
            // 
            this.tsslIpAddress.Name = "tsslIpAddress";
            this.tsslIpAddress.Size = new System.Drawing.Size(16, 17);
            this.tsslIpAddress.Text = "...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Network Discovery";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.pMain.ResumeLayout(false);
            this.gbMain.ResumeLayout(false);
            this.gbMain.PerformLayout();
            this.cmsMenu.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pMain;
		private System.Windows.Forms.GroupBox gbMain;
		private System.Windows.Forms.ListView lvDevices;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenWithHttp;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenWithHttps;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
        private System.Windows.Forms.ToolStripProgressBar tsProgressBar2;
        private System.Windows.Forms.ToolStripStatusLabel tsslDiscoveryOn;
        private System.Windows.Forms.ToolStripStatusLabel tsslIpAddress;
    }
}

