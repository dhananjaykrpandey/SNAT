using System.Configuration;

namespace SNAT.Comman_Classes
{
    class ClsSettings
    {
        #region Global Variable
        public static string ProjectName = "SNAT National Burial Scheme Management System";
        public static string dbCon = "";//System.Configuration.ConfigurationManager.AppSettings["CRMConnection"];
        public static string strEmployeeImageFolder = ConfigurationManager.AppSettings["EmployeeImageLocation"];
        public static string strSNATAttachedDocumentDetails = ConfigurationManager.AppSettings["SNATAttachedDocumentDetails"];
       
        public static string strFinancialDocAttached = ConfigurationManager.AppSettings["FinancialDocAttached"];
        public static string strClaimDocAttached = ConfigurationManager.AppSettings["ClaimDocAttached"];
        //  public static SqlConnection sqlcon = new SqlConnection(dbCon);
        public static string username = "";
        public static string usertype = "";
        public static bool  userstatus = false;
        public static string userid = "";
        #endregion

        #region "Master Form Instant"
        //public Account.Masters.frmCity instFrmCity;
        //public FrmLocation instFrmLocation;
        //public Frmservices instFrmServices;
        //public frmservicescharge instFrmServiceschrge;
        //public frmCustomer instFrmCustomer;
        #endregion
        #region "Transaction Form Instant"
        // public static Active_Phone.frmActivePhone istfrmActivePhone;

        #endregion
    }
}
