using SNAT.Comman_Classes;
using SNAT.Comman_Form;
using SNAT.CommanClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SNAT.Claim
{
    public partial class frmChequeRequisition : Form
    {
        DataTable dtChqEntery = new DataTable();
        BindingSource bsChqEntery = new BindingSource();
        string strSqlQuery = "";
        ErrorProvider errorProvider1 = new ErrorProvider();
        DataTable dtChqDocument = new DataTable();
        DataSet mdsCreateDataView = new DataSet();
        public frmChequeRequisition()
        {
            InitializeComponent();
            BindControl();
        }
        void FillMemberData()
        {
            try
            {
                strSqlQuery = "SELECT ch.id, ch.ClaimID, ch.letperson, ch.MemNationalID, ch.MemberID, ch.MemName, tm.contactno1 AS memContact, ch.BeneNationalID," + Environment.NewLine +
                              " ch.BeneName, tb.contactno1 AS benContact, ch.Payee, ch.PayeeNationalID, ch.TotalAmount, ch.ChequeNo, ch.RequestBy, ch.RequestedDate," + Environment.NewLine +
                              " ch.ResonFor, ch.EnteryUser, ch.EnteryDate, ch.EnteryRemarks, ch.Chariperson_Name, ch.Chariperson_Date, ch.Chariperson_Remarks," + Environment.NewLine +
                              " ch.Chariperson_Status, ch.Secteatary_Name, ch.Secteatary_Date, ch.Secteatary_Remarks, ch.Secteatary_Status, ch.Treasurer_Name," + Environment.NewLine +
                              " ch.Treasurer_Date, ch.Treasurer_Remarks, ch.Treasurer_Status, ch.ChqStatus, ch.cPostStatus, ch.ChqRecivedy, ch.ChqRecivedNationalID," + Environment.NewLine +
                              " ch.ChqRecivingDate, ch.CreatedBy, ch.CreateDate, ch.UpdateBy, ch.UpdateDate" + Environment.NewLine +
                              " ,CASE" + Environment.NewLine +
                                 "  WHEN ISNULL(ch.ChqStatus,'')= 'E' THEN 'Cheque Request Entered'" + Environment.NewLine +
                                 "  WHEN ISNULL(ch.ChqStatus,'')= 'P' THEN 'Cheque Request Posted'" + Environment.NewLine +
                                 "  WHEN ISNULL(ch.ChqStatus,'')= 'CA' THEN 'Cheque Request Approved by Chairperson'" + Environment.NewLine +
                                 "  WHEN ISNULL(ch.ChqStatus,'')= 'CR' THEN 'Cheque Request Rejected by Chairperson'" + Environment.NewLine +
                                 "  WHEN ISNULL(ch.ChqStatus,'')= 'SA' THEN 'Cheque Request Approved by Secretary'" + Environment.NewLine +
                                 "  WHEN ISNULL(ch.ChqStatus,'')= 'SR' THEN 'Cheque Request Rejected by Secretary'" + Environment.NewLine +
                                 "  WHEN ISNULL(ch.ChqStatus,'')= 'TA' THEN 'Cheque Request Approved by Treasurer'" + Environment.NewLine +
                                 "  WHEN ISNULL(ch.ChqStatus,'')= 'TR' THEN 'Cheque Request Rejected by Treasurer'" + Environment.NewLine +
                                 "  WHEN ISNULL(ch.ChqStatus,'')= 'CI' THEN 'Cheque Issued && Received'" + Environment.NewLine +
                                 "  Else 'Cheque Under Processing..' END as ChqStatusDesc,ch.ChqStatusNo" + Environment.NewLine +
                              " FROM SNAT.dbo.T_ChequeEntry AS ch(nolock)" + Environment.NewLine +
                              " LEFT OUTER JOIN SNAT.dbo.T_Member AS tm ON tm.nationalid = ch.MemNationalID " + Environment.NewLine +
                              " LEFT OUTER JOIN SNAT.dbo.T_Beneficiary AS tb ON tb.beneficiarynatioanalid = ch.BeneNationalID";
                dtChqEntery = ClsDataLayer.GetDataTable(strSqlQuery);
                bsChqEntery.DataSource = dtChqEntery.DefaultView;
                bindingNavigatorMain.BindingSource = bsChqEntery;



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
                dtChqDocument = new DataTable();
                strSqlQuery = "SELECT cd.id, cd.ChqReqID, cd.nationalid, cd.memberid, cd.membername, cd.beneficirynationalid, cd.beneficiaryname, cd.doccode,mcdt.name docName," + Environment.NewLine +
                              " cd.docLocation, cd.docUploaded, cd.createdby, cd.createddate, cd.updatedby, cd.updateddate,cd.IsMandatory,docUploaded dcIsDocAttached,'Preview' Preview , cast('Attach Doc' as varchar(20)) AttachDoc " + Environment.NewLine +
                              " FROM SNAT.dbo.T_ChequeDocuments  cd LEFT OUTER JOIN SNAT.dbo.M_ChequeDocType mcdt (nolock) ON mcdt.code=cd.doccode Where ChqReqID='" + strClaimID + "' ";
                dtChqDocument = ClsDataLayer.GetDataTable(strSqlQuery);

                //DataColumn dcIsDocAttached = new DataColumn("dcIsDocAttached", typeof(System.Boolean));
                //dcIsDocAttached.DefaultValue = false;
                //dcIsDocAttached.ReadOnly = false;
                //dtChqDocument.Columns.Add(dcIsDocAttached);

                grdDocList.DataSource = dtChqDocument.DefaultView;
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

                txtChqRequisitionID.DataBindings.Add("Text", bsChqEntery, "id", false, DataSourceUpdateMode.OnValidation);
                txtClaimID.DataBindings.Add("Text", bsChqEntery, "ClaimID", false, DataSourceUpdateMode.OnValidation);
                txtLetPerson.DataBindings.Add(new Binding("Text", bsChqEntery, "letperson", false, DataSourceUpdateMode.OnValidation));
                txtMemNationalId.DataBindings.Add(new Binding("Text", bsChqEntery, "MemNationalID", false, DataSourceUpdateMode.OnValidation));
                txtMemberID.DataBindings.Add("Text", bsChqEntery, "MemberID", false, DataSourceUpdateMode.OnValidation);
                txtMemberName.DataBindings.Add("Text", bsChqEntery, "MemName", false, DataSourceUpdateMode.OnValidation);
                txtMemContactNo.DataBindings.Add("Text", bsChqEntery, "memContact", false, DataSourceUpdateMode.OnValidation);

                txtBenNationalId.DataBindings.Add("Text", bsChqEntery, "BeneNationalID", false, DataSourceUpdateMode.OnPropertyChanged);
                txtBenName.DataBindings.Add("Text", bsChqEntery, "BeneName", false, DataSourceUpdateMode.OnValidation);
                txtBenContactNo.DataBindings.Add("Text", bsChqEntery, "benContact", false, DataSourceUpdateMode.OnValidation);
                txtTotalAmount.DataBindings.Add("Text", bsChqEntery, "TotalAmount", false, DataSourceUpdateMode.OnValidation);
                txtTotalAmount_D.DataBindings.Add("Text", bsChqEntery, "TotalAmount", false, DataSourceUpdateMode.OnValidation);

                txtPayee.DataBindings.Add("Text", bsChqEntery, "Payee", false, DataSourceUpdateMode.OnPropertyChanged);
                txtPayee_D.DataBindings.Add("Text", bsChqEntery, "Payee", false, DataSourceUpdateMode.OnPropertyChanged);
                txtPayeeNationalID.DataBindings.Add("Text", bsChqEntery, "PayeeNationalID", false, DataSourceUpdateMode.OnPropertyChanged);
                txtPayeeNationalID_D.DataBindings.Add("Text", bsChqEntery, "PayeeNationalID", false, DataSourceUpdateMode.OnPropertyChanged);

                txtChqNo.DataBindings.Add("Text", bsChqEntery, "ChequeNo", false, DataSourceUpdateMode.OnPropertyChanged);
                txtChqNo_D.DataBindings.Add("Text", bsChqEntery, "ChequeNo", false, DataSourceUpdateMode.OnPropertyChanged);
                txtRequestedBy.DataBindings.Add("Text", bsChqEntery, "RequestBy", false, DataSourceUpdateMode.OnPropertyChanged);
                dtpRequestDate.DataBindings.Add("Text", bsChqEntery, "RequestedDate", false, DataSourceUpdateMode.OnPropertyChanged);
                txtResonFor.DataBindings.Add("Text", bsChqEntery, "ResonFor", false, DataSourceUpdateMode.OnPropertyChanged);

                txtRecivedBy_D.DataBindings.Add("Text", bsChqEntery, "ChqRecivedy", false, DataSourceUpdateMode.OnPropertyChanged);
                txtReciverNationalID_D.DataBindings.Add("Text", bsChqEntery, "ChqRecivedNationalID", false, DataSourceUpdateMode.OnPropertyChanged);
                dtpChqRecivedDate.DataBindings.Add("Text", bsChqEntery, "ChqRecivingDate", false, DataSourceUpdateMode.OnPropertyChanged);

                txtName_Entery.DataBindings.Add("Text", bsChqEntery, "EnteryUser", false, DataSourceUpdateMode.OnPropertyChanged);
                dtpDate_Entery.DataBindings.Add("Text", bsChqEntery, "EnteryDate", false, DataSourceUpdateMode.OnPropertyChanged);
                txtRemarks_Entery.DataBindings.Add("Text", bsChqEntery, "EnteryRemarks", false, DataSourceUpdateMode.OnValidation);

                txtName_Chariperson.DataBindings.Add("Text", bsChqEntery, "Chariperson_Name", false, DataSourceUpdateMode.OnPropertyChanged);
                dtpDate_Chariperson.DataBindings.Add("Text", bsChqEntery, "Chariperson_Date", false, DataSourceUpdateMode.OnPropertyChanged);
                txtRemarks_Chariperson.DataBindings.Add("Text", bsChqEntery, "Chariperson_Remarks", false, DataSourceUpdateMode.OnValidation);

                txtName_Sectretary.DataBindings.Add("Text", bsChqEntery, "Secteatary_Name", false, DataSourceUpdateMode.OnPropertyChanged);
                dtpDate_Sectretary.DataBindings.Add("Text", bsChqEntery, "Secteatary_Date", false, DataSourceUpdateMode.OnPropertyChanged);
                txtRemarks_Sectretary.DataBindings.Add("Text", bsChqEntery, "Secteatary_Remarks", false, DataSourceUpdateMode.OnValidation);

                txtName_Treasurer.DataBindings.Add("Text", bsChqEntery, "Treasurer_Name", false, DataSourceUpdateMode.OnPropertyChanged);
                dtpDate_Treasurer.DataBindings.Add("Text", bsChqEntery, "Treasurer_Date", false, DataSourceUpdateMode.OnPropertyChanged);
                txtRemarks_Treasurer.DataBindings.Add("Text", bsChqEntery, "Treasurer_Remarks", false, DataSourceUpdateMode.OnValidation);

                lblChqStatus.DataBindings.Add("Text", bsChqEntery, "ChqStatusDesc", false, DataSourceUpdateMode.OnValidation);
                lblChqStatusNo.DataBindings.Add("Text", bsChqEntery, "ChqStatusNo", false, DataSourceUpdateMode.OnValidation);


                txtStatus_Sectretary.DataBindings.Add("Text", bsChqEntery, "Secteatary_Status", false, DataSourceUpdateMode.OnPropertyChanged);
                txtStatus_Treasurer.DataBindings.Add("Text", bsChqEntery, "Treasurer_Status", false, DataSourceUpdateMode.OnPropertyChanged);
                txtStatus_Chariperson.DataBindings.Add("Text", bsChqEntery, "Chariperson_Status", false, DataSourceUpdateMode.OnPropertyChanged);

                grdChequeList.DataSource = bsChqEntery;

                if (dtChqEntery != null && dtChqEntery.DefaultView.Count > 0)
                {
                    FillDocumentData(dtChqEntery.DefaultView[0]["ID"].ToString());
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

            txtClaimID.Enabled = lValue;
            txtPayee.Enabled = lValue;
            txtPayeeNationalID.Enabled = lValue;
            txtRecivedBy_D.Enabled = lValue;
            txtReciverNationalID_D.Enabled = lValue;
            txtRequestedBy.Enabled = lValue;
            dtpRequestDate.Enabled = lValue;
            txtResonFor.Enabled = lValue;
            txtRemarks_Entery.Enabled = lValue;
            txtChqNo.Enabled = lValue;
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

            btnPickClaimID.Enabled = lValue;


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
        private void frmChequeRequisition_Load(object sender, EventArgs e)
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
                FillDocumentData(txtChqRequisitionID.Text.Trim());
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
                bsChqEntery.AllowNew = true;
                bsChqEntery.AddNew();
                //  txtLetPerson.Text = "M";
                ClsUtility.FormMode = ClsUtility.enmFormMode.AddMode;
                SetFormMode(ClsUtility.enmFormMode.AddMode);
                //  rbLetMember.Checked = true;
                txtName_Entery.Text = ClsSettings.username;
                dtpDate_Entery.Text = DateTime.Now.ToShortDateString();
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
                if (lIsEdit("edit") == false) { return; }
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
                    Int32 iRow = bsChqEntery.Position;
                    bsChqEntery.RemoveAt(iRow);
                    // dtDepartment.DefaultView.Delete(iRow);
                    bsChqEntery.EndEdit();
                    if (dtChqEntery != null && dtChqEntery.DefaultView.Count > 0)
                    {
                        //dtChqEntery.DefaultView[iRow].BeginEdit();

                        //dtChqEntery.DefaultView[iRow].EndEdit();
                        if (dtChqEntery.GetChanges() != null)
                        {

                            bool lReturn = false;
                            strSqlQuery = "SELECT id , nationalid , memberid , employeeno , tscno , membername , dob , sex , school , contactno1 , contactno2 , residentaladdress , nomineenationalid , nomineename , wagesamount , wageseffectivedete , imagelocation , createdby , createddate , updateby , updateddate,email FROM SNAT.dbo.T_Member (nolock) WHERE 1=2";
                            lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtChqEntery);

                            if (lReturn == true)
                            {
                                ClsMessage.showDeleteMessage();
                                ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                                SetFormMode(ClsUtility.enmFormMode.NormalMode);
                                dtChqEntery.AcceptChanges();
                                FillMemberData();
                            }
                            else
                            {
                                ClsMessage.showMessage("Some problem occurs while deleting please contact system administrator.");
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
                if (string.IsNullOrEmpty(txtPayee.Text.Trim()))
                {
                    errorProvider1.SetError(txtPayee, "Payee name cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtPayee, -20);
                    return false;
                }
                if (string.IsNullOrEmpty(txtPayeeNationalID.Text.Trim()))
                {
                    errorProvider1.SetError(txtPayeeNationalID, "Payee national id cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtPayeeNationalID, -20);
                    return false;
                }
                if (string.IsNullOrEmpty(txtRequestedBy.Text.Trim()))
                {
                    errorProvider1.SetError(txtRequestedBy, "Request by cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtRequestedBy, -20);
                    return false;
                }
                if (string.IsNullOrEmpty(dtpRequestDate.Text.Trim()))
                {
                    errorProvider1.SetError(dtpRequestDate, "Request date cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.dtpRequestDate, -20);
                    return false;
                }
                if (string.IsNullOrEmpty(txtResonFor.Text.Trim()))
                {
                    errorProvider1.SetError(txtResonFor, "Reason for cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtResonFor, -20);

                    return false;
                }

                if (string.IsNullOrEmpty(txtTotalAmount.Text.Trim()))
                {
                    errorProvider1.SetError(txtTotalAmount, "Total amount cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtTotalAmount, -20);
                    return false;
                }
                if (string.IsNullOrEmpty(txtRecivedBy_D.Text.Trim()))
                {
                    errorProvider1.SetError(txtRecivedBy_D, "Receiver name cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtRecivedBy_D, -20);
                    ClsMessage.ProjectExceptionMessage("Receiver name cannot be left blank.");
                    tbMain.SelectedIndex = 1;
                    txtRecivedBy_D.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(txtReciverNationalID_D.Text.Trim()))
                {
                    errorProvider1.SetError(txtReciverNationalID_D, "Receiver national id cannot be left blank.");
                    this.errorProvider1.SetIconPadding(this.txtReciverNationalID_D, -20);
                    ClsMessage.ProjectExceptionMessage("Receiver national id cannot be left blank.");
                    tbMain.SelectedIndex = 1;
                    txtReciverNationalID_D.Focus();
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
                if (dtChqDocument != null && dtChqDocument.DefaultView.Count > 0)
                {
                    DataView dvDoc = new DataView();
                    dvDoc = mdsCreateDataView.DefaultViewManager.CreateDataView(dtChqDocument);
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

                if (dtChqDocument != null && dtChqDocument.DefaultView.Count > 0)
                {
                    if (ValidateDocSave() == false) { return; }
                }


                bsChqEntery.EndEdit();
                if (dtChqEntery != null && dtChqEntery.DefaultView.Count > 0)
                {
                    Int32 iRow = bsChqEntery.Position;
                    dtChqEntery.DefaultView[iRow].BeginEdit();
                    if (rbLetMember.Checked == true)
                    {
                        dtChqEntery.DefaultView[iRow]["letperson"] = "M";
                    }
                    if (rbLetBeneficry.Checked == true)
                    {
                        dtChqEntery.DefaultView[iRow]["letperson"] = "B";
                    }
                    if (string.IsNullOrEmpty(dtpRequestDate.Text.Trim()) == false)
                    {
                        dtChqEntery.DefaultView[iRow]["RequestedDate"] = dtpRequestDate.Value.ToShortDateString().Trim();
                    }

                    if (dtChqEntery.DefaultView[iRow]["ChqStatusNo"] == DBNull.Value || Convert.ToInt16(dtChqEntery.DefaultView[iRow]["ChqStatusNo"].ToString()) <= 1)
                    {

                        dtChqEntery.DefaultView[iRow]["ChqStatus"] = "E";
                        dtChqEntery.DefaultView[iRow]["ChqStatusNo"] = 1;
                    }

                    if (rbApproved_Chariperson.Enabled == true && rbApproved_Chariperson.Checked == true)
                    {
                        dtChqEntery.DefaultView[iRow]["Chariperson_Status"] = "A";
                        dtChqEntery.DefaultView[iRow]["ChqStatus"] = "CA";
                        dtChqEntery.DefaultView[iRow]["ChqStatusNo"] = 3;
                    }
                    if (rbRejected_Chariperson.Enabled == true && rbRejected_Chariperson.Checked == true)
                    {
                        dtChqEntery.DefaultView[iRow]["Chariperson_Status"] = "R";
                        dtChqEntery.DefaultView[iRow]["ChqStatus"] = "CR";
                        dtChqEntery.DefaultView[iRow]["ChqStatusNo"] = 3;
                    }

                    if (dtpDate_Chariperson.Enabled == true && string.IsNullOrEmpty(dtpDate_Chariperson.Text.Trim()) == false)
                    {
                        dtChqEntery.DefaultView[iRow]["Chariperson_Date"] = dtpDate_Chariperson.Value.ToShortDateString().Trim();
                    }

                    if (rbApproved_Sectretary.Enabled == true && rbApproved_Sectretary.Checked == true)
                    {
                        dtChqEntery.DefaultView[iRow]["Secteatary_Status"] = "A";
                        dtChqEntery.DefaultView[iRow]["ChqStatus"] = "SA";
                        dtChqEntery.DefaultView[iRow]["ChqStatusNo"] = 4;
                    }
                    if (rbRejected_Sectretary.Enabled == true && rbRejected_Sectretary.Checked == true)
                    {
                        dtChqEntery.DefaultView[iRow]["Secteatary_Status"] = "R";
                        dtChqEntery.DefaultView[iRow]["ChqStatus"] = "SR";
                        dtChqEntery.DefaultView[iRow]["ChqStatusNo"] = 4;
                    }
                    if (dtpDate_Sectretary.Enabled == true && string.IsNullOrEmpty(dtpDate_Sectretary.Text.Trim()) == false)
                    {
                        dtChqEntery.DefaultView[iRow]["Secteatary_Date"] = dtpDate_Sectretary.Value.ToShortDateString().Trim();
                    }
                    if (rbApproved_Treasurer.Enabled == true && rbApproved_Treasurer.Checked == true)
                    {
                        dtChqEntery.DefaultView[iRow]["Treasurer_Status"] = "A";
                        dtChqEntery.DefaultView[iRow]["ChqStatus"] = "TA";
                        dtChqEntery.DefaultView[iRow]["ChqStatusNo"] = 5;
                    }
                    if (rbRejected_Treasurer.Enabled == true && rbRejected_Treasurer.Checked == true)
                    {
                        dtChqEntery.DefaultView[iRow]["Treasurer_Status"] = "R";
                        dtChqEntery.DefaultView[iRow]["ChqStatus"] = "TR";
                        dtChqEntery.DefaultView[iRow]["ChqStatusNo"] = 5;
                    }
                    if (dtpDate_Treasurer.Enabled == true && string.IsNullOrEmpty(dtpDate_Treasurer.Text.Trim()) == false)
                    {
                        dtChqEntery.DefaultView[iRow]["Treasurer_Date"] = dtpDate_Treasurer.Value.ToShortDateString().Trim();
                    }


                    if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                    {
                        dtChqEntery.DefaultView[iRow]["EnteryUser"] = ClsSettings.username;
                        dtChqEntery.DefaultView[iRow]["EnteryDate"] = DateTime.Now.ToString();

                        dtChqEntery.DefaultView[iRow]["createdby"] = ClsSettings.username;
                        dtChqEntery.DefaultView[iRow]["CreateDate"] = DateTime.Now.ToString();
                    }
                    dtChqEntery.DefaultView[iRow]["UpdateBy"] = ClsSettings.username;
                    dtChqEntery.DefaultView[iRow]["UpdateDate"] = DateTime.Now.ToString();

                    dtChqEntery.DefaultView[iRow].EndEdit();
                    if (dtChqEntery.GetChanges() != null)
                    {
                        ClsDataLayer.openConnection();
                        sqlTrans = ClsDataLayer.dbConn.BeginTransaction();


                        strSqlQuery = "SELECT id, ClaimID, letperson, MemNationalID, MemberID, MemName, BeneNationalID, BeneName, Payee, PayeeNationalID," + Environment.NewLine +
                                      " TotalAmount, ChequeNo, RequestBy, RequestedDate, ResonFor, EnteryUser, EnteryDate, EnteryRemarks, Chariperson_Name," + Environment.NewLine +
                                      " Chariperson_Date, Chariperson_Remarks, Chariperson_Status, Secteatary_Name, Secteatary_Date, Secteatary_Remarks, Secteatary_Status," + Environment.NewLine +
                                      " Treasurer_Name, Treasurer_Date, Treasurer_Remarks, Treasurer_Status, ChqStatus, cPostStatus, ChqRecivedy, ChqRecivedNationalID," + Environment.NewLine +
                                      " ChqRecivingDate, CreatedBy, CreateDate, UpdateBy, UpdateDate,ChqStatusNo FROM SNAT.dbo.T_ChequeEntry WHERE 1=2";

                        iRow = bsChqEntery.Position;
                        dtChqEntery.DefaultView[iRow].BeginEdit();
                        dtChqEntery.DefaultView[iRow].EndEdit();
                        ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtChqEntery, sqlTrans);

                        // string strClaimID = dtClaimEntery.DefaultView[iRow]["id"].ToString();
                        if (dtChqDocument.GetChanges() != null)
                        {

                            string strDoc = "";
                            foreach (DataRowView drvMemDocRow in dtChqDocument.DefaultView)
                            {
                                strDoc = drvMemDocRow["doccode"].ToString();

                                string imagenewlocation = "";// Path.GetFullPath(drvMemDocRow["docLocation"].ToString().Trim());
                                string DocLocation = Path.GetFullPath(drvMemDocRow["docLocation"].ToString().Trim());// Path.GetExtension(drvMemDocRow["docLocation"].ToString().Trim());
                                if (File.Exists(DocLocation))
                                {
                                    GC.Collect();

                                    imagenewlocation = CopyDocument(DocLocation, strDoc);
                                    drvMemDocRow.BeginEdit();
                                    drvMemDocRow["docLocation"] = imagenewlocation;
                                    drvMemDocRow["docUploaded"] = true;
                                    // drvMemDocRow["claimid"] = strClaimID;
                                    if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                                    {

                                        drvMemDocRow["createdby"] = ClsSettings.username;
                                        drvMemDocRow["createddate"] = DateTime.Now.ToString();
                                    }
                                    drvMemDocRow["updatedby"] = ClsSettings.username;
                                    drvMemDocRow["updateddate"] = DateTime.Now.ToString();
                                    drvMemDocRow.EndEdit();

                                }
                            }
                        }
                        strSqlQuery = "SELECT cd.id, cd.ChqReqID, cd.nationalid, cd.memberid, cd.membername, cd.beneficirynationalid, cd.beneficiaryname, cd.doccode, " + Environment.NewLine +
                    " cd.docLocation, cd.docUploaded, cd.createdby, cd.createddate, cd.updatedby, cd.updateddate,cd.IsMandatory FROM snat.dbo.T_ChequeDocuments  cd  Where ChqReqID='" + txtChqRequisitionID.Text.Trim() + "' ";
                     
                        ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtChqDocument, sqlTrans);





                        sqlTrans.Commit();
                        dtChqEntery.AcceptChanges();
                        dtChqDocument.AcceptChanges();


                        ClsMessage.showSaveMessage();
                        ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                        SetFormMode(ClsUtility.enmFormMode.NormalMode);
                        FillMemberData();
                        FillDocumentData(txtChqRequisitionID.Text.Trim());
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
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                bsChqEntery.CancelEdit();
                dtChqEntery.RejectChanges();
                dtChqDocument.RejectChanges();
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
                frmsrch.infSqlSelectQuery = "SELECT ch.id, ch.ClaimID, ch.letperson, ch.MemNationalID,ch.BeneNationalID FROM SNAT.dbo.T_ChequeEntry AS ch(nolock)";
                frmsrch.infSqlWhereCondtion = "";
                frmsrch.infSqlOrderBy = " id";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Cheque Entry....";
                frmsrch.infCodeFieldName = "id";
                frmsrch.infDescriptionFieldName = "ClaimID";
                frmsrch.infGridFieldName = "id,ClaimID,letperson,MemNationalID,BeneNationalID";
                frmsrch.infGridFieldCaption = " Cheque id,Claim ID,Let Person ,Member National id ,Beneficiary National ID";
                frmsrch.infGridFieldSize = "100,100,100,200,200";
                frmsrch.ShowDialog(this);
                if (frmsrch.DialogResult == DialogResult.OK && frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()))
                {
                    DataView dvDesg = new DataView();
                    dvDesg = frmsrch.infSearchReturnDataView;
                    dvDesg.RowFilter = "lSelect=1";

                    if (string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) == false)
                    {
                        int iRow = bsChqEntery.Find("id", frmsrch.infCodeFieldText.Trim());
                        bsChqEntery.Position = iRow;
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
                frmsrch.infSqlSelectQuery = "SELECT CE.id,CE.LetPerson,CE.MemNationalID,CE.MemberID,CE.MemName,CE.BenfNationalID,CE.BenfName,CE.NomineeName,CE.TotalAmount,CE.ReciviedBy" + Environment.NewLine +
                                            " ,tm.nomineenationalid,tm.contactno1 MemContact, tb.contactno1 benContact" + Environment.NewLine +
                                            " FROM SNAT.dbo.T_ClaimEntery CE (nolock)" + Environment.NewLine +
                                            " LEFT OUTER JOIN  SNAT.dbo.T_Member AS tm  (nolock) ON tm.nationalid = CE.MemNationalID AND  tm.memberid = CE.MemNationalID" + Environment.NewLine +
                                            " LEFT OUTER JOIN  SNAT.dbo.T_Beneficiary AS tb  (nolock) ON tb.beneficiarynatioanalid = ce.BenfNationalID";
                frmsrch.infSqlWhereCondtion = " ISNULL(cPostStatus,'')='P' AND  ISNULL(Chariperson_Status,'')='A' AND  ISNULL(Secteatary_Status,'')='A' AND  ISNULL(Treasurer_Status,'')='A' " + Environment.NewLine +
                                              " AND NOT EXISTS (SELECT * FROM SNAT.dbo.T_ChequeEntry AS ch(nolock) WHERE ch.ClaimID=ce.id AND ISNULL(ch.cPostStatus,'')='P' AND  ISNULL(ch.Chariperson_Status,'')='A' AND  ISNULL(ch.Secteatary_Status,'')='A' AND  ISNULL(ch.Treasurer_Status,'')='A')";
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
                        //int iRow = bsClaimEntery.Find("id", frmsrch.infCodeFieldText.Trim());
                        //bsClaimEntery.Position = iRow;
                        //dvDesg.RowFilter = "";
                        txtClaimID.Text = string.IsNullOrEmpty(dvDesg[0]["id"].ToString()) == true ? "" : dvDesg[0]["id"].ToString();
                        txtLetPerson.Text = string.IsNullOrEmpty(dvDesg[0]["LetPerson"].ToString()) == true ? "" : dvDesg[0]["LetPerson"].ToString();
                        txtMemNationalId.Text = string.IsNullOrEmpty(dvDesg[0]["MemNationalID"].ToString()) == true ? "" : dvDesg[0]["MemNationalID"].ToString();
                        txtMemberID.Text = string.IsNullOrEmpty(dvDesg[0]["MemberID"].ToString()) == true ? "" : dvDesg[0]["MemberID"].ToString();
                        txtMemberName.Text = string.IsNullOrEmpty(dvDesg[0]["MemName"].ToString()) == true ? "" : dvDesg[0]["MemName"].ToString();
                        txtMemContactNo.Text = string.IsNullOrEmpty(dvDesg[0]["MemContact"].ToString()) == true ? "" : dvDesg[0]["MemContact"].ToString();
                        txtBenNationalId.Text = string.IsNullOrEmpty(dvDesg[0]["BenfNationalID"].ToString()) == true ? "" : dvDesg[0]["BenfNationalID"].ToString();
                        txtBenName.Text = string.IsNullOrEmpty(dvDesg[0]["BenfName"].ToString()) == true ? "" : dvDesg[0]["BenfName"].ToString();
                        txtBenContactNo.Text = string.IsNullOrEmpty(dvDesg[0]["benContact"].ToString()) == true ? "" : dvDesg[0]["benContact"].ToString();
                        txtTotalAmount.Text = string.IsNullOrEmpty(dvDesg[0]["TotalAmount"].ToString()) == true ? "" : dvDesg[0]["TotalAmount"].ToString();

                        txtPayee.Text = string.IsNullOrEmpty(dvDesg[0]["NomineeName"].ToString()) == true ? "" : dvDesg[0]["BenfName"].ToString();
                        txtPayeeNationalID.Text = string.IsNullOrEmpty(dvDesg[0]["nomineenationalid"].ToString()) == true ? "" : dvDesg[0]["benContact"].ToString();
                        txtPayee_D.Text = string.IsNullOrEmpty(dvDesg[0]["NomineeName"].ToString()) == true ? "" : dvDesg[0]["BenfName"].ToString();
                        txtPayeeNationalID_D.Text = string.IsNullOrEmpty(dvDesg[0]["nomineenationalid"].ToString()) == true ? "" : dvDesg[0]["benContact"].ToString();


                        dvDesg.RowFilter = "";
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
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
                if (string.IsNullOrEmpty(txtChqRequisitionID.Text.Trim()) == false) { strClaimid = txtChqRequisitionID.Text.Trim(); }

                strSqlQuery = "SELECT TOP 1 1 AS cPostStatus  FROM SNAT.dbo.T_ChequeEntry ce WHERE isnull(ce.cPostStatus,'')='P' and id=" + strClaimid + "  and ChqStatusNo > 1";
                DataTable dtCheck = new DataTable();
                dtCheck = ClsDataLayer.GetDataTable(strSqlQuery);
                if (dtCheck != null && dtCheck.DefaultView.Count > 0)
                {

                    txtChqRequisitionID.Enabled = false;
                    txtClaimID.Enabled = false;
                    btnPickClaimID.Enabled = false;

                    txtMemNationalId.Enabled = false;
                    txtBenNationalId.Enabled = false;
                    txtPayee.Enabled = false;
                    txtPayeeNationalID.Enabled = false;
                    txtRequestedBy.Enabled = false;
                    dtpRequestDate.Enabled = false;
                    txtResonFor.Enabled = false;
                    txtTotalAmount.Enabled = false;
                    txtChqNo.Enabled = false;
                    // txtre.Enabled = false;
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
                if (string.IsNullOrEmpty(txtChqRequisitionID.Text.Trim()) == false) { strClaimid = txtChqRequisitionID.Text.Trim(); }

                strSqlQuery = "SELECT TOP 1 1 AS cPostStatus  FROM SNAT.dbo.T_ChequeEntry ce WHERE isnull(ce.cPostStatus,'')='P' AND isnull(ce.Chariperson_Status,'')='' and id=" + strClaimid + "";
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
                if (string.IsNullOrEmpty(txtChqRequisitionID.Text.Trim()) == false) { strClaimid = txtChqRequisitionID.Text.Trim(); }
                strSqlQuery = "SELECT TOP 1 1 AS cPostStatus  FROM SNAT.dbo.T_ChequeEntry ce WHERE isnull(ce.cPostStatus,'')='P' AND  isnull(ce.Chariperson_Status,'')='A'  AND isnull(ce.Secteatary_Status,'')='' and id=" + strClaimid + "";
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
                if (string.IsNullOrEmpty(txtChqRequisitionID.Text.Trim()) == false) { strClaimid = txtChqRequisitionID.Text.Trim(); }
                strSqlQuery = "SELECT TOP 1 1 AS cPostStatus  FROM SNAT.dbo.T_ChequeEntry ce WHERE isnull(ce.cPostStatus,'')='P' AND  isnull(ce.Chariperson_Status,'')='A' AND  isnull(ce.Secteatary_Status,'')='A' AND isnull(ce.Treasurer_Status,'')='' and id=" + strClaimid + "";
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
                if (Convert.ToInt16(lblChqStatusNo.Text.Trim().ToUpper()) > 1) { ClsMessage.showMessage("Cheque Requisition record already posted!!" + Environment.NewLine + "Re-Posting cannot be possible."); return; }

                if (dtChqDocument == null || dtChqDocument.DefaultView.Count <= 0) { ClsMessage.ProjectExceptionMessage("Claim supported document not uploaded!" + Environment.NewLine + "Please upload supported document."); return; }
                if (ValidateSave() == false) { return; }
                if (dtChqDocument != null && dtChqDocument.DefaultView.Count > 0)
                {
                    if (ValidateDocSave() == false) { return; }
                }


                //  dtChqEntery.DefaultView[iRow]["ChqStatusNo"] = 5;

                ClsDataLayer.openConnection();
                sqlTrans = ClsDataLayer.dbConn.BeginTransaction();

                strSqlQuery = "Update SNAT.dbo.T_ChequeEntry  set ChqStatus='P', cPostStatus='P',ChqStatusNo=2 Where id='" + txtChqRequisitionID.Text.Trim() + "'";
                ClsDataLayer.ExcuteTranstion(strSqlQuery, sqlTrans);

                sqlTrans.Commit();
                ClsMessage.showMessage("Cheque Requisition record posted successfully!!");
                ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                SetFormMode(ClsUtility.enmFormMode.NormalMode);
                FillMemberData();
                FillDocumentData(txtChqRequisitionID.Text.Trim());
                ClsDataLayer.clsoeConnection();
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

        private void txtLetPerson_TextChanged_1(object sender, EventArgs e)
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
        private void FillMandatoryDoc()
        {
            try
            {

                //trSqlQuery = "SELECT id, claimid, nationalid, memberid, membername, beneficirynationalid, beneficiaryname, doccode," + Environment.NewLine +
                //              " docLocation, docUploaded, createdby, createddate, updatedby, updateddate FROM dbo.T_ClaimDocuments  Where claimid='" + strClaimID + "' ";
                //dtChqDocument = ClsDataLayer.GetDataTable(strSqlQuery);
                //grdClaimList.DataSource = dtChqDocument.DefaultView;
                DataTable dtMandDoc = new DataTable();
                strSqlQuery = "SELECT id,code,name,status,mandatory FROM SNAT.dbo.M_ChequeDocType mcdt WHERE ISNULL(status,0)=1 and Isnull(mandatory,0)=1";
                dtMandDoc = ClsDataLayer.GetDataTable(strSqlQuery);
                dtChqDocument.Clear();
                DataView dvDoc = new DataView();
                dvDoc = mdsCreateDataView.DefaultViewManager.CreateDataView(dtChqDocument);


                foreach (DataRowView drvDoc in dtMandDoc.DefaultView)
                {
                    dvDoc.RowFilter = "doccode='" + drvDoc["code"].ToString().Trim() + "'";
                    if (dvDoc == null || dvDoc.Count <= 0)
                    {
                        DataRow dRow = dtChqDocument.NewRow();
                        dRow["ChqReqID"] = txtChqRequisitionID.Text.Trim();
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

                        dtChqDocument.Rows.Add(dRow);
                    }

                }
                // dtChqDocument.AcceptChanges();

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
                if (dtChqEntery != null && dtChqEntery.DefaultView.Count > 0)
                {
                    FillDocumentData(txtChqRequisitionID.Text.Trim());
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

            if (string.IsNullOrEmpty(txtMemNationalId.Text.Trim())) { ClsMessage.showMessage("Please select member national id. "); txtMemNationalId.Focus(); return; }
            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT id,code,name,status,mandatory FROM SNAT.dbo.M_ChequeDocType mcdt ";
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
                    dvDoc = mdsCreateDataView.DefaultViewManager.CreateDataView(dtChqDocument);
                    dvDoc.RowFilter = "doccode='" + frmsrch.infCodeFieldText.Trim() + "'";
                    if (dvDoc.Count <= 0)
                    {
                        DataRow dRow = dtChqDocument.NewRow();
                        dRow["ChqReqID"] = txtChqRequisitionID.Text.Trim();
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
                        dtChqDocument.Rows.Add(dRow);
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
                if (dtChqEntery != null && dtChqEntery.DefaultView.Count > 0)
                {
                    if (dtChqDocument != null && dtChqDocument.DefaultView.Count > 0)
                    {
                        int iRow = 0;
                        iRow = grdDocList.CurrentRow.Index;

                        if (dtChqDocument.DefaultView[iRow]["IsMandatory"] != DBNull.Value && Convert.ToBoolean(dtChqDocument.DefaultView[iRow]["IsMandatory"]) == true)
                        {
                            ClsMessage.ProjectExceptionMessage("Mandatory document cannot delete!!");
                        }
                        dtChqDocument.DefaultView[iRow].Delete();
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

                if (dtChqEntery != null && dtChqEntery.DefaultView.Count > 0)
                {
                    if (dtChqDocument != null && dtChqDocument.DefaultView.Count > 0)
                    {

                        if (dtChqDocument.GetChanges() != null) { dtChqDocument.RejectChanges(); }
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
                if (Directory.Exists(ClsSettings.strClaimDocAttached + @"\" + txtChqRequisitionID.Text.Trim()))
                {

                    string Fileext = Path.GetExtension(DocLocation);
                    string FileFinalLocation = ClsSettings.strClaimDocAttached + @"\" + txtChqRequisitionID.Text.Trim()
                                               + "\\" + txtChqRequisitionID.Text.Trim() + "_" + docCode + Fileext;

                    if (File.Exists(FileFinalLocation))
                    {
                        string tempLoc = Path.GetTempPath() + txtChqRequisitionID.Text.Trim() + "_" + docCode + Fileext;
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
                    Directory.CreateDirectory(ClsSettings.strClaimDocAttached + @"\" + txtChqRequisitionID.Text.Trim());
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
                if (dtChqEntery != null && dtChqEntery.DefaultView.Count > 0)
                {
                    if (dtChqDocument != null && dtChqDocument.DefaultView.Count > 0)
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
                                    dtChqDocument.DefaultView[e.RowIndex].BeginEdit();
                                    dtChqDocument.DefaultView[e.RowIndex]["dcIsDocAttached"] = true;
                                    dtChqDocument.DefaultView[e.RowIndex]["docLocation"] = filelocation;
                                    dtChqDocument.DefaultView[e.RowIndex].EndEdit();
                                }
                            }
                        }
                        if (e.Column.FieldName.ToUpper() == "Preview".ToUpper())
                        {
                            if (dtChqDocument.DefaultView[e.RowIndex]["docLocation"] != null &&
                                dtChqDocument.DefaultView[e.RowIndex]["docLocation"].ToString().Trim() != "")
                            {
                                Process.Start(dtChqDocument.DefaultView[e.RowIndex]["docLocation"].ToString().Trim());
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

        private void btnChequeRecived_Click(object sender, EventArgs e)
        {

            SqlTransaction sqlTrans = null;
            try
            {

                //if (lblChqStatus.Text.Trim().ToUpper() == "Cheque Issued && Received".ToUpper()) { ClsMessage.showMessage("Cheque Issued && Received!!" + Environment.NewLine + "Re-Posting cannot be possible."); return; }
                if (lIsIssue() == false) { return; }
                if (dtChqEntery != null && dtChqEntery.DefaultView.Count > 0)
                {
                    if (ClsMessage.showQuestionMessage("Are you sure want to complete reviving process?") == DialogResult.No)
                    {
                        return;
                    }
                    strSqlQuery = "SELECT TOP 1 1 FROM SNAT.dbo.T_ChequeEntry AS ch(nolock) WHERE ch.ID='" + txtChqRequisitionID.Text.Trim() + "' AND ISNULL(ch.cPostStatus,'')='P' AND  ISNULL(ch.Chariperson_Status,'')='A' AND  ISNULL(ch.Secteatary_Status,'')='A' AND  ISNULL(ch.Treasurer_Status,'')='A'";
                    DataTable dtChkec = new DataTable();
                    dtChkec = ClsDataLayer.GetDataTable(strSqlQuery);
                    if (dtChkec != null && dtChkec.DefaultView.Count > 0)
                    {


                        if (dtChqDocument == null || dtChqDocument.DefaultView.Count <= 0) { ClsMessage.ProjectExceptionMessage("Cheque supported document not uploaded!" + Environment.NewLine + "Please upload supported document."); return; }
                        if (ValidateSave() == false) { return; }
                        if (dtChqDocument != null && dtChqDocument.DefaultView.Count > 0)
                        {
                            if (ValidateDocSave() == false) { return; }
                        }

                        if (string.IsNullOrEmpty(txtRecivedBy_D.Text.Trim()))
                        {
                            errorProvider1.SetError(txtRecivedBy_D, "Receiver name cannot be left blank.");
                            this.errorProvider1.SetIconPadding(this.txtRecivedBy_D, -20);
                            txtRecivedBy_D.Focus();
                            return;
                        }

                        if (string.IsNullOrEmpty(txtReciverNationalID_D.Text.Trim()))
                        {
                            errorProvider1.SetError(txtReciverNationalID_D, "Receiver national id cannot be left blank.");
                            this.errorProvider1.SetIconPadding(this.txtReciverNationalID_D, -20);
                            txtReciverNationalID_D.Focus();
                            return;
                        }


                        ClsDataLayer.openConnection();
                        sqlTrans = ClsDataLayer.dbConn.BeginTransaction();

                        strSqlQuery = "Update SNAT.dbo.T_ChequeEntry  set ChqStatus='CI',ChqStatusNo=6 ,ChqRecivingDate=Getdate() Where id='" + txtChqRequisitionID.Text.Trim() + "'";
                        ClsDataLayer.ExcuteTranstion(strSqlQuery, sqlTrans);

                        sqlTrans.Commit();
                        ClsMessage.showMessage("Cheque Requisition record issue and received successfully!!");
                        ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                        SetFormMode(ClsUtility.enmFormMode.NormalMode);
                        FillMemberData();
                        FillDocumentData(txtChqRequisitionID.Text.Trim());
                    }
                    else
                    {
                        ClsMessage.showMessage("Please make sure all approval process completed!!"); return;
                    }
                }

                //}
                //else { ClsMessage.showMessage("Some problem occurs while saving please contact system administrator."); return; }



                //
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
                if (string.IsNullOrEmpty(txtChqRequisitionID.Text.Trim()) == false) { strClaimid = txtChqRequisitionID.Text.Trim(); }
                strSqlQuery = "SELECT IsNull(ChqStatusNo,0) ChqStatusNo  FROM SNAT.dbo.T_ChequeEntry ce WHERE  id=" + strClaimid + "";
                DataTable dtCheck = new DataTable();
                dtCheck = ClsDataLayer.GetDataTable(strSqlQuery);
                if (dtCheck != null && dtCheck.DefaultView.Count > 0)
                {
                    switch (dtCheck.DefaultView[0]["ChqStatusNo"].ToString())
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

                clsEmail.lChequeEntry(strLetPerson, txtMemNationalId.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), txtChqRequisitionID.Text.Trim(),
                                   txtPayee.Text.Trim(), txtChqNo.Text.Trim(), txtRequestedBy.Text.Trim(), dtpRequestDate.Value.ToShortDateString(), txtTotalAmount.Text.Trim(),
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

                clsEmail.lChequeEntry(strLetPerson, txtMemNationalId.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), txtChqRequisitionID.Text.Trim(),
                                    txtPayee.Text.Trim(), txtChqNo.Text.Trim(), txtRequestedBy.Text.Trim(), dtpRequestDate.Value.ToShortDateString(), txtTotalAmount.Text.Trim(),
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

                clsEmail.lChequeApproval(strLetPerson, txtMemNationalId.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), txtChqRequisitionID.Text.Trim(),
                                     txtPayee.Text.Trim(), txtChqNo.Text.Trim(), txtRequestedBy.Text.Trim(), dtpRequestDate.Value.ToShortDateString(), txtTotalAmount.Text.Trim(),
                                    strBeneID, strBeneName, "Chairperson", txtName_Chariperson.Text.Trim(), dtpDate_Chariperson.Text.Trim()
                                    , txtRemarks_Chariperson.Text.Trim(), strStatus, "6");

                if (ClsUtility.SendSMS(txtMemNationalId.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), txtMemContactNo.Text.Trim(),
                               txtChqNo.Text.Trim(), txtPayee.Text.Trim(), "Chairperson", strStatus, true) == true)
                {
                    ClsMessage.showMessage("SMS Send to Member contact no.");
                }
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

                clsEmail.lChequeApproval(strLetPerson, txtMemNationalId.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), txtChqRequisitionID.Text.Trim(),
                                      txtPayee.Text.Trim(), txtChqNo.Text.Trim(), txtRequestedBy.Text.Trim(), dtpRequestDate.Value.ToShortDateString(), txtTotalAmount.Text.Trim(),
                                    strBeneID, strBeneName, "Secretary", txtName_Sectretary.Text.Trim(), dtpDate_Sectretary.Text.Trim()
                                    , txtRemarks_Sectretary.Text.Trim(), strStatus, "7");
                if (ClsUtility.SendSMS(txtMemNationalId.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), txtMemContactNo.Text.Trim(),
                               txtChqNo.Text.Trim(), txtPayee.Text.Trim(), "Secretary", strStatus, true) == true)
                {
                    ClsMessage.showMessage("SMS Send to Member contact no.");
                }
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

                clsEmail.lChequeApproval(strLetPerson, txtMemNationalId.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), txtChqRequisitionID.Text.Trim(),
                                      txtPayee.Text.Trim(), txtChqNo.Text.Trim(), txtRequestedBy.Text.Trim(), dtpRequestDate.Value.ToShortDateString(), txtTotalAmount.Text.Trim(),
                                    strBeneID, strBeneName, "Treasurer", txtName_Treasurer.Text.Trim(), dtpDate_Treasurer.Text.Trim()
                                    , txtRemarks_Treasurer.Text.Trim(), strStatus, "8");
                if (ClsUtility.SendSMS(txtMemNationalId.Text.Trim(), txtMemberID.Text.Trim(), txtMemberName.Text.Trim(), txtMemContactNo.Text.Trim(), 
                                txtChqNo.Text.Trim(), txtPayee.Text.Trim(), "Treasurer",strStatus, true)==true)
                {
                    ClsMessage.showMessage("SMS Send to Member contact no.");
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private bool lIsEdit(string strType)
        {

            try
            {

                string strClaimid = "0";
                if (string.IsNullOrEmpty(txtChqRequisitionID.Text.Trim()) == false) { strClaimid = txtChqRequisitionID.Text.Trim(); }
                strSqlQuery = "SELECT IsNull(ChqStatusNo,0) ChqStatusNo, ISNULL(ce.ChqStatus, '')  ChqStatus FROM SNAT.dbo.T_ChequeEntry ce WHERE  id=" + strClaimid + "";
                DataTable dtCheck = new DataTable();
                dtCheck = ClsDataLayer.GetDataTable(strSqlQuery);
                if (dtCheck != null && dtCheck.DefaultView.Count > 0)
                {
                    switch (dtCheck.DefaultView[0]["ChqStatusNo"].ToString())

                    {
                        case "5":
                            ClsMessage.ProjectExceptionMessage("Treasury approved/rejected this claim." + Environment.NewLine + "Cannot " + strType + " record!!");
                            return false;
                        case "3":
                            if (dtCheck.DefaultView[0]["ChqStatus"].ToString().ToUpper() == "CR")
                            {
                                ClsMessage.ProjectExceptionMessage("Chairperson rejected this cheque." + Environment.NewLine + "Cannot " + strType + " record!!");
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        case "4":
                            if (dtCheck.DefaultView[0]["ChqStatus"].ToString().ToUpper() == "SR")
                            {
                                ClsMessage.ProjectExceptionMessage("Secretary rejected this cheque." + Environment.NewLine + "Cannot " + strType + " record!!");
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
        private bool lIsIssue()
        {

            try
            {

                string strClaimid = "0";
                if (string.IsNullOrEmpty(txtChqRequisitionID.Text.Trim()) == false) { strClaimid = txtChqRequisitionID.Text.Trim(); }
                strSqlQuery = "SELECT IsNull(ChqStatusNo,0) ChqStatusNo, ISNULL(ce.ChqStatus, '')  ChqStatus FROM SNAT.dbo.T_ChequeEntry ce WHERE  id=" + strClaimid + "";
                DataTable dtCheck = new DataTable();
                dtCheck = ClsDataLayer.GetDataTable(strSqlQuery);
                if (dtCheck != null && dtCheck.DefaultView.Count > 0)
                {
                    switch (dtCheck.DefaultView[0]["ChqStatusNo"].ToString())

                    {
                        case "6":

                            ClsMessage.ProjectExceptionMessage("Cheque already issued and received" + Environment.NewLine + "Cannot posted record!!");
                            return false;

                        case "5":
                            if (dtCheck.DefaultView[0]["ChqStatus"].ToString().ToUpper() == "TR")
                            {
                                ClsMessage.ProjectExceptionMessage("Treasury rejected this cheque." + Environment.NewLine + "Cannot posted record!!");
                                return false;
                            }
                            else
                            {
                                return true;
                            }

                        case "3":
                            if (dtCheck.DefaultView[0]["ChqStatus"].ToString().ToUpper() == "CR")
                            {
                                ClsMessage.ProjectExceptionMessage("Chairperson rejected this cheque." + Environment.NewLine + "Cannot posted  record!!");
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        case "4":
                            if (dtCheck.DefaultView[0]["ChqStatus"].ToString().ToUpper() == "SR")
                            {
                                ClsMessage.ProjectExceptionMessage("Secretary rejected this cheque." + Environment.NewLine + "Cannot posted record!!");
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
