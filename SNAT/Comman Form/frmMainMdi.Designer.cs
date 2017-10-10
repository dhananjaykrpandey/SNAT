namespace SNAT
{
    partial class frmMainMDI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

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
        public void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainMDI));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.radDockBody = new Telerik.WinControls.UI.Docking.RadDock();
            this.toolWindowMenu = new Telerik.WinControls.UI.Docking.ToolWindow();
            this.treeMenu = new Telerik.WinControls.UI.RadTreeView();
            this.toolTabStrip1 = new Telerik.WinControls.UI.Docking.ToolTabStrip();
            this.documentContainerBody = new Telerik.WinControls.UI.Docking.DocumentContainer();
            this.office2013DarkTheme1 = new Telerik.WinControls.Themes.Office2013DarkTheme();
            this.visualStudio2012DarkTheme1 = new Telerik.WinControls.Themes.VisualStudio2012DarkTheme();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radDockBody)).BeginInit();
            this.radDockBody.SuspendLayout();
            this.toolWindowMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolTabStrip1)).BeginInit();
            this.toolTabStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentContainerBody)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.splitContainer1.Panel2.Controls.Add(this.radDockBody);
            this.splitContainer1.Size = new System.Drawing.Size(720, 460);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 1;
            // 
            // radDockBody
            // 
            this.radDockBody.ActiveWindow = this.toolWindowMenu;
            this.radDockBody.AutoHideAnimation = Telerik.WinControls.UI.Docking.AutoHideAnimateMode.Both;
            this.radDockBody.BackColor = System.Drawing.Color.Transparent;
            this.radDockBody.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radDockBody.BackgroundImage")));
            this.radDockBody.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radDockBody.CausesValidation = false;
            this.radDockBody.Controls.Add(this.toolTabStrip1);
            this.radDockBody.Controls.Add(this.documentContainerBody);
            this.radDockBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radDockBody.IsCleanUpTarget = true;
            this.radDockBody.Location = new System.Drawing.Point(0, 0);
            this.radDockBody.MainDocumentContainer = this.documentContainerBody;
            this.radDockBody.Name = "radDockBody";
            // 
            // 
            // 
            this.radDockBody.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.radDockBody.Size = new System.Drawing.Size(720, 431);
            this.radDockBody.TabIndex = 0;
            this.radDockBody.TabStop = false;
            this.radDockBody.Text = "radDock1";
            this.radDockBody.ThemeName = "ControlDefault";
            this.radDockBody.DockWindowClosing += new Telerik.WinControls.UI.Docking.DockWindowCancelEventHandler(this.radDockBody_DockWindowClosing);
            this.radDockBody.Click += new System.EventHandler(this.radDockBody_Click);
            // 
            // toolWindowMenu
            // 
            this.toolWindowMenu.BackColor = System.Drawing.Color.Transparent;
            this.toolWindowMenu.Caption = null;
            this.toolWindowMenu.Controls.Add(this.treeMenu);
            this.toolWindowMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolWindowMenu.Location = new System.Drawing.Point(1, 24);
            this.toolWindowMenu.Name = "toolWindowMenu";
            this.toolWindowMenu.PreviousDockState = Telerik.WinControls.UI.Docking.DockState.Docked;
            this.toolWindowMenu.Size = new System.Drawing.Size(202, 395);
            this.toolWindowMenu.Text = "SNAT Software Menu ";
            // 
            // treeMenu
            // 
            this.treeMenu.BackColor = System.Drawing.Color.LightBlue;
            this.treeMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.treeMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.treeMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMenu.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.treeMenu.ForeColor = System.Drawing.SystemColors.ControlText;
            this.treeMenu.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.treeMenu.Location = new System.Drawing.Point(0, 0);
            this.treeMenu.Name = "treeMenu";
            this.treeMenu.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.treeMenu.ShowLines = true;
            this.treeMenu.Size = new System.Drawing.Size(202, 395);
            this.treeMenu.SpacingBetweenNodes = -1;
            this.treeMenu.TabIndex = 0;
            this.treeMenu.Text = "radTreeView1";
            this.treeMenu.ThemeName = "VisualStudio2012Dark";
            this.treeMenu.NodeMouseClick += new Telerik.WinControls.UI.RadTreeView.TreeViewEventHandler(this.treeMenu_NodeMouseClick);
            // 
            // toolTabStrip1
            // 
            this.toolTabStrip1.CanUpdateChildIndex = true;
            this.toolTabStrip1.Controls.Add(this.toolWindowMenu);
            this.toolTabStrip1.Location = new System.Drawing.Point(5, 5);
            this.toolTabStrip1.Name = "toolTabStrip1";
            // 
            // 
            // 
            this.toolTabStrip1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.toolTabStrip1.SelectedIndex = 0;
            this.toolTabStrip1.Size = new System.Drawing.Size(204, 421);
            this.toolTabStrip1.SizeInfo.AbsoluteSize = new System.Drawing.Size(204, 200);
            this.toolTabStrip1.SizeInfo.SplitterCorrection = new System.Drawing.Size(4, 0);
            this.toolTabStrip1.TabIndex = 1;
            this.toolTabStrip1.TabStop = false;
            this.toolTabStrip1.ThemeName = "ControlDefault";
            // 
            // documentContainerBody
            // 
            this.documentContainerBody.CausesValidation = false;
            this.documentContainerBody.Name = "documentContainerBody";
            // 
            // 
            // 
            this.documentContainerBody.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.documentContainerBody.SizeInfo.AbsoluteSize = new System.Drawing.Size(502, 200);
            this.documentContainerBody.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Fill;
            this.documentContainerBody.SizeInfo.SplitterCorrection = new System.Drawing.Size(-4, 0);
            this.documentContainerBody.TabIndex = 2;
            this.documentContainerBody.ThemeName = "ControlDefault";
            // 
            // frmMainMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(720, 460);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmMainMDI";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "SNAT National Burial Scheme Management System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainMDI_FormClosing);
            this.Load += new System.EventHandler(this.frmMainMDI_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radDockBody)).EndInit();
            this.radDockBody.ResumeLayout(false);
            this.toolWindowMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolTabStrip1)).EndInit();
            this.toolTabStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentContainerBody)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Telerik.WinControls.UI.Docking.RadDock radDockBody;
        public Telerik.WinControls.UI.Docking.ToolWindow toolWindowMenu;
        public Telerik.WinControls.UI.Docking.DocumentContainer documentContainerBody;
        public System.Windows.Forms.SplitContainer splitContainer1;
        public Telerik.WinControls.UI.RadTreeView treeMenu;
        private Telerik.WinControls.UI.Docking.ToolTabStrip toolTabStrip1;
        private Telerik.WinControls.Themes.Office2013DarkTheme office2013DarkTheme1;
        private Telerik.WinControls.Themes.VisualStudio2012DarkTheme visualStudio2012DarkTheme1;
        
    }
}

