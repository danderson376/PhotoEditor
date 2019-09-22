namespace PhotoEditor
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
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.locateOnDiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectRootFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.detailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.smallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.largeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.listView1 = new System.Windows.Forms.ListView();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Location = new System.Drawing.Point(12, 35);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(175, 403);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(800, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locateOnDiskToolStripMenuItem,
            this.selectRootFolderToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// locateOnDiskToolStripMenuItem
			// 
			this.locateOnDiskToolStripMenuItem.Name = "locateOnDiskToolStripMenuItem";
			this.locateOnDiskToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.locateOnDiskToolStripMenuItem.Text = "Locate On Disk";
			this.locateOnDiskToolStripMenuItem.Click += new System.EventHandler(this.LocateOnDiskToolStripMenuItem_Click);
			// 
			// selectRootFolderToolStripMenuItem
			// 
			this.selectRootFolderToolStripMenuItem.Name = "selectRootFolderToolStripMenuItem";
			this.selectRootFolderToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.selectRootFolderToolStripMenuItem.Text = "Select Root Folder";
			this.selectRootFolderToolStripMenuItem.Click += new System.EventHandler(this.SelectRootFolderToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailToolStripMenuItem,
            this.smallToolStripMenuItem,
            this.largeToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "&View";
			// 
			// detailToolStripMenuItem
			// 
			this.detailToolStripMenuItem.Name = "detailToolStripMenuItem";
			this.detailToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.detailToolStripMenuItem.Text = "Detail";
			this.detailToolStripMenuItem.Click += new System.EventHandler(this.DetailToolStripMenuItem_Click);
			// 
			// smallToolStripMenuItem
			// 
			this.smallToolStripMenuItem.Name = "smallToolStripMenuItem";
			this.smallToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.smallToolStripMenuItem.Text = "Small";
			this.smallToolStripMenuItem.Click += new System.EventHandler(this.SmallToolStripMenuItem_Click);
			// 
			// largeToolStripMenuItem
			// 
			this.largeToolStripMenuItem.Name = "largeToolStripMenuItem";
			this.largeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.largeToolStripMenuItem.Text = "Large";
			this.largeToolStripMenuItem.Click += new System.EventHandler(this.LargeToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
			// 
			// listView1
			// 
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(193, 35);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(595, 403);
			this.listView1.TabIndex = 3;
			this.listView1.UseCompatibleStateImageBehavior = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "PhotoEditor";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locateOnDiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectRootFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ListView listView1;
	}
}

