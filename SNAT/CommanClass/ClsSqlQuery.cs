using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SNAT.Comman_Classes
{
 public  class  ClsSqlQuery

    {

        public enum _enmQueryType
        {
            Select,
            Insert_Update
            
        }
        public _enmQueryType _QueryType = _enmQueryType.Select;
        public  _enmQueryType QueryType
        {
            get
            {
                return _QueryType;
            }
            set
            {
                _QueryType = value;
            }
        }
        public static DataTable _infDatatable = new DataTable();
        public static DataSet _infDataset = new DataSet();
        public static SqlDataAdapter _infDataAdapeter = new SqlDataAdapter();
        public static SqlCommandBuilder _infCommbulider = new SqlCommandBuilder();
        public static DataTable _dtLocalTable = new DataTable();
        public static void GetSqlCommand()
        {
            _infCommbulider = new SqlCommandBuilder(_infDataAdapeter);
           
            _infDataAdapeter.InsertCommand = _infCommbulider.GetInsertCommand();
            _infDataAdapeter.UpdateCommand = _infCommbulider.GetUpdateCommand();
            _infDataAdapeter.DeleteCommand = _infCommbulider.GetDeleteCommand();
        }

        public static Boolean GetCompayDetailsQuery(_enmQueryType querytype,DataTable _infReturnDataTable = null)
        {
            try
            {
                string rstrQuery = "";
                if(querytype==_enmQueryType.Insert_Update)
                {
                    rstrQuery = "SELECT cs.id,cs.Name,cs.Address,cs.phoneno ,cs.phoneno2,cs.faxno,cs.emailid,cs.website ,cs.contactperson,cs.contractdesg,cs.contactmobile,cs.contemailid,cs.currency,cs.logo,cs.logolocation   FROM SNAT.dbo.M_CompanySetup  cs (nolock) ";
                    if (ClsDataLayer.dbConn.State == ConnectionState.Closed) { ClsDataLayer.dbConn.Open(); }

                    _infDataAdapeter = new SqlDataAdapter(rstrQuery, ClsDataLayer.dbConn);
                    GetSqlCommand();
                    _infDataAdapeter.FillSchema(_infReturnDataTable, SchemaType.Source);
                 
                    _infDataAdapeter.Update(_infReturnDataTable);
                    _infReturnDataTable.AcceptChanges();


                }
                else
                {
                    rstrQuery = "SELECT cs.id,cs.Name,cs.Address,cs.phoneno ,cs.phoneno2,cs.faxno,cs.emailid,cs.website ,cs.contactperson,cs.contractdesg,cs.contactmobile,cs.contemailid,cs.currency,cu.description ,cs.logo,cs.logolocation   FROM SNAT.dbo.M_CompanySetup  cs (nolock) Left outer  join  SNAT.dbo.m_currency cu (nolock) on cs.currency=cu.code";
                    _infDatatable = ClsDataLayer.GetDataTable(rstrQuery);
                }

                return true;
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
                return false;
            }
            finally
            {
                if (ClsDataLayer.dbConn.State == ConnectionState.Open) { ClsDataLayer.dbConn.Close(); }
            }

        }
    }
}
