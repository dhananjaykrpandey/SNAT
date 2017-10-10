using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SNAT.Comman_Classes;
using SNAT.Comman_Form;

namespace SNAT.Employee
{
    public partial class frmDesgination : Form
    {
        BindingSource bsDesgination = new BindingSource();
        string strSqlQuery = "";
        DataTable dtDesgination = new DataTable();
        public frmDesgination()
        {
            InitializeComponent();
            LoadDesgHichry();
            BindControl();
        }
        private void FillDataTable()
        {
            try
            {
                strSqlQuery = "SELECT dg.id,dg.code , dg.Name , dg.DepartCode,td.name DepartName , dg.HeadofDepartment , dg.IsReportingDesg" +
                           " , dg.ReportCode,rdg.name ReportName , dg.Remarks , dg.lStatus FROM SNAT.dbo.T_Designation dg (nolock)" +
                           " INNER JOIN dbo.T_Department td  (nolock) ON td.code=dg.DepartCode LEFT OUTER JOIN SNAT.dbo.T_Designation rdg (nolock)" +
                           " ON rdg.code=dg.ReportCode ORDER BY dg.id,dg.code";
                dtDesgination = ClsDataLayer.GetDataTable(strSqlQuery);
                bsDesgination.DataSource = dtDesgination.DefaultView;
                bindingNavigatorMain.BindingSource = bsDesgination;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);

            }
        }
        private void BindControl()
        {
            try
            {
                FillDataTable();

                txtcode.DataBindings.Add("Text", bsDesgination, "code", false, DataSourceUpdateMode.OnPropertyChanged);
                txtName.DataBindings.Add("Text", bsDesgination, "name", false, DataSourceUpdateMode.OnPropertyChanged);

                txtDeptCode.DataBindings.Add("Text", bsDesgination, "DepartCode", false, DataSourceUpdateMode.OnPropertyChanged);
                txtDepartName.DataBindings.Add("Text", bsDesgination, "DepartName", false, DataSourceUpdateMode.OnPropertyChanged);

                txtchkHeadOFDepart.DataBindings.Add("Text", bsDesgination, "HeadofDepartment", false, DataSourceUpdateMode.OnPropertyChanged);
                txtchkReportRequd.DataBindings.Add("Text", bsDesgination, "IsReportingDesg", false, DataSourceUpdateMode.OnPropertyChanged);



                txtRDesgCode.DataBindings.Add("Text", bsDesgination, "ReportCode", false, DataSourceUpdateMode.OnPropertyChanged);
                txtRDesgName.DataBindings.Add("Text", bsDesgination, "ReportName", false, DataSourceUpdateMode.OnPropertyChanged);

                txtRemarks.DataBindings.Add("Text", bsDesgination, "remarks", false, DataSourceUpdateMode.OnPropertyChanged);
                txtChkBoxStatus.DataBindings.Add("Text", bsDesgination, "lStatus", false, DataSourceUpdateMode.OnPropertyChanged);

                // chkStatus.DataBindings.Add("Checked", bsDepartmetn, "status", false);
                grdList.DataSource = bsDesgination;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SetEnable(bool lValue)
        {
            txtcode.Enabled = lValue;
            txtName.Enabled = lValue;
            txtRemarks.Enabled = lValue;
            chkStatus.Enabled = lValue;
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
            txtDeptCode.Enabled = lValue;
            //  txtDepartName.Enabled = lValue;
            btnPickDepartment.Enabled = lValue;

            txtRDesgCode.Enabled = lValue;
            //   txtRDesgName.Enabled = lValue;
            btnPickRDesg.Enabled = lValue;

            chkReportingRequ.Enabled = lValue;
            chkHeadOfDepartment.Enabled = lValue;


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
                    txtcode.Enabled = false;
                    break;
                case ClsUtility.enmFormMode.NormalMode:
                    SetEnable(false);
                    break;
            }
        }
        private void frmDesgination_Load(object sender, EventArgs e)
        {
            try
            {
                ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                SetFormMode(ClsUtility.enmFormMode.NormalMode);
                LoadDesgHichry();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void chkReportingRequ_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ClsUtility.FormMode != ClsUtility.enmFormMode.NormalMode)
                {
                    txtRDesgCode.Enabled = chkReportingRequ.Checked;
                    // txtRDesgName.Enabled = chkReportingRequ.Checked;
                    btnPickRDesg.Enabled = chkReportingRequ.Checked;
                }

                txtchkReportRequd.Text = chkReportingRequ.Checked.ToString();
                if (chkReportingRequ.Checked == false)
                {
                    txtRDesgCode.Text = "SNAT";
                    txtRDesgName.Text = "SNAT Department";
                }
                else
                {
                    if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                    {//
                        Int32 iRow = bsDesgination.Position;
                        txtRDesgCode.Text = "";// dtDesgination.DefaultView[iRow]["ReportCode"].ToString();
                        txtRDesgName.Text = "";
                    }
                    if (ClsUtility.FormMode == ClsUtility.enmFormMode.EditMode)
                    {//
                        Int32 iRow = bsDesgination.Position;
                        txtRDesgCode.Text = dtDesgination.DefaultView[iRow]["ReportCode"] != null ? dtDesgination.DefaultView[iRow]["ReportCode"].ToString() : "";// dtDesgination.DefaultView[iRow]["ReportCode"].ToString();
                        txtRDesgName.Text = dtDesgination.DefaultView[iRow]["ReportName"] != null ? dtDesgination.DefaultView[iRow]["ReportName"].ToString() : "";
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }


        }

        private void chkHeadOfDepartment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtchkHeadOFDepart.Text = chkHeadOfDepartment.Checked.ToString();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtchkHeadOFDepart_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtchkHeadOFDepart.Text.Trim()))
                {
                    chkHeadOfDepartment.Checked = Convert.ToBoolean(txtchkHeadOFDepart.Text);
                }
                else
                {
                    chkHeadOfDepartment.Checked = false;
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtchkReportRequd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtchkReportRequd.Text.Trim()))
                {
                    chkReportingRequ.Checked = Convert.ToBoolean(txtchkReportRequd.Text);
                }
                else
                {
                    chkReportingRequ.Checked = false;
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void txtChkBoxStatus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtChkBoxStatus.Text.Trim()))
                {
                    chkStatus.Checked = Convert.ToBoolean(txtChkBoxStatus.Text);
                }
                else
                {
                    chkStatus.Checked = false;
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
                bsDesgination.AllowNew = true;
                bsDesgination.AddNew();
                ClsUtility.FormMode = ClsUtility.enmFormMode.AddMode;
                chkReportingRequ.Checked = true;
                SetFormMode(ClsUtility.enmFormMode.AddMode);
                txtcode.Focus();
                //ClsDataLayer.setLogAcitivity("Designation Form", ClsSettings.username, "Adding New Designation", "", "New Designation", "Msg");
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtcode.Text.Trim().ToUpper() == "SNAT") { ClsMessage.showMessage("Company designation Cannot Edited!", MessageBoxIcon.Information); return; }
            ClsUtility.FormMode = ClsUtility.enmFormMode.EditMode;
            SetFormMode(ClsUtility.enmFormMode.EditMode);
            txtName.Focus();
            //ClsDataLayer.setLogAcitivity("Designation Form", ClsSettings.username, "Editing Existing Designation", "", "Existing Designation Edited Successfully", "Msg");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtcode.Text.Trim()) == false)
                {
                    if (txtcode.Text.Trim().ToUpper() == "SNAT") { ClsMessage.showMessage("Company designation Cannot deleted!", MessageBoxIcon.Information); return; }
                    if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
                    {
                        Int32 iRow = bsDesgination.Position;
                        bsDesgination.RemoveAt(iRow);
                        // dtDepartment.DefaultView.Delete(iRow);
                        bsDesgination.EndEdit();
                        if (dtDesgination != null && dtDesgination.DefaultView.Count > 0)
                        {
                            if (dtDesgination.GetChanges() != null)
                            {
                                dtDesgination.DefaultView[iRow].BeginEdit();
                                dtDesgination.DefaultView[iRow].EndEdit();
                                bool lReturn = false;
                                strSqlQuery = "SELECT id, code, Name, DepartCode, HeadofDepartment, IsReportingDesg, ReportCode, Remarks, lStatus FROM   SNAT.dbo.T_Designation where 1=2";
                                lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtDesgination);
                                if (lReturn == true)
                                {
                                    ClsMessage.showDeleteMessage();
                                    dtDesgination.AcceptChanges();
                                    ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                                    SetFormMode(ClsUtility.enmFormMode.NormalMode);
                                    FillDataTable();
                                    //ClsDataLayer.setLogAcitivity("Designation Form", ClsSettings.username, "Deleted Existing Designation", "", "Successfully Deleted the Existing Designation", "Msg");
                                }
                                else
                                {
                                    ClsMessage.showMessage("Some problem occurs while deleting please contact system administrator.", MessageBoxIcon.Information);
                                }
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


                if (string.IsNullOrEmpty(txtcode.Text.Trim()))
                {
                    errorProvider1.SetError(txtcode, "Designation code cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Designation", "code", "code", txtcode.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtcode, "Designation code already exists.");
                        ClsMessage.showMessage("Designation code already exists.", MessageBoxIcon.Information);
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    errorProvider1.SetError(txtName, "Designation name cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Designation", "name", "name", txtcode.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtcode, "Designation Name already exists.");
                        ClsMessage.showMessage("Designation Name already exists.", MessageBoxIcon.Information);
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(txtDeptCode.Text.Trim()))
                {
                    errorProvider1.SetError(txtDepartName, "Department name cannot be left blank.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtRDesgCode.Text.Trim()))
                {
                    errorProvider1.SetError(txtRDesgName, "Reporting Designation cannot be left blank.");
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

                int iRow = bsDesgination.Position;
                if (ValidateSave() == false) { return; }
                bsDesgination.EndEdit();
                if (dtDesgination != null && dtDesgination.DefaultView.Count > 0)
                {
                    //txtChkBoxStatus.Text = chkStatus.Checked.ToString();
                    dtDesgination.DefaultView[iRow].BeginEdit();
                    dtDesgination.DefaultView[iRow]["lStatus"] = chkStatus.Checked.ToString();
                    dtDesgination.DefaultView[iRow].EndEdit();

                    if (dtDesgination.GetChanges() != null)
                    {

                        bool lReturn = false;
                        strSqlQuery = "   SELECT id, code, Name, DepartCode, HeadofDepartment, IsReportingDesg, ReportCode, Remarks, lStatus FROM   SNAT.dbo.T_Designation where 1=2";

                        // Int32 iRow = bsDesgination.Position;
                        //dtDesgination.DefaultView[iRow].BeginEdit();
                        //dtDesgination.DefaultView[iRow].EndEdit();



                        lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtDesgination);

                        dtDesgination.AcceptChanges();
                        if (lReturn == true)
                        {
                            ClsMessage.showSaveMessage();
                            //ClsDataLayer.setLogAcitivity("Designation Form", ClsSettings.username, "Designation Created Successfully", "", "New Designation", "Msg");
                            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                            SetFormMode(ClsUtility.enmFormMode.NormalMode);
                            LoadDesgHichry();
                            FillDataTable();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {

            try
            {
                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT dg.id,dg.code  , dg.Name , dg.DepartCode,td.name DepartName , dg.HeadofDepartment , dg.IsReportingDesg" +
                            " , dg.ReportCode,rdg.name ReportName , dg.Remarks , dg.lStatus FROM SNAT.dbo.T_Designation dg (nolock)" +
                            " INNER JOIN dbo.T_Department td  (nolock) ON td.code=dg.DepartCode LEFT OUTER JOIN SNAT.dbo.T_Designation rdg (nolock)" +
                            " ON rdg.code=dg.ReportCode";
                frmsrch.infSqlWhereCondtion = " dg.code <>'SNAT'";
                frmsrch.infSqlOrderBy = " dg.code , dg.Name ";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Department ....";
                frmsrch.infCodeFieldName = "code";
                frmsrch.infDescriptionFieldName = "name";
                frmsrch.infGridFieldName = "id,code,name,DepartName";
                frmsrch.infGridFieldCaption = "id, Designation Code,Designation Name,Department Name";
                frmsrch.infGridFieldSize = "0,100,150,200";
                frmsrch.ShowDialog(this);
                if (frmsrch.DialogResult == DialogResult.OK)
                {
                    if (frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) && frmsrch.infCodeFieldText.Trim() != "")
                    {
                        //txtDesgCode.Text = frmsrch.infCodeFieldText;
                        //txtDesgName.Text = frmsrch.infDescriptionFieldText;
                        DataView dvDesg = new DataView();
                        dvDesg = frmsrch.infSearchReturnDataView;
                        dvDesg.RowFilter = "lSelect=1";
                        //txtDeptCode.Text = string.IsNullOrEmpty(dvDesg[0]["deptcode"].ToString()) == true ? "" : dvDesg[0]["deptcode"].ToString();
                        //txtDeptDesc.Text = string.IsNullOrEmpty(dvDesg[0]["deptname"].ToString()) == true ? "" : dvDesg[0]["deptname"].ToString();
                        if (string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) == false)
                        {
                            int iRow = bsDesgination.Find("Code", frmsrch.infCodeFieldText.Trim());
                            bsDesgination.Position = iRow;
                            dvDesg.RowFilter = "";
                        }
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
            this.Close();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClsMessage.showAskDiscardMessage() == DialogResult.Yes)
                {
                    bsDesgination.CancelEdit();
                    dtDesgination.RejectChanges();
                    ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                    SetFormMode(ClsUtility.enmFormMode.NormalMode);
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnPickDepartment_Click(object sender, EventArgs e)
        {
            try
            {

                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT id,code,name FROM SNAT.dbo.T_Department";
                frmsrch.infSqlWhereCondtion = "  isnull(status,0)=1  and code<>'SNAT'";
                frmsrch.infSqlOrderBy = " Code ";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Department ....";
                frmsrch.infCodeFieldName = "code";
                frmsrch.infDescriptionFieldName = "name";
                frmsrch.infGridFieldName = " id,code,name";
                frmsrch.infGridFieldCaption = " id, Department Code,Department Name";
                frmsrch.infGridFieldSize = "0,100,150";
                frmsrch.ShowDialog(this);
                if (frmsrch.DialogResult == DialogResult.OK)
                {
                    if (frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) && frmsrch.infCodeFieldText.Trim() != "")
                    {
                        txtDeptCode.Text = frmsrch.infCodeFieldText;
                        txtDepartName.Text = frmsrch.infDescriptionFieldText;
                    }
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtDeptCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDeptCode.Text.Trim()) == false)
                {
                    strSqlQuery = "SELECT id,code,name FROM SNAT.dbo.T_Department Where isnull(status,0)=1 and code='" + txtDeptCode.Text.Trim() + "' ";
                    DataTable dtDepartment = new DataTable();
                    dtDepartment = ClsDataLayer.GetDataTable(strSqlQuery);
                    if (dtDepartment != null && dtDepartment.DefaultView.Count > 0)
                    {
                        txtDeptCode.Text = dtDepartment.DefaultView[0]["code"].ToString();
                        txtDepartName.Text = dtDepartment.DefaultView[0]["name"].ToString();
                    }
                    else
                    {
                        ClsMessage.ProjectExceptionMessage("Invalid Department Code!");
                        txtDeptCode.Focus();

                    }
                }
                else
                {
                    txtDepartName.Text = "";
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnPickRDesg_Click(object sender, EventArgs e)
        {
            try
            {

                frmSearch frmsrch = new frmSearch();
                frmsrch.infSqlSelectQuery = "SELECT id, code, Name  FROM   SNAT.dbo.T_Designation";
                frmsrch.infSqlWhereCondtion = "  isnull(lStatus,0)=1  and code<>'SNAT'";
                frmsrch.infSqlOrderBy = " Code ";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Designation ....";
                frmsrch.infCodeFieldName = "code";
                frmsrch.infDescriptionFieldName = "name";
                frmsrch.infGridFieldName = " id,code,name";
                frmsrch.infGridFieldCaption = " id, Designation Code,Designation Name";
                frmsrch.infGridFieldSize = "0,100,150";
                frmsrch.ShowDialog(this);
                txtRDesgCode.Text = frmsrch.infCodeFieldText;
                txtRDesgName.Text = frmsrch.infDescriptionFieldText;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void txtRDesgCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRDesgCode.Text.Trim()) == false)
                {
                    strSqlQuery = "SELECT id,code,name FROM SNAT.dbo.T_Designation Where isnull(lStatus,0)=1 and code='" + txtRDesgCode.Text.Trim() + "' ";
                    DataTable dtDesg = new DataTable();
                    dtDesg = ClsDataLayer.GetDataTable(strSqlQuery);
                    if (dtDesg != null && dtDesg.DefaultView.Count > 0)
                    {
                        txtRDesgCode.Text = dtDesg.DefaultView[0]["code"].ToString();
                        txtRDesgName.Text = dtDesg.DefaultView[0]["name"].ToString();
                    }
                    else
                    {
                        ClsMessage.ProjectExceptionMessage("Invalid Designation Code!");
                        txtRDesgCode.Focus();

                    }
                }
                else
                {
                    txtRDesgName.Text = "";
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void LoadDesgHichry()
        {

            try
            {
                DataSet menuDataSet = new DataSet();
                treeDocHierarchy.Nodes.Clear();
                TreeNode trnode = new TreeNode();

                strSqlQuery = "SELECT dg.id,dg.code , dg.Name , dg.DepartCode,td.name DepartName , dg.HeadofDepartment , dg.IsReportingDesg , dg.ReportCode,rdg.name ReportName ," +
                                " dg.Remarks , dg.lStatus FROM SNAT.dbo.T_Designation dg (nolock) INNER JOIN dbo.T_Department td  (nolock) ON td.code=dg.DepartCode" +
                                " LEFT OUTER JOIN SNAT.dbo.T_Designation rdg ON rdg.code=dg.ReportCode ORDER BY dg.ID,dg.code";

                DataTable dtHeaderMenu = new DataTable();
                dtHeaderMenu = ClsDataLayer.GetDataTable(strSqlQuery);
                DataView dvTreeView = new DataView(), dvTreeViewPreant = new DataView(), dvTreeViewSNAT = new DataView();

                dvTreeView = menuDataSet.DefaultViewManager.CreateDataView(dtHeaderMenu);
                dvTreeViewPreant = menuDataSet.DefaultViewManager.CreateDataView(dtHeaderMenu);
                dvTreeViewSNAT = menuDataSet.DefaultViewManager.CreateDataView(dtHeaderMenu);
                dvTreeViewSNAT = dvTreeViewSNAT.ToTable(true, "ReportCode").DefaultView;
                foreach (DataRowView itemhead in dvTreeViewSNAT)
                {
                    dvTreeView.RowFilter = "code='" + itemhead["ReportCode"].ToString() + "'";
                    foreach (DataRowView item in dvTreeView)
                    {
                        if (item["code"].ToString().ToUpper() != "SNAT")
                        {
                            trnode = new TreeNode();
                            trnode.Text = item["Name"].ToString();
                            trnode.Tag = item["HeadofDepartment"].ToString();
                            trnode.Name = item["code"].ToString();
                            trnode.ImageKey = item["DepartCode"].ToString();
                            trnode.ForeColor = Color.Blue;

                            //if (item["lStatus"].ToString().ToUpper() == "TRUE")
                            //{
                            //    trnode.ForeColor = Color.Red;
                            //}
                            if (item["lStatus"].ToString().ToUpper() == "FALSE")
                            {
                                trnode.ForeColor = Color.Gray;
                            }


                            //dvTreeViewPreant.RowFilter = "code='" + item["ReportCode"].ToString() + "'";
                            //TreeNode[] trPreantNode = null;
                            //if (dvTreeViewPreant.Count > 0)
                            //{
                            //    trPreantNode = treeDocHierarchy.Nodes.Find(dvTreeViewPreant[0]["code"].ToString(), true);
                            //    if (trPreantNode.Length > 0)
                            //    {
                            //        if (trPreantNode[0].ForeColor == Color.Red)
                            //        {
                            //            trnode.ForeColor = Color.Red;
                            //        }

                            //        if (trPreantNode[0].ForeColor == Color.Gray)
                            //        {
                            //            trnode.ForeColor = Color.Gray;
                            //        }
                            //        trPreantNode[0].Nodes.Add(trnode);
                            //    }

                            //}
                        }
                        else
                        {
                            trnode = new TreeNode();
                            trnode = new TreeNode();
                            trnode.Text = item["Name"].ToString();
                            trnode.Tag = item["HeadofDepartment"].ToString();
                            trnode.Name = item["code"].ToString();
                            trnode.ImageKey = item["DepartCode"].ToString();
                            treeDocHierarchy.Nodes.Add(trnode);
                        }

                    }

                    dvTreeViewPreant.RowFilter = "ReportCode='" + itemhead["ReportCode"].ToString() + "' AND CODE<>'SNAT'";
                    foreach (DataRowView item in dvTreeViewPreant)
                    {
                        if (item["code"].ToString().ToUpper() != "SNAT")
                        {
                            trnode = new TreeNode();
                            trnode.Text = item["Name"].ToString();
                            trnode.Tag = item["HeadofDepartment"].ToString();
                            trnode.Name = item["code"].ToString();
                            trnode.ImageKey = item["DepartCode"].ToString();
                            trnode.ForeColor = Color.Blue;

                            if (item["lStatus"].ToString().ToUpper() == "FALSE")
                            {
                                trnode.ForeColor = Color.Gray;
                            }

                            TreeNode[] trPreantNode2 = null;
                            if (dvTreeViewPreant.Count > 0)
                            {
                                trPreantNode2 = treeDocHierarchy.Nodes.Find(itemhead["ReportCode"].ToString(), true);
                                if (trPreantNode2.Length > 0)
                                {
                                    if (trPreantNode2[0].ForeColor == Color.Red)
                                    {
                                        trnode.ForeColor = Color.Red;
                                    }

                                    if (trPreantNode2[0].ForeColor == Color.Gray)
                                    {
                                        trnode.ForeColor = Color.Gray;
                                    }
                                    trPreantNode2[0].Nodes.Add(trnode);
                                }

                            }

                        }
                        else
                        {
                            trnode = new TreeNode();
                            trnode = new TreeNode();
                            trnode.Text = item["Name"].ToString();
                            trnode.Tag = item["HeadofDepartment"].ToString();
                            trnode.Name = item["code"].ToString();
                            trnode.ImageKey = item["DepartCode"].ToString();
                            treeDocHierarchy.Nodes.Add(trnode);
                        }

                    }
                }





                //foreach (DataRowView item in dvTreeView)
                //{
                //    if (item["code"].ToString().ToUpper() != "SNAT")
                //    {
                //        trnode = new TreeNode();
                //        trnode.Text = item["Name"].ToString();
                //        trnode.Tag = item["HeadofDepartment"].ToString();
                //        trnode.Name = item["code"].ToString();
                //        trnode.ImageKey = item["DepartCode"].ToString();
                //        trnode.ForeColor = Color.Blue;

                //        //if (item["lStatus"].ToString().ToUpper() == "TRUE")
                //        //{
                //        //    trnode.ForeColor = Color.Red;
                //        //}
                //        if (item["lStatus"].ToString().ToUpper() == "FALSE")
                //        {
                //            trnode.ForeColor = Color.Gray;
                //        }


                //        dvTreeViewPreant.RowFilter = "code='" + item["ReportCode"].ToString() + "'";
                //        TreeNode[] trPreantNode = null;
                //        if (dvTreeViewPreant.Count > 0)
                //        {
                //            trPreantNode = treeDocHierarchy.Nodes.Find(dvTreeViewPreant[0]["code"].ToString(), true);
                //            if (trPreantNode.Length > 0)
                //            {
                //                if (trPreantNode[0].ForeColor == Color.Red)
                //                {
                //                    trnode.ForeColor = Color.Red;
                //                }

                //                if (trPreantNode[0].ForeColor == Color.Gray)
                //                {
                //                    trnode.ForeColor = Color.Gray;
                //                }
                //                trPreantNode[0].Nodes.Add(trnode);
                //            }

                //        }
                //    }
                //    else
                //    {
                //        trnode = new TreeNode();
                //        trnode = new TreeNode();
                //        trnode.Text = item["Name"].ToString();
                //        trnode.Tag = item["HeadofDepartment"].ToString();
                //        trnode.Name = item["code"].ToString();
                //        trnode.ImageKey = item["DepartCode"].ToString();
                //        treeDocHierarchy.Nodes.Add(trnode);
                //    }

                //}

                treeDocHierarchy.ExpandAll();



            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);

            }



        }

        private void txtcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.NormalMode)
                {
                    if (string.IsNullOrEmpty(txtcode.Text.Trim()) == false)
                    {

                        TreeNode[] trPreantNode = null;
                        trPreantNode = treeDocHierarchy.Nodes.Find(txtcode.Text.Trim(), true);
                        treeDocHierarchy.Focus();
                        if (trPreantNode != null)
                        { treeDocHierarchy.SelectedNode = trPreantNode[0]; }
                    }




                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);

            }
        }

        private void treeDocHierarchy_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (treeDocHierarchy.SelectedNode != null && treeDocHierarchy.SelectedNode.Name != null && treeDocHierarchy.SelectedNode.Name != "")
                {
                    DataRow[] drRow = dtDesgination.Select("Code='" + treeDocHierarchy.SelectedNode.Name.ToString() + "'");
                    Int32 iRow = dtDesgination.Rows.IndexOf(drRow[0]);
                    bsDesgination.Position = iRow;

                }
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
                LoadDesgHichry();
                FillDataTable();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);

            }
        }
    }
}
