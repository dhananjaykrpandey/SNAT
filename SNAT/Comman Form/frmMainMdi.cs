using System;
using System.Windows.Forms;
using SNAT.Comman_Classes;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls.UI;
using System.Reflection;
using System.Linq;


namespace SNAT
{
    public partial class frmMainMDI : RadForm
    {
        public frmMainMDI()
        {
            InitializeComponent();
            this.radDockBody.AutoDetectMdiChildren = true;
            this.documentContainerBody.SendToBack();
            // this.radDockBody.DockStateChanged += new Telerik.WinControls.UI.Docking.DockWindowEventHandler(radDock1_DockStateChanged);
        }

        public void HideCloseButton(ToolWindow toolWindow)
        {
            if (toolWindow.DockState == DockState.Docked)
            {
                toolWindow.ToolCaptionButtons = ~ToolStripCaptionButtons.Close;
            }
            else if (toolWindow.DockState == DockState.Floating)
            {
                toolWindow.FloatingParent.FormElement.TitleBar.CloseButton.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            }
        }

        public void frmMainMDI_Load(object sender, EventArgs e)
        {
            clsMenu._menuTree = treeMenu;
            clsMenu.FillMenu();
            toolWindowMenu.AllowedDockState = AllowedDockState.Docked | AllowedDockState.AutoHide;


            toolWindowMenu.ToolCaptionButtons &= ~ToolStripCaptionButtons.SystemMenu;
            toolWindowMenu.ToolCaptionButtons &= ~ToolStripCaptionButtons.Close;
            radDockBody.DockWindow(toolWindowMenu, DockPosition.Left);
            splitContainer1.Panel1Collapsed = true;
        }

        public void btnBulkRename_Click(object sender, EventArgs e)
        {

        }

        public void treeMenu_NodeMouseClick(object sender, Telerik.WinControls.UI.RadTreeViewEventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(e.Node.Text) && e.Node.Text != "") && (!string.IsNullOrEmpty(e.Node.ImageKey) && e.Node.ImageKey != ""))
                {
                    openFrom(e.Node.Text, e.Node.ImageKey);
                }
                if ((!string.IsNullOrEmpty(e.Node.Text) && e.Node.Text != "") && (e.Node.Text.ToUpper() == "LOG OUT & EXIST"))
                {
                   
                    DialogResult dgRsult = MessageBox.Show(this, "Do you want exit or log out?" + Environment.NewLine + "Click Yes for exit No for Log Out.", ClsSettings.ProjectName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                    switch (dgRsult)
                    {
                        case DialogResult.Yes:
                            Application.Exit();
                            break;
                        case DialogResult.No:
                            Application.Restart();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {


                ClsMessage.ProjectExceptionMessage(ex.Message);
            }

        }

        public void frmMainMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        //void radDock1_DockStateChanged(object sender, Telerik.WinControls.UI.Docking.DockWindowEventArgs e)
        //{
        //    if (e.DockWindow.Equals(toolWindowMenu))
        //    {
        //        HideCloseButton(toolWindowMenu);
        //    }
        //}


        //public void btnAsset_Click(object sender, EventArgs e)
        //{
        //    frmAssetRegister frmobj = new frmAssetRegister();
        //    frmobj.Text = "Aesset Register";
        //    frmobj.MdiParent = this;
        //    frmobj.Show();
        //}
        public Form TryGetFormByName(string frmname)
        {
            var formType = Assembly.GetExecutingAssembly().GetTypes()
.FirstOrDefault(a => a.BaseType == typeof(Form) && a.Name == frmname);

            if (formType == null) // If there is no form with the given frmname
                return null;

            return (Form)Activator.CreateInstance(formType);
        }

        public void openFrom(string strMenuName, string strFormName)
        {
            try
            {


                //switch (strMenuName.ToUpper())
                //{
                //    case ("CUSTOMER REGISTRATION"):

                //        OpenForm(new frmcustomer(), "Customer Registration");
                //        break;
                //    case ("CUSTOMER REGISTER"):

                //        OpenForm(new frmcustomer(), "Customer Registration");
                //        break;

                //    case ("ASSET REGISTRATION"):
                //        OpenForm(new frmRfAssetCategory(), "Asset Registration");
                //        break;


                //    case ("ASSET REGISTER"):
                //        OpenForm(new frmAssetRegister(), "Asset Register");
                //        break;


                //}
                var obj = new Form();
                obj = TryGetFormByName(strFormName);
                //Form frmobj = new Form()  obj();
                OpenForm(obj, strMenuName);

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);

            }
        }

        public void OpenForm(Form frm, string strFromText)
        {
            if (ClsUtility.IsFormOpen(frm.GetType()))
            {
                foreach (DockWindow ctrl in documentContainerBody.Controls[0].Controls)
                {
                    if (ctrl.Name == frm.GetType().Name + "1")
                    {
                        ctrl.Select();
                        frm.Activate();
                        return;
                    }
                }
            }
            else
            {

                frm.Text = strFromText;
                frm.MdiParent = this;

                frm.Show();
                // this.documentContainerBody.Controls.Add(frm);

            }
        }

        public void radDockBody_DockWindowClosing(object sender, DockWindowCancelEventArgs e)
        {
            if (e.NewWindow.Text.ToUpper() == "Start Page".ToUpper())
            {
                e.Cancel = true;
            }
        }

        private void radDockBody_Click(object sender, EventArgs e)
        {

        }
    }
}
