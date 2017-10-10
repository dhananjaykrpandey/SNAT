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
    public partial class frmMemberDocumentUpload : Form
    {
        DataTable dtMemberList = new DataTable();
        DataTable dtMemberDocument = new DataTable();
        BindingSource bsMemberList = new BindingSource();
        string strSqlQuery = "";
        DataSet mdsCreateDataView = new DataSet();
        ErrorProvider err = new ErrorProvider();
        public frmMemberDocumentUpload()
        {
            InitializeComponent();
            BindMember();
        }
        private void SetEnable(bool lValue)
        {

            //txtMemberID.Enabled = lValue;
            //txtMemberName.Enabled = lValue;
            txtNationalId.Enabled = lValue;
            grdDocList.ReadOnly = !lValue;
            toolStipChildBar.Enabled = lValue;
            btnPickMember.Enabled = lValue;
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
            btnRefresh.Enabled = !lValue;
            btnDelete.Enabled = !lValue;
            grdList.Enabled = !lValue;



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
                bsMemberList.DataSource = dtMemberList.DefaultView;
                bindingNavigatorMain.BindingSource = bsMemberList;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        void FillMemberDocument(decimal iNationalID)
        {
            try
            {
                dtMemberDocument = new DataTable();
                strSqlQuery = " SELECT tmd.id , tmd.nationalid , tmd.memberid , tmd.membername , tmd.doccode ,mdt.name docName, tmd.docLocation,tmd.docUploaded" +
                              " , tmd.createdby , tmd.createddate , tmd.updatedby , tmd.updateddate,'Upload Document' UploadDoc,'Preview' Preview,tmd.docUploaded docAttached" +
                              " FROM SNAT.dbo.T_MemberDocuments tmd LEFT OUTER JOIN dbo.M_DocumentType mdt ON mdt.code=tmd.doccode" +
                              " Where nationalid='" + iNationalID + "'";
                dtMemberDocument = ClsDataLayer.GetDataTable(strSqlQuery);

                //DataColumn dcdocReadLocation = new DataColumn("docAttached", typeof(bool));
                //dcdocReadLocation.DefaultValue = false;
                //dtMemberDocument.Columns.Add(dcdocReadLocation);

                grdDocList.DataSource = dtMemberDocument.DefaultView;
                dtMemberDocument.AcceptChanges();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        void BindMember()
        {
            try
            {
                FillMember();
                txtNationalId.DataBindings.Add("Text", bsMemberList, "nationalid", false, DataSourceUpdateMode.OnPropertyChanged);
                txtMemberID.DataBindings.Add("Text", bsMemberList, "memberid", false, DataSourceUpdateMode.OnPropertyChanged);
                txtMemberName.DataBindings.Add("Text", bsMemberList, "membername", false, DataSourceUpdateMode.OnPropertyChanged);
                grdList.DataSource = bsMemberList;

                int iRow = 0;
                if (dtMemberList != null && dtMemberList.DefaultView.Count > 0)
                {
                    iRow = bsMemberList.Position;
                    string strnationalid = string.IsNullOrEmpty(dtMemberList.DefaultView[iRow]["nationalid"].ToString()) == true ? "" : dtMemberList.DefaultView[iRow]["nationalid"].ToString();
                    if (strnationalid != "")
                    {
                        FillMemberDocument(Convert.ToDecimal(strnationalid));
                    }
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void frmMemberDocumentUpload_Load(object sender, EventArgs e)
        {

            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
            SetFormMode(ClsUtility.enmFormMode.NormalMode);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            txtNationalId.Text = "";
            txtMemberName.Text = "";
            txtMemberID.Text = "";
            FillMemberDocument(0);
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
            else
            {

                ClsUtility.FormMode = ClsUtility.enmFormMode.EditMode;
                SetFormMode(ClsUtility.enmFormMode.EditMode);
                txtNationalId.Enabled = false;
                btnPickMember.Enabled = false;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dtMemberList != null && dtMemberList.DefaultView.Count > 0)
            {
                if (!string.IsNullOrEmpty(txtNationalId.Text.Trim()))
                {
                    if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
                    {


                        strSqlQuery = "Delete from SNAT.dbo.T_MemberDocuments where nationalid='" + txtNationalId.Text.Trim() + "'";
                        int iResult = ClsDataLayer.UpdateData(strSqlQuery);
                        if (iResult > 0)
                        {
                            ClsMessage.showDeleteMessage();
                            FillMember();
                            decimal strnationalid = string.IsNullOrEmpty(txtNationalId.Text.Trim()) == true ? 0 : Convert.ToDecimal(txtNationalId.Text.Trim());
                            FillMemberDocument(strnationalid);
                        }
                        else
                        {
                            ClsMessage.showMessage("Some problem occurs while deleting please contact system administrator.", MessageBoxIcon.Information);
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

                if (dtMemberDocument != null && dtMemberDocument.DefaultView.Count > 0)
                {
                    DataView dvMemberDoc = new DataView();
                    dvMemberDoc = mdsCreateDataView.DefaultViewManager.CreateDataView(dtMemberDocument);
                    dvMemberDoc.RowFilter = "isNull(docAttached,0)=0";
                    if (dvMemberDoc.Count > 0)
                    {
                        string strDoc = "";
                        foreach (DataRowView drvMemDocRow in dvMemberDoc)
                        {
                            strDoc = strDoc + Environment.NewLine + drvMemDocRow["docName"].ToString();
                        }

                        ClsMessage.ProjectExceptionMessage("No document attached!!" + strDoc + Environment.NewLine + "Please attached member supported document.");
                        return false;
                    }
                    //foreach(var drvMemDoc)

                }
                else
                {
                    ClsMessage.ProjectExceptionMessage("No document attached!!" + Environment.NewLine + "Please attached member supported document.");
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

                if (dtMemberDocument != null && dtMemberDocument.DefaultView.Count > 0)
                {
                    //DataView dvMemberDoc = new DataView();
                    //dvMemberDoc = mdsCreateDataView.DefaultViewManager.CreateDataView(dtMemberDocument);
                    dtMemberDocument.DefaultView.RowFilter = "isNull(docAttached,0)=1";
                    if (dtMemberDocument.DefaultView.Count > 0)
                    {

                        string strDoc = "";
                        foreach (DataRowView drvMemDocRow in dtMemberDocument.DefaultView)
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
                    dtMemberDocument.DefaultView.RowFilter = "";

                    //if (string.IsNullOrEmpty(txtImageLocation.Text.Trim()) == false)
                    //{
                    //    if (File.Exists(txtImageLocation.Text.Trim()))
                    //    {
                    //        string imagenewlocation = Path.GetFullPath(txtImageLocation.Text.Trim());
                    //        string imageext = Path.GetExtension(txtImageLocation.Text.Trim());

                    //        dtMemberDocument.DefaultView[iRow]["imagelocation"] = ClsSettings.strEmployeeImageFolder + @"\" + txtBeneficiaryNationalId.Text.Trim() + imageext;
                    //        imageFinalLocation = ClsSettings.strEmployeeImageFolder + @"\" + txtBeneficiaryNationalId.Text.Trim() + imageext;
                    //        CopyEmployeeImage(imageFinalLocation);
                    //    }

                    //}
                    //if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                    //{

                    //    dtMemberDocument.DefaultView[iRow]["createdby"] = ClsSettings.username;
                    //    dtMemberDocument.DefaultView[iRow]["createddate"] = DateTime.Now.ToString();
                    //}
                    //dtMemberDocument.DefaultView[iRow]["updateby"] = ClsSettings.username;
                    //dtMemberDocument.DefaultView[iRow]["updateddate"] = DateTime.Now.ToString();
                    //dtMemberDocument.DefaultView[iRow]["lstatus"] = chkStatus.Checked;
                    //dtMemberDocument.DefaultView[iRow].EndEdit();
                    if (dtMemberDocument.GetChanges() != null)
                    {

                        bool lReturn = false;
                        strSqlQuery = "SELECT id, nationalid, memberid, membername, doccode, docLocation, docUploaded, createdby, createddate, updatedby, updateddate FROM SNAT.dbo.T_MemberDocuments (nolock) where 1=2 ";
                        lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtMemberDocument);
                        dtMemberDocument.AcceptChanges();

                        if (lReturn == true)
                        {
                            ClsMessage.showSaveMessage();
                            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                            SetFormMode(ClsUtility.enmFormMode.NormalMode);
                            FillMember();
                            FillMemberDocument(Convert.ToDecimal(txtNationalId.Text.Trim()));
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
                    dtMemberDocument.RejectChanges();
                    if (dtMemberList != null && dtMemberList.DefaultView.Count > 0)
                    {
                        bsMemberList.Position = 0;
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
                        int iRow = bsMemberList.Find("nationalid", frmsrch.infCodeFieldText.Trim());
                        bsMemberList.Position = iRow;
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

        private void btnPickMember_Click(object sender, EventArgs e)
        {

            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT id , nationalid , memberid , employeeno , tscno , membername FROM SNAT.dbo.T_Member AS ted";
                frmsrch.infSqlWhereCondtion = " Not Exists ( SELECT DISTINCT tmd.id,tmd.nationalid,tmd.memberid,tmd.membername  FROM SNAT.dbo.T_MemberDocuments tmd where tmd.nationalid=ted.nationalid)";
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
                    txtNationalId.Text = frmsrch.infCodeFieldText.Trim();
                    txtMemberName.Text = string.IsNullOrEmpty(dvDesg[0]["membername"].ToString()) == true ? "" : dvDesg[0]["membername"].ToString();
                    txtMemberID.Text = string.IsNullOrEmpty(dvDesg[0]["memberid"].ToString()) == true ? "" : dvDesg[0]["memberid"].ToString();
                    //txtMemberid.Tag = string.IsNullOrEmpty(dvDesg[0]["employeeno"].ToString()) == true ? "" : dvDesg[0]["employeeno"].ToString();
                    //if (string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) == false)
                    //{
                    //    int iRow = bsMemmberEntery.Find("nationalid", frmsrch.infCodeFieldText.Trim());
                    //    bsMemmberEntery.Position = iRow;
                    //    dvDesg.RowFilter = "";
                    //}
                    FillMemberDocument(Convert.ToDecimal(txtNationalId.Text.Trim()));
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
                        FillMemberDocument(Convert.ToDecimal(txtNationalId.Text.Trim()));
                    }
                    else
                    {
                        ClsMessage.showMessage("Invalid member national id", MessageBoxIcon.Information);
                        txtNationalId.Focus();
                        txtMemberID.Text = "";
                        txtMemberName.Text = "";
                        FillMemberDocument(0);

                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnChildAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNationalId.Text.Trim()))
                {
                    ClsMessage.showMessage("Please select/enter member national id.", MessageBoxIcon.Information);
                    txtNationalId.Focus();
                    return;
                }

                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT  id, code, name FROM  SNAT.dbo.M_DocumentType AS mdt";
                frmsrch.infSqlWhereCondtion = " isnull(status,0)=1";
                frmsrch.infSqlOrderBy = " id, code, name";
                frmsrch.infMultiSelect = true;
                frmsrch.infSearchFormName = "Search Document Type ....";
                frmsrch.infCodeFieldName = "code";
                frmsrch.infDescriptionFieldName = "name";
                frmsrch.infGridFieldName = " id, code, name";
                frmsrch.infGridFieldCaption = " id, Document Code, Document Name";
                frmsrch.infGridFieldSize = "0,100,200";
                frmsrch.ShowDialog(this);
                if (frmsrch.DialogResult == DialogResult.OK && frmsrch.infSearchReturnDataView != null && frmsrch.infSearchReturnDataView.Count > 0)
                {
                    DataView dvDesg = new DataView();
                    dvDesg = frmsrch.infSearchReturnDataView;
                    dvDesg.RowFilter = "lSelect=1";

                    DataView dvMemberDoc = new DataView();
                    dvMemberDoc = mdsCreateDataView.DefaultViewManager.CreateDataView(dtMemberDocument);


                    foreach (DataRowView item in dvDesg)
                    {
                        string Code = item["Code"] == DBNull.Value ? "" : item["Code"].ToString().Trim();
                        dvMemberDoc.RowFilter = "doccode='" + Code + "'";

                        if (dvMemberDoc.Count <= 0)
                        {                           DataRow dRow = dtMemberDocument.NewRow();
                            dRow["nationalid"] = txtNationalId.Text.Trim();
                            dRow["memberid"] = txtMemberID.Text.Trim();
                            dRow["membername"] = txtMemberName.Text.Trim();
                            dRow["doccode"] = item["Code"] == DBNull.Value ? "" : item["Code"].ToString().Trim();
                            dRow["docName"] = item["name"] == DBNull.Value ? "" : item["name"].ToString().Trim();
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
                            dtMemberDocument.Rows.Add(dRow);
                        }
                        dvMemberDoc.RowFilter = "";
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
                if (dtMemberDocument != null && dtMemberDocument.DefaultView.Count > 0)
                {
                    if (grdDocList.SelectedRows.Count > 0)
                    {
                        if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
                        {

                            int iSelectedRow = grdDocList.CurrentRow.Index;
                            dtMemberDocument.DefaultView.Delete(iSelectedRow);
                        }
                    }
                    else
                    {
                        ClsMessage.showMessage("Please select document row to delete.", MessageBoxIcon.Information);
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
                    dtMemberDocument.RejectChanges();
                    decimal nationalid = string.IsNullOrEmpty(txtNationalId.Text.Trim()) == true ? 0 : Convert.ToDecimal(txtNationalId.Text.Trim());
                    FillMemberDocument(nationalid);
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
                if (dtMemberDocument != null && dtMemberDocument.DefaultView.Count > 0)
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
                                dtMemberDocument.DefaultView[e.RowIndex].BeginEdit();
                                dtMemberDocument.DefaultView[e.RowIndex]["docAttached"] = true;
                                dtMemberDocument.DefaultView[e.RowIndex]["docLocation"] = filelocation;
                                dtMemberDocument.DefaultView[e.RowIndex].EndEdit();
                            }
                        }
                    }
                    if (e.Column.FieldName.ToUpper() == "Preview".ToUpper())
                    {
                        if (dtMemberDocument.DefaultView[e.RowIndex]["docLocation"] != null &&
                            dtMemberDocument.DefaultView[e.RowIndex]["docLocation"].ToString().Trim() != "")
                        {
                            Process.Start(dtMemberDocument.DefaultView[e.RowIndex]["docLocation"].ToString().Trim());
                        }
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
                if (Directory.Exists(ClsSettings.strSNATAttachedDocumentDetails + @"\" + txtMemberID.Text.Trim()))
                {

                    string Fileext = Path.GetExtension(DocLocation);
                    string FileFinalLocation = ClsSettings.strSNATAttachedDocumentDetails + @"\" + txtMemberID.Text.Trim()
                                               + "\\" + txtMemberID.Text.Trim() + "_" + docCode + Fileext;

                    if (File.Exists(FileFinalLocation))
                    {
                        string tempLoc = Path.GetTempPath() + txtMemberID.Text.Trim() + "_" + docCode + Fileext;
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
                    Directory.CreateDirectory(ClsSettings.strSNATAttachedDocumentDetails + @"\" + txtMemberID.Text.Trim());
                    goto CopyDoc;

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return null;
            }
        }

        private void MasterTemplate_SelectionChanging(object sender, Telerik.WinControls.UI.GridViewSelectionCancelEventArgs e)
        {
            try
            {
                if (dtMemberList != null && dtMemberList.DefaultView.Count > 0)
                {
                    decimal iNationalID = string.IsNullOrEmpty(txtNationalId.Text.Trim()) == true ? 0 : Convert.ToDecimal(txtNationalId.Text.Trim());
                    FillMemberDocument(iNationalID);
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                FillMember();
                FillMemberDocument(Convert.ToDecimal(txtNationalId.Text.Trim()));
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex.Message);
            }
        }
    }
}
