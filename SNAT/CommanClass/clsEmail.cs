using SNAT.Comman_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SNAT.CommanClass
{
    public class clsEmail
    {
        static string strSqlQuery = "";

        private static Dictionary<string, string> EmployeeCont(string strAlertCode)
        {

            try
            {
                Dictionary<string, string> dicEmpcont = new Dictionary<string, string>();

                strSqlQuery = "SELECT IsNull(ted.email,'') email,IsNull(ted.contactno1,'') contactno1,IsNull(tesa.EmailAlert,0) EmailAlert,IsNull(tesa.SmsAlert,0)  SmsAlert  FROM SNAT.dbo.T_EmployeeDetails ted (nolock) " + Environment.NewLine +
                              " INNER JOIN SNAT.dbo.T_Email_SMS_Alert tesa  (nolock) ON ted.nationalid = tesa.EmpNationalID AND ted.employeeno = tesa.EmpNo" + Environment.NewLine +
                              " WHERE tesa.alertyID='" + strAlertCode + "'";
                DataTable dtEmployeeCont = new DataTable();
                dtEmployeeCont = ClsDataLayer.GetDataTable(strSqlQuery);
                if (dtEmployeeCont != null && dtEmployeeCont.DefaultView.Count > 0)
                {
                    string strEmail = ""; string strContactNo = "";
                    foreach (DataRowView drvEmp in dtEmployeeCont.DefaultView)
                    {
                        if (drvEmp["email"].ToString().Trim() != "")
                        {
                            if (drvEmp["EmailAlert"].ToString().Trim().ToUpper() != "FALSE")
                            {
                                strEmail = strEmail + ";" + drvEmp["email"].ToString().Trim();
                            }
                        }
                        if (drvEmp["contactno1"].ToString().Trim() != "")
                        {
                            if (drvEmp["SmsAlert"].ToString().Trim().ToUpper() != "FALSE")
                            {
                                strContactNo = strContactNo + ";" + drvEmp["contactno1"].ToString().Trim();
                            }
                        }

                    }

                    if (strEmail.Length > 1)
                    {
                        strEmail = strEmail.Substring(1, strEmail.Length - 1);
                    }
                    if (strContactNo.Length > 1)
                    {
                        strContactNo = strContactNo.Substring(1, strContactNo.Length - 1);
                    }
                    dicEmpcont.Add("Email", strEmail);
                    dicEmpcont.Add("Contact", strContactNo);
                    return dicEmpcont;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return null;
            }
        }
        public static bool lMemberCreated(string memNationalID, string strMemEmpID, string strMemberID, string strMemName, string strPernimumAmount, string strMemEmailID = "")
        {
            try
            {
                string strEmailID = EmployeeCont("1")["Email"].ToString().Trim();
                if (strMemEmailID != "")
                {
                    strEmailID = strEmailID + ";" + strMemEmailID;
                }

                if (strEmailID == "")
                {
                    ClsMessage.ProjectExceptionMessage("Authorize person(s) e-mail id not available." + Environment.NewLine + "E-mail Cannot send." + Environment.NewLine + "Please contact system administrator.");
                    return false;
                }
                string strMesg = "Member [ " + strMemberID + " ] Data Update Successfully!! ";
                strMesg = strMesg + Environment.NewLine + "Member National ID    :  " + memNationalID;
                strMesg = strMesg + Environment.NewLine + "Member Employee #     : " + strMemEmpID;
                strMesg = strMesg + Environment.NewLine + "Member ID             :  " + strMemberID;
                strMesg = strMesg + Environment.NewLine + "Member Name           : " + strMemName;
                strMesg = strMesg + Environment.NewLine + "Member Premium Amount : " + strPernimumAmount;


                ClsUtility.SendMail(strEmailID, "Member [ " + strMemberID + " ] Data Update Successfully!!", strMesg);

                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }

        public static bool lBeneficiryCreated(string memNationalID, string strMemberID, string strMemName, string strBeneficiryNationalID,
                                                string strBeneficiryName, string strBenfEmailID = "")
        {
            try
            {
                string strEmailID = EmployeeCont("14")["Email"].ToString().Trim();

                //if(strMemEmailID!="")
                //{
                //    strEmailID= strEmailID + ";" + strMemEmailID;
                //}
                if (strBenfEmailID != "")
                {
                    strEmailID = strEmailID + ";" + strBenfEmailID;
                }
                if (strEmailID == "")
                {
                    ClsMessage.ProjectExceptionMessage("Authorize person(s) e-mail id not available." + Environment.NewLine + "E-mail Cannot send." + Environment.NewLine + "Please contact system administrator.");
                    return false;
                }
                string strMesg = "Member [ " + strMemberID + " ] - Beneficiary [ " + strBeneficiryNationalID + " ] Data Update Successfully!! ";
                strMesg = strMesg + Environment.NewLine + "Member National ID      :  " + memNationalID;
                strMesg = strMesg + Environment.NewLine + "Member ID               :  " + strMemberID;
                strMesg = strMesg + Environment.NewLine + "Member Name             : " + strMemName;
                strMesg = strMesg + Environment.NewLine + "Beneficiary National ID : " + strBeneficiryNationalID;
                strMesg = strMesg + Environment.NewLine + "Beneficiary Name        : " + strBeneficiryName;


                ClsUtility.SendMail(strEmailID, "Member [ " + strMemberID + " ] - Beneficiary [ " + strBeneficiryNationalID + " ] Data Update Successfully!! ", strMesg);

                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }

        public static bool lPrenumUpload(string strwageMonthYear, string strwageFrom, string strTotalUpload, string strIvalidMember, string striInvalidMember)
        {
            try
            {
                string strEmailID = EmployeeCont("2")["Email"].ToString().Trim();

                if (strEmailID == "")
                {
                    ClsMessage.ProjectExceptionMessage("Authorize person(s) e-mail id not available." + Environment.NewLine + "E-mail Cannot send." + Environment.NewLine + "Please contact system administrator.");
                    return false;
                }
                string strMesg = "[ " + strwageMonthYear + " ] Month - [ " + strwageFrom + " ]  Premium Uploaded Successfully!! ";
                strMesg = strMesg + Environment.NewLine + "Premium Month         :  " + strwageMonthYear;
                strMesg = strMesg + Environment.NewLine + "Premium From          : " + strwageFrom;
                strMesg = strMesg + Environment.NewLine + "Total Uploaded        :  " + strTotalUpload;
                strMesg = strMesg + Environment.NewLine + "Valid Member Count    : " + strIvalidMember;
                strMesg = strMesg + Environment.NewLine + "In-Valid Member Count : " + striInvalidMember;


                ClsUtility.SendMail(strEmailID, "[ " + strwageMonthYear + " ] Month - [ " + strwageFrom + " ]  Premium Uploaded Successfully!! ", strMesg);

                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }
        public static bool lPrenumProcess(string strwageMonthYear, string strwageFrom, string strStatus)
        {
            try
            {
                string strEmailID = EmployeeCont("3")["Email"].ToString().Trim();
                if (strEmailID == "")
                {
                    ClsMessage.ProjectExceptionMessage("Authorize person(s) e-mail id not available." + Environment.NewLine + "E-mail Cannot send." + Environment.NewLine + "Please contact system administrator.");
                    return false;
                }

                string strMesg = "[ " + strwageMonthYear + " ] Month - [ " + strwageFrom + " ]  Premium Processed Successfully!! ";
                strMesg = strMesg + Environment.NewLine + "Premium Month         :  " + strwageMonthYear;
                strMesg = strMesg + Environment.NewLine + "Premium From          : " + strwageFrom;
                strMesg = strMesg + Environment.NewLine + "Status                :  " + strStatus;



                ClsUtility.SendMail(strEmailID, "[ " + strwageMonthYear + " ] Month - [ " + strwageFrom + " ]  Premium Processed Successfully!! ", strMesg);

                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }

        public static bool lPrenumApproved(string strwageMonthYear, string strwageFrom, string strStatus)
        {
            try
            {
                string strEmailID = EmployeeCont("4")["Email"].ToString().Trim();

                if (strEmailID == "")
                {
                    ClsMessage.ProjectExceptionMessage("Authorize person(s) e-mail id not available." + Environment.NewLine + "E-mail Cannot send." + Environment.NewLine + "Please contact system administrator.");
                    return false;
                }
                string strMesg = "[ " + strwageMonthYear + " ] Month - [ " + strwageFrom + " ]  Premium Approved Successfully!! ";
                strMesg = strMesg + Environment.NewLine + "Premium Month         :  " + strwageMonthYear;
                strMesg = strMesg + Environment.NewLine + "Premium From          : " + strwageFrom;
                strMesg = strMesg + Environment.NewLine + "Status                :  " + strStatus;



                ClsUtility.SendMail(strEmailID, "[ " + strwageMonthYear + " ] Month - [ " + strwageFrom + " ]  Premium Approved Successfully!! ", strMesg);

                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }

        public static bool lClaimEntry(string strLetPerson, string strMemebrNationalID, string strMemberID, string strMemberName, string strClaimID,
                                        string strPlaceOfBurial, string strMortury, string strDateOfBurial, string strClaimAmount, string strBenefNationalID,
                                        string strBenefName, string strStatus)
        {
            try
            {
                string strEmailID = EmployeeCont("5")["Email"].ToString().Trim();

                if (strEmailID == "")
                {
                    ClsMessage.ProjectExceptionMessage("Authorize person(s) e-mail id not available." + Environment.NewLine + "E-mail Cannot send." + Environment.NewLine + "Please contact system administrator.");
                    return false;
                }

                string strMesg = "Claim ID # - [ " + strClaimID + " ]   " + strStatus + " for Member - [ " + strMemebrNationalID + " ]/[ " + strMemberName + " ].";
                strMesg = strMesg + Environment.NewLine + "Claim ID                    :  " + strClaimID;
                strMesg = strMesg + Environment.NewLine + "Let. Person                 :  " + strLetPerson;
                strMesg = strMesg + Environment.NewLine + "Member National ID          : " + strMemebrNationalID;
                strMesg = strMesg + Environment.NewLine + "Member ID                   :  " + strMemberID;
                strMesg = strMesg + Environment.NewLine + "Member Name                 :  " + strMemberName;
                if (strBenefNationalID != "")
                {
                    strMesg = strMesg + Environment.NewLine + "Beneficiary National ID         :  " + strBenefNationalID;
                    strMesg = strMesg + Environment.NewLine + "Beneficiary Name                :  " + strBenefName;
                }


                strMesg = strMesg + Environment.NewLine + "Place Of Burial         :  " + strPlaceOfBurial;
                strMesg = strMesg + Environment.NewLine + "Mortuary                :  " + strMortury;
                strMesg = strMesg + Environment.NewLine + "Date Of Burial          :  " + strDateOfBurial;
                strMesg = strMesg + Environment.NewLine + "Claim Amount            :  " + strClaimAmount;



                ClsUtility.SendMail(strEmailID, "Claim ID # - [ " + strClaimID + " ]  " + strStatus + " for Member - [ " + strMemebrNationalID + " ]/[ " + strMemberName + " ].", strMesg);

                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }

        public static bool lClaimApproval(string strLetPerson, string strMemebrNationalID, string strMemberID, string strMemberName, string strClaimID,
                                        string strPlaceOfBurial, string strMortury, string strDateOfBurial, string strClaimAmount, string strBenefNationalID,
                                        string strBenefName, string strApprovedPerson, string strApprovedBy, string strApprovedDate
                                        , string strApprovalRemarks, string strStatus, string strAlertType)
        {
            try
            {
                string strEmailID = EmployeeCont(strAlertType)["Email"].ToString().Trim();
                if (strEmailID == "")
                {
                    ClsMessage.ProjectExceptionMessage("Authorize person(s) e-mail id not available." + Environment.NewLine + "E-mail Cannot send." + Environment.NewLine + "Please contact system administrator.");
                    return false;
                }

                string strMesg = "Claim ID # -  [ " + strClaimID + " ] " + strStatus + " By " + strApprovedPerson + ".";
                strMesg = strMesg + Environment.NewLine + "Claim ID                    :  " + strClaimID;
                strMesg = strMesg + Environment.NewLine + "Let. Person                 :  " + strLetPerson;
                strMesg = strMesg + Environment.NewLine + "Member National ID          : " + strMemebrNationalID;
                strMesg = strMesg + Environment.NewLine + "Member ID                   :  " + strMemberID;
                strMesg = strMesg + Environment.NewLine + "Member Name                 :  " + strMemberName;
                if (strBenefNationalID != "")
                {
                    strMesg = strMesg + Environment.NewLine + "Beneficiary National ID         :  " + strBenefNationalID;
                    strMesg = strMesg + Environment.NewLine + "Beneficiary Name                :  " + strBenefName;
                }


                strMesg = strMesg + Environment.NewLine + "Place Of Burial                   :  " + strPlaceOfBurial;
                strMesg = strMesg + Environment.NewLine + "Mortuary                          :  " + strMortury;
                strMesg = strMesg + Environment.NewLine + "Date Of Burial                    :  " + strDateOfBurial;
                strMesg = strMesg + Environment.NewLine + "Claim Amount                      :  " + strClaimAmount;
                strMesg = strMesg + Environment.NewLine + "Claim Status                      :  " + strStatus;
                strMesg = strMesg + Environment.NewLine + "" + strStatus + "  By             :  " + strApprovedBy;
                strMesg = strMesg + Environment.NewLine + "" + strStatus + "  Date           :  " + strApprovedDate;
                strMesg = strMesg + Environment.NewLine + "" + strStatus + "  Remarks        :  " + strApprovalRemarks;

                ClsUtility.SendMail(strEmailID, "Claim ID # - [ " + strClaimID + " ] " + strStatus + " By " + strApprovedPerson + ".", strMesg);

                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }



        public static bool lChequeEntry(string strLetPerson, string strMemebrNationalID, string strMemberID, string strMemberName, string strClaimID,
                                        string strPayee, string strChqNo, string strRequestBy, string strDateOfrequest, string strClaimAmount, string strBenefNationalID,
                                        string strBenefName, string strStatus)
        {
            try
            {
                string strEmailID = EmployeeCont("5")["Email"].ToString().Trim();

                if (strEmailID == "")
                {
                    ClsMessage.ProjectExceptionMessage("Authorize person(s) e-mail id not available." + Environment.NewLine + "E-mail Cannot send." + Environment.NewLine + "Please contact system administrator.");
                    return false;
                }

                string strMesg = "Cheque Requisition ID # - [ " + strClaimID + " ]   " + strStatus + " for Member - [ " + strMemebrNationalID + " ]/[ " + strMemberName + " ].";
                strMesg = strMesg + Environment.NewLine + "Claim ID                    :  " + strClaimID;
                strMesg = strMesg + Environment.NewLine + "Let. Person                 :  " + strLetPerson;
                strMesg = strMesg + Environment.NewLine + "Member National ID          : " + strMemebrNationalID;
                strMesg = strMesg + Environment.NewLine + "Member ID                   :  " + strMemberID;
                strMesg = strMesg + Environment.NewLine + "Member Name                 :  " + strMemberName;
                if (strBenefNationalID != "")
                {
                    strMesg = strMesg + Environment.NewLine + "Beneficiary National ID         :  " + strBenefNationalID;
                    strMesg = strMesg + Environment.NewLine + "Beneficiary Name                :  " + strBenefName;
                }


                strMesg = strMesg + Environment.NewLine + "Payee         :  " + strPayee;
                strMesg = strMesg + Environment.NewLine + "Cheque No                :  " + strChqNo;
                strMesg = strMesg + Environment.NewLine + "Requested By          :  " + strRequestBy;
                strMesg = strMesg + Environment.NewLine + "Requested Date          :  " + strDateOfrequest;
                strMesg = strMesg + Environment.NewLine + "Claim Amount            :  " + strClaimAmount;



                ClsUtility.SendMail(strEmailID, "Cheque Requisition ID # - [ " + strClaimID + " ]  " + strStatus + " for Member - [ " + strMemebrNationalID + " ]/[ " + strMemberName + " ].", strMesg);

                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }

        public static bool lChequeApproval(string strLetPerson, string strMemebrNationalID, string strMemberID, string strMemberName, string strClaimID,
                                        string strPayee, string strChqNo, string strRequestBy, string strDateOfrequest, string strClaimAmount, string strBenefNationalID,
                                        string strBenefName, string strApprovedPerson, string strApprovedBy, string strApprovedDate
                                        , string strApprovalRemarks, string strStatus, string strAlertType)
        {
            try
            {
                string strEmailID = EmployeeCont(strAlertType)["Email"].ToString().Trim();
                if (strEmailID == "")
                {
                    ClsMessage.ProjectExceptionMessage("Authorize person(s) e-mail id not available." + Environment.NewLine + "E-mail Cannot send." + Environment.NewLine + "Please contact system administrator.");
                    return false;
                }

                string strMesg = "Claim ID # -  [ " + strClaimID + " ] " + strStatus + " By " + strApprovedPerson + ".";
                strMesg = strMesg + Environment.NewLine + "Claim ID                    :  " + strClaimID;
                strMesg = strMesg + Environment.NewLine + "Let. Person                 :  " + strLetPerson;
                strMesg = strMesg + Environment.NewLine + "Member National ID          : " + strMemebrNationalID;
                strMesg = strMesg + Environment.NewLine + "Member ID                   :  " + strMemberID;
                strMesg = strMesg + Environment.NewLine + "Member Name                 :  " + strMemberName;
                if (strBenefNationalID != "")
                {
                    strMesg = strMesg + Environment.NewLine + "Beneficiary National ID         :  " + strBenefNationalID;
                    strMesg = strMesg + Environment.NewLine + "Beneficiary Name                :  " + strBenefName;
                }


                strMesg = strMesg + Environment.NewLine + "Payee         :  " + strPayee;
                strMesg = strMesg + Environment.NewLine + "Cheque No                :  " + strChqNo;
                strMesg = strMesg + Environment.NewLine + "Requested By          :  " + strRequestBy;
                strMesg = strMesg + Environment.NewLine + "Requested Date          :  " + strDateOfrequest;
                strMesg = strMesg + Environment.NewLine + "Claim Amount                      :  " + strClaimAmount;
                strMesg = strMesg + Environment.NewLine + "Claim Status                      :  " + strStatus;
                strMesg = strMesg + Environment.NewLine + "" + strStatus + "  By             :  " + strApprovedBy;
                strMesg = strMesg + Environment.NewLine + "" + strStatus + "  Date           :  " + strApprovedDate;
                strMesg = strMesg + Environment.NewLine + "" + strStatus + "  Remarks        :  " + strApprovalRemarks;

                ClsUtility.SendMail(strEmailID, "Claim ID # - [ " + strClaimID + " ] " + strStatus + " By " + strApprovedPerson + ".", strMesg);

                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }


        public static bool lManualPrenumProcess(string strMemebrNationalID, string strMemberID, string strMemberName, string strwageMonthYear, string strwage, string strStatus)
        {
            try
            {
                string strEmailID = EmployeeCont("3")["Email"].ToString().Trim();
                if (strEmailID == "")
                {
                    ClsMessage.ProjectExceptionMessage("Authorize person(s) e-mail id not available." + Environment.NewLine + "E-mail Cannot send." + Environment.NewLine + "Please contact system administrator.");
                    return false;
                }

                string strMesg = "Member  [ " + strMemebrNationalID +"/"+ strMemberName + " ] Premium for  Month - [ " + strwageMonthYear + " ] Manually Processed Successfully!! ";
                strMesg = strMesg + Environment.NewLine + "Member                :  " + strMemebrNationalID + "/" + strMemberName;
                strMesg = strMesg + Environment.NewLine + "Premium Month         :  " + strwageMonthYear;
                strMesg = strMesg + Environment.NewLine + "Premium From          :  Manually";
               strMesg = strMesg + Environment.NewLine +  "Premium Amount        :  "+ strwage;



                ClsUtility.SendMail(strEmailID, "Member  [ " + strMemebrNationalID + "/" + strMemberName + " ] Premium for  Month - [ " + strwageMonthYear + " ] Manually Processed Successfully!!", strMesg);

                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }



        public static bool lManualPrenumApproved(string strMemebrNationalID, string strMemberID, string strMemberName, string strwageMonthYear, string strwage, string strStatus)
        {
            try
            {
                string strEmailID = EmployeeCont("4")["Email"].ToString().Trim();
                if (strEmailID == "")
                {
                    ClsMessage.ProjectExceptionMessage("Authorize person(s) e-mail id not available." + Environment.NewLine + "E-mail Cannot send." + Environment.NewLine + "Please contact system administrator.");
                    return false;
                }

                string strMesg = "Member  [ " + strMemebrNationalID + "/" + strMemberName + " ] Premium for  Month - [ " + strwageMonthYear + " ] Manually Approved Successfully!! ";
                strMesg = strMesg + Environment.NewLine + "Member                :  " + strMemebrNationalID + "/" + strMemberName;
                strMesg = strMesg + Environment.NewLine + "Premium Month         :  " + strwageMonthYear;
                strMesg = strMesg + Environment.NewLine + "Premium From          :  Manually";
                strMesg = strMesg + Environment.NewLine + "Premium Amount        :  " + strwage;



                ClsUtility.SendMail(strEmailID, "Member  [ " + strMemebrNationalID + "/" + strMemberName + " ] Premium for  Month - [ " + strwageMonthYear + " ] Manually Approved Successfully!!", strMesg);

                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }


        public static bool lManualPrenumDelete(string strMemebrNationalID, string strMemberID, string strMemberName, string strwageMonthYear, string strwage, string strStatus)
        {
            try
            {
                string strEmailID = EmployeeCont("3")["Email"].ToString().Trim();
                if (strEmailID == "")
                {
                    ClsMessage.ProjectExceptionMessage("Authorize person(s) e-mail id not available." + Environment.NewLine + "E-mail Cannot send." + Environment.NewLine + "Please contact system administrator.");
                    return false;
                }

                string strMesg = "Member  [ " + strMemebrNationalID + "/" + strMemberName + " ] Premium for  Month - [ " + strwageMonthYear + " ] Deleted Successfully!! ";
                strMesg = strMesg + Environment.NewLine + "Member                :  " + strMemebrNationalID + "/" + strMemberName;
                strMesg = strMesg + Environment.NewLine + "Premium Month         :  " + strwageMonthYear;
                strMesg = strMesg + Environment.NewLine + "Premium From          :  Manually";
                strMesg = strMesg + Environment.NewLine + "Premium Amount        :  " + strwage;



                ClsUtility.SendMail(strEmailID, "Member  [ " + strMemebrNationalID + "/" + strMemberName + " ] Premium for  Month - [ " + strwageMonthYear + " ] Deleted Successfully!!", strMesg);

                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
        }

    }
}
