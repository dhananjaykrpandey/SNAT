using SNAT.Comman_Classes;
using SNAT.Comman_Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SNAT.Employee
{
    public partial class frmEmailAutorization : Form
    {
        BindingSource bsEAlter = new BindingSource();
        string strSqlQuery = "";
        DataTable dtEAlert = new DataTable();
        ErrorProvider errorProvider1 = new ErrorProvider();
        DataTable dtEAlertDetails = new DataTable();
        public frmEmailAutorization()
        {
            InitializeComponent();
            BindControl();
        }
        private void FillDataTable()
        {
            try
            {

                string strSqlQuery = "SELECT Distinct tesa.EmpNationalID, tesa.EmpNo,ted.name empname,tesa.Status, tesa.Remarks " + Environment.NewLine +
                                      " FROM SNAT.dbo.T_Email_SMS_Alert AS tesa WITH (nolock)  " + Environment.NewLine +
                                      " LEFT OUTER JOIN SNAT.dbo.T_EmployeeDetails ted ON ted.employeeno=tesa.EmpNo AND ted.nationalid=tesa.EmpNationalID   " + Environment.NewLine +
                                      " ORDER BY EmpNationalID, EmpNo,ted.name";
                dtEAlert = ClsDataLayer.GetDataTable(strSqlQuery);
                //  dtEAlter = ClsDataLayer.GetDataTable(strSqlQuery);
                bsEAlter.DataSource = dtEAlert.DefaultView;
                bindingNavigatorMain.BindingSource = bsEAlter;
                if (dtEAlert != null && dtEAlert.DefaultView.Count > 0)
                {
                    FillAlterGrid(dtEAlert.DefaultView[0]["EmpNationalID"].ToString(), dtEAlert.DefaultView[0]["EmpNo"].ToString());
                }

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
                txtEmployeeNo.DataBindings.Add("Text", bsEAlter, "EmpNo", false, DataSourceUpdateMode.OnValidation);
                txtNationalId.DataBindings.Add("Text", bsEAlter, "EmpNationalID", false, DataSourceUpdateMode.OnValidation);
                txtEmployeeName.DataBindings.Add("Text", bsEAlter, "empname", false, DataSourceUpdateMode.OnValidation);
                txtRemarks.DataBindings.Add("Text", bsEAlter, "Remarks", false, DataSourceUpdateMode.OnPropertyChanged);
                txtChkBoxStatus.DataBindings.Add("Text", bsEAlter, "Status", false, DataSourceUpdateMode.OnPropertyChanged);
                grdList.DataSource = bsEAlter;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }


        private void FillAlterGrid(string strEmployeeNationalID, string strEmpNo)
        {
            try
            {
                strSqlQuery = "SELECT  ROW_NUMBER() OVER (ORDER BY tesa.id) srNo ,tesa.id, tesa.EmpNationalID, tesa.EmpNo, tesa.AlertyID,mat.alert, tesa.EmailAlert, tesa.SmsAlert, tesa.Status, tesa.Remarks,tesa.updateby,tesa.updateddate " + Environment.NewLine +
                              " FROM SNAT.dbo.T_Email_SMS_Alert AS tesa WITH (nolock) " + Environment.NewLine +
                              " LEFT OUTER JOIN SNAT.dbo.M_AlertType mat (nolock) ON tesa.AlertyID = mat.id" + Environment.NewLine +
                              " Where tesa.EmpNationalID='" + strEmployeeNationalID + "' and  tesa.EmpNo='" + strEmpNo + "'";
                dtEAlertDetails = ClsDataLayer.GetDataTable(strSqlQuery);
                grdAlert.DataSource = dtEAlertDetails.DefaultView;


            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SetEnable(bool lValue)
        {
            txtEmployeeNo.Enabled = lValue;
            txtRemarks.Enabled = lValue;
            chkStatus.Enabled = lValue;

            grdAlert.ReadOnly =! lValue;

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
            errorProvider1.Clear();
            btnRefresh.Enabled = !lValue;
            btnDelete.Enabled = !lValue;
        }
        private void SetFormMode(ClsUtility.enmFormMode _FormMode)
        {
            switch (_FormMode)
            {
                case ClsUtility.enmFormMode.AddMode:
                    SetEnable(true);
                    txtEmployeeNo.Focus();
                    break;
                case ClsUtility.enmFormMode.EditMode:
                    SetEnable(true);
                    txtEmployeeNo.Enabled = false;
                    break;
                case ClsUtility.enmFormMode.NormalMode:
                    SetEnable(false);
                    break;
            }
        }
        private void frmEmailAutorization_Load(object sender, EventArgs e)
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

        private void txtChkBoxStatus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtChkBoxStatus.Text.Trim()))
                {
                    chkStatus.Checked = Convert.ToBoolean(txtChkBoxStatus.Text.Trim());
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bsEAlter.AllowNew = true;
                bsEAlter.AddNew();
                ClsUtility.FormMode = ClsUtility.enmFormMode.AddMode;
                SetFormMode(ClsUtility.enmFormMode.AddMode);
                chkStatus.Checked = true;

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
                ClsUtility.FormMode = ClsUtility.enmFormMode.EditMode;
                SetFormMode(ClsUtility.enmFormMode.EditMode);
                txtEmployeeNo.Enabled = false;

                //ClsDataLayer.setLogAcitivity("Employee Details", ClsSettings.username, "Editing Existing Employee", "", " ", "Msg");
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                //ClsDataLayer.setLogAcitivity("Employee Details", ClsSettings.username, ""+ex.Message.ToString(), "", "Some Problem Occured while Editing Existing Employee Details", "Msg");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //    try
            //    {
            //        if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
            //        {
            //            Int32 iRow = bsEAlter.Position;
            //            bsEAlter.RemoveAt(iRow);
            //            // dtDepartment.DefaultView.Delete(iRow);
            //            bsEAlter.EndEdit();
            //            if (dtEmployee != null && dtEmployee.DefaultView.Count > 0)
            //            {
            //                if (dtEmployee.GetChanges() != null)
            //                {
            //                    dtEmployee.DefaultView[iRow].BeginEdit();
            //                    dtEmployee.DefaultView[iRow].EndEdit();
            //                    bool lReturn = false;
            //                    strSqlQuery = "SELECT ed.id, ed.employeeno, ed.name, ed.dob, ed.deptcode, ed.desigcode, ed.contactno1, ed.contactno2, ed.email, ed.physicaladdress, ed.dateofjoining, ed.wrokstatus, ed.leaveingdate,ed.imagelocation FROM SNAT.dbo.T_EmployeeDetails ed (nolock) where 1=2";
            //                    lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtEmployee);

            //                    if (lReturn == true)
            //                    {
            //                        ClsMessage.showDeleteMessage();
            //                        ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
            //                        SetFormMode(ClsUtility.enmFormMode.NormalMode);
            //                        dtEmployee.AcceptChanges();
            //                        FillDataTable();
            //                        //ClsDataLayer.setLogAcitivity("Employee Details", ClsSettings.username, "Deleted Existing Employee Details", "", "", "Msg");
            //                    }
            //                    else
            //                    {
            //                        ClsMessage.showMessage("Some problem occurs while deleting please contact system administrator.", MessageBoxIcon.Information);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //        ClsMessage.ProjectExceptionMessage(ex);
            //    }
        }

        private bool ValidateSave()
        {
            try
            {

                errorProvider1.Clear();


                if (string.IsNullOrEmpty(txtEmployeeNo.Text.Trim()))
                {
                    errorProvider1.SetError(txtEmployeeNo, "Employee no code cannot be left blank.");
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

                grdAlert.Update();
                if (ValidateSave() == false) { return; }
                bsEAlter.EndEdit();
                if (dtEAlertDetails != null && dtEAlertDetails.DefaultView.Count > 0)
                {
                    Int32 iRow = bsEAlter.Position;

                    foreach (DataRowView drvAlert in dtEAlertDetails.DefaultView)
                    {
                        drvAlert.BeginEdit();
                        drvAlert["Status"] = chkStatus.Checked;
                        drvAlert["Remarks"] = txtRemarks.Text.Trim();
                        drvAlert["updateby"] = ClsSettings.username;
                        drvAlert["updateddate"] = DateTime.Now;
                        drvAlert.EndEdit();
                    }



                    if (dtEAlertDetails.GetChanges() != null)
                    {

                        bool lReturn = false;
                        strSqlQuery = "SELECT id, EmpNationalID, EmpNo, AlertyID, EmailAlert, SmsAlert, Status, Remarks,updateby, updateddate FROM SNAT.dbo.T_Email_SMS_Alert AS tesa WITH (nolock)  where 1=2";
                        lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtEAlertDetails);
                        dtEAlertDetails.AcceptChanges();
                        if (lReturn == true)
                        {

                            ClsMessage.showSaveMessage();
                            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                            SetFormMode(ClsUtility.enmFormMode.NormalMode);
                            FillDataTable();
                            FillAlterGrid(txtNationalId.Text.Trim(), txtEmployeeNo.Text.Trim());
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
                bsEAlter.CancelEdit();
                dtEAlertDetails.RejectChanges();
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
                frmsrch.infSqlSelectQuery = "SELECT Distinct tesa.EmpNationalID, tesa.EmpNo,ted.name empname " + Environment.NewLine +
                                      " FROM SNAT.dbo.T_Email_SMS_Alert AS tesa WITH (nolock)  " + Environment.NewLine +
                                      " LEFT OUTER JOIN SNAT.dbo.T_EmployeeDetails ted ON ted.employeeno=tesa.EmpNo AND ted.nationalid=tesa.EmpNationalID   ";
                frmsrch.infSqlWhereCondtion = "";
                frmsrch.infSqlOrderBy = " EmpNationalID, EmpNo,ted.name ";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Employee ....";
                frmsrch.infCodeFieldName = "EmpNationalID";
                frmsrch.infDescriptionFieldName = "EmpNo";
                frmsrch.infGridFieldName = "EmpNationalID,EmpNo,empname ";
                frmsrch.infGridFieldCaption = " National ID , Employee No,Employee Name";
                frmsrch.infGridFieldSize = "100,150,200";
                frmsrch.ShowDialog(this);
                //txtDesgCode.Text = frmsrch.infCodeFieldText;
                //txtDesgName.Text = frmsrch.infDescriptionFieldText;
                if (frmsrch.DialogResult == DialogResult.OK)
                {
                    if (frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) && frmsrch.infCodeFieldText.Trim() != "")
                    {
                        DataView dvDesg = new DataView();
                        dvDesg = frmsrch.infSearchReturnDataView;
                        dvDesg.RowFilter = "lSelect=1";
                        if (string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) == false)
                        {
                            int iRow = bsEAlter.Find("EmpNationalID", frmsrch.infCodeFieldText.Trim());
                            bsEAlter.Position = iRow;
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grdList_SelectionChanging(object sender, Telerik.WinControls.UI.GridViewSelectionCancelEventArgs e)
        {

            try
            {
                int IRow = 0;
                IRow = grdList.CurrentRow.Index;
                if (dtEAlert != null && dtEAlert.DefaultView.Count > 0)
                {
                    FillAlterGrid(dtEAlert.DefaultView[IRow]["EmpNationalID"].ToString(), dtEAlert.DefaultView[IRow]["EmpNo"].ToString());
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
                FillDataTable();
                FillAlterGrid(txtNationalId.Text.Trim(), txtEmployeeNo.Text.Trim());
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
    }
}
