using SNAT.Comman_Classes;
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
    public partial class frmMemberRegister : Form
    {
        BindingSource bsMemmberEntery = new BindingSource();
        string strSqlQuery = "";
        DataTable dtMemberEntery = new DataTable();
        //  // string imageFinalLocation = "";
        DataTable dtBeneficiryList = new DataTable();
        DataTable dtPayment = new DataTable();
        public frmMemberRegister()
        {
            InitializeComponent();
            BindControl();
        }
        void FillMemberData(string strMember)
        {
            try
            {
                strSqlQuery = "SELECT mb.id,mb.nationalid,mb.memberid ,mb.employeeno ,mb.tscno ,mb.membername ,mb.dob ,mb.sex ,mb.school,ms.name schoolname " +
                                " ,mb.contactno1 ,mb.contactno2 ,mb.residentaladdress ,mb.nomineenationalid ,mb.nomineename ,mb.wagesamount ,mb.wageseffectivedete" +
                                " ,mb.imagelocation ,mb.createdby ,mb.createddate ,mb.updateby ,mb.updateddate,mb.email FROM SNAT.dbo.T_Member mb (nolock)" +
                                " LEFT OUTER JOIN SNAT.dbo.M_School ms (nolock) ON ms.code=mb.school order by mb.nationalid,mb.memberid ,mb.employeeno";
                if( strMember.Trim()!="")
                {
                    strSqlQuery = strSqlQuery + strMember;
                }

                dtMemberEntery = ClsDataLayer.GetDataTable(strSqlQuery);

                DataColumn dcImage = new DataColumn("image", typeof(byte[]));
                dtMemberEntery.Columns.Add(dcImage);
                foreach (DataRowView drvRow in dtMemberEntery.DefaultView)
                {
                    drvRow.BeginEdit();
                    if (drvRow["imagelocation"] != null && string.IsNullOrEmpty(drvRow["imagelocation"].ToString()) == false)
                    {
                       // drvRow["image"] = ClsUtility.GetByteArray(drvRow["imagelocation"].ToString());

                    }
                    else
                    {
                        //if (File.Exists(Application.StartupPath + @"\img_not_available120X120.png"))
                        //{
                        //    drvRow["image"] = ClsUtility.GetByteArray(Application.StartupPath + @"\img_not_available120X120.png");
                        //}

                    }

                    drvRow.EndEdit();
                }
                dtMemberEntery.AcceptChanges();
                bsMemmberEntery.DataSource = dtMemberEntery.DefaultView;

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
                FillMemberData("");

                txtNationalId.DataBindings.Add("Text", bsMemmberEntery, "nationalid", false, DataSourceUpdateMode.OnPropertyChanged);
                txtMemberID.DataBindings.Add("Text", bsMemmberEntery, "memberid", false, DataSourceUpdateMode.OnValidation);
                txtEmployeeNo.DataBindings.Add("Text", bsMemmberEntery, "employeeno", false, DataSourceUpdateMode.OnValidation);
                txtTSCNo.DataBindings.Add("Text", bsMemmberEntery, "tscno", false, DataSourceUpdateMode.OnValidation);
                txtMemberName.DataBindings.Add("Text", bsMemmberEntery, "membername", false, DataSourceUpdateMode.OnValidation);
                dtpDOB.DataBindings.Add("Text", bsMemmberEntery, "dob", false, DataSourceUpdateMode.OnValidation);
                txtSex.DataBindings.Add("Text", bsMemmberEntery, "sex", false, DataSourceUpdateMode.OnValidation);
                txtSchoolCode.DataBindings.Add("Text", bsMemmberEntery, "school", false, DataSourceUpdateMode.OnValidation);
                txtSchoolDesc.DataBindings.Add("Text", bsMemmberEntery, "schoolname", false, DataSourceUpdateMode.OnValidation);
                txtContactNo1.DataBindings.Add("Text", bsMemmberEntery, "contactno1", false, DataSourceUpdateMode.OnValidation);
                txtContactNo2.DataBindings.Add("Text", bsMemmberEntery, "contactno2", false, DataSourceUpdateMode.OnValidation);
                txtResidentalAddress.DataBindings.Add("Text", bsMemmberEntery, "residentaladdress", false, DataSourceUpdateMode.OnValidation);
                txtNomineeNationalId.DataBindings.Add("Text", bsMemmberEntery, "nomineenationalid", false, DataSourceUpdateMode.OnValidation);
                txtNomineeName.DataBindings.Add("Text", bsMemmberEntery, "nomineename", false, DataSourceUpdateMode.OnValidation);
                txtWagesAmount.DataBindings.Add("Text", bsMemmberEntery, "wagesamount", false, DataSourceUpdateMode.OnValidation);
                dtpWagesEffectiveDate.DataBindings.Add("Text", bsMemmberEntery, "wageseffectivedete", false, DataSourceUpdateMode.OnValidation);
                // txtImageLocation.DataBindings.Add("Text", bsMemmberEntery, "imagelocation", false, DataSourceUpdateMode.OnValidation);
                txtemailid.DataBindings.Add("Text", bsMemmberEntery, "email", false, DataSourceUpdateMode.OnValidation);
                grdMemberDetails.DataSource = bsMemmberEntery;

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);

            }
        }
        private void frmMemberRegister_Load(object sender, EventArgs e)
        {
            splitContainerBody.Panel2Collapsed = true;
            splitContainerGrid.Panel2Collapsed = true;
            splitContainerbeneficiry.Panel2Collapsed = true;
            //filldata();
        }

        private void chkShofinancial_CheckedChanged(object sender, EventArgs e)
        {

            if (chkShowBeneficiry.Checked && !chkShofinancial.Checked)
            {
                splitContainerGrid.Panel2Collapsed = false;
                splitContainerbeneficiry.Panel1Collapsed = chkShowBeneficiry.Checked;
                splitContainerbeneficiry.Panel2Collapsed = !chkShowBeneficiry.Checked;
            }
            else if (chkShowBeneficiry.Checked && chkShofinancial.Checked)
            {
                splitContainerGrid.Panel2Collapsed = false;
                splitContainerbeneficiry.Panel1Collapsed = !chkShowBeneficiry.Checked;
                splitContainerbeneficiry.Panel2Collapsed = !chkShowBeneficiry.Checked;
            }
            else if (!chkShowBeneficiry.Checked && chkShofinancial.Checked)
            {
                splitContainerGrid.Panel2Collapsed = false;
                splitContainerbeneficiry.Panel1Collapsed = !chkShowBeneficiry.Checked;
                splitContainerbeneficiry.Panel2Collapsed = !chkShowBeneficiry.Checked;
            }
            else
            {
                splitContainerGrid.Panel2Collapsed = true;
                splitContainerbeneficiry.Panel2Collapsed = true;
            }
            splitContainerBody.Panel2Collapsed = !chkShowMember.Checked;


        }

        private void grdMemberDetails_SelectionChanging(object sender, Telerik.WinControls.UI.GridViewSelectionCancelEventArgs e)
        {

            try
            {

                string iNationalID = "0";
                int iMemRow = bsMemmberEntery.Position;
                if (dtMemberEntery != null && dtMemberEntery.DefaultView.Count > 0)
                {
                    iNationalID = string.IsNullOrEmpty(dtMemberEntery.DefaultView[iMemRow]["nationalid"].ToString()) == true ? "0" : dtMemberEntery.DefaultView[iMemRow]["nationalid"].ToString();
                }

                FillBeneficiary(iNationalID);
                MemPayment(iNationalID);

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void FillBeneficiary(string iNationalID)
        {
            try
            {

                strSqlQuery = " SELECT id, membernationalid, memberid, membername, beneficiarynatioanalid, beneficiaryname,wages, effactivedate,relationship,contactno1,imagelocation," +
                              " lstatus FROM dbo.T_Beneficiary bn (nolock) " +
                              " Where bn.membernationalid='" + iNationalID + "' Order by membernationalid, beneficiarynatioanalid";

                dtBeneficiryList = ClsDataLayer.GetDataTable(strSqlQuery);


                DataColumn dcImage = new DataColumn("image", typeof(byte[]));
                dtBeneficiryList.Columns.Add(dcImage);
                foreach (DataRowView drvRow in dtBeneficiryList.DefaultView)
                {
                    drvRow.BeginEdit();
                    if (drvRow["imagelocation"] != null && string.IsNullOrEmpty(drvRow["imagelocation"].ToString()) == false)
                    {
                       // drvRow["image"] = ClsUtility.GetByteArray(drvRow["imagelocation"].ToString());

                    }
                    else
                    {
                        //if (File.Exists(Application.StartupPath + @"\img_not_available120X120.png"))
                        //{
                        //    drvRow["image"] = ClsUtility.GetByteArray(Application.StartupPath + @"\img_not_available120X120.png");
                        //}

                    }

                    drvRow.EndEdit();
                }
                dtBeneficiryList.AcceptChanges();


                grdBeneficiry.DataSource = dtBeneficiryList.DefaultView;
                // bindingNavigatorMain.BindingSource = bsMemberWages;

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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    ClsUtility.SearchText(dtMemberEntery, txtSearch.Text.Trim());
                }
                else
                {
                    dtMemberEntery.DefaultView.RowFilter = "";
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                    {
                        ClsUtility.SearchText(dtMemberEntery, txtSearch.Text.Trim());
                    }
                    else
                    {
                        dtMemberEntery.DefaultView.RowFilter = "";
                    }

                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);
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
                                " Where mwp.memNationalID='" + MemNationalID + "' ";
                if ((!string.IsNullOrEmpty(txtMonthFrom.Text.Trim()) && !string.IsNullOrEmpty(txtYearFrom.Text.Trim()))
                        && ((!string.IsNullOrEmpty(txtMonthTo.Text.Trim()) && !string.IsNullOrEmpty(txtYearTo.Text.Trim()))))
                {
                    strSqlQuery = strSqlQuery + Environment.NewLine + " AND CAST(wp.wageMonthYear + '-' + '15' AS datetime)>='CAST("+ txtYearFrom.Text.Trim() +"-" + txtMonthFrom.Text.Trim() + "'-' + '15' AS datetime)'  AND CAST(wp.wageMonthYear + ' - ' + '15' AS datetime)>='CAST("+ txtYearTo.Text.Trim() +" - " + txtMonthTo.Text.Trim() + "'-' + '15' AS datetime)'";
                }
                strSqlQuery = strSqlQuery + Environment.NewLine + " GROUP BY mwp.wageMonthYear, mwp.wagePending, mwp.wagebalnace, mwp.wagecredit, mwp.memNationalID, mwp.memMemberID, mwp.memEmployeeNo, mwp.memTSCNo ";

                dtPayment = ClsDataLayer.GetDataTable(strSqlQuery);
                dtPayment.DefaultView.Sort = " wageMonthYear DESC";
                grdMemFinanceDetails.DataSource = dtPayment.DefaultView;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtMonthFrom.Text = string.Empty;
            txtMonthTo.Text = string.Empty;
            txtYearFrom.Text = string.Empty;
            txtYearTo.Text = string.Empty;
            FilterMemberPayment();
        }
        private void FilterMemberPayment()
        {
            try
            {
                string iNationalID = "0";
                int iMemRow = bsMemmberEntery.Position;
                if (dtMemberEntery != null && dtMemberEntery.DefaultView.Count > 0)
                {
                    iNationalID = string.IsNullOrEmpty(dtMemberEntery.DefaultView[iMemRow]["nationalid"].ToString()) == true ? "0" : dtMemberEntery.DefaultView[iMemRow]["nationalid"].ToString();
                }
                if (dtPayment != null) { dtPayment.DefaultView.RowFilter = ""; }
                if (dtPayment != null && dtPayment.DefaultView.Count > 0)
                {

                    if ((!string.IsNullOrEmpty(txtMonthFrom.Text.Trim()) && !string.IsNullOrEmpty(txtYearFrom.Text.Trim()))
                        && ((!string.IsNullOrEmpty(txtMonthTo.Text.Trim()) && !string.IsNullOrEmpty(txtYearTo.Text.Trim()))))
                    {
                        MemPayment(iNationalID);
                    }
                    else
                    {
                        MemPayment(iNationalID);
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            FilterMemberPayment();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
