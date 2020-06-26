using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SNAT.Comman_Classes
{
    public static class ClsMessage
    {
        static readonly string   ProjectName =ClsSettings.ProjectName;

        public static void showMessage(string msg, RadMessageIcon Icon=RadMessageIcon.Info)
        {
            RadMessageBox.Show(msg, ProjectName, MessageBoxButtons.OK, Icon);
        }
        public static void ProjectExceptionMessage(string msg)
        {
            RadMessageBox.Show(msg, ProjectName,MessageBoxButtons.OK, RadMessageIcon.Error);
        }
        public static void ProjectExceptionMessage(Exception msg)
        {
            string innerex = "";
            if (msg.InnerException!=null)
            {
                innerex = msg.InnerException.ToString();
            }
            RadMessageBox.Show(msg + Environment.NewLine + innerex , ProjectName, MessageBoxButtons.OK, RadMessageIcon.Error);
        }
        public static DialogResult showQuestionMessage(string msg)
        {
            return RadMessageBox.Show(msg, ProjectName, MessageBoxButtons.YesNo, RadMessageIcon.Question);
        }
        public static void showSaveMessage()
        {
            RadMessageBox.Show("Record save successfully!", ProjectName, MessageBoxButtons.OK, RadMessageIcon.Info);
        }
        public static void showDeleteMessage()
        {
            RadMessageBox.Show("Record deleted successfully!", ProjectName, MessageBoxButtons.OK, RadMessageIcon.Info);
        }
        public static DialogResult showAskDeleteMessage()
        {
            return RadMessageBox.Show("Are you sure want to delete this record?", ProjectName, MessageBoxButtons.YesNo, RadMessageIcon.Question);
        }
        public static DialogResult showAskExitMessage()
        {
            return RadMessageBox.Show("Are you sure want to exit?", ProjectName, MessageBoxButtons.YesNo, RadMessageIcon.Question);
        }
        public static DialogResult showAskDiscardMessage()
        {
            return RadMessageBox.Show("Are you sure want to discard changes?", ProjectName, MessageBoxButtons.YesNo, RadMessageIcon.Question);
        }
    }
}
