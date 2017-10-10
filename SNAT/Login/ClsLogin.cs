using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using SNAT.Comman_Classes;



public static class ClsLogin
{
    static string sStr;
    static DataTable DtResult;
    public static DataTable Checkuser(string userId, string Password)
    {
        try
        {
            sStr = "select * from logintable where Username='" + userId + "' and Password='" + Password + "' and userstatus='true'";
           ClsDataLayer.sConn = ClsSettings.dbCon;
           DtResult = ClsDataLayer.GetDataTable(sStr);
            if (DtResult.Rows.Count > 0)
            {
                return DtResult;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

}

