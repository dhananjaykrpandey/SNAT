using SNAT.Comman_Classes;
using SNAT.Comman_Form;
using SNAT.CommanClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SNAT.Member
{
    public partial class frmMemberEntry : Form
    {
        BindingSource bsMemmberEntery = new BindingSource();
        string strSqlQuery = "";
        DataTable dtMemberEntery = new DataTable();
        string imageFinalLocation = "";
        public frmMemberEntry()
        {
            InitializeComponent();
            BindControl();

        }
        void FillMemberData()
        {
            try
            {
                strSqlQuery = "SELECT mb.id,mb.nationalid,mb.memberid ,mb.employeeno ,mb.tscno ,mb.membername ,mb.dob ,mb.sex ,mb.school,ms.name schoolname " + Environment.NewLine +
                               " ,mb.contactno1 ,mb.contactno2 ,mb.residentaladdress ,mb.nomineenationalid ,mb.nomineename ,mb.wagesamount ,mb.wageseffectivedete" + Environment.NewLine +
                                " ,mb.imagelocation ,mb.createdby ,mb.createddate ,mb.updateby ,mb.updateddate,mb.email,mb.cWorkingStatus,mb.livingstatus," + Environment.NewLine +
                                " mb.deathdate, mb.mritalstatus , mb.suposenationaid , mb.suposename , mb.suposegender , mb.suposejoindate,CASE WHEN ISNULL(mb.lActive,0)=0 THEN 'Member Active Status : IN-Active' ELSE 'Member Active Status : Active' END cActive,mb.lActive " + Environment.NewLine +
                                " FROM SNAT.dbo.T_Member mb (nolock)" + Environment.NewLine +
                                " LEFT OUTER JOIN SNAT.dbo.M_School ms (nolock) ON ms.code=mb.school order by mb.nationalid,mb.memberid ,mb.employeeno";
                dtMemberEntery = ClsDataLayer.GetDataTable(strSqlQuery);
                bsMemmberEntery.DataSource = dtMemberEntery.DefaultView;
                bindingNavigatorMain.BindingSource = bsMemmberEntery;
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
                FillMemberData();

                txtNationalId.DataBindings.Add(new Binding("Text", bsMemmberEntery, "nationalid", false, DataSourceUpdateMode.OnValidation));
                txtMemberID.DataBindings.Add("Text", bsMemmberEntery, "memberid", false, DataSourceUpdateMode.OnValidation);
                txtEmployeeNo.DataBindings.Add("Text", bsMemmberEntery, "employeeno", false, DataSourceUpdateMode.OnValidation);
                txtTSCNo.DataBindings.Add("Text", bsMemmberEntery, "tscno", false, DataSourceUpdateMode.OnValidation);
                txtMemberName.DataBindings.Add("Text", bsMemmberEntery, "membername", false, DataSourceUpdateMode.OnValidation);
                dtpDOB.DataBindings.Add("Text", bsMemmberEntery, "dob", false, DataSourceUpdateMode.OnPropertyChanged);
                txtSex.DataBindings.Add("Text", bsMemmberEntery, "sex", false, DataSourceUpdateMode.OnPropertyChanged);
                txtSchoolCode.DataBindings.Add("Text", bsMemmberEntery, "school", false, DataSourceUpdateMode.OnValidation);
                txtSchoolDesc.DataBindings.Add("Text", bsMemmberEntery, "schoolname", false, DataSourceUpdateMode.OnValidation);
                txtContactNo1.DataBindings.Add("Text", bsMemmberEntery, "contactno1", false, DataSourceUpdateMode.OnValidation);
                txtContactNo2.DataBindings.Add("Text", bsMemmberEntery, "contactno2", false, DataSourceUpdateMode.OnValidation);
                txtResidentalAddress.DataBindings.Add("Text", bsMemmberEntery, "residentaladdress", false, DataSourceUpdateMode.OnValidation);
                txtNomineeNationalId.DataBindings.Add("Text", bsMemmberEntery, "nomineenationalid", false, DataSourceUpdateMode.OnValidation);
                txtNomineeName.DataBindings.Add("Text", bsMemmberEntery, "nomineename", false, DataSourceUpdateMode.OnValidation);
                txtWagesAmount.DataBindings.Add("Text", bsMemmberEntery, "wagesamount", false, DataSourceUpdateMode.OnPropertyChanged);
                dtpWagesEffectiveDate.DataBindings.Add("Text", bsMemmberEntery, "wageseffectivedete", false, DataSourceUpdateMode.OnPropertyChanged);
                txtImageLocation.DataBindings.Add("Text", bsMemmberEntery, "imagelocation", false, DataSourceUpdateMode.OnPropertyChanged);
                txtemailid.DataBindings.Add("Text", bsMemmberEntery, "email", false, DataSourceUpdateMode.OnValidation);

                dtpDeathDate.DataBindings.Add("Text", bsMemmberEntery, "deathdate", false, DataSourceUpdateMode.OnPropertyChanged);
                txtLivingStatus.DataBindings.Add("Text", bsMemmberEntery, "livingstatus", false, DataSourceUpdateMode.OnPropertyChanged);
                txtWorkingStatus.DataBindings.Add("Text", bsMemmberEntery, "cWorkingStatus", false, DataSourceUpdateMode.OnPropertyChanged);


                txtMritalStatus.DataBindings.Add("Text", bsMemmberEntery, "mritalstatus", false, DataSourceUpdateMode.OnPropertyChanged);
                txtsupouseNationalId.DataBindings.Add("Text", bsMemmberEntery, "suposenationaid", false, DataSourceUpdateMode.OnValidation);

                dtpSuposeJoinDate.DataBindings.Add("Text", bsMemmberEntery, "suposejoindate", false, DataSourceUpdateMode.OnPropertyChanged);
                txtsupouseName.DataBindings.Add("Text", bsMemmberEntery, "suposename", false, DataSourceUpdateMode.OnPropertyChanged);
                txtSpouseGender.DataBindings.Add("Text", bsMemmberEntery, "suposegender", false, DataSourceUpdateMode.OnPropertyChanged);
                lblActiveStatus.DataBindings.Add("Text", bsMemmberEntery, "cActive", false, DataSourceUpdateMode.OnPropertyChanged);

                grdList.DataSource = bsMemmberEntery;

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);

            }
        }
        private void SetEnable(bool lValue)
        {
            txtNationalId.Enabled = lValue;
            txtMemberID.Enabled = lValue;
            txtEmployeeNo.Enabled = lValue;
            txtTSCNo.Enabled = lValue;
            txtMemberName.Enabled = lValue;
            dtpDOB.Enabled = lValue;
            rbMale.Enabled = lValue;
            rbFemale.Enabled = lValue;
            txtSchoolCode.Enabled = lValue;
            btnPickSchool.Enabled = lValue;
            txtContactNo1.Enabled = lValue;
            txtContactNo2.Enabled = lValue;
            txtResidentalAddress.Enabled = lValue;
            txtNomineeName.Enabled = lValue;
            txtNomineeNationalId.Enabled = lValue;

            dtpWagesEffectiveDate.Enabled = lValue;
            btnselectpicture.Enabled = lValue;
            txtemailid.Enabled = lValue;
            rbWorking.Enabled = lValue;
            rbRetired.Enabled = lValue;
            rbLiving.Enabled = lValue;
            rbDead.Enabled = lValue;
            dtpDeathDate.Enabled = lValue;

            txtsupouseNationalId.Enabled = lValue;
            txtsupouseName.Enabled = lValue;
            dtpSuposeJoinDate.Enabled = lValue;
            rbSpouseMale.Enabled = lValue;
            rbSpouseFemale.Enabled = lValue;
            rbSingle.Enabled = lValue;
            rbMarried.Enabled = lValue;
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
            txtWagesAmount.Enabled = false;
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
                    txtNationalId.Enabled = false;
                    txtMemberID.Enabled = false;
                    txtEmployeeNo.Enabled = false;
                    break;
                case ClsUtility.enmFormMode.NormalMode:
                    SetEnable(false);
                    break;
            }
        }
        private void frmMemberEntry_Load(object sender, EventArgs e)
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
        private void btnPickSchool_Click(object sender, EventArgs e)
        {

            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT  id,code,name,status,remarks FROM SNAT.dbo.M_School";
                frmsrch.infSqlWhereCondtion = " ";
                frmsrch.infSqlOrderBy = " code,name ";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Department ....";
                frmsrch.infCodeFieldName = "code";
                frmsrch.infDescriptionFieldName = "name";
                frmsrch.infGridFieldName = "id,code,name";
                frmsrch.infGridFieldCaption = "id, School Code,School Name";
                frmsrch.infGridFieldSize = "0,100,150";
                frmsrch.ShowDialog(this);
                if (frmsrch.DialogResult == DialogResult.OK && frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()))
                {
                    txtSchoolCode.Text = frmsrch.infCodeFieldText;
                    txtSchoolDesc.Text = frmsrch.infDescriptionFieldText;
                }
                //DataView dvDesg = new DataView();
                //dvDesg = frmsrch.infSearchReturnDataView;
                //if (dvDesg != null && dvDesg.Count > 0)
                //{


                //    dvDesg.RowFilter = "lSelect=1";
                //    //txtDeptCode.Text = string.IsNullOrEmpty(dvDesg[0]["deptcode"].ToString()) == true ? "" : dvDesg[0]["deptcode"].ToString();
                //    //txtDeptDesc.Text = string.IsNullOrEmpty(dvDesg[0]["deptname"].ToString()) == true ? "" : dvDesg[0]["deptname"].ToString();
                //    if (string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) == false)
                //    {
                //        int iRow = bsSchool.Find("Code", frmsrch.infCodeFieldText.Trim());
                //        bsSchool.Position = iRow;
                //        dvDesg.RowFilter = "";
                //    }
                //}
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtSchoolCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSchoolCode.Text.Trim()))
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.M_School", "code", "code", txtSchoolCode.Text.Trim(), txtSchoolDesc, "name") == false)
                    {
                        ClsMessage.ProjectExceptionMessage("Invalid school code!!");
                        txtSchoolCode.Focus();
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
        private void txtImageLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtImageLocation.Text.Trim()) == false)
                {
                    if (System.IO.File.Exists(txtImageLocation.Text.Trim()))
                    {

                        picImage.Image = ClsUtility.GetImageFormFile(txtImageLocation.Text.Trim());
                        GC.Collect();

                    }

                }
                else
                {

                    Bitmap image = new Bitmap(Properties.Resources.img_not_available120X120);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bsMemmberEntery.AllowNew = true;
                bsMemmberEntery.AddNew();
                txtSex.Text = "M";
                //txtWagesAmount.Text = "50";
                txtWagesAmount.Text = "60";

                ClsUtility.FormMode = ClsUtility.enmFormMode.AddMode;
                SetFormMode(ClsUtility.enmFormMode.AddMode);
                rbSingle.Checked = true;
                rbLiving.Checked = true;

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
                txtMemberID.Enabled = false;
                txtEmployeeNo.Enabled = false;
                txtWagesAmount.Enabled = false;
                dtpWagesEffectiveDate.Enabled = false;
                txtMemberName.Focus();
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
                if (ValidateDetete() == false) { return; }
                if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
                {
                    Int32 iRow = bsMemmberEntery.Position;
                    bsMemmberEntery.RemoveAt(iRow);
                    // dtDepartment.DefaultView.Delete(iRow);
                    bsMemmberEntery.EndEdit();
                    if (dtMemberEntery != null && dtMemberEntery.DefaultView.Count > 0)
                    {
                        //dtMemberEntery.DefaultView[iRow].BeginEdit();

                        //dtMemberEntery.DefaultView[iRow].EndEdit();
                        if (dtMemberEntery.GetChanges() != null)
                        {

                            bool lReturn = false;
                            strSqlQuery = "SELECT id , nationalid , memberid , employeeno , tscno , membername , dob , sex , school , contactno1 , contactno2 , residentaladdress , nomineenationalid , nomineename , wagesamount , wageseffectivedete , imagelocation , createdby , createddate , updateby , updateddate,email FROM SNAT.dbo.T_Member (nolock) WHERE 1=2";
                            lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtMemberEntery);

                            if (lReturn == true)
                            {
                                ClsMessage.showDeleteMessage();
                                ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                                SetFormMode(ClsUtility.enmFormMode.NormalMode);
                                dtMemberEntery.AcceptChanges();
                                FillMemberData();
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

                if (string.IsNullOrEmpty(txtNationalId.Text.Trim()))
                {
                    errorProvider1.SetError(txtNationalId, "National id cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Member", "nationalid", "nationalid", txtNationalId.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtNationalId, "National id already exists.");
                        ClsMessage.showMessage("National id already exists.", MessageBoxIcon.Information);
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(txtMemberID.Text.Trim()))
                {
                    errorProvider1.SetError(txtMemberID, "Member id cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Member", "memberid", "memberid", txtMemberID.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtMemberID, "Member id already exists.");
                        ClsMessage.showMessage("Member id already exists.", MessageBoxIcon.Information);
                        return false;
                    }
                }


                if (string.IsNullOrEmpty(txtEmployeeNo.Text.Trim()))
                {
                    errorProvider1.SetError(txtEmployeeNo, "Member employee no cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Member", "employeeno", "employeeno", txtEmployeeNo.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtEmployeeNo, "Member employee no already exists.");
                        ClsMessage.showMessage("Member employee no already exists.", MessageBoxIcon.Information);
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(txtTSCNo.Text.Trim()))
                {
                    errorProvider1.SetError(txtTSCNo, "TSC no cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Member", "tscno", "tscno", txtTSCNo.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtTSCNo, "TSC no already exists.");
                        ClsMessage.showMessage("TSC no already exists.", MessageBoxIcon.Information);
                        return false;
                    }
                }


                if (string.IsNullOrEmpty(txtMemberName.Text.Trim()))
                {
                    errorProvider1.SetError(txtMemberName, "Member name cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Member", "membername", "membername", txtMemberName.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtMemberName, "Member name already exists.");
                        ClsMessage.showMessage("Member name already exists.", MessageBoxIcon.Information);
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(dtpDOB.Text.Trim()))
                {
                    errorProvider1.SetError(dtpDOB, "Date of birth  cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtSex.Text.Trim()))
                {
                    errorProvider1.SetError(panel1, "Member sex cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtSchoolCode.Text.Trim()))
                {
                    errorProvider1.SetError(txtSchoolDesc, "School code cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtContactNo1.Text.Trim()))
                {
                    errorProvider1.SetError(txtContactNo1, "First contact no cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtemailid.Text.Trim()))
                {
                    errorProvider1.SetError(txtemailid, "Email id cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtemailid.Text.Trim()) == false && ClsUtility.IsValidEmail(txtemailid.Text.Trim()) == false)
                {
                    //ClsMessage.showMessage("Invalid email id!!", MessageBoxIcon.Information);
                    errorProvider1.SetError(txtemailid, "Invalid email id!!");
                    txtemailid.Focus();
                    return false;
                }

                if (rbMarried.Checked)
                {
                    if (string.IsNullOrEmpty(txtsupouseNationalId.Text.Trim()))
                    {
                        errorProvider1.SetError(txtsupouseNationalId, "Spouse  national id cannot be left blank.");
                        return false;
                    }
                    if (string.IsNullOrEmpty(txtsupouseName.Text.Trim()))
                    {
                        errorProvider1.SetError(txtsupouseName, "Spouse  name cannot be left blank.");
                        return false;
                    }
                    if (rbSpouseMale.Checked == false && rbSpouseFemale.Checked == false)
                    {
                        errorProvider1.SetError(pnlSupposeGender, "Spouse  gender cannot be left blank.");
                        return false;
                    }

                }

                if (string.IsNullOrEmpty(txtNomineeNationalId.Text.Trim()))
                {
                    errorProvider1.SetError(txtNomineeNationalId, "Nominee national id cannot be left blank.");
                    return false;
                }

                if (string.IsNullOrEmpty(txtNomineeName.Text.Trim()))
                {
                    errorProvider1.SetError(txtNomineeName, "Nominee name cannot be left blank.");
                    return false;
                }

                if (string.IsNullOrEmpty(txtWagesAmount.Text.Trim()))
                {
                    errorProvider1.SetError(txtemailid, "Premium amount cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(dtpWagesEffectiveDate.Text.Trim()))
                {
                    errorProvider1.SetError(dtpWagesEffectiveDate, "Premium effective date cannot be left blank.");
                    return false;
                }
                //if (string.IsNullOrEmpty(txtNonmineeRelection.Text.Trim()))
                //{
                //    errorProvider1.SetError(pnlRelection, "Nominee relation cannot be left blank.");
                //    return false;
                //}
                if (string.IsNullOrEmpty(txtLivingStatus.Text.Trim()))
                {
                    errorProvider1.SetError(pnlLivingStatus, "Live status cannot be left blank.");
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


                if (ValidateSave() == false) { return; }
                bsMemmberEntery.EndEdit();
                if (dtMemberEntery != null && dtMemberEntery.DefaultView.Count > 0)
                {
                    Int32 iRow = bsMemmberEntery.Position;
                    dtMemberEntery.DefaultView[iRow].BeginEdit();
                    if (rbMale.Checked == true)
                    {
                        dtMemberEntery.DefaultView[iRow]["Sex"] = "M";
                    }
                    if (rbFemale.Checked == true)
                    {
                        dtMemberEntery.DefaultView[iRow]["Sex"] = "F";
                    }
                    if (string.IsNullOrEmpty(dtpDOB.Value.ToShortDateString().Trim()) == false)
                    {
                        dtMemberEntery.DefaultView[iRow]["dob"] = dtpDOB.Value.ToShortDateString().Trim();
                    }
                    //if (string.IsNullOrEmpty(dtp.Text.Trim()) == false)
                    //{
                    //    dtBeneficiary.DefaultView[iRow]["dateofsubmission"] = dtpDateOfSubmisson.Text.Trim();
                    //}
                    if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                    {
                        if (string.IsNullOrEmpty(dtpWagesEffectiveDate.Value.ToShortDateString().Trim()) == false)
                        {
                            if (dtpWagesEffectiveDate.Value.ToShortDateString().Trim() == "01/01/0001")
                            {
                                dtMemberEntery.DefaultView[iRow]["wageseffectivedete"] = DateTime.Now.ToShortDateString();
                            }
                            else
                            {
                                dtMemberEntery.DefaultView[iRow]["wageseffectivedete"] = dtpWagesEffectiveDate.Value.ToShortDateString().Trim();
                            }

                        }
                    }
                    if (rbWorking.Checked == true)
                    {
                        dtMemberEntery.DefaultView[iRow]["cWorkingStatus"] = "W";
                    }
                    if (rbRetired.Checked == true)
                    {
                        dtMemberEntery.DefaultView[iRow]["cWorkingStatus"] = "R";
                    }

                    if (rbLiving.Checked == true)
                    {
                        dtMemberEntery.DefaultView[iRow]["livingstatus"] = "L";
                        dtMemberEntery.DefaultView[iRow]["lActive"] = true;
                    }
                    if (rbDead.Checked == true)
                    {
                        dtMemberEntery.DefaultView[iRow]["livingstatus"] = "D";

                    }

                    if (string.IsNullOrEmpty(dtpDeathDate.Value.ToShortDateString().Trim()) == false && dtpDeathDate.Value.ToShortDateString().Trim() != "01/01/0001")
                    {
                        dtMemberEntery.DefaultView[iRow]["deathdate"] = dtpDeathDate.Value.ToShortDateString().Trim();
                    }

                    if (rbSingle.Checked == true)
                    {
                        dtMemberEntery.DefaultView[iRow]["mritalstatus"] = "S";
                    }
                    if (rbMarried.Checked == true)
                    {
                        dtMemberEntery.DefaultView[iRow]["mritalstatus"] = "M";
                    }
                    if (string.IsNullOrEmpty(txtsupouseNationalId.Text.Trim()) == false)
                    {
                        dtMemberEntery.DefaultView[iRow]["suposenationaid"] = txtsupouseNationalId.Text.Trim();
                    }
                    if (string.IsNullOrEmpty(txtsupouseName.Text.Trim()) == false)
                    {
                        dtMemberEntery.DefaultView[iRow]["suposename"] = txtsupouseName.Text.Trim();
                    }
                    if (string.IsNullOrEmpty(dtpSuposeJoinDate.Value.ToShortDateString().Trim()) == false && dtpSuposeJoinDate.Value.ToShortDateString().Trim() != "01/01/0001")
                    {
                        dtMemberEntery.DefaultView[iRow]["suposejoindate"] = dtpSuposeJoinDate.Value.ToShortDateString().Trim();
                    }
                    if (rbSpouseMale.Checked == true)
                    {
                        dtMemberEntery.DefaultView[iRow]["suposegender"] = "M";
                    }
                    if (rbSpouseFemale.Checked == true)
                    {
                        dtMemberEntery.DefaultView[iRow]["suposegender"] = "F";
                    }

                    if (string.IsNullOrEmpty(txtImageLocation.Text.Trim()) == false)
                    {
                        if (File.Exists(txtImageLocation.Text.Trim()))
                        {
                            string imagenewlocation = Path.GetFullPath(txtImageLocation.Text.Trim());
                            string imageext = Path.GetExtension(txtImageLocation.Text.Trim());

                            dtMemberEntery.DefaultView[iRow]["imagelocation"] = ClsSettings.strEmployeeImageFolder + @"\" + txtNationalId.Text.Trim() + imageext;
                            imageFinalLocation = ClsSettings.strEmployeeImageFolder + @"\" + txtNationalId.Text.Trim() + imageext;
                            CopyEmployeeImage(imageFinalLocation);
                        }

                    }
                    if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                    {

                        dtMemberEntery.DefaultView[iRow]["createdby"] = ClsSettings.username;
                        dtMemberEntery.DefaultView[iRow]["createddate"] = DateTime.Now.ToString();
                    }
                    dtMemberEntery.DefaultView[iRow]["updateby"] = ClsSettings.username;
                    dtMemberEntery.DefaultView[iRow]["updateddate"] = DateTime.Now.ToString();

                    dtMemberEntery.DefaultView[iRow].EndEdit();
                    if (dtMemberEntery.GetChanges() != null)
                    {

                        bool lReturn = false;
                        strSqlQuery = "SELECT id , nationalid , memberid , employeeno , tscno , membername , dob , sex , school , contactno1 , contactno2 , residentaladdress , nomineenationalid , nomineename , wagesamount , wageseffectivedete , imagelocation , createdby , createddate , updateby , updateddate,email,cWorkingStatus,livingstatus,deathdate, mritalstatus , suposenationaid , suposename , suposegender , suposejoindate,lActive FROM SNAT.dbo.T_Member (nolock) WHERE 1=2";

                        iRow = bsMemmberEntery.Position;
                        dtMemberEntery.DefaultView[iRow].BeginEdit();
                        dtMemberEntery.DefaultView[iRow].EndEdit();
                        lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtMemberEntery);
                        dtMemberEntery.AcceptChanges();

                        if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                        {
                            strSqlQuery = "SELECT id, nationalid, memberid, employeeno, wagesamount, effactivedate, lstatus, createdby, createddate, updatedby, updateddate FROM SNAT.dbo.T_MemberWagesDetails (nolock) WHERE 1=2";
                            DataTable dtWages = new DataTable();
                            dtWages = ClsDataLayer.GetDataTable(strSqlQuery);
                            DataRow dRow = dtWages.NewRow();
                            dRow["nationalid"] = txtNationalId.Text.Trim();
                            dRow["memberid"] = txtMemberID.Text.Trim();
                            dRow["employeeno"] = txtEmployeeNo.Text.Trim();
                            dRow["wagesamount"] = txtWagesAmount.Text.Trim();
                            dRow["effactivedate"] = dtpWagesEffectiveDate.Value.ToShortDateString().Trim();
                            dRow["lstatus"] = true;
                            dRow["createdby"] = ClsSettings.username;
                            dRow["createddate"] = DateTime.Now;
                            dRow["updatedby"] = ClsSettings.username;
                            dRow["updateddate"] = DateTime.Now;
                            dtWages.Rows.Add(dRow);
                            lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtWages);
                            dtWages.AcceptChanges();
                        }

                        if (rbMarried.Checked)
                        {
                            if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                            {
                                strSqlQuery = "SELECT id, membernationalid, memberid, membername, beneficiarynatioanalid, beneficiaryname,sex, dateofsubmission, relationship," +
                                               " nomineenationalid, nomineename,  wages, effactivedate, lstatus, createdby, createddate, updateby, updateddate FROM SNAT.dbo.T_Beneficiary (nolock) WHERE 1 = 2";
                                DataTable dtBenficiry = new DataTable();
                                dtBenficiry = ClsDataLayer.GetDataTable(strSqlQuery);
                                DataRow dRow = dtBenficiry.NewRow();
                                dRow["membernationalid"] = txtNationalId.Text.Trim();
                                dRow["memberid"] = txtMemberID.Text.Trim();
                                dRow["membername"] = txtMemberName.Text.Trim();
                                dRow["beneficiarynatioanalid"] = txtsupouseNationalId.Text.Trim();
                                dRow["beneficiaryname"] = txtsupouseName.Text.Trim();
                                dRow["sex"] = rbSpouseMale.Checked ? "M" : "F";
                                dRow["dateofsubmission"] = dtpSuposeJoinDate.Value.ToShortDateString().Trim();
                                dRow["relationship"] = "W";
                                dRow["nomineenationalid"] = txtNationalId.Text.Trim();
                                dRow["nomineename"] = txtMemberName.Text.Trim();
                                dRow["wages"] = 23;
                                dRow["effactivedate"] = dtpSuposeJoinDate.Value.ToShortDateString().Trim();
                                dRow["lstatus"] = true;
                                dRow["createdby"] = ClsSettings.username;
                                dRow["createddate"] = DateTime.Now;
                                dRow["updateby"] = ClsSettings.username;
                                dRow["updateddate"] = DateTime.Now;
                                dtBenficiry.Rows.Add(dRow);
                                lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtBenficiry);
                                dtBenficiry.AcceptChanges();
                            }
                            if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                            {
                                strSqlQuery = "SELECT id, nationalid, memberid,  beneficirynationalid, wagesamount, effactivedate, lstatus, createdby, createddate, updatedby, updateddate FROM SNAT.dbo.T_BeneficiaryWagesDetails (nolock) WHERE 1=2";
                                DataTable dtWages = new DataTable();
                                dtWages = ClsDataLayer.GetDataTable(strSqlQuery);
                                DataRow dRow = dtWages.NewRow();
                                dRow["nationalid"] = txtNationalId.Text.Trim();
                                dRow["memberid"] = txtMemberID.Text.Trim();

                                dRow["beneficirynationalid"] = txtsupouseNationalId.Text.Trim();
                                dRow["wagesamount"] = txtWagesAmount.Text.Trim();
                                dRow["effactivedate"] = dtpWagesEffectiveDate.Value.ToShortDateString().Trim();
                                dRow["lstatus"] = true;
                                dRow["createdby"] = ClsSettings.username;
                                dRow["createddate"] = DateTime.Now;
                                dRow["updatedby"] = ClsSettings.username;
                                dRow["updateddate"] = DateTime.Now;
                                dtWages.Rows.Add(dRow);
                                lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtWages);
                                dtWages.AcceptChanges();
                            }
                        }

                        if (lReturn == true)
                        {

                            ClsMessage.showSaveMessage();
                            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                            SetFormMode(ClsUtility.enmFormMode.NormalMode);
                            FillMemberData();
                            clsEmail.lMemberCreated(txtNationalId.Text.Trim(), txtEmployeeNo.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), txtWagesAmount.Text.Trim(), txtemailid.Text.Trim());
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
                bsMemmberEntery.CancelEdit();
                dtMemberEntery.RejectChanges();
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
                        int iRow = bsMemmberEntery.Find("nationalid", frmsrch.infCodeFieldText.Trim());
                        bsMemmberEntery.Position = iRow;
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
                DataTable dtReport = new DataTable();
                int iRow = 0;
                iRow = bsMemmberEntery.Position;
                dtReport = dtMemberEntery.Clone();

                dtReport.ImportRow(dtMemberEntery.DefaultView[iRow].Row);
                if (dtReport != null && dtReport.DefaultView.Count > 0)
                {
                    ClsUtility.PrintReport("rptMember.rpt", dtReport, "rptMember");//
                }
                else
                {
                    ClsMessage.ProjectExceptionMessage("No Record Found!!");
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

        private void txtWagesAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex.Message);
            }
        }

        private void txtWagesAmount_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                ClsUtility.DecilmalKeyPress(txtWagesAmount.Text.Trim(), e);
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex.Message);
            }
        }

        private void txtNationalId_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ClsUtility.NumericKeyPress(e);
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex.Message);
            }
        }

        private void txtSex_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSex.Text.Trim()))
                {
                    if (txtSex.Text.Trim().ToUpper() == "F")
                    {
                        rbFemale.Checked = true;
                    }
                    else
                    {
                        rbMale.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);
            }
        }

        private bool ValidateDetete()
        {
            try
            {
                if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Beneficiary", "membernationalid", "membernationalid", txtNationalId.Text.Trim()) == true)
                {

                    ClsMessage.showMessage("National id already used" + Environment.NewLine + "Cannot Delete!!!", MessageBoxIcon.Information);
                    return false;
                }
                if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_BeneficiaryWagesDetails", "nationalid", "nationalid", txtNationalId.Text.Trim()) == true)
                {

                    ClsMessage.showMessage("National id already used" + Environment.NewLine + "Cannot Delete!!!", MessageBoxIcon.Information);
                    return false;
                }
                if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_BeneficiryDocuments", "nationalid", "nationalid", txtNationalId.Text.Trim()) == true)
                {

                    ClsMessage.showMessage("National id already used" + Environment.NewLine + "Cannot Delete!!!", MessageBoxIcon.Information);
                    return false;
                }
                if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_MemberDocuments", "nationalid", "nationalid", txtNationalId.Text.Trim()) == true)
                {

                    ClsMessage.showMessage("National id already used" + Environment.NewLine + "Cannot Delete!!!", MessageBoxIcon.Information);
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

        private void rbWife_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbWorking.Checked)
                {
                    txtWorkingStatus.Text = "W";

                }
                if (rbRetired.Checked)
                {
                    txtWorkingStatus.Text = "R";

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        private void rbLiving_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbLiving.Checked)
                {
                    txtLivingStatus.Text = "L";
                }
                if (rbDead.Checked)
                {
                    txtLivingStatus.Text = "D";
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        private void txtNonmineeRelection_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtWorkingStatus.Text.Trim()))
                {
                    if (txtWorkingStatus.Text.Trim() == "W")
                    {
                        rbWorking.Checked = true;
                    }
                    if (txtWorkingStatus.Text.Trim() == "R")
                    {
                        rbRetired.Checked = true;
                    }

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        private void txtLivingStatus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtLivingStatus.Text.Trim()))
                {
                    if (txtLivingStatus.Text.Trim() == "L")
                    {
                        rbLiving.Checked = true;
                    }
                    if (txtLivingStatus.Text.Trim() == "D")
                    {
                        rbDead.Checked = true;
                    }

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        private void tbpDetails_Click(object sender, EventArgs e)
        {

        }

        private void rbSingle_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ClsUtility.FormMode != ClsUtility.enmFormMode.NormalMode)
                {
                    if (rbSingle.Checked)
                    {
                        txtMritalStatus.Text = "S";
                        txtsupouseNationalId.Enabled = false;
                        txtsupouseNationalId.Text = "";
                        txtsupouseName.Enabled = false;
                        txtsupouseName.Text = "";
                        dtpSuposeJoinDate.Text = null;
                        txtSpouseGender.Text = "";

                        rbSpouseMale.Enabled = false;
                        rbSpouseFemale.Enabled = false;
                        dtpSuposeJoinDate.Enabled = false;
                        rbSpouseMale.Checked = false;
                        rbSpouseFemale.Checked = false;
                        /************************/
                        txtNomineeNationalId.Enabled = true;
                        txtNomineeName.Enabled = true;

                        txtNomineeNationalId.Text = "";
                        txtNomineeNationalId.Text = "";

                        //txtWagesAmount.Text = "50";
                        txtWagesAmount.Text = "60";

                    }
                    if (rbMarried.Checked)
                    {
                        txtMritalStatus.Text = "M";

                        txtsupouseNationalId.Enabled = true;
                        txtsupouseNationalId.Text = "";
                        txtsupouseName.Enabled = true;
                        txtsupouseName.Text = "";
                        dtpSuposeJoinDate.Text = null;
                        dtpSuposeJoinDate.Enabled = true;
                        txtSpouseGender.Text = "";

                        rbSpouseMale.Enabled = true;
                        rbSpouseFemale.Enabled = true;
                        rbSpouseMale.Checked = false;
                        rbSpouseFemale.Checked = false;
                        /************************/
                        txtNomineeNationalId.Enabled = true;
                        txtNomineeName.Enabled = true;

                        txtNomineeNationalId.Text = "";
                        txtNomineeNationalId.Text = "";


                        //txtWagesAmount.Text = "63";
                        txtWagesAmount.Text = "73";
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }
        private void txtMritalStatus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMritalStatus.Text.Trim()))
                {
                    if (txtMritalStatus.Text.Trim() == "S")
                    {
                        rbSingle.Checked = true;
                    }
                    if (txtMritalStatus.Text.Trim() == "M")
                    {
                        rbMarried.Checked = true;
                    }

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }
        private void rbSpouseMale_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbSpouseMale.Checked)
                {
                    txtSpouseGender.Text = "M";
                }
                if (rbSpouseFemale.Checked)
                {
                    txtSpouseGender.Text = "F";
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }
        private void txtSpouseGender_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSpouseGender.Text.Trim()))
                {
                    if (txtSpouseGender.Text.Trim() == "M")
                    {
                        rbSpouseMale.Checked = true;
                    }
                    if (txtSpouseGender.Text.Trim() == "F")
                    {
                        rbSpouseFemale.Checked = true;
                    }

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        private void rbMarried_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtsupouseNationalId_Validated(object sender, EventArgs e)
        {
            try
            {
                if (rbMarried.Checked)
                {
                    if (!string.IsNullOrEmpty(txtsupouseNationalId.Text.Trim()))
                    {
                        txtNomineeNationalId.Text = txtsupouseNationalId.Text.Trim();
                    }
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        private void txtsupouseName_Validated(object sender, EventArgs e)
        {
            try
            {
                if (rbMarried.Checked)
                {
                    if (!string.IsNullOrEmpty(txtsupouseName.Text.Trim()))
                    {
                        txtNomineeName.Text = txtsupouseName.Text.Trim();
                    }
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillMemberData();
        }

        private void lblActiveStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtMemberEntery != null && dtMemberEntery.DefaultView.Count > 0)
                {
                    int iRow = bsMemmberEntery.Position;
                    bool lAcive = false;
                    lAcive = dtMemberEntery.DefaultView[iRow]["lActive"] == DBNull.Value ? false : Convert.ToBoolean(dtMemberEntery.DefaultView[iRow]["lActive"]);
                    if(lAcive==true)
                    {
                        lblActiveStatus.ForeColor = Color.DarkGreen;
                    }
                    else
                    {
                        lblActiveStatus.ForeColor = Color.Red;
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
