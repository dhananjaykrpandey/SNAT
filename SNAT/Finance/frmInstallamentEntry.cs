using SNAT.Comman_Classes;
using SNAT.Comman_Form;
using SNAT.CommanClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SNAT.Finance
{
    public partial class frmInstallamentEntry : Form
    {
        string strSqlQuery = "";
        DataTable dtMemberDetails = new DataTable();
        DataTable dtMemberPaymentDetails = new DataTable();
        BindingSource bsemberDetails = new BindingSource();
        BindingSource bsemberPaymentDetails = new BindingSource();
        ErrorProvider errorProvider1 = new ErrorProvider();
        public frmInstallamentEntry()
        {
            InitializeComponent();
            BindControl();
        }

        private void frmInstallamentEntry_Load(object sender, EventArgs e)
        {
            try
            {
                ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                SetFormMode(ClsUtility.enmFormMode.NormalMode);
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        void FillMemberDetails()
        {
            try
            {
                dtMemberPaymentDetails = new DataTable();
                // strSqlQuery = "SELECT tm.nationalid,tm.memberid, tm.employeeno, tm.tscno, tm.membername, tm.wagesamount,CAST(0 AS decimal) Totalamount,CAST(0 AS decimal) lastmonthamount,CAST(0 AS decimal) lastPendingamount FROM SNAT.dbo.T_Member tm WHERE tm.livingstatus='L'";
                strSqlQuery = "SELECT tm.nationalid,tm.memberid, tm.employeeno, tm.tscno, tm.membername, tm.wagesamount," + Environment.NewLine +
                                "  Totalamount = ISNULL((SELECT SUM(wagecredit)  FROM SNAT.dbo.T_MemberWegesProcess AS wp WHERE wp.memNationalID = tm.nationalid AND" + Environment.NewLine +
                                "  wp.memMemberID = tm.memberid AND  wp.memEmployeeNo = tm.employeeno AND wp.memTSCNo = tm.tscno" + Environment.NewLine +
                                "  GROUP BY wp.memNationalID, wp.memMemberID, wp.memEmployeeNo, wp.memTSCNo),0)," + Environment.NewLine +
                                " lastmonthamount = ISNULL((SELECT  wagecredit  FROM SNAT.dbo.T_MemberWegesProcess AS wp WHERE wp.memNationalID = tm.nationalid AND" + Environment.NewLine +
                                " wp.memMemberID = tm.memberid AND  wp.memEmployeeNo = tm.employeeno AND wp.memTSCNo = tm.tscno" + Environment.NewLine +
                                " AND CAST(wp.wageMonthYear + '-' + '15' AS datetime) = " + Environment.NewLine +
                                " (SELECT  MAX(CAST(mwp.wageMonthYear + '-' + '15' AS datetime))" + Environment.NewLine +
                                " FROM SNAT.dbo.T_MemberWegesProcess mwp" + Environment.NewLine +
                                " WHERE wp.memNationalID = mwp.memNationalID AND" + Environment.NewLine +
                                " wp.memMemberID = mwp.memMemberID AND  wp.memEmployeeNo = mwp.memEmployeeNo AND wp.memTSCNo = mwp.memTSCNo)),0)," + Environment.NewLine +
                                " lastPendingamount = ISNULL((SELECT  wp.wagePending  FROM SNAT.dbo.T_MemberWegesProcess AS wp WHERE wp.memNationalID = tm.nationalid AND" + Environment.NewLine +
                                " wp.memMemberID = tm.memberid AND  wp.memEmployeeNo = tm.employeeno AND wp.memTSCNo = tm.tscno" + Environment.NewLine +
                                " AND CAST(wp.wageMonthYear + '-' + '15' AS datetime) = " + Environment.NewLine +
                                " (SELECT  MAX(CAST(mwp.wageMonthYear + '-' + '15' AS datetime))" + Environment.NewLine +
                                " FROM SNAT.dbo.T_MemberWegesProcess mwp" + Environment.NewLine +
                                " WHERE wp.memNationalID = mwp.memNationalID AND" + Environment.NewLine +
                                " wp.memMemberID = mwp.memMemberID AND  wp.memEmployeeNo = mwp.memEmployeeNo AND wp.memTSCNo = mwp.memTSCNo)),0)" + Environment.NewLine +
                                " FROM SNAT.dbo.T_Member tm WHERE tm.livingstatus = 'L'";
                dtMemberDetails = ClsDataLayer.GetDataTable(strSqlQuery);
                bsemberDetails.DataSource = dtMemberDetails.DefaultView;
                bindingNavigatorMain.BindingSource = bsemberDetails;
                grdList.DataSource = bsemberDetails;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        void FillPaymentDetails(string strmemNationalID)
        {
            try
            {
                strSqlQuery = "SELECT id, wageMonthYear, wageFrom, memNationalID, memMemberID, memEmployeeNo, memTSCNo, memName, processingdate," + Environment.NewLine +
                              " wagecredit, wagePending, wagebalnace, memWegeRemarks, cProcessed, Remarks, cApprovalRemarks, luploaded, lProcessed," + Environment.NewLine +
                              " ProcessedBy, Processeddate, lApproved, ApprovedBy, ApprovedDate, createby, createdate, updateby, updatedate" + Environment.NewLine +
                              ", SUBSTRING(wageMonthYear,1,(PATINDEX('%-%',wageMonthYear)-1)) WageMonth,SUBSTRING(wageMonthYear,(PATINDEX('%-%',wageMonthYear)+1),(LEN(wageMonthYear)-(PATINDEX('%-%',wageMonthYear)-1))) WageYear" + Environment.NewLine +
                              " ,Totalamount =  ISNULL((SELECT SUM(wagecredit)  FROM SNAT.dbo.T_MemberWegesProcess AS wp " + Environment.NewLine +
                              " WHERE wp.memNationalID = tmwp.memNationalID AND  wp.memMemberID = tmwp.memMemberID AND  wp.memEmployeeNo = tmwp.memEmployeeNo " + Environment.NewLine +
                              " AND wp.memTSCNo = tmwp.memTSCNo  GROUP BY wp.memNationalID, wp.memMemberID, wp.memEmployeeNo, wp.memTSCNo ),0)," + Environment.NewLine +
                              " TotalPending =  ISNULL((SELECT SUM(wp.wagePending)  FROM SNAT.dbo.T_MemberWegesProcess AS wp WHERE " + Environment.NewLine +
                              " wp.memNationalID = tmwp.memNationalID AND wp.memMemberID = tmwp.memMemberID AND  wp.memEmployeeNo = tmwp.memEmployeeNo AND" + Environment.NewLine +
                              " wp.memTSCNo = tmwp.memTSCNo  GROUP BY wp.memNationalID, wp.memMemberID, wp.memEmployeeNo, wp.memTSCNo ),0)" + Environment.NewLine +
                              " ,Case When ISNULL(cProcessed,'')='A' Then 'Approved' else 'Processed' END  Processed " + Environment.NewLine +
                              " FROM SNAT.dbo.T_MemberWegesProcess AS tmwp Where  memNationalID='" + strmemNationalID + "' Order by CAST(tmwp.wageMonthYear + '-' + '15' AS datetime) DESC";
                dtMemberPaymentDetails = ClsDataLayer.GetDataTable(strSqlQuery);
                bsemberPaymentDetails.DataSource = dtMemberPaymentDetails.DefaultView;

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void BindControl()
        {
            try
            {
                FillMemberDetails();
                if (dtMemberDetails != null && dtMemberDetails.DefaultView.Count > 0)
                {
                    FillPaymentDetails(dtMemberDetails.DefaultView[0]["nationalid"].ToString());
                }
                else
                {
                    FillPaymentDetails("0");
                }
                txtInstID.DataBindings.Add(new Binding("Text", bsemberPaymentDetails, "id", false, DataSourceUpdateMode.OnValidation));
                txtWagesMonthYear.DataBindings.Add(new Binding("Text", bsemberPaymentDetails, "wageMonthYear", false, DataSourceUpdateMode.OnPropertyChanged));
                txtMemNationalID.DataBindings.Add(new Binding("Text", bsemberPaymentDetails, "memNationalID", false, DataSourceUpdateMode.OnValidation));
                txtMemberID.DataBindings.Add("Text", bsemberPaymentDetails, "memMemberID", false, DataSourceUpdateMode.OnValidation);
                txtMemberName.DataBindings.Add("Text", bsemberPaymentDetails, "memName", false, DataSourceUpdateMode.OnValidation);
                txtTotalAmount.DataBindings.Add("Text", bsemberPaymentDetails, "Totalamount", false, DataSourceUpdateMode.OnPropertyChanged);
                txtTotalPending.DataBindings.Add("Text", bsemberPaymentDetails, "TotalPending", false, DataSourceUpdateMode.OnPropertyChanged);
                txtInstAmount.DataBindings.Add("Text", bsemberPaymentDetails, "wagecredit", false, DataSourceUpdateMode.OnValidation);
                txtPendingAmount.DataBindings.Add("Text", bsemberPaymentDetails, "wagePending", false, DataSourceUpdateMode.OnValidation);
                txtRemarks.DataBindings.Add("Text", bsemberPaymentDetails, "memWegeRemarks", false, DataSourceUpdateMode.OnValidation);
                txtMonth.DataBindings.Add("Text", bsemberPaymentDetails, "WageMonth", false, DataSourceUpdateMode.OnValidation);
                txtYear.DataBindings.Add("Text", bsemberPaymentDetails, "WageYear", false, DataSourceUpdateMode.OnValidation);
                grdInstList.DataSource = bsemberPaymentDetails;
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);

            }
        }
        private void SetEnable(bool lValue)
        {

            txtMonth.Enabled = lValue;
            txtYear.Enabled = lValue;
            //txtMemNationalID.Enabled = lValue;
            //txtMemberID.Enabled = lValue;
            //txtMemberName.Enabled = lValue;
            txtInstAmount.Enabled = lValue;
            txtRemarks.Enabled = lValue;

            /************************************************/
            btnAdd.Enabled = !lValue;
            btnExit.Enabled = !lValue;
            btnEdit.Enabled = !lValue;
            btnSave.Enabled = lValue;
            btnUndo.Enabled = lValue;
            btnMoveFirst.Enabled = !lValue;
            btnMoveLast.Enabled = !lValue;
            btnMoveNext.Enabled = !lValue;
            btnMovePrevious.Enabled = !lValue;
            txtPositionItem.Enabled = !lValue;
            btnSearch.Enabled = !lValue;
            btnPrint.Enabled = !lValue;
            btnRefresh.Enabled = !lValue;
            btnDelete.Enabled = !lValue;
            btnApproved.Enabled = !lValue;
        }
        private void SetFormMode(ClsUtility.enmFormMode _FormMode)
        {
            switch (_FormMode)
            {
                case ClsUtility.enmFormMode.AddMode:
                    SetEnable(true);
                    break;
                case ClsUtility.enmFormMode.EditMode:
                    SetEnable(true);
                    txtMemNationalID.Enabled = false;
                    txtMemberID.Enabled = false;
                    txtMemberName.Enabled = false;
                    break;
                case ClsUtility.enmFormMode.NormalMode:
                    SetEnable(false);
                    break;
            }
        }

        private void grdInstList_SelectionChanging(object sender, Telerik.WinControls.UI.GridViewSelectionCancelEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void grdList_SelectionChanging(object sender, Telerik.WinControls.UI.GridViewSelectionCancelEventArgs e)
        {
            try
            {
                int iRow = 0;
                iRow = bsemberDetails.Position;
                if (dtMemberDetails != null && dtMemberDetails.DefaultView.Count > 0)
                {
                    FillPaymentDetails(dtMemberDetails.DefaultView[iRow]["nationalid"].ToString());
                }
                else
                {
                    FillPaymentDetails("0");
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnSearchSummary_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text.Trim() != "")
                {
                    ClsUtility.SearchText(dtMemberDetails, txtSearch.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (txtSearch.Text.Trim() != "")
                    {
                        ClsUtility.SearchText(dtMemberDetails, txtSearch.Text.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //tm.nationalid,tm., tm., tm., tm.
                string strmemNationalID = "", strmemMemberID = "", strmemEmployeeNo = "", strmemTSCNo = "", strmemName = "";//,strTotalAmount,strTotalPending;
                int iRow = 0;
                iRow = bsemberDetails.Position;
                strmemNationalID = dtMemberDetails.DefaultView[iRow]["nationalid"].ToString();
                strmemMemberID = dtMemberDetails.DefaultView[iRow]["memberid"].ToString();
                strmemEmployeeNo = dtMemberDetails.DefaultView[iRow]["employeeno"].ToString();
                strmemTSCNo = dtMemberDetails.DefaultView[iRow]["tscno"].ToString();
                strmemName = dtMemberDetails.DefaultView[iRow]["membername"].ToString();
                //strTotalAmount = dtMemberDetails.DefaultView[iRow]["tscno"].ToString();
                //strTotalPending = dtMemberDetails.DefaultView[iRow]["membername"].ToString();



                bsemberPaymentDetails.AllowNew = true;
                bsemberPaymentDetails.AddNew();
                ClsUtility.FormMode = ClsUtility.enmFormMode.AddMode;
                SetFormMode(ClsUtility.enmFormMode.AddMode);


                iRow = bsemberPaymentDetails.Position;
                dtMemberPaymentDetails.DefaultView[iRow].BeginEdit();
                dtMemberPaymentDetails.DefaultView[iRow]["memNationalID"] = strmemNationalID;
                dtMemberPaymentDetails.DefaultView[iRow]["memMemberID"] = strmemMemberID;
                dtMemberPaymentDetails.DefaultView[iRow]["memEmployeeNo"] = strmemEmployeeNo;
                dtMemberPaymentDetails.DefaultView[iRow]["memTSCNo"] = strmemTSCNo;
                dtMemberPaymentDetails.DefaultView[iRow]["memName"] = strmemName;
                dtMemberPaymentDetails.DefaultView[iRow]["wageFrom"] = "MANUAL";
                dtMemberPaymentDetails.DefaultView[iRow]["cProcessed"] = "P";
                dtMemberPaymentDetails.DefaultView[iRow]["processingdate"] = DateTime.Now.ToString();
                dtMemberPaymentDetails.DefaultView[iRow]["luploaded"] = false;
                dtMemberPaymentDetails.DefaultView[iRow]["lProcessed"] = true;
                dtMemberPaymentDetails.DefaultView[iRow]["ProcessedBy"] = ClsSettings.username; ;
                dtMemberPaymentDetails.DefaultView[iRow]["Processeddate"] = DateTime.Now.ToString();
                //dtMemberPaymentDetails.DefaultView[iRow].EndEdit();
                txtMemNationalID.Text = strmemNationalID;
                txtMemberID.Text = strmemMemberID;
                txtMemberName.Text = strmemName;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (lCanEdit() == false) { return; }


                ClsUtility.FormMode = ClsUtility.enmFormMode.EditMode;
                SetFormMode(ClsUtility.enmFormMode.EditMode);

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lCanEdit() == false) { return; }
                //        if (ValidateDetete() == false) { return; }
                        if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
                       {

                    string strWagesMonthyear = "", strInstAmount = "";

                    strWagesMonthyear = txtWagesMonthYear.Text.Trim();
                    strInstAmount = txtInstAmount.Text.Trim();

                    string strSqlQuery = "Delete FROM SNAT.dbo.T_MemberWegesProcess (nolock) WHERE  memNationalID='" + txtMemNationalID.Text.Trim() + "' " + Environment.NewLine +
                                      " AND  memMemberID='" + txtMemberID.Text.Trim() + "' AND wageMonthYear ='" + txtWagesMonthYear.Text.Trim() + "' and ID='" + txtInstID.Text.Trim() + "'" + Environment.NewLine +
                                      " AND ISNULL(lApproved,0)=0";
                   int lReturn = ClsDataLayer.UpdateData(strSqlQuery);

                    if (lReturn >0)
                    {

                        ClsMessage.showDeleteMessage();
                        clsEmail.lManualPrenumDelete(txtMemNationalID.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), strWagesMonthyear.Trim(), strInstAmount.Trim(), "");
                        ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                        SetFormMode(ClsUtility.enmFormMode.NormalMode);
                        dtMemberPaymentDetails.AcceptChanges();
                        FillMemberDetails();
                        if (dtMemberDetails != null && dtMemberDetails.DefaultView.Count > 0)
                        {
                            FillPaymentDetails(txtMemNationalID.Text.Trim());
                        }
                        else
                        {
                            FillPaymentDetails("0");
                        }
                    }
                    else
                    {
                        ClsMessage.showMessage("Some problem occurs while deleting please contact system administrator.", MessageBoxIcon.Information);
                    }
                    
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private bool ValidateSave()
        {
            try
            {

                errorProvider1.Clear();

                if (string.IsNullOrEmpty(txtMonth.Text.Trim()))
                {
                    errorProvider1.SetError(txtMonth, "Premium month cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtYear.Text.Trim()))
                {
                    errorProvider1.SetError(txtMonth, "Premium year cannot be left blank.");
                    return false;
                }

                string dDate = txtYear.Text.Trim() + "-" + txtMonth.Text.Trim() + "-01";

                if (!ClsUtility.IsValidDate(dDate))
                {
                    ClsMessage.showMessage("Please enter correct month-year.(MM-YYYY)", MessageBoxIcon.Information);
                    txtYear.Focus();
                    return false;
                }

                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (lValidateWages() == false)
                    {
                        errorProvider1.SetError(txtWagesMonthYear, "Premium details are exists for this month.");
                        ClsMessage.showMessage("Premium details are exists for this month.", MessageBoxIcon.Information);
                        return false;
                    }
                }


                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {


                if (ValidateSave() == false) { return; }

                //dtMemberPaymentDetails.DefaultView[iRow]["wageFrom"] = "MANUAL";
                //dtMemberPaymentDetails.DefaultView[iRow]["processingdate"] = DateTime.Now.ToString();
                //dtMemberPaymentDetails.DefaultView[iRow]["luploaded"] = false;
                //dtMemberPaymentDetails.DefaultView[iRow]["lProcessed"] = true;
                //dtMemberPaymentDetails.DefaultView[iRow]["ProcessedBy"] = ClsSettings.username; ;
                //dtMemberPaymentDetails.DefaultView[iRow]["Processeddate"] = DateTime.Now.ToString();
                bsemberPaymentDetails.EndEdit();
                if (dtMemberPaymentDetails != null && dtMemberPaymentDetails.DefaultView.Count > 0)
                {
                    Int32 iRow = bsemberPaymentDetails.Position;
                    dtMemberPaymentDetails.DefaultView[iRow].BeginEdit();
                    dtMemberPaymentDetails.DefaultView[iRow]["wageMonthYear"] = txtWagesMonthYear.Text.Trim();

                    if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                    {

                        dtMemberPaymentDetails.DefaultView[iRow]["createby"] = ClsSettings.username;
                        dtMemberPaymentDetails.DefaultView[iRow]["createdate"] = DateTime.Now.ToString();
                    }
                    dtMemberPaymentDetails.DefaultView[iRow]["updateby"] = ClsSettings.username;
                    dtMemberPaymentDetails.DefaultView[iRow]["updatedate"] = DateTime.Now.ToString();

                    dtMemberPaymentDetails.DefaultView[iRow].EndEdit();
                    if (dtMemberPaymentDetails.GetChanges() != null)
                    {

                        bool lReturn = false;
                        strSqlQuery = " SELECT id, wageMonthYear, wageFrom, memNationalID, memMemberID, memEmployeeNo, memTSCNo, memName, processingdate, wagecredit, wagePending, wagebalnace, memWegeRemarks, cProcessed, Remarks, cApprovalRemarks, luploaded, lProcessed, ProcessedBy, Processeddate, lApproved, ApprovedBy, ApprovedDate, createby, createdate, updateby, updatedate FROM SNAT.dbo.T_MemberWegesProcess (nolock) WHERE 1=2";

                        iRow = bsemberPaymentDetails.Position;
                        dtMemberPaymentDetails.DefaultView[iRow].BeginEdit();
                        dtMemberPaymentDetails.DefaultView[iRow].EndEdit();
                        lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtMemberPaymentDetails);
                        dtMemberPaymentDetails.AcceptChanges();


                        if (lReturn == true)
                        {

                            ClsMessage.showSaveMessage();
                            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                            SetFormMode(ClsUtility.enmFormMode.NormalMode);
                            FillMemberDetails();
                            FillPaymentDetails(txtMemNationalID.Text.Trim());
                            clsEmail.lManualPrenumProcess(txtMemNationalID.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), txtWagesMonthYear.Text.Trim(), txtInstAmount.Text.Trim(), "");

                        }
                        else
                        {
                            ClsMessage.showMessage("Some problem occurs while saving please contact system administrator.", MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                bsemberPaymentDetails.CancelEdit();
                dtMemberPaymentDetails.RejectChanges();
                ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                SetFormMode(ClsUtility.enmFormMode.NormalMode);
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT id , nationalid , memberid , employeeno , tscno , membername FROM SNAT.dbo.T_Member AS ted";
                frmsrch.infSqlWhereCondtion = "";
                frmsrch.infSqlOrderBy = " nationalid , memberid , employeeno";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Member ....";
                frmsrch.infCodeFieldName = "nationalid";
                frmsrch.infDescriptionFieldName = "memberid";
                frmsrch.infGridFieldName = " id , nationalid , memberid , employeeno , tscno , membername";
                frmsrch.infGridFieldCaption = " id , National id , Member ID , Employee No , Tsc No , Member Name";
                frmsrch.infGridFieldSize = "0,100,100,100,100,200";
                frmsrch.ShowDialog(this);
                if (frmsrch.DialogResult == DialogResult.OK && frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()))
                {
                    DataView dvDesg = new DataView();
                    dvDesg = frmsrch.infSearchReturnDataView;
                    dvDesg.RowFilter = "lSelect=1";
                    if (string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) == false)
                    {
                        int iRow = bsemberDetails.Find("nationalid", frmsrch.infCodeFieldText.Trim());
                        bsemberDetails.Position = iRow;
                        dvDesg.RowFilter = "";
                        if (dtMemberDetails != null && dtMemberDetails.DefaultView.Count > 0)
                        {
                            FillPaymentDetails(frmsrch.infCodeFieldText.Trim());
                        }
                        else
                        {
                            FillPaymentDetails("0");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                FillMemberDetails();
                if (dtMemberDetails != null && dtMemberDetails.DefaultView.Count > 0)
                {
                    FillPaymentDetails(dtMemberDetails.DefaultView[0]["nationalid"].ToString());
                }
                else
                {
                    FillPaymentDetails("0");
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }

        private void txtMonth_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMonth.Text) && !string.IsNullOrEmpty(txtYear.Text))
                {
                    string dDate = txtYear.Text.Trim() + "-" + txtMonth.Text.Trim() + "-01";

                    if (!ClsUtility.IsValidDate(dDate))
                    {
                        ClsMessage.showMessage("Please enter correct month-year.(MM-YYYY)", MessageBoxIcon.Information);
                        txtYear.Focus();
                        return;
                    }

                    string strWagesMonthYear = txtMonth.Text.Trim() + "-" + txtYear.Text.Trim();
                    txtWagesMonthYear.Text = strWagesMonthYear;
                    if (lValidateWages() == false)
                    {
                        ClsMessage.ProjectExceptionMessage("Premium already exists for this month-year!!");
                        // txtMonth.Focus();
                        bsemberPaymentDetails.CancelEdit();
                        dtMemberPaymentDetails.RejectChanges();
                        ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                        SetFormMode(ClsUtility.enmFormMode.NormalMode);
                        int iRow = bsemberPaymentDetails.Find("wageMonthYear", strWagesMonthYear.Trim());
                        bsemberPaymentDetails.Position = iRow;
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }

        private bool lValidateWages()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMonth.Text) && !string.IsNullOrEmpty(txtYear.Text))
                {
                    string strWagesMonthYear = txtMonth.Text.Trim() + "-" + txtYear.Text.Trim();
                    txtWagesMonthYear.Text = strWagesMonthYear;
                    string strSqlQuery = "SELECT id FROM SNAT.dbo.T_MemberWegesProcess (nolock) WHERE  memNationalID='" + txtMemNationalID.Text.Trim() + "' " + Environment.NewLine +
                                      " AND  memMemberID='" + txtMemberID.Text.Trim() + "' AND wageMonthYear ='" + txtWagesMonthYear.Text.Trim() + "'";
                    DataTable dtCheck = new DataTable();
                    dtCheck = ClsDataLayer.GetDataTable(strSqlQuery);
                    if (dtCheck != null && dtCheck.DefaultView.Count > 0)
                    {
                        return false;
                    }
                }


                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }
        private void CalculatePending()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtInstAmount.Text.Trim()) && Convert.ToDecimal(txtInstAmount.Text.Trim()) > 0)
                {
                    decimal dlmemNationalID = 0;
                    decimal dlPendingAmount = 0;
                    int iRow = 0;
                    iRow = bsemberDetails.Position;
                    dlmemNationalID = Convert.ToDecimal(dtMemberDetails.DefaultView[iRow]["wagesamount"].ToString());
                    dlPendingAmount = dlmemNationalID - Convert.ToDecimal(txtInstAmount.Text.Trim());

                    if (dlPendingAmount <= 0)
                    {
                        txtPendingAmount.Text = "0";

                    }
                    else
                    {
                        txtPendingAmount.Text = dlPendingAmount.ToString();
                    }

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtMonth_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtInstAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ClsUtility.DecilmalKeyPress(txtInstAmount.Text.Trim(), e);
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtInstAmount_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                CalculatePending();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private bool lCanEdit()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMonth.Text) && !string.IsNullOrEmpty(txtYear.Text))
                {
                    string strWagesMonthYear = txtMonth.Text.Trim() + "-" + txtYear.Text.Trim();
                    txtWagesMonthYear.Text = strWagesMonthYear;
                    string strSqlQuery = "SELECT id FROM SNAT.dbo.T_MemberWegesProcess (nolock) WHERE  memNationalID='" + txtMemNationalID.Text.Trim() + "' " + Environment.NewLine +
                                      " AND  memMemberID='" + txtMemberID.Text.Trim() + "' AND wageMonthYear ='" + txtWagesMonthYear.Text.Trim() + "' and ID='" + txtInstID.Text.Trim() + "'" + Environment.NewLine +
                                      " AND ISNULL(lApproved,0)=1";
                    DataTable dtCheck = new DataTable();
                    dtCheck = ClsDataLayer.GetDataTable(strSqlQuery);
                    if (dtCheck != null && dtCheck.DefaultView.Count > 0)
                    {
                        ClsMessage.ProjectExceptionMessage("Premium already approved cannot edit/delete.");
                        return false;
                    }

                    // strSqlQuery = "SELECT id FROM SNAT.dbo.T_MemberWegesProcess (nolock) WHERE  memNationalID='" + txtMemNationalID.Text.Trim() + "' " + Environment.NewLine +
                    //                 " AND  memMemberID='" + txtMemberID.Text.Trim() + "' AND wageMonthYear ='" + txtWagesMonthYear.Text.Trim() + "' and ID='" + txtInstID.Text.Trim() + "'" + Environment.NewLine +
                    //                 " AND ISNULL(lApproved,0)=1";
                    // dtCheck = new DataTable();
                    //dtCheck = ClsDataLayer.GetDataTable(strSqlQuery);
                    //if (dtCheck != null && dtCheck.DefaultView.Count > 0)
                    //{
                    //    ClsMessage.ProjectExceptionMessage("Premium already approved cannot edit/delete.");
                    //    return false;
                    //}
                    int iRow = bsemberPaymentDetails.Position;
                    if (iRow>0)
                    {
                        ClsMessage.ProjectExceptionMessage("Premium cannot edit/delete." + Environment.NewLine +"Only last premium entry can edit/delete.");
                        return false;
                    }

                }

                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClsUtility.lsAutorizeForApproval(ClsUtility.enuAutorizeCheck.Premium) == false) { return; }

                strSqlQuery = "SELECT id FROM SNAT.dbo.T_MemberWegesProcess (nolock) WHERE  memNationalID='" + txtMemNationalID.Text.Trim() + "' " + Environment.NewLine +
                                " AND  memMemberID='" + txtMemberID.Text.Trim() + "' AND wageMonthYear ='" + txtWagesMonthYear.Text.Trim() + "' and ID='" + txtInstID.Text.Trim() + "'" + Environment.NewLine +
                                " AND ISNULL(lApproved,0)=1";
                DataTable dtCheck = new DataTable();
                dtCheck = ClsDataLayer.GetDataTable(strSqlQuery);
                if (dtCheck != null && dtCheck.DefaultView.Count > 0)
                {
                    ClsMessage.ProjectExceptionMessage("Premium already approved cannot preform this action.");
                    return;
                }

                if (ClsMessage.showQuestionMessage("Are you sure want to approved processed Premium?") == DialogResult.No) { return; }

                {

                    string strWagesMonthyear = "", strInstAmount = "";

                    strWagesMonthyear = txtWagesMonthYear.Text.Trim();
                    strInstAmount = txtInstAmount.Text.Trim();

                    string strSqlQuery = "Update SNAT.dbo.T_MemberWegesProcess set cProcessed='A',lApproved=1," + Environment.NewLine +
                                        "cApprovalRemarks='Premium approved by " + ClsSettings.username + "  manually' " + Environment.NewLine +
                                        ",ApprovedBy='" + ClsSettings.username + "',ApprovedDate=getdate(),updateby='" + ClsSettings.username + "' ,updatedate=GetDate() " + Environment.NewLine +
                                        " WHERE  memNationalID='" + txtMemNationalID.Text.Trim() + "' " + Environment.NewLine +
                                        " AND  memMemberID='" + txtMemberID.Text.Trim() + "' AND wageMonthYear ='" + txtWagesMonthYear.Text.Trim() + "' and ID='" + txtInstID.Text.Trim() + "'" + Environment.NewLine +
                                        " AND ISNULL(lApproved,0)=0";
                    int lReturn = ClsDataLayer.UpdateData(strSqlQuery);

                    if (lReturn > 0)
                    {

                        ClsMessage.showSaveMessage();
                        clsEmail.lManualPrenumApproved(txtMemNationalID.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), strWagesMonthyear.Trim(), strInstAmount.Trim(), "");
                        ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                        SetFormMode(ClsUtility.enmFormMode.NormalMode);
                        dtMemberPaymentDetails.AcceptChanges();
                        FillMemberDetails();
                        if (dtMemberDetails != null && dtMemberDetails.DefaultView.Count > 0)
                        {
                            FillPaymentDetails(txtMemNationalID.Text.Trim());
                        }
                        else
                        {
                            FillPaymentDetails("0");
                        }
                    }
                    else
                    {
                        ClsMessage.showMessage("Some problem occurs while deleting please contact system administrator.", MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }
    }
}
