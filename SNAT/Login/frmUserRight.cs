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
   
    public partial class frmUserRight : Form
    {
        DataTable dtMenuDetails = new DataTable();
        DataTable dtUserRightsDetails = new DataTable();
        DataSet mdsCreateDataView = new DataSet();
        string strSQlQuery = "";
        public frmUserRight()
        {
            InitializeComponent();
        }

        private void frmUserRight_Load(object sender, EventArgs e)
        {
            try
            {

                strSQlQuery = "  Select Distinct  HeaderID, HeaderName, HeaderPosition, ChiledID,childname,childposition  from " +
                              " SNAT.dbo.vw_MenuDetails   order by HeaderPosition,childposition";

                dtMenuDetails = ClsDataLayer.GetDataTable(strSQlQuery);

                DataColumn dcRights = new DataColumn("Rigths", Type.GetType("System.Boolean"));
                dcRights.DefaultValue = false;
                dtMenuDetails.Columns.Add(dcRights);

                grdRights.DataSource = dtMenuDetails.DefaultView;

                //clsFolderHierarchy.treeView = treeFolderLocation;
                //dtFolderHir = clsFolderHierarchy.GetmenuDataSet(ClsSettings.usertype, false);
                //treeFolderLocation.Focus();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            try
            {
                frmSearch frmsrch = new frmSearch();
                using (frmsrch)
                {
                    frmsrch.infSqlSelectQuery = "SELECT  id, username, usertype, userstatus FROM   SNAT.dbo.logintable AS l";
                    frmsrch.infSqlWhereCondtion = " Isnull(userstatus,0)=1 And  usertype<>'Admin'";
                    frmsrch.infSqlOrderBy = " username ";
                    frmsrch.infMultiSelect = false;
                    frmsrch.infSearchFormName = "Search User(s) ....";
                    frmsrch.infCodeFieldName = "id";
                    frmsrch.infDescriptionFieldName = "username";
                    frmsrch.infGridFieldName = " id, username,usertype, userstatus";
                    frmsrch.infGridFieldCaption = " id, User Name,User Type, userstatus";
                    frmsrch.infGridFieldSize = "0,100,150,0";
                    frmsrch.ShowDialog(this);
                    if (frmsrch.DialogResult == DialogResult.OK)
                    {
                        if (frmsrch.infCodeFieldText != null && !string.IsNullOrEmpty(frmsrch.infCodeFieldText.Trim()) && frmsrch.infCodeFieldText.Trim() != "")
                        {
                            txtUserName.Text = frmsrch.infDescriptionFieldText;
                            txtUserName.Tag = frmsrch.infCodeFieldText;
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void grdRights_Click(object sender, EventArgs e)
        {


        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUserName.Text.Trim()) == false)
                {
                    if (tabControl1.SelectedIndex == 0)
                    {
                        foreach (DataRowView drvUserRights in dtMenuDetails.DefaultView)
                        {

                            drvUserRights.BeginEdit();
                            drvUserRights["Rigths"] = false;
                            drvUserRights.EndEdit();

                        }

                        strSQlQuery = "SELECT id, userid, menuid, childmenuid, status  FROM SNAT.dbo.M_MenuRights Where userid='" + txtUserName.Tag.ToString().Trim() + "'";

                        dtUserRightsDetails = ClsDataLayer.GetDataTable(strSQlQuery);

                        foreach (DataRowView drvUserRights in dtUserRightsDetails.DefaultView)
                        {
                            dtMenuDetails.DefaultView.RowFilter = "ChiledID='" + drvUserRights["childmenuid"] + "' and HeaderID='" + drvUserRights["menuid"] + "' ";
                            if (dtMenuDetails.DefaultView.Count > 0)
                            {
                                dtMenuDetails.DefaultView[0].BeginEdit();
                                dtMenuDetails.DefaultView[0]["Rigths"] = drvUserRights["status"];
                                dtMenuDetails.DefaultView[0].EndEdit();
                            }

                        }
                        dtMenuDetails.DefaultView.RowFilter = "";
                        grdRights.DataSource = dtMenuDetails.DefaultView;
                    }
                    else if (tabControl1.SelectedIndex == 1)
                    {

                        //trPreantNode = treeFolderLocation.Nodes.Find("N1", true);
                        //List<TreeNode> lstTreeNode = new List<TreeNode>();

                        //  clsSearchFiles.AddChildren(lstTreeNode, trPreantNode[0]);

                        //string strQuery = "SELECT id ,userid,ParentFolderid,childFolderid,status FROM SNAT.dbo.T_FolderRights where userid='" + txtUserName.Tag.ToString().Trim() + "'";

                        //dtFolderRightsDetails = ClsDataLayer.GetDataTable(strQuery);

                        //foreach (DataRowView drvRow in dtFolderRightsDetails.DefaultView)
                        //{
                        //    TreeNode[] trPreantNode = null;
                        //    trPreantNode = treeFolderLocation.Nodes.Find(drvRow["childFolderid"].ToString(), true);
                        //    trPreantNode[0].Checked = Convert.ToBoolean(drvRow["status"]);

                        //}

                    }

                }
                else
                {
                    ClsMessage.ProjectExceptionMessage("Please select user name for rights.");
                    txtUserName.Focus();
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }

        private void btnUpdateRigths_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUserName.Text.Trim()) == false)
                {
                    strSQlQuery = "SELECT id, userid, menuid, childmenuid, status  FROM SNAT.dbo.M_MenuRights Where userid='" + txtUserName.Tag.ToString().Trim() + "'";

                    dtUserRightsDetails = ClsDataLayer.GetDataTable(strSQlQuery);

                    foreach (DataRowView drvUserRights in dtMenuDetails.DefaultView)
                    {
                        dtUserRightsDetails.DefaultView.RowFilter = "childmenuid='" + drvUserRights["ChiledID"] + "' and menuid='" + drvUserRights["HeaderID"] + "' ";
                        if (dtUserRightsDetails.DefaultView.Count > 0)
                        {
                            dtUserRightsDetails.DefaultView[0].BeginEdit();
                            dtUserRightsDetails.DefaultView[0]["status"] = drvUserRights["Rigths"];
                            dtUserRightsDetails.DefaultView[0].EndEdit();
                        }
                        else
                        {
                            DataRow dRow = dtUserRightsDetails.NewRow();
                            dRow["userid"] = txtUserName.Tag.ToString().Trim();
                            dRow["menuid"] = drvUserRights["HeaderID"];
                            dRow["childmenuid"] = drvUserRights["ChiledID"];
                            dRow["status"] = drvUserRights["Rigths"];
                            dtUserRightsDetails.Rows.Add(dRow);
                        }

                    }

                    bool lResult = false;
                    lResult = ClsDataLayer.UpdateDataAdapter(strSQlQuery, dtUserRightsDetails);
                    if (lResult == true)
                    {
                        ClsMessage.showSaveMessage();
                       // ClsDataLayer.setLogAcitivity("User Right", ClsSettings.username, "Updating User Rights", "", "Permission Granted  to user " + txtUserName.Text.Trim(), "Ex");
                        btnLoadData_Click(null, null);
                    }
                    else
                    {
                        ClsMessage.ProjectExceptionMessage("Some problem occurred while updating user rights." + Environment.NewLine + "Please contact system administrator.");
                       // ClsDataLayer.setLogAcitivity("User Right", ClsSettings.username, "Updating User Rights", "", "Some Problem Occured while updating user rights  to user " + txtUserName.Text.Trim() + ", Please Contact System Administrator ", "Msg");
                    }

                }
                else
                {
                    ClsMessage.ProjectExceptionMessage("Please select user name for rights.");
                    txtUserName.Focus();
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                //ClsDataLayer.setLogAcitivity("User Right", ClsSettings.username, "Updating User Rights", ex.Message.ToString(), "", "Ex");
            }
        }

        private void btnResetRigths_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUserName.Text.Trim()) == false)
                {
                    if (ClsMessage.showQuestionMessage("Are you sure want to reset all rights from selected user <" + txtUserName.Text.Trim() + ">") == DialogResult.No)
                    {
                        return;
                    }
                    strSQlQuery = "SELECT id, userid, menuid, childmenuid, status  FROM SNAT.dbo.M_MenuRights Where userid='" + txtUserName.Tag.ToString().Trim() + "'";

                    dtUserRightsDetails = ClsDataLayer.GetDataTable(strSQlQuery);

                    foreach (DataRowView drvUserRights in dtMenuDetails.DefaultView)
                    {
                        dtUserRightsDetails.DefaultView.RowFilter = "childmenuid='" + drvUserRights["ChiledID"] + "' and menuid='" + drvUserRights["HeaderID"] + "' ";
                        if (dtUserRightsDetails.DefaultView.Count > 0)
                        {
                            dtUserRightsDetails.DefaultView[0].BeginEdit();
                            dtUserRightsDetails.DefaultView[0]["status"] = false;
                            dtUserRightsDetails.DefaultView[0].EndEdit();
                        }

                    }

                    bool lResult = false;
                    lResult = ClsDataLayer.UpdateDataAdapter(strSQlQuery, dtUserRightsDetails);
                    if (lResult == true)
                    {
                        ClsMessage.showSaveMessage();
                        btnLoadData_Click(null, null);
                    }
                    else
                    {
                        ClsMessage.ProjectExceptionMessage("Some problem occurred while updating user rights." + Environment.NewLine + "Please contact system administrator.");
                    }

                }
                else
                {
                    ClsMessage.ProjectExceptionMessage("Please select user name for rights.");
                    txtUserName.Focus();
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void grdRights_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                if (e.Column.Name.ToUpper() == "DCRIGTHS")
                {
                    int iRowindex = 0;
                    iRowindex = e.RowIndex;
                    // dtMenuDetails.DefaultView.RowFilter = "ChiledID='" + drvUserRights["childmenuid"] + "' and HeaderID='" + drvUserRights["menuid"] + "' ";
                    if (dtMenuDetails.DefaultView.Count > 0)
                    {
                        dtMenuDetails.DefaultView[e.RowIndex].BeginEdit();
                        dtMenuDetails.DefaultView[e.RowIndex]["Rigths"] = !Convert.ToBoolean(dtMenuDetails.DefaultView[e.RowIndex]["Rigths"]);
                        dtMenuDetails.DefaultView[e.RowIndex].EndEdit();
                    }



                    grdRights.DataSource = dtMenuDetails.DefaultView;
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void treeFolderLocation_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {

        }

        private void treeFolderLocation_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {



                // MessageBox.Show("Test"); ;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        //private void selectAllFolderToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (treeFolderLocation.SelectedNode != null)
        //        {
        //            List<TreeNode> lstTreeNode = new List<TreeNode>();
        //            clsSearchFiles.AddChildren(lstTreeNode, treeFolderLocation.SelectedNode);

        //            foreach (var iNode in lstTreeNode)
        //            {
        //                iNode.Checked = true;
        //            }
        //            treeFolderLocation.SelectedNode.Checked = true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        ClsMessage.ProjectExceptionMessage(ex);
        //    }
        //}

        //private void unSelectAllFolderToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (treeFolderLocation.SelectedNode != null)
        //        {
        //            List<TreeNode> lstTreeNode = new List<TreeNode>();
        //            clsSearchFiles.AddChildren(lstTreeNode, treeFolderLocation.SelectedNode);

        //            foreach (var iNode in lstTreeNode)
        //            {
        //                iNode.Checked = false;
        //            }
        //            treeFolderLocation.SelectedNode.Checked = false;
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        ClsMessage.ProjectExceptionMessage(ex);
        //    }
        //}

        //private void btnUpdateFolderRights_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        if (string.IsNullOrEmpty(txtUserName.Text.Trim()) == false)
        //        {
        //            string strQuery = "SELECT id ,userid,ParentFolderid,childFolderid,status FROM SNAT.dbo.T_FolderRights where userid='" + txtUserName.Tag.ToString().Trim() + "'";

        //            dtFolderRightsDetails = ClsDataLayer.GetDataTable(strQuery);

        //            TreeNode[] trPreantNode = null;
        //            trPreantNode = treeFolderLocation.Nodes.Find("N1", true);
        //            List<TreeNode> lstTreeNode = new List<TreeNode>();
        //            DataRow dRow;
        //            clsSearchFiles.AddChildren(lstTreeNode, trPreantNode[0]);
        //            //  lstTreeNode.Add(trPreantNode[0]);
        //            foreach (var node in lstTreeNode)
        //            {
        //                dtFolderRightsDetails.DefaultView.RowFilter = "childFolderid='" + node.Name + "' and ParentFolderid='" + node.Parent.Name + "' ";
        //                if (dtFolderRightsDetails.DefaultView.Count > 0)
        //                {
        //                    dtFolderRightsDetails.DefaultView[0].BeginEdit();
        //                    dtFolderRightsDetails.DefaultView[0]["status"] = node.Checked;
        //                    dtFolderRightsDetails.DefaultView[0].EndEdit();
        //                }
        //                else
        //                {
        //                    dRow = dtFolderRightsDetails.NewRow();
        //                    dRow["userid"] = txtUserName.Tag.ToString().Trim();
        //                    dRow["ParentFolderid"] = node.Parent.Name;
        //                    dRow["childFolderid"] = node.Name;
        //                    dRow["status"] = node.Checked;
        //                    dtFolderRightsDetails.Rows.Add(dRow);
        //                }

        //            }

        //            dRow = dtFolderRightsDetails.NewRow();
        //            dRow["userid"] = txtUserName.Tag.ToString().Trim();
        //            dRow["ParentFolderid"] = trPreantNode[0].Name;
        //            dRow["childFolderid"] = trPreantNode[0].Name;
        //            dRow["status"] = true;
        //            dtFolderRightsDetails.Rows.Add(dRow);

        //            bool lResult = false;
        //            lResult = ClsDataLayer.UpdateDataAdapter(strQuery, dtFolderRightsDetails);
        //            if (lResult == true)
        //            {
        //                ClsMessage.showSaveMessage();
        //                btnLoadData_Click(null, null);
        //            }
        //            else
        //            {
        //                ClsMessage.ProjectExceptionMessage("Some problem occurred while updating user rights." + Environment.NewLine + "Please contact system administrator.");
        //            }

        //        }
        //        else
        //        {
        //            ClsMessage.ProjectExceptionMessage("Please select user name for rights.");
        //            txtUserName.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        ClsMessage.ProjectExceptionMessage(ex);
        //    }
        //}

        //private void btnResetFolderRights_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(txtUserName.Text.Trim()) == false)
        //        {
        //            string strQuery = "SELECT id ,userid,ParentFolderid,childFolderid,status FROM SNAT.dbo.T_FolderRights where userid='" + txtUserName.Tag.ToString().Trim() + "'";

        //            dtFolderRightsDetails = ClsDataLayer.GetDataTable(strQuery);

        //            TreeNode[] trPreantNode = null;
        //            trPreantNode = treeFolderLocation.Nodes.Find("N1", true);
        //            List<TreeNode> lstTreeNode = new List<TreeNode>();

        //            clsSearchFiles.AddChildren(lstTreeNode, trPreantNode[0]);

        //            foreach (var node in lstTreeNode)
        //            {
        //                dtFolderRightsDetails.DefaultView.RowFilter = "childFolderid='" + node.Name + "' and ParentFolderid='" + node.Parent.Name + "' ";
        //                if (dtFolderRightsDetails.DefaultView.Count > 0)
        //                {
        //                    dtFolderRightsDetails.DefaultView[0].BeginEdit();
        //                    dtFolderRightsDetails.DefaultView[0]["status"] = false;
        //                    dtFolderRightsDetails.DefaultView[0].EndEdit();
        //                }
        //                else
        //                {
        //                    DataRow dRow = dtFolderRightsDetails.NewRow();
        //                    dRow["userid"] = txtUserName.Tag.ToString().Trim();
        //                    dRow["ParentFolderid"] = node.Parent.Name;
        //                    dRow["childFolderid"] = node.Name;
        //                    dRow["status"] = false;
        //                    dtFolderRightsDetails.Rows.Add(dRow);
        //                }

        //            }

        //            bool lResult = false;
        //            lResult = ClsDataLayer.UpdateDataAdapter(strQuery, dtFolderRightsDetails);
        //            if (lResult == true)
        //            {
        //                ClsMessage.showSaveMessage();
        //                btnLoadData_Click(null, null);
        //            }
        //            else
        //            {
        //                ClsMessage.ProjectExceptionMessage("Some problem occurred while updating user rights." + Environment.NewLine + "Please contact system administrator.");
        //            }

        //        }
        //        else
        //        {
        //            ClsMessage.ProjectExceptionMessage("Please select user name for rights.");
        //            txtUserName.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        ClsMessage.ProjectExceptionMessage(ex);
        //    }
        //}
    }
}

