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

namespace SNAT.Document
{
    public partial class frmCreateUser : Form
    {
        BindingSource bsUser = new BindingSource();
        string strSqlQuery = "";
        DataTable dtUser = new DataTable();
        public frmCreateUser()
        {
            InitializeComponent();
            BindControl();
        }
        void FillUserDetails()
        {
            try
            {
                strSqlQuery = " SELECT lg.id, lg.username, lg.password, lg.usertype, lg.userstatus, lg.employee, lg.remarks, lg.emailid, lg.contactno, lg.employeeno,"+ Environment.NewLine  +
                               " ted.name AS employeename, lg.employeeno+'/'+ted.name AS empname," + Environment.NewLine +
                               " CASE   WHEN ISNULL(employee, '') = 'E' THEN 'EMPLOYEE'  WHEN ISNULL(employee, '') = 'M' THEN 'Member'  END AS cemployee," + Environment.NewLine +
                               " lg.Memnationalid, tm.membername FROM SNAT.dbo.logintable AS lg(nolock)" + Environment.NewLine +
                               " LEFT OUTER JOIN  SNAT.dbo.T_EmployeeDetails AS ted(nolock)  ON ted.employeeno = lg.employeeno" + Environment.NewLine + 
                               " LEFT OUTER JOIN SNAT.dbo.T_Member AS tm(nolock) ON tm.nationalid = lg.Memnationalid WHERE lg.usertype <> 'admin'";//AND ted.nationalid = lg.employeenationalid
                dtUser = ClsDataLayer.GetDataTable(strSqlQuery);
                bsUser.DataSource = dtUser.DefaultView;
                bindingNavigatorMain.BindingSource = bsUser;
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
                FillUserDetails();


                txtUserId.DataBindings.Add("Text", bsUser, "username", false, DataSourceUpdateMode.OnValidation);
                txtpassword.DataBindings.Add("Text", bsUser, "password", false, DataSourceUpdateMode.OnValidation);
                txtconfirmpassword.DataBindings.Add("Text", bsUser, "password", false, DataSourceUpdateMode.OnValidation);
                txtRemarks.DataBindings.Add("Text", bsUser, "remarks", false, DataSourceUpdateMode.OnValidation);
                txtChkBoxStatus.DataBindings.Add("Text", bsUser, "userstatus", false, DataSourceUpdateMode.OnValidation);

                txtemail.DataBindings.Add("Text", bsUser, "emailid", false, DataSourceUpdateMode.OnPropertyChanged);
                txtContactNo.DataBindings.Add("Text", bsUser, "contactno", false, DataSourceUpdateMode.OnPropertyChanged);
                txtEmployeeNo.DataBindings.Add("Text", bsUser, "employeeno", false, DataSourceUpdateMode.OnPropertyChanged);
                txtEmployeeName.DataBindings.Add("Text", bsUser, "employeename", false, DataSourceUpdateMode.OnPropertyChanged);
                txtEmployee.DataBindings.Add("Text", bsUser, "employee", false, DataSourceUpdateMode.OnPropertyChanged);

                txtMemNationalID.DataBindings.Add("Text", bsUser, "Memnationalid", false, DataSourceUpdateMode.OnPropertyChanged);
                txtMemName.DataBindings.Add("Text", bsUser, "membername", false, DataSourceUpdateMode.OnPropertyChanged);
                grdList.DataSource = bsUser;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }
        private void SetEnable(bool lValue)
        {
            txtUserId.Enabled = lValue;
            txtpassword.Enabled = lValue;
            txtRemarks.Enabled = lValue;
            chkStatus.Enabled = lValue;
           // txtemail.Enabled = lValue;
            //txtContactNo.Enabled = lValue;
            txtEmployeeNo.Enabled = lValue;
           // txtEmployeeName.Enabled = lValue;
            txtEmployee.Enabled = lValue;
            txtconfirmpassword.Enabled = lValue;
            btnPickEmployee.Enabled = lValue;
            rbEmployee.Enabled = lValue;
            rbNonEmployee.Enabled = lValue;
            txtMemNationalID.Enabled = lValue;
            btnPickMemNationalID.Enabled = lValue;
            /*****************************************************/

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
                    txtUserId.Enabled = false;
                    break;
                case ClsUtility.enmFormMode.NormalMode:
                    SetEnable(false);
                    break;
            }
        }

        private void frmCreateUser_Load(object sender, EventArgs e)
        {
            try
            {
                ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                SetFormMode(ClsUtility.enmFormMode.NormalMode);
                //rbEmployee.Checked = true;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtChkBoxStatus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtChkBoxStatus.Text.Trim()))
                {
                    chkStatus.Checked = Convert.ToBoolean(txtChkBoxStatus.Text);
                }
                else
                {
                    chkStatus.Checked = false;
                }

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bsUser.AllowNew = true;
                bsUser.AddNew();
                ClsUtility.FormMode = ClsUtility.enmFormMode.AddMode;
                SetFormMode(ClsUtility.enmFormMode.AddMode);
                rbEmployee.Checked = true;
                txtUserId.Focus();
                rbEmployee_CheckedChanged(rbEmployee, null);
              //ClsDataLayer.setLogAcitivity("Create New User", ClsSettings.username, "New User Created ", "", " ", "Msg");
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

                SetFormMode(ClsUtility.enmFormMode.EditMode);
                ClsUtility.FormMode = ClsUtility.enmFormMode.EditMode;
                txtpassword.Focus();
              //ClsDataLayer.setLogAcitivity("Create User", ClsSettings.username, "Editing Existing User Details ", "", "Edited Existing User Successfully", "Msg");
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
                if (string.IsNullOrEmpty(txtUserId.Text.Trim()) == false)
                {

                    if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
                    {
                        Int32 iRow = bsUser.Position;
                        bsUser.RemoveAt(iRow);
                        bsUser.EndEdit();
                        if (dtUser != null && dtUser.DefaultView.Count > 0)
                        {
                            if (dtUser.GetChanges() != null)
                            {

                                bool lReturn = false;
                                strSqlQuery = "SELECT id , username , password , usertype , userstatus , employee , remarks , emailid , contactno,employeeno ,employeenationalid FROM SNAT.dbo.logintable;";
                                lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtUser);

                                dtUser.AcceptChanges();
                                if (lReturn == true)
                                {
                                    ClsMessage.showDeleteMessage();
                                  //ClsDataLayer.setLogAcitivity("Create User", ClsSettings.username, "Deleted Existing User", "", "Deleted Existing User Successfully!", "Msg");
                                    SetFormMode(ClsUtility.enmFormMode.NormalMode);
                                }
                                else
                                {
                                    ClsMessage.showMessage("Some problem occurs while deleting please contact system administrator.", MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
              //ClsDataLayer.setLogAcitivity("Create User", ClsSettings.username, " " + ex.Message.ToString(), "", "Some Problem Occurred while deleting Existing user, please contact System Administrator", "Msg");
            }
        }

        private bool ValidateSave()
        {
            try
            {

                errorProvider1.Clear();


                if (string.IsNullOrEmpty(txtUserId.Text.Trim()))
                {
                    errorProvider1.SetError(txtUserId, "User id cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("logintable", "username", "username", txtUserId.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtUserId, "User ID already exists.");
                        ClsMessage.showMessage("User ID already exists.", MessageBoxIcon.Information);
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(txtpassword.Text.Trim()))
                {
                    errorProvider1.SetError(txtpassword, "Password cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtconfirmpassword.Text.Trim()))
                {
                    errorProvider1.SetError(txtconfirmpassword, "Confirm password cannot be left blank.");
                    return false;
                }

                if (txtpassword.Text.Trim().ToUpper() != txtconfirmpassword.Text.Trim().ToUpper())
                {
                    ClsMessage.showMessage("Confirm password does not match with password.", MessageBoxIcon.Information);
                    errorProvider1.SetError(txtconfirmpassword, "Confirm password does not match with password.");
                    return false;
                }

                //if (string.IsNullOrEmpty(txtEmployee.Text.Trim()))
                //{
                //    errorProvider1.SetError(panel1, "Employee/Non-Employee cannot left  blank");
                //    return false;
                //}

                if (rbEmployee.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtEmployeeNo.Text.Trim()))
                    {
                        errorProvider1.SetError(txtEmployeeNo, "Employee no cannot left  blank");
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(txtemail.Text.Trim()))
                {
                    errorProvider1.SetError(txtemail, "Email-ID cannot left  blank");
                    return false;
                }
                if (string.IsNullOrEmpty(txtemail.Text.Trim()) == false && ClsUtility.IsValidEmail(txtemail.Text.Trim()) == false)
                {
                    //ClsMessage.showMessage("Invalid email id!!", MessageBoxIcon.Information);
                    errorProvider1.SetError(txtemail, "Invalid email id!!");
                    txtemail.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(txtContactNo.Text.Trim()))
                {
                    errorProvider1.SetError(txtContactNo, "Contact no cannot left  blank");
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                txtChkBoxStatus.Text = chkStatus.Checked.ToString();

                if (ValidateSave() == false) { return; }

                bsUser.EndEdit();
                if (dtUser != null && dtUser.DefaultView.Count > 0)
                {
                    Int32 iRow = bsUser.Position;
                    dtUser.DefaultView[iRow].BeginEdit();
                    if (rbEmployee.Checked == true)
                    {
                        dtUser.DefaultView[iRow]["employee"] = "E";
                    }
                    if (rbNonEmployee.Checked == true)
                    {
                        dtUser.DefaultView[iRow]["employee"] = "M";
                    }
                    dtUser.DefaultView[iRow]["usertype"] = "User";
                    dtUser.DefaultView[iRow].EndEdit();

                    if (dtUser.GetChanges() != null)
                    {


                        bool lReturn = false;
                        strSqlQuery = "SELECT id , username , password , usertype , userstatus , employee , remarks , emailid , contactno,employeeno ,Memnationalid FROM SNAT.dbo.logintable (nolock) where 1=2";
                        lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtUser);
                        dtUser.AcceptChanges();
                        if (lReturn == true)
                        {
                            ClsMessage.showSaveMessage();
                            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                            SetFormMode(ClsUtility.enmFormMode.NormalMode);
                            FillUserDetails();
                          //ClsDataLayer.setLogAcitivity("Create User", ClsSettings.username, "User Details Saved Successfully ", "", " ", "Msg");

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
                if (ClsMessage.showAskDiscardMessage() == DialogResult.Yes)
                {
                    bsUser.CancelEdit();
                    dtUser.RejectChanges();
                    ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                    SetFormMode(ClsUtility.enmFormMode.NormalMode);
                }
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
                frmsrch.infSqlSelectQuery = "SELECT id , username , case when Isnull(employee,'')='E' then 'EMPLOYEE' when Isnull(employee,'')='N' then 'Non-Employee'  end  employee FROM SNAT.dbo.logintable";
                frmsrch.infSqlWhereCondtion = " Isnull(userstatus,0)=1 and usertype<>'admin'";
                frmsrch.infSqlOrderBy = " username ";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Department ....";
                frmsrch.infCodeFieldName = "username";
                frmsrch.infDescriptionFieldName = "employee";
                frmsrch.infGridFieldName = "id,username,employee";
                frmsrch.infGridFieldCaption = "id, User Name,Employee/Non-Employee";
                frmsrch.infGridFieldSize = "0,100,200";
                frmsrch.ShowDialog(this);
                //txtDesgCode.Text = frmsrch.infCodeFieldText;
                //txtDesgName.Text = frmsrch.infDescriptionFieldText;
                if (frmsrch.DialogResult == DialogResult.OK)
                {
                    if (frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) && frmsrch.infCodeFieldText.Trim() != "")
                    {
                        DataView dvDesg = new DataView();
                        dvDesg = frmsrch.infSearchReturnDataView;
                        dvDesg.RowFilter = "lSelect=1";
                        //txtDeptCode.Text = string.IsNullOrEmpty(dvDesg[0]["deptcode"].ToString()) == true ? "" : dvDesg[0]["deptcode"].ToString();
                        //txtDeptDesc.Text = string.IsNullOrEmpty(dvDesg[0]["deptname"].ToString()) == true ? "" : dvDesg[0]["deptname"].ToString();
                        if (string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) == false)
                        {
                            int iRow = bsUser.Find("username", frmsrch.infCodeFieldText.Trim());
                            bsUser.Position = iRow;
                            dvDesg.RowFilter = "";
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

        }

        private void rbEmployee_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbEmployee.Checked == true)
                {
                    txtEmployee.Text = "E";
                    if (ClsUtility.FormMode != ClsUtility.enmFormMode.NormalMode)
                    {
                        txtEmployeeNo.Enabled = true;
                        txtEmployeeName.Enabled = true;
                        btnPickEmployee.Enabled = true;
                    }
                    else
                    {
                        txtEmployeeNo.Enabled = false;
                        txtEmployeeName.Enabled = false;
                        btnPickEmployee.Enabled = false;
                    }
                }
                else
                {
                    txtEmployeeNo.Enabled = false;
                    txtEmployeeName.Enabled = false;
                    btnPickEmployee.Enabled = false;
                }
                if (rbNonEmployee.Checked == true)
                {
                    txtEmployee.Text = "M";
                    if (ClsUtility.FormMode != ClsUtility.enmFormMode.NormalMode)
                    {
                        txtMemNationalID.Enabled = true;
                        txtMemName.Enabled = true;
                        btnPickMemNationalID.Enabled = true;
                    }
                    else
                    {
                        txtMemNationalID.Enabled = false;
                        txtMemName.Enabled = false;
                        btnPickMemNationalID.Enabled = false;
                    }
                }
                else
                {
                    txtMemNationalID.Enabled = false;
                    txtMemName.Enabled = false;
                    btnPickMemNationalID.Enabled = false;
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtEmployee_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEmployee.Text.Trim()) == false)
                {
                    if (txtEmployee.Text.Trim().ToUpper() == "E")
                    {
                        rbEmployee.Checked = true;
                    }
                    else
                    {
                        rbNonEmployee.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnPickEmployee_Click(object sender, EventArgs e)
        {

            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT ted.id, ted.employeeno,ted.nationalid, ted.name,ted.email,ted.contactno1 FROM  SNAT.dbo.T_EmployeeDetails AS ted";
                frmsrch.infSqlWhereCondtion = " NOT EXISTS(SELECT * FROM dbo.logintable l WHERE l.employeeno=ted.employeeno)";
                frmsrch.infSqlOrderBy = " employeeno,nationalid ";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Employee ....";
                frmsrch.infCodeFieldName = "employeeno";
                frmsrch.infDescriptionFieldName = "name";
                frmsrch.infGridFieldName = " id,employeeno,name,nationalid";
                frmsrch.infGridFieldCaption = " id, Employee No,Nationa ID ,Employee Name";
                frmsrch.infGridFieldSize = "0,100,150,200";
                frmsrch.ShowDialog(this);
                if (frmsrch.DialogResult == DialogResult.OK)
                {
                    if (frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) && frmsrch.infCodeFieldText.Trim() != "")
                    {


                        txtEmployeeNo.Text = frmsrch.infCodeFieldText;
                        txtEmployeeName.Text = frmsrch.infDescriptionFieldText;
                        DataView dvDesg = new DataView();
                        dvDesg = frmsrch.infSearchReturnDataView;
                        dvDesg.RowFilter = "lSelect=1";
                        txtemail.Text = string.IsNullOrEmpty(dvDesg[0]["email"].ToString()) == true ? "" : dvDesg[0]["email"].ToString();
                        txtContactNo.Text = string.IsNullOrEmpty(dvDesg[0]["contactno1"].ToString()) == true ? "" : dvDesg[0]["contactno1"].ToString();
                        //if (string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) == false)
                        //{
                        //    int iRow = bsEmployee.Find("employeeno", frmsrch.infCodeFieldText.Trim());
                        //    bsEmployee.Position = iRow;
                        //    dvDesg.RowFilter = "";
                        //}
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtEmployeeNo_Validating(object sender, CancelEventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtEmployeeNo.Text.Trim()) == false)
                {
                    string strSqlQuery = "SELECT ted.id, ted.employeeno,ted.nationalid, ted.name,ted.email,ted.contactno1 FROM  SNAT.dbo.T_EmployeeDetails AS ted" +
                                        " Where NOT EXISTS(SELECT * FROM dbo.logintable l WHERE l.employeeno=ted.employeeno) and employeeno='" + txtEmployeeNo.Text.Trim() + "'";
                    DataTable dtDesg = new DataTable();
                    dtDesg = ClsDataLayer.GetDataTable(strSqlQuery);
                    if (dtDesg != null && dtDesg.DefaultView.Count > 0)
                    {
                        txtEmployeeName.Text = string.IsNullOrEmpty(dtDesg.DefaultView[0]["Name"].ToString()) == true ? "" : dtDesg.DefaultView[0]["Name"].ToString();
                        txtemail.Text = string.IsNullOrEmpty(dtDesg.DefaultView[0]["email"].ToString()) == true ? "" : dtDesg.DefaultView[0]["email"].ToString();
                        txtContactNo.Text = string.IsNullOrEmpty(dtDesg.DefaultView[0]["contactno1"].ToString()) == true ? "" : dtDesg.DefaultView[0]["contactno1"].ToString();
                    }
                    else
                    {
                        ClsMessage.showMessage("Invalid Employee #!", MessageBoxIcon.Information);
                        txtEmployeeNo.Focus();
                        txtEmployeeName.Text = "";
                        txtemail.Text = "";
                        txtContactNo.Text = "";

                    }

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);

            }
        }
        private void btnPickMemNationalID_Click(object sender, EventArgs e)
        {

            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT nationalid, memberid, employeeno, tscno, membername, contactno1, email FROM SNAT.dbo.T_Member tm (nolock)";
                frmsrch.infSqlWhereCondtion = " Isnull(tm.livingstatus,'')='L'  AND NOT EXISTS(SELECT * FROM dbo.logintable l WHERE l.Memnationalid=tm.nationalid)";
                frmsrch.infSqlOrderBy = " nationalid, memberid, employeeno, tscno, membername";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Member ....";
                frmsrch.infCodeFieldName = "nationalid";
                frmsrch.infDescriptionFieldName = "membername";
                frmsrch.infGridFieldName = " nationalid, memberid, employeeno, tscno, membername, contactno1, email";
                frmsrch.infGridFieldCaption = " National id,Member ID, Employee No,TSC No. ,Member Name,contactno1, email";
                frmsrch.infGridFieldSize = "100,100,100,100,200,0,0";
                frmsrch.ShowDialog(this);
                if (frmsrch.DialogResult == DialogResult.OK)
                {
                    if (frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) && frmsrch.infCodeFieldText.Trim() != "")
                    {


                        txtMemNationalID.Text = frmsrch.infCodeFieldText;
                        txtMemName.Text = frmsrch.infDescriptionFieldText;
                        DataView dvDesg = new DataView();
                        dvDesg = frmsrch.infSearchReturnDataView;
                        dvDesg.RowFilter = "lSelect=1";
                        txtemail.Text = string.IsNullOrEmpty(dvDesg[0]["email"].ToString()) == true ? "" : dvDesg[0]["email"].ToString();
                        txtContactNo.Text = string.IsNullOrEmpty(dvDesg[0]["contactno1"].ToString()) == true ? "" : dvDesg[0]["contactno1"].ToString();
                       
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtMemNationalID_Validating(object sender, CancelEventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtEmployeeNo.Text.Trim()) == false)
                {
                    string strSqlQuery = "SELECT nationalid, memberid, employeeno, tscno, membername, contactno1, email FROM SNAT.dbo.T_Member tm (nolock)" +
                                        " Where Isnull(tm.livingstatus,'')='L'  AND NOT EXISTS(SELECT * FROM dbo.logintable l WHERE l.Memnationalid=tm.nationalid) and nationalid='" + txtMemNationalID.Text.Trim() + "'";
                    DataTable dtDesg = new DataTable();
                    dtDesg = ClsDataLayer.GetDataTable(strSqlQuery);
                    if (dtDesg != null && dtDesg.DefaultView.Count > 0)
                    {
                        txtEmployeeName.Text = string.IsNullOrEmpty(dtDesg.DefaultView[0]["membername"].ToString()) == true ? "" : dtDesg.DefaultView[0]["membername"].ToString();
                        txtemail.Text = string.IsNullOrEmpty(dtDesg.DefaultView[0]["email"].ToString()) == true ? "" : dtDesg.DefaultView[0]["email"].ToString();
                        txtContactNo.Text = string.IsNullOrEmpty(dtDesg.DefaultView[0]["contactno1"].ToString()) == true ? "" : dtDesg.DefaultView[0]["contactno1"].ToString();
                    }
                    else
                    {
                        ClsMessage.showMessage("Invalid Member #!", MessageBoxIcon.Information);
                        txtEmployeeNo.Focus();
                        txtEmployeeName.Text = "";
                        txtemail.Text = "";
                        txtContactNo.Text = "";

                    }

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillUserDetails();
        }
    }
}

