using SNAT.Comman_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using SNAT.Comman_Form;
using System.Dynamic;
using SNAT.CommanClass;

namespace SNAT.Document
{
    public partial class frmFinanceDocumentUpload : Form
    {
        DataTable dtWages = new DataTable();
        DataTable dtList = new DataTable();
        DataSet mdsCreateDatatable = new DataSet();
        string strSqlQuery = "";
        Int64 iRowCount = 0;
        Int64 iTotalRowCount = 0;
        public frmFinanceDocumentUpload()
        {
            InitializeComponent();
        }
        private void FillList()
        {
            try
            {
                dtList = new DataTable();
                strSqlQuery = "SELECT wageMonthYear, wageFrom,count(memNationalID)TotalUpload, sum(CASE WHEN ISNULL(lValidMemmber, 0) = 0 THEN 0 ELSE 1 END) ivalidMember, sum(CASE WHEN ISNULL(lValidMemmber, 0) = 0 THEN 1 ELSE 0 END) iInvalidMember , sum(CASE WHEN ISNULL(lWagesProcessed, 0) = 0 THEN 0 ELSE 1 END) iWagesProcessed, sum(CASE WHEN ISNULL(lApproved, 0) = 0 THEN 0 ELSE 1 END) iApproved,Remarks FROM SNAT.dbo.T_WagesUpload GROUP BY wageMonthYear, wageFrom,Remarks  Order by Cast('01-'+  wageMonthYear AS datetime) DESC";

                dtList = ClsDataLayer.GetDataTable(strSqlQuery);
                grdList.DataSource = dtList.DefaultView;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CreateDataTable(string MonthYear)
        {
            try
            {


                dtWages = new DataTable();
                strSqlQuery = "SELECT id, wageMonthYear, wageFrom, memNationalID, memMemberID, memEmployeeNo, memTSCNo, memName, processingdate,wagecredit, wagebalnace, memWegeRemarks, Remarks, luploaded, lValidMemmber,lWagesProcessed, lApproved, documentType, documentName, createby, createdate, updateby, updatedate   FROM SNAT.dbo.T_WagesUpload where wageMonthYear='" + MonthYear + "'";

                if (rbBank.Checked)
                {
                    strSqlQuery = strSqlQuery + Environment.NewLine + " AND wageFrom='BANK'";
                }
                if (rbPSPF.Checked)
                {
                    strSqlQuery = strSqlQuery + Environment.NewLine + " AND wageFrom='PSPF'";
                }
                if (rbTreasury.Checked)
                {
                    strSqlQuery = strSqlQuery + Environment.NewLine + " AND wageFrom='TREASURY'";
                }

                dtWages = ClsDataLayer.GetDataTable(strSqlQuery);
                DataColumn dclSelect = new DataColumn("lSelect", typeof(bool));
                dclSelect.DefaultValue = false;
                dclSelect.ReadOnly = false;
                dtWages.Columns.Add(dclSelect);

                dclSelect = new DataColumn("cStatus", typeof(string));
                dclSelect.DefaultValue = false;
                dclSelect.ReadOnly = false;
                dclSelect.DefaultValue = "L";
                dtWages.Columns.Add(dclSelect);
                grdImport_Excel.DataSource = dtWages.DefaultView;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void frmFinanceDocumentUpload_Load(object sender, EventArgs e)
        {
            try
            {
                CreateDataTable(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
                FillList();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }


        private void grdImport_Excel_RowFormatting(object sender, Telerik.WinControls.UI.RowFormattingEventArgs e)
        {


            //DataView dtCheckView = new DataView();
            //dtCheckView = mdsCreateDatatable.DefaultViewManager.CreateDataView(dtWages);
            //dtCheckView.RowFilter = "";

            //if (Convert.ToBoolean(dtCheckView[e.RowElement.RowInfo.Index]["lValidMemmber"]) == false)
            //{
            //    e.RowElement.DrawFill = true;
            //    e.RowElement.GradientStyle = GradientStyles.Solid;
            //    e.RowElement.BackColor = Color.LightPink;
            //}
            //else if ((dtCheckView[e.RowElement.RowInfo.Index]["lWagesProcessed"] != DBNull.Value) && (Convert.ToBoolean(dtCheckView[e.RowElement.RowInfo.Index]["lWagesProcessed"]) == true))
            //{
            //    e.RowElement.DrawFill = true;
            //    e.RowElement.GradientStyle = GradientStyles.Solid;
            //    e.RowElement.BackColor = Color.LightGoldenrodYellow;
            //}
            //else if ((dtCheckView[e.RowElement.RowInfo.Index]["lApproved"] != DBNull.Value) && (Convert.ToBoolean(dtCheckView[e.RowElement.RowInfo.Index]["lApproved"]) == true))
            //{
            //    e.RowElement.DrawFill = true;
            //    e.RowElement.GradientStyle = GradientStyles.Solid;
            //    e.RowElement.BackColor = Color.LightSeaGreen;
            //}
            //else
            //{
            //    e.RowElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
            //    e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
            //    e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
            //}

            try
            {
                grdImport_Excel.MasterTemplate.BeginUpdate();

                string strcStatus = "";

                strcStatus = e.RowElement.RowInfo.Cells["dccStatus"].Value == DBNull.Value ? "" : e.RowElement.RowInfo.Cells["dccStatus"].Value.ToString();
                switch (strcStatus)
                {
                    case "I":
                        e.RowElement.DrawFill = true;
                        e.RowElement.GradientStyle = GradientStyles.Solid;
                        e.RowElement.BackColor = Color.LightPink;
                        break;
                    case "P":
                        e.RowElement.DrawFill = true;
                        e.RowElement.GradientStyle = GradientStyles.Solid;
                        e.RowElement.BackColor = Color.LightGoldenrodYellow;
                        break;
                    case "A":
                        e.RowElement.DrawFill = true;
                        e.RowElement.GradientStyle = GradientStyles.Solid;
                        e.RowElement.BackColor = Color.LightSeaGreen;
                        break;
                    default:
                        e.RowElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                        break;

                }
                //    //if (Convert.ToBoolean(e.RowElement.RowInfo.Cells["dclValidMemmber"].Value) == false)
                //    //{
                //    //    e.RowElement.DrawFill = true;
                //    //    e.RowElement.GradientStyle = GradientStyles.Solid;
                //    //    e.RowElement.BackColor = Color.LightPink;
                //    //}
                //    //else if ((e.RowElement.RowInfo.Cells["dclWagesProcessed"].Value != DBNull.Value) && (Convert.ToBoolean(e.RowElement.RowInfo.Cells["dclWagesProcessed"].Value) == true))
                //    //{
                //    //    e.RowElement.DrawFill = true;
                //    //    e.RowElement.GradientStyle = GradientStyles.Solid;
                //    //    e.RowElement.BackColor = Color.LightGoldenrodYellow;
                //    //}
                //    //else if ((e.RowElement.RowInfo.Cells["dclApproved"].Value != DBNull.Value) && (Convert.ToBoolean(e.RowElement.RowInfo.Cells["dclApproved"].Value) == true))
                //    //{
                //    //    e.RowElement.DrawFill = true;
                //    //    e.RowElement.GradientStyle = GradientStyles.Solid;
                //    //    e.RowElement.BackColor = Color.LightSeaGreen;
                //    //}
                //    //else
                //    //{
                //    //    e.RowElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
                //    //    e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                //    //    e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                //    //}
                grdImport_Excel.MasterTemplate.EndUpdate();
                iRowCount = grdImport_Excel.RowCount;
                lblCount.Text = "Total Row(s) Added = " + (iRowCount) + " Out of Total Row(s) " + iTotalRowCount;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }


        }
        private bool validateMonthYear()
        {
            try
            {

                if (string.IsNullOrEmpty(txtMonth_Excel.Text.Trim()))
                {
                    ClsMessage.showMessage("Please enter upload month.");
                    txtMonth_Excel.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtYear_Excel.Text.Trim()))
                {
                    ClsMessage.showMessage("Please enter upload year.");
                    txtYear_Excel.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(txtYear_Excel.Text.Trim()) == false && Convert.ToInt32(txtYear_Excel.Text.Trim()) <= 1980)
                {
                    ClsMessage.showMessage("Year cannot be less than 1980.");
                    txtYear_Excel.Focus();
                    return false;
                }

                string dDate = txtYear_Excel.Text.Trim() + "-" + txtMonth_Excel.Text.Trim() + "-01";

                if (!ClsUtility.IsValidDate(dDate))
                {
                    ClsMessage.showMessage("Please enter correct month-year.(MM-YYYY)");
                    txtYear_Excel.Focus();
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }
        private void btnSelectFile_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                GC.Collect();
                if (validateMonthYear() == false) { return; }
                OpenFileDialog opdlg = new OpenFileDialog();
                opdlg.Filter = "Microsoft Excel File(*.xlsx)|*.xlsx|Microsoft Excel File(97-2003) (*.xls)|*.xls|comma separated file (*.csv)|*.csv";
                if (opdlg.ShowDialog() == DialogResult.OK && opdlg.FileName != null)
                {
                    txtFileName_Excel.Text = opdlg.FileName;
                    ReadExcelSheet(txtFileName_Excel.Text.Trim());
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        public void ReadExcelSheet(string filename)
        {

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Open(filename);
            cmbExcelSheet.Items.Clear();
            // Get worksheet names
            foreach (Microsoft.Office.Interop.Excel.Worksheet sh in wb.Worksheets)
                //Debug.WriteLine(sh.Name);
                cmbExcelSheet.Items.Add(sh.Name.ToString().Trim());
            wb.Close();
            excel.Quit();
        }
        private void btnLoadData_Excel_Click(object sender, EventArgs e)
        {
            lblCount.Visible = true;
            try
            {
                if (string.IsNullOrEmpty(txtMonth_Excel.Text.Trim()))
                {
                    ClsMessage.showMessage("Please enter upload month.");
                    txtMonth_Excel.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtYear_Excel.Text.Trim()))
                {
                    ClsMessage.showMessage("Please enter upload year.");
                    txtYear_Excel.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtFileName_Excel.Text.Trim()))
                {
                    ClsMessage.showMessage("Please enter/select excel file.");
                    txtFileName_Excel.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbExcelSheet.Text.Trim()))
                {
                    ClsMessage.showMessage("Please select excel sheet name.");
                    cmbExcelSheet.Focus();
                    return;
                }
                UploadExcel_Advance();
                iRowCount = dtWages.DefaultView.Count;
                lblCount.Text = "Total Row(s) Added = " + iRowCount + " Out of " + iTotalRowCount;
                lblCount.Update();
                //UploadExcel();
                //if (rbBank.Checked)
                //{

                //}
                //if (rbPSPF.Checked)
                //{
                //    UploadPSPFExcel();
                //}
                //if (rbTreasury.Checked)
                //{
                //    UploadPSPFExcel();
                //}
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void UploadExcel_Advance()
        {
           
            this.UseWaitCursor = true;
            int iCount = 1;
            try
            {
                CreateDataTable(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
                frmImportExcel frmobj = new frmImportExcel();

                frmobj.StartPosition = FormStartPosition.CenterParent;

                frmobj.FileName = txtFileName_Excel.Text.Trim();
                frmobj.SheetName = cmbExcelSheet.Text.Trim();
                frmobj.strMonth = txtMonth_Excel.Text.Trim();
                frmobj.strYear = txtYear_Excel.Text.Trim();
                if (rbBank.Checked)
                {

                }
                if (rbPSPF.Checked)
                {
                    frmobj.ImportType = frmImportExcel.enmImportType.PSPF;
                }
                if (rbTreasury.Checked)
                {
                    frmobj.ImportType = frmImportExcel.enmImportType.Treasury;
                }

                frmobj.ShowDialog(this);


                if (frmobj.DialogResult == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    DataTable dtTemp = new DataTable();
                    dtTemp = frmobj.dtExcelData;

                    iTotalRowCount =  dtTemp.Compute("Max(dumpid)", "") ==DBNull.Value ? 0 : Convert.ToInt64(dtTemp.Compute("Max(dumpid)", ""));
                    DataRow dRow;
                    foreach (DataRowView drvRow in dtTemp.DefaultView)
                    {
                        string strIDColName = "";
                        if (rbBank.Checked == true) { strIDColName = "memberid"; }
                        if (rbPSPF.Checked == true) { strIDColName = "memberid"; }
                        if (rbTreasury.Checked == true) { strIDColName = "employeeno"; }

                        if (drvRow["dumpmemberid"] != DBNull.Value && drvRow["dumpmemberid"].ToString().Trim() != "")
                        {
                            if (CheckDataTable(drvRow[strIDColName].ToString().Trim()) == false)
                            {
                                dRow = dtWages.NewRow();
                                dRow["wageMonthYear"] = txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim();
                               
                                if (rbPSPF.Checked)
                                {
                                    dRow["wageFrom"] = "PSPF";
                                }
                                else if (rbBank.Checked)
                                {
                                    dRow["wageFrom"] = "BANK";
                                }
                                else
                                {
                                    dRow["wageFrom"] = "TREASURY";
                                }

                               
                                //dRow["memMemberID"] = drvRow["memberid"].ToString().Trim();
                                bool lValidMember = drvRow["lValidMember"] == DBNull.Value ? false : Convert.ToBoolean(drvRow["lValidMember"]);

                                if (lValidMember == false)
                                {

                                    dRow["memMemberID"] = drvRow["dumpmemberid"] == DBNull.Value ? iCount.ToString() : drvRow["dumpmemberid"].ToString().Trim();
                                    dRow["memEmployeeNo"] = drvRow["dumpmemberid"] == DBNull.Value ? iCount.ToString() : drvRow["dumpmemberid"].ToString().Trim();
                                    dRow["memNationalID"] = drvRow["dumpmemberid"] == DBNull.Value ? iCount.ToString() : drvRow["dumpmemberid"].ToString().Trim();
                                    dRow["memTSCNo"] = drvRow["dumpmemberid"] == DBNull.Value ? iCount.ToString() : drvRow["dumpmemberid"].ToString().Trim();

                                    dRow["memName"] = drvRow["membername"] == DBNull.Value ? "" : drvRow["membername"].ToString().Trim();
                                    dRow["processingdate"] = DateTime.Now.ToString("dd-MM-yyyy");
                                    dRow["wagecredit"] = drvRow["wagesamount"] == DBNull.Value ? 0 : Convert.ToDecimal(drvRow["wagesamount"].ToString().Trim());

                                    dRow["wagebalnace"] = 0;
                                    dRow["memWegeRemarks"] = "Invalid Member. Member details not exits!!";
                                    dRow["Remarks"] = "";
                                    dRow["luploaded"] = false;
                                    dRow["lValidMemmber"] = false;
                                    dRow["lApproved"] = false;
                                    dRow["documentType"] = dRow["wageFrom"];
                                    dRow["documentName"] = txtFileName_Excel.Text.Trim();
                                    dRow["createby"] = ClsSettings.username;
                                    dRow["createdate"] = DateTime.Now.ToString();
                                    dRow["updateby"] = ClsSettings.username;
                                    dRow["updatedate"] = DateTime.Now.ToString();
                                    dRow["lSelect"] = true;
                                    dRow["cStatus"] = "I";
                                    
                                    //drvRow["lWagesProcessed"] = false;
                                    iCount = iCount + 1;
                                }
                                else
                                {
                                    dRow["memMemberID"] = drvRow["memberid"] == DBNull.Value ? iCount.ToString() : drvRow["memberid"].ToString().Trim(); ;
                                    dRow["memEmployeeNo"] = drvRow["employeeno"] == DBNull.Value ? iCount.ToString() : drvRow["employeeno"].ToString().Trim(); ;
                                    dRow["memNationalID"] = drvRow["nationalid"] == DBNull.Value ? iCount.ToString() : drvRow["nationalid"].ToString().Trim(); ;
                                    dRow["memTSCNo"] = drvRow["tscno"] == DBNull.Value ? iCount.ToString() : drvRow["tscno"].ToString().Trim(); ;
                                    dRow["memName"] = drvRow["membername"] == DBNull.Value ? iCount.ToString() : drvRow["membername"].ToString().Trim(); ;
                                    dRow["processingdate"] = DateTime.Now.ToString("dd-MM-yyyy");
                                    dRow["wagecredit"] = drvRow["wagesamount"] == DBNull.Value ? 0 : Convert.ToDecimal(drvRow["wagesamount"].ToString().Trim());
                                    dRow["wagebalnace"] = 0;
                                    dRow["memWegeRemarks"] = "";
                                    dRow["Remarks"] = "";
                                    dRow["luploaded"] = false;
                                    dRow["lValidMemmber"] = true;
                                    dRow["lApproved"] = false;
                                    //drvRow["lWagesProcessed"] = false;
                                    dRow["documentType"] = dRow["wageFrom"];
                                    dRow["documentName"] = txtFileName_Excel.Text.Trim();
                                    dRow["createby"] = ClsSettings.username;
                                    dRow["createdate"] = DateTime.Now.ToString();
                                    dRow["updateby"] = ClsSettings.username;
                                    dRow["updatedate"] = DateTime.Now.ToString();
                                    dRow["lSelect"] = true;
                                    //dRow["cStatus"] = "//P";
                                }

                                dtWages.Rows.Add(dRow);
                            }
                            else
                            {
                                dRow = dtWages.DefaultView[0].Row;

                                if (dRow["lApproved"] != DBNull.Value && dRow["lApproved"].ToString().Trim() != "" && Convert.ToBoolean(dRow["lApproved"]) == false)
                                {
                                    dRow.BeginEdit();
                                    dRow["wagecredit"] = drvRow["wagesamount"] == DBNull.Value ? 0 : Convert.ToDecimal(drvRow["wagesamount"].ToString().Trim());
                                    dRow["lSelect"] = true;
                                    dRow["updateby"] = ClsSettings.username;
                                    dRow["updatedate"] = DateTime.Now.ToString();
                                    dRow["cStatus"] = "A";
                                    dRow.EndEdit();
                                }

                            }

                        }
                        iRowCount = dtWages.DefaultView.Count;
                        lblCount.Text = "Total Row(s) Added = " + (iRowCount) + " Out of " + iTotalRowCount;
                        lblCount.Update();
                       // System.Threading.Thread.Sleep(100);
                    }
                    Cursor.Current = Cursors.Default;
                }
                dtWages.DefaultView.RowFilter = "";
               
                
                this.UseWaitCursor = false;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void UploadExcel()
        {
            int iCount = 1;
            try
            {
                CreateDataTable(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
                frmImportExcel frmobj = new frmImportExcel();

                frmobj.StartPosition = FormStartPosition.CenterParent;

                frmobj.FileName = txtFileName_Excel.Text.Trim();
                frmobj.SheetName = cmbExcelSheet.Text.Trim();
                frmobj.strMonth = txtMonth_Excel.Text.Trim();
                frmobj.strYear = txtYear_Excel.Text.Trim();
                if (rbBank.Checked)
                {

                }
                if (rbPSPF.Checked)
                {
                    frmobj.ImportType = frmImportExcel.enmImportType.PSPF;
                }
                if (rbTreasury.Checked)
                {
                    frmobj.ImportType = frmImportExcel.enmImportType.Treasury;
                }

                frmobj.ShowDialog(this);
                if (frmobj.DialogResult == DialogResult.OK)
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp = frmobj.dtExcelData;

                    DataRow dRow;
                    foreach (DataRowView drvRow in dtTemp.DefaultView)
                    {
                        string strIDColName = "";
                        if (rbBank.Checked == true) { strIDColName = "memberid"; }
                        if (rbPSPF.Checked == true) { strIDColName = "memberid"; }
                        if (rbTreasury.Checked == true) { strIDColName = "employeeno"; }



                        if (drvRow[strIDColName] != DBNull.Value && drvRow[strIDColName].ToString().Trim() != "")
                        {

                            if (CheckDataTable(drvRow[strIDColName].ToString().Trim()) == false)
                            {
                                dRow = dtWages.NewRow();
                                dynamic MemberDetail = new ExpandoObject();
                                string strMemBID = "";

                                dRow["wageMonthYear"] = txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim();
                                if (rbBank.Checked)
                                {
                                    //ProcessBankDetails(drvRow);

                                    //dRow["wageFrom"] = "BANK";

                                    //if (drvRow[strIDColName] != DBNull.Value && drvRow[strIDColName].ToString().Trim() != "")
                                    //{
                                    //    dRow["memMemberID"] = drvRow["memberid"].ToString().Trim();
                                    //    strMemBID = drvRow["memberid"].ToString().Trim();
                                    //    MemberDetail = MemberDetails(drvRow["memberid"].ToString());

                                    //}

                                }
                                if (rbPSPF.Checked)
                                {

                                    dRow["wageFrom"] = "PSPF";
                                    if (drvRow[strIDColName] != DBNull.Value && drvRow[strIDColName].ToString().Trim() != "")
                                    {
                                        dRow["memMemberID"] = drvRow["memberid"].ToString().Trim();
                                        strMemBID = drvRow["memberid"].ToString().Trim();
                                        MemberDetail = MemberDetails(drvRow["memberid"].ToString());

                                    }

                                }
                                if (rbTreasury.Checked)
                                {

                                    dRow["wageFrom"] = "TREASURY";
                                    if (drvRow[strIDColName] != DBNull.Value && drvRow[strIDColName].ToString().Trim() != "")
                                    {
                                        dRow["memEmployeeNo"] = drvRow["employeeno"].ToString().Trim();
                                        strMemBID = drvRow["employeeno"].ToString().Trim();
                                        MemberDetail = MemberDetails(drvRow["employeeno"].ToString());

                                    }

                                }

                                if (MemberDetail == null)
                                {

                                    dRow["memMemberID"] = strMemBID == "" ? iCount.ToString() : strMemBID;
                                    dRow["memEmployeeNo"] = strMemBID == "" ? iCount.ToString() : strMemBID;
                                    dRow["memNationalID"] = strMemBID == "" ? iCount.ToString() : strMemBID;
                                    dRow["memTSCNo"] = strMemBID == "" ? iCount.ToString() : strMemBID;

                                    dRow["memName"] = drvRow["membername"] == DBNull.Value ? "" : drvRow["membername"].ToString().Trim();
                                    dRow["processingdate"] = DateTime.Now.ToString("dd-MM-yyyy");
                                    dRow["wagecredit"] = drvRow["wagesamount"] == DBNull.Value ? "" : drvRow["wagesamount"].ToString().Trim();

                                    dRow["wagebalnace"] = 0;
                                    dRow["memWegeRemarks"] = "Invalid Member. Member details not exits!!";
                                    dRow["Remarks"] = "";
                                    dRow["luploaded"] = false;
                                    dRow["lValidMemmber"] = false;
                                    dRow["lApproved"] = false;
                                    dRow["documentType"] = dRow["wageFrom"];
                                    dRow["documentName"] = txtFileName_Excel.Text.Trim();
                                    dRow["createby"] = ClsSettings.username;
                                    dRow["createdate"] = DateTime.Now.ToString();
                                    dRow["updateby"] = ClsSettings.username;
                                    dRow["updatedate"] = DateTime.Now.ToString();
                                    dRow["lSelect"] = true;
                                    //drvRow["lWagesProcessed"] = false;
                                    iCount = iCount + 1;
                                }
                                else
                                {
                                    dRow["memMemberID"] = MemberDetail.memberid;
                                    dRow["memEmployeeNo"] = MemberDetail.employeeno;
                                    dRow["memNationalID"] = MemberDetail.nationalid;
                                    dRow["memTSCNo"] = MemberDetail.tscno;
                                    dRow["memName"] = MemberDetail.membername;
                                    dRow["processingdate"] = DateTime.Now.ToString("dd-MM-yyyy");
                                    dRow["wagecredit"] = drvRow["wagesamount"] == DBNull.Value ? "" : drvRow["wagesamount"].ToString().Trim(); ;
                                    dRow["wagebalnace"] = 0;
                                    dRow["memWegeRemarks"] = "";
                                    dRow["Remarks"] = "";
                                    dRow["luploaded"] = false;
                                    dRow["lValidMemmber"] = true;
                                    dRow["lApproved"] = false;
                                    //drvRow["lWagesProcessed"] = false;
                                    dRow["documentType"] = dRow["wageFrom"];
                                    dRow["documentName"] = txtFileName_Excel.Text.Trim();
                                    dRow["createby"] = ClsSettings.username;
                                    dRow["createdate"] = DateTime.Now.ToString();
                                    dRow["updateby"] = ClsSettings.username;
                                    dRow["updatedate"] = DateTime.Now.ToString();
                                    dRow["lSelect"] = true;
                                }

                                dtWages.Rows.Add(dRow);
                            }

                            else
                            {
                                dRow = dtWages.DefaultView[0].Row;

                                if (dRow["lApproved"] != DBNull.Value && dRow["lApproved"].ToString().Trim() != "" && Convert.ToBoolean(dRow["lApproved"]) == false)
                                {
                                    dRow.BeginEdit();
                                    dRow["wagecredit"] = drvRow["wagesamount"] == DBNull.Value ? "" : drvRow["wagesamount"].ToString().Trim();
                                    dRow["lSelect"] = true;
                                    dRow["updateby"] = ClsSettings.username;
                                    dRow["updatedate"] = DateTime.Now.ToString();
                                    dRow.EndEdit();
                                }

                            }
                        }
                        else
                        {
                            continue;
                        }


                    }



                }
                dtWages.DefaultView.RowFilter = "";
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private bool CheckDataTable(string MemberID)
        {
            try
            {
                DataView dtCheckView = new DataView();
                dtCheckView = mdsCreateDatatable.DefaultViewManager.CreateDataView(dtWages);
                dtCheckView.RowFilter = "";
                if (rbBank.Checked)
                {
                    dtCheckView.RowFilter = "memberid='" + MemberID + "'";
                }
                if (rbPSPF.Checked)
                {
                    dtCheckView.RowFilter = "memMemberID='" + MemberID + "'";
                }
                if (rbTreasury.Checked)
                {
                    dtCheckView.RowFilter = "memEmployeeNo='" + MemberID + "'";
                }
                if (dtCheckView.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }
        private ExpandoObject MemberDetails(string strId)
        {
            try
            {
                dynamic MemberDetail = new ExpandoObject();
                string strSqlQuery = "SELECT id, nationalid, memberid, employeeno, tscno, membername, wagesamount, wageseffectivedete FROM SNAT.dbo.T_Member Where livingstatus='L' ";
                if (rbBank.Checked)
                {
                    strSqlQuery = strSqlQuery + Environment.NewLine + " AND memberid='" + strId + "'";
                }
                if (rbPSPF.Checked)
                {
                    strSqlQuery = strSqlQuery + Environment.NewLine + " AND memberid='" + strId + "'";
                }
                if (rbTreasury.Checked)
                {
                    strSqlQuery = strSqlQuery + Environment.NewLine + " AND employeeno='" + strId + "'";
                }

                DataTable dtMember = new DataTable();
                dtMember = ClsDataLayer.GetDataTable(strSqlQuery);
                if (dtMember != null && dtMember.DefaultView.Count > 0)
                {
                    var mem = MemberDetail;
                    mem.id = dtMember.DefaultView[0]["id"] != DBNull.Value ? dtMember.DefaultView[0]["id"].ToString() : "";
                    mem.nationalid = dtMember.DefaultView[0]["nationalid"] != DBNull.Value ? dtMember.DefaultView[0]["nationalid"].ToString() : ""; ;
                    mem.memberid = dtMember.DefaultView[0]["memberid"] != DBNull.Value ? dtMember.DefaultView[0]["memberid"].ToString() : ""; ;
                    mem.employeeno = dtMember.DefaultView[0]["employeeno"] != DBNull.Value ? dtMember.DefaultView[0]["employeeno"].ToString() : ""; ;
                    mem.tscno = dtMember.DefaultView[0]["tscno"] != DBNull.Value ? dtMember.DefaultView[0]["tscno"].ToString() : ""; ;
                    mem.membername = dtMember.DefaultView[0]["membername"] != DBNull.Value ? dtMember.DefaultView[0]["membername"].ToString() : ""; ;
                    mem.wagesamount = dtMember.DefaultView[0]["wagesamount"] != DBNull.Value ? dtMember.DefaultView[0]["wagesamount"].ToString() : ""; ;
                    mem.wageseffectivedete = dtMember.DefaultView[0]["wageseffectivedete"] != DBNull.Value ? dtMember.DefaultView[0]["wageseffectivedete"].ToString() : ""; ;

                }
                else
                {
                    MemberDetail = null;
                }

                return MemberDetail;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return null;
            }
        }
        private void txtMonth_Excel_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ClsUtility.NumericKeyPress(e);
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void btnProcessPaymentExcel_Click(object sender, EventArgs e)
        {
            try
            {
                dtWages.DefaultView.RowFilter = "ISNULL(lSelect,0)=1";
                if (dtWages.DefaultView.Count <= 0) { ClsMessage.showMessage("Please select record to import Premium."); dtWages.DefaultView.RowFilter = ""; return; }
                foreach (DataRowView drvRow in dtWages.DefaultView)
                {
                    drvRow.BeginEdit();
                    drvRow["Remarks"] = txtRemaks.Text.Trim();
                    drvRow["luploaded"] = true;


                    if (rbPSPF.Checked)
                    {
                        drvRow["wageFrom"] = "PSPF";
                    }
                    else if (rbBank.Checked)
                    {
                        drvRow["wageFrom"] = "BANK";
                    }
                    else
                    {
                        drvRow["wageFrom"] = "TREASURY";
                    }
                    drvRow["updateby"] = ClsSettings.username;
                    drvRow["updatedate"] = DateTime.Now.ToString();


                    drvRow.EndEdit();
                }

                if (validateMonthYear() == false) { return; }
                string MonthYear = txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim();
                if (dtWages.GetChanges() != null)
                {
                    DataTable dtSave = new DataTable();
                    dtSave = dtWages.GetChanges();
                    strSqlQuery = "SELECT id, wageMonthYear, wageFrom, memNationalID, memMemberID, memEmployeeNo, memTSCNo, memName, processingdate,wagecredit, wagebalnace, memWegeRemarks, Remarks, luploaded, lValidMemmber,lWagesProcessed, lApproved, documentType, documentName, createby, createdate, updateby, updatedate FROM SNAT.dbo.T_WagesUpload where 1=2";
                    bool lReturn = false;
                    lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtSave);

                    dtWages.AcceptChanges();
                    if (lReturn == true)
                    {
                        ClsMessage.showSaveMessage();
                        ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                        // SetFormMode(ClsUtility.enmFormMode.NormalMode);
                        CreateDataTable(MonthYear);
                        FillList();

                        DataView dvMonth = new DataView();
                        dvMonth = mdsCreateDatatable.DefaultViewManager.CreateDataView(dtList);
                        dvMonth.RowFilter = "wageMonthYear='" + MonthYear + "'";
                        if (dvMonth.Count > 0)
                        {
                            clsEmail.lPrenumUpload(dvMonth[0]["wageMonthYear"].ToString().Trim(), dvMonth[0]["wageFrom"].ToString().Trim(), dvMonth[0]["TotalUpload"].ToString().Trim(), dvMonth[0]["ivalidMember"].ToString().Trim(), dvMonth[0]["iInvalidMember"].ToString().Trim());
                        }

                    }
                    else
                    {
                        ClsMessage.showMessage("Some problem occurs while saving please contact system administrator.");
                    }
                }
                dtWages.DefaultView.RowFilter = "";

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void rbPSPF_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtFileName_Excel.Text = "";
                cmbExcelSheet.Items.Clear();
                CreateDataTable(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
                lblCount.Visible = false;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void MasterTemplate_CellClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (e.Column.FieldName.ToUpper() == "lSelect".ToUpper())
                {
                    if (dtWages != null && dtWages.DefaultView.Count > 0)
                    {
                        DataRowView dRow = dtWages.DefaultView[e.RowIndex];

                        if (dRow["lApproved"] != DBNull.Value && dRow["lApproved"].ToString().Trim() != "" && Convert.ToBoolean(dRow["lApproved"]) == false)//&& Convert.ToBoolean(dRow["lValidMemmber"]) == true
                        {
                            dRow.BeginEdit();

                            dRow["lSelect"] = !Convert.ToBoolean(dRow["lSelect"]);
                            dRow.EndEdit();
                        }
                    }
                }



            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                switch (((Button)sender).Name.ToString().ToUpper())
                {
                    case "BTNSELECTALL":
                        Select_UnSelect(true);
                        break;
                    case "BTNUNSELECTALL":
                        Select_UnSelect(false);
                        break;

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void Select_UnSelect(bool lValue)
        {
            try
            {
                if (dtWages != null && dtWages.DefaultView.Count > 0)
                {
                    dtWages.DefaultView.RowFilter = "ISNULL(lApproved,0)=0";
                    foreach (DataRowView dRow in dtWages.DefaultView)
                    {
                        //if (Convert.ToBoolean(dRow["lValidMemmber"]) == true)
                        //{
                        dRow.BeginEdit();

                        dRow["lSelect"] = lValue;
                        dRow.EndEdit();
                        //}
                    }
                    dtWages.DefaultView.RowFilter = "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void MasterTemplate_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {

        }
        private void MasterTemplate_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                if (dtList != null && dtList.DefaultView.Count > 0)
                {
                    DataRowView dRow = dtList.DefaultView[grdList.CurrentRow.Index];

                    if ((dRow["wageMonthYear"] != DBNull.Value && dRow["wageMonthYear"].ToString().Trim() != "") && (dRow["wageFrom"] != DBNull.Value && dRow["wageFrom"].ToString().Trim() != ""))//&& Convert.ToBoolean(dRow["lValidMemmber"]) == true
                    {
                        string[] strsplitwageMonthYear = null;
                        strsplitwageMonthYear = dRow["wageMonthYear"].ToString().Trim().Split('-');
                        if (strsplitwageMonthYear.Length > 0)
                        {
                            txtMonth_Excel.Text = strsplitwageMonthYear[0];
                            txtYear_Excel.Text = strsplitwageMonthYear[1];
                        }

                        CreateDataTable(dRow["wageMonthYear"].ToString().Trim());
                        switch (dRow["wageFrom"].ToString().Trim().ToUpper())
                        {

                            case "BANK":
                                rbBank.Checked = true;
                                break;
                            case "PSPF":
                                rbPSPF.Checked = true;
                                break;
                            case "TREASURY":
                                rbTreasury.Checked = true;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void ProcessBankDetails(DataTable dtBankExcel)
        {
            try
            {

                DataView dvBankExcel = new DataView();
                mdsCreateDatatable = new DataSet();
                dvBankExcel = mdsCreateDatatable.DefaultViewManager.CreateDataView(dtBankExcel);
                dvBankExcel.RowFilter = "TransactionTypeCode in ('CMI','TRF') and IsNull(TransactionTypeCode,'')<>''";

                foreach (DataRowView drvExcel in dvBankExcel)
                {
                    string strMember = "";
                    strMember = drvExcel["Description"].ToString();
                    string strBankReferenceNo = "";
                    strBankReferenceNo = drvExcel["BankReferenceNo"].ToString();

                    string[] strMemberSplit = strMember.Split(' ');
                    string[] strBankReferenceNoSplit = strBankReferenceNo.Split(' ');

                    foreach (var strMemberID in strMemberSplit)
                    {
                        if (strMemberID != null && strMemberID.Length > 0)
                        {
                            if (ClsUtility.IsNumeric(strMemberID))
                            {

                            }
                        }
                    }


                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtMonth_Excel_Validating(object sender, CancelEventArgs e)
        {
            //FilterMonthYear();
        }

        private void txtYear_Excel_Validating(object sender, CancelEventArgs e)
        {
            FilterMonthYear();
        }

        private void FilterMonthYear()
        {
            try
            {


                cmbExcelSheet.Items.Clear();
                txtFileName_Excel.Text = "";
                //  grdImport_Excel.Rows.Clear();
                dtWages.Rows.Clear();
                if (txtMonth_Excel.Text.Trim() != "" && txtYear_Excel.Text.Trim() != "")
                {
                    string strMonthYear = txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim();

                    DataView dvFilter = new DataView();
                    dvFilter = mdsCreateDatatable.DefaultViewManager.CreateDataView(dtList);
                    dvFilter.RowFilter = "wageMonthYear='" + strMonthYear + "'";
                    if (dvFilter.Count > 0)
                    {
                        int iRowIndex = dtList.Rows.IndexOf(dvFilter[0].Row);
                        this.grdList.CurrentRow = this.grdList.Rows[iRowIndex];
                        //grdList.Rows[iRowIndex].IsSelected = true;
                        CreateDataTable(strMonthYear);
                    }
                }


            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void grdImport_Excel_ViewRowFormatting(object sender, RowFormattingEventArgs e)
        {
            //try
            //{
            //    if (dtWages != null && dtWages.DefaultView.Count > 0)
            //    {


            //        //DataView dtCheckView = new DataView();
            //        //dtCheckView = mdsCreateDatatable.DefaultViewManager.CreateDataView(dtWages);
            //        //dtCheckView.RowFilter = "";

            //        if ((e.RowElement.RowInfo.Cells[11].Value != DBNull.Value) || Convert.ToBoolean(e.RowElement.RowInfo.Cells[11].Value) == false)
            //        {
            //            e.RowElement.DrawFill = true;
            //            e.RowElement.GradientStyle = GradientStyles.Solid;
            //            e.RowElement.BackColor = Color.LightPink;
            //        }
            //        else if ((e.RowElement.RowInfo.Cells[12].Value != DBNull.Value) && (Convert.ToBoolean(e.RowElement.RowInfo.Cells[12].Value) == true))
            //        {
            //            e.RowElement.DrawFill = true;
            //            e.RowElement.GradientStyle = GradientStyles.Solid;
            //            e.RowElement.BackColor = Color.LightGoldenrodYellow;
            //        }
            //        else if ((e.RowElement.RowInfo.Cells[13].Value != DBNull.Value) && (Convert.ToBoolean(e.RowElement.RowInfo.Cells[13].Value) == true))
            //        {
            //            e.RowElement.DrawFill = true;
            //            e.RowElement.GradientStyle = GradientStyles.Solid;
            //            e.RowElement.BackColor = Color.LightSeaGreen;
            //        }
            //        else
            //        {
            //            e.RowElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
            //            e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
            //            e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    ClsMessage.ProjectExceptionMessage(ex);
            //}
        }
    }

}
