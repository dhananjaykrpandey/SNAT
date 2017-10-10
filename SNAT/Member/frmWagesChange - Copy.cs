using System;
using System.Data;
using System.Windows.Forms;
using SNAT.Comman_Classes;
using SNAT.Comman_Form;
using Telerik.WinControls.UI;
using Telerik.WinControls;

namespace SNAT.Member
{
    public partial class frmWagesChange : Form
    {
        //BindingSource bsMember = new BindingSource();

        DataTable dtMember = new DataTable();
        BindingSource bsMemberWages = new BindingSource();
        DataTable dtMemberWages = new DataTable();
        string strSqlQuery = "";
        BaseGridEditor _gridEditor;
        public frmWagesChange()
        {
            InitializeComponent();
            BindMemberWages();
        }
        private void FillMember()
        {
            try
            {
                strSqlQuery = "SELECT id , nationalid , memberid , employeeno , tscno , membername FROM SNAT.dbo.T_Member AS ted";
                dtMember = ClsDataLayer.GetDataTable(strSqlQuery);
                grdMemList.DataSource = dtMember.DefaultView;


                strSqlQuery = "SELECT id, nationalid, memberid, employeeno, wagesamount, effactivedate, lstatus, createdby, createddate, updatedby, updateddate FROM SNAT.dbo.T_MemberWagesDetails;";
                dtMemberWages = ClsDataLayer.GetDataTable(strSqlQuery);
                bsMemberWages.DataSource = dtMemberWages.DefaultView;
                bindingNavigatorMember.BindingSource = bsMemberWages;
                grdMemWages.DataSource = bsMemberWages;
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
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SetEnable(bool lValue)
        {

            if (tbMain.SelectedIndex == 0)
            {
                grdMemWages.ReadOnly = !lValue;
                grdMemWages.AllowEditRow = lValue;
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
                // txtPositionItem.Enabled = !lValue;
                btnDelete.Enabled = !lValue;
                btnSearch.Enabled = !lValue;
                btnPrint.Enabled = !lValue;
            }
            if (tbMain.SelectedIndex == 1)
            {
                grdBenefWages.ReadOnly = lValue;
                /************************************************/
                btnAddBenef.Enabled = !lValue;
                btnExitBenef.Enabled = !lValue;
                btnEditBenef.Enabled = !lValue;
                btnSaveBenef.Enabled = lValue;
                btnDeleteBenef.Enabled = !lValue;
                btnUndoBenef.Enabled = lValue;
                btnMoveFirstBenef.Enabled = !lValue;
                btnMoveLastBenef.Enabled = !lValue;
                btnMoveNextBenef.Enabled = !lValue;
                btnMovePreviousBenef.Enabled = !lValue;
                // txtPositionItem.Enabled = !lValue;
                btnSearchBenef.Enabled = !lValue;
                btnPrintBenef.Enabled = !lValue;
            }





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
                bsMemberWages.AllowNew = true;
                bsMemberWages.AddNew();

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
                            strSqlQuery = "SELECT id , nationalid , memberid , employeeno , tscno , membername , dob , sex , school , contactno1 , contactno2 , residentaladdress , nomineenationalid , nomineename , wagesamount , wageseffectivedete , imagelocation , createdby , createddate , updateby , updateddate,email FROM SNAT.dbo.T_Member (nolock) WHERE 1=2";
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
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private bool ValidateSave()
        {
            try
            {

                //errorProvider1.Clear();

                //if (string.IsNullOrEmpty(txtNationalId.Text.Trim()))
                //{
                //    errorProvider1.SetError(txtNationalId, "National id cannot be left blank.");
                //    return false;
                //}
                //if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                //{
                //    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Member", "nationalid", "nationalid", txtNationalId.Text.Trim()) == true)
                //    {
                //        errorProvider1.SetError(txtNationalId, "National id already exists.");
                //        ClsMessage.showMessage("National id already exists.", MessageBoxIcon.Information);
                //        return false;
                //    }
                //}
                //if (string.IsNullOrEmpty(txtMemberID.Text.Trim()))
                //{
                //    errorProvider1.SetError(txtMemberID, "Member id cannot be left blank.");
                //    return false;
                //}
                //if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                //{
                //    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Member", "memberid", "memberid", txtMemberID.Text.Trim()) == true)
                //    {
                //        errorProvider1.SetError(txtMemberID, "Member id already exists.");
                //        ClsMessage.showMessage("Member id already exists.", MessageBoxIcon.Information);
                //        return false;
                //    }
                //}


                //if (string.IsNullOrEmpty(txtEmployeeNo.Text.Trim()))
                //{
                //    errorProvider1.SetError(txtEmployeeNo, "Member employee no cannot be left blank.");
                //    return false;
                //}
                //if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                //{
                //    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Member", "employeeno", "employeeno", txtEmployeeNo.Text.Trim()) == true)
                //    {
                //        errorProvider1.SetError(txtEmployeeNo, "Member employee no already exists.");
                //        ClsMessage.showMessage("Member employee no already exists.", MessageBoxIcon.Information);
                //        return false;
                //    }
                //}

                //if (string.IsNullOrEmpty(txtTSCNo.Text.Trim()))
                //{
                //    errorProvider1.SetError(txtTSCNo, "TSC no cannot be left blank.");
                //    return false;
                //}
                //if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                //{
                //    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Member", "tscno", "tscno", txtTSCNo.Text.Trim()) == true)
                //    {
                //        errorProvider1.SetError(txtTSCNo, "TSC no already exists.");
                //        ClsMessage.showMessage("TSC no already exists.", MessageBoxIcon.Information);
                //        return false;
                //    }
                //}


                //if (string.IsNullOrEmpty(txtMemberName.Text.Trim()))
                //{
                //    errorProvider1.SetError(txtMemberName, "Member name cannot be left blank.");
                //    return false;
                //}
                //if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                //{
                //    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Member", "membername", "membername", txtMemberName.Text.Trim()) == true)
                //    {
                //        errorProvider1.SetError(txtMemberName, "Member name already exists.");
                //        ClsMessage.showMessage("Member name already exists.", MessageBoxIcon.Information);
                //        return false;
                //    }
                //}
                //if (string.IsNullOrEmpty(dtpDOB.Text.Trim()))
                //{
                //    errorProvider1.SetError(dtpDOB, "Date of birth  cannot be left blank.");
                //    return false;
                //}
                //if (string.IsNullOrEmpty(txtSex.Text.Trim()))
                //{
                //    errorProvider1.SetError(panel1, "Member sex cannot be left blank.");
                //    return false;
                //}
                //if (string.IsNullOrEmpty(txtSchoolCode.Text.Trim()))
                //{
                //    errorProvider1.SetError(txtSchoolDesc, "School code cannot be left blank.");
                //    return false;
                //}
                //if (string.IsNullOrEmpty(txtContactNo1.Text.Trim()))
                //{
                //    errorProvider1.SetError(txtContactNo1, "First contact no cannot be left blank.");
                //    return false;
                //}
                //if (string.IsNullOrEmpty(txtemailid.Text.Trim()))
                //{
                //    errorProvider1.SetError(txtemailid, "Email id cannot be left blank.");
                //    return false;
                //}
                //if (string.IsNullOrEmpty(txtemailid.Text.Trim()) == false && ClsUtility.IsValidEmail(txtemailid.Text.Trim()) == false)
                //{
                //    //ClsMessage.showMessage("Invalid email id!!", MessageBoxIcon.Information);
                //    errorProvider1.SetError(txtemailid, "Invalid email id!!");
                //    txtemailid.Focus();
                //    return false;
                //}
                //if (string.IsNullOrEmpty(txtNomineeNationalId.Text.Trim()))
                //{
                //    errorProvider1.SetError(txtNomineeNationalId, "Nominee national id cannot be left blank.");
                //    return false;
                //}

                //if (string.IsNullOrEmpty(txtNomineeName.Text.Trim()))
                //{
                //    errorProvider1.SetError(txtNomineeName, "Nominee name cannot be left blank.");
                //    return false;
                //}

                //if (string.IsNullOrEmpty(txtWagesAmount.Text.Trim()))
                //{
                //    errorProvider1.SetError(txtemailid, "Wages amount cannot be left blank.");
                //    return false;
                //}
                //if (string.IsNullOrEmpty(dtpWagesEffectiveDate.Text.Trim()))
                //{
                //    errorProvider1.SetError(dtpWagesEffectiveDate, "Wages effective date cannot be left blank.");
                //    return false;
                //}


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
                bsMemberWages.EndEdit();
                if (dtMemberWages != null && dtMemberWages.DefaultView.Count > 0)
                {
                    Int32 iRow = bsMemberWages.Position;
                    dtMemberWages.DefaultView[iRow].BeginEdit();

                    if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                    {

                        dtMemberWages.DefaultView[iRow]["createdby"] = ClsSettings.username;
                        dtMemberWages.DefaultView[iRow]["createddate"] = DateTime.Now.ToString();
                    }
                    dtMemberWages.DefaultView[iRow]["updateby"] = ClsSettings.username;
                    dtMemberWages.DefaultView[iRow]["updateddate"] = DateTime.Now.ToString();

                    dtMemberWages.DefaultView[iRow].EndEdit();
                    if (dtMemberWages.GetChanges() != null)
                    {

                        bool lReturn = false;
                        strSqlQuery = "SELECT id , nationalid , memberid , employeeno , tscno , membername , dob , sex , school , contactno1 , contactno2 , residentaladdress , nomineenationalid , nomineename , wagesamount , wageseffectivedete , imagelocation , createdby , createddate , updateby , updateddate,email FROM SNAT.dbo.T_Member (nolock) WHERE 1=2";

                        iRow = bsMemberWages.Position;
                        dtMemberWages.DefaultView[iRow].BeginEdit();
                        dtMemberWages.DefaultView[iRow].EndEdit();
                        lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtMemberWages);
                        dtMemberWages.AcceptChanges();
                        if (lReturn == true)
                        {
                            ClsMessage.showSaveMessage();
                            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                            SetFormMode(ClsUtility.enmFormMode.NormalMode);
                            FillMember();
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
                bsMemberWages.CancelEdit();
                dtMemberWages.RejectChanges();
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
                        int iRow = bsMemberWages.Find("nationalid", frmsrch.infCodeFieldText.Trim());
                        bsMemberWages.Position = iRow;
                        dvDesg.RowFilter = "";
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


    }
}
