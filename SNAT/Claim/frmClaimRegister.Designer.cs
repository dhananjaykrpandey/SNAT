namespace SNAT.Document
{
    partial class frmClaimRegister
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.btnSearch = new Telerik.WinControls.UI.RadButton();
            this.txtSearch = new Telerik.WinControls.UI.RadTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grdClaimList = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaimList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaimList.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSearch.Image = global::SNAT.Properties.Resources.searchbutton;
            this.btnSearch.Location = new System.Drawing.Point(385, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(77, 24);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(115, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.NullText = "Please enter keyword for search ";
            this.txtSearch.Size = new System.Drawing.Size(263, 20);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(0, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Search Member :";
            // 
            // grdClaimList
            // 
            this.grdClaimList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdClaimList.AutoSizeRows = true;
            this.grdClaimList.BackColor = System.Drawing.Color.Transparent;
            this.grdClaimList.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdClaimList.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.grdClaimList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdClaimList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.grdClaimList.Location = new System.Drawing.Point(4, 34);
            // 
            // 
            // 
            this.grdClaimList.MasterTemplate.AllowSearchRow = true;
            this.grdClaimList.MasterTemplate.AutoGenerateColumns = false;
            this.grdClaimList.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "id";
            gridViewTextBoxColumn1.HeaderText = "Claim ID";
            gridViewTextBoxColumn1.Name = "dcid";
            gridViewTextBoxColumn1.Width = 126;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "LetPerson";
            gridViewTextBoxColumn2.HeaderText = "Let Person";
            gridViewTextBoxColumn2.Name = "dcLetPerson";
            gridViewTextBoxColumn2.Width = 99;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "MemNationalID";
            gridViewTextBoxColumn3.HeaderText = "Mem. National ID";
            gridViewTextBoxColumn3.Name = "dcMemNationalID";
            gridViewTextBoxColumn3.Width = 153;
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "MemName";
            gridViewTextBoxColumn4.HeaderText = "Mem. Name";
            gridViewTextBoxColumn4.Name = "dcMemName";
            gridViewTextBoxColumn4.Width = 166;
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "BenfNationalID";
            gridViewTextBoxColumn5.HeaderText = "Benf.National ID";
            gridViewTextBoxColumn5.Name = "dcBenfNationalID";
            gridViewTextBoxColumn5.Width = 88;
            gridViewTextBoxColumn6.EnableExpressionEditor = false;
            gridViewTextBoxColumn6.FieldName = "BenfName";
            gridViewTextBoxColumn6.HeaderText = "Benf. Name";
            gridViewTextBoxColumn6.Name = "dcBenfName";
            gridViewTextBoxColumn6.Width = 153;
            gridViewTextBoxColumn7.EnableExpressionEditor = false;
            gridViewTextBoxColumn7.FieldName = "ClaimDesc";
            gridViewTextBoxColumn7.HeaderText = "Claim Status";
            gridViewTextBoxColumn7.Name = "dcClaimStatus";
            gridViewTextBoxColumn7.Width = 88;
            this.grdClaimList.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7});
            this.grdClaimList.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.grdClaimList.Name = "grdClaimList";
            this.grdClaimList.ReadOnly = true;
            this.grdClaimList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdClaimList.Size = new System.Drawing.Size(888, 451);
            this.grdClaimList.TabIndex = 10;
            this.grdClaimList.Text = "radGridView2";
            // 
            // frmClaimRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 485);
            this.Controls.Add(this.grdClaimList);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label1);
            this.Name = "frmClaimRegister";
            this.Text = "frmClaimRegister";
            this.Load += new System.EventHandler(this.frmClaimRegister_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaimList.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaimList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnSearch;
        private Telerik.WinControls.UI.RadTextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadGridView grdClaimList;
    }
}