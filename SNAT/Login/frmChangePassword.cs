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
    public partial class frmChangePassword : Form
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }
        private void frmChangePassword_Load(object sender, EventArgs e)
        {

            txtUserId.Text = ClsSettings.username;
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

                if (string.IsNullOrEmpty(txtoldPassword.Text.Trim()))
                {
                    errorProvider1.SetError(txtoldPassword, "Please enter old password.");
                    return false;
                }

                if (ClsUtility.IsCodeValueExists("SNAT.dbo.logintable", "password", "password", txtoldPassword.Text.Trim()) == false)
                {
                    errorProvider1.SetError(txtoldPassword, "Old doesn't match,Please enter correct old password.");
                    ClsMessage.showMessage("Old doesn't match,Please enter correct old password.");
                    return false;
                }

                if (string.IsNullOrEmpty(txtpassword.Text.Trim()))
                {
                    errorProvider1.SetError(txtpassword, "Please enter new password.");
                    return false;
                }

                if (string.IsNullOrEmpty(txtconfirmpassword.Text.Trim()))
                {
                    errorProvider1.SetError(txtconfirmpassword, "Please enter confirm password.");
                    return false;
                }
                if (txtpassword.Text.Trim().ToUpper() != txtconfirmpassword.Text.Trim().ToUpper())
                {
                    ClsMessage.showMessage("Confirm password does not match with password.");
                    errorProvider1.SetError(txtconfirmpassword, "Confirm password does not match with password.");
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
                if (ClsMessage.showQuestionMessage("Are you sure want to change password?") == DialogResult.Yes)
                {
                    string strQuery = "Update SNAT.dbo.logintable set password='" + txtpassword.Text.Trim() + "' where username='" + txtUserId.Text.Trim() + "'";
                    int lResult = ClsDataLayer.UpdateData(strQuery);
                    if (lResult >0)
                    {
                        ClsMessage.showMessage("User password change successfully!!");
                       // ClsDataLayer.setLogAcitivity("User Password Change", ClsSettings.username, "Password Chagned", "", "User Has Successfully Changed the Password", "Msg");
                    }
                    else
                    {
                        ClsMessage.showMessage("Some problem occurs while saving please contact system administrator.");

                    }

                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                //ClsDataLayer.setLogAcitivity("User Password Change", ClsSettings.username, " " + ex.Message.ToString(), "", "Problem Occured while changing password, please contact System Administrator", "Ex");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

