using System;
using System.Windows.Forms;
using SNAT.Comman_Classes;


namespace SNAT
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        public void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        public void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        void Login()
        {
            try
            {
                if (string.IsNullOrEmpty(txtUserName.Text.Trim())) { ClsMessage.ProjectExceptionMessage("Please enter user name."); txtUserName.Focus(); return; }
                if (string.IsNullOrEmpty(txtPassword.Text.Trim())) { ClsMessage.ProjectExceptionMessage("Please enter password."); txtPassword.Focus(); return; }
                System.Data.DataTable dtUser = new System.Data.DataTable();
                dtUser = ClsLogin.Checkuser(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                if (dtUser != null && dtUser.DefaultView.Count>0)
                {
                    ClsSettings.username = string.IsNullOrEmpty(dtUser.DefaultView[0]["Username"].ToString()) == true ? "" : dtUser.DefaultView[0]["Username"].ToString();
                    ClsSettings.usertype = string.IsNullOrEmpty(dtUser.DefaultView[0]["usertype"].ToString()) == true ? "" : dtUser.DefaultView[0]["usertype"].ToString();
                    ClsSettings.userstatus = string.IsNullOrEmpty(dtUser.DefaultView[0]["userstatus"].ToString()) == true ? false : Convert.ToBoolean(dtUser.DefaultView[0]["userstatus"].ToString());
                    ClsSettings.userid = string.IsNullOrEmpty(dtUser.DefaultView[0]["ID"].ToString()) == true ? "" : dtUser.DefaultView[0]["ID"].ToString();

                    this.Hide();
                    frmMainMDI obj = new frmMainMDI();
                    obj.Show();
                }
                else
                {
                    ClsMessage.ProjectExceptionMessage("Invalid user login details! " + Environment.NewLine + " Please check user name and password.");

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }
        public void BtnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        public void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        public void frmLogin_Load(object sender, EventArgs e)
        {

        }


    }
}