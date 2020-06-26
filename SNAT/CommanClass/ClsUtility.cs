using System;
using System.Linq;
using System.Windows.Forms;
using System.Net.Mail;
using SNAT.Comman_Classes;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
//using Telerik.Windows.Pdf.Documents.Model.Drawing.Shapes;
using System.Net;
using SNAT.Comman_Form;
using System.Collections.Generic;
using SNAT.CommanClass;

namespace SNAT
{
    public static class ClsUtility
    {
        public static Form IsFormAlreadyOpen(Form frm)
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm == frm)
                    return OpenForm;
            }

            return null;
        }
        public static bool IsFormOpen(Type frm)
        {
            foreach (Form form in Application.OpenForms)
                if (form.GetType().Name == frm.Name)
                    return true;
            return false;
        }
        public static void NumericKeyPress(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
         (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }

            //// only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}

        }
        public static void DecilmalKeyPress(string strValue, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && (strValue.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        public static bool IsValidEmail(string s)
        {
            try
            {
                var address = new MailAddress(s);
                return true;
            }
            catch (Exception)
            {

                ClsMessage.ProjectExceptionMessage("Invalid Email!");
                return false;
            }

        }
        public static bool IsValidWebAddress(string s)
        {
            try
            {

                //bool result = Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out uriResult)
                //    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                bool result = (Regex.IsMatch(s, @"(((([a-z]|\d|-|.||~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'()*+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]).(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]).(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]).(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|.||~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))).)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))).?)(:\d*)?)(/((([a-z]|\d|-|.||~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'()*+,;=]|:|@)+(/(([a-z]|\d|-|.||~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'()*+,;=]|:|@)))?)?(\?((([a-z]|\d|-|.||~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'()*+,;=]|:|@)|[\uE000-\uF8FF]|/|\?)*)?(#((([a-z]|\d|-|.||~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'()*+,;=]|:|@)|/|\?)*)?$"));
                if (result == false) { ClsMessage.ProjectExceptionMessage("Invalid Web Address!"); return result; } else return result;
            }
            catch (Exception)
            {

                ClsMessage.ProjectExceptionMessage("Invalid Web Address!");
                return false;
            }

        }
        public static bool IsValidDate(string s)
        {
            DateTime output;
            return DateTime.TryParse(s, out output);

        }
        public static double Val(string value)
        {
            String result = String.Empty;
            foreach (char c in value)
            {
                if (Char.IsNumber(c) || (c.Equals('.') && result.Count(x => x.Equals('.')) == 0))
                    result += c;
                else if (!c.Equals(' '))
                    return String.IsNullOrEmpty(result) ? 0 : Convert.ToDouble(result);
            }
            return String.IsNullOrEmpty(result) ? 0 : Convert.ToDouble(result);
        }
        public static bool IsNumeric(object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { return false; } // just dismiss errors but return false

        }

        public enum _formMode
        {
            Add_EditMode,
            NoramlMode

        }


        public static bool IsCodeValueExists(string strTablename, string strCodecolumn, string strCheckcolumn, string strValue, object txtdescription = null, string strDesccolumn = "")
        {
            try
            {
                string strquery = " Select " + strCodecolumn + (string.IsNullOrEmpty(strDesccolumn.Trim()) ? "" : ("," + strDesccolumn)) + " from " + strTablename + Environment.NewLine +
                                    " where " + strCheckcolumn + "='" + strValue + "' ";
                DataTable dtcheck = new DataTable();
                dtcheck = ClsDataLayer.GetDataTable(strquery);
                if (dtcheck != null && dtcheck.DefaultView.Count > 0)
                {

                    if (!string.IsNullOrEmpty(dtcheck.DefaultView[0][strCodecolumn].ToString()))
                    {
                        if (!string.IsNullOrEmpty(strDesccolumn.Trim()) && txtdescription != null)
                        {

                            ((Control)txtdescription).Text = dtcheck.DefaultView[0][strDesccolumn].ToString();

                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }

        public enum enmFormMode
        {
            AddMode, EditMode, NormalMode

        }
        private static enmFormMode _enmFormMode = ClsUtility.enmFormMode.NormalMode;
        public static enmFormMode FormMode
        {
            get
            {
                return _enmFormMode;
            }
            set
            {
                _enmFormMode = value;
            }
        }

        public static byte[] GetByteArray(String strFileName)
        {
            GC.Collect();
            byte[] imgbyte = null;
            if (File.Exists(strFileName))
            {
                string newfilelocation = Path.GetTempPath();
                newfilelocation = newfilelocation + Path.GetFileName(strFileName);
                File.Copy(strFileName, newfilelocation, true);

                using (var fs = new FileStream(newfilelocation, FileMode.Open))
                {
                    // initialise the binary reader from file streamobject
                    System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                    // define the byte array of filelength

                    imgbyte = new byte[fs.Length + 1];
                    // read the bytes from the binary reader

                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
                    // add the image in bytearray

                    br.Close();
                    // close the binary reader

                    fs.Close();
                    // close the file stream

                }

                File.Delete(newfilelocation);

            }
            return imgbyte;
        }
        public static System.Drawing.Image GetImageFormFile(String strFileName)
        {
            GC.Collect();
            byte[] imgbyte = null;
            if (File.Exists(strFileName))
            {
                string newfilelocation = Path.GetTempPath();
                newfilelocation = newfilelocation + Path.GetFileName(strFileName);
                File.Copy(strFileName, newfilelocation, true);

                using (var fs = new FileStream(newfilelocation, FileMode.Open))
                {
                    // initialise the binary reader from file streamobject
                    System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                    // define the byte array of filelength

                    imgbyte = new byte[fs.Length + 1];
                    // read the bytes from the binary reader

                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
                    // add the image in bytearray

                    br.Close();
                    // close the binary reader

                    fs.Close();
                    // close the file stream

                }

                File.Delete(newfilelocation);

            }
            using (var ms = new MemoryStream(imgbyte))
            {
                return System.Drawing.Image.FromStream(ms);
            }

        }
        public static System.DateTime GetDateTime()
        {
            try
            {
                DateTime dtpReturn = DateTime.Now;
                DataTable dtcheck = new DataTable();
                dtcheck = ClsDataLayer.GetDataTable("select GetDate() as  currentdatetime");
                dtpReturn = string.IsNullOrEmpty(dtcheck.DefaultView[0]["currentdatetime"].ToString()) == true ? DateTime.Now : Convert.ToDateTime(dtcheck.DefaultView[0]["currentdatetime"]);
                return dtpReturn;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return DateTime.Now;
            }
        }
        public static void SearchText(DataTable infSearchDatatable, string strSearchKeyword, bool Searchcol = true, string columnName = "")
        {
            try
            {
                if (infSearchDatatable != null)
                {
                    infSearchDatatable.DefaultView.RowFilter = "";
                    bool UseContains = Searchcol;
                    int colCount = infSearchDatatable.Columns.Count;
                    System.Text.StringBuilder query = new System.Text.StringBuilder();
                    string filterString = "";


                    string likeStatement = (UseContains) ? "Like '%{0}%'" : " Like '{0}%'";

                    if (Searchcol == true && columnName == "")
                    {
                        for (int i = 0; i < colCount; i++)
                        {
                            string colName = infSearchDatatable.Columns[i].ColumnName;
                            query.Append(string.Concat("Convert(", colName, ", 'System.String')", likeStatement));


                            if (i != colCount - 1)
                                query.Append(" OR ");
                        }
                    }
                    else
                    {
                        query.Append(string.Concat("Convert(", columnName, ", 'System.String')", likeStatement));
                    }



                    filterString = query.ToString();
                    string currFilter = string.Format(filterString, strSearchKeyword.Trim());
                    DataRow[] tmpRows = infSearchDatatable.Select(currFilter);
                    infSearchDatatable.DefaultView.RowFilter = currFilter;
                }
                //MessageBox.Show("");
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }

        public enum enuAutorizeCheck
        {
            Chairperson, Sectretary, Treasurer, Premium
        }
        public static bool lsAutorizeForApproval(enuAutorizeCheck _enuAutorizeCheck)
        {
            try
            {
                string strSql = "SELECT  Cast (Case When count(*) >0 then 1 else 0 end as bit) lStatus  FROM dbo.logintable l  INNER JOIN dbo.T_EmployeeDetails ted ON ted.employeeno = l.employeeno  WHERE l.id='" + ClsSettings.userid + "' and l.username='" + ClsSettings.username + "' ";

                switch (_enuAutorizeCheck)
                {
                    case enuAutorizeCheck.Chairperson:
                        strSql = strSql + "AND ISNULL(ted.Approval_Chairperson, 0) = 1";
                        break;
                    case enuAutorizeCheck.Sectretary:
                        strSql = strSql + "AND ISNULL(ted.Approval_Sectretary, 0) = 1";
                        break;
                    case enuAutorizeCheck.Treasurer:
                        strSql = strSql + "AND ISNULL(ted.Approval_Treasurer, 0) = 1";
                        break;
                    case enuAutorizeCheck.Premium:
                        strSql = strSql + "AND ISNULL(ted.Approval_Premium, 0) = 1";
                        break;
                }

                DataTable dtCheck = new DataTable();
                dtCheck = ClsDataLayer.GetDataTable(strSql);
                if (dtCheck != null && dtCheck.DefaultView.Count > 0)
                {
                    if (Convert.ToBoolean(dtCheck.DefaultView[0][0].ToString()) == true)
                    {
                        return true;
                    }
                    else
                    {
                        ClsMessage.ProjectExceptionMessage("Logged user not authorize for this action!!");
                        return false;
                    }
                }
                else
                {
                    ClsMessage.ProjectExceptionMessage("Logged user not authorize for this action!!");
                    return false;
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }

        }
        public static bool SendMail(string strToEmailAddress, string strEmailSubject, string strEmailMessage)
        {
            try
            {
                // MailMessage mail = new MailMessage();
                // SmtpClient SmtpServer = new SmtpClient("snatburial.com");
                //// ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                // mail.From = new MailAddress("donotreply@snatburial.com");
                // mail.To.Add(strToEmailAddress);
                // mail.Subject = strEmailSubject;
                // mail.Body = strEmailMessage;

                // SmtpServer.Port = 25;
                // SmtpServer.Credentials = new System.Net.NetworkCredential("donotreply@snatburial.com", "santosh#123456");
                // SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                // //SmtpServer.EnableSsl = true;
                // //SmtpServer.s

                // SmtpServer.Send(mail);
                // MessageBox.Show("mail Send");

                string strSqlQuery = "INSERT INTO SNAT.dbo.T_MailSent(EmailAddress_To, sSubject, sMessage, sStatus, createby, createdate,MessageType,ErrMessage )" + Environment.NewLine +
                                     " VALUES('" + strToEmailAddress + "','" + strEmailSubject + "','" + strEmailMessage + "','Created','" + ClsSettings.username + "',GetDate(),'M','Mail Created Successfully') ";
                int iReslult = ClsDataLayer.UpdateData(strSqlQuery);
                if (iReslult > 0) { return true; } else { return false; }

            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }


        public static void PrintReport(string strReportName, DataTable dtReportData, string strReportDataTableName, Dictionary<string, string> dicFormula = null)
        {
            try
            {
                frmReportViewer frmrpt = new frmReportViewer();
                frmrpt.printReport(strReportName, dtReportData, strReportDataTableName, dicFormula);
                frmrpt.Show();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);

            }
        }

        public static bool SendSMS(string strMemberNationalID, string strMemberID, string strMemberName, string strMemberContactNo, string strChequeNo,
                                    string strPayee, string strApprovedBy, string strStats, bool lReadyForIssue = false)
        {
            try
            {
                string strMassage = "National ID- " + strMemberNationalID;
                strMassage = strMassage + Environment.NewLine +" ID " + strMemberID;
                strMassage = strMassage + Environment.NewLine +  "Name " + strMemberName;
                strMassage = strMassage + Environment.NewLine +  "Cheq#" + strChequeNo;
                strMassage = strMassage + Environment.NewLine + " Payee-" + strPayee;
                strMassage = strMassage + Environment.NewLine +  strStats + " By - " + strApprovedBy;
                if (lReadyForIssue == true)
                {
                    strMassage = strMassage + Environment.NewLine + "Ready For Issue";
                }
                if (strMemberContactNo != null && string.IsNullOrEmpty(strMemberContactNo) == false)
                {

                    if (strMassage.Length>160)
                    {
                        strMassage = strMassage.Substring(1, 160);
                    }
                    return clsSendSMS.SendSMSToMobileNo(strMemberContactNo, strMassage);
                }
                else
                {
                    ClsMessage.ProjectExceptionMessage("Member mobile number not available " + Environment.NewLine + "SMS cannot send!!");
                    return false;
                }

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }

    }
}
