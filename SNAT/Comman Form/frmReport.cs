using SNAT.Comman_Classes;
using SNAT.CommanClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Export;
using Telerik.WinControls.UI;

namespace SNAT.Comman_Form
{
    public partial class frmReport : Form
    {
        DataSet mdsCreateDataView = new DataSet();
        DataTable dtReport = new DataTable();
        string strSqlQuery = "";

        DataTable mdtMemberDetails = new DataTable();
        DataTable mdtBeneficiaryDetails = new DataTable();
        DataTable mdtLetMemberDetails = new DataTable();
        DataTable mdtClaimDetails = new DataTable();
        DataTable mdtChequeDetails = new DataTable();
        DataTable mdtPrimumUpload = new DataTable();
        DataTable mdtPrimumProcess = new DataTable();
        DataTable mdtTotalPrimumDetails = new DataTable();
        public frmReport()
        {
            InitializeComponent();
        }
        private void frmReport_Load(object sender, EventArgs e)
        {
            lblMnuHeader.ForeColor = Color.Red;
            A_lblHeader.Text = "SNAT Reporting Form";
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatePrint(tbMain.SelectedTab.Name.ToString()) == false) { return; }
                PrintReport(tbMain.SelectedTab.Name.ToString());
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        bool ValidatePrint(string strTbpName)
        {
            try
            {
                errorProvider1.Clear();
                switch (strTbpName.ToUpper())
                {
                    case "TBPCHEQUEREQUISITION":
                        if (string.IsNullOrEmpty(A_txtChqReqID.Text.Trim()))
                        {
                            errorProvider1.SetError(A_txtChqReqID, "Please select cheque requisition id.");
                            errorProvider1.SetIconPadding(A_txtChqReqID, -20);
                            ClsMessage.ProjectExceptionMessage("Please select cheque requisition id.");
                            return false;
                        }
                        break;
                    case "TBPLETPERSON":
                        if (string.IsNullOrEmpty(B_txtClaimId.Text.Trim()))
                        {
                            errorProvider1.SetError(B_txtClaimId, "Please select claim id.");
                            errorProvider1.SetIconPadding(B_txtClaimId, -20);
                            ClsMessage.ProjectExceptionMessage("Please select claim id.");
                            return false;
                        }
                        break;
                    case "TBPPREMIUMDETAILS":
                        if (I_chkSearchByMonthlyUpload.Checked)
                        {
                            if (string.IsNullOrEmpty(I_txtMonthFrom.Text.Trim()))
                            {
                                errorProvider1.SetError(I_txtMonthFrom, "Please enter upload month from.");
                                errorProvider1.SetIconPadding(I_txtMonthFrom, -20);
                                ClsMessage.ProjectExceptionMessage("Please enter upload month from.");
                                return false;
                            }
                            if (string.IsNullOrEmpty(I_txtYearFrom.Text.Trim()))
                            {
                                errorProvider1.SetError(I_txtYearFrom, "Please enter upload year from.");
                                errorProvider1.SetIconPadding(I_txtYearFrom, -20);
                                ClsMessage.ProjectExceptionMessage("Please enter upload year from.");
                                return false;
                            }
                            if (string.IsNullOrEmpty(I_txtMonthTo.Text.Trim()))
                            {
                                errorProvider1.SetError(I_txtMonthTo, "Please enter upload month to.");
                                errorProvider1.SetIconPadding(I_txtMonthTo, -20);
                                ClsMessage.ProjectExceptionMessage("Please enter upload month to.");
                                return false;
                            }
                            if (string.IsNullOrEmpty(I_txtYearTo.Text.Trim()))
                            {
                                errorProvider1.SetError(I_txtYearTo, "Please enter upload year to.");
                                errorProvider1.SetIconPadding(I_txtYearTo, -20);
                                ClsMessage.ProjectExceptionMessage("Please enter upload year to.");
                                return false;
                            }
                        }

                        if (I_chkSearchByMember.Checked)
                        {
                            if (string.IsNullOrEmpty(I_txtmembernationalid.Text.Trim()))
                            {
                                errorProvider1.SetError(I_txtmembernationalid, "Please enter/select member national id.");
                                errorProvider1.SetIconPadding(I_txtmembernationalid, -20);
                                ClsMessage.ProjectExceptionMessage("Please enter/select member national id");
                                return false;
                            }
                        }

                        break;
                    case "TBPPREMIUMPROCESSDETAILS":
                        if (J_chkSearchByUploadFrom.Checked)
                        {
                            if (string.IsNullOrEmpty(J_txtMonthFrom.Text.Trim()))
                            {
                                errorProvider1.SetError(J_txtMonthFrom, "Please enter upload month from.");
                                errorProvider1.SetIconPadding(J_txtMonthFrom, -20);
                                ClsMessage.ProjectExceptionMessage("Please enter upload month from.");
                                return false;
                            }
                            if (string.IsNullOrEmpty(J_txtYearFrom.Text.Trim()))
                            {
                                errorProvider1.SetError(J_txtYearFrom, "Please enter upload year from.");
                                errorProvider1.SetIconPadding(J_txtYearFrom, -20);
                                ClsMessage.ProjectExceptionMessage("Please enter upload year from.");
                                return false;
                            }
                            if (string.IsNullOrEmpty(J_txtMonthTo.Text.Trim()))
                            {
                                errorProvider1.SetError(J_txtMonthTo, "Please enter upload month to.");
                                errorProvider1.SetIconPadding(J_txtMonthTo, -20);
                                ClsMessage.ProjectExceptionMessage("Please enter upload month to.");
                                return false;
                            }
                            if (string.IsNullOrEmpty(J_txtYearTo.Text.Trim()))
                            {
                                errorProvider1.SetError(J_txtYearTo, "Please enter upload year to.");
                                errorProvider1.SetIconPadding(J_txtYearTo, -20);
                                ClsMessage.ProjectExceptionMessage("Please enter upload year to.");
                                return false;
                            }
                        }

                        if (J_chkSearchByMember.Checked)
                        {
                            if (string.IsNullOrEmpty(J_txtMemNationalID.Text.Trim()))
                            {
                                errorProvider1.SetError(J_txtMemNationalID, "Please enter/select member national id.");
                                errorProvider1.SetIconPadding(J_txtMemNationalID, -20);
                                ClsMessage.ProjectExceptionMessage("Please enter/select member national id");
                                return false;
                            }
                        }

                        break;

                }
                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }
        private void ClearText(TabPage tbpName)
        {
            try
            {
                foreach (Control ctrltb in tbpName.Controls)
                {
                    if (ctrltb.GetType() == typeof(Panel))
                    {
                        foreach (Control ctrltb2 in ctrltb.Controls)
                        {
                            if (ctrltb2.GetType() == typeof(TextBox) || ctrltb2.GetType() == typeof(RadTextBox))
                            {
                                ctrltb2.Text = string.Empty;
                            }

                        }
                    }
                    if (ctrltb.GetType() == typeof(TextBox) || ctrltb.GetType() == typeof(RadTextBox))
                    {
                        ctrltb.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void SelectUnSelectAll(string strButtonName, DataTable dtDataTable)
        {
            try
            {
                if (dtDataTable != null && dtDataTable.DefaultView.Count > 0)
                {
                    if (dtDataTable.Columns.Contains("lSelect"))
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (strButtonName.ToUpper().EndsWith("BTNSELECTALL"))
                        {
                            foreach (DataRowView drvItem in dtDataTable.DefaultView)
                            {
                                drvItem.BeginEdit();
                                drvItem["lSelect"] = true;
                                drvItem.EndEdit();
                            }
                        }
                        else
                        {
                            // dtDataTable.DefaultView.RowFilter = "ISNULL(lSelect,0)=1";
                            foreach (DataRowView drvItem in dtDataTable.DefaultView)
                            {
                                drvItem.BeginEdit();
                                drvItem["lSelect"] = false;
                                drvItem.EndEdit();
                            }
                            // dtDataTable.DefaultView.RowFilter = "";
                        }

                    }
                    dtDataTable.AcceptChanges();
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnChequeRequistion_Click(object sender, EventArgs e)
        {
            try
            {
                tbMain.SelectedIndex = 1;
                A_lblHeader.Text = "Cheque Requisition";
                tbMain.SelectedTab.Tag = "CR";
                ClearText(tbpChequeRequisition);
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void btnChequeApproval_Click(object sender, EventArgs e)
        {
            try
            {
                tbMain.SelectedIndex = 1;
                A_lblHeader.Text = "Cheque Approval";
                tbMain.SelectedTab.Tag = "CA";
                ClearText(tbpChequeRequisition);
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void btnMnuPremiumDetails_Click(object sender, EventArgs e)
        {
            try
            {
                tbMain.SelectedIndex = 3;
                A_lblHeader.Text = "Member Details";
                tbMain.SelectedTab.Tag = "MD";
                ClearText(tbpMemberDetails);
                SetMemberFiltterCheckBox();
                // FillMemberDetails();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnMnuBenefiDetails_Click(object sender, EventArgs e)
        {
            try
            {
                tbMain.SelectedIndex = 4;
                A_lblHeader.Text = "Beneficiary Details";
                tbMain.SelectedTab.Tag = "BD";
                ClearText(tbpBenefDetails);
                SetBeneficiaryFilterStatus();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnMnuLetMember_Click(object sender, EventArgs e)
        {
            try
            {
                tbMain.SelectedIndex = 5;
                A_lblHeader.Text = "Let. Member Details";
                tbMain.SelectedTab.Tag = "LM";
                ClearText(tbpLetMemberDetails);
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnMnuTotalClaimClear_Click(object sender, EventArgs e)
        {
            try
            {
                tbMain.SelectedIndex = 6;
                A_lblHeader.Text = "Claim Details";
                tbMain.SelectedTab.Tag = "CD";
                ClearText(tbpClaimDetails);
                SetClaimFilterStatus();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }


        private void btnMnuChequeStatus_Click(object sender, EventArgs e)
        {
            try
            {
                tbMain.SelectedIndex = 7;
                A_lblHeader.Text = "Cheque Requisition Status";
                tbMain.SelectedTab.Tag = "CR";
                ClearText(tbpChequeStatus);
                SetChequeFilterStatus();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }


        private void btnLetMember_Click(object sender, EventArgs e)
        {
            tbMain.SelectedIndex = 2;
            tbpChequeRequisition.Tag = "LP";
            ClearText(tbpLetPerson);

        }
        private void btnMnuPremiumUploadDetails_Click(object sender, EventArgs e)
        {
            try
            {
                tbMain.SelectedIndex = 8;
                A_lblHeader.Text = "Premium Upload Details";
                tbMain.SelectedTab.Tag = "PU";
                ClearText(tbpPremiumDetails);
                SetPremiumUploadFilterStatus();

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void btnMnuPremiumProcessDetails_Click(object sender, EventArgs e)
        {
            try
            {
                tbMain.SelectedIndex = 9;
                A_lblHeader.Text = "Premium Process Details";
                tbMain.SelectedTab.Tag = "PP";
                ClearText(tbpPremiumProcessDetails);
                SetPremiumProcessFilterStatus();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void btnMnuTotalPremiumDetails_Click(object sender, EventArgs e)
        {

            try
            {
                tbMain.SelectedIndex = 10;
                A_lblHeader.Text = "Total Premium Details";
                tbMain.SelectedTab.Tag = "TP";
                ClearText(tbpTotalPremiumDetails);
                SetTotalPremiumFilterStatus();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        #region Cheque Requisition
        private void A_btnSelectFolder_Click(object sender, EventArgs e)
        {
            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT ch.id, ch.ClaimID, Case when ISNULL(ch.LetPerson,'')='M' then 'Member' else 'Beneficiary' end LetPerson ,  ch.MemNationalID,ch.MemName, ch.MemberID ,ch.BeneNationalID,ch.BeneName FROM SNAT.dbo.T_ChequeEntry AS ch(nolock)";
                frmsrch.infSqlWhereCondtion = "";
                frmsrch.infSqlOrderBy = " id";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Cheque Entry....";
                frmsrch.infCodeFieldName = "id";
                frmsrch.infDescriptionFieldName = "ClaimID";
                frmsrch.infGridFieldName = "id,ClaimID,letperson,MemNationalID,BeneNationalID,MemName,MemberID ,BeneName";
                frmsrch.infGridFieldCaption = " Cheque id,Claim ID,Let Person ,Member National id ,Beneficiary National ID,MemName,MemberID ,BeneName";
                frmsrch.infGridFieldSize = "100,100,100,200,200,0,0,0";
                frmsrch.ShowDialog(this);
                if (frmsrch.DialogResult == DialogResult.OK && frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()))
                {
                    DataView dvDesg = new DataView();
                    dvDesg = frmsrch.infSearchReturnDataView;
                    dvDesg.RowFilter = "lSelect=1";

                    if (string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) == false)
                    {
                        A_txtChqReqID.Text = string.IsNullOrEmpty(dvDesg[0]["id"].ToString()) == true ? "" : dvDesg[0]["id"].ToString();
                        A_txtLetPerson.Text = string.IsNullOrEmpty(dvDesg[0]["LetPerson"].ToString()) == true ? "" : dvDesg[0]["LetPerson"].ToString();
                        A_txtMemNationalId.Text = string.IsNullOrEmpty(dvDesg[0]["MemNationalID"].ToString()) == true ? "" : dvDesg[0]["MemNationalID"].ToString();
                        A_txtMemberID.Text = string.IsNullOrEmpty(dvDesg[0]["MemberID"].ToString()) == true ? "" : dvDesg[0]["MemberID"].ToString();
                        A_txtMemberName.Text = string.IsNullOrEmpty(dvDesg[0]["MemName"].ToString()) == true ? "" : dvDesg[0]["MemName"].ToString();
                        A_txtClaimID.Text = string.IsNullOrEmpty(dvDesg[0]["ClaimID"].ToString()) == true ? "" : dvDesg[0]["ClaimID"].ToString();
                        A_txtBenID.Text = string.IsNullOrEmpty(dvDesg[0]["BeneNationalID"].ToString()) == true ? "" : dvDesg[0]["BeneNationalID"].ToString();
                        A_txtBenName.Text = string.IsNullOrEmpty(dvDesg[0]["BeneName"].ToString()) == true ? "" : dvDesg[0]["BeneName"].ToString();

                        dvDesg.RowFilter = "";
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        #endregion
        private void B_btnClaimId_Click(object sender, EventArgs e)
        {

            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT CE.id,Case when ISNULL(LetPerson,'')='M' then 'Member' else 'Beneficiary' end LetPerson,CE.MemNationalID,CE.MemberID,CE.MemName,CE.BenfNationalID,CE.BenfName,CE.NomineeName,CE.TotalAmount,CE.ReciviedBy" + Environment.NewLine +
                                            " ,tm.nomineenationalid,tm.contactno1 MemContact, tb.contactno1 benContact" + Environment.NewLine +
                                            " FROM SNAT.dbo.T_ClaimEntery CE (nolock)" + Environment.NewLine +
                                            " LEFT OUTER JOIN  SNAT.dbo.T_Member AS tm  (nolock) ON tm.nationalid = CE.MemNationalID AND  tm.memberid = CE.MemNationalID" + Environment.NewLine +
                                            " LEFT OUTER JOIN  SNAT.dbo.T_Beneficiary AS tb  (nolock) ON tb.beneficiarynatioanalid = ce.BenfNationalID";
                frmsrch.infSqlWhereCondtion = "";
                //" ISNULL(cPostStatus,'')='P' AND  ISNULL(Chariperson_Status,'')='A' AND  ISNULL(Secteatary_Status,'')='A' AND  ISNULL(Treasurer_Status,'')='A' " + Environment.NewLine +
                //                          " AND NOT EXISTS (SELECT * FROM SNAT.dbo.T_ChequeEntry AS ch(nolock) WHERE ch.ClaimID=ce.id AND ISNULL(ch.cPostStatus,'')='P' AND  ISNULL(ch.Chariperson_Status,'')='A' AND  ISNULL(ch.Secteatary_Status,'')='A' AND  ISNULL(ch.Treasurer_Status,'')='A')";
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
                        B_txtClaimId.Text = string.IsNullOrEmpty(dvDesg[0]["id"].ToString()) == true ? "" : dvDesg[0]["id"].ToString();
                        B_txtLetPerson.Text = string.IsNullOrEmpty(dvDesg[0]["LetPerson"].ToString()) == true ? "" : dvDesg[0]["LetPerson"].ToString();
                        B_txtMemNationalId.Text = string.IsNullOrEmpty(dvDesg[0]["MemNationalID"].ToString()) == true ? "" : dvDesg[0]["MemNationalID"].ToString();
                        B_txtMemID.Text = string.IsNullOrEmpty(dvDesg[0]["MemberID"].ToString()) == true ? "" : dvDesg[0]["MemberID"].ToString();
                        B_txtMemName.Text = string.IsNullOrEmpty(dvDesg[0]["MemName"].ToString()) == true ? "" : dvDesg[0]["MemName"].ToString();
                        B_txtMemContactNo.Text = string.IsNullOrEmpty(dvDesg[0]["MemContact"].ToString()) == true ? "" : dvDesg[0]["MemContact"].ToString();
                        B_txtBenNationalId.Text = string.IsNullOrEmpty(dvDesg[0]["BenfNationalID"].ToString()) == true ? "" : dvDesg[0]["BenfNationalID"].ToString();
                        B_txtBenName.Text = string.IsNullOrEmpty(dvDesg[0]["BenfName"].ToString()) == true ? "" : dvDesg[0]["BenfName"].ToString();
                        B_txtBenContactNo.Text = string.IsNullOrEmpty(dvDesg[0]["benContact"].ToString()) == true ? "" : dvDesg[0]["benContact"].ToString();
                        dvDesg.RowFilter = "";
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void PrintReport(string strTbpName)
        {
            try
            {
                DataView dvFilter = new DataView();
                switch (strTbpName.ToUpper())
                {

                    case "TBPCHEQUEREQUISITION":

                        if (!string.IsNullOrEmpty(A_txtChqReqID.Text.Trim()))
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
                              " LEFT OUTER JOIN SNAT.dbo.T_Member AS tm ON tm.nationalid = ch.MemNationalID AND tm.memberid = ch.MemNationalID" + Environment.NewLine +
                              " LEFT OUTER JOIN SNAT.dbo.T_Beneficiary AS tb ON tb.beneficiarynatioanalid = ch.BeneNationalID" + Environment.NewLine +
                              " Where ch.ID='" + A_txtChqReqID.Text.Trim() + "'";
                            dtReport = ClsDataLayer.GetDataTable(strSqlQuery);

                            if (dtReport != null && dtReport.DefaultView.Count > 0)
                            {
                                if (tbMain.SelectedTab.Tag != null)
                                {


                                    if (tbMain.SelectedTab.Tag.ToString().ToUpper() == "CR")
                                    {
                                        ClsUtility.PrintReport("rptChequeRequisiton.rpt", dtReport, "rptChequeRequisition");//
                                    }
                                    else
                                    {
                                        ClsUtility.PrintReport("rptChequeApproval.rpt", dtReport, "rptChequeRequisition");//
                                    }
                                }
                            }
                            else
                            {
                                ClsMessage.ProjectExceptionMessage("No Record Found!!");
                                return;
                            }
                        }
                        break;
                    case "TBPLETPERSON":
                        if (!string.IsNullOrEmpty(B_txtClaimId.Text.Trim()))
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
                             " LEFT OUTER JOIN SNAT.dbo.M_School ms ON ms.code=tm.school" + Environment.NewLine +
                             " Where ce.ID='" + B_txtClaimId.Text.Trim() + "'";

                            dtReport = ClsDataLayer.GetDataTable(strSqlQuery);
                            if (dtReport != null && dtReport.DefaultView.Count > 0)
                            {
                                ClsUtility.PrintReport("rptLetPerson.rpt", dtReport, "rptClaimDetails");//
                            }
                            else
                            {
                                ClsMessage.ProjectExceptionMessage("No Record Found!!");
                                return;
                            }

                        }
                        break;
                    case "TBPMEMBERDETAILS":

                        dtReport = new DataTable();
                        mdsCreateDataView = new DataSet();
                        dvFilter = new DataView();

                        dvFilter = mdsCreateDataView.DefaultViewManager.CreateDataView(mdtMemberDetails);
                        dvFilter.RowFilter = "lSelect=1";
                        dtReport = dvFilter.ToTable();

                        if (dtReport != null && dtReport.DefaultView.Count > 0)
                        {
                            if (D_rbDetails.Checked)
                            {
                                ClsUtility.PrintReport("rptMember.rpt", dtReport, "rptMember");//
                            }

                            else
                            {
                                ClsUtility.PrintReport("rptMember_List.rpt", dtReport, "rptMember");//
                            }
                        }
                        else
                        {
                            ClsMessage.ProjectExceptionMessage("No Record Found!!");
                        }
                        break;
                    case "TBPBENEFDETAILS":

                        dtReport = new DataTable();
                        mdsCreateDataView = new DataSet();
                        dvFilter = new DataView();

                        dvFilter = mdsCreateDataView.DefaultViewManager.CreateDataView(mdtBeneficiaryDetails);
                        dvFilter.RowFilter = "lSelect=1";
                        dtReport = dvFilter.ToTable();

                        if (dtReport != null && dtReport.DefaultView.Count > 0)
                        {
                            if (E_rbDetails.Checked)
                            {
                                ClsUtility.PrintReport("rptBeneficiary.rpt", dtReport, "rptBeneficiary");//
                            }

                            else
                            {
                                ClsUtility.PrintReport("rptBeneficiary_List.rpt", dtReport, "rptBeneficiary");//
                            }
                        }
                        else
                        {
                            ClsMessage.ProjectExceptionMessage("No Record Found!!");
                        }
                        break;
                    case "TBPLETMEMBERDETAILS":

                        dtReport = new DataTable();
                        mdsCreateDataView = new DataSet();
                        dvFilter = new DataView();

                        dvFilter = mdsCreateDataView.DefaultViewManager.CreateDataView(mdtLetMemberDetails);
                        dvFilter.RowFilter = "lSelect=1";
                        dtReport = dvFilter.ToTable();

                        if (dtReport != null && dtReport.DefaultView.Count > 0)
                        {
                            Dictionary<string, string> dicFormula = new Dictionary<string, string>();
                            dicFormula.Add("AsOnDate", F_dtpNoMoreDate.Value.ToString("dd/MMM/yyyy"));
                            if (F_rbDetails.Checked)
                            {

                                ClsUtility.PrintReport("rptLetMember.rpt", dtReport, "rptMember", dicFormula);//
                            }

                            else
                            {
                                ClsUtility.PrintReport("rptLetMember_List.rpt", dtReport, "rptMember", dicFormula);//
                            }
                        }
                        else
                        {
                            ClsMessage.ProjectExceptionMessage("No Record Found!!");
                        }
                        break;
                    case "TBPCLAIMDETAILS":

                        dtReport = new DataTable();
                        mdsCreateDataView = new DataSet();
                        dvFilter = new DataView();

                        dvFilter = mdsCreateDataView.DefaultViewManager.CreateDataView(mdtClaimDetails);
                        dvFilter.RowFilter = "lSelect=1";
                        dtReport = dvFilter.ToTable();

                        if (dtReport != null && dtReport.DefaultView.Count > 0)
                        {

                            if (G_rbDetails.Checked)
                            {

                                ClsUtility.PrintReport("rptClaimDetails.rpt", dtReport, "rptClaimDetails");//
                            }

                            else
                            {
                                ClsUtility.PrintReport("rptClaimDetails_List.rpt", dtReport, "rptClaimDetails");//
                            }
                        }
                        else
                        {
                            ClsMessage.ProjectExceptionMessage("No Record Found!!");
                        }
                        break;

                    case "TBPCHEQUESTATUS":

                        dtReport = new DataTable();
                        mdsCreateDataView = new DataSet();
                        dvFilter = new DataView();

                        dvFilter = mdsCreateDataView.DefaultViewManager.CreateDataView(mdtChequeDetails);
                        dvFilter.RowFilter = "lSelect=1";
                        dtReport = dvFilter.ToTable();

                        if (dtReport != null && dtReport.DefaultView.Count > 0)
                        {

                            if (H_rbDetails.Checked)
                            {

                                ClsUtility.PrintReport("rptChequeRequistionDetails.rpt", dtReport, "rptChequeRequisitionDetails");
                            }

                            else
                            {
                                ClsUtility.PrintReport("rptChequeRequistionDetails_List.rpt", dtReport, "rptChequeRequisitionDetails");
                            }
                        }
                        else
                        {
                            ClsMessage.ProjectExceptionMessage("No Record Found!!");
                        }
                        break;

                    case "TBPPREMIUMDETAILS":

                        dtReport = new DataTable();
                        mdsCreateDataView = new DataSet();
                        dvFilter = new DataView();

                        dvFilter = mdsCreateDataView.DefaultViewManager.CreateDataView(mdtPrimumUpload);
                        dvFilter.RowFilter = "lSelect=1";
                        dtReport = dvFilter.ToTable();

                        if (dtReport != null && dtReport.DefaultView.Count > 0)
                        {



                            ClsUtility.PrintReport("rptPremiumnUploadDetails.rpt", dtReport, "rptPremiumnUploadDetails");

                        }
                        else
                        {
                            ClsMessage.ProjectExceptionMessage("No Record Found!!");
                        }
                        break;
                    case "TBPPREMIUMPROCESSDETAILS":

                        dtReport = new DataTable();
                        mdsCreateDataView = new DataSet();
                        dvFilter = new DataView();

                        dvFilter = mdsCreateDataView.DefaultViewManager.CreateDataView(mdtPrimumProcess);
                        dvFilter.RowFilter = "lSelect=1";
                        dtReport = dvFilter.ToTable();

                        if (dtReport != null && dtReport.DefaultView.Count > 0)
                        {



                            ClsUtility.PrintReport("rptPremiumProcessDetails.rpt", dtReport, "rptPremiumProcessDetails");

                        }
                        else
                        {
                            ClsMessage.ProjectExceptionMessage("No Record Found!!");
                        }
                        break;
                    case "TBPTOTALPREMIUMDETAILS":


                        dtReport = new DataTable();
                        mdsCreateDataView = new DataSet();
                        dvFilter = new DataView();

                        dvFilter = mdsCreateDataView.DefaultViewManager.CreateDataView(mdtTotalPrimumDetails);
                        dvFilter.RowFilter = "lSelect=1";
                        dtReport = dvFilter.ToTable();

                        if (dtReport != null && dtReport.DefaultView.Count > 0)
                        {

                            if (K_rbInvoice.Checked)
                            {
                                ClsUtility.PrintReport("rptInvoic.rpt", dtReport, "rptTotalPremiumDetails");
                            }
                            else
                            {
                                ClsUtility.PrintReport("rptTotalPremiumDetails.rpt", dtReport, "rptTotalPremiumDetails");
                            }



                        }
                        else
                        {
                            ClsMessage.ProjectExceptionMessage("No Record Found!!");
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        #region Member Details
        private void D_SearchbyEffectiveDate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                D_dtpEffectiveDateFrom.Enabled = D_chkSearchbyEffectiveDate.Checked;
                D_dtpEffectiveDateTo.Enabled = D_chkSearchbyEffectiveDate.Checked;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void D_SearchbyMaterialStatus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                D_rbSingle.Enabled = D_chkSearchbyMaterialStatus.Checked;
                D_rbMarried.Enabled = D_chkSearchbyMaterialStatus.Checked;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void D_SearchbyLivingStatus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                D_rbLiving.Enabled = D_chkSearchbyLivingStatus.Checked;
                D_rbNoMore.Enabled = D_chkSearchbyLivingStatus.Checked;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void D_SearchbyNoMoreDate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                D_dtpNoMoreFrom.Enabled = D_chkSearchbyNoMoreDate.Checked;
                D_dtpNoMoreTo.Enabled = D_chkSearchbyNoMoreDate.Checked;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void D_SearchbySchool_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                D_txtSchoolCode.Enabled = D_chkSearchbySchool.Checked;

                D_btnPickSchool.Enabled = D_chkSearchbySchool.Checked;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SetMemberFiltterCheckBox()
        {
            try
            {
                D_dtpEffectiveDateFrom.Enabled = D_chkSearchbyEffectiveDate.Checked;
                D_dtpEffectiveDateTo.Enabled = D_chkSearchbyEffectiveDate.Checked;
                D_rbSingle.Enabled = D_chkSearchbyMaterialStatus.Checked;
                D_rbMarried.Enabled = D_chkSearchbyMaterialStatus.Checked;
                D_rbLiving.Enabled = D_chkSearchbyLivingStatus.Checked;
                D_rbNoMore.Enabled = D_chkSearchbyLivingStatus.Checked;
                D_dtpNoMoreFrom.Enabled = D_chkSearchbyNoMoreDate.Checked;
                D_dtpNoMoreTo.Enabled = D_chkSearchbyNoMoreDate.Checked;
                D_txtSchoolCode.Enabled = D_chkSearchbySchool.Checked;
                D_btnPickSchool.Enabled = D_chkSearchbySchool.Checked;
                D_btnSelectAll.ButtonElement.ToolTipText = "Select all record";
                D_btnUnSelectAll.ButtonElement.ToolTipText = "Un-Select all record";
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private DataTable FillMemberDetails()
        {
            try
            {
                mdtMemberDetails = new DataTable();
                strSqlQuery = "SELECT  cast(1 as bit) lSelect,id, nationalid, memberid, employeeno, tscno, membername, dob, sex, school, contactno1, contactno2, residentaladdress," + Environment.NewLine +
                            " nomineenationalid, nomineename,wagesamount, wageseffectivedete, imageLocation, createdby, createddate, updateby, updateddate," + Environment.NewLine +
                            " email, nomineereleation, livingstatus, deathdate, mritalstatus,suposenationaid, suposename, suposegender, suposejoindate" + Environment.NewLine +
                            " FROM  SNAT.dbo.T_Member AS tm Where 1=1";

                if (D_chkSearchbyEffectiveDate.Checked)
                {
                    strSqlQuery = strSqlQuery + Environment.NewLine + " and  wageseffectivedete between  '" + D_dtpEffectiveDateFrom.Value.ToString("yyyy/MM/dd") + "' and '" + D_dtpEffectiveDateTo.Value.ToString("yyyy/MM/dd") + "'  ";
                }

                if (D_chkSearchbyMaterialStatus.Checked)
                {
                    if (D_rbMarried.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " and  ISNULL(mritalstatus,'') = 'M' ";
                    }
                    else
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " and  ISNULL(mritalstatus,'') <> 'M' ";
                    }

                }
                if (D_chkSearchbyLivingStatus.Checked)
                {
                    if (D_rbLiving.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " and  ISNULL(livingstatus,'') = 'L' ";
                    }
                    else
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " and  ISNULL(livingstatus,'') <> 'L' ";
                    }

                }

                if (D_chkSearchbyNoMoreDate.Checked)
                {
                    strSqlQuery = strSqlQuery + Environment.NewLine + " and  deathdate between  '" + D_dtpNoMoreFrom.Value.ToString("yyyy/MM/dd") + "' and '" + D_dtpNoMoreTo.Value.ToString("yyyy/MM/dd") + "'  ";
                }

                if (D_chkSearchbySchool.Checked)
                {
                    if (string.IsNullOrEmpty(D_txtSchoolCode.Text.Trim()))
                    {
                        ClsMessage.ProjectExceptionMessage("Please enter school code to filter data");
                        D_txtSchoolCode.Focus();
                    }
                    strSqlQuery = strSqlQuery + Environment.NewLine + " and  school='" + D_txtSchoolCode.Text.Trim() + "'";
                }
                strSqlQuery = strSqlQuery + Environment.NewLine + " Order by memberid";
                mdtMemberDetails = ClsDataLayer.GetDataTable(strSqlQuery);
                if (mdtMemberDetails.Columns.Contains("lSelect")) { mdtMemberDetails.Columns["lSelect"].ReadOnly = false; }
                D_grdMemberDetails.DataSource = mdtMemberDetails.DefaultView;
                return mdtMemberDetails;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return null;
            }
        }
        private void D_btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                FillMemberDetails();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }
        private void D_btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                SelectUnSelectAll(((Telerik.WinControls.UI.RadButton)sender).Name, mdtMemberDetails);
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void D_btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ClsUtility.SearchText(mdtMemberDetails, D_txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void D_txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {

                    ClsUtility.SearchText(mdtMemberDetails, D_txtSearch.Text.Trim());
                }
                //else
                //{
                //    mdtMemberDetails.DefaultView.RowFilter = "";
                //}
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void D_btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewSpreadExport spreadExporter = new GridViewSpreadExport(this.D_grdMemberDetails);
                //SpreadExportRenderer exportRenderer = new SpreadExportRenderer();
                spreadExporter.RunExport("D:\\exportedFile.xlsx");
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void D_btnPickSchool_Click(object sender, EventArgs e)
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
                    D_txtSchoolCode.Text = frmsrch.infCodeFieldText;
                    D_txtSchoolDesc.Text = frmsrch.infDescriptionFieldText;
                }
            }


            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void D_txtSchoolCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(D_txtSchoolCode.Text.Trim()))
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.M_School", "code", "code", D_txtSchoolCode.Text.Trim(), D_txtSchoolDesc, "name") == false)
                    {
                        ClsMessage.ProjectExceptionMessage("Invalid school code!!");
                        D_txtSchoolCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        private DataTable FillPrimumUploadData()
        {
            try
            {
                mdtPrimumUpload = new DataTable();
                strSqlQuery = " SELECT wu.id, wu.wageMonthYear, wu.wageFrom, wu.memNationalID, wu.memMemberID, wu.memEmployeeNo, wu.memTSCNo, wu.memName, wu.processingdate,wu.wagecredit," + Environment.NewLine +
                              " wu.wagebalnace, wu.memWegeRemarks, wu.Remarks, wu.luploaded, wu.lValidMemmber,wu.lWagesProcessed, wu.lApproved , wu.documentType, wu.documentName," + Environment.NewLine +
                              " wu.createby, wu.createdate, wu.updateby, wu.updatedate,wp.cProcessed,wp.lProcessed,wp.lApproved  lWpApproved " + Environment.NewLine +
                              " FROM SNAT.dbo.T_WagesUpload wu (nolock)" + Environment.NewLine +
                              " LEFT OUTER JOIN SNAT.dbo.T_MemberWegesProcess wp ON wp.memNationalID = wu.memNationalID AND wp.memMemberID = wu.memMemberID AND wp.memEmployeeNo = wu.memEmployeeNo AND wp.wageMonthYear = wu.wageMonthYear" + Environment.NewLine +
                              " Where 1=1 ";
                if (I_chkSearchByMonthlyUpload.Checked)
                {
                    string strMothYearFrom = I_txtYearFrom.Text.Trim() + "-" + I_txtMonthFrom.Text.Trim() + "-15";
                    string strMothYearTo = I_txtYearTo.Text.Trim() + "-" + I_txtMonthTo.Text.Trim() + "-15";
                    strSqlQuery = strSqlQuery + " AND  Cast(wu.wageMonthYear +'-15' as DateTime) Between Cast('" + strMothYearFrom + "' as DateTime) AND Cast('" + strMothYearTo + "' as DateTime) ";
                }
                if (I_chkSearchByUploadFrom.Checked)
                {
                    string strWageFrom = "";
                    if (I_rbBank.Checked)
                    {
                        strWageFrom = "BANK";
                    }
                    if (I_rbPSPF.Checked)
                    {
                        strWageFrom = "PSPF";
                    }
                    if (I_rbTreasury.Checked)
                    {
                        strWageFrom = "TREASURY";
                    }
                    strSqlQuery = strSqlQuery + " AND  wu.wageFrom='" + strWageFrom + "'";
                }
                if (I_chkSearchByMember.Checked)
                {

                    strSqlQuery = strSqlQuery + " AND  wu.memNationalID='" + I_txtmembernationalid.Text.Trim() + "'";
                }

                strSqlQuery = strSqlQuery + Environment.NewLine + " Order by wu.wageMonthYear Desc, wu.memNationalID, wu.memMemberID ASC";
                mdtPrimumUpload = ClsDataLayer.GetDataTable(strSqlQuery);
                DataColumn dclSelect = new DataColumn("lSelect", typeof(bool));
                dclSelect.DefaultValue = true;
                dclSelect.ReadOnly = false;
                mdtPrimumUpload.Columns.Add(dclSelect);
                I_grdMemberPayemnt.DataSource = mdtPrimumUpload.DefaultView;
                return mdtPrimumUpload;

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
                return null;
            }
        }
        #endregion
        #region Beneficiary Details
        private void E_txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    ClsUtility.SearchText(mdtBeneficiaryDetails, E_txtSearch.Text.Trim());
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void E_btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ClsUtility.SearchText(mdtBeneficiaryDetails, E_txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void E_btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                FillBeneficiaryDetails();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void E_chkSearchbyEffectiveDate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                E_dtpEffectiveDataFrom.Enabled = E_chkSearchbyEffectiveDate.Checked;
                E_dtpEffectiveDateTo.Enabled = E_chkSearchbyEffectiveDate.Checked;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void E_chkSearchByLivingStatus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                E_rblive.Enabled = E_chkSearchByLivingStatus.Checked;
                E_rbNoMore.Enabled = E_chkSearchByLivingStatus.Checked;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void E_chkSearchByNoMoreDate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                E_dtpNoMoreDateFrom.Enabled = E_chkSearchByNoMoreDate.Checked;
                E_dtpNoMoreDateTo.Enabled = E_chkSearchByNoMoreDate.Checked;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void E_btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                SelectUnSelectAll(((Telerik.WinControls.UI.RadButton)sender).Name, mdtBeneficiaryDetails);
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SetBeneficiaryFilterStatus()
        {
            try
            {
                E_dtpEffectiveDataFrom.Enabled = E_chkSearchbyEffectiveDate.Checked;
                E_dtpEffectiveDateTo.Enabled = E_chkSearchbyEffectiveDate.Checked;
                E_rblive.Enabled = E_chkSearchByLivingStatus.Checked;
                E_rbNoMore.Enabled = E_chkSearchByLivingStatus.Checked;
                E_dtpNoMoreDateFrom.Enabled = E_chkSearchByNoMoreDate.Checked;
                E_dtpNoMoreDateTo.Enabled = E_chkSearchByNoMoreDate.Checked;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private DataTable FillBeneficiaryDetails()
        {
            try
            {
                mdtBeneficiaryDetails = new DataTable();
                strSqlQuery = " SELECT cast(1 as bit) lSelect,  id, membernationalid, memberid, membername, beneficiarynatioanalid, beneficiaryname, dob, sex, dateofsubmission," + Environment.NewLine +
                              " relationship, contactno1, contactno2, email, residentaladrees, nomineenationalid, nomineename, wages, effactivedate," + Environment.NewLine +
                              " imagelocation, lstatus, createdby, createddate, updateby, updateddate, livingstatus,dateofDate " + Environment.NewLine +
                              " FROM  SNAT.dbo.T_Beneficiary AS tb Where 1=1 ";

                if (E_chkSearchbyEffectiveDate.Checked)
                {
                    strSqlQuery = strSqlQuery + Environment.NewLine + " and  effactivedate between  '" + E_dtpEffectiveDataFrom.Value.ToString("yyyy/MM/dd") + "' and '" + E_dtpEffectiveDateTo.Value.ToString("yyyy/MM/dd") + "'  ";
                }

                if (E_chkSearchByLivingStatus.Checked)
                {
                    if (E_rblive.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " and  ISNULL(livingstatus,'') <> 'D' ";
                    }
                    else
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " and  ISNULL(livingstatus,'') = 'D' ";
                    }

                }

                if (E_chkSearchByNoMoreDate.Checked)
                {
                    strSqlQuery = strSqlQuery + Environment.NewLine + " and  dateofDate between  '" + E_dtpNoMoreDateFrom.Value.ToString("yyyy/MM/dd") + "' and '" + E_dtpNoMoreDateTo.Value.ToString("yyyy/MM/dd") + "'  ";
                }


                strSqlQuery = strSqlQuery + Environment.NewLine + " Order by memberid,beneficiarynatioanalid";
                mdtBeneficiaryDetails = ClsDataLayer.GetDataTable(strSqlQuery);
                if (mdtBeneficiaryDetails.Columns.Contains("lSelect")) { mdtBeneficiaryDetails.Columns["lSelect"].ReadOnly = false; }
                E_grdBeneList.DataSource = mdtBeneficiaryDetails.DefaultView;
                return mdtBeneficiaryDetails;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return null;
            }
        }
        #endregion
        #region Let Member Details


        private void F_txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    ClsUtility.SearchText(mdtLetMemberDetails, F_txtSearch.Text.Trim());
                }

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void F_btnSerch_Click(object sender, EventArgs e)
        {
            try
            {
                ClsUtility.SearchText(mdtLetMemberDetails, F_txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void F_btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                FillLetMemberDetails();
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void F_btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                SelectUnSelectAll(((RadButton)sender).Name, mdtLetMemberDetails);
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private DataTable FillLetMemberDetails()
        {
            try
            {
                mdtLetMemberDetails = new DataTable();
                strSqlQuery = "SELECT  cast(1 as bit) lSelect,id, nationalid, memberid, employeeno, tscno, membername, dob, sex, school, contactno1, contactno2, residentaladdress," + Environment.NewLine +
                            " nomineenationalid, nomineename,wagesamount, wageseffectivedete, imageLocation, createdby, createddate, updateby, updateddate," + Environment.NewLine +
                            " email, nomineereleation, livingstatus, deathdate, mritalstatus,suposenationaid, suposename, suposegender, suposejoindate" + Environment.NewLine +
                            " FROM  SNAT.dbo.T_Member AS tm Where ISNULL(livingstatus,'')='D'";


                strSqlQuery = strSqlQuery + Environment.NewLine + " and  deathdate <=  '" + F_dtpNoMoreDate.Value.ToString("yyyy/MM/dd") + "'";


                strSqlQuery = strSqlQuery + Environment.NewLine + " Order by memberid";
                mdtLetMemberDetails = ClsDataLayer.GetDataTable(strSqlQuery);
                if (mdtLetMemberDetails.Columns.Contains("lSelect")) { mdtLetMemberDetails.Columns["lSelect"].ReadOnly = false; }
                F_grdMemberList.DataSource = mdtLetMemberDetails.DefaultView;
                return mdtLetMemberDetails;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return null;
            }
        }
        #endregion
        #region Claim Details
        private void G_chkSearchByMonthly_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                G_dtpClaimMonthlyForm.Enabled = G_chkSearchByMonthly.Checked;
                G_dtpClaimMonthlyTo.Enabled = G_chkSearchByMonthly.Checked;
            }

            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void G_SearchByClaimStatus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                G_rbEntered.Enabled = G_chkSearchByClaimStatus.Checked;
                G_rbPosted.Enabled = G_chkSearchByClaimStatus.Checked;
                G_rbApprovedByChairperson.Enabled = G_chkSearchByClaimStatus.Checked;
                G_rbApprovedBySecretary.Enabled = G_chkSearchByClaimStatus.Checked;
                G_rbApprovedByTreasurer.Enabled = G_chkSearchByClaimStatus.Checked;
                G_rbRejectedByChairperson.Enabled = G_chkSearchByClaimStatus.Checked;
                G_rbRejectedBySecretary.Enabled = G_chkSearchByClaimStatus.Checked;
                G_rbRejectedByTreasurer.Enabled = G_chkSearchByClaimStatus.Checked;
            }

            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void G_txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    ClsUtility.SearchText(mdtClaimDetails, G_txtSearch.Text.Trim());
                }

            }

            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void G_btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ClsUtility.SearchText(mdtClaimDetails, G_txtSearch.Text.Trim());
            }

            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void G_btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                FillClaimDetails();
            }

            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SetClaimFilterStatus()
        {
            try
            {
                G_dtpClaimMonthlyForm.Enabled = G_chkSearchByMonthly.Checked;
                G_dtpClaimMonthlyTo.Enabled = G_chkSearchByMonthly.Checked;
                G_rbEntered.Enabled = G_chkSearchByClaimStatus.Checked;
                G_rbPosted.Enabled = G_chkSearchByClaimStatus.Checked;
                G_rbApprovedByChairperson.Enabled = G_chkSearchByClaimStatus.Checked;
                G_rbApprovedBySecretary.Enabled = G_chkSearchByClaimStatus.Checked;
                G_rbApprovedByTreasurer.Enabled = G_chkSearchByClaimStatus.Checked;
                G_rbRejectedByChairperson.Enabled = G_chkSearchByClaimStatus.Checked;
                G_rbRejectedBySecretary.Enabled = G_chkSearchByClaimStatus.Checked;
                G_rbRejectedByTreasurer.Enabled = G_chkSearchByClaimStatus.Checked;
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private DataTable FillClaimDetails()
        {
            try
            {
                mdtClaimDetails = new DataTable();

                strSqlQuery = "SELECT Cast(1 as Bit ) lSelect,  ce.id, ce.LetPerson, ce.MemNationalID, ce.MemberID, ce.MemName, ce.BenfNationalID, ce.BenfName, ce.PlaceOfBurial," + Environment.NewLine +
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
                              " LEFT OUTER JOIN SNAT.dbo.M_School ms ON ms.code=tm.school" + Environment.NewLine +
                              " Where 1=1";

                if (G_chkSearchByMonthly.Checked)
                {
                    strSqlQuery = strSqlQuery + Environment.NewLine + " AND CreateDate BETWEEN  '" + G_dtpClaimMonthlyForm.Value.ToString("yyyy/MM/dd") + "'  AND '" + G_dtpClaimMonthlyTo.Value.ToString("yyyy/MM/dd") + "' ";
                }
                if (G_chkSearchByClaimStatus.Checked)
                {

                    if (G_rbEntered.Checked)
                    {

                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ce.ClaimStatus='E' ";
                    }
                    if (G_rbPosted.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ce.ClaimStatus='P' ";
                    }
                    if (G_rbApprovedByChairperson.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ce.ClaimStatus='CA' AND ISNULL(ce.Chariperson_Status,'')='A' ";
                    }
                    if (G_rbRejectedByChairperson.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ce.ClaimStatus='CR' AND ISNULL(ce.Chariperson_Status,'')='R' ";
                    }
                    if (G_rbApprovedBySecretary.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ce.ClaimStatus='SA' AND ISNULL(ce.Secteatary_Status,'')='A' ";
                    }
                    if (G_rbRejectedBySecretary.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ce.ClaimStatus='SR' AND ISNULL(ce.Secteatary_Status,'')='R' ";
                    }
                    if (G_rbApprovedByTreasurer.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ce.ClaimStatus='TA' AND ISNULL(ce.Treasurer_Status,'')='A' ";
                    }
                    if (G_rbRejectedByTreasurer.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ce.ClaimStatus='TR' AND ISNULL(ce.Treasurer_Status,'')='R' ";
                    }

                }
                strSqlQuery = strSqlQuery + Environment.NewLine + "  Order By  ce.id, ce.LetPerson, ce.MemNationalID, ce.MemberID";
                mdtClaimDetails = ClsDataLayer.GetDataTable(strSqlQuery);
                if (mdtClaimDetails.Columns.Contains("lSelect")) { mdtClaimDetails.Columns["lSelect"].ReadOnly = false; }
                G_grdClaimList.DataSource = mdtClaimDetails.DefaultView;
                return mdtClaimDetails;
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
                return null;
            }
        }
        private void G_btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                SelectUnSelectAll(((RadButton)sender).Name, mdtClaimDetails);
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        #endregion
        #region Cheque Requisition Status
        private void H_txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    ClsUtility.SearchText(mdtChequeDetails, H_txtSearch.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void H_btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ClsUtility.SearchText(mdtChequeDetails, H_txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void H_btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                FillChequeStatus();
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void H_chkSearchByMonthly_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                H_dtpChequeReqFrom.Enabled = H_chkSearchByMonthly.Checked;
                H_dtpChequeReqTo.Enabled = H_chkSearchByMonthly.Checked;

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void H_chkChequeReciving_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                H_dtpChequeRecivingFrom.Enabled = H_chkChequeReciving.Checked;
                H_dtpChequeRecivingTo.Enabled = H_chkChequeReciving.Checked;
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void H_chkSearchByChqStatus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                H_rbEntered.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbPosted.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbApprovedbyChairperson.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbRejectedbyChairperson.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbApprovedbySecretary.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbRejectedbySecretary.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbApprovedbyTreasurer.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbRejectedbyTreasurer.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbChequeIssueRecivied.Enabled = H_chkSearchByChqStatus.Checked;

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void H_btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                SelectUnSelectAll(((RadButton)sender).Name, mdtChequeDetails);
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SetChequeFilterStatus()
        {
            try
            {
                H_dtpChequeReqFrom.Enabled = H_chkSearchByMonthly.Checked;
                H_dtpChequeReqTo.Enabled = H_chkSearchByMonthly.Checked;
                H_rbEntered.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbPosted.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbApprovedbyChairperson.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbRejectedbyChairperson.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbApprovedbySecretary.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbRejectedbySecretary.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbApprovedbyTreasurer.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbRejectedbyTreasurer.Enabled = H_chkSearchByChqStatus.Checked;
                H_rbChequeIssueRecivied.Enabled = H_chkSearchByChqStatus.Checked;
                H_dtpChequeRecivingFrom.Enabled = H_chkChequeReciving.Checked;
                H_dtpChequeRecivingTo.Enabled = H_chkChequeReciving.Checked;

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void FillChequeStatus()
        {
            try
            {
                mdtChequeDetails = new DataTable();
                strSqlQuery = "SELECT Cast(1 as bit) lSelect, ch.id, ch.ClaimID, ch.letperson, ch.MemNationalID, ch.MemberID, ch.MemName, tm.contactno1 AS memContact, ch.BeneNationalID," + Environment.NewLine +
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
                            " LEFT OUTER JOIN SNAT.dbo.T_Member AS tm ON tm.nationalid = ch.MemNationalID AND tm.memberid = ch.MemNationalID" + Environment.NewLine +
                            " LEFT OUTER JOIN SNAT.dbo.T_Beneficiary AS tb ON tb.beneficiarynatioanalid = ch.BeneNationalID Where 1=1   ";


                if (H_chkSearchByMonthly.Checked)
                {
                    strSqlQuery = strSqlQuery + Environment.NewLine + " AND ch.CreateDate BetWeen '" + H_dtpChequeReqFrom.Value.ToString("yyyy/MM/dd") + "' AND '" + H_dtpChequeReqTo.Value.ToString("yyyy/MM/dd") + "'";
                }

                if (H_chkChequeReciving.Checked)
                {
                    strSqlQuery = strSqlQuery + Environment.NewLine + " AND ch.ChqRecivingDate BetWeen '" + H_dtpChequeRecivingFrom.Value.ToString("yyyy/MM/dd") + "' AND '" + H_dtpChequeRecivingTo.Value.ToString("yyyy/MM/dd") + "'";
                }

                if (H_chkSearchByChqStatus.Checked)
                {

                    if (H_rbEntered.Checked)
                    {

                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ISNULL(ch.ChqStatus,'')='E' ";
                    }
                    if (H_rbPosted.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ISNULL(ch.ChqStatus,'')='P' ";
                    }
                    if (H_rbApprovedbyChairperson.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ISNULL(ch.ChqStatus,'')='CA' AND ISNULL(ch.Chariperson_Status,'')='A' ";
                    }
                    if (H_rbRejectedbyChairperson.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ISNULL(ch.ChqStatus,'')='CR' AND ISNULL(ch.Chariperson_Status,'')='R' ";
                    }
                    if (H_rbApprovedbySecretary.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ISNULL(ch.ChqStatus,'')='SA' AND ISNULL(ch.Secteatary_Status,'')='A' ";
                    }
                    if (H_rbRejectedbySecretary.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ISNULL(ch.ChqStatus,'')='SR' AND ISNULL(ch.Secteatary_Status,'')='R' ";
                    }
                    if (H_rbApprovedbyTreasurer.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ISNULL(ch.ChqStatus,'')='TA' AND ISNULL(ch.Treasurer_Status,'')='A' ";
                    }
                    if (H_rbRejectedbyTreasurer.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ISNULL(ch.ChqStatus,'')='TR' AND ISNULL(ch.Treasurer_Status,'')='R' ";
                    }
                    if (H_rbChequeIssueRecivied.Checked)
                    {
                        strSqlQuery = strSqlQuery + Environment.NewLine + " AND ISNULL(ch.ChqStatus,'')='CI' AND ISNULL(ch.ChqRecivingDate,'')<>'' ";
                    }
                }
                strSqlQuery = strSqlQuery + Environment.NewLine + "  Order By  ch.id, ch.ClaimID, ch.letperson, ch.MemNationalID, ch.MemberID";
                mdtChequeDetails = ClsDataLayer.GetDataTable(strSqlQuery);
                if (mdtChequeDetails.Columns.Contains("lSelect")) { mdtChequeDetails.Columns["lSelect"].ReadOnly = false; }
                H_grdChequeList.DataSource = mdtChequeDetails.DefaultView;
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }



        #endregion
        #region Premium Upload
        private void I_txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    ClsUtility.SearchText(mdtPrimumUpload, I_txtSearch.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void I_btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                ClsUtility.SearchText(mdtPrimumUpload, I_txtSearch.Text.Trim());

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void I_btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatePrint(tbpPremiumDetails.Name) == false) { return; }
                FillPrimumUploadData();

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void I_chkSearchByMonthlyUpload_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                I_txtMonthFrom.Enabled = I_chkSearchByMonthlyUpload.Checked;
                I_txtMonthTo.Enabled = I_chkSearchByMonthlyUpload.Checked;
                I_txtYearFrom.Enabled = I_chkSearchByMonthlyUpload.Checked;
                I_txtYearTo.Enabled = I_chkSearchByMonthlyUpload.Checked;

                if (I_chkSearchByMonthlyUpload.Checked)
                {
                    I_txtMonthFrom.Text = DateTime.Now.ToString("MM");
                    I_txtMonthTo.Text = DateTime.Now.ToString("MM");
                    I_txtYearFrom.Text = DateTime.Now.Year.ToString();
                    I_txtYearTo.Text = DateTime.Now.Year.ToString();
                }



            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }

        }

        private void I_chkSearchByUploadFrom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                I_rbPSPF.Enabled = I_chkSearchByUploadFrom.Checked;
                I_rbBank.Enabled = I_chkSearchByUploadFrom.Checked;
                I_rbTreasury.Enabled = I_chkSearchByUploadFrom.Checked;

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void I_chkSearchByMember_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                I_txtmembernationalid.Enabled = I_chkSearchByMember.Checked;
                I_btnPickMemberNationid.Enabled = I_chkSearchByMember.Checked;


            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void SetPremiumUploadFilterStatus()
        {
            try
            {
                I_txtMonthFrom.Enabled = I_chkSearchByMonthlyUpload.Checked;
                I_txtMonthTo.Enabled = I_chkSearchByMonthlyUpload.Checked;
                I_txtYearFrom.Enabled = I_chkSearchByMonthlyUpload.Checked;
                I_txtYearTo.Enabled = I_chkSearchByMonthlyUpload.Checked;
                I_txtmembernationalid.Enabled = I_chkSearchByMember.Checked;
                I_btnPickMemberNationid.Enabled = I_chkSearchByMember.Checked;
                I_rbPSPF.Enabled = I_chkSearchByUploadFrom.Checked;
                I_rbBank.Enabled = I_chkSearchByUploadFrom.Checked;
                I_rbTreasury.Enabled = I_chkSearchByUploadFrom.Checked;

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void I_btnSelectAll_Click(object sender, EventArgs e)
        {
            SelectUnSelectAll(((RadButton)sender).Name, mdtPrimumUpload);
        }
        private void I_txtmembernationalid_Validating(object sender, CancelEventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(I_txtmembernationalid.Text.Trim()))
                {
                    string strSql = "SELECT id , nationalid , memberid , employeeno , tscno , membername FROM SNAT.dbo.T_Member AS ted where nationalid='" + I_txtmembernationalid.Text.Trim() + "'";
                    DataTable dtMemberid = new DataTable();
                    dtMemberid = ClsDataLayer.GetDataTable(strSql);
                    if (dtMemberid != null && dtMemberid.DefaultView.Count > 0)
                    {
                        I_txtMemberId.Text = string.IsNullOrEmpty(dtMemberid.DefaultView[0]["memberid"].ToString()) == true ? "" : dtMemberid.DefaultView[0]["memberid"].ToString();
                        I_txtMemberName.Text = string.IsNullOrEmpty(dtMemberid.DefaultView[0]["membername"].ToString()) == true ? "" : dtMemberid.DefaultView[0]["membername"].ToString();

                    }
                    else
                    {
                        ClsMessage.showMessage("Invalid member national id", MessageBoxIcon.Information);
                        I_txtmembernationalid.Focus();
                        I_txtMemberId.Text = "";
                        I_txtMemberName.Text = "";


                    }
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void I_btnPickMemberNationid_Click(object sender, EventArgs e)
        {


            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT id , nationalid , memberid , employeeno , tscno , membername,email,contactno1 FROM SNAT.dbo.T_Member AS ted";
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
                    I_txtmembernationalid.Text = frmsrch.infCodeFieldText.Trim();
                    I_txtMemberName.Text = string.IsNullOrEmpty(dvDesg[0]["membername"].ToString()) == true ? "" : dvDesg[0]["membername"].ToString();
                    I_txtMemberId.Text = string.IsNullOrEmpty(dvDesg[0]["memberid"].ToString()) == true ? "" : dvDesg[0]["memberid"].ToString();
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        #endregion
        #region Premium Process
        private void J_txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    ClsUtility.SearchText(mdtPrimumProcess, J_txtSearch.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void J_btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                ClsUtility.SearchText(mdtPrimumProcess, J_txtSearch.Text.Trim());

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void J_btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatePrint(tbpPremiumProcessDetails.Name) == false) { return; }
                FillPrimumProcessData();

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void J_chkSearchByProcessMonth_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                J_txtMonthFrom.Enabled = J_chkSearchByProcessMonth.Checked;
                J_txtMonthTo.Enabled = J_chkSearchByProcessMonth.Checked;
                J_txtYearFrom.Enabled = J_chkSearchByProcessMonth.Checked;
                J_txtYearTo.Enabled = J_chkSearchByProcessMonth.Checked;


                if (J_chkSearchByProcessMonth.Checked)
                {
                    J_txtMonthFrom.Text = DateTime.Now.ToString("MM");
                    J_txtMonthTo.Text = DateTime.Now.ToString("MM");
                    J_txtYearFrom.Text = DateTime.Now.Year.ToString();
                    J_txtYearTo.Text = DateTime.Now.Year.ToString();
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void J_chkSearchByUploadFrom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                J_rbPSPF.Enabled = J_chkSearchByUploadFrom.Checked;
                J_rbBank.Enabled = J_chkSearchByUploadFrom.Checked;
                J_rbTreasury.Enabled = J_chkSearchByUploadFrom.Checked;
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void J_chkSearchByMember_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                J_txtMemNationalID.Enabled = J_chkSearchByMember.Checked;
                J_btnPickMember.Enabled = J_chkSearchByMember.Checked;

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SetPremiumProcessFilterStatus()
        {
            try
            {
                J_txtMonthFrom.Enabled = J_chkSearchByProcessMonth.Checked;
                J_txtMonthTo.Enabled = J_chkSearchByProcessMonth.Checked;
                J_txtYearFrom.Enabled = J_chkSearchByProcessMonth.Checked;
                J_txtYearTo.Enabled = J_chkSearchByProcessMonth.Checked;
                J_rbPSPF.Enabled = J_chkSearchByUploadFrom.Checked;
                J_rbBank.Enabled = J_chkSearchByUploadFrom.Checked;
                J_rbTreasury.Enabled = J_chkSearchByUploadFrom.Checked;
                J_txtMemNationalID.Enabled = J_chkSearchByMember.Checked;
                J_btnPickMember.Enabled = J_chkSearchByMember.Checked;
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void J_btnPickMember_Click(object sender, EventArgs e)
        {

            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT id , nationalid , memberid , employeeno , tscno , membername,email,contactno1 FROM SNAT.dbo.T_Member AS ted";
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
                    J_txtMemNationalID.Text = frmsrch.infCodeFieldText.Trim();
                    J_txtMemberName.Text = string.IsNullOrEmpty(dvDesg[0]["membername"].ToString()) == true ? "" : dvDesg[0]["membername"].ToString();
                    J_txtMemberID.Text = string.IsNullOrEmpty(dvDesg[0]["memberid"].ToString()) == true ? "" : dvDesg[0]["memberid"].ToString();
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void J_txtMemNationalID_Validating(object sender, CancelEventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(J_txtMemNationalID.Text.Trim()))
                {
                    string strSql = "SELECT id , nationalid , memberid , employeeno , tscno , membername FROM SNAT.dbo.T_Member AS ted where nationalid='" + J_txtMemNationalID.Text.Trim() + "'";
                    DataTable dtMemberid = new DataTable();
                    dtMemberid = ClsDataLayer.GetDataTable(strSql);
                    if (dtMemberid != null && dtMemberid.DefaultView.Count > 0)
                    {
                        J_txtMemberID.Text = string.IsNullOrEmpty(dtMemberid.DefaultView[0]["memberid"].ToString()) == true ? "" : dtMemberid.DefaultView[0]["memberid"].ToString();
                        J_txtMemberName.Text = string.IsNullOrEmpty(dtMemberid.DefaultView[0]["membername"].ToString()) == true ? "" : dtMemberid.DefaultView[0]["membername"].ToString();

                    }
                    else
                    {
                        ClsMessage.showMessage("Invalid member national id", MessageBoxIcon.Information);
                        J_txtMemNationalID.Focus();
                        J_txtMemberID.Text = "";
                        J_txtMemberName.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void J_btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                SelectUnSelectAll(((RadButton)sender).Name, mdtPrimumProcess);
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private DataTable FillPrimumProcessData()
        {
            try
            {
                mdtPrimumProcess = new DataTable();
                strSqlQuery = "SELECT  id, wageMonthYear, wageFrom, memNationalID, memMemberID, memEmployeeNo, memTSCNo, memName, processingdate, wagecredit," + Environment.NewLine +
                          " wagePending, wagebalnace, memWegeRemarks, cProcessed, Remarks,cApprovalRemarks, luploaded, lProcessed, ProcessedBy, Processeddate, lApproved, ApprovedBy," + Environment.NewLine +
                          " ApprovedDate, createby, createdate, updateby, updatedate FROM SNAT.dbo.T_MemberWegesProcess " + Environment.NewLine +
                           " Where 1=1 ";
                if (J_chkSearchByProcessMonth.Checked)
                {
                    string strMothYearFrom = J_txtYearFrom.Text.Trim() + "-" + J_txtMonthFrom.Text.Trim() + "-15";
                    string strMothYearTo = J_txtYearTo.Text.Trim() + "-" + J_txtMonthTo.Text.Trim() + "-15";
                    strSqlQuery = strSqlQuery + " AND  Cast(wageMonthYear +'-15' as DateTime) Between Cast('" + strMothYearFrom + "' as DateTime) AND Cast('" + strMothYearTo + "' as DateTime) ";
                }
                if (J_chkSearchByUploadFrom.Checked)
                {
                    string strWageFrom = "";
                    if (J_rbBank.Checked)
                    {
                        strWageFrom = "BANK";
                    }
                    if (J_rbPSPF.Checked)
                    {
                        strWageFrom = "PSPF";
                    }
                    if (J_rbTreasury.Checked)
                    {
                        strWageFrom = "TREASURY";
                    }
                    strSqlQuery = strSqlQuery + " AND  wageFrom='" + strWageFrom + "'";
                }
                if (J_chkSearchByMember.Checked)
                {

                    strSqlQuery = strSqlQuery + " AND  memNationalID='" + J_txtMemNationalID.Text.Trim() + "'";
                }
                strSqlQuery = strSqlQuery + Environment.NewLine + " Order by wageMonthYear Desc, memNationalID, memMemberID ASC";
                mdtPrimumProcess = ClsDataLayer.GetDataTable(strSqlQuery);
                DataColumn dclSelect = new DataColumn("lSelect", typeof(bool));
                dclSelect.DefaultValue = true;
                dclSelect.ReadOnly = false;
                mdtPrimumProcess.Columns.Add(dclSelect);
                J_grdProcessedPayment.DataSource = mdtPrimumProcess.DefaultView;
                return mdtPrimumProcess;

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
                return null;
            }
        }



        #endregion
        #region Total Premium Details
        private void K_txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    ClsUtility.SearchText(mdtTotalPrimumDetails, K_txtSearch.Text.Trim());
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void K_btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ClsUtility.SearchText(mdtTotalPrimumDetails, K_txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void K_btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                FillTotalPrimum();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private DataTable FillTotalPrimum()
        {
            try
            {
                mdtTotalPrimumDetails = new DataTable();
                K_grdMemFinanceDetails.DataSource = null;
                strSqlQuery = " SELECT mwp.memNationalID, mwp.memMemberID, mwp.memEmployeeNo, mwp.memTSCNo ,mwp.memName,wageMonthYear, wagecredit, wagePending, wagebalnace," + Environment.NewLine +
                         " TotalCredit = " + Environment.NewLine +
                         " (" + Environment.NewLine +
                         "   SELECT SUM(wagecredit)  FROM SNAT.dbo.T_MemberWegesProcess AS wp WHERE wp.memNationalID = mwp.memNationalID AND " + Environment.NewLine +
                         " wp.memMemberID = mwp.memMemberID AND  wp.memEmployeeNo = mwp.memEmployeeNo AND wp.memTSCNo = mwp.memTSCNo AND " + Environment.NewLine +
                         " CAST(wp.wageMonthYear + '-' + '15' AS datetime) <= CAST(mwp.wageMonthYear + '-' + '15' AS datetime) " + Environment.NewLine +
                         "   GROUP BY wp.memNationalID, wp.memMemberID, wp.memEmployeeNo, wp.memTSCNo " + Environment.NewLine +
                         " ) , CAST(mwp.wageMonthYear + '-' + '15' AS datetime) UploadDate " + Environment.NewLine +
                         " ,iPrimumAmount = (SELECT TOP 1 tm.wagesamount FROM SNAT.dbo.T_Member tm WHERE tm.nationalid = mwp.memNationalID AND tm.memberid = mwp.memMemberID ORDER BY tm.wagesamount DESC)" + Environment.NewLine +
                         " FROM SNAT.dbo.T_MemberWegesProcess AS mwp " + Environment.NewLine +
                         "Where 1=1";

                if (K_chkSearchByPocessMonth.Checked)
                {
                    string strMothYearFrom = K_txtYearFrom.Text.Trim() + "-" + K_txtMonthFrom.Text.Trim() + "-15";
                    string strMothYearTo = K_txtYearTo.Text.Trim() + "-" + K_txtMonthTo.Text.Trim() + "-15";
                    strSqlQuery = strSqlQuery + " AND  Cast(mwp.wageMonthYear +'-15' as DateTime) Between Cast('" + strMothYearFrom + "' as DateTime) AND Cast('" + strMothYearTo + "' as DateTime) ";
                }

                if (K_chkSearchByMember.Checked)
                {

                    strSqlQuery = strSqlQuery + " AND  mwp.memNationalID='" + K_txtMemNationalID.Text.Trim() + "'";
                }
                strSqlQuery = strSqlQuery + Environment.NewLine + " GROUP BY mwp.wageMonthYear, mwp.wagePending, mwp.wagebalnace, mwp.wagecredit, mwp.memNationalID, mwp.memMemberID, mwp.memEmployeeNo, mwp.memTSCNo,mwp.memName ";
                strSqlQuery = strSqlQuery + Environment.NewLine + " Order by wageMonthYear Desc, mwp.memNationalID, mwp.memMemberID ASC";
                mdtTotalPrimumDetails = ClsDataLayer.GetDataTable(strSqlQuery);

                DataColumn dclSelect = new DataColumn("lSelect", typeof(bool));
                dclSelect.DefaultValue = true;
                dclSelect.ReadOnly = false;
                mdtTotalPrimumDetails.Columns.Add(dclSelect);
                K_grdMemFinanceDetails.DataSource = mdtTotalPrimumDetails.DefaultView;
                return mdtTotalPrimumDetails;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return null;
            }
        }
        private void K_chkSearchByPocessMonth_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                K_txtMonthFrom.Enabled = K_chkSearchByPocessMonth.Checked;
                K_txtYearFrom.Enabled = K_chkSearchByPocessMonth.Checked;
                K_txtMonthTo.Enabled = K_chkSearchByPocessMonth.Checked;
                K_txtYearTo.Enabled = K_chkSearchByPocessMonth.Checked;

                if (K_chkSearchByPocessMonth.Checked)
                {
                    K_txtMonthFrom.Text = DateTime.Now.ToString("MM");
                    K_txtMonthTo.Text = DateTime.Now.ToString("MM");
                    K_txtYearFrom.Text = DateTime.Now.Year.ToString();
                    K_txtYearTo.Text = DateTime.Now.Year.ToString();
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void K_chkSearchByMember_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                K_txtMemberID.Enabled = K_chkSearchByMember.Checked;
                K_btnPickMember.Enabled = K_chkSearchByMember.Checked;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SetTotalPremiumFilterStatus()
        {
            try
            {
                K_txtMonthFrom.Enabled = K_chkSearchByPocessMonth.Checked;
                K_txtYearFrom.Enabled = K_chkSearchByPocessMonth.Checked;
                K_txtMonthTo.Enabled = K_chkSearchByPocessMonth.Checked;
                K_txtYearTo.Enabled = K_chkSearchByPocessMonth.Checked;
                K_txtMemberID.Enabled = K_chkSearchByMember.Checked;
                K_btnPickMember.Enabled = K_chkSearchByMember.Checked;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void K_btnPickMember_Click(object sender, EventArgs e)
        {

            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT id , nationalid , memberid , employeeno , tscno , membername,email,contactno1 FROM SNAT.dbo.T_Member AS ted";
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
                    K_txtMemNationalID.Text = frmsrch.infCodeFieldText.Trim();
                    K_txtMemName.Text = string.IsNullOrEmpty(dvDesg[0]["membername"].ToString()) == true ? "" : dvDesg[0]["membername"].ToString();
                    K_txtMemberID.Text = string.IsNullOrEmpty(dvDesg[0]["memberid"].ToString()) == true ? "" : dvDesg[0]["memberid"].ToString();
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void K_txtMemNationalID_Validating(object sender, CancelEventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(K_txtMemNationalID.Text.Trim()))
                {
                    string strSql = "SELECT id , nationalid , memberid , employeeno , tscno , membername FROM SNAT.dbo.T_Member AS ted where nationalid='" + K_txtMemNationalID.Text.Trim() + "'";
                    DataTable dtMemberid = new DataTable();
                    dtMemberid = ClsDataLayer.GetDataTable(strSql);
                    if (dtMemberid != null && dtMemberid.DefaultView.Count > 0)
                    {
                        K_txtMemberID.Text = string.IsNullOrEmpty(dtMemberid.DefaultView[0]["memberid"].ToString()) == true ? "" : dtMemberid.DefaultView[0]["memberid"].ToString();
                        K_txtMemName.Text = string.IsNullOrEmpty(dtMemberid.DefaultView[0]["membername"].ToString()) == true ? "" : dtMemberid.DefaultView[0]["membername"].ToString();

                    }
                    else
                    {
                        ClsMessage.showMessage("Invalid member national id", MessageBoxIcon.Information);
                        K_txtMemNationalID.Focus();
                        K_txtMemberID.Text = "";
                        K_txtMemName.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void K_btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                SelectUnSelectAll(((RadButton)sender).Name, mdtTotalPrimumDetails);
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }


        #endregion

        #region Grid lSelect
        private void K_grdMemFinanceDetails_CellClick(object sender, GridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.Column.FieldName.ToString().ToUpper() == "lSelect".ToUpper())
            //    {
            //        RadGridView grdView = new RadGridView();
            //      //  grdView = (RadGridView)sender;

            //        DataView dtGridView = new DataView();
            //        dtGridView = (DataView)grdView.DataSource;

            //        if (dtGridView != null && dtGridView.Count > 0)
            //        {
            //            if (dtGridView.ToTable().Columns.Contains("lSelect"))
            //            {
            //                int iRow = 0;
            //                iRow = grdView.CurrentRow.Index;
            //                dtGridView[iRow].BeginEdit();
            //                dtGridView[iRow]["lSelect"] = ! Convert.ToBoolean(dtGridView[iRow]["lSelect"]);
            //                dtGridView[iRow].EndEdit();

            //            }
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{

            //    ClsMessage.ProjectExceptionMessage(ex);
            //}
        }

        #endregion

        private void btnMnuSendSms_Click(object sender, EventArgs e)
        {
            try
            {
                clsSendSMS.SendSMSToMobileNo("971559604376", "Hello Santosh Sir");
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
    }
}

