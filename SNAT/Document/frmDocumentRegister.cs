using SNAT.Comman_Classes;
using SNAT.Comman_Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SNAT.Document
{
    public partial class frmDocumentRegister : Form
    {
        string strSqlQuery = "";
        DataTable dtMemberList = new DataTable();
        DataTable dtMemberDocument = new DataTable();
        DataTable dtBeneficryDocument = new DataTable();
        DataTable dtBeneficiry = new DataTable();
        DataTable dtFinanceRegister = new DataTable();
        DataTable dtMemPayment = new DataTable();
        public frmDocumentRegister()
        {
            InitializeComponent();
            FillMember();
        }

        private void frmDocumentRegister_Load(object sender, EventArgs e)
        {

        }
        void FillMember()
        {
            try
            {
                strSqlQuery = "SELECT DISTINCT tmd.nationalid,tmd.memberid,tmd.membername  FROM SNAT.dbo.T_MemberDocuments tmd";
                dtMemberList = ClsDataLayer.GetDataTable(strSqlQuery);

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnPickMember_Click(object sender, EventArgs e)
        {

            GetMember("MEM");
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

                if (dtMemberDocument != null && dtMemberDocument.DefaultView.Count <= 0)
                {
                    ClsMessage.ProjectExceptionMessage("Member document not found.");
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
        private void grdMemDoc_CommandCellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                if (dtMemberDocument != null && dtMemberDocument.DefaultView.Count > 0)
                {

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
        private void grdBefDoc_CommandCellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                if (dtBeneficryDocument != null && dtBeneficryDocument.DefaultView.Count > 0)
                {

                    if (e.Column.FieldName.ToUpper() == "dcPreview".ToUpper())
                    {
                        if (dtBeneficryDocument.DefaultView[e.RowIndex]["docLocation"] != null &&
                            dtBeneficryDocument.DefaultView[e.RowIndex]["docLocation"].ToString().Trim() != "")
                        {
                            Process.Start(dtBeneficryDocument.DefaultView[e.RowIndex]["docLocation"].ToString().Trim());
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnMemNationalID_Click(object sender, EventArgs e)
        {
            GetMember("BEN");
        }

        void GetMember(string strValue)
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
                    if (strValue.ToUpper() == "MEM")
                    {
                        DataView dvDesg = new DataView();
                        dvDesg = frmsrch.infSearchReturnDataView;
                        dvDesg.RowFilter = "lSelect=1";
                        txtNationalId.Text = frmsrch.infCodeFieldText.Trim();
                        txtMemberName.Text = string.IsNullOrEmpty(dvDesg[0]["membername"].ToString()) == true ? "" : dvDesg[0]["membername"].ToString();
                        txtMemberID.Text = string.IsNullOrEmpty(dvDesg[0]["memberid"].ToString()) == true ? "" : dvDesg[0]["memberid"].ToString();
                        FillMemberDocument(Convert.ToDecimal(txtNationalId.Text.Trim()));
                    }
                    if (strValue.ToUpper() == "BEN")
                    {
                        DataView dvDesg = new DataView();
                        dvDesg = frmsrch.infSearchReturnDataView;
                        dvDesg.RowFilter = "lSelect=1";
                        txtMemNationalID_BF.Text = frmsrch.infCodeFieldText.Trim();
                        txtMemberName_BF.Text = string.IsNullOrEmpty(dvDesg[0]["membername"].ToString()) == true ? "" : dvDesg[0]["membername"].ToString();
                        txtMemberID_BF.Text = string.IsNullOrEmpty(dvDesg[0]["memberid"].ToString()) == true ? "" : dvDesg[0]["memberid"].ToString();

                        FillBeneficiry(txtMemNationalID_BF.Text);

                    }
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void btnMemNationalID_BF_Validating(object sender, CancelEventArgs e)
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
                        txtMemberID_BF.Text = string.IsNullOrEmpty(dtMemberid.DefaultView[0]["memberid"].ToString()) == true ? "" : dtMemberid.DefaultView[0]["memberid"].ToString();
                        txtMemberName_BF.Text = string.IsNullOrEmpty(dtMemberid.DefaultView[0]["membername"].ToString()) == true ? "" : dtMemberid.DefaultView[0]["membername"].ToString();
                        FillMemberDocument(Convert.ToDecimal(txtNationalId.Text.Trim()));
                    }
                    else
                    {
                        ClsMessage.showMessage("Invalid member national id", MessageBoxIcon.Information);
                        txtNationalId.Focus();
                        txtMemberID_BF.Text = "";
                        txtMemberName_BF.Text = "";
                        FillMemberDocument(0);

                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        void FillBeneficiry(string strMemNationalID)

        {
            try
            {
                dtBeneficiry = new DataTable();
                strSqlQuery = "SELECT id, membernationalid, memberid, membername, beneficiarynatioanalid, beneficiaryname FROM  SNAT.dbo.T_Beneficiary AS tb (nolock) where membernationalid='" + strMemNationalID + "' ";
                dtBeneficiry = ClsDataLayer.GetDataTable(strSqlQuery);
                grdBenfeList.DataSource = dtBeneficiry.DefaultView;
                if (dtBeneficiry != null && dtBeneficiry.DefaultView.Count > 0)
                {
                    FillBeneficiryDocument(dtBeneficiry.DefaultView[0]["beneficiarynatioanalid"].ToString());
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        void FillBeneficiryDocument(string strBeneficiryNationalID)
        {
            try
            {
                dtBeneficryDocument = new DataTable();
                strSqlQuery = @"SELECT  id, nationalid, memberid, membername, beneficirynationalid, beneficiaryname, doccode, docLocation," + Environment.NewLine +
                             " docUploaded,'Preview' dcPreview,REVERSE(LEFT(REVERSE(docLocation),PATINDEX('%\\%',REVERSE(docLocation))-1)) as docname FROM SNAT.dbo.T_BeneficiryDocuments AS tbd (nolock) Where beneficirynationalid='" + strBeneficiryNationalID + "'";
                dtBeneficryDocument = ClsDataLayer.GetDataTable(strSqlQuery);
                grdBenfeDocList.DataSource = dtBeneficryDocument.DefaultView;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void grdBemfList_SelectionChanging(object sender, GridViewSelectionCancelEventArgs e)
        {
            try
            {
                if (dtBeneficiry != null && dtBeneficiry.DefaultView.Count > 0)
                {
                    FillBeneficiryDocument(dtBeneficiry.DefaultView[grdBenfeList.CurrentRow.Index]["beneficiarynatioanalid"].ToString());
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void FillFinanceRegister(string strYear)
        {
            try
            {
                dtFinanceRegister = new DataTable();
                strSqlQuery = " SELECT wageMonthYear, wageFrom,count(memNationalID)TotalUpload, " + Environment.NewLine +
                              " sum(CASE WHEN ISNULL(lValidMemmber, 0) = 0 THEN 0 ELSE 1 END) ivalidMember" + Environment.NewLine +
                              ", sum(CASE WHEN ISNULL(lValidMemmber, 0) = 0 THEN 1 ELSE 0 END) iInvalidMember " + Environment.NewLine +
                              ", sum(CASE WHEN ISNULL(lWagesProcessed, 0) = 0 THEN 0 ELSE 1 END) iWagesProcessed," + Environment.NewLine +
                              " sum(CASE WHEN ISNULL(lApproved, 0) = 0 THEN 0 ELSE 1 END) iApproved,Remarks " + Environment.NewLine +
                              " FROM SNAT.dbo.T_WagesUpload " + Environment.NewLine +
                              " Where RIGHT(wageMonthYear, len(wageMonthYear)-PATINDEX('%-%', wageMonthYear ))='" + strYear + "'" + Environment.NewLine +
                              " GROUP BY wageMonthYear, wageFrom,Remarks";
                dtFinanceRegister = ClsDataLayer.GetDataTable(strSqlQuery);
                grdFinanceList.DataSource = dtFinanceRegister.DefaultView;

                if (dtFinanceRegister != null && dtFinanceRegister.DefaultView.Count > 0)
                {
                    FillMemPaymentList(dtFinanceRegister.DefaultView[0]["wageMonthYear"].ToString());
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void FillMemPaymentList(string strMonthYear)
        {
            try
            {
                strSqlQuery = " SELECT  mwp.memNationalID, mwp.memMemberID, mwp.memEmployeeNo, mwp.memTSCNo,mwp.memName,wageMonthYear, wagecredit, wagePending, wagebalnace," + Environment.NewLine +
                                " TotalCredit = " + Environment.NewLine +
                                " (" + Environment.NewLine +
                                "   SELECT SUM(wagecredit)  FROM SNAT.dbo.T_MemberWegesProcess AS wp WHERE wp.memNationalID = mwp.memNationalID AND " + Environment.NewLine +
                                " wp.memMemberID = mwp.memMemberID AND  wp.memEmployeeNo = mwp.memEmployeeNo AND wp.memTSCNo = mwp.memTSCNo AND " + Environment.NewLine +
                                "  CAST(wp.wageMonthYear + '-' + '15' AS datetime) <= CAST(mwp.wageMonthYear + '-' + '15' AS datetime) " + Environment.NewLine +
                                "   GROUP BY wp.memNationalID, wp.memMemberID, wp.memEmployeeNo, wp.memTSCNo " + Environment.NewLine +
                                " ) , CAST(mwp.wageMonthYear + '-' + '15' AS datetime) UploadDate " + Environment.NewLine +
                                " FROM SNAT.dbo.T_MemberWegesProcess AS mwp " + Environment.NewLine +
                                " Where mwp.wageMonthYear='" + strMonthYear + "' " + Environment.NewLine +
                                " GROUP BY mwp.wageMonthYear, mwp.wagePending, mwp.wagebalnace, mwp.wagecredit, mwp.memNationalID, mwp.memMemberID, mwp.memEmployeeNo, mwp.memTSCNo,mwp.memName ";

                dtMemPayment = ClsDataLayer.GetDataTable(strSqlQuery);
                dtMemPayment.DefaultView.Sort = " wageMonthYear DESC";
                grdMemFinanceDetails.DataSource = dtMemPayment.DefaultView;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void btnFinanceSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFinanceSearch.Text.Trim())) { ClsMessage.ProjectExceptionMessage("Please enter financial year."); txtFinanceSearch.Focus(); return; }
            grdFinanceList.DataSource = null;
            grdMemFinanceDetails.DataSource = null;

            FillFinanceRegister(txtFinanceSearch.Text.Trim());
        }

        private void grdFinanceList_SelectionChanging(object sender, GridViewSelectionCancelEventArgs e)
        {
            try
            {
                if (dtFinanceRegister != null && dtFinanceRegister.DefaultView.Count > 0)
                {
                    FillMemPaymentList(dtFinanceRegister.DefaultView[grdFinanceList.CurrentRow.Index]["wageMonthYear"].ToString());
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtFinanceSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (string.IsNullOrEmpty(txtFinanceSearch.Text.Trim())) { ClsMessage.ProjectExceptionMessage("Please enter financial year."); txtFinanceSearch.Focus(); return; }
                grdFinanceList.DataSource = null;
                grdMemFinanceDetails.DataSource = null;
                FillFinanceRegister(txtFinanceSearch.Text.Trim());
            }
        }
    }
}