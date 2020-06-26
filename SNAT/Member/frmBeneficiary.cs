using SNAT.Comman_Classes;
using SNAT.Comman_Form;
using SNAT.CommanClass;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SNAT.Member
{
    public partial class frmBeneficiary : Form
    {
        BindingSource bsBeneficiary = new BindingSource();
        ErrorProvider errorProvider1 = new ErrorProvider();
        string strSqlQuery = "";
        DataTable dtBeneficiary = new DataTable();
        string imageFinalLocation = "";
        DataSet mdsCreateDataView = new DataSet();
        public frmBeneficiary()
        {
            InitializeComponent();
            BindControl();

        }
        void FillMemberData()
        {
            try
            {
                strSqlQuery = "SELECT bf.id , bf.membernationalid , bf.memberid , bf.membername , bf.beneficiarynatioanalid , bf.beneficiaryname ," +
                              " bf.dob , bf.sex , bf.dateofsubmission , bf.relationship , bf.contactno1 , bf.contactno2 , bf.email , bf.residentaladrees ," +
                              " bf.nomineenationalid , bf.nomineename , bf.wages , bf.effactivedate , bf.imagelocation,bf.lstatus,createdby,createddate,updateby,updateddate" +
                              " FROM SNAT.dbo.T_Beneficiary bf (nolock) ORDER BY bf.id,bf.membernationalid,bf.beneficiarynatioanalid ";

                dtBeneficiary = ClsDataLayer.GetDataTable(strSqlQuery);
                bsBeneficiary.DataSource = dtBeneficiary.DefaultView;
                bindingNavigatorMain.BindingSource = bsBeneficiary;
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

                txtmembernationalid.DataBindings.Add("Text", bsBeneficiary, "membernationalid", false, DataSourceUpdateMode.OnValidation);
                txtMemberid.DataBindings.Add("Text", bsBeneficiary, "memberid", false, DataSourceUpdateMode.OnValidation);
                // txtMemberid.DataBindings.Add("Text", bsBeneficiary, "memberid", false, DataSourceUpdateMode.OnValidation);
                txtMemberName.DataBindings.Add("Text", bsBeneficiary, "membername", false, DataSourceUpdateMode.OnValidation);

                txtBeneficiaryNationalId.DataBindings.Add("Text", bsBeneficiary, "beneficiarynatioanalid", false, DataSourceUpdateMode.OnValidation);
                txtBeneficiaryName.DataBindings.Add("Text", bsBeneficiary, "beneficiaryname", false, DataSourceUpdateMode.OnValidation);
                dtpDOB.DataBindings.Add("Text", bsBeneficiary, "dob", false, DataSourceUpdateMode.OnValidation);
                txtSex.DataBindings.Add("Text", bsBeneficiary, "sex", false, DataSourceUpdateMode.OnValidation);
                dtpDateOfSubmisson.DataBindings.Add("Text", bsBeneficiary, "dateofsubmission", false, DataSourceUpdateMode.OnValidation);
                txtrelationship.DataBindings.Add("Text", bsBeneficiary, "relationship", false, DataSourceUpdateMode.OnValidation);
                txtContactNo1.DataBindings.Add("Text", bsBeneficiary, "contactno1", false, DataSourceUpdateMode.OnValidation);
                txtContactNo2.DataBindings.Add("Text", bsBeneficiary, "contactno2", false, DataSourceUpdateMode.OnValidation);
                txtResidentalAddress.DataBindings.Add("Text", bsBeneficiary, "residentaladrees", false, DataSourceUpdateMode.OnValidation);
                txtemailid.DataBindings.Add("Text", bsBeneficiary, "email", false, DataSourceUpdateMode.OnValidation);

                txtNomineeNationalId.DataBindings.Add("Text", bsBeneficiary, "nomineenationalid", false, DataSourceUpdateMode.OnValidation);
                txtNomineeName.DataBindings.Add("Text", bsBeneficiary, "nomineename", false, DataSourceUpdateMode.OnValidation);
                txtWagesAmount.DataBindings.Add("Text", bsBeneficiary, "wages", false, DataSourceUpdateMode.OnValidation);
                dtpWagesEffectiveDate.DataBindings.Add("Text", bsBeneficiary, "effactivedate", false, DataSourceUpdateMode.OnValidation);

                txtImageLocation.DataBindings.Add("Text", bsBeneficiary, "imagelocation", false, DataSourceUpdateMode.OnValidation);
                txtStatus.DataBindings.Add("Text", bsBeneficiary, "lstatus", false, DataSourceUpdateMode.OnValidation);

                grdList.DataSource = bsBeneficiary;

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);

            }
        }
        private void SetEnable(bool lValue)
        {
            //txtNationalId.Enabled = lValue;
            //txtMemberID.Enabled = lValue;
            //txtEmployeeNo.Enabled = lValue;
            //txtTSCNo.Enabled = lValue;
            // txtMemberName.Enabled = lValue;
            dtpDOB.Enabled = lValue;
            rbMale.Enabled = lValue;
            rbFemale.Enabled = lValue;
            txtmembernationalid.Enabled = lValue;
            btnPickMemberNationid.Enabled = lValue;
            txtContactNo1.Enabled = lValue;
            txtContactNo2.Enabled = lValue;
            txtResidentalAddress.Enabled = lValue;
            // txtNomineeName.Enabled = lValue;
            //txtNomineeNationalId.Enabled = lValue;
            //txtWagesAmount.Enabled = lValue;
            dtpWagesEffectiveDate.Enabled = lValue;
            btnselectpicture.Enabled = lValue;
            txtemailid.Enabled = lValue;
            txtBeneficiaryNationalId.Enabled = lValue;
            txtBeneficiaryName.Enabled = lValue;
            rbWife.Enabled = lValue;
            rbFriend.Enabled = lValue;
            rbChiledren.Enabled = lValue;
            rbReletive.Enabled = lValue;
            dtpDateOfSubmisson.Enabled = lValue;
            chkStatus.Enabled = lValue;
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
                    //txtNationalId.Enabled = false;
                    //txtMemberID.Enabled = false;
                    //txtEmployeeNo.Enabled = false;
                    break;
                case ClsUtility.enmFormMode.NormalMode:
                    SetEnable(false);
                    break;
            }
        }
        private void frmBeneficiary_Load(object sender, EventArgs e)
        {
            try
            {
                ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                SetFormMode(ClsUtility.enmFormMode.NormalMode);
                //this.helpProvider1.SetShowHelp(this.txtBeneficiaryNationalId, true);
                //this.helpProvider1.SetHelpString(this.txtBeneficiaryNationalId, "Enter the street address in this text box.");

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);

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

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }

        private void rbWife_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbWife.Checked == true)
                {
                    txtrelationship.Text = "W";
                }
                if (rbChiledren.Checked == true)
                {
                    txtrelationship.Text = "C";
                }
                if (rbFriend.Checked == true)
                {
                    txtrelationship.Text = "F";
                }
                if (rbReletive.Checked == true)
                {
                    txtrelationship.Text = "R";
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtrelationship_TextChanged(object sender, EventArgs e)
        {


            try
            {
                if (!string.IsNullOrEmpty(txtrelationship.Text.Trim()))
                {
                    switch (txtrelationship.Text.Trim().ToUpper())
                    {
                        case "W":
                            rbWife.Checked = true;
                            break;
                        case "C":
                            rbChiledren.Checked = true;
                            break;
                        case "F":
                            rbFriend.Checked = true;
                            break;
                        case "R":
                            rbReletive.Checked = true;
                            break;

                    }

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);
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
                                ClsMessage.showMessage("Employee image max size(Height & Width) is 120 X 120");

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
                bsBeneficiary.AllowNew = true;
                bsBeneficiary.AddNew();
                txtSex.Text = "M";
                txtWagesAmount.Text = "10";
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
                txtmembernationalid.Enabled = false;
                txtBeneficiaryNationalId.Enabled = false;
                // txtBeneficiaryName.Enabled = false;
                txtWagesAmount.Enabled = false;
                dtpWagesEffectiveDate.Enabled = false;
                txtBeneficiaryName.Focus();
                btnPickMemberNationid.Enabled = false;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private bool ValidateDetete()
        {
            try
            {
                if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_BeneficiryDocuments", "beneficirynationalid", "beneficirynationalid", txtBeneficiaryNationalId.Text.Trim()) == true)
                {

                    ClsMessage.showMessage("Beneficiry National id already used" + Environment.NewLine + "Cannot Delete!!!");
                    return false;
                }
                //if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_BeneficiaryWagesDetails", "nationalid", "nationalid", txtNationalId.Text.Trim()) == true)
                //{

                //    ClsMessage.showMessage("National id already used" + Environment.NewLine + "Cannot Delete!!!");
                //    return false;
                //}
                //if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_BeneficiryDocuments", "nationalid", "nationalid", txtNationalId.Text.Trim()) == true)
                //{

                //    ClsMessage.showMessage("National id already used" + Environment.NewLine + "Cannot Delete!!!");
                //    return false;
                //}
                //if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_MemberDocuments", "nationalid", "nationalid", txtNationalId.Text.Trim()) == true)
                //{

                //    ClsMessage.showMessage("National id already used" + Environment.NewLine + "Cannot Delete!!!");
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateDetete() == false) { return; }
                if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
                {
                    string strRelection = "";
                    if (rbWife.Checked == true)
                    {

                        strRelection = "W";
                    }
                    else
                    {
                        strRelection = "O";
                    }
                    string memNationalID = txtmembernationalid.Text.Trim();
                    string memMmberID = txtMemberid.Text.Trim();
                    Int32 iRow = bsBeneficiary.Position;
                    bsBeneficiary.RemoveAt(iRow);
                    // dtDepartment.DefaultView.Delete(iRow);
                    bsBeneficiary.EndEdit();
                    if (dtBeneficiary != null && dtBeneficiary.DefaultView.Count > 0)
                    {
                        //dtBeneficiary.DefaultView[iRow].BeginEdit();

                        //dtBeneficiary.DefaultView[iRow].EndEdit();
                        if (dtBeneficiary.GetChanges() != null)
                        {

                            bool lReturn = false;
                            strSqlQuery = "SELECT bf.id , bf.membernationalid , bf.memberid , bf.membername , bf.beneficiarynatioanalid , bf.beneficiaryname ," +
                             " bf.dob , bf.sex , bf.dateofsubmission , bf.relationship , bf.contactno1 , bf.contactno2 , bf.email , bf.residentaladrees ," +
                             " bf.nomineenationalid , bf.nomineename , bf.wages , bf.effactivedate , bf.imagelocation,bf.lstatus,createdby,createddate,updateby,updateddate" +
                             " FROM SNAT.dbo.T_Beneficiary bf (nolock) where 1=2 ";
                            lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtBeneficiary);

                            if (lReturn == true)
                            {


                                decimal diMemWages = diGetMemberWages(memNationalID, strRelection, "DELETE");

                                strSqlQuery = "UPDATE SNAT.dbo.T_Member SET wagesamount = " + diMemWages + " WHERE nationalid = '" + memNationalID + "' and dbo.T_Member.memberid  = '" + memMmberID + "'";
                                ClsDataLayer.UpdateData(strSqlQuery);
                                strSqlQuery = "UPDATE SNAT.dbo.T_MemberWagesDetails SET lstatus = 0,updatedby='" + ClsSettings.username + "',updateddate=getdate()   where nationalid  = '" + memNationalID + "' and memberid  = '" + memMmberID + "' and lstatus = 1 ";
                                ClsDataLayer.UpdateData(strSqlQuery);
                                strSqlQuery = "INSERT SNAT.dbo.T_MemberWagesDetails (nationalid,memberid,employeeno, wagesamount,effactivedate," + Environment.NewLine +
                                               "lstatus,createdby,createddate,updatedby,updateddate)" + Environment.NewLine +
                                               " Values('" + memNationalID.Trim() + "','" + memMmberID.Trim() + "','0'," + diMemWages + ",Getdate(),'1','" + ClsSettings.username + "',getDate(),'" + ClsSettings.username + "',getDate()) ";
                                ClsDataLayer.UpdateData(strSqlQuery);

                                ClsMessage.showDeleteMessage();
                                ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                                SetFormMode(ClsUtility.enmFormMode.NormalMode);
                                dtBeneficiary.AcceptChanges();
                                FillMemberData();
                            }
                            else
                            {
                                ClsMessage.showMessage("Some problem occurs while deleting please contact system administrator.");
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

                if (string.IsNullOrEmpty(txtmembernationalid.Text.Trim()))
                {
                    errorProvider1.SetError(txtmembernationalid, "Member National id cannot be left blank.");
                    return false;
                }
                //if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                //{
                //    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Beneficiary", "nationalid", "nationalid", txtNationalId.Text.Trim()) == true)
                //    {
                //        errorProvider1.SetError(txtNationalId, "National id already exists.");
                //        ClsMessage.showMessage("National id already exists.");
                //        return false;
                //    }
                //}
                if (string.IsNullOrEmpty(txtBeneficiaryNationalId.Text.Trim()))
                {
                    errorProvider1.SetError(txtBeneficiaryNationalId, "Beneficiary National id cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Beneficiary", "beneficiarynatioanalid", "beneficiarynatioanalid", txtBeneficiaryNationalId.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtBeneficiaryNationalId, "Beneficiary National id already exists.");
                        ClsMessage.showMessage("Beneficiary National id already exists.");
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(txtBeneficiaryName.Text.Trim()))
                {
                    errorProvider1.SetError(txtBeneficiaryName, "Beneficiary name cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Beneficiary", "beneficiaryname", "beneficiaryname", txtBeneficiaryName.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtBeneficiaryName, "Beneficiary name already exists.");
                        ClsMessage.showMessage("Beneficiary name already exists.");
                        return false;
                    }
                }
                //if (string.IsNullOrEmpty(dtpDOB.Text.Trim()))
                //{
                //    errorProvider1.SetError(dtpDOB, "Date of birth  cannot be left blank.");
                //    return false;
                //}
                //if (string.IsNullOrEmpty(txtSex.Text.Trim()))
                //{
                //    errorProvider1.SetError(pnlSex, "Member sex cannot be left blank.");
                //    return false;
                //}
                if (string.IsNullOrEmpty(dtpDateOfSubmisson.Text.Trim()))
                {
                    errorProvider1.SetError(dtpDateOfSubmisson, "Date of submission cannot be left blank.");
                    return false;
                }
                //if (string.IsNullOrEmpty(txtrelationship.Text.Trim()))
                //{
                //    errorProvider1.SetError(pnlReletion, "Member - Beneficiary  cannot be left blank.");
                //    return false;
                //}
                //if (string.IsNullOrEmpty(txtContactNo1.Text.Trim()))
                //{
                //    errorProvider1.SetError(txtContactNo1, "Contact no. cannot be left blank.");
                //    return false;
                //}
                //if (string.IsNullOrEmpty(txtemailid.Text.Trim()))
                //{
                //    errorProvider1.SetError(txtemailid, "Email id cannot be left blank.");
                //    return false;
                //}
                //if (string.IsNullOrEmpty(txtemailid.Text.Trim()) == false && ClsUtility.IsValidEmail(txtemailid.Text.Trim()) == false)
                //{
                //    //ClsMessage.showMessage("Invalid email id!!");
                //    errorProvider1.SetError(txtemailid, "Invalid email id!!");
                //    txtemailid.Focus();
                //    return false;
                //}
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
                string strRelection = "";

                if (ValidateSave() == false) { return; }
                bsBeneficiary.EndEdit();
                if (dtBeneficiary != null && dtBeneficiary.DefaultView.Count > 0)
                {
                    Int32 iRow = bsBeneficiary.Position;
                    dtBeneficiary.DefaultView[iRow].BeginEdit();
                    if (rbMale.Checked == true)
                    {
                        dtBeneficiary.DefaultView[iRow]["Sex"] = "M";
                    }
                    if (rbFemale.Checked == true)
                    {
                        dtBeneficiary.DefaultView[iRow]["Sex"] = "F";
                    }
                    if (rbWife.Checked == true)
                    {
                        dtBeneficiary.DefaultView[iRow]["relationship"] = "W";
                        strRelection = "W";
                    }
                    if (rbChiledren.Checked == true)
                    {
                        dtBeneficiary.DefaultView[iRow]["relationship"] = "C";
                        strRelection = "C";
                    }
                    if (rbFriend.Checked == true)
                    {
                        dtBeneficiary.DefaultView[iRow]["relationship"] = "F";
                        strRelection = "F";
                    }
                    if (rbReletive.Checked == true)
                    {
                        dtBeneficiary.DefaultView[iRow]["relationship"] = "R";
                        strRelection = "R";
                    }
                    if (string.IsNullOrEmpty(dtpDOB.Value.ToShortDateString().Trim()) == false)
                    {
                        if (dtpDOB.Value.ToShortDateString().Trim() == "01/01/0001")
                        {
                            dtBeneficiary.DefaultView[iRow]["dob"] = DateTime.Now.ToShortDateString();
                          
                        }
                        else
                        {
                            dtBeneficiary.DefaultView[iRow]["dob"] = dtpDOB.Value.ToShortDateString().Trim();
                        }

                    }
                    if (string.IsNullOrEmpty(dtpDateOfSubmisson.Value.ToShortDateString().Trim()) == false)
                    {
                        if (dtpDateOfSubmisson.Value.ToShortDateString().Trim() == "01/01/0001")
                        {
                            dtBeneficiary.DefaultView[iRow]["dateofsubmission"] = DateTime.Now.ToShortDateString();
                        }
                        else
                        {
                            dtBeneficiary.DefaultView[iRow]["dateofsubmission"] = dtpDateOfSubmisson.Value.ToShortDateString().Trim();
                        }
                    }
                    if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                    {
                        if (string.IsNullOrEmpty(dtpWagesEffectiveDate.Value.ToShortDateString().Trim()) == false)
                        {
                            if (dtpWagesEffectiveDate.Value.ToShortDateString().Trim() == "01/01/0001")
                            {
                                dtBeneficiary.DefaultView[iRow]["effactivedate"] = DateTime.Now.ToShortDateString();
                            }
                            else
                            {
                                dtBeneficiary.DefaultView[iRow]["effactivedate"] = dtpWagesEffectiveDate.Value.ToShortDateString().Trim();
                            }
                        }
                    }


                    if (string.IsNullOrEmpty(txtImageLocation.Text.Trim()) == false)
                    {
                        if (File.Exists(txtImageLocation.Text.Trim()))
                        {
                            string imagenewlocation = Path.GetFullPath(txtImageLocation.Text.Trim());
                            string imageext = Path.GetExtension(txtImageLocation.Text.Trim());

                            dtBeneficiary.DefaultView[iRow]["imagelocation"] = ClsSettings.strEmployeeImageFolder + @"\" + txtBeneficiaryNationalId.Text.Trim() + imageext;
                            imageFinalLocation = ClsSettings.strEmployeeImageFolder + @"\" + txtBeneficiaryNationalId.Text.Trim() + imageext;
                            CopyEmployeeImage(imageFinalLocation);
                        }

                    }
                    if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                    {

                        dtBeneficiary.DefaultView[iRow]["createdby"] = ClsSettings.username;
                        dtBeneficiary.DefaultView[iRow]["createddate"] = DateTime.Now.ToString();
                    }
                    dtBeneficiary.DefaultView[iRow]["updateby"] = ClsSettings.username;
                    dtBeneficiary.DefaultView[iRow]["updateddate"] = DateTime.Now.ToString();
                    dtBeneficiary.DefaultView[iRow]["lstatus"] = chkStatus.Checked;
                    dtBeneficiary.DefaultView[iRow].EndEdit();
                    if (dtBeneficiary.GetChanges() != null)
                    {

                        bool lReturn = false;
                        strSqlQuery = "SELECT bf.id , bf.membernationalid , bf.memberid , bf.membername , bf.beneficiarynatioanalid , bf.beneficiaryname ," +
                                      " bf.dob , bf.sex , bf.dateofsubmission , bf.relationship , bf.contactno1 , bf.contactno2 , bf.email , bf.residentaladrees ," +
                                      " bf.nomineenationalid , bf.nomineename , bf.wages , bf.effactivedate , bf.imagelocation,bf.lstatus,createdby,createddate,updateby,updateddate" +
                                      " FROM SNAT.dbo.T_Beneficiary bf (nolock) where 1=2 ";

                        iRow = bsBeneficiary.Position;
                        dtBeneficiary.DefaultView[iRow].BeginEdit();
                        dtBeneficiary.DefaultView[iRow].EndEdit();
                        lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtBeneficiary);
                        dtBeneficiary.AcceptChanges();

                        decimal diMemWages = diGetMemberWages(txtmembernationalid.Text.Trim(), strRelection, "SAVE");

                        strSqlQuery = "UPDATE SNAT.dbo.T_Member SET wagesamount = " + diMemWages + " WHERE nationalid = '" + txtmembernationalid.Text.Trim() + "' and dbo.T_Member.memberid  = '" + txtMemberid.Text.Trim() + "'";
                        ClsDataLayer.UpdateData(strSqlQuery);
                        strSqlQuery = "UPDATE SNAT.dbo.T_MemberWagesDetails SET lstatus = 0,updatedby='" + ClsSettings.username + "',updateddate=getdate()   where nationalid  = '" + txtmembernationalid.Text.Trim() + "' and memberid  = '" + txtMemberid.Text.Trim() + "' and lstatus = 1 ";
                        ClsDataLayer.UpdateData(strSqlQuery);
                        strSqlQuery = "INSERT SNAT.dbo.T_MemberWagesDetails (nationalid,memberid,employeeno, wagesamount,effactivedate," + Environment.NewLine +
                                       "lstatus,createdby,createddate,updatedby,updateddate)" + Environment.NewLine +
                                       " Values('" + txtmembernationalid.Text.Trim() + "','" + txtMemberid.Text.Trim() + "','0'," + diMemWages + ",Getdate(),'1','" + ClsSettings.username + "',getDate(),'" + ClsSettings.username + "',getDate()) ";
                        ClsDataLayer.UpdateData(strSqlQuery);
                        if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                        {
                            strSqlQuery = "SELECT id, nationalid, memberid,  beneficirynationalid, wagesamount, effactivedate, lstatus, createdby, createddate, updatedby, updateddate FROM SNAT.dbo.T_BeneficiaryWagesDetails (nolock) WHERE 1=2";
                            DataTable dtWages = new DataTable();
                            dtWages = ClsDataLayer.GetDataTable(strSqlQuery);
                            DataRow dRow = dtWages.NewRow();
                            dRow["nationalid"] = txtmembernationalid.Text.Trim();
                            dRow["memberid"] = txtMemberid.Text.Trim();

                            dRow["beneficirynationalid"] = txtBeneficiaryNationalId.Text.Trim();
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


                        if (lReturn == true)
                        {
                            ClsMessage.showSaveMessage();
                            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                            SetFormMode(ClsUtility.enmFormMode.NormalMode);
                            FillMemberData();
                            clsEmail.lBeneficiryCreated(txtmembernationalid.Text.Trim(), txtMemberid.Text.Trim(), txtMemberName.Text.Trim(), txtBeneficiaryNationalId.Text.Trim(), txtBeneficiaryName.Text.Trim());
                        }
                        else
                        {
                            ClsMessage.showMessage("Some problem occurs while saving please contact system administrator.");
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
                bsBeneficiary.CancelEdit();
                dtBeneficiary.RejectChanges();
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
                frmsrch.infSqlSelectQuery = "SELECT membernationalid , memberid , membername , beneficiarynatioanalid , beneficiaryname , lstatus FROM SNAT.dbo.T_Beneficiary";
                frmsrch.infSqlWhereCondtion = "";
                frmsrch.infSqlOrderBy = " membernationalid , beneficiarynatioanalid ";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Member ....";
                frmsrch.infCodeFieldName = "beneficiarynatioanalid";
                frmsrch.infDescriptionFieldName = "beneficiaryname";
                frmsrch.infGridFieldName = " id , membernationalid , memberid , membername , beneficiarynatioanalid , beneficiaryname , lstatus";
                frmsrch.infGridFieldCaption = " id , Member National ID , Member ID , Member Name , Beneficiary Natioanal ID , Beneficiary Name , Status";
                frmsrch.infGridFieldSize = "0,100,100,200,100,200,80";
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
                        int iRow = bsBeneficiary.Find("beneficiarynatioanalid", frmsrch.infCodeFieldText.Trim());
                        bsBeneficiary.Position = iRow;
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
                iRow = bsBeneficiary.Position;
                dtReport = dtBeneficiary.Clone();

                dtReport.ImportRow(dtBeneficiary.DefaultView[iRow].Row);
                if (dtReport != null && dtReport.DefaultView.Count > 0)
                {
                    ClsUtility.PrintReport("rptBeneficiary.rpt", dtReport, "rptBeneficiary");//
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

        private void btnPickMemberNationid_Click(object sender, EventArgs e)
        {

            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT id , nationalid , memberid , employeeno , tscno , membername,email,contactno1,residentaladdress FROM SNAT.dbo.T_Member AS ted";
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
                    txtmembernationalid.Text = frmsrch.infCodeFieldText.Trim();
                    txtMemberName.Text = string.IsNullOrEmpty(dvDesg[0]["membername"].ToString()) == true ? "" : dvDesg[0]["membername"].ToString();
                    txtMemberid.Text = string.IsNullOrEmpty(dvDesg[0]["memberid"].ToString()) == true ? "" : dvDesg[0]["memberid"].ToString();
                    txtMemberid.Tag = string.IsNullOrEmpty(dvDesg[0]["employeeno"].ToString()) == true ? "" : dvDesg[0]["employeeno"].ToString();
                    txtemailid.Text = string.IsNullOrEmpty(dvDesg[0]["email"].ToString()) == true ? "" : dvDesg[0]["email"].ToString();
                    txtContactNo1.Text = string.IsNullOrEmpty(dvDesg[0]["contactno1"].ToString()) == true ? "" : dvDesg[0]["contactno1"].ToString();
                    txtResidentalAddress.Text = string.IsNullOrEmpty(dvDesg[0]["residentaladdress"].ToString()) == true ? "" : dvDesg[0]["residentaladdress"].ToString();

                    //if (string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) == false)
                    //{
                    //    int iRow = bsMemmberEntery.Find("nationalid", frmsrch.infCodeFieldText.Trim());
                    //    bsMemmberEntery.Position = iRow;
                    //    dvDesg.RowFilter = "";
                    //}
                    txtNomineeName.Text = txtMemberName.Text;
                    txtNomineeNationalId.Text = txtmembernationalid.Text;
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtmembernationalid_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtmembernationalid.Text.Trim()))
                {
                    string strSql = "SELECT id , nationalid , memberid , employeeno , tscno , membername FROM SNAT.dbo.T_Member AS ted where nationalid='" + txtmembernationalid.Text.Trim() + "'";
                    DataTable dtMemberid = new DataTable();
                    dtMemberid = ClsDataLayer.GetDataTable(strSql);
                    if (dtMemberid != null && dtMemberid.DefaultView.Count > 0)
                    {
                        txtMemberid.Text = string.IsNullOrEmpty(dtMemberid.DefaultView[0]["memberid"].ToString()) == true ? "" : dtMemberid.DefaultView[0]["memberid"].ToString();
                        txtMemberName.Text = string.IsNullOrEmpty(dtMemberid.DefaultView[0]["membername"].ToString()) == true ? "" : dtMemberid.DefaultView[0]["membername"].ToString();
                        txtNomineeName.Text = txtMemberName.Text;
                        txtNomineeNationalId.Text = txtmembernationalid.Text;
                    }
                    else
                    {
                        ClsMessage.showMessage("Invalid member national id");
                        txtmembernationalid.Focus();
                        txtMemberid.Text = "";
                        txtMemberName.Text = "";
                        txtNomineeName.Text = txtMemberName.Text;
                        txtNomineeNationalId.Text = txtmembernationalid.Text;

                    }
                }
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

        private decimal diGetMemberWages(string strmembernationalid, string strRelection, string strType)
        {
            try
            {


                string strQuery = " SELECT tb.id, tb.membernationalid, tb.memberid, tb.membername, tb.beneficiarynatioanalid, tb.beneficiaryname, tb.sex, tb.relationship," + Environment.NewLine +
                                  " tb.nomineenationalid, tb.nomineename,IsNUll(tb.wages,0) BenefWages, tb.effactivedate, tb.lstatus, tb.dateofDate,IsNUll(tm.wagesamount,0) MemWages " + Environment.NewLine +
                                  " FROM SNAT.dbo.T_Beneficiary AS tb (nolock) " + Environment.NewLine +
                                  " LEFT OUTER JOIN SNAT.dbo.T_Member tm (nolock) ON tm.nationalid=tb.membernationalid AND tm.memberid = tb.memberid" + Environment.NewLine +
                                  " WHERE ISNULL(tb.lstatus, 0) = 1 and membernationalid='" + strmembernationalid + "'; ";
                DataTable dtWagesCount = new DataTable();
                dtWagesCount = ClsDataLayer.GetDataTable(strQuery);
                DataView dvWife = new DataView();
                DataView dvOthers = new DataView();
                DataView dvMemberWages = new DataView();
                if (dtWagesCount != null && dtWagesCount.DefaultView.Count > 0)
                {

                    dvWife = mdsCreateDataView.DefaultViewManager.CreateDataView(dtWagesCount);
                    dvOthers = mdsCreateDataView.DefaultViewManager.CreateDataView(dtWagesCount);
                    dvMemberWages = mdsCreateDataView.DefaultViewManager.CreateDataView(dtWagesCount);
                    dvMemberWages = dvMemberWages.ToTable(true, "MemWages").DefaultView;

                    dvWife.RowFilter = "Isnull(relationship,'')='W'";
                    dvOthers.RowFilter = "Isnull(relationship,'')<>'W'";
                    dvMemberWages.RowFilter = "Isnull(MemWages,0)<>0";

                    int iWifeCount = dvWife.Count;
                    int iOtherCount = dvOthers.Count;
                    int iMemberWages = Convert.ToInt32(dvMemberWages[0][0]);

                    if (strRelection.ToUpper() == "W")
                    {
                        if (iMemberWages > 60)
                        {
                            if (strType.ToUpper() == "DELETE")
                            {
                                return iMemberWages - 23;
                            }
                            else
                            {
                                return iMemberWages + 23;
                            }

                        }
                        if (iMemberWages == 60)
                        {
                            if (iOtherCount < 5 && iWifeCount <= 1)
                            {

                                return 50;
                            }
                            if (iOtherCount >= 5 && iWifeCount <= 1)
                            {
                                if (strType.ToUpper() == "DELETE")
                                {
                                    return iMemberWages - 23;
                                }
                                else
                                {
                                    return iMemberWages + 23;
                                }

                            }
                        }
                    }
                    else
                    {
                        if (iMemberWages > 60)
                        {
                            if (strType.ToUpper() == "DELETE")
                            {
                                return iMemberWages - 10;
                            }
                            else
                            {
                                return iMemberWages + 10;
                            }

                        }
                        if (iMemberWages == 60)
                        {
                            if (iOtherCount <= 4)
                            {
                                if (iWifeCount == 1)
                                {
                                    return 73;
                                }

                                else if (iWifeCount == 0)
                                {
                                    return 60;
                                }

                            }
                            if (iOtherCount >= 5)
                            {
                                if (strType.ToUpper() == "DELETE")
                                {
                                    return iMemberWages - 10;
                                }
                                else
                                {
                                    return iMemberWages + 10;
                                }

                            }
                            //if (iOtherCount >= 5)
                            //{
                            //    return iMemberWages + 10;
                            //}
                        }
                    }


                }
                return 0;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return 0;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillMemberData();
        }
    }
}
