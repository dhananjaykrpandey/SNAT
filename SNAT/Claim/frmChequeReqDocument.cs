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

namespace SNAT.Claim
{
    public partial class frmChequeReqDocument : Form
    {
        BindingSource bsClaimDoc = new BindingSource();
        string strSqlQuery = "";
        DataTable dtClaimDoc = new DataTable();


        public frmChequeReqDocument()
        {
            InitializeComponent();
            BindControl();
        }
        void FillDepartment()
        {
            try
            {
                string strSqlQuery = "SELECT id , code , name , status , mandatory , remarks FROM SNAT.dbo.M_ChequeDocType ORDER BY code";
                dtClaimDoc = ClsDataLayer.GetDataTable(strSqlQuery);
                bsClaimDoc.DataSource = dtClaimDoc.DefaultView;
                bindingNavigatorMain.BindingSource = bsClaimDoc;
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
                txtCode.DataBindings.Add("Text", bsClaimDoc, "code", false, DataSourceUpdateMode.OnPropertyChanged);
                txtName.DataBindings.Add("Text", bsClaimDoc, "name", false, DataSourceUpdateMode.OnPropertyChanged);
                txtRemarks.DataBindings.Add("Text", bsClaimDoc, "remarks", false, DataSourceUpdateMode.OnPropertyChanged);
                txtStatus.DataBindings.Add("Text", bsClaimDoc, "status", false, DataSourceUpdateMode.OnPropertyChanged);
                txtMandatory.DataBindings.Add("Text", bsClaimDoc, "mandatory", false, DataSourceUpdateMode.OnPropertyChanged);
                // chkStatus.DataBindings.Add("Checked", bsSchool, "status", false);
                grdList.DataSource = bsClaimDoc;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }
        private void SetEnable(bool lValue)
        {
            txtCode.Enabled = lValue;
            txtName.Enabled = lValue;
            txtRemarks.Enabled = lValue;
            chkStatus.Enabled = lValue;
            chkMandatory.Enabled = lValue;
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
                    txtCode.Enabled = false;
                    break;
                case ClsUtility.enmFormMode.NormalMode:
                    SetEnable(false);
                    break;
            }
        }
        private void frmClaimDocument_Load(object sender, EventArgs e)
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
                bsClaimDoc.AllowNew = true;
                bsClaimDoc.AddNew();
                ClsUtility.FormMode = ClsUtility.enmFormMode.AddMode;
                SetFormMode(ClsUtility.enmFormMode.AddMode);
                txtCode.Focus();
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
                txtName.Focus();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        bool ValidateDelete()
        {

            try
            {

                if (ClsUtility.IsCodeValueExists("SNAT.dbo.T_ChequeDocuments", "doccode", "doccode", txtCode.Text.Trim()) == true)
                {

                    ClsMessage.showMessage("Document already in use cannot delete!!.", MessageBoxIcon.Information);
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode.Text.Trim()) == false)
                {

                    if (ValidateDelete() == false) { return; }
                    if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
                    {
                        Int32 iRow = bsClaimDoc.Position;
                        bsClaimDoc.RemoveAt(iRow);
                        // dtSchool.DefaultView.Delete(iRow);
                        bsClaimDoc.EndEdit();
                        if (dtClaimDoc != null && dtClaimDoc.DefaultView.Count > 0)
                        {
                            if (dtClaimDoc.GetChanges() != null)
                            {

                                bool lReturn = false;
                                strSqlQuery = "SELECT id , code , name , status , mandatory , remarks FROM SNAT.dbo.M_ChequeDocType where 1=2";
                                lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtClaimDoc);

                                dtClaimDoc.AcceptChanges();
                                if (lReturn == true)
                                {
                                    ClsMessage.showDeleteMessage();
                                    ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                                    SetFormMode(ClsUtility.enmFormMode.NormalMode);
                                    FillDepartment();
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


                if (string.IsNullOrEmpty(txtCode.Text.Trim()))
                {
                    errorProvider1.SetError(txtCode, "Claim document code cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.M_ChequeDocType", "code", "code", txtCode.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtCode, "Claim document code already exists.");
                        ClsMessage.showMessage("Claim document code already exists.", MessageBoxIcon.Information);
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    errorProvider1.SetError(txtName, "Claim document name cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.M_ChequeDocType", "name", "name", txtName.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtName, "Claim document Name already exists.");
                        ClsMessage.showMessage("Claim document Name already exists.", MessageBoxIcon.Information);
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
                int iRow = bsClaimDoc.Position;

                if (ValidateSave() == false) { return; }

                bsClaimDoc.EndEdit();
                if (dtClaimDoc != null && dtClaimDoc.DefaultView.Count > 0)
                {
                    dtClaimDoc.DefaultView[iRow].BeginEdit();
                    dtClaimDoc.DefaultView[iRow]["status"] = chkStatus.Checked.ToString();
                    dtClaimDoc.DefaultView[iRow]["mandatory"] = chkMandatory.Checked.ToString();
                    dtClaimDoc.DefaultView[iRow].EndEdit();
                    if (dtClaimDoc.GetChanges() != null)
                    {

                        bool lReturn = false;
                        strSqlQuery = "SELECT id , code , name , status , mandatory , remarks FROM SNAT.dbo.M_ChequeDocType where 1=2";
                        lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtClaimDoc);

                        dtClaimDoc.AcceptChanges();
                        if (lReturn == true)
                        {
                            ClsMessage.showSaveMessage();
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
                    bsClaimDoc.CancelEdit();
                    dtClaimDoc.RejectChanges();
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
                frmsrch.infSqlSelectQuery = "SELECT id , code , name , status , mandatory , remarks FROM SNAT.dbo.M_ChequeDocType";
                frmsrch.infSqlWhereCondtion = " ";
                frmsrch.infSqlOrderBy = " code,name ";
                frmsrch.infMultiSelect = false;
                frmsrch.infSearchFormName = "Search Cheque Document Type ....";
                frmsrch.infCodeFieldName = "code";
                frmsrch.infDescriptionFieldName = "name";
                frmsrch.infGridFieldName = "id,code,name";
                frmsrch.infGridFieldCaption = "id, Code, Name";
                frmsrch.infGridFieldSize = "0,100,150";
                frmsrch.ShowDialog(this);
                //txtDesgCode.Text = frmsrch.infCodeFieldText;
                //txtDesgName.Text = frmsrch.infDescriptionFieldText;
                DataView dvDesg = new DataView();
                dvDesg = frmsrch.infSearchReturnDataView;
                if (dvDesg != null && dvDesg.Count > 0)
                {


                    dvDesg.RowFilter = "lSelect=1";
                    //txtDeptCode.Text = string.IsNullOrEmpty(dvDesg[0]["deptcode"].ToString()) == true ? "" : dvDesg[0]["deptcode"].ToString();
                    //txtDeptDesc.Text = string.IsNullOrEmpty(dvDesg[0]["deptname"].ToString()) == true ? "" : dvDesg[0]["deptname"].ToString();
                    if (string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) == false)
                    {
                        int iRow = bsClaimDoc.Find("Code", frmsrch.infCodeFieldText.Trim());
                        bsClaimDoc.Position = iRow;
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
                txtStatus.Text = chkStatus.Checked.ToString();
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
                if (!string.IsNullOrEmpty(txtStatus.Text.Trim()))
                {
                    chkStatus.Checked = Convert.ToBoolean(txtStatus.Text);
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

        private void txtMandatory_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(txtMandatory.Text.Trim()))
                {
                    chkMandatory.Checked = Convert.ToBoolean(txtMandatory.Text);
                }
                else
                {
                    chkMandatory.Checked = false;
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }

        private void chkMandatory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtMandatory.Text = chkMandatory.Checked.ToString();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        
    }
}
