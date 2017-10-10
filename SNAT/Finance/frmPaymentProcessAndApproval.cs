using SNAT.Comman_Classes;
using SNAT.Comman_Form;
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
using System.Data.SqlClient;
using SNAT.CommanClass;

namespace SNAT.Finance
{
    public partial class frmPaymentProcessAndApproval : Form
    {
        DataTable dtWages = new DataTable();
        DataTable dtList = new DataTable();
        DataTable dtWagesUpload = new DataTable();
        string strSqlQuery = "";
        DataSet mds = new DataSet();
        DataView dvWages = new DataView();
        DataSet mdsCreateDatatable = new DataSet();
        public frmPaymentProcessAndApproval()
        {
            InitializeComponent();
            FillWagesUpload("");
            FillList();
        }

        private void frmPaymentProcessAndApproval_Load(object sender, EventArgs e)
        {
            try
            {
                FillMemberPayment(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
                FillWagesUpload(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
                FillList();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void FillWagesUpload(string strMonthYear)
        {
            try
            {
                strSqlQuery = "SELECT  id, wageMonthYear, wageFrom, memNationalID, memMemberID, memEmployeeNo, memTSCNo, memName, processingdate, wagecredit," + Environment.NewLine +
                             " wagePending, wagebalnace, memWegeRemarks, cProcessed, Remarks,cApprovalRemarks, luploaded, lProcessed, ProcessedBy, Processeddate, lApproved, ApprovedBy," + Environment.NewLine +
                             " ApprovedDate, createby, createdate, updateby, updatedate FROM SNAT.dbo.T_MemberWegesProcess where wageMonthYear='" + strMonthYear + "'";

                dtWagesUpload = new DataTable();
                dtWagesUpload = ClsDataLayer.GetDataTable(strSqlQuery);
                dgrdProcessedPayment.DataSource = dtWagesUpload.DefaultView;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void FillList()
        {
            try
            {

                dtList = new DataTable();
                strSqlQuery = "SELECT DISTINCT  wp.wageMonthYear, STUFF((SELECT DISTINCT ','+twu.wageFrom FROM SNAT.dbo.T_MemberWegesProcess  AS twu FOR xml PATH('') ), 1, 1, '') AS wageFrom,  CASE WHEN ISNULL(wp.cProcessed,'')='P' THEN 'Processed' WHEN ISNULL(wp.cProcessed,'')='A' THEN 'Approved'  ELSE '' END cProcessed, wp.Remarks, wp.luploaded, wp.lProcessed, wp.lApproved,wp.cApprovalRemarks FROM SNAT.dbo.T_MemberWegesProcess wp (nolock) Order by wp.wageMonthYear";

                dtList = ClsDataLayer.GetDataTable(strSqlQuery);
                grdList.DataSource = dtList.DefaultView;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void FillMemberPayment(string MonthYear)
        {
            try
            {


                dtWages = new DataTable();
                strSqlQuery = " SELECT wu.id, wu.wageMonthYear, wu.wageFrom, wu.memNationalID, wu.memMemberID, wu.memEmployeeNo, wu.memTSCNo, wu.memName, wu.processingdate,wu.wagecredit," + Environment.NewLine +
                              " wu.wagebalnace, wu.memWegeRemarks, wu.Remarks, wu.luploaded, wu.lValidMemmber,wu.lWagesProcessed, wu.lApproved , wu.documentType, wu.documentName," + Environment.NewLine +
                              " wu.createby, wu.createdate, wu.updateby, wu.updatedate,wp.cProcessed,wp.lProcessed,wp.lApproved  lWpApproved " + Environment.NewLine +
                              " FROM SNAT.dbo.T_WagesUpload wu (nolock)" + Environment.NewLine +
                              " LEFT OUTER JOIN SNAT.dbo.T_MemberWegesProcess wp ON wp.memNationalID = wu.memNationalID AND wp.memMemberID = wu.memMemberID AND wp.memEmployeeNo = wu.memEmployeeNo AND wp.wageMonthYear = wu.wageMonthYear" + Environment.NewLine +
                              " where wu.wageMonthYear='" + MonthYear + "'";

                dtWages = ClsDataLayer.GetDataTable(strSqlQuery);
                DataColumn dclSelect = new DataColumn("lSelect", typeof(bool));
                dclSelect.DefaultValue = false;
                dclSelect.ReadOnly = false;
                dtWages.Columns.Add(dclSelect);
                dclSelect = new DataColumn("lValidAmount", typeof(bool));
                dclSelect.DefaultValue = DBNull.Value;
                dclSelect.ReadOnly = false;
                dtWages.Columns.Add(dclSelect);
                grdMemberPayemnt.DataSource = dtWages.DefaultView;
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
                    ClsMessage.showMessage("Please enter upload month.", MessageBoxIcon.Information);
                    txtMonth_Excel.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtYear_Excel.Text.Trim()))
                {
                    ClsMessage.showMessage("Please enter upload year.", MessageBoxIcon.Information);
                    txtYear_Excel.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(txtYear_Excel.Text.Trim()) == false && Convert.ToInt32(txtYear_Excel.Text.Trim()) <= 1980)
                {
                    ClsMessage.showMessage("Year cannot be less than 1980.", MessageBoxIcon.Information);
                    txtYear_Excel.Focus();
                    return false;
                }

                string dDate = txtYear_Excel.Text.Trim() + "-" + txtMonth_Excel.Text.Trim() + "-01";

                if (!ClsUtility.IsValidDate(dDate))
                {
                    ClsMessage.showMessage("Please enter correct month-year.(MM-YYYY)", MessageBoxIcon.Information);
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
        private void btnMonthPick_Click(object sender, EventArgs e)
        {
            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT DISTINCT  twu.wageMonthYear, STUFF((SELECT DISTINCT ','+twu.wageFrom FROM SNAT.dbo.T_WagesUpload AS twu FOR xml PATH('') ), 1, 1, '') AS wageFrom FROM SNAT.dbo.T_WagesUpload AS twu (nolock)";
                frmsrch.infSqlWhereCondtion = "";
                frmsrch.infSqlOrderBy = " wageMonthYear , wageFrom ";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Premium Month-Year ....";
                frmsrch.infCodeFieldName = "wageMonthYear";
                frmsrch.infDescriptionFieldName = "wageFrom";
                frmsrch.infGridFieldName = " wageMonthYear, wageFrom";
                frmsrch.infGridFieldCaption = " Premium Month-Year,Premium Form";
                frmsrch.infGridFieldSize = "200,200";
                frmsrch.ShowDialog(this);
                if (frmsrch.DialogResult == DialogResult.OK && frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()))
                {
                    DataView dvDesg = new DataView();
                    dvDesg = frmsrch.infSearchReturnDataView;
                    dvDesg.RowFilter = "lSelect=1";
                    //txtDeptCode.Text = string.IsNullOrEmpty(dvDesg[0]["deptcode"].ToString()) == true ? "" : dvDesg[0]["deptcode"].ToString();
                    //txtDeptDesc.Text = string.IsNullOrEmpty(dvDesg[0]["deptname"].ToString()) == true ? "" : dvDesg[0]["deptname"].ToString();
                    if (string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) == false)
                    {
                        string strMonthYear = frmsrch.infCodeFieldText.Trim();
                        string[] spltStrMonthYear = strMonthYear.Split('-');
                        txtMonth_Excel.Text = spltStrMonthYear[0].ToString();
                        txtYear_Excel.Text = spltStrMonthYear[1].ToString();
                        FillMemberPayment(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
                        FillWagesUpload(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnSelectFile_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateMonthYear() == false) { return; }

                FillMemberPayment(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
                FillWagesUpload(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnProcessPaymentExcel_Click(object sender, EventArgs e)
        {
            SqlTransaction sqlTran = null;
            try
            {

                if (string.IsNullOrEmpty(txtRemaks.Text.Trim()) == true) { ClsMessage.ProjectExceptionMessage("Processes remarks cannot be blank."); txtRemaks.Focus(); return; }

                mds = new DataSet();
                dvWages = new DataView();
                dvWages = mds.DefaultViewManager.CreateDataView(dtWages);
                dvWages.RowFilter = "IsNull(lSelect,0)=1  and IsNull(lValidMemmber,0)=1 ";
                if (dvWages.Count <= 0) { ClsMessage.ProjectExceptionMessage("Please select member(s) to process payment."); return; }


                if (ClsMessage.showQuestionMessage("Are you sure want to processed Premium?") == DialogResult.No) { return; }
                ProcessPayment();


                if (dtWagesUpload != null && dtWagesUpload.DefaultView.Count > 0)
                {
                    if (dtWagesUpload.GetChanges() != null)
                    {

                        if (ClsDataLayer.dbConn.State == ConnectionState.Closed) { ClsDataLayer.openConnection(); }
                        sqlTran = ClsDataLayer.dbConn.BeginTransaction();

                        if (dtWages.GetChanges() != null)
                        {
                            strSqlQuery = " SELECT wu.id, wu.wageMonthYear, wu.wageFrom, wu.memNationalID, wu.memMemberID, wu.memEmployeeNo, wu.memTSCNo, wu.memName, wu.processingdate,wu.wagecredit," + Environment.NewLine +
                             " wu.wagebalnace, wu.memWegeRemarks, wu.Remarks, wu.luploaded, wu.lValidMemmber,wu.lWagesProcessed, wu.lApproved, wu.documentType, wu.documentName," + Environment.NewLine +
                             " wu.createby, wu.createdate, wu.updateby, wu.updatedate " + Environment.NewLine +
                             " FROM SNAT.dbo.T_WagesUpload wu (nolock)" + Environment.NewLine +
                             " where wu.wageMonthYear='" + txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim() + "'";

                            ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtWages, sqlTran);
                        }

                        strSqlQuery = "SELECT  id, wageMonthYear, wageFrom, memNationalID, memMemberID, memEmployeeNo, memTSCNo, memName, processingdate, wagecredit," + Environment.NewLine +
                                     " wagePending, wagebalnace, memWegeRemarks, cProcessed, Remarks, luploaded, lProcessed, ProcessedBy, Processeddate, lApproved, ApprovedBy," + Environment.NewLine +
                                     " ApprovedDate, createby, createdate, updateby, updatedate FROM SNAT.dbo.T_MemberWegesProcess where wageMonthYear='" + txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim() + "'";

                        ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtWagesUpload, sqlTran);

                        sqlTran.Commit();
                        if (ClsDataLayer.dbConn.State == ConnectionState.Open) { ClsDataLayer.clsoeConnection(); }

                        ClsMessage.showMessage("Process payment successfully!!", MessageBoxIcon.Information);
                        FillMemberPayment(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
                        FillWagesUpload(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
                        FillList();

                        DataView dvMonth = new DataView();
                        dvMonth = mdsCreateDatatable.DefaultViewManager.CreateDataView(dtList);
                        dvMonth.RowFilter = "wageMonthYear='" + txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim() + "'";
                        if (dvMonth.Count > 0)
                        {
                            clsEmail.lPrenumProcess(dvMonth[0]["wageMonthYear"].ToString().Trim(), dvMonth[0]["wageFrom"].ToString().Trim(), dvMonth[0]["cProcessed"].ToString().Trim());
                        }

                    }
                }

            }
            catch (Exception ex)
            {

                if (sqlTran != null) { sqlTran.Rollback(); }
                ClsMessage.ProjectExceptionMessage(ex);
                if (ClsDataLayer.dbConn.State == ConnectionState.Open) { ClsDataLayer.clsoeConnection(); }
            }
        }
        private void ProcessPayment()
        {
            try
            {
                FillWagesUpload(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
                dtWages.DefaultView.RowFilter = "IsNull(lSelect,0)=1";
                foreach (DataRowView drvWages in dtWages.DefaultView)
                {

                    //strSqlQuery= " SELECT DISTINCT  tb.membernationalid,tb.memberid, tb.membername,tm.nationalid,tm.memberid, tm.employeeno, tm.membername, tm.tscno,tb.relationship" + Environment.NewLine +
                    //             " FROM SNAT.dbo.T_Beneficiary tb (nolock) INNER JOIN SNAT.dbo.T_Member (nolock) tm ON tm.nationalid = tb.membernationalid AND tm.memberid = tb.memberid" + Environment.NewLine +
                    //             " WHERE ISNULL(tm.nationalid, '')<> '' and "


                    strSqlQuery = " SELECT DISTINCT  tb.membernationalid,tb.memberid, tb.membername,tb.relationship" + Environment.NewLine +
                                 " FROM SNAT.dbo.T_Beneficiary tb (nolock) " + Environment.NewLine +
                                 " WHERE ISNULL(tb.membernationalid, '')<> '' and tb.membernationalid='" + drvWages["memNationalID"].ToString() + "' and tb.memberid='" + drvWages["memMemberID"].ToString() + "' ";

                    DataTable dtBeneficiry = new DataTable();
                    dtBeneficiry = ClsDataLayer.GetDataTable(strSqlQuery);
                    int iWifeCount = 0;
                    int iOtherCount = 0;
                    decimal dWifeAmounValidate = 0;
                    decimal dOtherAmounValidate = 0;
                    decimal dAmountValidate = 0;
                    decimal dWagesAmount = 0;
                    decimal dWagesPending = 0;
                    decimal dWageExtra = 0;
                    if (dtBeneficiry != null && dtBeneficiry.DefaultView.Count > 0)
                    {

                        dtBeneficiry.DefaultView.RowFilter = "relationship='W'";
                        if (dtBeneficiry.DefaultView.Count > 0) { iWifeCount = dtBeneficiry.DefaultView.Count; }
                        dtBeneficiry.DefaultView.RowFilter = "";
                        dtBeneficiry.DefaultView.RowFilter = "relationship<>'W'";
                        if (dtBeneficiry.DefaultView.Count > 0) { iOtherCount = dtBeneficiry.DefaultView.Count; }



                    }

                    if (iWifeCount > 0)
                    {
                        dWifeAmounValidate = 23 * iWifeCount;
                    }
                    if (iOtherCount > 0)
                    {
                        dOtherAmounValidate = 10 * iOtherCount;
                    }
                    dAmountValidate = dWifeAmounValidate + dOtherAmounValidate;
                    if (drvWages["wagecredit"] != null && Convert.ToDecimal(drvWages["wagecredit"]) > 0)
                    {
                        dWagesAmount = Convert.ToDecimal(drvWages["wagecredit"]);
                    }

                    if (dAmountValidate > dWagesAmount)
                    {
                        drvWages["lValidAmount"] = false;
                    }

                    else
                    {
                        drvWages["lValidAmount"] = true;
                        drvWages["lWagesProcessed"] = true;
                        dWagesPending = dAmountValidate - dWagesAmount;
                        dWageExtra = dWagesAmount - dAmountValidate;

                        if (CheckExistsUpload(drvWages["memNationalID"].ToString()) == true)
                        {

                            DataRowView dRowView = dtWagesUpload.DefaultView[0];
                            dRowView.BeginEdit();

                            dRowView["processingdate"] = DateTime.Now;
                            dRowView["wagecredit"] = drvWages["wagecredit"].ToString();
                            dRowView["wagePending"] = dWagesPending > 0 ? dWagesPending : 0;
                            dRowView["wagebalnace"] = dWageExtra;
                            dRowView["memWegeRemarks"] = drvWages["memWegeRemarks"].ToString();
                            dRowView["cProcessed"] = "P";
                            dRowView["Remarks"] = txtRemaks.Text.Trim();
                            dRowView["luploaded"] = true;
                            dRowView["lProcessed"] = true;
                            dRowView["ProcessedBy"] = ClsSettings.username;
                            dRowView["Processeddate"] = DateTime.Now;

                            dRowView["updateby"] = ClsSettings.username;
                            dRowView["updatedate"] = DateTime.Now;

                            dRowView.EndEdit();
                        }
                        else
                        {
                            DataRow dRow = dtWagesUpload.NewRow();
                            dRow["wageMonthYear"] = drvWages["wageMonthYear"].ToString();
                            dRow["wageFrom"] = drvWages["wageFrom"].ToString();
                            dRow["memNationalID"] = drvWages["memNationalID"].ToString();
                            dRow["memMemberID"] = drvWages["memMemberID"].ToString();
                            dRow["memEmployeeNo"] = drvWages["memEmployeeNo"].ToString();
                            dRow["memTSCNo"] = drvWages["memTSCNo"].ToString();
                            dRow["memName"] = drvWages["memName"].ToString();
                            dRow["processingdate"] = DateTime.Now;
                            dRow["wagecredit"] = drvWages["wagecredit"].ToString();
                            dRow["wagePending"] = dWagesPending > 0 ? dWagesPending : 0;
                            dRow["wagebalnace"] = dWageExtra;
                            dRow["memWegeRemarks"] = drvWages["memWegeRemarks"].ToString();
                            dRow["cProcessed"] = "P";
                            dRow["Remarks"] = txtRemaks.Text.Trim();
                            dRow["luploaded"] = true;
                            dRow["lProcessed"] = true;
                            dRow["ProcessedBy"] = ClsSettings.username;
                            dRow["Processeddate"] = DateTime.Now;
                            dRow["createby"] = ClsSettings.username;
                            dRow["createdate"] = DateTime.Now;
                            dRow["updateby"] = ClsSettings.username;
                            dRow["updatedate"] = DateTime.Now;
                            dtWagesUpload.Rows.Add(dRow);

                        }


                    }

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private bool CheckExistsUpload(string memNationalID)
        {

            try
            {
                dtWagesUpload.DefaultView.RowFilter = "";

                dtWagesUpload.DefaultView.RowFilter = "memNationalID='" + memNationalID + "'";

                if (dtWagesUpload.DefaultView.Count > 0)
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

        private void grdMemberPayemnt_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            try
            {
                if (e.RowElement.RowInfo.Index < 0) { return; }
                if ((dtWages.DefaultView[e.RowElement.RowInfo.Index]["lValidAmount"] != DBNull.Value) && (Convert.ToBoolean(dtWages.DefaultView[e.RowElement.RowInfo.Index]["lValidAmount"])) == false)
                {
                    e.RowElement.DrawFill = true;
                    e.RowElement.GradientStyle = GradientStyles.Solid;
                    e.RowElement.BackColor = Color.LightPink;
                }
                else if ((dtWages.DefaultView[e.RowElement.RowInfo.Index]["lProcessed"] != DBNull.Value) && (Convert.ToBoolean(dtWages.DefaultView[e.RowElement.RowInfo.Index]["lProcessed"]) == true) && (Convert.ToString(dtWages.DefaultView[e.RowElement.RowInfo.Index]["cProcessed"]).ToUpper() == "P"))
                {
                    e.RowElement.DrawFill = true;
                    e.RowElement.GradientStyle = GradientStyles.Solid;
                    e.RowElement.BackColor = Color.LightGoldenrodYellow;
                }
                else if ((dtWages.DefaultView[e.RowElement.RowInfo.Index]["lWpApproved"] != DBNull.Value) && (Convert.ToBoolean(dtWages.DefaultView[e.RowElement.RowInfo.Index]["lWpApproved"]) == true) && (Convert.ToString(dtWages.DefaultView[e.RowElement.RowInfo.Index]["cProcessed"]).ToUpper() == "A"))
                {
                    e.RowElement.DrawFill = true;
                    e.RowElement.GradientStyle = GradientStyles.Solid;
                    e.RowElement.BackColor = Color.LightSeaGreen;
                }
                else
                {
                    e.RowElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        private void MasterTemplate_RowFormatting(object sender, RowFormattingEventArgs e)
        {

            try
            {
                if (e.RowElement.RowInfo.Index < 0) { return; }
                if (dtWagesUpload != null && dtWagesUpload.DefaultView.Count > 0)
                {
                    if ((dtWagesUpload.DefaultView[e.RowElement.RowInfo.Index]["lProcessed"] != DBNull.Value) && (Convert.ToBoolean(dtWagesUpload.DefaultView[e.RowElement.RowInfo.Index]["lProcessed"]) == true) && (Convert.ToString(dtWagesUpload.DefaultView[e.RowElement.RowInfo.Index]["cProcessed"]).ToUpper() == "P"))
                    {
                        e.RowElement.DrawFill = true;
                        e.RowElement.GradientStyle = GradientStyles.Solid;
                        e.RowElement.BackColor = Color.LightGoldenrodYellow;
                    }
                    else if ((dtWagesUpload.DefaultView[e.RowElement.RowInfo.Index]["lApproved"] != DBNull.Value) && (Convert.ToBoolean(dtWagesUpload.DefaultView[e.RowElement.RowInfo.Index]["lApproved"]) == true) && (Convert.ToString(dtWagesUpload.DefaultView[e.RowElement.RowInfo.Index]["cProcessed"]).ToUpper() == "A"))
                    {
                        e.RowElement.DrawFill = true;
                        e.RowElement.GradientStyle = GradientStyles.Solid;
                        e.RowElement.BackColor = Color.LightSeaGreen;
                    }
                    else
                    {
                        e.RowElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        private void grdList_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            try
            {
                if (e.RowElement.RowInfo.Index < 0) { return; }
                if (dtList != null && dtList.DefaultView.Count > 0)
                {
                    if ((dtList.DefaultView[e.RowElement.RowInfo.Index]["lProcessed"] != DBNull.Value) && (Convert.ToBoolean(dtList.DefaultView[e.RowElement.RowInfo.Index]["lProcessed"]) == true) && (Convert.ToString(dtList.DefaultView[e.RowElement.RowInfo.Index]["cProcessed"]).ToUpper() == "PROCESSED"))
                    {
                        e.RowElement.DrawFill = true;
                        e.RowElement.GradientStyle = GradientStyles.Solid;
                        e.RowElement.BackColor = Color.LightGoldenrodYellow;
                    }
                    else if ((dtList.DefaultView[e.RowElement.RowInfo.Index]["lApproved"] != DBNull.Value) && (Convert.ToBoolean(dtList.DefaultView[e.RowElement.RowInfo.Index]["lApproved"]) == true) && (Convert.ToString(dtList.DefaultView[e.RowElement.RowInfo.Index]["cProcessed"]).ToUpper() == "APPROVED"))
                    {
                        e.RowElement.DrawFill = true;
                        e.RowElement.GradientStyle = GradientStyles.Solid;
                        e.RowElement.BackColor = Color.LightSeaGreen;
                    }
                    else
                    {
                        e.RowElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        private void grdList_SelectionChanging(object sender, GridViewSelectionCancelEventArgs e)
        {
            try
            {
                if (dtList != null && dtList.DefaultView.Count > 0)
                {
                    int iRowindex = 0;
                    iRowindex = grdList.CurrentRow.Index;

                    string strMonthYear = dtList.DefaultView[iRowindex]["wageMonthYear"] == DBNull.Value ? "" : dtList.DefaultView[iRowindex]["wageMonthYear"].ToString();
                    string[] spltStrMonthYear = strMonthYear.Split('-');
                    txtMonth_Excel.Text = spltStrMonthYear[0].ToString();
                    txtYear_Excel.Text = spltStrMonthYear[1].ToString();
                    FillMemberPayment(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
                    FillWagesUpload(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());

                    txtRemaks.Text = dtList.DefaultView[iRowindex]["Remarks"] == DBNull.Value ? "" : dtList.DefaultView[iRowindex]["Remarks"].ToString();
                    txtApprovalRemarks.Text = dtList.DefaultView[iRowindex]["cApprovalRemarks"] == DBNull.Value ? "" : dtList.DefaultView[iRowindex]["cApprovalRemarks"].ToString();
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            SqlTransaction sqlTran = null;
            try
            {
                if (ClsUtility.lsAutorizeForApproval(ClsUtility.enuAutorizeCheck.Premium) == false) { return; }
                mds = new DataSet();
                dvWages = new DataView();
                dvWages = mds.DefaultViewManager.CreateDataView(dtWages);
                dvWages.RowFilter = "IsNull(lSelect,0)=1  and IsNull(lValidMemmber,0)=1 ";
                if (dvWages.Count <= 0) { ClsMessage.ProjectExceptionMessage("Please select member(s) to process approval."); return; }

                UpdateApproval();

                if (dtWagesUpload != null && dtWagesUpload.DefaultView.Count > 0)
                {
                    if (dtWagesUpload.GetChanges() != null)
                    {
                        if (ClsDataLayer.dbConn.State == ConnectionState.Closed) { ClsDataLayer.openConnection(); }
                        sqlTran = ClsDataLayer.dbConn.BeginTransaction();

                        if (dtWages.GetChanges() != null)
                        {
                            strSqlQuery = " SELECT wu.id, wu.wageMonthYear, wu.wageFrom, wu.memNationalID, wu.memMemberID, wu.memEmployeeNo, wu.memTSCNo, wu.memName, wu.processingdate,wu.wagecredit," + Environment.NewLine +
                             " wu.wagebalnace, wu.memWegeRemarks, wu.Remarks, wu.luploaded, wu.lValidMemmber,wu.lWagesProcessed, wu.lApproved, wu.documentType, wu.documentName," + Environment.NewLine +
                             " wu.createby, wu.createdate, wu.updateby, wu.updatedate " + Environment.NewLine +
                             " FROM SNAT.dbo.T_WagesUpload wu (nolock)" + Environment.NewLine +
                             " where wu.wageMonthYear='" + txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim() + "'";

                            ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtWages, sqlTran);
                        }

                        strSqlQuery = "SELECT  id, wageMonthYear, wageFrom, memNationalID, memMemberID, memEmployeeNo, memTSCNo, memName, processingdate, wagecredit," + Environment.NewLine +
                                     " wagePending, wagebalnace, memWegeRemarks, cProcessed, Remarks, cApprovalRemarks,luploaded, lProcessed, ProcessedBy, Processeddate, lApproved, ApprovedBy," + Environment.NewLine +
                                     " ApprovedDate, createby, createdate, updateby, updatedate FROM SNAT.dbo.T_MemberWegesProcess where wageMonthYear='" + txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim() + "'";

                        ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtWagesUpload, sqlTran);

                        sqlTran.Commit();
                        if (ClsDataLayer.dbConn.State == ConnectionState.Open) { ClsDataLayer.clsoeConnection(); }

                        ClsMessage.showMessage("Monthly Premium approved successfully!!", MessageBoxIcon.Information);
                        FillMemberPayment(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
                        FillWagesUpload(txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim());
                        FillList();

                        DataView dvMonth = new DataView();
                        dvMonth = mdsCreateDatatable.DefaultViewManager.CreateDataView(dtList);
                        dvMonth.RowFilter = "wageMonthYear='" + txtMonth_Excel.Text.Trim() + "-" + txtYear_Excel.Text.Trim() + "'";
                        if (dvMonth.Count > 0)
                        {
                            clsEmail.lPrenumApproved(dvMonth[0]["wageMonthYear"].ToString().Trim(), dvMonth[0]["wageFrom"].ToString().Trim(), dvMonth[0]["cProcessed"].ToString().Trim());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                if (sqlTran != null) { sqlTran.Rollback(); }
                ClsMessage.ProjectExceptionMessage(ex);
                if (ClsDataLayer.dbConn.State == ConnectionState.Open) { ClsDataLayer.clsoeConnection(); }

            }
        }
        private void UpdateApproval()
        {
            try
            {
                //strSqlQuery = "SELECT  id, wageMonthYear, wageFrom, memNationalID, memMemberID, memEmployeeNo, memTSCNo, memName, processingdate, wagecredit," + Environment.NewLine +
                //    " wagePending, wagebalnace, memWegeRemarks, cProcessed, Remarks, luploaded, lProcessed, ProcessedBy, Processeddate, lApproved, ApprovedBy," + Environment.NewLine +
                //    " ApprovedDate, createby, createdate, updateby, updatedate FROM SNAT.dbo.T_MemberWegesProcess where 1=2";

                if (string.IsNullOrEmpty(txtApprovalRemarks.Text.Trim()) == true) { ClsMessage.ProjectExceptionMessage("Approval remarks cannot be blank."); txtApprovalRemarks.Focus(); return; }
                if (dtWagesUpload != null && dtWagesUpload.DefaultView.Count > 0)
                {
                    if (ClsMessage.showQuestionMessage("Are you sure want to approved processed Premium?") == DialogResult.No) { return; }

                    foreach (DataRowView drvApp in dtWagesUpload.DefaultView)
                    {
                        drvApp.BeginEdit();
                        drvApp["cProcessed"] = "A";
                        drvApp["lApproved"] = true;
                        drvApp["cApprovalRemarks"] = txtApprovalRemarks.Text.Trim();
                        drvApp["ApprovedBy"] = ClsSettings.username;
                        drvApp["ApprovedDate"] = DateTime.Now;
                        drvApp["updateby"] = ClsSettings.username;
                        drvApp["updatedate"] = DateTime.Now;
                        drvApp.EndEdit();
                    }
                    //SELECT id, wageMonthYear, wageFrom, memNationalID, memMemberID, memEmployeeNo, memTSCNo, memName, processingdate,wagecredit, wagebalnace, memWegeRemarks, Remarks, luploaded, lValidMemmber,
                    //lWagesProcessed, lApproved, documentType, documentName, createby, createdate, updateby, updatedate
                    foreach (DataRowView drvweg in dtWages.DefaultView)
                    {
                        drvweg.BeginEdit();
                        drvweg["lApproved"] = true;
                        drvweg["updateby"] = ClsSettings.username;
                        drvweg["updatedate"] = DateTime.Now;
                        drvweg.EndEdit();
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                dtWages.DefaultView.RowFilter = "";
                if (dtWages != null && dtWages.DefaultView.Count > 0)
                {
                    //dtWagesUpload.DefaultView.RowFilter = "IsNull(lApproved,0)=0 and cProcessed<>'A' ";
                    //if (dtWagesUpload.DefaultView.Count <= 0) { ClsMessage.showMessage("All record approved , can not select approved record", MessageBoxIcon.Information); return; }
                    //dtWagesUpload.DefaultView.RowFilter = "";
                    if (((Button)(sender)).Name.ToUpper() == "BTNSELECTALL")
                    {
                        dtWages.DefaultView.RowFilter = "IsNull(lApproved,0)=0  and IsNull(lValidMemmber,0)=1 ";
                        if (dtWages.DefaultView.Count <= 0) { ClsMessage.showMessage("All record approved , can not select approved record", MessageBoxIcon.Information); dtWages.DefaultView.RowFilter = ""; return; }
                        foreach (DataRowView drvAppr in dtWages.DefaultView)
                        {
                            if ((drvAppr["lApproved"] != DBNull.Value) && (Convert.ToBoolean(drvAppr["lApproved"]) == false))
                            {
                                drvAppr.BeginEdit();
                                drvAppr["lSelect"] = true;
                                drvAppr.EndEdit();
                            }
                        }
                        dtWages.DefaultView.RowFilter = "";
                    }
                    if (((Button)(sender)).Name.ToUpper() == "BTNUNSELECTALL")
                    {
                        dtWages.DefaultView.RowFilter = "IsNull(lSelect,0)=1";
                        foreach (DataRowView drvAppr in dtWages.DefaultView)
                        {

                            drvAppr.BeginEdit();
                            drvAppr["lSelect"] = false;
                            drvAppr.EndEdit();

                        }
                        dtWages.DefaultView.RowFilter = "";
                    }




                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        private void grdMemberPayemnt_CellClick(object sender, GridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex < 0) { return; }

                dtWages.DefaultView.RowFilter = "";
                if (dtWages != null && dtWages.DefaultView.Count > 0)
                {
                    DataView dvwg = new DataView();
                    dvwg = mds.DefaultViewManager.CreateDataView(dtWages);

                    // dtWages.DefaultView.RowFilter = "IsNull(lWpApproved,0)=0 and cProcessed<>'A' and IsNull(lValidMemmber,0)=1 ";




                    if (e.Column.Name.ToUpper() == "DCSELECT")
                    {
                        dvwg.RowFilter = "IsNull(lWpApproved,0)=0  and IsNull(lValidMemmber,0)=1";
                        if (dvwg.Count <= 0) { ClsMessage.showMessage("All record approved , can not select approved record", MessageBoxIcon.Information); dtWages.DefaultView.RowFilter = ""; return; }
                        dtWages.DefaultView.RowFilter = "";
                        if (grdMemberPayemnt.CurrentRow.Index < 0) { return; }
                        if ((dtWages.DefaultView[grdMemberPayemnt.CurrentRow.Index]["lValidMemmber"] != DBNull.Value) && (Convert.ToBoolean(dtWages.DefaultView[grdMemberPayemnt.CurrentRow.Index]["lValidMemmber"]) == true))
                        {
                            dtWages.DefaultView[grdMemberPayemnt.CurrentRow.Index].BeginEdit();
                            dtWages.DefaultView[grdMemberPayemnt.CurrentRow.Index]["lSelect"] = !Convert.ToBoolean(dtWages.DefaultView[grdMemberPayemnt.CurrentRow.Index]["lSelect"]);
                            dtWages.DefaultView[grdMemberPayemnt.CurrentRow.Index].EndEdit();

                        }
                        else
                        {
                            return;
                        }

                        if ((dtWages.DefaultView[grdMemberPayemnt.CurrentRow.Index]["lWpApproved"] != DBNull.Value) && (Convert.ToBoolean(dtWages.DefaultView[grdMemberPayemnt.CurrentRow.Index]["lWpApproved"]) == true))
                        {
                            ClsMessage.showMessage("All record approved , can not select approved record", MessageBoxIcon.Information); 
                            dtWages.DefaultView[grdMemberPayemnt.CurrentRow.Index].BeginEdit();
                            dtWages.DefaultView[grdMemberPayemnt.CurrentRow.Index]["lSelect"] = false;
                            //Convert.ToBoolean(dtWages.DefaultView[grdMemberPayemnt.CurrentRow.Index]["lSelect"]);
                            dtWages.DefaultView[grdMemberPayemnt.CurrentRow.Index].EndEdit();
                        }
                        else
                        {
                           return;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grdMemberPayemnt_Click(object sender, EventArgs e)
        {

        }

        private void MasterTemplate_Click(object sender, EventArgs e)
        {

        }
    }
}
