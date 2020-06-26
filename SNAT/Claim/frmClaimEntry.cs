using SNAT.Comman_Classes;
using SNAT.Comman_Form;
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using SNAT.CommanClass;

namespace SNAT.Document
{
    public partial class frmClaimEntry : Form
    {
        DataTable dtClaimEntery = new DataTable();
        BindingSource bsClaimEntery = new BindingSource();
        string strSqlQuery = "";
        ErrorProvider errorProvider1 = new ErrorProvider();
        DataTable dtClaimDocument = new DataTable();
        DataSet mdsCreateDataView = new DataSet();
        public frmClaimEntry()
        {
            InitializeComponent();
            BindControl();

        }
        void FillMemberData()
        {
            try
            {
                strSqlQuery = "SELECT ce.id, ce.LetPerson, ce.MemNationalID, ce.MemberID, ce.MemName, ce.BenfNationalID, ce.BenfName, ce.PlaceOfBurial," + Environment.NewLine +
                              " ce.Mortuary, ce.Entery, ce.DateOfBurial,ce.NomineeName, ce.TotalAmount, ce.ReciviedBy, ce.RecivingRemarks, ce.EnteryUser," + Environment.NewLine +
                              " ce.EnteryDate, ce.EnteryRemarks, ce.Chariperson_Name, ce.Chariperson_Date,ce.Chariperson_Remarks, ce.Chariperson_Status," + Environment.NewLine +
                              " ce.Secteatary_Name, ce.Secteatary_Date, ce.Secteatary_Remarks, ce.Secteatary_Status, ce.Treasurer_Name,ce.Treasurer_Date," + Environment.NewLine +
                              " ce.Treasurer_Remarks, ce.Treasurer_Status,ce.ClaimStatus, ce.CreatedBy, ce.CreateDate, ce.UpdateBy, ce.UpdateDate" + Environment.NewLine +
                              " ,ce.cPostStatus,ms.name schoolName,tm.contactno1 memContactNo,tm.residentaladdress MemResidentalAddress,tb.contactno1 BenContactNo,tb.residentaladrees BenResidentalAddress" + Environment.NewLine +
                              " ,CASE WHEN ISNULL(ce.ClaimStatus,'')='E' THEN 'Claim Entered'" + Environment.NewLine +
                              " WHEN ISNULL(ce.ClaimStatus, '')= 'P' THEN 'Claim posted for approval'" + Environment.NewLine +
                              " WHEN ISNULL(ce.ClaimStatus, '')= 'CA' THEN 'Claim approved by chairperson'" + Environment.NewLine +
                              " WHEN ISNULL(ce.ClaimStatus, '')= 'CR' THEN 'Claim rejected by chairperson'" + Environment.NewLine +
                              " WHEN ISNULL(ce.ClaimStatus, '')= 'SA' THEN 'Claim approved by secretary'" + Environment.NewLine +
                              " WHEN ISNULL(ce.ClaimStatus, '')= 'SR' THEN 'Claim rejected by secretary'" + Environment.NewLine +
                              " WHEN ISNULL(ce.ClaimStatus, '')= 'TA' THEN 'Claim approved by Treasurer'" + Environment.NewLine +
                              " WHEN ISNULL(ce.ClaimStatus, '')= 'TR' THEN 'Claim rejected by Treasurer'" + Environment.NewLine +
                              " ELSE 'No Status' END ClaimDesc,ce.ClaimStatusNo" + Environment.NewLine +
                              " FROM SNAT.dbo.T_ClaimEntery ce" + Environment.NewLine +
                              " LEFT OUTER JOIN SNAT.dbo.T_Member tm ON ce.MemNationalID = tm.nationalid AND ce.MemberID = tm.memberid" + Environment.NewLine +
                              " LEFT OUTER JOIN SNAT.dbo.T_Beneficiary tb ON ce.MemNationalID = tb.membernationalid AND ce.MemberID = tb.memberid AND ce.BenfNationalID=tb.beneficiarynatioanalid" + Environment.NewLine +
                              " LEFT OUTER JOIN SNAT.dbo.M_School ms ON ms.code=tm.school";
                dtClaimEntery = ClsDataLayer.GetDataTable(strSqlQuery);
                bsClaimEntery.DataSource = dtClaimEntery.DefaultView;
                bindingNavigatorMain.BindingSource = bsClaimEntery;



            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        void FillDocumentData(string strClaimID)
        {
            try
            {
                dtClaimDocument = new DataTable();
                strSqlQuery = "SELECT cd.id, cd.claimid, cd.nationalid, cd.memberid, cd.membername, cd.beneficirynationalid, cd.beneficiaryname, cd.doccode,mcdt.name docName," + Environment.NewLine +
                              " cd.docLocation, cd.docUploaded, cd.createdby, cd.createddate, cd.updatedby, cd.updateddate,cd.IsMandatory,docUploaded dcIsDocAttached,'Preview' Preview , cast('Attach Doc' as varchar(20)) AttachDoc " + Environment.NewLine +
                              " FROM SNAT.dbo.T_ClaimDocuments  cd LEFT OUTER JOIN SNAT.dbo.M_ClaimDocType mcdt (nolock) ON mcdt.code=cd.doccode Where claimid='" + strClaimID + "' ";
                dtClaimDocument = ClsDataLayer.GetDataTable(strSqlQuery);

                //DataColumn dcIsDocAttached = new DataColumn("dcIsDocAttached", typeof(System.Boolean));
                //dcIsDocAttached.DefaultValue = false;
                //dcIsDocAttached.ReadOnly = false;
                //dtClaimDocument.Columns.Add(dcIsDocAttached);

                grdDocList.DataSource = dtClaimDocument.DefaultView;
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
                txtLetPerson.DataBindings.Add(new Binding("Text", bsClaimEntery, "LetPerson", false, DataSourceUpdateMode.OnValidation));
                txtMemNationalId.DataBindings.Add(new Binding("Text", bsClaimEntery, "MemNationalID", false, DataSourceUpdateMode.OnValidation));
                txtMemberID.DataBindings.Add("Text", bsClaimEntery, "MemberID", false, DataSourceUpdateMode.OnValidation);
                txtMemberName.DataBindings.Add("Text", bsClaimEntery, "MemName", false, DataSourceUpdateMode.OnValidation);
                txtMemContactNo.DataBindings.Add("Text", bsClaimEntery, "memContactNo", false, DataSourceUpdateMode.OnValidation);
                txtMemSchool.DataBindings.Add("Text", bsClaimEntery, "schoolName", false, DataSourceUpdateMode.OnValidation);
                txtMemResidence.DataBindings.Add("Text", bsClaimEntery, "MemResidentalAddress", false, DataSourceUpdateMode.OnPropertyChanged);
                txtBenNationalId.DataBindings.Add("Text", bsClaimEntery, "BenfNationalID", false, DataSourceUpdateMode.OnPropertyChanged);
                txtBenName.DataBindings.Add("Text", bsClaimEntery, "BenfName", false, DataSourceUpdateMode.OnValidation);
                txtBenContactNo.DataBindings.Add("Text", bsClaimEntery, "BenContactNo", false, DataSourceUpdateMode.OnValidation);
                txtBenResidence.DataBindings.Add("Text", bsClaimEntery, "BenResidentalAddress", false, DataSourceUpdateMode.OnValidation);
                txtPlaceOfBurial.DataBindings.Add("Text", bsClaimEntery, "PlaceOfBurial", false, DataSourceUpdateMode.OnValidation);
                txtMortuary.DataBindings.Add("Text", bsClaimEntery, "Mortuary", false, DataSourceUpdateMode.OnValidation);
                txtEntry.DataBindings.Add("Text", bsClaimEntery, "Entery", false, DataSourceUpdateMode.OnValidation);
                dtpDateOfBurial.DataBindings.Add("Text", bsClaimEntery, "DateOfBurial", false, DataSourceUpdateMode.OnPropertyChanged);

                txtNomineeName.DataBindings.Add("Text", bsClaimEntery, "NomineeName", false, DataSourceUpdateMode.OnPropertyChanged);
                txtTotalAmount.DataBindings.Add("Text", bsClaimEntery, "TotalAmount", false, DataSourceUpdateMode.OnValidation);

                txtRecivedBy.DataBindings.Add("Text", bsClaimEntery, "ReciviedBy", false, DataSourceUpdateMode.OnPropertyChanged);
                txtRemarks.DataBindings.Add("Text", bsClaimEntery, "RecivingRemarks", false, DataSourceUpdateMode.OnPropertyChanged);

                txtName_Entery.DataBindings.Add("Text", bsClaimEntery, "EnteryUser", false, DataSourceUpdateMode.OnPropertyChanged);
                dtpDate_Entery.DataBindings.Add("Text", bsClaimEntery, "EnteryDate", false, DataSourceUpdateMode.OnPropertyChanged);
                txtRemarks_Entery.DataBindings.Add("Text", bsClaimEntery, "EnteryRemarks", false, DataSourceUpdateMode.OnValidation);

                txtName_Chariperson.DataBindings.Add("Text", bsClaimEntery, "Chariperson_Name", false, DataSourceUpdateMode.OnPropertyChanged);
                dtpDate_Chariperson.DataBindings.Add("Text", bsClaimEntery, "Chariperson_Date", false, DataSourceUpdateMode.OnPropertyChanged);
                txtRemarks_Chariperson.DataBindings.Add("Text", bsClaimEntery, "Chariperson_Remarks", false, DataSourceUpdateMode.OnValidation);

                txtName_Sectretary.DataBindings.Add("Text", bsClaimEntery, "Secteatary_Name", false, DataSourceUpdateMode.OnPropertyChanged);
                dtpDate_Sectretary.DataBindings.Add("Text", bsClaimEntery, "Secteatary_Date", false, DataSourceUpdateMode.OnPropertyChanged);
                txtRemarks_Sectretary.DataBindings.Add("Text", bsClaimEntery, "Secteatary_Remarks", false, DataSourceUpdateMode.OnValidation);

                txtName_Treasurer.DataBindings.Add("Text", bsClaimEntery, "Treasurer_Name", false, DataSourceUpdateMode.OnPropertyChanged);
                dtpDate_Treasurer.DataBindings.Add("Text", bsClaimEntery, "Treasurer_Date", false, DataSourceUpdateMode.OnPropertyChanged);
                txtRemarks_Treasurer.DataBindings.Add("Text", bsClaimEntery, "Treasurer_Remarks", false, DataSourceUpdateMode.OnValidation);
                lblClaimID.DataBindings.Add("Text", bsClaimEntery, "id", false, DataSourceUpdateMode.OnValidation);
                lblClaimStatus.DataBindings.Add("Text", bsClaimEntery, "ClaimDesc", false, DataSourceUpdateMode.OnValidation);
                lblClaimStatusNo.DataBindings.Add("Text", bsClaimEntery, "ClaimStatusNo", false, DataSourceUpdateMode.OnValidation);


                txtStatus_Sectretary.DataBindings.Add("Text", bsClaimEntery, "Secteatary_Status", false, DataSourceUpdateMode.OnPropertyChanged);
                txtStatus_Treasurer.DataBindings.Add("Text", bsClaimEntery, "Treasurer_Status", false, DataSourceUpdateMode.OnPropertyChanged);
                txtStatus_Chariperson.DataBindings.Add("Text", bsClaimEntery, "Chariperson_Status", false, DataSourceUpdateMode.OnPropertyChanged);

                grdClaimList.DataSource = bsClaimEntery;
                if (dtClaimEntery != null && dtClaimEntery.DefaultView.Count > 0)
                {
                    FillDocumentData(dtClaimEntery.DefaultView[0]["ID"].ToString());
                }
                else
                {
                    FillDocumentData("0");
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);

            }
        }
        private void SetEnable(bool lValue)
        {
            //txtLetPerson.Enabled = lValue;
            txtMemNationalId.Enabled = lValue;
            // txtMemberID.Enabled = lValue;
            //txtMemberName.Enabled = lValue;
            //txtMemContactNo.Enabled = lValue;
            //txtMemSchool.Enabled = lValue;
            //txtMemResidence.Enabled = lValue;
            txtBenNationalId.Enabled = lValue;
            //txtBenName.Enabled = lValue;
            //txtBenContactNo.Enabled = lValue;
            //txtBenResidence.Enabled = lValue;
            txtPlaceOfBurial.Enabled = lValue;
            txtMortuary.Enabled = lValue;
            txtEntry.Enabled = lValue;
            dtpDateOfBurial.Enabled = lValue;
            txtNomineeName.Enabled = lValue;
            txtTotalAmount.Enabled = lValue;
            txtRecivedBy.Enabled = lValue;
            txtRemarks.Enabled = lValue;
            //txtName_Entery.Enabled = lValue;
            //dtpDate_Entery.Enabled = lValue;
            txtRemarks_Entery.Enabled = lValue;




            rbLetBeneficry.Enabled = lValue;
            rbLetMember.Enabled = lValue;

            txtName_Chariperson.Enabled = false;
            dtpDate_Chariperson.Enabled = false;
            txtRemarks_Chariperson.Enabled = false;
            txtName_Sectretary.Enabled = false;
            dtpDate_Sectretary.Enabled = false;
            txtRemarks_Sectretary.Enabled = false;
            txtName_Treasurer.Enabled = false;
            dtpDate_Treasurer.Enabled = false;
            txtRemarks_Treasurer.Enabled = false;
            txtStatus_Chariperson.Enabled = false;
            txtStatus_Sectretary.Enabled = false;
            txtStatus_Treasurer.Enabled = false;
            rbApproved_Chariperson.Enabled = false;
            rbApproved_Sectretary.Enabled = false;
            rbApproved_Treasurer.Enabled = false;
            rbRejected_Chariperson.Enabled = false;
            rbRejected_Sectretary.Enabled = false;
            rbRejected_Treasurer.Enabled = false;

            btnPickBeneficiry.Enabled = lValue;
            btnPickMember.Enabled = lValue;

            grdDocList.Enabled = lValue;
            toolbarChild.Enabled = lValue;

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
                      btnPostSave.Enabled = !lValue;
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

                    break;
                case ClsUtility.enmFormMode.NormalMode:
                    SetEnable(false);
                    break;
            }
        }
        private void frmClaimEntry_Load(object sender, EventArgs e)
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
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                FillMemberData();
                FillDocumentData(lblClaimID.Text.Trim());
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
                bsClaimEntery.AllowNew = true;
                bsClaimEntery.AddNew();
                txtLetPerson.Text = "M";
                ClsUtility.FormMode = ClsUtility.enmFormMode.AddMode;
                SetFormMode(ClsUtility.enmFormMode.AddMode);
                rbLetMember.Checked = true;
                rbLetMember_CheckedChanged(rbLetMember, null);
                FillMandatoryDoc();
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
                if (lIsEdit() == false) { return; }
                ClsUtility.FormMode = ClsUtility.enmFormMode.EditMode;
                SetFormMode(ClsUtility.enmFormMode.EditMode);
                EditAfterPost("Edit");
                txtMemberName.Focus();
                EditChairPersonApproval();
                EditSectretaryApproval();
                EditTreasurerApproval();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {/*
                if (ValidateDetete() == false) { return; }
                if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
                {
                    Int32 iRow = bsClaimEntery.Position;
                    bsClaimEntery.RemoveAt(iRow);
                    // dtDepartment.DefaultView.Delete(iRow);
                    bsClaimEntery.EndEdit();
                    if (dtClaimEntery != null && dtClaimEntery.DefaultView.Count > 0)
                    {
                        //dtClaimEntery.DefaultView[iRow].BeginEdit();

                        //dtClaimEntery.DefaultView[iRow].EndEdit();
                        if (dtClaimEntery.GetChanges() != null)
                        {

                            bool lReturn = false;
                            strSqlQuery = "SELECT id , nationalid , memberid , employeeno , tscno , membername , dob , sex , school , contactno1 , contactno2 , residentaladdress , nomineenationalid , nomineename , wagesamount , wageseffectivedete , imagelocation , createdby , createddate , updateby , updateddate,email FROM SNAT.dbo.T_Member (nolock) WHERE 1=2";
                            lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtClaimEntery);

                            if (lReturn == true)
                            {
                                ClsMessage.showDeleteMessage();
                                ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                                SetFormMode(ClsUtility.enmFormMode.NormalMode);
                                dtClaimEntery.AcceptChanges();
                                FillMemberData();
                            }
                            else
                            {
                                ClsMessage.showMessage("Some problem occurs while deleting please contact system administrator.", MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                */
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

                if (string.IsNullOrEmpty(txtMemNationalId.Text.Trim()))
                {
                    errorProvider1.SetError(txtMemNationalId, "Member National id cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtMemNationalId, -20);
                    return false;
                }

                if (string.IsNullOrEmpty(txtMemberID.Text.Trim()))
                {
                    errorProvider1.SetError(txtMemberID, "Member id cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtMemberID, -20);
                    return false;
                }



                if (string.IsNullOrEmpty(txtMemberName.Text.Trim()))
                {
                    errorProvider1.SetError(txtMemberName, "Member name cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtMemberName, -20);
                    return false;
                }
                if (rbLetBeneficry.Checked)
                {
                    if (string.IsNullOrEmpty(txtBenNationalId.Text.Trim()))
                    {
                        errorProvider1.SetError(txtBenNationalId, "Beneficiary National id cannot be left blank.");
                        this.errorProvider1.SetIconPadding(this.txtBenNationalId, -20);
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(txtPlaceOfBurial.Text.Trim()))
                {
                    errorProvider1.SetError(txtPlaceOfBurial, "Place of burial cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtPlaceOfBurial, -20);
                    return false;
                }
                if (string.IsNullOrEmpty(txtMortuary.Text.Trim()))
                {
                    errorProvider1.SetError(txtMortuary, "Mortuary cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtMortuary, -20);
                    return false;
                }
                if (string.IsNullOrEmpty(txtEntry.Text.Trim()))
                {
                    errorProvider1.SetError(txtEntry, "Entry cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtEntry, -20);
                    return false;
                }
                if (string.IsNullOrEmpty(dtpDateOfBurial.Text.Trim()))
                {
                    errorProvider1.SetError(dtpDateOfBurial, "Burial date cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.dtpDateOfBurial, -20);
                    return false;
                }
                if (string.IsNullOrEmpty(txtNomineeName.Text.Trim()))
                {
                    errorProvider1.SetError(txtNomineeName, "Nominee Name cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtNomineeName, -20);
                    return false;
                }

                if (string.IsNullOrEmpty(txtTotalAmount.Text.Trim()))
                {
                    errorProvider1.SetError(txtTotalAmount, "Total amount cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtTotalAmount, -20);
                    return false;
                }

                if (string.IsNullOrEmpty(txtRecivedBy.Text.Trim()))
                {
                    errorProvider1.SetError(txtRecivedBy, "Received by cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtRecivedBy, -20);
                    return false;
                }

                if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
                {
                    errorProvider1.SetError(txtRemarks, "Claim remarks cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtRemarks, -20);
                    return false;
                }

                if (txtName_Chariperson.Enabled == true && string.IsNullOrEmpty(txtName_Chariperson.Text.Trim()))
                {
                    errorProvider1.SetError(txtName_Chariperson, "Chairperson name cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtName_Chariperson, -20);
                    return false;
                }


                if (rbApproved_Chariperson.Enabled == true && rbApproved_Chariperson.Checked == false)
                {
                    if (rbRejected_Chariperson.Enabled == true && rbRejected_Chariperson.Checked == false)
                    {
                        errorProvider1.SetError(rbApproved_Chariperson, "Please select chairperson approve/reject .");
                        //this.errorProvider1.SetIconPadding(this.txtTotalAmount, -20);
                        return false;
                    }
                }

                if (txtName_Sectretary.Enabled == true && string.IsNullOrEmpty(txtName_Sectretary.Text.Trim()))
                {
                    errorProvider1.SetError(txtName_Sectretary, "Secretary name cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtName_Sectretary, -20);
                    return false;
                }


                if (rbApproved_Sectretary.Enabled == true && rbApproved_Sectretary.Checked == false)
                {
                    if (rbRejected_Sectretary.Enabled == true && rbRejected_Sectretary.Checked == false)
                    {
                        errorProvider1.SetError(rbApproved_Sectretary, "Please select secretary approve/reject .");
                        // this.errorProvider1.SetIconPadding(this.txtTotalAmount, -20);
                        return false;
                    }
                }

                if (txtName_Treasurer.Enabled == true && string.IsNullOrEmpty(txtName_Treasurer.Text.Trim()))
                {
                    errorProvider1.SetError(txtName_Treasurer, "Treasurer name cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtName_Treasurer, -20);
                    return false;
                }


                if (rbApproved_Treasurer.Enabled == true && rbApproved_Treasurer.Checked == false)
                {
                    if (rbRejected_Treasurer.Enabled == true && rbRejected_Treasurer.Checked == false)
                    {
                        errorProvider1.SetError(rbApproved_Treasurer, "Please select treasurer approve/reject .");
                        //this.errorProvider1.SetIconPadding(this.txtTotalAmount, -20);
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
        private bool ValidateDocSave()
        {
            try
            {
                if (dtClaimDocument != null && dtClaimDocument.DefaultView.Count > 0)
                {
                    DataView dvDoc = new DataView();
                    dvDoc = mdsCreateDataView.DefaultViewManager.CreateDataView(dtClaimDocument);
                    foreach (DataRowView drvDoc in dvDoc)
                    {
                        if (drvDoc["dcIsDocAttached"] == DBNull.Value || Convert.ToBoolean(drvDoc["dcIsDocAttached"]) == false)
                        {
                            ClsMessage.ProjectExceptionMessage("Please make sure all claim document attached properly.");
                            return false;
                        }

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
            SqlTransaction sqlTrans = null;
            try
            {


                if (ValidateSave() == false) { return; }

                if (dtClaimDocument != null && dtClaimDocument.DefaultView.Count > 0)
                {
                    if (ValidateDocSave() == false) { return; }
                }


                bsClaimEntery.EndEdit();
                if (dtClaimEntery != null && dtClaimEntery.DefaultView.Count > 0)
                {
                    Int32 iRow = bsClaimEntery.Position;
                    dtClaimEntery.DefaultView[iRow].BeginEdit();
                    if (rbLetMember.Checked == true)
                    {
                        dtClaimEntery.DefaultView[iRow]["LetPerson"] = "M";
                    }
                    if (rbLetBeneficry.Checked == true)
                    {
                        dtClaimEntery.DefaultView[iRow]["LetPerson"] = "B";
                    }
                    if (string.IsNullOrEmpty(dtpDateOfBurial.Text.Trim()) == false)
                    {
                        dtClaimEntery.DefaultView[iRow]["DateOfBurial"] = dtpDateOfBurial.Value.ToShortDateString();
                    }

                    if (dtClaimEntery.DefaultView[iRow]["ClaimStatusNo"] == DBNull.Value || Convert.ToInt16(dtClaimEntery.DefaultView[iRow]["ClaimStatusNo"].ToString()) <= 1)
                    {
                        dtClaimEntery.DefaultView[iRow]["ClaimStatus"] = "E";
                        dtClaimEntery.DefaultView[iRow]["ClaimStatusNo"] = "1";
                    }

                    if (rbApproved_Chariperson.Enabled == true && rbApproved_Chariperson.Checked == true)
                    {
                        dtClaimEntery.DefaultView[iRow]["Chariperson_Status"] = "A";
                        dtClaimEntery.DefaultView[iRow]["ClaimStatus"] = "CA";
                        dtClaimEntery.DefaultView[iRow]["ClaimStatusNo"] = "3";
                    }
                    if (rbRejected_Chariperson.Enabled == true && rbRejected_Chariperson.Checked == true)
                    {
                        dtClaimEntery.DefaultView[iRow]["Chariperson_Status"] = "R";
                        dtClaimEntery.DefaultView[iRow]["ClaimStatus"] = "CR";
                        dtClaimEntery.DefaultView[iRow]["ClaimStatusNo"] = "3";
                    }

                    if (dtpDate_Chariperson.Enabled == true && string.IsNullOrEmpty(dtpDate_Chariperson.Text.Trim()) == false)
                    {
                        dtClaimEntery.DefaultView[iRow]["Chariperson_Date"] = dtpDate_Chariperson.Value.ToShortDateString();
                    }

                    if (rbApproved_Sectretary.Enabled == true && rbApproved_Sectretary.Checked == true)
                    {
                        dtClaimEntery.DefaultView[iRow]["Secteatary_Status"] = "A";
                        dtClaimEntery.DefaultView[iRow]["ClaimStatus"] = "SA";
                        dtClaimEntery.DefaultView[iRow]["ClaimStatusNo"] = "4";
                    }
                    if (rbRejected_Sectretary.Enabled == true && rbRejected_Sectretary.Checked == true)
                    {
                        dtClaimEntery.DefaultView[iRow]["Secteatary_Status"] = "R";
                        dtClaimEntery.DefaultView[iRow]["ClaimStatus"] = "SR";
                        dtClaimEntery.DefaultView[iRow]["ClaimStatusNo"] = "4";
                    }
                    if (dtpDate_Sectretary.Enabled == true && string.IsNullOrEmpty(dtpDate_Sectretary.Text.Trim()) == false)
                    {
                        dtClaimEntery.DefaultView[iRow]["Secteatary_Date"] = dtpDate_Sectretary.Value.ToShortDateString();
                    }
                    if (rbApproved_Treasurer.Enabled == true && rbApproved_Treasurer.Checked == true)
                    {
                        dtClaimEntery.DefaultView[iRow]["Treasurer_Status"] = "A";
                        dtClaimEntery.DefaultView[iRow]["ClaimStatus"] = "TA";
                        dtClaimEntery.DefaultView[iRow]["ClaimStatusNo"] = "5";
                    }
                    if (rbRejected_Treasurer.Enabled == true && rbRejected_Treasurer.Checked == true)
                    {
                        dtClaimEntery.DefaultView[iRow]["Treasurer_Status"] = "R";
                        dtClaimEntery.DefaultView[iRow]["ClaimStatus"] = "TR";
                        dtClaimEntery.DefaultView[iRow]["ClaimStatusNo"] = "5";
                    }
                    if (dtpDate_Treasurer.Enabled == true && string.IsNullOrEmpty(dtpDate_Treasurer.Text.Trim()) == false)
                    {
                        dtClaimEntery.DefaultView[iRow]["Treasurer_Date"] = dtpDate_Treasurer.Value.ToShortDateString();
                    }


                    if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                    {
                        dtClaimEntery.DefaultView[iRow]["EnteryUser"] = ClsSettings.username;
                        dtClaimEntery.DefaultView[iRow]["EnteryDate"] = DateTime.Now.ToString();

                        dtClaimEntery.DefaultView[iRow]["createdby"] = ClsSettings.username;
                        dtClaimEntery.DefaultView[iRow]["CreateDate"] = DateTime.Now.ToString();
                    }
                    dtClaimEntery.DefaultView[iRow]["UpdateBy"] = ClsSettings.username;
                    dtClaimEntery.DefaultView[iRow]["UpdateDate"] = DateTime.Now.ToString();

                    dtClaimEntery.DefaultView[iRow].EndEdit();
                    if (dtClaimEntery.GetChanges() != null)
                    {
                        ClsDataLayer.openConnection();
                        sqlTrans = ClsDataLayer.dbConn.BeginTransaction();


                        strSqlQuery = "SELECT ce.id, ce.LetPerson, ce.MemNationalID, ce.MemberID, ce.MemName, ce.BenfNationalID, ce.BenfName, ce.PlaceOfBurial," + Environment.NewLine +
                                       " ce.Mortuary, ce.Entery, ce.DateOfBurial,ce.NomineeName, ce.TotalAmount, ce.ReciviedBy, ce.RecivingRemarks, ce.EnteryUser," + Environment.NewLine +
                                       " ce.EnteryDate, ce.EnteryRemarks, ce.Chariperson_Name, ce.Chariperson_Date,ce.Chariperson_Remarks, ce.Chariperson_Status, " + Environment.NewLine +
                                       " ce.Secteatary_Name, ce.Secteatary_Date, ce.Secteatary_Remarks, ce.Secteatary_Status, ce.Treasurer_Name,ce.Treasurer_Date," + Environment.NewLine +
                                       " ce.Treasurer_Remarks, ce.Treasurer_Status,ce.ClaimStatus, ce.CreatedBy, ce.CreateDate, ce.UpdateBy, ce.UpdateDate,ce.ClaimStatusNo" + Environment.NewLine +
                                       " FROM SNAT.dbo.T_ClaimEntery ce (nolock) WHERE 1=2";

                        iRow = bsClaimEntery.Position;
                        dtClaimEntery.DefaultView[iRow].BeginEdit();
                        dtClaimEntery.DefaultView[iRow].EndEdit();
                        ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtClaimEntery, sqlTrans);

                        // string strClaimID = dtClaimEntery.DefaultView[iRow]["id"].ToString();
                        if (dtClaimDocument.GetChanges() != null)
                        {

                            string strDoc = "";
                            foreach (DataRowView drvMemDocRow in dtClaimDocument.DefaultView)
                            {
                                drvMemDocRow.BeginEdit();
                                strDoc = drvMemDocRow["doccode"].ToString();

                                string imagenewlocation = "";// Path.GetFullPath(drvMemDocRow["docLocation"].ToString().Trim());
                                string DocLocation = Path.GetFullPath(drvMemDocRow["docLocation"].ToString().Trim());// Path.GetExtension(drvMemDocRow["docLocation"].ToString().Trim());
                                if (File.Exists(DocLocation))
                                {
                                    GC.Collect();

                                    imagenewlocation = CopyDocument(DocLocation, strDoc);

                                    drvMemDocRow["docLocation"] = imagenewlocation;
                                    drvMemDocRow["docUploaded"] = true;
                                    // drvMemDocRow["claimid"] = strClaimID;


                                }


                                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                                {

                                    drvMemDocRow["createdby"] = ClsSettings.username;
                                    drvMemDocRow["createddate"] = DateTime.Now.ToString();
                                }
                                drvMemDocRow["updatedby"] = ClsSettings.username;
                                drvMemDocRow["updateddate"] = DateTime.Now.ToString();
                                drvMemDocRow.EndEdit();
                            }






                            strSqlQuery = "SELECT cd.id, cd.claimid, cd.nationalid, cd.memberid, cd.membername, cd.beneficirynationalid, cd.beneficiaryname, cd.doccode, " + Environment.NewLine +
                            " cd.docLocation, cd.docUploaded, cd.createdby, cd.createddate, cd.updatedby, cd.updateddate,cd.IsMandatory FROM snat.dbo.T_ClaimDocuments  cd  Where claimid='" + lblClaimID.Text.Trim() + "' ";
                            //iRow = bsClaimEntery.Position;

                            ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtClaimDocument, sqlTrans);

                        }



                        sqlTrans.Commit();
                        dtClaimEntery.AcceptChanges();
                        dtClaimDocument.AcceptChanges();
                        ClsMessage.showSaveMessage();
                        ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                        SetFormMode(ClsUtility.enmFormMode.NormalMode);
                        FillMemberData();
                        FillDocumentData(lblClaimID.Text.Trim());
                        ClsDataLayer.clsoeConnection();
                        SendMail();
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                if (sqlTrans != null) { sqlTrans.Rollback(); ClsDataLayer.clsoeConnection(); }

            }
            finally
            {
                ClsDataLayer.clsoeConnection();
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                bsClaimEntery.CancelEdit();
                dtClaimEntery.RejectChanges();
                dtClaimDocument.RejectChanges();
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
                frmsrch.infSqlSelectQuery = "SELECT id ,LetPerson,MemNationalID,BenfNationalID  FROM SNAT.dbo.T_ClaimEntery CE";
                frmsrch.infSqlWhereCondtion = "";
                frmsrch.infSqlOrderBy = " id";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search claim ....";
                frmsrch.infCodeFieldName = "id";
                frmsrch.infDescriptionFieldName = "MemNationalID";
                frmsrch.infGridFieldName = " id ,LetPerson,MemNationalID,BenfNationalID";
                frmsrch.infGridFieldCaption = " Claim id,Let Person ,Member National id ,Beneficiary National ID";
                frmsrch.infGridFieldSize = "100,100,200,200";
                frmsrch.ShowDialog(this);
                if (frmsrch.DialogResult == DialogResult.OK && frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()))
                {
                    DataView dvDesg = new DataView();
                    dvDesg = frmsrch.infSearchReturnDataView;
                    dvDesg.RowFilter = "lSelect=1";

                    if (string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) == false)
                    {
                        int iRow = bsClaimEntery.Find("id", frmsrch.infCodeFieldText.Trim());
                        bsClaimEntery.Position = iRow;
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

        private void btnPickMember_Click(object sender, EventArgs e)
        {

            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = " SELECT ted.id, ted.nationalid, ted.memberid, ted.employeeno, ted.tscno, ted.membername, ted.school, ted.contactno1," + Environment.NewLine +
                                            " ted.residentaladdress, ted.nomineename,ms.name schoolname,ted.wagesamount,ted.nomineereleation," + Environment.NewLine +
                                            " CASE WHEN ISNULL(ted.nomineereleation,'')='R' then 6000 When ISNULL(ted.nomineereleation,'')='W' then 15000 else 0 End TotalAmount" + Environment.NewLine +
                                            " FROM SNAT.dbo.T_Member AS ted (nolock) LEFT OUTER JOIN SNAT.dbo.M_School ms (nolock) ON ms.code=ted.school ";
                frmsrch.infSqlWhereCondtion = " ted.livingstatus='L' AND ISNULL(ted.lActive,0)=1";
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
                        //int iRow = bsClaimEntery.Find("nationalid", frmsrch.infCodeFieldText.Trim());
                        //bsClaimEntery.Position = iRow;

                        txtMemNationalId.Text = string.IsNullOrEmpty(dvDesg[0]["nationalid"].ToString()) == true ? "" : dvDesg[0]["nationalid"].ToString();
                        txtMemberID.Text = string.IsNullOrEmpty(dvDesg[0]["memberid"].ToString()) == true ? "" : dvDesg[0]["memberid"].ToString();
                        txtMemberName.Text = string.IsNullOrEmpty(dvDesg[0]["membername"].ToString()) == true ? "" : dvDesg[0]["membername"].ToString();
                        txtMemContactNo.Text = string.IsNullOrEmpty(dvDesg[0]["contactno1"].ToString()) == true ? "" : dvDesg[0]["contactno1"].ToString();
                        txtMemSchool.Text = string.IsNullOrEmpty(dvDesg[0]["schoolname"].ToString()) == true ? "" : dvDesg[0]["schoolname"].ToString();
                        txtMemResidence.Text = string.IsNullOrEmpty(dvDesg[0]["residentaladdress"].ToString()) == true ? "" : dvDesg[0]["residentaladdress"].ToString();
                        txtNomineeName.Text = string.IsNullOrEmpty(dvDesg[0]["nomineename"].ToString()) == true ? "" : dvDesg[0]["nomineename"].ToString();
                        txtTotalAmount.Text = string.IsNullOrEmpty(dvDesg[0]["TotalAmount"].ToString()) == true ? "" : dvDesg[0]["TotalAmount"].ToString();

                        dvDesg.RowFilter = "";
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnPickBeneficiry_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMemNationalId.Text.Trim())) { ClsMessage.showMessage("Please select member national id. ", MessageBoxIcon.Information); txtMemNationalId.Focus(); return; }
            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT membernationalid , memberid , membername , beneficiarynatioanalid , beneficiaryname , lstatus,residentaladrees,contactno1 " + Environment.NewLine +
                                            " ,CASE WHEN ISNULL(tb.nomineename,'')='' then tb.membername  else tb.nomineename End nomineename " + Environment.NewLine +
                                            " ,CASE WHEN ISNULL(tb.nomineenationalid,'')='' then tb.membernationalid  else tb.nomineenationalid End nomineenationalid,cast(6000 AS decimal(18,3))  TotalAmount" + Environment.NewLine +
                                            " FROM SNAT.dbo.T_Beneficiary tb (nolock) "+ Environment.NewLine  +
                                            " INNER JOIN dbo.T_Member tm ON tm.nationalid=tb.membernationalid AND  tb.memberid = tm.memberid ";
                frmsrch.infSqlWhereCondtion = " membernationalid='" + txtMemNationalId.Text.Trim() + "' AND ISNULL(tm.lActive,0)=1";
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
                    txtBenNationalId.Text = frmsrch.infCodeFieldText.Trim();
                    txtBenName.Text = frmsrch.infDescriptionFieldText.Trim();

                    txtBenContactNo.Text = string.IsNullOrEmpty(dvDesg[0]["contactno1"].ToString()) == true ? "" : dvDesg[0]["contactno1"].ToString();
                    txtBenResidence.Text = string.IsNullOrEmpty(dvDesg[0]["residentaladrees"].ToString()) == true ? "" : dvDesg[0]["residentaladrees"].ToString();
                    txtNomineeName.Text = string.IsNullOrEmpty(dvDesg[0]["nomineename"].ToString()) == true ? "" : dvDesg[0]["nomineename"].ToString();
                    txtTotalAmount.Text = string.IsNullOrEmpty(dvDesg[0]["TotalAmount"].ToString()) == true ? "" : dvDesg[0]["TotalAmount"].ToString();
                    dvDesg.RowFilter = "";
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void rbLetMember_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLetBeneficry.Checked)
            {
                txtBenNationalId.Enabled = true;
                btnPickBeneficiry.Enabled = true;
            }
            else
            {
                txtBenNationalId.Enabled = false;
                btnPickBeneficiry.Enabled = false;
            }
        }
        private void EditAfterPost(string strType)
        {
            try
            {

                txtName_Chariperson.Enabled = false;
                dtpDate_Chariperson.Enabled = false;
                txtRemarks_Chariperson.Enabled = false;
                rbApproved_Chariperson.Enabled = false;
                rbRejected_Chariperson.Enabled = false;
                string strClaimid = "0";
                if (string.IsNullOrEmpty(lblClaimID.Text.Trim()) == false) { strClaimid = lblClaimID.Text.Trim(); }

                strSqlQuery = "SELECT TOP 1 1 AS cPostStatus  FROM SNAT.dbo.T_ClaimEntery ce WHERE isnull(ce.cPostStatus,'')='P' and id=" + strClaimid + " and ClaimStatusNo > 1";
                DataTable dtCheck = new DataTable();
                dtCheck = ClsDataLayer.GetDataTable(strSqlQuery);
                if (dtCheck != null && dtCheck.DefaultView.Count > 0)
                {



                    txtMemNationalId.Enabled = false;

                    txtBenNationalId.Enabled = false;

                    txtPlaceOfBurial.Enabled = false;
                    txtMortuary.Enabled = false;
                    txtEntry.Enabled = false;
                    dtpDateOfBurial.Enabled = false;
                    txtNomineeName.Enabled = false;
                    txtTotalAmount.Enabled = false;
                    txtRecivedBy.Enabled = false;
                    txtRemarks.Enabled = false;
                    txtName_Entery.Enabled = false;
                    dtpDate_Entery.Enabled = false;
                    txtRemarks_Entery.Enabled = false;




                    rbLetBeneficry.Enabled = false;
                    rbLetMember.Enabled = false;

                    txtName_Chariperson.Enabled = false;
                    dtpDate_Chariperson.Enabled = false;
                    txtRemarks_Chariperson.Enabled = false;
                    txtName_Sectretary.Enabled = false;
                    dtpDate_Sectretary.Enabled = false;
                    txtRemarks_Sectretary.Enabled = false;
                    txtName_Treasurer.Enabled = false;
                    dtpDate_Treasurer.Enabled = false;
                    txtRemarks_Treasurer.Enabled = false;
                    txtStatus_Chariperson.Enabled = false;
                    txtStatus_Sectretary.Enabled = false;
                    txtStatus_Treasurer.Enabled = false;
                    rbApproved_Chariperson.Enabled = false;
                    rbApproved_Sectretary.Enabled = false;
                    rbApproved_Treasurer.Enabled = false;
                    rbRejected_Chariperson.Enabled = false;
                    rbRejected_Sectretary.Enabled = false;
                    rbRejected_Treasurer.Enabled = false;

                    btnPickBeneficiry.Enabled = false;
                    btnPickMember.Enabled = false;

                    grdDocList.Enabled = false;
                    toolbarChild.Enabled = false;

                    /************************************************/

                    btnDelete.Enabled = false;
                    btnPostSave.Enabled = false;




                }
                else
                {
                    if (strType.ToUpper() != "edit".ToUpper())
                    {
                        ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                        SetFormMode(ClsUtility.enmFormMode.NormalMode);
                    }

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void EditChairPersonApproval()
        {
            try
            {

                txtName_Chariperson.Enabled = false;
                dtpDate_Chariperson.Enabled = false;
                txtRemarks_Chariperson.Enabled = false;
                rbApproved_Chariperson.Enabled = false;
                rbRejected_Chariperson.Enabled = false;

                if (ClsUtility.lsAutorizeForApproval(ClsUtility.enuAutorizeCheck.Chairperson) == false) { return; }

                string strClaimid = "0";
                if (string.IsNullOrEmpty(lblClaimID.Text.Trim()) == false) { strClaimid = lblClaimID.Text.Trim(); }

                strSqlQuery = "SELECT TOP 1 1 AS cPostStatus  FROM SNAT.dbo.T_ClaimEntery ce WHERE isnull(ce.cPostStatus,'')='P' AND isnull(ce.Chariperson_Status,'')='' and id=" + strClaimid + " ";
                DataTable dtCheck = new DataTable();
                dtCheck = ClsDataLayer.GetDataTable(strSqlQuery);
                if (dtCheck != null && dtCheck.DefaultView.Count > 0)
                {
                    if (dtCheck.DefaultView[0]["cPostStatus"] != DBNull.Value && Convert.ToBoolean(dtCheck.DefaultView[0]["cPostStatus"]) == true)
                    {

                        txtName_Chariperson.Enabled = true;
                        dtpDate_Chariperson.Enabled = true;
                        txtRemarks_Chariperson.Enabled = true;
                        rbApproved_Chariperson.Enabled = true;
                        rbRejected_Chariperson.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void EditSectretaryApproval()
        {
            try
            {
                txtName_Sectretary.Enabled = false;
                dtpDate_Sectretary.Enabled = false;
                txtRemarks_Sectretary.Enabled = false;
                rbApproved_Sectretary.Enabled = false;
                rbRejected_Sectretary.Enabled = false;
                if (ClsUtility.lsAutorizeForApproval(ClsUtility.enuAutorizeCheck.Sectretary) == false) { return; }
                string strClaimid = "0";
                if (string.IsNullOrEmpty(lblClaimID.Text.Trim()) == false) { strClaimid = lblClaimID.Text.Trim(); }
                strSqlQuery = "SELECT TOP 1 1 AS cPostStatus  FROM SNAT.dbo.T_ClaimEntery ce WHERE isnull(ce.cPostStatus,'')='P' AND  isnull(ce.Chariperson_Status,'')='A'  AND isnull(ce.Secteatary_Status,'')='' and id=" + strClaimid + " ";
                DataTable dtCheck = new DataTable();
                dtCheck = ClsDataLayer.GetDataTable(strSqlQuery);
                if (dtCheck != null && dtCheck.DefaultView.Count > 0)
                {
                    if (dtCheck.DefaultView[0]["cPostStatus"] != DBNull.Value && Convert.ToBoolean(dtCheck.DefaultView[0]["cPostStatus"]) == true)
                    {
                        txtName_Sectretary.Enabled = true;
                        dtpDate_Sectretary.Enabled = true;
                        txtRemarks_Sectretary.Enabled = true;
                        rbApproved_Sectretary.Enabled = true;
                        rbRejected_Sectretary.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void EditTreasurerApproval()
        {
            try
            {
                txtName_Treasurer.Enabled = false;
                dtpDate_Treasurer.Enabled = false;
                txtRemarks_Treasurer.Enabled = false;
                rbApproved_Treasurer.Enabled = false;
                rbRejected_Treasurer.Enabled = false;
                if (ClsUtility.lsAutorizeForApproval(ClsUtility.enuAutorizeCheck.Treasurer) == false) { return; }
                string strClaimid = "0";
                if (string.IsNullOrEmpty(lblClaimID.Text.Trim()) == false) { strClaimid = lblClaimID.Text.Trim(); }
                strSqlQuery = "SELECT TOP 1 1 AS cPostStatus  FROM SNAT.dbo.T_ClaimEntery ce WHERE isnull(ce.cPostStatus,'')='P' AND  isnull(ce.Chariperson_Status,'')='A' AND  isnull(ce.Secteatary_Status,'')='A' AND isnull(ce.Treasurer_Status,'')='' and id=" + strClaimid + "";
                DataTable dtCheck = new DataTable();
                dtCheck = ClsDataLayer.GetDataTable(strSqlQuery);
                if (dtCheck != null && dtCheck.DefaultView.Count > 0)
                {
                    if (dtCheck.DefaultView[0]["cPostStatus"] != DBNull.Value && Convert.ToBoolean(dtCheck.DefaultView[0]["cPostStatus"]) == true)
                    {
                        txtName_Treasurer.Enabled = true;
                        dtpDate_Treasurer.Enabled = true;
                        txtRemarks_Treasurer.Enabled = true;
                        rbApproved_Treasurer.Enabled = true;
                        rbRejected_Treasurer.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtMemberID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLetPerson_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtLetPerson.Text.Trim()))
                {
                    if (txtLetPerson.Text.Trim().ToUpper() == "B")
                    {
                        rbLetBeneficry.Checked = true;
                    }
                    if (txtLetPerson.Text.Trim().ToUpper() == "M")
                    {
                        rbLetMember.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnPostSave_Click(object sender, EventArgs e)
        {
            SqlTransaction sqlTrans = null;


            try
            {
                if (Convert.ToInt16(lblClaimStatusNo.Text.Trim().ToUpper()) > 1) { ClsMessage.showMessage("Claim record already posted!!" + Environment.NewLine + "Re-Posting cannot be possible.", MessageBoxIcon.Information); return; }
                if (dtClaimDocument == null || dtClaimDocument.DefaultView.Count <= 0) { ClsMessage.ProjectExceptionMessage("Claim supported document not uploaded!" + Environment.NewLine + "Please upload supported document."); return; }
                if (ValidateSave() == false) { return; }
                if (dtClaimDocument != null && dtClaimDocument.DefaultView.Count > 0)
                {
                    if (ValidateDocSave() == false) { return; }
                }

                ClsDataLayer.openConnection();
                sqlTrans = ClsDataLayer.dbConn.BeginTransaction();

                strSqlQuery = "Update SNAT.dbo.T_ClaimEntery  set cPostStatus='P',ClaimStatus='P',ClaimStatusNo=2 Where id='" + lblClaimID.Text.Trim() + "'";
                ClsDataLayer.ExcuteTranstion(strSqlQuery, sqlTrans);

                if (rbLetBeneficry.Checked)
                {
                    strSqlQuery = "Update   SNAT.dbo.T_Beneficiary set lStatus=0 , livingstatus='D'  , dateofDate=getdate() Where beneficiarynatioanalid='" + txtBenNationalId.Text.Trim() + "'";
                    ClsDataLayer.ExcuteTranstion(strSqlQuery, sqlTrans);
                }
                if (rbLetMember.Checked)
                {
                    strSqlQuery = "Update SNAT.dbo.T_Member set livingstatus='D'  , deathdate=getdate() Where nationalid='" + txtMemNationalId.Text.Trim() + "'";
                    ClsDataLayer.ExcuteTranstion(strSqlQuery, sqlTrans);
                }


                //strSqlQuery = "Update SNAT.dbo.T_ClaimEntery  set cPostStatus='P' Where id='" + lblClaimID.Text.Trim() + "'";
                //ClsDataLayer.ExcuteTranstion(strSqlQuery, sqlTrans);

                sqlTrans.Commit();
                ClsMessage.showMessage("Claim record posted successfully!!", MessageBoxIcon.Information);
                ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                SetFormMode(ClsUtility.enmFormMode.NormalMode);
                FillMemberData();
                FillDocumentData(lblClaimID.Text.Trim());
                SendMail();



            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                if (sqlTrans != null) { sqlTrans.Rollback(); }
            }
            finally
            {
                ClsDataLayer.clsoeConnection();
            }
        }
        private void FillMandatoryDoc()
        {
            try
            {

                //trSqlQuery = "SELECT id, claimid, nationalid, memberid, membername, beneficirynationalid, beneficiaryname, doccode," + Environment.NewLine +
                //              " docLocation, docUploaded, createdby, createddate, updatedby, updateddate FROM dbo.T_ClaimDocuments  Where claimid='" + strClaimID + "' ";
                //dtClaimDocument = ClsDataLayer.GetDataTable(strSqlQuery);
                //grdClaimList.DataSource = dtClaimDocument.DefaultView;
                DataTable dtMandDoc = new DataTable();
                strSqlQuery = "SELECT id,code,name,status,mandatory FROM SNAT.dbo.M_ClaimDocType mcdt WHERE ISNULL(status,0)=1 and Isnull(mandatory,0)=1";
                dtMandDoc = ClsDataLayer.GetDataTable(strSqlQuery);
                dtClaimDocument.Clear();
                DataView dvDoc = new DataView();
                dvDoc = mdsCreateDataView.DefaultViewManager.CreateDataView(dtClaimDocument);


                foreach (DataRowView drvDoc in dtMandDoc.DefaultView)
                {
                    dvDoc.RowFilter = "doccode='" + drvDoc["code"].ToString().Trim() + "'";
                    if (dvDoc == null || dvDoc.Count <= 0)
                    {
                        DataRow dRow = dtClaimDocument.NewRow();
                        dRow["claimid"] = lblClaimID.Text.Trim();
                        dRow["nationalid"] = txtMemNationalId.Text.Trim();
                        dRow["memberid"] = txtMemberID.Text.Trim();
                        dRow["membername"] = txtMemberName.Text.Trim();
                        dRow["beneficirynationalid"] = txtBenNationalId.Text.Trim();
                        dRow["beneficiaryname"] = txtBenName.Text.Trim();
                        dRow["doccode"] = drvDoc["code"];
                        dRow["docName"] = drvDoc["name"];
                        dRow["Preview"] = "Preview";
                        dRow["AttachDoc"] = "Attach Doc.";
                        dRow["dcIsDocAttached"] = false;
                        dRow["IsMandatory"] = true;

                        dtClaimDocument.Rows.Add(dRow);
                    }

                }
                // dtClaimDocument.AcceptChanges();

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnChildLoadDefault_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtClaimEntery != null && dtClaimEntery.DefaultView.Count > 0)
                {
                    FillDocumentData(lblClaimID.Text.Trim());
                    FillMandatoryDoc();
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnChildAdd_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtMemNationalId.Text.Trim())) { ClsMessage.showMessage("Please select member national id. ", MessageBoxIcon.Information); txtMemNationalId.Focus(); return; }
            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT id,code,name,status,mandatory FROM SNAT.dbo.M_ClaimDocType mcdt ";
                frmsrch.infSqlWhereCondtion = " ISNULL(status,0)=1 and Isnull(mandatory,0)=1";
                frmsrch.infSqlOrderBy = " code,name";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search claim document ....";
                frmsrch.infCodeFieldName = "code";
                frmsrch.infDescriptionFieldName = "name";
                frmsrch.infGridFieldName = " id,code,name,status,mandatory";
                frmsrch.infGridFieldCaption = " id ,Document Code , Document name ,status , mandatory";
                frmsrch.infGridFieldSize = "0,100,200,0,0";
                frmsrch.ShowDialog(this);
                if (frmsrch.DialogResult == DialogResult.OK && frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()))
                {
                    DataView dvDesg = new DataView();
                    dvDesg = frmsrch.infSearchReturnDataView;
                    dvDesg.RowFilter = "lSelect=1";
                    mdsCreateDataView = new DataSet();
                    DataView dvDoc = new DataView();
                    dvDoc = mdsCreateDataView.DefaultViewManager.CreateDataView(dtClaimDocument);
                    dvDoc.RowFilter = "doccode='" + frmsrch.infCodeFieldText.Trim() + "'";
                    if (dvDoc.Count <= 0)
                    {
                        DataRow dRow = dtClaimDocument.NewRow();
                        dRow["claimid"] = lblClaimID.Text.Trim();
                        dRow["nationalid"] = txtMemNationalId.Text.Trim();
                        dRow["memberid"] = txtMemberID.Text.Trim();
                        dRow["membername"] = txtMemberName.Text.Trim();
                        dRow["beneficirynationalid"] = txtBenNationalId.Text.Trim();
                        dRow["beneficiaryname"] = txtBenName.Text.Trim();
                        dRow["doccode"] = dvDesg[0]["code"];
                        dRow["docName"] = dvDesg[0]["name"];
                        dRow["Preview"] = "Preview";
                        dRow["AttachDoc"] = "Attach Doc.";
                        dRow["dcIsDocAttached"] = false;
                        dRow["IsMandatory"] = dvDesg[0]["mandatory"];
                        dtClaimDocument.Rows.Add(dRow);
                    }

                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnChildDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtClaimEntery != null && dtClaimEntery.DefaultView.Count > 0)
                {
                    if (dtClaimDocument != null && dtClaimDocument.DefaultView.Count > 0)
                    {
                        int iRow = 0;
                        iRow = grdDocList.CurrentRow.Index;

                        if (dtClaimDocument.DefaultView[iRow]["IsMandatory"] != DBNull.Value && Convert.ToBoolean(dtClaimDocument.DefaultView[iRow]["IsMandatory"]) == true)
                        {
                            ClsMessage.ProjectExceptionMessage("Mandatory document cannot delete!!");
                        }
                        dtClaimDocument.DefaultView[iRow].Delete();
                    }
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnChildUndo_Click(object sender, EventArgs e)
        {
            try
            {

                if (dtClaimEntery != null && dtClaimEntery.DefaultView.Count > 0)
                {
                    if (dtClaimDocument != null && dtClaimDocument.DefaultView.Count > 0)
                    {

                        if (dtClaimDocument.GetChanges() != null) { dtClaimDocument.RejectChanges(); }
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }

        string CopyDocument(string DocLocation, string docCode)
        {
            try
            {
            CopyDoc:
                if (Directory.Exists(ClsSettings.strClaimDocAttached + @"\" + lblClaimID.Text.Trim()))
                {

                    string Fileext = Path.GetExtension(DocLocation);
                    string FileFinalLocation = ClsSettings.strClaimDocAttached + @"\" + lblClaimID.Text.Trim()
                                               + "\\" + lblClaimID.Text.Trim() + "_" + docCode + Fileext;

                    if (File.Exists(FileFinalLocation))
                    {
                        string tempLoc = Path.GetTempPath() + lblClaimID.Text.Trim() + "_" + docCode + Fileext;
                        File.Copy(FileFinalLocation, tempLoc);
                        File.Delete(FileFinalLocation);
                        File.Copy(tempLoc, FileFinalLocation, true);
                        File.Delete(tempLoc);
                        return FileFinalLocation;

                    }
                    else
                    {
                        File.Copy(DocLocation, FileFinalLocation, true);
                        return FileFinalLocation;
                    }



                }
                else
                {
                    Directory.CreateDirectory(ClsSettings.strClaimDocAttached + @"\" + lblClaimID.Text.Trim());
                    goto CopyDoc;

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return null;
            }
        }

        private void grdDocList_CommandCellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {

            try
            {
                if (dtClaimEntery != null && dtClaimEntery.DefaultView.Count > 0)
                {
                    if (dtClaimDocument != null && dtClaimDocument.DefaultView.Count > 0)
                    {
                        if (ClsUtility.FormMode != ClsUtility.enmFormMode.NormalMode)
                        {


                            if (e.Column.FieldName.ToUpper() == "AttachDoc".ToUpper())
                            {
                                OpenFileDialog opdlg = new OpenFileDialog();
                                opdlg.Filter = "Portable document file (*.pdf)|*.pdf|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"; ;
                                if (opdlg.ShowDialog() == DialogResult.OK)
                                {
                                    string filelocation = opdlg.FileName;
                                    dtClaimDocument.DefaultView[e.RowIndex].BeginEdit();
                                    dtClaimDocument.DefaultView[e.RowIndex]["dcIsDocAttached"] = true;
                                    dtClaimDocument.DefaultView[e.RowIndex]["docLocation"] = filelocation;
                                    dtClaimDocument.DefaultView[e.RowIndex].EndEdit();
                                }
                            }
                        }
                        if (e.Column.FieldName.ToUpper() == "Preview".ToUpper())
                        {
                            if (dtClaimDocument.DefaultView[e.RowIndex]["docLocation"] != null &&
                                dtClaimDocument.DefaultView[e.RowIndex]["docLocation"].ToString().Trim() != "")
                            {
                                Process.Start(dtClaimDocument.DefaultView[e.RowIndex]["docLocation"].ToString().Trim());
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

        private void grdClaimList_SelectionChanging(object sender, Telerik.WinControls.UI.GridViewSelectionCancelEventArgs e)
        {
            try
            {
                FillDocumentData(lblClaimID.Text.Trim());
                EditAfterPost("");
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtStatus_Chariperson_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtStatus_Chariperson.Text.Trim() != null && !string.IsNullOrEmpty(txtStatus_Chariperson.Text.Trim()))
                {
                    if (txtStatus_Chariperson.Text.Trim().ToUpper() == "A")
                    {
                        rbApproved_Chariperson.Checked = true;
                    }
                    if (txtStatus_Chariperson.Text.Trim().ToUpper() == "R")
                    {
                        rbRejected_Chariperson.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtStatus_Sectretary_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtStatus_Sectretary.Text.Trim() != null && !string.IsNullOrEmpty(txtStatus_Sectretary.Text.Trim()))
                {
                    if (txtStatus_Sectretary.Text.Trim().ToUpper() == "A")
                    {
                        rbApproved_Sectretary.Checked = true;
                    }
                    if (txtStatus_Sectretary.Text.Trim().ToUpper() == "R")
                    {
                        rbRejected_Sectretary.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtStatus_Treasurer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtStatus_Treasurer.Text.Trim() != null && !string.IsNullOrEmpty(txtStatus_Treasurer.Text.Trim()))
                {
                    if (txtStatus_Treasurer.Text.Trim().ToUpper() == "A")
                    {
                        rbApproved_Treasurer.Checked = true;
                    }
                    if (txtStatus_Treasurer.Text.Trim().ToUpper() == "R")
                    {
                        rbApproved_Treasurer.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }


        private void SendMail()
        {
            try
            {
                string strClaimid = "0";
                if (string.IsNullOrEmpty(lblClaimID.Text.Trim()) == false) { strClaimid = lblClaimID.Text.Trim(); }
                strSqlQuery = "SELECT IsNull(ClaimStatusNo,0) ClaimStatusNo  FROM SNAT.dbo.T_ClaimEntery ce WHERE  id=" + strClaimid + "";
                DataTable dtCheck = new DataTable();
                dtCheck = ClsDataLayer.GetDataTable(strSqlQuery);
                if (dtCheck != null && dtCheck.DefaultView.Count > 0)
                {
                    switch (dtCheck.DefaultView[0]["ClaimStatusNo"].ToString())
                    {
                        case "1":
                            SendClaimMail();
                            break;
                        case "2":
                            SendClaimPostMail();
                            break;
                        case "3":
                            SendClaimChairPersonAppovalMail();
                            break;
                        case "4":
                            SendClaimSectreataryAppovalMail();
                            break;
                        case "5":
                            SendClaimTreasurerAppovalMail();
                            break;
                    }
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SendClaimMail()
        {
            try
            {

                string strLetPerson = "";
                string strBeneID = "";
                string strBeneName = "";
                if (rbLetMember.Checked)
                {
                    strLetPerson = "Member";
                }
                if (rbLetBeneficry.Checked)
                {
                    strLetPerson = "Beneficiary";
                }

                if (txtBenNationalId.Text.Trim() != "")
                {
                    strBeneID = txtBenNationalId.Text.Trim();
                    strBeneName = txtBenName.Text.Trim();
                }

                clsEmail.lClaimEntry(strLetPerson, txtMemNationalId.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), lblClaimID.Text.Trim(),
                                    txtPlaceOfBurial.Text.Trim(), txtMortuary.Text.Trim(), dtpDateOfBurial.Text.Trim(), txtTotalAmount.Text.Trim(),
                                    strBeneID, strBeneName, "Entered");
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SendClaimPostMail()
        {
            try
            {

                string strLetPerson = "";
                string strBeneID = "";
                string strBeneName = "";
                if (rbLetMember.Checked)
                {
                    strLetPerson = "Member";
                }
                if (rbLetBeneficry.Checked)
                {
                    strLetPerson = "Beneficiary";
                }

                if (txtBenNationalId.Text.Trim() != "")
                {
                    strBeneID = txtBenNationalId.Text.Trim();
                    strBeneName = txtBenName.Text.Trim();
                }

                clsEmail.lClaimEntry(strLetPerson, txtMemNationalId.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), lblClaimID.Text.Trim(),
                                    txtPlaceOfBurial.Text.Trim(), txtMortuary.Text.Trim(), dtpDateOfBurial.Text.Trim(), txtTotalAmount.Text.Trim(),
                                    strBeneID, strBeneName, "Posted");
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SendClaimChairPersonAppovalMail()
        {
            try
            {

                string strLetPerson = "";
                string strBeneID = "";
                string strBeneName = "";
                string strStatus = "";
                if (rbLetMember.Checked)
                {
                    strLetPerson = "Member";
                }
                if (rbLetBeneficry.Checked)
                {
                    strLetPerson = "Beneficiary";
                }

                if (txtBenNationalId.Text.Trim() != "")
                {
                    strBeneID = txtBenNationalId.Text.Trim();
                    strBeneName = txtBenName.Text.Trim();
                }

                if (rbApproved_Chariperson.Checked)
                {
                    strStatus = "Approved";
                }
                if (rbRejected_Chariperson.Checked)
                {
                    strStatus = "Rejected";
                }

                clsEmail.lClaimApproval(strLetPerson, txtMemNationalId.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), lblClaimID.Text.Trim(),
                                    txtPlaceOfBurial.Text.Trim(), txtMortuary.Text.Trim(), dtpDateOfBurial.Text.Trim(), txtTotalAmount.Text.Trim(),
                                    strBeneID, strBeneName, "Chairperson", txtName_Chariperson.Text.Trim(), dtpDate_Chariperson.Text.Trim()
                                    , txtRemarks_Chariperson.Text.Trim(), strStatus, "6");
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SendClaimSectreataryAppovalMail()
        {
            try
            {

                string strLetPerson = "";
                string strBeneID = "";
                string strBeneName = "";
                string strStatus = "";
                if (rbLetMember.Checked)
                {
                    strLetPerson = "Member";
                }
                if (rbLetBeneficry.Checked)
                {
                    strLetPerson = "Beneficiary";
                }

                if (txtBenNationalId.Text.Trim() != "")
                {
                    strBeneID = txtBenNationalId.Text.Trim();
                    strBeneName = txtBenName.Text.Trim();
                }

                if (rbApproved_Sectretary.Checked)
                {
                    strStatus = "Approved";
                }
                if (rbRejected_Sectretary.Checked)
                {
                    strStatus = "Rejected";
                }

                clsEmail.lClaimApproval(strLetPerson, txtMemNationalId.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), lblClaimID.Text.Trim(),
                                    txtPlaceOfBurial.Text.Trim(), txtMortuary.Text.Trim(), dtpDateOfBurial.Text.Trim(), txtTotalAmount.Text.Trim(),
                                    strBeneID, strBeneName, "Secretary", txtName_Sectretary.Text.Trim(), dtpDate_Sectretary.Text.Trim()
                                    , txtRemarks_Sectretary.Text.Trim(), strStatus, "7");
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SendClaimTreasurerAppovalMail()
        {
            try
            {

                string strLetPerson = "";
                string strBeneID = "";
                string strBeneName = "";
                string strStatus = "";
                if (rbLetMember.Checked)
                {
                    strLetPerson = "Member";
                }
                if (rbLetBeneficry.Checked)
                {
                    strLetPerson = "Beneficiary";
                }

                if (txtBenNationalId.Text.Trim() != "")
                {
                    strBeneID = txtBenNationalId.Text.Trim();
                    strBeneName = txtBenName.Text.Trim();
                }

                if (rbApproved_Treasurer.Checked)
                {
                    strStatus = "Approved";
                }
                if (rbRejected_Treasurer.Checked)
                {
                    strStatus = "Rejected";
                }

                clsEmail.lClaimApproval(strLetPerson, txtMemNationalId.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), lblClaimID.Text.Trim(),
                                    txtPlaceOfBurial.Text.Trim(), txtMortuary.Text.Trim(), dtpDateOfBurial.Text.Trim(), txtTotalAmount.Text.Trim(),
                                    strBeneID, strBeneName, "Treasurer", txtName_Treasurer.Text.Trim(), dtpDate_Treasurer.Text.Trim()
                                    , txtRemarks_Treasurer.Text.Trim(), strStatus, "8");
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private bool lIsEdit()
        {

            try
            {

                string strClaimid = "0";
                if (string.IsNullOrEmpty(lblClaimID.Text.Trim()) == false) { strClaimid = lblClaimID.Text.Trim(); }
                strSqlQuery = "SELECT IsNull(ClaimStatusNo,0) ClaimStatusNo, ISNULL(ce.ClaimStatus, '')  ClaimStatus FROM SNAT.dbo.T_ClaimEntery ce WHERE  id=" + strClaimid + "";
                DataTable dtCheck = new DataTable();
                dtCheck = ClsDataLayer.GetDataTable(strSqlQuery);
                if (dtCheck != null && dtCheck.DefaultView.Count > 0)
                {
                    switch (dtCheck.DefaultView[0]["ClaimStatusNo"].ToString())

                    {
                        case "5":
                            ClsMessage.ProjectExceptionMessage("Treasury approved/rejected this claim." + Environment.NewLine + "Cannot edit record!!");
                            return false;
                        case "3":
                            if (dtCheck.DefaultView[0]["ClaimStatus"].ToString().ToUpper() == "CR")
                            {
                                ClsMessage.ProjectExceptionMessage("Chairperson rejected this claim." + Environment.NewLine + "Cannot edit record!!");
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        case "4":
                            if (dtCheck.DefaultView[0]["ClaimStatus"].ToString().ToUpper() == "SR")
                            {
                                ClsMessage.ProjectExceptionMessage("Secretary rejected this claim." + Environment.NewLine + "Cannot edit record!!");
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        default:
                            return true;





                    }

                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }


    }
}
