namespace SNAT.Document
{
    partial class frmUserRight
    {

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserRight));
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.btnLoadData = new Telerik.WinControls.UI.RadButton();
            this.btnSelectFolder = new Telerik.WinControls.UI.RadButton();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtUserName = new Telerik.WinControls.UI.RadTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpMenuRights = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnResetRigths = new Telerik.WinControls.UI.RadButton();
            this.btnUpdateRigths = new Telerik.WinControls.UI.RadButton();
            this.grdRights = new Telerik.WinControls.UI.RadGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unSelectAllFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.btnLoadData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tbpMenuRights.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnResetRigths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdateRigths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRights)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRights.MasterTemplate)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadData
            // 
            this.btnLoadData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadData.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadData.Image")));
            this.btnLoadData.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLoadData.Location = new System.Drawing.Point(538, 3);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(133, 24);
            this.btnLoadData.TabIndex = 11;
            this.btnLoadData.Text = "Load Menu";
            this.btnLoadData.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectFolder.Image")));
            this.btnSelectFolder.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSelectFolder.Location = new System.Drawing.Point(399, 3);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(133, 24);
            this.btnSelectFolder.TabIndex = 10;
            this.btnSelectFolder.Text = "Select User Name";
            this.btnSelectFolder.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnLoadData);
            this.splitContainer1.Panel1.Controls.Add(this.btnSelectFolder);
            this.splitContainer1.Panel1.Controls.Add(this.txtUserName);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(684, 494);
            this.splitContainer1.SplitterDistance = 30;
            this.splitContainer1.TabIndex = 2;
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Location = new System.Drawing.Point(79, 5);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(313, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpMenuRights);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(58, 30);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(682, 458);
            this.tabControl1.TabIndex = 0;
            // 
            // tbpMenuRights
            // 
            this.tbpMenuRights.Controls.Add(this.panel1);
            this.tbpMenuRights.Controls.Add(this.grdRights);
            this.tbpMenuRights.Location = new System.Drawing.Point(4, 34);
            this.tbpMenuRights.Name = "tbpMenuRights";
            this.tbpMenuRights.Padding = new System.Windows.Forms.Padding(3);
            this.tbpMenuRights.Size = new System.Drawing.Size(674, 420);
            this.tbpMenuRights.TabIndex = 0;
            this.tbpMenuRights.Text = "U s e r   M e n u   R i g h t s";
            this.tbpMenuRights.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.btnResetRigths);
            this.panel1.Controls.Add(this.btnUpdateRigths);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 376);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(668, 41);
            this.panel1.TabIndex = 1;
            // 
            // btnResetRigths
            // 
            this.btnResetRigths.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetRigths.Image = global::SNAT.Properties.Resources.reload;
            this.btnResetRigths.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnResetRigths.Location = new System.Drawing.Point(543, 7);
            this.btnResetRigths.Name = "btnResetRigths";
            this.btnResetRigths.Size = new System.Drawing.Size(121, 31);
            this.btnResetRigths.TabIndex = 11;
            this.btnResetRigths.Text = "Reset Rights";
            this.btnResetRigths.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResetRigths.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnResetRigths.Click += new System.EventHandler(this.btnResetRigths_Click);
            // 
            // btnUpdateRigths
            // 
            this.btnUpdateRigths.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateRigths.Image = global::SNAT.Properties.Resources.Accept;
            this.btnUpdateRigths.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpdateRigths.Location = new System.Drawing.Point(418, 7);
            this.btnUpdateRigths.Name = "btnUpdateRigths";
            this.btnUpdateRigths.Size = new System.Drawing.Size(121, 31);
            this.btnUpdateRigths.TabIndex = 11;
            this.btnUpdateRigths.Text = "Update Rights";
            this.btnUpdateRigths.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdateRigths.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpdateRigths.Click += new System.EventHandler(this.btnUpdateRigths_Click);
            // 
            // grdRights
            // 
            this.grdRights.AllowShowFocusCues = true;
            this.grdRights.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRights.AutoSizeRows = true;
            this.grdRights.BackColor = System.Drawing.Color.Transparent;
            this.grdRights.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdRights.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.grdRights.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdRights.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.grdRights.Location = new System.Drawing.Point(3, 3);
            // 
            // 
            // 
            this.grdRights.MasterTemplate.AllowAddNewRow = false;
            this.grdRights.MasterTemplate.AllowDeleteRow = false;
            this.grdRights.MasterTemplate.AllowSearchRow = true;
            this.grdRights.MasterTemplate.AutoGenerateColumns = false;
            this.grdRights.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "HeaderID";
            gridViewTextBoxColumn1.HeaderText = "HeaderID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "dcHeaderID";
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "HeaderName";
            gridViewTextBoxColumn2.HeaderText = "Header Menu Name";
            gridViewTextBoxColumn2.Name = "dcHeaderName";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 304;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "HeaderPosition";
            gridViewTextBoxColumn3.HeaderText = "HeaderPosition";
            gridViewTextBoxColumn3.IsVisible = false;
            gridViewTextBoxColumn3.Name = "dcHeaderPosition";
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "ChiledID";
            gridViewTextBoxColumn4.HeaderText = "ChiledID";
            gridViewTextBoxColumn4.IsVisible = false;
            gridViewTextBoxColumn4.Name = "dcChiledID";
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "childname";
            gridViewTextBoxColumn5.HeaderText = "Menu Name";
            gridViewTextBoxColumn5.Name = "dcchildname";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn5.Width = 194;
            gridViewTextBoxColumn6.EnableExpressionEditor = false;
            gridViewTextBoxColumn6.FieldName = "childposition";
            gridViewTextBoxColumn6.HeaderText = "childposition";
            gridViewTextBoxColumn6.IsVisible = false;
            gridViewTextBoxColumn6.Name = "dcchildposition";
            gridViewTextBoxColumn7.EnableExpressionEditor = false;
            gridViewTextBoxColumn7.FieldName = "userid";
            gridViewTextBoxColumn7.HeaderText = "userid";
            gridViewTextBoxColumn7.IsVisible = false;
            gridViewTextBoxColumn7.Name = "dcuserid";
            gridViewCheckBoxColumn1.EnableExpressionEditor = false;
            gridViewCheckBoxColumn1.FieldName = "Rigths";
            gridViewCheckBoxColumn1.HeaderText = "Rigths";
            gridViewCheckBoxColumn1.MinWidth = 20;
            gridViewCheckBoxColumn1.Name = "dcRigths";
            gridViewCheckBoxColumn1.Width = 151;
            this.grdRights.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewCheckBoxColumn1});
            this.grdRights.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.grdRights.Name = "grdRights";
            this.grdRights.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdRights.Size = new System.Drawing.Size(668, 367);
            this.grdRights.TabIndex = 0;
            this.grdRights.Text = "radGridView1";
            this.grdRights.Click += new System.EventHandler(this.grdRights_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllFolderToolStripMenuItem,
            this.unSelectAllFolderToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(179, 48);
            // 
            // selectAllFolderToolStripMenuItem
            // 
            this.selectAllFolderToolStripMenuItem.Name = "selectAllFolderToolStripMenuItem";
            this.selectAllFolderToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.selectAllFolderToolStripMenuItem.Text = "Select All Folder";
            // 
            // unSelectAllFolderToolStripMenuItem
            // 
            this.unSelectAllFolderToolStripMenuItem.Name = "unSelectAllFolderToolStripMenuItem";
            this.unSelectAllFolderToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.unSelectAllFolderToolStripMenuItem.Text = "Un-Select All Folder";
            // 
            // frmUserRight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 494);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmUserRight";
            this.Text = "frmUserRight";
            this.Load += new System.EventHandler(this.frmUserRight_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnLoadData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectFolder)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tbpMenuRights.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnResetRigths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdateRigths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRights.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRights)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnLoadData;
        private Telerik.WinControls.UI.RadButton btnSelectFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Telerik.WinControls.UI.RadTextBox txtUserName;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpMenuRights;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadButton btnResetRigths;
        private Telerik.WinControls.UI.RadButton btnUpdateRigths;
        private Telerik.WinControls.UI.RadGridView grdRights;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem selectAllFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unSelectAllFolderToolStripMenuItem;
        private System.ComponentModel.IContainer components;
    }
}