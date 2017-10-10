using System;
using System.Data;
using System.Windows.Forms;
using SNAT.Comman_Classes;
using SNAT.Comman_Form;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using System.Drawing;

namespace SNAT.Member
{
    public partial class frmWagesChange : Form
    {//
        //BindingSource bsMember = new BindingSource();

        DataTable dtMember = new DataTable();
        BindingSource bsMemberWages = new BindingSource();
        DataTable dtMemberWages = new DataTable();
        string strSqlQuery = "";
        // BaseGridEditor _gridEditor;
        ErrorProvider errorProvider1 = new ErrorProvider();
        BindingSource bsMemmberEntery = new BindingSource();
        DataTable dtMemberEntery = new DataTable();

        BindingSource bsBeneficiryWages = new BindingSource();
        DataTable dtBeneficiryWages = new DataTable();
        DataTable dtBeneficiryList = new DataTable();
        public frmWagesChange()
        {
            InitializeComponent();
            BindMember();
            BindMemberWages();
            BindBeneficiaryWages();
        }
        private void FillMember()
        {
            try
            {
                strSqlQuery = "SELECT id , nationalid , memberid , employeeno , tscno , membername FROM SNAT.dbo.T_Member AS ted";
                dtMemberEntery = ClsDataLayer.GetDataTable(strSqlQuery);
                bsMemmberEntery.DataSource = dtMemberEntery.DefaultView;
                bindingNavigatorMain.BindingSource = bsMemmberEntery;



            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void BindMember()
        {
            try
            {

                FillMember();

                txtNationalId.DataBindings.Add("Text", bsMemmberEntery, "nationalid", false, DataSourceUpdateMode.OnPropertyChanged);
                txtMemberID.DataBindings.Add("Text", bsMemmberEntery, "memberid", false, DataSourceUpdateMode.OnValidation);
                txtEmployeeNo.DataBindings.Add("Text", bsMemmberEntery, "employeeno", false, DataSourceUpdateMode.OnValidation);
                // txtTSCNo.DataBindings.Add("Text", bsMemberWages, "tscno", false, DataSourceUpdateMode.OnValidation);
                txtMemberName.DataBindings.Add("Text", bsMemmberEntery, "membername", false, DataSourceUpdateMode.OnValidation);

                txtmembernationalidBenf.DataBindings.Add("Text", bsMemmberEntery, "nationalid", false, DataSourceUpdateMode.OnPropertyChanged);
                txtMemberIdBenf.DataBindings.Add("Text", bsMemmberEntery, "memberid", false, DataSourceUpdateMode.OnValidation);
                //   txtEmployeeNo.DataBindings.Add("Text", bsMemberWages, "employeeno", false, DataSourceUpdateMode.OnValidation);
                // txtTSCNo.DataBindings.Add("Text", bsMemberWages, "tscno", false, DataSourceUpdateMode.OnValidation);
                txtMemberNameBenf.DataBindings.Add("Text", bsMemmberEntery, "membername", false, DataSourceUpdateMode.OnValidation);
                grdList.DataSource = bsMemmberEntery;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                string iNationalID = "0";
                if (dtMemberEntery != null && dtMemberEntery.DefaultView.Count > 0)
                {
                    iNationalID = string.IsNullOrEmpty(dtMemberEntery.DefaultView[grdList.CurrentRow.Index]["nationalid"].ToString()) == true ?
                                    "0" : dtMemberEntery.DefaultView[grdList.CurrentRow.Index]["nationalid"].ToString();
                }
                FillMemberWages(iNationalID);
                FillBeneficiary(iNationalID);

                string iBeneficiryNationalID = "0";
                if (dtBeneficiryList != null && dtBeneficiryList.DefaultView.Count > 0)
                {
                    iBeneficiryNationalID = string.IsNullOrEmpty(dtBeneficiryList.DefaultView[grdBenefList.CurrentRow.Index]["beneficiarynatioanalid"].ToString()) == true ? "0" : dtBeneficiryList.DefaultView[grdBenefList.CurrentRow.Index]["beneficiarynatioanalid"].ToString();
                }
                FillBeneficiaryWages(iNationalID, iBeneficiryNationalID);
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SetEnable(bool lValue)
        {
            txtMemberID.Enabled = false;
            txtMemberName.Enabled = false;
            txtNationalId.Enabled = false;
            txtStatus.Enabled = false;
            txtEmployeeNo.Enabled = false;
            txtWagesAmount.Enabled = false;
            chkStatus.Enabled = false;
            dtpWagesEffectiveDate.Enabled = false;
            /************************************************/
            txtBeneficiaryName.Enabled = false;
            txtBeneficiaryNationalId.Enabled = false;
            txtMemberIdBenf.Enabled = false;
            txtMemberNameBenf.Enabled = false;
            txtmembernationalidBenf.Enabled = false;
            txtWagesAmountBenf.Enabled = false;
            dtpWagesEffectiveDate.Enabled = false;
            chkStatusBenf.Enabled = false;
            dtpEffectiveDateBenf.Enabled = false;

            if (tbWagesEntery.SelectedIndex == 0)
            {

                txtWagesAmount.Enabled = lValue;
                chkStatus.Enabled = lValue;
                dtpWagesEffectiveDate.Enabled = lValue;

            }
            if (tbWagesEntery.SelectedIndex == 1)
            {

                txtWagesAmountBenf.Enabled = lValue;
                dtpEffectiveDateBenf.Enabled = lValue;
                chkStatusBenf.Enabled = lValue;

            }

            /************************************************/
            btnAdd.Enabled = !lValue;
            btnExit.Enabled = !lValue;
           // btnEdit.Enabled = !lValue;
            btnSave.Enabled = lValue;
            btnUndo.Enabled = lValue;
            btnMoveFirst.Enabled = !lValue;
            btnMoveLast.Enabled = !lValue;
            btnMoveNext.Enabled = !lValue;
            btnMovePrevious.Enabled = !lValue;
            txtPositionItem.Enabled = !lValue;
            //btnDelete.Enabled = !lValue;
            btnSearch.Enabled = !lValue;
            btnPrint.Enabled = !lValue;
            btnRefresh.Enabled = !lValue;
          //  btnDelete.Enabled = !lValue;
            grdBenefList.Enabled = !lValue;
            grdList.Enabled = !lValue;
            grdBenefWages.Enabled = !lValue;
            grdMemWages.Enabled = !lValue;


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

                    break;
                case ClsUtility.enmFormMode.NormalMode:
                    SetEnable(false);
                    break;
            }
        }

        private void tbpMem_Click(object sender, EventArgs e)
        {

        }

        private void frmWagesChange_Load(object sender, EventArgs e)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbWagesEntery.SelectedIndex == 0)
                {

                    dtpWagesEffectiveDate.Text = null;

                    bsMemberWages.AllowNew = true;
                    bsMemberWages.AddNew();
                    Int32 iRow = bsMemberWages.Position;
                    dtMemberWages.DefaultView[iRow].BeginEdit();

                    if (string.IsNullOrEmpty(txtNationalId.Text.Trim()) == false)
                    {
                        dtMemberWages.DefaultView[iRow]["nationalid"] = txtNationalId.Text.Trim();
                    }

                    if (string.IsNullOrEmpty(txtMemberID.Text.Trim()) == false)
                    {
                        dtMemberWages.DefaultView[iRow]["memberid"] = txtMemberID.Text.Trim();
                    }

                    if (string.IsNullOrEmpty(txtEmployeeNo.Text.Trim()) == false)
                    {
                        dtMemberWages.DefaultView[iRow]["employeeno"] = txtEmployeeNo.Text.Trim();
                    }

                    dtMemberWages.DefaultView[iRow]["effactivedate"] = DateTime.Now;
                    dtMemberWages.DefaultView[iRow]["lstatus"] = chkStatus.Checked;
                    // dtMemberWages.DefaultView[iRow].EndEdit();
                }
                else if (tbWagesEntery.SelectedIndex == 1)
                {

                    dtpEffectiveDateBenf.Text = null;

                    bsBeneficiryWages.AllowNew = true;
                    bsBeneficiryWages.AddNew();
                    Int32 iRow = bsBeneficiryWages.Position;

                    txtBeneficiaryNationalId.Text = string.IsNullOrEmpty(dtBeneficiryList.DefaultView[grdBenefList.CurrentRow.Index]["beneficiarynatioanalid"].ToString()) == true ? "" : dtBeneficiryList.DefaultView[grdBenefList.CurrentRow.Index]["beneficiarynatioanalid"].ToString();
                    txtBeneficiaryName.Text = string.IsNullOrEmpty(dtBeneficiryList.DefaultView[grdBenefList.CurrentRow.Index]["beneficiaryname"].ToString()) == true ? "" : dtBeneficiryList.DefaultView[grdBenefList.CurrentRow.Index]["beneficiaryname"].ToString();



                    dtBeneficiryWages.DefaultView[iRow].BeginEdit();

                    if (string.IsNullOrEmpty(txtNationalId.Text.Trim()) == false)
                    {
                        dtBeneficiryWages.DefaultView[iRow]["nationalid"] = txtNationalId.Text.Trim();
                    }

                    if (string.IsNullOrEmpty(txtMemberID.Text.Trim()) == false)
                    {
                        dtBeneficiryWages.DefaultView[iRow]["memberid"] = txtMemberID.Text.Trim();
                    }

                    if (string.IsNullOrEmpty(txtBeneficiaryNationalId.Text.Trim()) == false)
                    {
                        dtBeneficiryWages.DefaultView[iRow]["beneficirynationalid"] = txtBeneficiaryNationalId.Text.Trim();
                    }

                    if (string.IsNullOrEmpty(txtBeneficiaryName.Text.Trim()) == false)
                    {
                        dtBeneficiryWages.DefaultView[iRow]["beneficiaryname"] = txtBeneficiaryName.Text.Trim();
                    }

                    dtBeneficiryWages.DefaultView[iRow]["effactivedate"] = DateTime.Now;
                    dtBeneficiryWages.DefaultView[iRow]["lstatus"] = chkStatus.Checked;
                    // dtMemberWages.DefaultView[iRow].EndEdit();
                }


                ClsUtility.FormMode = ClsUtility.enmFormMode.AddMode;
                SetFormMode(ClsUtility.enmFormMode.AddMode);

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
                if (tbWagesEntery.SelectedIndex == 0)
                {
                    DeleteMember();
                }
                else if (tbWagesEntery.SelectedIndex == 1)
                {

                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbWagesEntery.SelectedIndex == 0)
                {
                    SaveMember();
                }
                else if (tbWagesEntery.SelectedIndex == 1)
                {
                    SaveBeneficiry();
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {

                if (tbWagesEntery.SelectedIndex == 0)
                {
                    bsMemberWages.CancelEdit();
                    dtMemberWages.RejectChanges();
                }
                else if (tbWagesEntery.SelectedIndex == 1)
                {
                    bsBeneficiryWages.CancelEdit();
                    dtBeneficiryWages.RejectChanges();
                }
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
                    //txtDeptCode.Text = string.IsNullOrEmpty(dvDesg[0]["deptcode"].ToString()) == true ? "" : dvDesg[0]["deptcode"].ToString();
                    //txtDeptDesc.Text = string.IsNullOrEmpty(dvDesg[0]["deptname"].ToString()) == true ? "" : dvDesg[0]["deptname"].ToString();
                    if (string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) == false)
                    {
                        //int iRow = bsMemberWages.Find("nationalid", frmsrch.infCodeFieldText.Trim());
                        //bsMemberWages.Position = iRow;
                        //dvDesg.RowFilter = "";
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
        #region  Member Details
        private void FillMemberWages(string iNationalID)
        {
            try
            {

                strSqlQuery = " SELECT wd.id, wd.nationalid, wd.memberid, wd.employeeno,tm.membername, wd.wagesamount, wd.effactivedate, wd.lstatus, wd.createdby," +
                              " wd.createddate, wd.updatedby, wd.updateddate FROM SNAT.dbo.T_MemberWagesDetails wd (nolock)  " +
                              " INNER JOIN dbo.T_Member (nolock) tm ON tm.nationalid = wd.nationalid " +
                              " Where wd.nationalid='" + iNationalID + "' Order BY wd.lstatus DESC ";
                //AND tm.memberid = wd.memberid AND tm.employeeno = wd.employeeno
                dtMemberWages = ClsDataLayer.GetDataTable(strSqlQuery);
                bsMemberWages.DataSource = dtMemberWages.DefaultView;
                grdList.DataSource = bsMemmberEntery;
                // bindingNavigatorMain.BindingSource = bsMemberWages;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void BindMemberWages()
        {
            try
            {
                FillMember();
                string iNationalID = "0";
                if (dtMemberEntery != null && dtMemberEntery.DefaultView.Count > 0)
                {// string.IsNullOrEmpty(dvDesg[0]["deptname"].ToString()) == true ? "" : dvDesg[0]["deptname"].ToString();
                    iNationalID = string.IsNullOrEmpty(dtMemberEntery.DefaultView[grdList.CurrentRow.Index]["nationalid"].ToString()) == true ? "0"
                                    : dtMemberEntery.DefaultView[grdList.CurrentRow.Index]["nationalid"].ToString();
                }
                FillMemberWages(iNationalID);


                txtWagesAmount.DataBindings.Add("Text", bsMemberWages, "wagesamount", false, DataSourceUpdateMode.OnValidation);
                dtpWagesEffectiveDate.DataBindings.Add("Text", bsMemberWages, "effactivedate", false, DataSourceUpdateMode.OnPropertyChanged);
                txtStatus.DataBindings.Add("Text", bsMemberWages, "lstatus", false, DataSourceUpdateMode.OnPropertyChanged);

                grdMemWages.DataSource = bsMemberWages;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void DeleteMember()
        {
            if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
            {
                Int32 iRow = bsMemberWages.Position;
                bsMemberWages.RemoveAt(iRow);
                // dtDepartment.DefaultView.Delete(iRow);
                bsMemberWages.EndEdit();
                if (dtMemberWages != null && dtMemberWages.DefaultView.Count > 0)
                {
                    //dtMemberWages.DefaultView[iRow].BeginEdit();

                    //dtMemberWages.DefaultView[iRow].EndEdit();
                    if (dtMemberWages.GetChanges() != null)
                    {

                        bool lReturn = false;
                        strSqlQuery = "SELECT wd.id, wd.nationalid, wd.memberid, wd.employeeno, wd.wagesamount, wd.effactivedate, wd.lstatus, wd.createdby, wd.createddate, wd.updatedby, wd.updateddate FROM SNAT.dbo.T_MemberWagesDetails wd (nolock)   WHERE 1=2";
                        lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtMemberWages);

                        if (lReturn == true)
                        {
                            ClsMessage.showDeleteMessage();
                            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                            SetFormMode(ClsUtility.enmFormMode.NormalMode);
                            dtMemberWages.AcceptChanges();
                            FillMember();
                        }
                        else
                        {
                            ClsMessage.showMessage("Some problem occurs while deleting please contact system administrator.", MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }
        private bool ValidateSave_Member()
        {
            try
            {

                errorProvider1.Clear();

                if (string.IsNullOrEmpty(txtNationalId.Text.Trim()))
                {
                    errorProvider1.SetError(txtNationalId, "National id cannot be left blank.");
                    return false;
                }

                if (string.IsNullOrEmpty(txtMemberID.Text.Trim()))
                {
                    errorProvider1.SetError(txtMemberID, "Member id cannot be left blank.");
                    return false;
                }

                if (string.IsNullOrEmpty(txtEmployeeNo.Text.Trim()))
                {
                    errorProvider1.SetError(txtEmployeeNo, "Member employee no cannot be left blank.");
                    return false;
                }


                if (string.IsNullOrEmpty(txtWagesAmount.Text.Trim()))
                {
                    errorProvider1.SetError(txtWagesAmount, "Premium amount cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(dtpWagesEffectiveDate.Text.Trim()))
                {
                    errorProvider1.SetError(dtpWagesEffectiveDate, "Premium effective date cannot be left blank.");
                    return false;
                }

                if (Convert.ToDateTime(dtpWagesEffectiveDate.Value.ToShortDateString()) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                {
                    errorProvider1.SetError(dtpWagesEffectiveDate, "Premium effective date cannot be less than current date.");
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
        private void SaveMember()
        {
            try
            {


                if (ValidateSave_Member() == false) { return; }
                bsMemberWages.EndEdit();
                if (dtMemberWages != null && dtMemberWages.DefaultView.Count > 0)
                {
                    Int32 iRow = bsMemberWages.Position;
                    dtMemberWages.DefaultView[iRow].BeginEdit();

                    if (string.IsNullOrEmpty(txtNationalId.Text.Trim()) == false)
                    {
                        dtMemberWages.DefaultView[iRow]["nationalid"] = txtNationalId.Text.Trim();
                    }

                    if (string.IsNullOrEmpty(txtMemberID.Text.Trim()) == false)
                    {
                        dtMemberWages.DefaultView[iRow]["memberid"] = txtMemberID.Text.Trim();
                    }

                    if (string.IsNullOrEmpty(txtEmployeeNo.Text.Trim()) == false)
                    {
                        dtMemberWages.DefaultView[iRow]["employeeno"] = txtEmployeeNo.Text.Trim();
                    }
                    if (string.IsNullOrEmpty(dtpWagesEffectiveDate.Text.Trim()) == false)
                    {
                        if (dtpWagesEffectiveDate.Value.ToShortDateString().Trim() == "01/01/0001")
                        {
                            dtMemberWages.DefaultView[iRow]["effactivedate"] = DateTime.Now.ToShortDateString();
                        }
                        else
                        {
                            dtMemberWages.DefaultView[iRow]["effactivedate"] = dtpWagesEffectiveDate.Value.ToShortDateString().Trim(); ;
                        }
                    }

                    dtMemberWages.DefaultView[iRow]["lstatus"] = true;
                    if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                    {

                        dtMemberWages.DefaultView[iRow]["createdby"] = ClsSettings.username;
                        dtMemberWages.DefaultView[iRow]["createddate"] = DateTime.Now.ToString();
                    }
                    dtMemberWages.DefaultView[iRow]["updatedby"] = ClsSettings.username;
                    dtMemberWages.DefaultView[iRow]["updateddate"] = DateTime.Now.ToString();

                    dtMemberWages.DefaultView[iRow].EndEdit();
                    if (dtMemberWages.GetChanges() != null)
                    {
                        DataTable dtMemberWagesDetails = new DataTable();
                        strSqlQuery = "SELECT wd.id, wd.nationalid, wd.memberid, wd.employeeno, wd.wagesamount, wd.effactivedate, wd.lstatus, wd.createdby, wd.createddate, wd.updatedby, wd.updateddate FROM SNAT.dbo.T_MemberWagesDetails wd (nolock)    Where wd.nationalid='" + txtNationalId.Text.Trim() + "' and isnull(wd.lstatus,0)=1";
                        dtMemberWagesDetails = ClsDataLayer.GetDataTable(strSqlQuery);
                        foreach (DataRowView drvRow in dtMemberWagesDetails.DefaultView)
                        {
                            drvRow.BeginEdit();
                            drvRow["lstatus"] = 0;
                            drvRow.EndEdit();
                        }
                       

                        ClsDataLayer.openConnection();
                        ClsDataLayer.sqlTrans = ClsDataLayer.dbConn.BeginTransaction();

                        strSqlQuery = "SELECT id, nationalid, memberid, employeeno, wagesamount, wageseffectivedete, updateby, updateddate FROM    SNAT.dbo.T_Member AS tm where nationalid='" + txtNationalId.Text.Trim() + "'";
                        DataTable dtMemUpdate = new DataTable();
                        dtMemUpdate = ClsDataLayer.GetDataTable(strSqlQuery, ClsDataLayer.sqlTrans);
                        if (dtMemUpdate != null && dtMemUpdate.DefaultView.Count > 0)
                        {
                            dtMemUpdate.DefaultView[0].BeginEdit();
                            dtMemUpdate.DefaultView[0]["wagesamount"] = txtWagesAmount.Text.Trim();
                            dtMemUpdate.DefaultView[0]["wageseffectivedete"] = dtpWagesEffectiveDate.Value.ToShortDateString().Trim();
                            dtMemUpdate.DefaultView[0]["updateby"] = ClsSettings.username;
                            dtMemUpdate.DefaultView[0]["updateddate"] = DateTime.Now.ToString();
                            dtMemUpdate.DefaultView[0].EndEdit();
                            ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtMemUpdate, ClsDataLayer.sqlTrans);


                            /*---------------------------------------------------------------*/
                            strSqlQuery = "SELECT wd.id, wd.nationalid, wd.memberid, wd.employeeno, wd.wagesamount, wd.effactivedate, wd.lstatus, wd.createdby, wd.createddate, wd.updatedby, wd.updateddate FROM SNAT.dbo.T_MemberWagesDetails wd (nolock)    Where wd.nationalid='" + txtNationalId.Text.Trim() + "' and isnull(wd.lstatus,0)=1";
                            ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtMemberWagesDetails, ClsDataLayer.sqlTrans);

                            /*---------------------------------------------------------------*/

                            strSqlQuery = "SELECT wd.id, wd.nationalid, wd.memberid, wd.employeeno, wd.wagesamount, wd.effactivedate, wd.lstatus, wd.createdby, wd.createddate, wd.updatedby, wd.updateddate FROM SNAT.dbo.T_MemberWagesDetails wd (nolock)   WHERE 1=2";

                            iRow = bsMemberWages.Position;
                            dtMemberWages.DefaultView[iRow].BeginEdit();
                            dtMemberWages.DefaultView[iRow].EndEdit();
                            ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtMemberWages, ClsDataLayer.sqlTrans);
                            ClsDataLayer.sqlTrans.Commit();
                            ClsDataLayer.clsoeConnection();
                            dtMemberWages.AcceptChanges();

                            ClsMessage.showSaveMessage();
                            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                            SetFormMode(ClsUtility.enmFormMode.NormalMode);
                            FillMember();
                            FillMemberWages(txtNationalId.Text.Trim());
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
                if (ClsDataLayer.sqlTrans != null) { ClsDataLayer.sqlTrans.Rollback(); ClsDataLayer.clsoeConnection(); }
            }
        }
        private void grdMemWages_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void grdMemWages_CellBeginEdit(object sender, Telerik.WinControls.UI.GridViewCellCancelEventArgs e)
        {
            //try
            //{
            //    _gridEditor = this.grdMemWages.ActiveEditor as BaseGridEditor;
            //    if (_gridEditor != null)
            //    {
            //        RadItem editorElement = _gridEditor.EditorElement as RadItem;
            //        editorElement.KeyPress += new KeyPressEventHandler(grdMemWages_KeyDown);
            //    }
            //}
            //catch (Exception ex)
            //{

            //    ClsMessage.ProjectExceptionMessage(ex);
            //}

        }
        private void grdMemWages_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //try
            //{
            //    if (_gridEditor != null)
            //    {
            //        RadItem editorElement = _gridEditor.EditorElement as RadItem;
            //        editorElement.KeyPress -= grdMemWages_KeyDown;
            //    }
            //    _gridEditor = null;
            //}
            //catch (Exception ex)
            //{

            //    ClsMessage.ProjectExceptionMessage(ex);
            //}
        }
        void grdMemWages_KeyDown(object sender, KeyPressEventArgs e)
        {
            try
            {
                //   ClsUtility.DecilmalKeyPress(e.KeyChar.ToString(), e);

                //  MessageBox.Show(String.Format("Grid's RowsCount: {0}", this.grdMemWages.RowCount));
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }
        private void dtpEffectiveDateBenf_ValueChanged(object sender, EventArgs e)
        {
        }
        private void dtpWagesEffectiveDate_ValueChanged(object sender, EventArgs e)
        {
            //LET PRINCIPAL MEMBER / BENEFICIARY
            try
            {
                //if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode || ClsUtility.FormMode == ClsUtility.enmFormMode.EditMode)
                //{
                //    Int32 iRow = bsMemberWages.Position;

                //    if (dtMemberWages.DefaultView[iRow].IsEdit)
                //    {
                //        if (string.IsNullOrEmpty(txtNationalId.Text.Trim()) == false)
                //        {
                //            dtMemberWages.DefaultView[iRow]["effactivedate"] = txtNationalId.Text.Trim();
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtStatus.Text = chkStatus.Checked.ToString();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void txtStatus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtStatus.Text.Trim()))
                {
                    chkStatus.Checked = Convert.ToBoolean(txtStatus.Text.Trim());
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void grdMemWages_RowFormatting(object sender, RowFormattingEventArgs e)
        {

            try
            {
                DateTime dtpCurrentDate = Convert.ToDateTime(ClsUtility.GetDateTime().ToShortDateString());
                if (dtMemberWages != null && dtMemberWages.DefaultView.Count > 0)
                {
                    int iRow = 0;
                    iRow = e.RowElement.RowInfo.Index;
                    if (dtMemberWages.DefaultView[iRow]["effactivedate"] != DBNull.Value)
                    {
                        //if (Convert.ToDateTime(dtMemberWages.DefaultView[iRow]["effactivedate"]) < dtpCurrentDate)
                        //{
                        if (Convert.ToBoolean(dtMemberWages.DefaultView[iRow]["lstatus"]) == false)
                        {
                            e.RowElement.DrawFill = true;
                            e.RowElement.GradientStyle = GradientStyles.Solid;
                            e.RowElement.BackColor = Color.LightPink;
                        }
                        else
                        {
                            e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                            e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                            e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                        }
                    }



                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void grdMemWages_CellFormatting(object sender, CellFormattingEventArgs e)
        {

        }
        /**/
        #endregion
        #region Beneficiary Details
        private void FillBeneficiary(string iNationalID)
        {
            try
            {

                strSqlQuery = " SELECT id, membernationalid, memberid, membername, beneficiarynatioanalid, beneficiaryname,wages, effactivedate," +
                              " lstatus, createdby, createddate, updateby, updateddate FROM dbo.T_Beneficiary bn (nolock) " +
                              " Where bn.membernationalid='" + iNationalID + "'  Order by membernationalid, beneficiarynatioanalid";

                dtBeneficiryList = ClsDataLayer.GetDataTable(strSqlQuery);
                grdBenefList.DataSource = dtBeneficiryList.DefaultView;
                // bindingNavigatorMain.BindingSource = bsMemberWages;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void FillBeneficiaryWages(string iNationalID, string iBeneficiryNationalID)
        {
            try
            {

                strSqlQuery = " SELECT tbwd.id, tbwd.nationalid, tbwd.memberid,  tbwd.beneficirynationalid,tb.beneficiaryname," +
                              " tbwd.wagesamount, tbwd.effactivedate, tbwd.lstatus, tbwd.createdby, tbwd.createddate, tbwd.updatedby," +
                              " tbwd.updateddate " +
                              " FROM SNAT.dbo.T_BeneficiaryWagesDetails tbwd (nolock)" +
                              " INNER JOIN SNAT.dbo.T_Beneficiary tb(nolock) ON tb.beneficiarynatioanalid = tbwd.beneficirynationalid" +
                              " Where tbwd.nationalid='" + iNationalID + "' and tbwd.beneficirynationalid='" + iBeneficiryNationalID + "' Order by effactivedate Desc";

                dtBeneficiryWages = ClsDataLayer.GetDataTable(strSqlQuery);
                bsBeneficiryWages.DataSource = dtBeneficiryWages.DefaultView;

                // bindingNavigatorMain.BindingSource = bsMemberWages;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void BindBeneficiaryWages()
        {
            try
            {
                FillMember();
                string iNationalID = "0";
                if (dtMemberEntery != null && dtMemberEntery.DefaultView.Count > 0)
                {
                    iNationalID = string.IsNullOrEmpty(dtMemberEntery.DefaultView[grdList.CurrentRow.Index]["nationalid"].ToString()) == true ? "0"
                                    : dtMemberEntery.DefaultView[grdList.CurrentRow.Index]["nationalid"].ToString();
                }
                FillBeneficiary(iNationalID);

                string iBeneficiryNationalID = "0";
                if (dtBeneficiryList != null && dtBeneficiryList.DefaultView.Count > 0)
                {
                    iBeneficiryNationalID = string.IsNullOrEmpty(dtBeneficiryList.DefaultView[grdBenefList.CurrentRow.Index]["beneficiarynatioanalid"].ToString()) == true ?
                                            "0" : dtBeneficiryList.DefaultView[grdBenefList.CurrentRow.Index]["beneficiarynatioanalid"].ToString();
                }
                FillBeneficiaryWages(iNationalID, iBeneficiryNationalID);

                txtBeneficiaryNationalId.DataBindings.Add("Text", bsBeneficiryWages, "beneficirynationalid", false, DataSourceUpdateMode.OnValidation);
                txtBeneficiaryName.DataBindings.Add("Text", bsBeneficiryWages, "beneficiaryname", false, DataSourceUpdateMode.OnValidation);
                txtWagesAmountBenf.DataBindings.Add("Text", bsBeneficiryWages, "wagesamount", false, DataSourceUpdateMode.OnValidation);
                dtpEffectiveDateBenf.DataBindings.Add("Text", bsBeneficiryWages, "effactivedate", false, DataSourceUpdateMode.OnPropertyChanged);
                //txtStatus.DataBindings.Add("Text", bsMemberWages, "lstatus", false, DataSourceUpdateMode.OnPropertyChanged);

                grdBenefWages.DataSource = bsBeneficiryWages;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void grdBenefList_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {

        }
        private void grdBenefList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                string iNationalID = "0";
                if (dtMemberEntery != null && dtMemberEntery.DefaultView.Count > 0)
                {
                    iNationalID = string.IsNullOrEmpty(dtMemberEntery.DefaultView[grdList.CurrentRow.Index]["nationalid"].ToString()) == true ? "0"
                                    : dtMemberEntery.DefaultView[grdList.CurrentRow.Index]["nationalid"].ToString();
                }
                // FillBeneficiary(iNationalID);



                string iBeneficiryNationalID = "0";
                if (dtBeneficiryList != null && dtBeneficiryList.DefaultView.Count > 0)
                {
                    iBeneficiryNationalID = string.IsNullOrEmpty(dtBeneficiryList.DefaultView[grdBenefList.CurrentRow.Index]["beneficiarynatioanalid"].ToString()) == true ?
                                            "0" : dtBeneficiryList.DefaultView[grdBenefList.CurrentRow.Index]["beneficiarynatioanalid"].ToString();
                }


                txtBeneficiaryNationalId.Text = string.IsNullOrEmpty(dtBeneficiryList.DefaultView[grdBenefList.CurrentRow.Index]["beneficiarynatioanalid"].ToString()) == true ? "" : dtBeneficiryList.DefaultView[grdBenefList.CurrentRow.Index]["beneficiarynatioanalid"].ToString();
                txtBeneficiaryName.Text = string.IsNullOrEmpty(dtBeneficiryList.DefaultView[grdBenefList.CurrentRow.Index]["beneficiaryname"].ToString()) == true ? "" : dtBeneficiryList.DefaultView[grdBenefList.CurrentRow.Index]["beneficiaryname"].ToString();

                FillBeneficiaryWages(iNationalID, iBeneficiryNationalID);
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private bool ValidateSave_Beneficiry()
        {
            try
            {

                errorProvider1.Clear();

                if (string.IsNullOrEmpty(txtmembernationalidBenf.Text.Trim()))
                {
                    errorProvider1.SetError(txtmembernationalidBenf, "Member national id cannot be left blank.");
                    return false;
                }

                if (string.IsNullOrEmpty(txtMemberIdBenf.Text.Trim()))
                {
                    errorProvider1.SetError(txtMemberID, "Member id cannot be left blank.");
                    return false;
                }

                if (string.IsNullOrEmpty(txtBeneficiaryNationalId.Text.Trim()))
                {
                    errorProvider1.SetError(txtBeneficiaryNationalId, "Beneficiary national id cannot be left blank.");
                    return false;
                }


                if (string.IsNullOrEmpty(txtWagesAmountBenf.Text.Trim()))
                {
                    errorProvider1.SetError(txtWagesAmountBenf, "Premium amount cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(dtpEffectiveDateBenf.Text.Trim()))
                {
                    errorProvider1.SetError(dtpEffectiveDateBenf, "Premium effective date cannot be left blank.");
                    return false;
                }

                if ( Convert.ToDateTime(dtpEffectiveDateBenf.Value.ToShortDateString()) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                {
                    errorProvider1.SetError(dtpEffectiveDateBenf, "Premium effective date cannot be less than current date.");
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
        private void SaveBeneficiry()
        {
            try
            {


                if (ValidateSave_Beneficiry() == false) { return; }
                bsBeneficiryWages.EndEdit();
                if (dtBeneficiryWages != null && dtBeneficiryWages.DefaultView.Count > 0)
                {
                    Int32 iRow = bsBeneficiryWages.Position;
                    dtBeneficiryWages.DefaultView[iRow].BeginEdit();

                    if (string.IsNullOrEmpty(dtpWagesEffectiveDate.Text.Trim()) == false)
                    {
                        if (dtpWagesEffectiveDate.Value.ToShortDateString().Trim() == "01/01/0001")
                        {
                            dtBeneficiryWages.DefaultView[iRow]["effactivedate"] = DateTime.Now.ToShortDateString();
                        }
                        else
                        {
                            dtBeneficiryWages.DefaultView[iRow]["effactivedate"] = dtpEffectiveDateBenf.Value.ToShortDateString().Trim(); ;
                        }
                    }

                    dtBeneficiryWages.DefaultView[iRow]["lstatus"] = chkStatusBenf.Checked;
                    if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                    {

                        dtBeneficiryWages.DefaultView[iRow]["createdby"] = ClsSettings.username;
                        dtBeneficiryWages.DefaultView[iRow]["createddate"] = DateTime.Now.ToString();
                    }
                    dtBeneficiryWages.DefaultView[iRow]["updatedby"] = ClsSettings.username;
                    dtBeneficiryWages.DefaultView[iRow]["updateddate"] = DateTime.Now.ToString();

                    dtBeneficiryWages.DefaultView[iRow].EndEdit();
                    if (dtBeneficiryWages.GetChanges() != null)
                    {
                        DataTable dtMemberWagesDetails = new DataTable();
                        strSqlQuery = "SELECT id, nationalid, memberid,  beneficirynationalid, wagesamount, effactivedate, lstatus, createdby, createddate, updatedby, updateddate FROM SNAT.dbo.T_BeneficiaryWagesDetails WD (nolock)     Where wd.nationalid='" + txtNationalId.Text.Trim() + "'and beneficirynationalid='"+ txtBeneficiaryNationalId.Text.Trim() + "' and isnull(wd.lstatus,0)=1";
                        dtMemberWagesDetails = ClsDataLayer.GetDataTable(strSqlQuery);
                        foreach (DataRowView drvRow in dtMemberWagesDetails.DefaultView)
                        {
                            drvRow.BeginEdit();
                            drvRow["lstatus"] = 0;
                            drvRow.EndEdit();
                        }


                        ClsDataLayer.openConnection();
                        ClsDataLayer.sqlTrans = ClsDataLayer.dbConn.BeginTransaction();

                        strSqlQuery = "SELECT id, membernationalid, memberid, membername, beneficiarynatioanalid, beneficiaryname,wages, effactivedate, lstatus, createdby, createddate, updateby, updateddate FROM dbo.T_Beneficiary where membernationalid='" + txtmembernationalidBenf.Text.Trim() + "' and beneficiarynatioanalid='" + txtBeneficiaryNationalId.Text.Trim() + "'";
                        DataTable dtMemUpdate = new DataTable();
                        dtMemUpdate = ClsDataLayer.GetDataTable(strSqlQuery, ClsDataLayer.sqlTrans);
                        if (dtMemUpdate != null && dtMemUpdate.DefaultView.Count > 0)
                        {
                            dtMemUpdate.DefaultView[0].BeginEdit();
                            dtMemUpdate.DefaultView[0]["wages"] = txtWagesAmount.Text.Trim();
                            dtMemUpdate.DefaultView[0]["effactivedate"] = dtpWagesEffectiveDate.Value.ToShortDateString().Trim();
                            dtMemUpdate.DefaultView[0]["updateby"] = ClsSettings.username;
                            dtMemUpdate.DefaultView[0]["updateddate"] = DateTime.Now.ToString();
                            dtMemUpdate.DefaultView[0].EndEdit();
                            ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtMemUpdate, ClsDataLayer.sqlTrans);


                            /***********************************************************/

                            strSqlQuery = "SELECT id, nationalid, memberid,  beneficirynationalid, wagesamount, effactivedate, lstatus, createdby, createddate, updatedby, updateddate FROM SNAT.dbo.T_BeneficiaryWagesDetails WD (nolock)     Where wd.nationalid='" + txtNationalId.Text.Trim() + "'and beneficirynationalid='" + txtBeneficiaryNationalId.Text.Trim() + "' and isnull(wd.lstatus,0)=1";
                            
                            ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtMemberWagesDetails, ClsDataLayer.sqlTrans);
                            /***********************************************************/




                            strSqlQuery = "SELECT id, nationalid, memberid,  beneficirynationalid, wagesamount, effactivedate, lstatus, createdby, createddate, updatedby, updateddate FROM SNAT.dbo.T_BeneficiaryWagesDetails (nolock)   WHERE 1=2";


                            dtBeneficiryWages.DefaultView[iRow].BeginEdit();
                            dtBeneficiryWages.DefaultView[iRow].EndEdit();
                            ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtBeneficiryWages, ClsDataLayer.sqlTrans);
                            ClsDataLayer.sqlTrans.Commit();
                            ClsDataLayer.clsoeConnection();
                            dtBeneficiryWages.AcceptChanges();

                            ClsMessage.showSaveMessage();
                            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                            SetFormMode(ClsUtility.enmFormMode.NormalMode);
                            FillBeneficiary(txtmembernationalidBenf.Text.Trim());
                            FillBeneficiaryWages(txtmembernationalidBenf.Text.Trim(), txtBeneficiaryNationalId.Text.Trim());
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
                if (ClsDataLayer.sqlTrans != null) { ClsDataLayer.sqlTrans.Rollback(); ClsDataLayer.clsoeConnection(); }
            }
        }

        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (tbWagesEntery.SelectedIndex == 0)
            {
                FillMember();
                FillMemberWages(txtNationalId.Text.Trim());
            }
            else if (tbWagesEntery.SelectedIndex == 1)
            {
                FillBeneficiary(txtmembernationalidBenf.Text.Trim());
                FillBeneficiaryWages(txtmembernationalidBenf.Text.Trim(), txtBeneficiaryNationalId.Text.Trim());
            }
        }
    }
}
