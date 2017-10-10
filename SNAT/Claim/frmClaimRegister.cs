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

namespace SNAT.Document
{
    public partial class frmClaimRegister : Form
    {
        DataTable dtClaimEntery = new DataTable();
        BindingSource bsClaimEntery = new BindingSource();
        string strSqlQuery = "";
        public frmClaimRegister()
        {
            InitializeComponent();
        }

        private void frmClaimRegister_Load(object sender, EventArgs e)
        {
            FillMemberData();
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
                              " ELSE 'No Status' END ClaimDesc" + Environment.NewLine +
                              " FROM SNAT.dbo.T_ClaimEntery ce" + Environment.NewLine +
                              " LEFT OUTER JOIN SNAT.dbo.T_Member tm ON ce.MemNationalID = tm.nationalid AND ce.MemberID = tm.memberid" + Environment.NewLine +
                              " LEFT OUTER JOIN SNAT.dbo.T_Beneficiary tb ON ce.MemNationalID = tb.membernationalid AND ce.MemberID = tb.memberid AND ce.BenfNationalID=tb.beneficiarynatioanalid" + Environment.NewLine +
                              " LEFT OUTER JOIN SNAT.dbo.M_School ms ON ms.code=tm.school";
                strSqlQuery = strSqlQuery + Environment.NewLine + " Order by ID DESC";
                dtClaimEntery = ClsDataLayer.GetDataTable(strSqlQuery);
                grdClaimList.DataSource = dtClaimEntery.DefaultView;




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
                dtClaimEntery.DefaultView.RowFilter = "";
                if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    ClsUtility.SearchText(dtClaimEntery, txtSearch.Text.Trim());
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (e.KeyChar == 13)
                {
                    dtClaimEntery.DefaultView.RowFilter = "";
                    if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                    {
                        ClsUtility.SearchText(dtClaimEntery, txtSearch.Text.Trim());
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
