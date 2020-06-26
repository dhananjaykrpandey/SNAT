namespace SNAT.Comman_Form
{
    partial class frmReportViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportViewer));
            this.crViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.BtnPrint = new System.Windows.Forms.ToolStripDropDownButton();
            this.BtnPrintDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnPrintDilog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnSave = new System.Windows.Forms.ToolStripDropDownButton();
            this.BtnSavePdf = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnSaveExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnSaveExcelUnFormated = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.SuspendLayout();
            // 
            // crViewer
            // 
            this.crViewer.ActiveViewIndex = -1;
            this.crViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crViewer.Location = new System.Drawing.Point(0, 0);
            this.crViewer.Name = "crViewer";
            this.crViewer.ShowCloseButton = false;
            this.crViewer.ShowCopyButton = false;
            this.crViewer.ShowGroupTreeButton = false;
            this.crViewer.ShowLogo = false;
            this.crViewer.ShowParameterPanelButton = false;
            this.crViewer.Size = new System.Drawing.Size(539, 404);
            this.crViewer.TabIndex = 0;
            this.crViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnPrint,
            this.toolStripSeparator1,
            this.BtnSave,
            this.toolStripSeparator2,
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.toolStripButton1,
            this.toolStripComboBox1});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(425, 25);
            this.bindingNavigator1.TabIndex = 1;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // BtnPrint
            // 
            this.BtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnPrint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnPrintDefault,
            this.BtnPrintDilog});
            this.BtnPrint.Image = global::SNAT.Properties.Resources.printer1;
            this.BtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(29, 22);
            this.BtnPrint.Text = "toolStripDropDownButton2";
            // 
            // BtnPrintDefault
            // 
            this.BtnPrintDefault.Image = global::SNAT.Properties.Resources.Icon_Printer02_Black;
            this.BtnPrintDefault.Name = "BtnPrintDefault";
            this.BtnPrintDefault.Size = new System.Drawing.Size(180, 22);
            this.BtnPrintDefault.Text = "Print Default";
            // 
            // BtnPrintDilog
            // 
            this.BtnPrintDilog.Image = global::SNAT.Properties.Resources.process;
            this.BtnPrintDilog.Name = "BtnPrintDilog";
            this.BtnPrintDilog.Size = new System.Drawing.Size(180, 22);
            this.BtnPrintDilog.Text = "Print Dilog";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // BtnSave
            // 
            this.BtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnSavePdf,
            this.BtnSaveExcel,
            this.BtnSaveExcelUnFormated});
            this.BtnSave.Image = global::SNAT.Properties.Resources.Save_icon;
            this.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(29, 22);
            this.BtnSave.Text = "toolStripDropDownButton1";
            // 
            // BtnSavePdf
            // 
            this.BtnSavePdf.Image = global::SNAT.Properties.Resources.pdf;
            this.BtnSavePdf.Name = "BtnSavePdf";
            this.BtnSavePdf.Size = new System.Drawing.Size(180, 22);
            this.BtnSavePdf.Text = "PDF";
            // 
            // BtnSaveExcel
            // 
            this.BtnSaveExcel.Image = global::SNAT.Properties.Resources.Excel;
            this.BtnSaveExcel.Name = "BtnSaveExcel";
            this.BtnSaveExcel.Size = new System.Drawing.Size(180, 22);
            this.BtnSaveExcel.Text = "Excel";
            // 
            // BtnSaveExcelUnFormated
            // 
            this.BtnSaveExcelUnFormated.Image = global::SNAT.Properties.Resources.tb_ExcelExport;
            this.BtnSaveExcelUnFormated.Name = "BtnSaveExcelUnFormated";
            this.BtnSaveExcelUnFormated.Size = new System.Drawing.Size(180, 22);
            this.BtnSaveExcelUnFormated.Text = "Excel Un-Formated";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::SNAT.Properties.Resources.EasyFind;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            // 
            // frmReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 404);
            this.Controls.Add(this.crViewer);
            this.Controls.Add(this.bindingNavigator1);
            this.Name = "frmReportViewer";
            this.Text = "frmReportViewer";
            this.Load += new System.EventHandler(this.frmReportViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crViewer;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton BtnSave;
        private System.Windows.Forms.ToolStripMenuItem BtnSavePdf;
        private System.Windows.Forms.ToolStripMenuItem BtnSaveExcel;
        private System.Windows.Forms.ToolStripMenuItem BtnSaveExcelUnFormated;
        private System.Windows.Forms.ToolStripDropDownButton BtnPrint;
        private System.Windows.Forms.ToolStripMenuItem BtnPrintDefault;
        private System.Windows.Forms.ToolStripMenuItem BtnPrintDilog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
    }
}