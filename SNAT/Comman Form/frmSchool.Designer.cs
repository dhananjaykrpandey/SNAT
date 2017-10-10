namespace SNAT.Comman_Form
{
    partial class frmSchool
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSchool));
            this.grdList = new Telerik.WinControls.UI.RadGridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tbpList = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.tbpDetails = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRemarks = new Telerik.WinControls.UI.RadTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtName = new Telerik.WinControls.UI.RadTextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtCode = new Telerik.WinControls.UI.RadTextBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.bindingNavigatorMain = new System.Windows.Forms.BindingNavigator(this.components);
            this.lblCountItem = new System.Windows.Forms.ToolStripLabel();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnMoveFirst = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMovePrevious = new System.Windows.Forms.ToolStripButton();
            this.txtPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMoveNext = new System.Windows.Forms.ToolStripButton();
            this.btnMoveLast = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList.MasterTemplate)).BeginInit();
            this.tbpList.SuspendLayout();
            this.tbpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarks)).BeginInit();
            this.txtRemarks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            this.txtName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).BeginInit();
            this.tabMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorMain)).BeginInit();
            this.bindingNavigatorMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdList
            // 
            this.grdList.AutoSizeRows = true;
            this.grdList.BackColor = System.Drawing.Color.Transparent;
            this.grdList.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.grdList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.grdList.Location = new System.Drawing.Point(3, 3);
            // 
            // 
            // 
            this.grdList.MasterTemplate.AllowAddNewRow = false;
            this.grdList.MasterTemplate.AllowDeleteRow = false;
            this.grdList.MasterTemplate.AllowEditRow = false;
            this.grdList.MasterTemplate.AllowSearchRow = true;
            this.grdList.MasterTemplate.AutoGenerateColumns = false;
            this.grdList.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "id";
            gridViewTextBoxColumn1.HeaderText = "id";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "dcid";
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "Code";
            gridViewTextBoxColumn2.HeaderText = "School Code";
            gridViewTextBoxColumn2.Name = "dccode";
            gridViewTextBoxColumn2.Width = 103;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "name";
            gridViewTextBoxColumn3.HeaderText = "School Name";
            gridViewTextBoxColumn3.Name = "dcname";
            gridViewTextBoxColumn3.Width = 328;
            gridViewCheckBoxColumn1.EnableExpressionEditor = false;
            gridViewCheckBoxColumn1.FieldName = "status";
            gridViewCheckBoxColumn1.HeaderText = "Status";
            gridViewCheckBoxColumn1.MinWidth = 20;
            gridViewCheckBoxColumn1.Name = "dcStatus";
            gridViewCheckBoxColumn1.Width = 37;
            this.grdList.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewCheckBoxColumn1});
            this.grdList.MasterTemplate.EnableAlternatingRowColor = true;
            this.grdList.MasterTemplate.MultiSelect = true;
            this.grdList.MasterTemplate.ReadOnly = true;
            this.grdList.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            this.grdList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdList.Size = new System.Drawing.Size(487, 209);
            this.grdList.TabIndex = 0;
            this.grdList.Text = "radGridView1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "detailsforms.png");
            this.imageList1.Images.SetKeyName(1, "list.png");
            this.imageList1.Images.SetKeyName(2, "list.png");
            // 
            // tbpList
            // 
            this.tbpList.Controls.Add(this.grdList);
            this.tbpList.ImageKey = "list.png";
            this.tbpList.Location = new System.Drawing.Point(4, 34);
            this.tbpList.Name = "tbpList";
            this.tbpList.Padding = new System.Windows.Forms.Padding(3);
            this.tbpList.Size = new System.Drawing.Size(493, 215);
            this.tbpList.TabIndex = 1;
            this.tbpList.Text = "L i s t";
            this.tbpList.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "School Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "School code";
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.Location = new System.Drawing.Point(107, 133);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(56, 17);
            this.chkStatus.TabIndex = 4;
            this.chkStatus.Text = "Active";
            this.chkStatus.UseVisualStyleBackColor = true;
            this.chkStatus.CheckedChanged += new System.EventHandler(this.chkStatus_CheckedChanged);
            // 
            // tbpDetails
            // 
            this.tbpDetails.Controls.Add(this.label3);
            this.tbpDetails.Controls.Add(this.txtRemarks);
            this.tbpDetails.Controls.Add(this.txtName);
            this.tbpDetails.Controls.Add(this.txtCode);
            this.tbpDetails.Controls.Add(this.chkStatus);
            this.tbpDetails.Controls.Add(this.label2);
            this.tbpDetails.Controls.Add(this.label1);
            this.tbpDetails.ImageKey = "detailsforms.png";
            this.tbpDetails.Location = new System.Drawing.Point(4, 34);
            this.tbpDetails.Name = "tbpDetails";
            this.tbpDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tbpDetails.Size = new System.Drawing.Size(493, 215);
            this.tbpDetails.TabIndex = 0;
            this.tbpDetails.Text = "D e t a i l s";
            this.tbpDetails.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Remarks";
            // 
            // txtRemarks
            // 
            this.txtRemarks.AutoSize = false;
            this.txtRemarks.Controls.Add(this.textBox1);
            this.txtRemarks.Location = new System.Drawing.Point(107, 67);
            this.txtRemarks.MaxLength = 500;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.NullText = "Enter school remarks";
            this.txtRemarks.Size = new System.Drawing.Size(332, 60);
            this.txtRemarks.TabIndex = 8;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(156, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 13);
            this.textBox1.TabIndex = 7;
            // 
            // txtName
            // 
            this.txtName.Controls.Add(this.txtStatus);
            this.txtName.Location = new System.Drawing.Point(107, 41);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.NullText = "Enter school name";
            this.txtName.Size = new System.Drawing.Size(332, 20);
            this.txtName.TabIndex = 6;
            // 
            // txtStatus
            // 
            this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStatus.Location = new System.Drawing.Point(156, 4);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(100, 13);
            this.txtStatus.TabIndex = 7;
            this.txtStatus.TextChanged += new System.EventHandler(this.txtChkBoxStatus_TextChanged);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(107, 16);
            this.txtCode.MaxLength = 15;
            this.txtCode.Name = "txtCode";
            this.txtCode.NullText = "Enter school code";
            this.txtCode.Size = new System.Drawing.Size(186, 20);
            this.txtCode.TabIndex = 5;
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tbpDetails);
            this.tabMain.Controls.Add(this.tbpList);
            this.tabMain.ImageList = this.imageList1;
            this.tabMain.ItemSize = new System.Drawing.Size(84, 30);
            this.tabMain.Location = new System.Drawing.Point(1, 28);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(501, 253);
            this.tabMain.TabIndex = 2;
            // 
            // bindingNavigatorMain
            // 
            this.bindingNavigatorMain.AddNewItem = null;
            this.bindingNavigatorMain.BackColor = System.Drawing.SystemColors.Control;
            this.bindingNavigatorMain.CountItem = this.lblCountItem;
            this.bindingNavigatorMain.DeleteItem = null;
            this.bindingNavigatorMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnEdit,
            this.btnDelete,
            this.toolStripSeparator1,
            this.btnSave,
            this.btnUndo,
            this.bindingNavigatorSeparator,
            this.btnMoveFirst,
            this.toolStripSeparator2,
            this.btnMovePrevious,
            this.txtPositionItem,
            this.lblCountItem,
            this.bindingNavigatorSeparator1,
            this.btnMoveNext,
            this.btnMoveLast,
            this.bindingNavigatorSeparator2,
            this.btnRefresh,
            this.btnSearch,
            this.btnPrint,
            this.btnExit});
            this.bindingNavigatorMain.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigatorMain.MoveFirstItem = this.btnMoveFirst;
            this.bindingNavigatorMain.MoveLastItem = this.btnMoveLast;
            this.bindingNavigatorMain.MoveNextItem = this.btnMoveNext;
            this.bindingNavigatorMain.MovePreviousItem = this.btnMovePrevious;
            this.bindingNavigatorMain.Name = "bindingNavigatorMain";
            this.bindingNavigatorMain.PositionItem = this.txtPositionItem;
            this.bindingNavigatorMain.Size = new System.Drawing.Size(505, 25);
            this.bindingNavigatorMain.TabIndex = 3;
            this.bindingNavigatorMain.Text = "bindingNavigator1";
            // 
            // lblCountItem
            // 
            this.lblCountItem.Name = "lblCountItem";
            this.lblCountItem.Size = new System.Drawing.Size(35, 22);
            this.lblCountItem.Text = "of {0}";
            this.lblCountItem.ToolTipText = "Total number of items";
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Image = global::SNAT.Properties.Resources.file_add;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.RightToLeftAutoMirrorImage = true;
            this.btnAdd.Size = new System.Drawing.Size(23, 22);
            this.btnAdd.Text = "Add new";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEdit.Image = global::SNAT.Properties.Resources.file_edit;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(23, 22);
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RightToLeftAutoMirrorImage = true;
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::SNAT.Properties.Resources.Save_icon;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUndo.Image = global::SNAT.Properties.Resources.Undo_icon;
            this.btnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(23, 22);
            this.btnUndo.Text = "Undo";
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnMoveFirst
            // 
            this.btnMoveFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveFirst.Image")));
            this.btnMoveFirst.Name = "btnMoveFirst";
            this.btnMoveFirst.RightToLeftAutoMirrorImage = true;
            this.btnMoveFirst.Size = new System.Drawing.Size(23, 22);
            this.btnMoveFirst.Text = "Move first";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnMovePrevious
            // 
            this.btnMovePrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMovePrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnMovePrevious.Image")));
            this.btnMovePrevious.Name = "btnMovePrevious";
            this.btnMovePrevious.RightToLeftAutoMirrorImage = true;
            this.btnMovePrevious.Size = new System.Drawing.Size(23, 22);
            this.btnMovePrevious.Text = "Move previous";
            // 
            // txtPositionItem
            // 
            this.txtPositionItem.AccessibleName = "Position";
            this.txtPositionItem.AutoSize = false;
            this.txtPositionItem.Name = "txtPositionItem";
            this.txtPositionItem.Size = new System.Drawing.Size(50, 23);
            this.txtPositionItem.Text = "0";
            this.txtPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnMoveNext
            // 
            this.btnMoveNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveNext.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveNext.Image")));
            this.btnMoveNext.Name = "btnMoveNext";
            this.btnMoveNext.RightToLeftAutoMirrorImage = true;
            this.btnMoveNext.Size = new System.Drawing.Size(23, 22);
            this.btnMoveNext.Text = "Move next";
            // 
            // btnMoveLast
            // 
            this.btnMoveLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveLast.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveLast.Image")));
            this.btnMoveLast.Name = "btnMoveLast";
            this.btnMoveLast.RightToLeftAutoMirrorImage = true;
            this.btnMoveLast.Size = new System.Drawing.Size(23, 22);
            this.btnMoveLast.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = global::SNAT.Properties.Resources.EasyFind;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 22);
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = global::SNAT.Properties.Resources.printer1;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23, 22);
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExit.Image = global::SNAT.Properties.Resources.Log_Out;
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(23, 22);
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::SNAT.Properties.Resources.reload;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 22);
            this.btnRefresh.Text = "Refresh";
            // 
            // frmSchool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 280);
            this.Controls.Add(this.bindingNavigatorMain);
            this.Controls.Add(this.tabMain);
            this.Name = "frmSchool";
            this.Text = "frmSchool";
            this.Load += new System.EventHandler(this.frmSchool_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdList.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.tbpList.ResumeLayout(false);
            this.tbpDetails.ResumeLayout(false);
            this.tbpDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarks)).EndInit();
            this.txtRemarks.ResumeLayout(false);
            this.txtRemarks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            this.txtName.ResumeLayout(false);
            this.txtName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).EndInit();
            this.tabMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorMain)).EndInit();
            this.bindingNavigatorMain.ResumeLayout(false);
            this.bindingNavigatorMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Telerik.WinControls.UI.RadGridView grdList;
        public System.Windows.Forms.ImageList imageList1;
       
        public System.Windows.Forms.TabPage tbpList;
        public System.Windows.Forms.Label label2;
      
        public System.Windows.Forms.Label label1;
        
        public System.Windows.Forms.TabPage tbpDetails;
        public System.Windows.Forms.TabControl tabMain;
        public System.Windows.Forms.CheckBox chkStatus;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.BindingNavigator bindingNavigatorMain;
        private System.Windows.Forms.ToolStripLabel lblCountItem;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripButton btnMoveFirst;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnMovePrevious;
        private System.Windows.Forms.ToolStripTextBox txtPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton btnMoveNext;
        private System.Windows.Forms.ToolStripButton btnMoveLast;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton btnExit;
        private Telerik.WinControls.UI.RadTextBox txtName;
        private Telerik.WinControls.UI.RadTextBox txtCode;
        private System.Windows.Forms.TextBox txtStatus;
        public System.Windows.Forms.Label label3;
        private Telerik.WinControls.UI.RadTextBox txtRemarks;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripButton btnRefresh;
    }
}