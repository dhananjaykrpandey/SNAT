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

namespace SNAT.Document
{
    public partial class frmDocumentType : Form
    {
        BindingSource bsSchool = new BindingSource();
        string strSqlQuery = "";
        DataTable dtSchool = new DataTable();
        public frmDocumentType()
        {
            InitializeComponent();
            BindControl();
        }

        private void frmDocumentType_Load(object sender, EventArgs e)
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
        
        void FillDocument()
        {
            try
            {
                string strSqlQuery = "SELECT  id,code,name,status,remarks FROM SNAT.dbo.M_DocumentType sc ORDER BY sc.code";
                dtSchool = ClsDataLayer.GetDataTable(strSqlQuery);
                bsSchool.DataSource = dtSchool.DefaultView;
                bindingNavigatorMain.BindingSource = bsSchool;
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
                FillDocument();
                txtCode.DataBindings.Add("Text", bsSchool, "code", false, DataSourceUpdateMode.OnValidation);
                txtName.DataBindings.Add("Text", bsSchool, "name", false, DataSourceUpdateMode.OnValidation);
                txtRemarks.DataBindings.Add("Text", bsSchool, "remarks", false, DataSourceUpdateMode.OnValidation);
                txtStatus.DataBindings.Add("Text", bsSchool, "status", false, DataSourceUpdateMode.OnPropertyChanged);
                // chkStatus.DataBindings.Add("Checked", bsSchool, "status", false);
                grdList.DataSource = bsSchool;
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
                    txtCode.Enabled = false;
                    break;
                case ClsUtility.enmFormMode.NormalMode:
                    SetEnable(false);
                    break;
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
                bsSchool.AllowNew = true;
                bsSchool.AddNew();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode.Text.Trim()) == false)
                {

                    if (ClsMessage.showAskDeleteMessage() == DialogResult.Yes)
                    {
                        Int32 iRow = bsSchool.Position;
                        bsSchool.RemoveAt(iRow);
                        // dtSchool.DefaultView.Delete(iRow);
                        bsSchool.EndEdit();
                        if (dtSchool != null && dtSchool.DefaultView.Count > 0)
                        {
                            if (dtSchool.GetChanges() != null)
                            {

                                bool lReturn = false;
                                strSqlQuery = "SELECT  id,code,name,status,remarks FROM SNAT.dbo.M_DocumentType sc where 1=2";
                                lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtSchool);

                                dtSchool.AcceptChanges();
                                if (lReturn == true)
                                {
                                    ClsMessage.showDeleteMessage();
                                    ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                                    SetFormMode(ClsUtility.enmFormMode.NormalMode);
                                    FillDocument();
                                }
                                else
                                {
                                    ClsMessage.showMessage("Some problem occurs while deleting please contact system administrator.");
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
                    errorProvider1.SetError(txtCode, "Document code cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.M_DocumentType", "code", "code", txtCode.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtCode, "Document code already exists.");
                        ClsMessage.showMessage("Document code already exists.");
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    errorProvider1.SetError(txtName, "Document name cannot be left blank.");
                    return false;
                }
                if (ClsUtility.FormMode == ClsUtility.enmFormMode.AddMode)
                {
                    if (ClsUtility.IsCodeValueExists("SNAT.dbo.M_DocumentType", "name", "name", txtName.Text.Trim()) == true)
                    {
                        errorProvider1.SetError(txtName, "Document Name already exists.");
                        ClsMessage.showMessage("Document Name already exists.");
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
                int iRow = bsSchool.Position;

                if (ValidateSave() == false) { return; }

                bsSchool.EndEdit();
                if (dtSchool != null && dtSchool.DefaultView.Count > 0)
                {
                    dtSchool.DefaultView[iRow].BeginEdit();
                    dtSchool.DefaultView[iRow]["status"] = chkStatus.Checked.ToString();
                    dtSchool.DefaultView[iRow].EndEdit();
                    if (dtSchool.GetChanges() != null)
                    {

                        bool lReturn = false;
                        strSqlQuery = "SELECT  id,code,name,status,remarks FROM SNAT.dbo.M_DocumentType where 1=2";
                        lReturn = ClsDataLayer.UpdateDataAdapter(strSqlQuery, dtSchool);

                        dtSchool.AcceptChanges();
                        if (lReturn == true)
                        {
                            ClsMessage.showSaveMessage();
                            ClsUtility.FormMode = ClsUtility.enmFormMode.NormalMode;
                            SetFormMode(ClsUtility.enmFormMode.NormalMode);
                            FillDocument();
                        }
                        else
                        {
                            ClsMessage.showMessage("Some problem occurs while saving please contact system administrator.");
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
                    bsSchool.CancelEdit();
                    dtSchool.RejectChanges();
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
                frmsrch.infSqlSelectQuery = "SELECT  id,code,name,status,remarks FROM SNAT.dbo.M_DocumentType";
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
                        int iRow = bsSchool.Find("Code", frmsrch.infCodeFieldText.Trim());
                        bsSchool.Position = iRow;
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                FillDocument();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
    }
    }

