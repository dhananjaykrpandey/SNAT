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
using SNAT.Comman_Form;
using System.IO;
using System.Drawing.Imaging;

namespace SNAT.Employee
{
    public partial class frmEmployee : Form
    {
        BindingSource bsEmployee = new BindingSource();
        string strSqlQuery = "";
        string imageFinalLocation = "";

        DataTable dtEmployee = new DataTable();
        public frmEmployee()
        {
            InitializeComponent();
            BindControl();
        }
        private void FillDataTable()
        {
            try
            {
                //string strSqlQuery = " SELECT ed.id, ed.employeeno, ed.name,ed.sex, ed.dob, ed.deptcode,dpt.name AS deptname, ed.desigcode,td.Name AS designame," +
                //                " ed.contactno1, ed.contactno2, ed.email, ed.physicaladdress, ed.dateofjoining, ed.wrokstatus, ed.leaveingdate,imagelocation" +
                //                   " FROM SNAT.dbo.T_EmployeeDetails ed (nolock)" +
                //                   " INNER JOIN dbo.T_Designation td(nolock) ON td.code=ed.desigcode" +
                //                   " LEFT OUTER JOIN dbo.T_Department dpt (nolock) ON dpt.code = td.code" +
                //                   " ORDER BY ed.employeeno, ed.name";
                string strSqlQuery = " SELECT ed.id, ed.employeeno,ed.nationalid, ed.name,ed.sex, ed.dob, ed.deptcode,dpt.name AS deptname, ed.desigcode,td.Name AS designame," +
                                      " ed.contactno1, ed.contactno2, ed.email, ed.physicaladdress, ed.dateofjoining, ed.wrokstatus, ed.leaveingdate,imagelocation,Approval_Chairperson,Approval_Sectretary,Approval_Treasurer,Approval_Premium,createdby,createddate,updateby,updateddate" +
                                      " FROM SNAT.dbo.T_EmployeeDetails ed (nolock)" +
                                      " INNER JOIN  SNAT.dbo.T_Designation td(nolock) ON td.code=ed.desigcode" +
                                      " LEFT OUTER JOIN  SNAT.dbo.T_Department dpt (nolock) ON dpt.code = td.DepartCode" +
                                      " ORDER BY ed.employeeno, ed.name";
                dtEmployee = ClsDataLayer.GetDataTable(strSqlQuery);
                //  dtEmployee = ClsDataLayer.GetDataTable(strSqlQuery);
                bsEmployee.DataSource = dtEmployee.DefaultView;
                bindingNavigatorMain.BindingSource = bsEmployee;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);

            }
        }
        private void BindControl()
        {
            try
            {
                FillDataTable();

                this.dtpDOB.NullableValue = null;
                this.dtpDOB.SetToNullValue();
                this.dtpDOB.NullText = "No Date";

                this.dtpJoiningDate.NullableValue = null;
                this.dtpJoiningDate.SetToNullValue();
                this.dtpJoiningDate.NullText = "No Date";

                this.dtpLeavingDate.NullableValue = null;
                this.dtpLeavingDate.SetToNullValue();
                this.dtpLeavingDate.NullText = "No Date";

                txtEmployeeNo.DataBindings.Add("Text", bsEmployee, "employeeno", false, DataSourceUpdateMode.OnValidation);
                txtNationalId.DataBindings.Add("Text", bsEmployee, "nationalid", false, DataSourceUpdateMode.OnValidation);
                txtEmployeeName.DataBindings.Add("Text", bsEmployee, "name", false, DataSourceUpdateMode.OnValidation);

                txtSex.DataBindings.Add("Text", bsEmployee, "sex", false, DataSourceUpdateMode.OnPropertyChanged);

                dtpDOB.DataBindings.Add("Text", bsEmployee, "dob", false, DataSourceUpdateMode.OnPropertyChanged);

                txtDesgCode.DataBindings.Add("Text", bsEmployee, "desigcode", false, DataSourceUpdateMode.OnValidation);
                txtDesgName.DataBindings.Add("Text", bsEmployee, "designame", false, DataSourceUpdateMode.OnPropertyChanged);
                txtDeptCode.DataBindings.Add("Text", bsEmployee, "deptcode", false, DataSourceUpdateMode.OnPropertyChanged);
                txtDeptDesc.DataBindings.Add("Text", bsEmployee, "deptname", false, DataSourceUpdateMode.OnPropertyChanged);

                txtContactNo1.DataBindings.Add("Text", bsEmployee, "contactno1", false, DataSourceUpdateMode.OnValidation);
                txtContactNo2.DataBindings.Add("Text", bsEmployee, "contactno2", false, DataSourceUpdateMode.OnValidation);

                txtEmail.DataBindings.Add("Text", bsEmployee, "email", false, DataSourceUpdateMode.OnValidation);
                txtResidentalAddress.DataBindings.Add("Text", bsEmployee, "physicaladdress", false, DataSourceUpdateMode.OnValidation);


                dtpJoiningDate.DataBindings.Add("Text", bsEmployee, "dateofjoining", false, DataSourceUpdateMode.OnPropertyChanged);
                txtStatus.DataBindings.Add("Text", bsEmployee, "wrokstatus", false, DataSourceUpdateMode.OnPropertyChanged);
                // dtpLeavingDate.DataBindings.Add("Text", bsEmployee, "leaveingdate", false, DataSourceUpdateMode.OnPropertyChanged);
                txtImageLocation.DataBindings.Add("Text", bsEmployee, "imagelocation", false, DataSourceUpdateMode.OnPropertyChanged);

                this.dtpLeavingDate.DataBindings.Add(new Binding("Value", this.bsEmployee, "leaveingdate", true, DataSourceUpdateMode.OnValidation, System.DateTime.Now.Date.AddSeconds(1), ""));

                txtApproval_Chairperson.DataBindings.Add("Text", bsEmployee, "Approval_Chairperson", false, DataSourceUpdateMode.OnPropertyChanged);
                txtApproval_Sectretary.DataBindings.Add("Text", bsEmployee, "Approval_Sectretary", false, DataSourceUpdateMode.OnPropertyChanged);
                txtApproval_Treasurer.DataBindings.Add("Text", bsEmployee, "Approval_Treasurer", false, DataSourceUpdateMode.OnPropertyChanged);
                txtApproval_Premium.DataBindings.Add("Text", bsEmployee, "Approval_Premium", false, DataSourceUpdateMode.OnPropertyChanged);
                // chkStatus.DataBindings.Add("Checked", bsDepartmetn, "status", false);
                grdList.DataSource = bsEmployee;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SetEnable(bool lValue)
        {
            txtEmployeeNo.Enabled = lValue;
            txtEmployeeName.Enabled = lValue;
            txtSex.Enabled = lValue;
            rbMale.Enabled = lValue;
            rbFemale.Enabled = lValue;
            dtpDOB.Enabled = lValue;
            txtNationalId.Enabled = lValue;
            btnselectpicture.Enabled = lValue;

            txtDesgCode.Enabled = lValue;
            txtDesgName.Enabled = lValue;
            txtDeptDesc.Enabled = lValue;
            btnPickDesg.Enabled = lValue;

            txtContactNo1.Enabled = lValue;
            txtContactNo2.Enabled = lValue;
            txtEmail.Enabled = lValue;
            txtResidentalAddress.Enabled = lValue;

            dtpJoiningDate.Enabled = lValue;
            txtStatus.Enabled = lValue;
            rbWorking.Enabled = lValue;
            rbResgine.Enabled = lValue;
            dtpLeavingDate.Enabled = lValue;
            chkApproval_Chairperson.Enabled = lValue;
            chkApproval_Sectretary.Enabled = lValue;
            chkApproval_Treasurer.Enabled = lValue;
            chkApproval_Premium.Enabled = lValue;

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
            errorProvider1.Clear();
            btnRefresh.Enabled = !lValue;
            btnDelete.Enabled = !lValue;
        }
        private void SetFormMode(ClsUtility.enmFormMode _FormMode)
        {
            switch (_FormMode)
            {
                case ClsUtility.enmFormMode.AddMode:
                    SetEnable(true);
                    txtEmployeeNo.Focus();
                    break;
                case ClsUtility.enmFormMode.EditMode:
                    SetEnable(true);
                    txtEmployeeNo.Enabled = false;
                    break;
                case ClsUtility.enmFormMode.NormalMode:
                    SetEnable(false);
                    break;
            }
        }
        private void frmEmployee_Load(object sender, EventArgs e)
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

        private void rbMan_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked == true)
            {
                txtSex.Text = "M";
            }
            if (rbFemale.Checked == true)
            {
                txtSex.Text = "F";
            }
        }

        private void txtSex_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSex.Text.Trim()) == false)
            {
                if (txtSex.Text.ToUpper().Trim() == "M")
                {
                    rbMale.Checked = true;
                }
                if (txtSex.Text.ToUpper().Trim() == "F")
                {
                    rbFemale.Checked = true;
                }
            }
        }

        private void rbWorking_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWorking.Checked == true)
            {
                txtStatus.Text = "W";
            }
            if (rbResgine.Checked == true)
            {
                txtStatus.Text = "R";
            }
        }

        private void txtStatus_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStatus.Text.Trim()) == false)
            {
                if (txtStatus.Text.ToUpper().Trim() == "W")
                {
                    rbWorking.Checked = true;
                }
                if (txtStatus.Text.ToUpper().Trim() == "R")
                {
                    rbResgine.Checked = true;
                }
            }
        }

        private void txtImageLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtImageLocation.Text.Trim()) == false)
                {
                    if (System.IO.File.Exists(txtImageLocation.Text.Trim()))
                    {


                        picImage.Image = ClsUtility.GetImageFormFile(txtImageLocation.Text.Trim());





                    }

                }
                else
                {

                    Bitmap image = new Bitmap(Properties.Resources.img_not_available);
                    if (image != null)
                    {
                        picImage.Image = image;
                    }


                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }

        private void btnselectpicture_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog oplg = new OpenFileDialog();
                oplg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                if (oplg.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.File.Exists(oplg.FileName.ToString()))
                    {
                        Bitmap image = new Bitmap(oplg.FileName.ToString());
                        if (image != null)
                        {
                            if (image.Width > 120 || image.Height > 120)
                            {
                                ClsMessage.showMessage("Employee image max size(Height & Width) is 120 X 120", MessageBoxIcon.Information);

                                image = new Bitmap(Properties.Resources.img_not_available120X120);
                                if (image != null)
                                {
                                    picImage.Image = image;
                                    txtImageLocation.Text = Application.StartupPath + @"\img_not_available120X120.png";
                                }
                            }
                            else
                            {
                                picImage.Image = image;
                                txtImageLocation.Text = oplg.FileName.ToString();
                            }




                        }

                    }
                    else
                    {

                        Bitmap image = new Bitmap(Properties.Resources.img_not_available120X120);
                        if (image != null)
                        {
                            picImage.Image = image;
                            txtImageLocation.Text = Application.StartupPath + @"\img_not_available120X120.png";
                        }


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
                bsEmployee.AllowNew = true;
                bsEmployee.AddNew();
                txtSex.Text = "M";
                txtStatus.Text = "W";
                this.dtpLeavingDate.NullableValue = null;
                this.dtpLeavingDate.SetToNullValue();
                this.dtpLeavingDate.NullText = "No Date";
                ClsUtility.FormMode = ClsUtility.enmFormMode.AddMode;
                SetFormMode(ClsUtility.enmFormMode.AddMode);
                //ClsDataLayer.setLogAcitivity("Employee Details", ClsSettings.username, "Trying to Add New New Employee into System", "", "New Employee", "Msg");

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
                txtNationalId.Enabled = false;
                txtEmployeeName.Focus();
                //ClsDataLayer.setLogAcitivity("Employee Details", ClsSettings.username, "Editing Existing Employee", "", " ", "Msg");
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                //ClsDataLayer.setLogAcitivity("Employee Details", ClsSettings.username, ""+ex.Message.ToString(), "", "Some Problem Occured while Editing Existing Employee Details", "Msg");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
                {
                    Int32 iRow = bsEmployee.Position;
                    bsEmployee.RemoveAt(iRow);
                    // dtDepartment.DefaultView.Delete(iRow);
                    bsEmployee.EndEdit();
                    if (dtEmployee != null && dtEmployee.DefaultView.Count > 0)
                    {
                        if (dtEmployee.GetChanges() != null)
                        {
                            dtEmployee.DefaultView[iRow].BeginEdit();
                            dtEmployee.DefaultView[iRow].EndEdit();
                            bool lReturn = false;
                            strSqlQuery = "SELECT ed.id, ed.employeeno, ed.name, ed.dob, ed.deptcode, ed.desigcode, ed.contactno1, ed.contactno2, ed.email, ed.physicaladdress, ed.dateofjoining, ed.wrokstatus, ed.leaveingdate,ed.imagelocation FROM SNAT.dbo.T_EmployeeDetails ed (nolock) where 1=2";
                            lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtEmployee);

                            if (lReturn == true)
                            {
                                ClsMessage.showDeleteMessage();
                                ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                                SetFormMode(ClsUtility.enmFormMode.NormalMode);
                                dtEmployee.AcceptChanges();
                                FillDataTable();
                                //ClsDataLayer.setLogAcitivity("Employee Details", ClsSettings.username, "Deleted Existing Employee Details", "", "", "Msg");
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

                errorProvider1.Clear();


                if (string.IsNullOrEmpty(txtEmployeeNo.Text.Trim()))
                {
                    errorProvider1.SetError(txtEmployeeNo, "Employee no code cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ValidateUniqeID(txtEmployeeNo.Text.Trim(), "EMPLYEENO") == false)
                    {
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(txtNationalId.Text.Trim()))
                {
                    errorProvider1.SetError(txtNationalId, "National id cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ValidateUniqeID(txtNationalId.Text.Trim(), "NATIONALID") == false)
                    {
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(txtEmployeeName.Text.Trim()))
                {
                    errorProvider1.SetError(txtEmployeeName, "Employee name cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(dtpDOB.Text.Trim()))
                {
                    errorProvider1.SetError(dtpDOB, "Date of birth  cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtSex.Text.Trim()))
                {
                    errorProvider1.SetError(panel1, "Employee sex cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtDesgCode.Text.Trim()))
                {
                    errorProvider1.SetError(txtDesgName, "Employee designation cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtContactNo1.Text.Trim()))
                {
                    errorProvider1.SetError(txtContactNo1, "Employee first contact no cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                {
                    errorProvider1.SetError(txtEmail, "Employee email id cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtEmail.Text.Trim()) == false && ClsUtility.IsValidEmail(txtEmail.Text.Trim()) == false)
                {
                    //ClsMessage.showMessage("Invalid email id!!", MessageBoxIcon.Information);
                    errorProvider1.SetError(txtEmail, "Invalid email id!!");
                    txtEmail.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(dtpJoiningDate.Text.Trim()))
                {
                    errorProvider1.SetError(dtpJoiningDate, "Employee joining date cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtStatus.Text.Trim()))
                {
                    errorProvider1.SetError(panel2, "Employee working status cannot be left blank.");
                    return false;
                }
                if (rbResgine.Checked == true)
                {
                    if (string.IsNullOrEmpty(dtpLeavingDate.Text.Trim()))
                    {
                        errorProvider1.SetError(dtpLeavingDate, "Reregistration /Terminated employee leaving cannot be left blank.");
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
                bsEmployee.EndEdit();
                if (dtEmployee != null && dtEmployee.DefaultView.Count > 0)
                {
                    Int32 iRow = bsEmployee.Position;
                    dtEmployee.DefaultView[iRow].BeginEdit();
                    if (rbWorking.Checked == true)
                    {
                        dtEmployee.DefaultView[iRow]["wrokstatus"] = "W";
                    }
                    if (rbResgine.Checked == true)
                    {
                        dtEmployee.DefaultView[iRow]["wrokstatus"] = "R";
                    }

                    if (rbMale.Checked == true)
                    {
                        dtEmployee.DefaultView[iRow]["Sex"] = "M";
                    }
                    if (rbFemale.Checked == true)
                    {
                        dtEmployee.DefaultView[iRow]["Sex"] = "F";
                    }
                    if (string.IsNullOrEmpty(dtpDOB.Text.Trim()) == false)
                    {
                        dtEmployee.DefaultView[iRow]["dob"] = dtpDOB.Text.Trim();
                    }
                    if (string.IsNullOrEmpty(dtpJoiningDate.Text.Trim()) == false)
                    {
                        dtEmployee.DefaultView[iRow]["dateofjoining"] = dtpJoiningDate.Text.Trim();
                    }
                    if (rbResgine.Checked == true && string.IsNullOrEmpty(dtpLeavingDate.Text.Trim()) == false)
                    {
                        dtEmployee.DefaultView[iRow]["leaveingdate"] = dtpLeavingDate.Text.Trim();
                    }
                    else
                    {
                        dtEmployee.DefaultView[iRow]["leaveingdate"] = DBNull.Value;
                    }

                    if (string.IsNullOrEmpty(txtImageLocation.Text.Trim()) == false)
                    {
                        if (File.Exists(txtImageLocation.Text.Trim()))
                        {
                            string imagenewlocation = Path.GetFullPath(txtImageLocation.Text.Trim());
                            string imageext = Path.GetExtension(txtImageLocation.Text.Trim());

                            dtEmployee.DefaultView[iRow]["imagelocation"] = ClsSettings.strEmployeeImageFolder + @"\" + txtNationalId.Text.Trim() + imageext;
                            imageFinalLocation = ClsSettings.strEmployeeImageFolder + @"\" + txtNationalId.Text.Trim() + imageext;
                            CopyEmployeeImage(imageFinalLocation);
                        }

                    }

                    dtEmployee.DefaultView[iRow]["Approval_Chairperson"] = chkApproval_Chairperson.Checked;
                    dtEmployee.DefaultView[iRow]["Approval_Sectretary"] = chkApproval_Sectretary.Checked;
                    dtEmployee.DefaultView[iRow]["Approval_Treasurer"] = chkApproval_Treasurer.Checked;
                    dtEmployee.DefaultView[iRow]["Approval_Premium"] = chkApproval_Premium.Checked;
                    if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                    {

                        dtEmployee.DefaultView[iRow]["createdby"] = ClsSettings.username;
                        dtEmployee.DefaultView[iRow]["createddate"] = DateTime.Now.ToString();
                    }
                    dtEmployee.DefaultView[iRow]["updateby"] = ClsSettings.username;
                    dtEmployee.DefaultView[iRow]["updateddate"] = DateTime.Now.ToString();


                    dtEmployee.DefaultView[iRow].EndEdit();
                    if (dtEmployee.GetChanges() != null)
                    {

                        bool lReturn = false;
                        strSqlQuery = "SELECT id, employeeno, nationalid, name, sex, dob, deptcode, desigcode, contactno1, contactno2, email, physicaladdress, dateofjoining, wrokstatus, leaveingdate, imagelocation ,Approval_Chairperson,Approval_Sectretary,Approval_Treasurer,Approval_Premium,createdby,createddate,updateby,updateddate FROM  SNAT.dbo.T_EmployeeDetails AS ted (nolock) where 1=2";

                        iRow = bsEmployee.Position;
                        dtEmployee.DefaultView[iRow].BeginEdit();
                        dtEmployee.DefaultView[iRow].EndEdit();
                        lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtEmployee);
                        dtEmployee.AcceptChanges();
                        if (lReturn == true)
                        {
                            string strEmail = "";
                            strEmail = "INSERT INTO SNAT.dbo.T_Email_SMS_Alert  (EmpNationalID,EmpNo,AlertyID,createdby,createddate)" + Environment.NewLine +
                                        " SELECT '" + txtNationalId.Text.Trim() + "' EmpNationalID,'" + txtEmployeeNo.Text.Trim() + "'" + Environment.NewLine +
                                        " EmpNo, mat.id,'" + ClsSettings.username + "' createdby ,GetDate() createddate" + Environment.NewLine + 
                                        " FROM SNAT.dbo.M_AlertType mat WITH(nolock)" + Environment.NewLine +
                                        " WHERE NOT EXISTS(SELECT * FROM SNAT.dbo.T_Email_SMS_Alert AS tesa WITH(nolock) " + Environment.NewLine +
                                        " WHERE mat.id = tesa.AlertyID AND tesa.EmpNationalID = '" + txtNationalId.Text.Trim() + "' AND tesa.EmpNo = '" + txtEmployeeNo.Text.Trim() + "')";
                            ClsDataLayer.UpdateData(strEmail);

                            ClsMessage.showSaveMessage();
                            //ClsDataLayer.setLogAcitivity("Employee Details", ClsSettings.username, "Employee Details Saved Successfully", "", " ", "Msg");
                            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                            SetFormMode(ClsUtility.enmFormMode.NormalMode);


                            FillDataTable();
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
                bsEmployee.CancelEdit();
                dtEmployee.RejectChanges();
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
                frmsrch.infSqlSelectQuery = "SELECT ted.id, ted.employeeno,ted.nationalid, ted.name FROM  SNAT.dbo.T_EmployeeDetails AS ted";
                frmsrch.infSqlWhereCondtion = "";
                frmsrch.infSqlOrderBy = " employeeno,nationalid ";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Employee ....";
                frmsrch.infCodeFieldName = "employeeno";
                frmsrch.infDescriptionFieldName = "nationalid";
                frmsrch.infGridFieldName = " id,employeeno,name,nationalid";
                frmsrch.infGridFieldCaption = " id, Employee No,Nationa ID ,Employee Name";
                frmsrch.infGridFieldSize = "0,100,150,200";
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
                            int iRow = bsEmployee.Find("employeeno", frmsrch.infCodeFieldText.Trim());
                            bsEmployee.Position = iRow;
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


        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {

        }

        private void btnPickDesg_Click(object sender, EventArgs e)
        {
            try
            {

                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT ds.id, ds.code, ds.Name,td.code deptcode,td.name deptname  FROM   SNAT.dbo.T_Designation  ds (nolock) LEFT OUTER JOIN	SNAT.dbo.T_Department td  (nolock) ON td.code = ds.DepartCode ";
                frmsrch.infSqlWhereCondtion = "  isnull(lStatus,0)=1 AND ds.code<>'SNAT' ";
                frmsrch.infSqlOrderBy = " Code ";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Designation ....";
                frmsrch.infCodeFieldName = "code";
                frmsrch.infDescriptionFieldName = "name";
                frmsrch.infGridFieldName = " id,code,name,deptname";
                frmsrch.infGridFieldCaption = " id, Designation Code,Designation Name,Department Name";
                frmsrch.infGridFieldSize = "0,100,150,200";
                frmsrch.ShowDialog(this);
                if (frmsrch.DialogResult == DialogResult.OK)
                {
                    if (frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) && frmsrch.infCodeFieldText.Trim() != "")
                    {
                        txtDesgCode.Text = frmsrch.infCodeFieldText;
                        txtDesgName.Text = frmsrch.infDescriptionFieldText;
                        DataView dvDesg = new DataView();
                        dvDesg = frmsrch.infSearchReturnDataView;
                        dvDesg.RowFilter = "lSelect=1";
                        txtDeptCode.Text = string.IsNullOrEmpty(dvDesg[0]["deptcode"].ToString()) == true ? "" : dvDesg[0]["deptcode"].ToString();
                        txtDeptDesc.Text = string.IsNullOrEmpty(dvDesg[0]["deptname"].ToString()) == true ? "" : dvDesg[0]["deptname"].ToString();
                        dvDesg.RowFilter = "";
                    }
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void txtDesgCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDesgCode.Text.Trim()) == false)
                {
                    string strSqlQuery = "SELECT ds.id, ds.code, ds.Name,td.code deptcode,td.name deptname  FROM   SNAT.dbo.T_Designation  ds (nolock)" +
                                        " LEFT OUTER JOIN	SNAT.dbo.T_Department td  (nolock) ON td.code = ds.DepartCode " +
                                        " Where  isnull(lStatus,0)=1 and ds.code='" + txtDesgCode.Text.Trim() + "'";
                    DataTable dtDesg = new DataTable();
                    dtDesg = ClsDataLayer.GetDataTable(strSqlQuery);
                    if (dtDesg != null && dtDesg.DefaultView.Count > 0)
                    {
                        txtDesgName.Text = string.IsNullOrEmpty(dtDesg.DefaultView[0]["Name"].ToString()) == true ? "" : dtDesg.DefaultView[0]["Name"].ToString();
                        txtDeptCode.Text = string.IsNullOrEmpty(dtDesg.DefaultView[0]["deptcode"].ToString()) == true ? "" : dtDesg.DefaultView[0]["deptcode"].ToString();
                        txtDeptDesc.Text = string.IsNullOrEmpty(dtDesg.DefaultView[0]["deptname"].ToString()) == true ? "" : dtDesg.DefaultView[0]["deptname"].ToString();
                    }
                    else
                    {
                        ClsMessage.showMessage("Invalid Desgination!", MessageBoxIcon.Information);
                        txtDesgCode.Focus();
                        txtDesgName.Text = "";
                        txtDeptCode.Text = "";
                        txtDeptDesc.Text = "";

                    }

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);

            }
        }
        private bool ValidateUniqeID(string strValue, string strValidate)
        {
            try
            {
                switch (strValidate.ToUpper())
                {
                    case "EMPLYEENO":
                        if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_EmployeeDetails", "employeeno", "employeeno", strValue) == true)
                        {
                            ClsMessage.showMessage("Employee no already exists!", MessageBoxIcon.Information);
                            txtEmployeeNo.Focus();
                            return false;
                        }
                        break;
                    case "NATIONALID":
                        if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_EmployeeDetails", "nationalid", "nationalid", strValue) == true)
                        {
                            ClsMessage.showMessage("Employee national id already exists!", MessageBoxIcon.Information);
                            txtNationalId.Focus();
                            return false;
                        }
                        break;
                }


                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);
                return false;

            }
        }

        private void CopyEmployeeImage(string imagefilanlocation)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtImageLocation.Text.Trim()) && File.Exists(txtImageLocation.Text.Trim()))
                {
                    if (Directory.Exists(ClsSettings.strEmployeeImageFolder))
                    {
                        GC.Collect(100000000, GCCollectionMode.Forced);
                        string strTmpLocation = Path.GetTempPath() + Path.GetFileName(txtImageLocation.Text.Trim());
                        File.Copy(txtImageLocation.Text.Trim(), strTmpLocation, true);

                        if (File.Exists(imagefilanlocation))
                        {
                            File.Delete(imagefilanlocation);
                            File.Copy(strTmpLocation, imagefilanlocation, true);
                        }
                        else
                        {
                            File.Copy(strTmpLocation, imagefilanlocation, true);
                        }
                        File.Delete(strTmpLocation);
                        File.SetAttributes(imagefilanlocation, FileAttributes.Normal);


                    }
                    else
                    {
                        Directory.CreateDirectory(ClsSettings.strEmployeeImageFolder);
                        File.Copy(txtImageLocation.Text.Trim(), imagefilanlocation, true);

                    }


                }


            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);


            }
        }
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        private void txtApproval_Chairperson_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtApproval_Chairperson.Text.Trim()))
                {
                    chkApproval_Chairperson.Checked = Convert.ToBoolean((txtApproval_Chairperson.Text.Trim()));
                }
                else
                {
                    chkApproval_Chairperson.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex.Message);
            }
        }

        private void txtApproval_Sectretary_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(txtApproval_Sectretary.Text.Trim()))
                {
                    chkApproval_Sectretary.Checked = Convert.ToBoolean((txtApproval_Sectretary.Text.Trim()));
                }
                else
                {
                    chkApproval_Sectretary.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex.Message);
            }
        }

        private void txtApproval_Treasurer_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(txtApproval_Treasurer.Text.Trim()))
                {
                    chkApproval_Treasurer.Checked = Convert.ToBoolean((txtApproval_Treasurer.Text.Trim()));
                }
                else
                {
                    chkApproval_Treasurer.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataTable();
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex.Message);
            }
        }

        private void txtApproval_Premium_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(txtApproval_Premium.Text.Trim()))
                {
                    chkApproval_Premium.Checked = Convert.ToBoolean((txtApproval_Premium.Text.Trim()));
                }
                else
                {
                    chkApproval_Chairperson.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex.Message);
            }
        }
    }
}
