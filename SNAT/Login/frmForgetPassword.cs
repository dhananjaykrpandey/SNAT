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

namespace SNAT.Document
{
    public partial class frmForgetPassword : Form
    {
        public frmForgetPassword()
        {
            InitializeComponent();
        }

        private void frmForgetPassword_Load(object sender, EventArgs e)
        {
            txtUserId.Text = ClsSettings.username;
            lblMessage.Visible = false;
            lblPassword.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private bool ValidateSave()
        {
            try
            {
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(txtUserId.Text.Trim()))
                {
                    errorProvider1.SetError(txtUserId, "Please enter user id.");
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
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateSave() == false) { return; }
                if (ClsMessage.showQuestionMessage("Are you sure want to reset password?") == DialogResult.Yes)
                {
                    Random rd = new Random();
                    rd.Next(3103, 31031982);
                    txtpassword.Text = rd.Next(3103, 31031982).ToString();
                    string strQuery = "Update SNAT.dbo.logintable set password='" + txtpassword.Text.Trim() + "' where username='" + txtUserId.Text.Trim() + "'";
                    int lResult = ClsDataLayer.UpdateData(strQuery);
                    if (lResult >0)
                    {
                        ClsMessage.showMessage("User password reset successfully!!", MessageBoxIcon.Information);
                        lblMessage.Visible = true;
                        lblPassword.Visible = true;
                        lblPassword.Text = txtpassword.Text.Trim();
                        //ClsDataLayer.setLogAcitivity("Forgot Password", ClsSettings.username, "Resetting Password", "", "User has changed the Password!", "Msg");
                    }
                    else
                    {
                        ClsMessage.showMessage("Some problem occurs while saving please contact system administrator.", MessageBoxIcon.Information);
                       // ClsDataLayer.setLogAcitivity("Forgot Password", ClsSettings.username, "Resetting Password", "", "Some problem occurs while saving please contact system administrator.", "Msg");

                    }

                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
               // ClsDataLayer.setLogAcitivity("Forgot Password", ClsSettings.username, "Resetting Password", ex.Message.ToString(), "", "Ex");

            }
        }
    }
}
