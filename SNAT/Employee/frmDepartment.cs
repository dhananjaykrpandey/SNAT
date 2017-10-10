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
    public partial class frmDepartment : Form
    {
        BindingSource bsDepartmetn = new BindingSource();
        string strSqlQuery = "";
        DataTable dtDepartment = new DataTable();
        public frmDepartment()
        {
            InitializeComponent();
            BindControl();
        }
            void FillDepartment()
            {
                try
                {
                    string strSqlQuery = "SELECT id,code,name,remarks,status FROM SNAT.dbo.T_Department order by code";
                    dtDepartment = ClsDataLayer.GetDataTable(strSqlQuery);
                    bsDepartmetn.DataSource = dtDepartment.DefaultView;
                    bindingNavigatorMain.BindingSource = bsDepartmetn;
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
                FillDepartment();
                txtcode.DataBindings.Add("Text", bsDepartmetn, "code", false, DataSourceUpdateMode.OnValidation);
                txtName.DataBindings.Add("Text", bsDepartmetn, "name", false, DataSourceUpdateMode.OnValidation);
                txtRemarks.DataBindings.Add("Text", bsDepartmetn, "remarks", false, DataSourceUpdateMode.OnValidation);
                txtChkBoxStatus.DataBindings.Add("Text", bsDepartmetn, "status", false, DataSourceUpdateMode.OnValidation);
                // chkStatus.DataBindings.Add("Checked", bsDepartmetn, "status", false);
                grdList.DataSource = bsDepartmetn;
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

        private void frmDepartment_Load(object sender, EventArgs e)
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bsDepartmetn.AllowNew = true;
                bsDepartmetn.AddNew();
                ClsUtility.FormMode = ClsUtility.enmFormMode.AddMode;
                SetFormMode(ClsUtility.enmFormMode.AddMode);
                txtcode.Focus();
              //ClsDataLayer.setLogAcitivity("Department Details", ClsSettings.username, "New Department", "", "Accessed to Add New Department", "Msg");
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
              //ClsDataLayer.setLogAcitivity("Department Details", ClsSettings.username, ""+ex.Message.ToString(), "", "Please Contact System Administrator", "Ex");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcode.Text.Trim().ToUpper() == "SNAT") { ClsMessage.showMessage("Company department Cannot be edited!", MessageBoxIcon.Information); return; }
                // bsDepartmetn.AllowEdit = true;
                // bsDepartmetn.e();
                ClsUtility.FormMode = ClsUtility.enmFormMode.EditMode;
                SetFormMode(ClsUtility.enmFormMode.EditMode);
                txtName.Focus();
              //ClsDataLayer.setLogAcitivity("Department Details", ClsSettings.username, "Editing Existing Department", "", "Edited The Existing Department", "Msg");
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
              //ClsDataLayer.setLogAcitivity("Department Details", ClsSettings.username, "" + ex.Message.ToString(), "", "Please Contact System Administrator", "Ex");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtcode.Text.Trim()) == false)
                {
                    if (txtcode.Text.Trim().ToUpper() == "SNAT") { ClsMessage.showMessage("Company department Cannot be deleted!", MessageBoxIcon.Information); return; }
                    if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
                    {
                        Int32 iRow = bsDepartmetn.Position;
                        bsDepartmetn.RemoveAt(iRow);
                        // dtDepartment.DefaultView.Delete(iRow);
                        bsDepartmetn.EndEdit();
                        if (dtDepartment != null && dtDepartment.DefaultView.Count > 0)
                        {
                            if (dtDepartment.GetChanges() != null)
                            {

                                bool lReturn = false;
                                strSqlQuery = "SELECT id,code,name,remarks,status FROM SNAT.dbo.T_Department order by code";
                                lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtDepartment);

                                dtDepartment.AcceptChanges();
                                if (lReturn == true)
                                {
                                    ClsMessage.showDeleteMessage();
                                    ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                                    SetFormMode(ClsUtility.enmFormMode.NormalMode);
                                    FillDepartment();
                                  //ClsDataLayer.setLogAcitivity("Department Details", ClsSettings.username, "Deleted Department", "", "Successfully Deleted Department", "Msg");
                                }
                                else
                                {
                                    ClsMessage.showMessage("Some problem occurs while deleting please contact system administrator.", MessageBoxIcon.Information);
                                  //ClsDataLayer.setLogAcitivity("Department Details", ClsSettings.username, "Failed to Delete Department", "", "Some problem occurs while deleting please contact system administrator.", "Ex");
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
                    errorProvider1.SetError(txtcode, "Department code cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Department", "code", "code", txtcode.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtcode, "Department code already exists.");
                        ClsMessage.showMessage("Department code already exists.", MessageBoxIcon.Information);
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    errorProvider1.SetError(txtName, "Department name cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_Department", "name", "name", txtName.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtName, "Department Name already exists.");
                        ClsMessage.showMessage("Department Name already exists.", MessageBoxIcon.Information);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                txtChkBoxStatus.Text = chkStatus.Checked.ToString();

                if (ValidateSave() == false) { return; }

                bsDepartmetn.EndEdit();
                if (dtDepartment != null && dtDepartment.DefaultView.Count > 0)
                {
                    if (dtDepartment.GetChanges() != null)
                    {

                        bool lReturn = false;
                        strSqlQuery = "SELECT id,code,name,remarks,status FROM SNAT.dbo.T_Department where 1=2";
                        lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtDepartment);

                        dtDepartment.AcceptChanges();
                        if (lReturn == true)
                        {
                            ClsMessage.showSaveMessage();
                          //ClsDataLayer.setLogAcitivity("Department Details", ClsSettings.username, "Department Details of Swazi Observer", "", "New Department Created Successfully", "Msg");
                            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                            SetFormMode(ClsUtility.enmFormMode.NormalMode);
                            FillDepartment();
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
                    bsDepartmetn.CancelEdit();
                    dtDepartment.RejectChanges();
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
                frmsrch.infSqlSelectQuery = "SELECT id,code,name,remarks,status FROM SNAT.dbo.T_Department";
                frmsrch.infSqlWhereCondtion = " code<>'SNAT'";
                frmsrch.infSqlOrderBy = " code,name ";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Department ....";
                frmsrch.infCodeFieldName = "code";
                frmsrch.infDescriptionFieldName = "name";
                frmsrch.infGridFieldName = "id,code,name";
                frmsrch.infGridFieldCaption = "id, Department Code,Department Name";
                frmsrch.infGridFieldSize = "0,100,150";
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
                            int iRow = bsDepartmetn.Find("Code", frmsrch.infCodeFieldText.Trim());
                            bsDepartmetn.Position = iRow;
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
            try
            {

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtChkBoxStatus.Text = chkStatus.Checked.ToString();
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                FillDepartment();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
    }
}
