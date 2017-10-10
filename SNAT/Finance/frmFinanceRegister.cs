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

namespace SNAT.Finance
{
    public partial class frmFinanceRegister : Form
    {
        string strSqlQuery = "";
        DataTable dtMemberEntery = new DataTable();
        DataTable dtPayment = new DataTable();
        public frmFinanceRegister()
        {
            InitializeComponent();
        }

        private void frmFinanceRegister_Load(object sender, EventArgs e)
        {
            MemRegister();


        }
        private void MemRegister()
        {
            try
            {
                strSqlQuery = "SELECT mb.id,mb.nationalid,mb.memberid ,mb.employeeno ,mb.tscno ,mb.membername ,mb.dob ,mb.sex ,mb.school,ms.name schoolname " +
                                " ,mb.contactno1 ,mb.contactno2 ,mb.residentaladdress ,mb.nomineenationalid ,mb.nomineename ,mb.wagesamount ,mb.wageseffectivedete" +
                                " ,mb.imagelocation ,mb.createdby ,mb.createddate ,mb.updateby ,mb.updateddate,mb.email,mb.nomineereleation,mb.livingstatus," +
                                " mb.deathdate, mb.mritalstatus , mb.suposenationaid , mb.suposename , mb.suposegender , mb.suposejoindate FROM SNAT.dbo.T_Member mb (nolock)" +
                                " LEFT OUTER JOIN SNAT.dbo.M_School ms (nolock) ON ms.code=mb.school order by mb.nationalid,mb.memberid ,mb.employeeno";
                dtMemberEntery = ClsDataLayer.GetDataTable(strSqlQuery);
                grdMemberList.DataSource = dtMemberEntery.DefaultView;

                if (dtMemberEntery != null && dtMemberEntery.DefaultView.Count > 0) { MemPayment(dtMemberEntery.DefaultView[0]["nationalid"].ToString()); } else { MemPayment("0"); }

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

                if (dtMemberEntery != null && dtMemberEntery.DefaultView.Count > 0)
                {
                    if (txtSearch.Text.Trim() != null && string.IsNullOrEmpty(txtSearch.Text.Trim()) == false)
                    {

                        ClsUtility.SearchText(dtMemberEntery, txtSearch.Text.Trim());

                    }
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


                    if (dtMemberEntery != null && dtMemberEntery.DefaultView.Count > 0)
                    {


                        ClsUtility.SearchText(dtMemberEntery, txtSearch.Text.Trim());


                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void MemPayment(string MemNationalID)
        {
            try
            {
                strSqlQuery = " SELECT wageMonthYear, wagecredit, wagePending, wagebalnace," + Environment.NewLine +
                                " TotalCredit = " + Environment.NewLine +
                                " (" + Environment.NewLine +
                                "   SELECT SUM(wagecredit)  FROM SNAT.dbo.T_MemberWegesProcess AS wp WHERE wp.memNationalID = mwp.memNationalID AND " + Environment.NewLine +
                                " wp.memMemberID = mwp.memMemberID AND  wp.memEmployeeNo = mwp.memEmployeeNo AND wp.memTSCNo = mwp.memTSCNo AND " + Environment.NewLine +
                                " CAST(wp.wageMonthYear + '-' + '15' AS datetime) <= CAST(mwp.wageMonthYear + '-' + '15' AS datetime) " + Environment.NewLine +
                                "   GROUP BY wp.memNationalID, wp.memMemberID, wp.memEmployeeNo, wp.memTSCNo " + Environment.NewLine +
                                " ) , CAST(mwp.wageMonthYear + '-' + '15' AS datetime) UploadDate " + Environment.NewLine +
                                " FROM SNAT.dbo.T_MemberWegesProcess AS mwp " + Environment.NewLine +
                                " Where mwp.memNationalID='" + MemNationalID + "' " + Environment.NewLine +
                                " GROUP BY mwp.wageMonthYear, mwp.wagePending, mwp.wagebalnace, mwp.wagecredit, mwp.memNationalID, mwp.memMemberID, mwp.memEmployeeNo, mwp.memTSCNo ";

                dtPayment = ClsDataLayer.GetDataTable(strSqlQuery);
                dtPayment.DefaultView.Sort = " wageMonthYear DESC";
                grdMemFinanceDetails.DataSource = dtPayment.DefaultView;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void grdMemberList_SelectionChanging(object sender, Telerik.WinControls.UI.GridViewSelectionCancelEventArgs e)
        {
            try
            {
                if (dtMemberEntery != null && dtMemberEntery.DefaultView.Count > 0)
                { MemPayment(dtMemberEntery.DefaultView[grdMemberList.CurrentRow.Index]["nationalid"].ToString()); }
                else
                { MemPayment("0"); }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnFilerData_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtPayment != null && dtPayment.DefaultView.Count > 0)
                {
                    string strMonthForm = "";
                    string strMonthTo = "";

                    strMonthForm = txtMonthFrom.Text.Trim() + "-" + txtYearFrom.Text.Trim();
                    strMonthTo = txtMonthTo.Text.Trim() + "-" + txtYearTo.Text.Trim();

                    dtPayment.DefaultView.RowFilter = "wageMonthYear>= '" + strMonthForm + "' AND wageMonthYear<='" + strMonthTo + "' ";
                }
                else
                {
                    dtPayment.DefaultView.RowFilter = "";
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {

            try
            {
                txtMonthFrom.Text = "";
                txtYearFrom.Text = "";
                txtMonthTo.Text = "";
                txtYearTo.Text = "";
                dtPayment.DefaultView.RowFilter = "";

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
    }
}
