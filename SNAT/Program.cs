using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SNAT.Comman_Classes;

namespace SNAT
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmLogin());
            try
            {
                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new frmLogin());
                if (File.Exists(Application.StartupPath + "\\dbspl.dfs") == true)
                {
                    string[] sStr = null;
                    sStr = File.ReadAllLines(Application.StartupPath + "\\dbspl.dfs");
                    ClsSettings.dbCon = EnCryptDecrypt.CryptorEngine.Decrypt(sStr[0], true);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //PbpControl.clspbpDataClass._connctionString = ClsSettings.dbCon;
                   // PbpControl.clspbpDataClass._ProjectName = "SNAT";
                    Application.Run(new frmLogin());

                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    ClsMessage.showMessage("Database not set please contact system administrator.", MessageBoxIcon.Stop);
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
    }
}
