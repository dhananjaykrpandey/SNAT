using SNAT.Comman_Classes;
using SNAT.Comman_Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SNAT.Document
{
    public partial class frmBenificiryDocumentUpload : Form
    {
        DataTable dtMemberList = new DataTable();
        DataTable dtBeneficiryList = new DataTable();
        DataTable dtBeneficiryDocument = new DataTable();
        BindingSource bsBeneficiryList = new BindingSource();
        string strSqlQuery = "";
        DataSet mdsCreateDataView = new DataSet();
        ErrorProvider err = new ErrorProvider();
        public frmBenificiryDocumentUpload()
        {
            InitializeComponent();
            BindBeneficiry();
        }
        private void SetEnable(bool lValue)
        {

            txtNationalId.Enabled = lValue;
            toolStipChildBar.Enabled = lValue;
            btnPickMember.Enabled = lValue;
            txtBeneficiryNationalId.Enabled = lValue;
            btnPickBeneficiry.Enabled = lValue;

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
            btnDelete.Enabled = !lValue;
            btnSearch.Enabled = !lValue;
            btnPrint.Enabled = !lValue;
            grdDocList.ReadOnly = !lValue;
            grdList.Enabled = !lValue;
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
        void FillMember()
        {
            try
            {
                strSqlQuery = "SELECT DISTINCT tmd.nationalid,tmd.memberid,tmd.membername  FROM SNAT.dbo.T_MemberDocuments tmd";
                dtMemberList = ClsDataLayer.GetDataTable(strSqlQuery);
                //bsBeneficiryList.DataSource = dtBeneficiryList.DefaultView;
                //bindingNavigatorMain.BindingSource = bsBeneficiryList;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        void FillBeneficiry()
        {
            try
            {
                strSqlQuery = " SELECT Distinct nationalid, memberid, membername, beneficirynationalid, beneficiaryname FROM  SNAT.dbo.T_BeneficiryDocuments AS tbd";
                dtBeneficiryList = ClsDataLayer.GetDataTable(strSqlQuery);
                bsBeneficiryList.DataSource = dtBeneficiryList.DefaultView;
                bindingNavigatorMain.BindingSource = bsBeneficiryList;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        void FillBeneficiryDocument(string iNationalID, string iBeneficiryNationalID)
        {
            try
            {
                dtBeneficiryDocument = new DataTable();
                strSqlQuery = " SELECT tbd.id, tbd.nationalid, tbd.memberid, tbd.membername, tbd.beneficirynationalid, tbd.beneficiaryname, tbd.doccode,mdt.name docName, tbd.docLocation," +
                              " tbd.docUploaded, tbd.createdby, tbd.createddate, tbd.updatedby, tbd.updateddate,'Upload Document' UploadDoc,'Preview' Preview," +
                              "tbd.docUploaded docAttached FROM SNAT.dbo.T_BeneficiryDocuments AS tbd LEFT OUTER JOIN dbo.M_DocumentType mdt ON mdt.code=tbd.doccode" +
                              " Where nationalid='" + iNationalID + "' and beneficirynationalid='"+ iBeneficiryNationalID + "'";
                dtBeneficiryDocument = ClsDataLayer.GetDataTable(strSqlQuery);
                grdDocList.DataSource = dtBeneficiryDocument.DefaultView;
                dtBeneficiryDocument.AcceptChanges();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        void BindBeneficiry()
        {
            try
            {
                FillBeneficiry();
                txtNationalId.DataBindings.Add("Text", bsBeneficiryList, "nationalid", false, DataSourceUpdateMode.OnPropertyChanged);
                txtMemberID.DataBindings.Add("Text", bsBeneficiryList, "memberid", false, DataSourceUpdateMode.OnPropertyChanged);
                txtMemberName.DataBindings.Add("Text", bsBeneficiryList, "membername", false, DataSourceUpdateMode.OnPropertyChanged);
                txtBeneficiryNationalId.DataBindings.Add("Text", bsBeneficiryList, "beneficirynationalid", false, DataSourceUpdateMode.OnPropertyChanged);
                txtBeneficiryName.DataBindings.Add("Text", bsBeneficiryList, "beneficiaryname", false, DataSourceUpdateMode.OnPropertyChanged);

                grdList.DataSource = bsBeneficiryList;

                int iRow = 0;
                if (dtBeneficiryList != null && dtBeneficiryList.DefaultView.Count > 0)
                {
                    iRow = bsBeneficiryList.Position;
                    string strnationalid = string.IsNullOrEmpty(dtBeneficiryList.DefaultView[iRow]["nationalid"].ToString()) == true ? "" : dtBeneficiryList.DefaultView[iRow]["nationalid"].ToString();
                    string strbeneficirynationalid = string.IsNullOrEmpty(dtBeneficiryList.DefaultView[iRow]["beneficirynationalid"].ToString()) == true ? "" : dtBeneficiryList.DefaultView[iRow]["beneficirynationalid"].ToString();
                    if (strnationalid != "" && strbeneficirynationalid != "")
                    {
                        FillBeneficiryDocument(strnationalid, strbeneficirynationalid);
                    }
                    else
                    {
                        FillBeneficiryDocument("0", "0");
                    }
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void frmBenificiryDocumentUpload_Load(object sender, EventArgs e)
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


        private void btnPickMember_Click(object sender, EventArgs e)
        {
            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT DISTINCT nationalid, memberid, membername FROM SNAT.dbo.T_MemberDocuments";
                frmsrch.infSqlWhereCondtion = "";
                frmsrch.infSqlOrderBy = " nationalid, memberid, membername";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Member Document ....";
                frmsrch.infCodeFieldName = "nationalid";
                frmsrch.infDescriptionFieldName = "memberid";
                frmsrch.infGridFieldName = " nationalid, memberid, membername";
                frmsrch.infGridFieldCaption = "National id , Member ID , Member Name";
                frmsrch.infGridFieldSize = "100,100,200";
                frmsrch.ShowDialog(this);
                if (frmsrch.DialogResult == DialogResult.OK && frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()))
                {
                    DataView dvDesg = new DataView();
                    dvDesg = frmsrch.infSearchReturnDataView;
                    dvDesg.RowFilter = "lSelect=1";
                    txtNationalId.Text = string.IsNullOrEmpty(dvDesg[0]["nationalid"].ToString()) == true ? "" : dvDesg[0]["nationalid"].ToString();
                    txtMemberID.Text = string.IsNullOrEmpty(dvDesg[0]["memberid"].ToString()) == true ? "" : dvDesg[0]["memberid"].ToString();
                    txtMemberName.Text = string.IsNullOrEmpty(dvDesg[0]["membername"].ToString()) == true ? "" : dvDesg[0]["membername"].ToString();

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtNationalId_Validating(object sender, CancelEventArgs e)
        {


            try
            {
                if (!string.IsNullOrEmpty(txtNationalId.Text.Trim()))
                {
                    string strSql = "SELECT id , nationalid , memberid , employeeno , tscno , membername FROM SNAT.dbo.T_Member AS ted where nationalid='" + txtNationalId.Text.Trim() + "' ";
                    DataTable dtMemberid = new DataTable();
                    dtMemberid = ClsDataLayer.GetDataTable(strSql);
                    if (dtMemberid != null && dtMemberid.DefaultView.Count > 0)
                    {
                        txtMemberID.Text = string.IsNullOrEmpty(dtMemberid.DefaultView[0]["memberid"].ToString()) == true ? "" : dtMemberid.DefaultView[0]["memberid"].ToString();
                        txtMemberName.Text = string.IsNullOrEmpty(dtMemberid.DefaultView[0]["membername"].ToString()) == true ? "" : dtMemberid.DefaultView[0]["membername"].ToString();

                    }
                    else
                    {
                        ClsMessage.showMessage("Invalid member national id");
                        e.Cancel = true;
                        txtMemberID.Text = "";
                        txtMemberName.Text = "";


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
            if (string.IsNullOrEmpty(txtNationalId.Text.Trim())) { ClsMessage.showMessage("Please select member national id. "); txtNationalId.Focus(); return; }
            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT membernationalid , memberid , membername , beneficiarynatioanalid , beneficiaryname , lstatus FROM SNAT.dbo.T_Beneficiary";
                frmsrch.infSqlWhereCondtion = " membernationalid='"+ txtNationalId.Text.Trim() +"'";
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
                    txtBeneficiryNationalId.Text = frmsrch.infCodeFieldText.Trim();
                    txtBeneficiryName.Text = frmsrch.infDescriptionFieldText.Trim();

                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtBeneficiryNationalId_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBeneficiryNationalId.Text.Trim()))
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Beneficiary", "beneficiarynatioanalid", "beneficiarynatioanalid", txtBeneficiryNationalId.Text.Trim(), txtBeneficiryName, "beneficiaryname") == false)
                    {

                        ClsMessage.showMessage("Invalid beneficiary national id.");
                        txtBeneficiryName.Text = "";
                        e.Cancel = true;
                        return;
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
            try
            {
                FillBeneficiry();
                FillBeneficiryDocument(txtNationalId.Text.Trim(), txtBeneficiryNationalId.Text.Trim());
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);

            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

            txtNationalId.Text = "";
            txtMemberName.Text = "";
            txtMemberID.Text = "";
            txtBeneficiryNationalId.Text = "";
            txtBeneficiryName.Text = "";
            FillBeneficiryDocument("0", "0");
            ClsUtility.FormMode = ClsUtility.enmFormMode.AddMode;
            SetFormMode(ClsUtility.enmFormMode.AddMode);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalId.Text.Trim()))
            {
                err.SetError(txtNationalId, "Please enter/select member national id.");
                txtNationalId.Focus();
                return;
            }
            
            if (string.IsNullOrEmpty(txtBeneficiryNationalId.Text.Trim()))
            {
                err.SetError(txtNationalId, "Please enter/select beneficiary national id.");
                txtBeneficiryNationalId.Focus();
                return;
            }
            else
            {

                ClsUtility.FormMode = ClsUtility.enmFormMode.EditMode;
                SetFormMode(ClsUtility.enmFormMode.EditMode);
                txtNationalId.Enabled = false;
                btnPickMember.Enabled = false;
                txtBeneficiryNationalId.Enabled = false;
                btnPickBeneficiry.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dtBeneficiryList != null && dtBeneficiryList.DefaultView.Count > 0)
            {
                if (!string.IsNullOrEmpty(txtNationalId.Text.Trim()))
                {
                    if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
                    {


                        strSqlQuery = "Delete from SNAT.dbo.T_BeneficiryDocuments where nationalid='" + txtNationalId.Text.Trim() + "' and beneficirynationalid='" + txtBeneficiryNationalId.Text.Trim() + "'";
                        int iResult = ClsDataLayer.UpdateData(strSqlQuery);
                        if (iResult > 0)
                        {
                            ClsMessage.showDeleteMessage();
                           // FillMember();
                            FillBeneficiry();
                            string strnationalid = string.IsNullOrEmpty(txtNationalId.Text.Trim()) == true ? "0" : txtNationalId.Text.Trim();
                            string strBeneficiryid = string.IsNullOrEmpty(txtBeneficiryNationalId.Text.Trim()) == true ? "0" : txtBeneficiryNationalId.Text.Trim();
                            FillBeneficiryDocument(strnationalid, strBeneficiryid);
                        }
                        else
                        {
                            ClsMessage.showMessage("Some problem occurs while deleting please contact system administrator.");
                        }
                    }
                }
                else
                {
                    ClsMessage.ProjectExceptionMessage("Please select member national id.");
                    return;
                }
            }
            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
            SetFormMode(ClsUtility.enmFormMode.NormalMode);
        }

        bool ValidateSave()
        {
            try
            {
                err.Clear();

                if (string.IsNullOrEmpty(txtNationalId.Text.Trim()))
                {
                    err.SetError(txtNationalId, "Please enter/select member national id.");
                    txtNationalId.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(txtBeneficiryNationalId.Text.Trim()))
                {
                    err.SetError(txtBeneficiryNationalId, "Please enter/select beneficiary national id.");
                    txtBeneficiryNationalId.Focus();
                    return false;
                }


                if (dtBeneficiryDocument != null && dtBeneficiryDocument.DefaultView.Count > 0)
                {
                    DataView dvMemberDoc = new DataView();
                    dvMemberDoc = mdsCreateDataView.DefaultViewManager.CreateDataView(dtBeneficiryDocument);
                    dvMemberDoc.RowFilter = "isNull(docAttached,0)=0";
                    if (dvMemberDoc.Count > 0)
                    {
                        string strDoc = "";
                        foreach (DataRowView drvMemDocRow in dvMemberDoc)
                        {
                            strDoc = strDoc + Environment.NewLine + drvMemDocRow["docName"].ToString();
                        }

                        ClsMessage.ProjectExceptionMessage("No document attached!!" + strDoc + Environment.NewLine + "Please attached beneficiary supported document.");
                        return false;
                    }
                    //foreach(var drvMemDoc)

                }
                else
                {
                    ClsMessage.ProjectExceptionMessage("No document attached!!" + Environment.NewLine + "Please attached beneficiary supported document.");
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

                if (dtBeneficiryDocument != null && dtBeneficiryDocument.DefaultView.Count > 0)
                {
                    //DataView dvMemberDoc = new DataView();
                    //dvMemberDoc = mdsCreateDataView.DefaultViewManager.CreateDataView(dtMemberDocument);
                    dtBeneficiryDocument.DefaultView.RowFilter = "isNull(docAttached,0)=1";
                    if (dtBeneficiryDocument.DefaultView.Count > 0)
                    {

                        string strDoc = "";
                        foreach (DataRowView drvMemDocRow in dtBeneficiryDocument.DefaultView)
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
                                drvMemDocRow.EndEdit();

                            }
                        }

                    }
                    dtBeneficiryDocument.DefaultView.RowFilter = "";

                   
                    if (dtBeneficiryDocument.GetChanges() != null)
                    {

                        bool lReturn = false;
                        strSqlQuery = "SELECT id, nationalid, memberid, membername, beneficirynationalid, beneficiaryname, doccode, docLocation, docUploaded, createdby, createddate, updatedby, updateddate FROM SNAT.dbo.T_BeneficiryDocuments (nolock) where 1=2 ";
                        lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtBeneficiryDocument);
                        dtBeneficiryDocument.AcceptChanges();

                        if (lReturn == true)
                        {
                            ClsMessage.showSaveMessage();
                            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                            SetFormMode(ClsUtility.enmFormMode.NormalMode);
                          //  FillMember();
                            FillBeneficiry();
                            FillBeneficiryDocument(txtNationalId.Text.Trim(),txtBeneficiryNationalId.Text.Trim());
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
                if (ClsMessage.showAskDiscardMessage() == DialogResult.Yes)
                {
                    dtBeneficiryDocument.RejectChanges();
                    if (dtMemberList != null && dtMemberList.DefaultView.Count > 0)
                    {
                        bsBeneficiryList.Position = 0;
                    }
                    else
                    {
                        txtMemberID.Text = "";
                        txtMemberName.Text = "";
                        txtNationalId.Text = "";
                    }


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
                frmsrch.infSqlSelectQuery = "SELECT DISTINCT nationalid, memberid, membername FROM SNAT.dbo.T_MemberDocuments";
                frmsrch.infSqlWhereCondtion = "";
                frmsrch.infSqlOrderBy = " nationalid, memberid, membername";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Member Document ....";
                frmsrch.infCodeFieldName = "nationalid";
                frmsrch.infDescriptionFieldName = "memberid";
                frmsrch.infGridFieldName = " nationalid, memberid, membername";
                frmsrch.infGridFieldCaption = "National id , Member ID , Member Name";
                frmsrch.infGridFieldSize = "100,100,200";
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
                        int iRow = bsBeneficiryList.Find("nationalid", frmsrch.infCodeFieldText.Trim());
                        bsBeneficiryList.Position = iRow;
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

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
   
    string CopyDocument(string DocLocation, string docCode)
    {
        try
        {
        CopyDoc:
            if (Directory.Exists(ClsSettings.strSNATAttachedDocumentDetails + @"\" + txtBeneficiryNationalId.Text.Trim()))
            {

                string Fileext = Path.GetExtension(DocLocation);
                string FileFinalLocation = ClsSettings.strSNATAttachedDocumentDetails + @"\" + txtBeneficiryNationalId.Text.Trim()
                                           + "\\" + txtBeneficiryNationalId.Text.Trim() + "_" + docCode + Fileext;

                if (File.Exists(FileFinalLocation))
                {
                    string tempLoc = Path.GetTempPath() + txtBeneficiryNationalId.Text.Trim() + "_" + docCode + Fileext;
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
                Directory.CreateDirectory(ClsSettings.strSNATAttachedDocumentDetails + @"\" + txtBeneficiryNationalId.Text.Trim());
                goto CopyDoc;

            }
        }
        catch (Exception ex)
        {

            ClsMessage.ProjectExceptionMessage(ex);
            return null;
        }
    }

        private void btnChildAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNationalId.Text.Trim()))
                {
                    ClsMessage.showMessage("Please select/enter member national id.");
                    txtNationalId.Focus();
                    return;
                }

                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT  id, code, name FROM  SNAT.dbo.M_DocumentType AS mdt";
                frmsrch.infSqlWhereCondtion = " isnull(status,0)=1";
                frmsrch.infSqlOrderBy = " id, code, name";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Document Type ....";
                frmsrch.infCodeFieldName = "code";
                frmsrch.infDescriptionFieldName = "name";
                frmsrch.infGridFieldName = " id, code, name";
                frmsrch.infGridFieldCaption = " id, Document Code, Document Name";
                frmsrch.infGridFieldSize = "0,100,200";
                frmsrch.ShowDialog(this);
                if (frmsrch.DialogResult == DialogResult.OK && frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()))
                {
                    DataView dvDesg = new DataView();
                    dvDesg = frmsrch.infSearchReturnDataView;
                    dvDesg.RowFilter = "lSelect=1";
                    DataRow dRow = dtBeneficiryDocument.NewRow();
                    dRow["nationalid"] = txtNationalId.Text.Trim();
                    dRow["memberid"] = txtMemberID.Text.Trim();
                    dRow["membername"] = txtMemberName.Text.Trim();
                    dRow["beneficirynationalid"] = txtBeneficiryNationalId.Text.Trim();
                    dRow["beneficiaryname"] = txtBeneficiryName.Text.Trim();
                    dRow["doccode"] = frmsrch.infCodeFieldText.Trim();
                    dRow["docName"] = frmsrch.infDescriptionFieldText.Trim();
                    dRow["docLocation"] = "";
                    dRow["docUploaded"] = false;
                    dRow["createdby"] = ClsSettings.username;
                    dRow["createddate"] = DateTime.Now;
                    dRow["updatedby"] = ClsSettings.username;
                    dRow["updateddate"] = DateTime.Now;
                    dRow["UploadDoc"] = "Upload Document";
                    dRow["Preview"] = "Preview";
                    dRow["docAttached"] = false;
                    //dRow.SetAdded();
                    dtBeneficiryDocument.Rows.Add(dRow);
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
                if (dtBeneficiryDocument != null && dtBeneficiryDocument.DefaultView.Count > 0)
                {
                    if (grdDocList.SelectedRows.Count > 0)
                    {
                        if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
                        {

                            int iSelectedRow = grdDocList.CurrentRow.Index;
                            dtBeneficiryDocument.DefaultView.Delete(iSelectedRow);
                        }
                    }
                    else
                    {
                        ClsMessage.showMessage("Please select document row to delete.");
                        return;
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
                if (ClsMessage.showAskDiscardMessage() == DialogResult.Yes)
                {
                    dtBeneficiryDocument.RejectChanges();
                   
                    FillBeneficiryDocument(string.IsNullOrEmpty(txtNationalId.Text.Trim()) == true ? "0" : txtNationalId.Text.Trim(),
                                           string.IsNullOrEmpty(txtBeneficiryNationalId.Text.Trim()) == true ? "0" : txtBeneficiryNationalId.Text.Trim() );
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void MasterTemplate_CommandCellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                if (dtBeneficiryDocument != null && dtBeneficiryDocument.DefaultView.Count > 0)
                {
                    if (ClsUtility.FormMode != ClsUtility.enmFormMode.NormalMode)
                    {


                        if (e.Column.FieldName.ToUpper() == "UploadDoc".ToUpper())
                        {
                            OpenFileDialog opdlg = new OpenFileDialog();
                            opdlg.Filter = "Portable document file (*.pdf)|*.pdf|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"; ;
                            if (opdlg.ShowDialog() == DialogResult.OK)
                            {
                                string filelocation = opdlg.FileName;
                                dtBeneficiryDocument.DefaultView[e.RowIndex].BeginEdit();
                                dtBeneficiryDocument.DefaultView[e.RowIndex]["docAttached"] = true;
                                dtBeneficiryDocument.DefaultView[e.RowIndex]["docLocation"] = filelocation;
                                dtBeneficiryDocument.DefaultView[e.RowIndex].EndEdit();
                            }
                        }
                    }
                    if (e.Column.FieldName.ToUpper() == "Preview".ToUpper())
                    {
                        if (dtBeneficiryDocument.DefaultView[e.RowIndex]["docLocation"] != null &&
                            dtBeneficiryDocument.DefaultView[e.RowIndex]["docLocation"].ToString().Trim() != "")
                        {
                            Process.Start(dtBeneficiryDocument.DefaultView[e.RowIndex]["docLocation"].ToString().Trim());
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void MasterTemplate_SelectionChanging(object sender, Telerik.WinControls.UI.GridViewSelectionCancelEventArgs e)
        {
            try
            {
                if (dtMemberList != null && dtMemberList.DefaultView.Count > 0)
                {
                
                    //FillBeneficiryDocument(string.IsNullOrEmpty(txtNationalId.Text.Trim()) == true ? "0" : txtNationalId.Text.Trim(),
                    //                     string.IsNullOrEmpty(txtBeneficiryNationalId.Text.Trim()) == true ? "0" : txtBeneficiryNationalId.Text.Trim());
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

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

       
    }
}
