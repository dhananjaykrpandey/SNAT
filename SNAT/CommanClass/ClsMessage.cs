using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SNAT.Comman_Classes
{
   public static class ClsMessage
    {
        static readonly string   ProjectName =ClsSettings.ProjectName;

        public static void showMessage(string msg, MessageBoxIcon Icon)
        {
            MessageBox.Show(msg, ProjectName, MessageBoxButtons.OK, Icon);
        }
        public static void ProjectExceptionMessage(string msg)
        {
            MessageBox.Show(msg, ProjectName, MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
        public static void ProjectExceptionMessage(Exception msg)
        {
            string innerex = "";
            if (msg.InnerException!=null)
            {
                innerex = msg.InnerException.ToString();
            }
            MessageBox.Show(msg + Environment.NewLine + innerex , ProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static DialogResult showQuestionMessage(string msg)
        {
            return MessageBox.Show(msg, ProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static void showSaveMessage()
        {
            MessageBox.Show("Record save successfully!", ProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void showDeleteMessage()
        {
            MessageBox.Show("Record deleted successfully!", ProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static DialogResult showAskDeleteMessage()
        {
            return MessageBox.Show("Are you sure want to delete this record?", ProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static DialogResult showAskExitMessage()
        {
            return MessageBox.Show("Are you sure want to exit?", ProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static DialogResult showAskDiscardMessage()
        {
            return MessageBox.Show("Are you sure want to discard changes?", ProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
