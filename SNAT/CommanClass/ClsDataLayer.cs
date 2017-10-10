using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;



public static class ClsDataLayer
{

    public static string sConn { get; set; }
    public static SqlConnection dbConn = new SqlConnection(sConn);


    public static SqlDataAdapter SqlDa = new SqlDataAdapter();
    public static SqlCommand Commd = new SqlCommand();
    public static SqlTransaction sqlTrans = null;
    public static void openConnection()
    {
        if (dbConn.ConnectionString == null || dbConn.ConnectionString.Trim()=="" ) { dbConn.ConnectionString = sConn; }
        if (dbConn.State == ConnectionState.Closed)
        {
           
            dbConn.Open();
        }

    }
    public static void clsoeConnection()
    {
        if (dbConn != null && dbConn.State == ConnectionState.Open)
        {
            dbConn.Close();
        }
    }
    /// <summary>
    /// This function return datatable for filling grid or retriving database.
    /// </summary>
    /// <param name="SqlQuery">Sql Query String</param>
    /// <returns>Data Table</returns>
    ///      
    public static DataTable GetDataTable(string SqlQuery)
    {
        DataTable DtResult = new DataTable();
        //
        try
        {
            openConnection();
            SqlDa = new SqlDataAdapter(SqlQuery, dbConn);
            SqlDa.FillSchema(DtResult, SchemaType.Source);
            SqlDa.Fill(DtResult);
        }
        catch (Exception ex)
        {
            clsoeConnection();
            throw ex;
        }
        finally
        { clsoeConnection(); }
        return DtResult;
    }
    /// <summary>
    /// This function is return a datareader.
    /// </summary>
    /// <param name="SqlQuery">Sql Query</param>
    /// <returns>Datareader</returns>
    public static SqlDataReader GetDataReader(string SqlQuery)
    {
        SqlDataReader DrResult;
        try
        {
            openConnection();
            Commd = new SqlCommand(SqlQuery, dbConn);
            DrResult = Commd.ExecuteReader();
        }
        catch (Exception ex)
        {
            clsoeConnection();
            throw ex;
        }
        finally { clsoeConnection(); }
        return DrResult;

    }
    /// <summary>
    /// This function is excute sql query and update data in database.
    /// </summary>
    /// <param name="SqlQuery">Insert/Update Query</param>
    /// <returns>Interger type return,no of row return.</returns>
    public static int UpdateData(string SqlQuery)
    {
        int iReturn = 0;
        try
        {
            openConnection();
            Commd = new SqlCommand(SqlQuery, dbConn);
            iReturn = Commd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            clsoeConnection();
            throw ex;
        }
        finally { clsoeConnection(); }

        return iReturn;
    }
    /// <summary>
    /// This function is excute sql query and update data in database.
    /// </summary>
    /// <param name="SqlQuery">Insert/Update Query</param>
    /// <returns>object type variable.</returns>
    public static object UpdateDataObject(string SqlQuery)
    {
        object iReturn = null;
        try
        {
            openConnection();
            Commd = new SqlCommand(SqlQuery, dbConn);
            iReturn = Commd.ExecuteScalar();

        }
        catch (Exception ex)
        {
            clsoeConnection();
            throw ex;
        }
        finally { clsoeConnection(); }

        return iReturn;
    }
    /// <summary>
    /// This function is ues for excute sql query in transtion.
    /// </summary>
    /// <param name="SqlQuery">String type sql query.</param>
    /// <param name="SqlTrans">Sql Transtion.</param>
    /// <returns>Return interger type no of row affected</returns>
    public static int ExcuteTranstion(string SqlQuery, SqlTransaction SqlTrans)
    {
        int iReturn = 0;
        try
        {
            // openConnection();
            Commd = new SqlCommand(SqlQuery, dbConn, SqlTrans);
            iReturn = Commd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            clsoeConnection();
            throw ex;
        }





        return iReturn;
    }
    /// <summary>
    ///This function is used for execute store procedure and update data in database.
    /// </summary>
    /// <param name="pram">Diction type pram </param>

    /// <returns>Return integer type no of row affected</returns>
    public static int UpdateStoreProcedure(Dictionary<string, string> pram)
    {

        int iReturn = 0;
        try
        {
            openConnection();
            Commd = new SqlCommand();
            Commd.Connection = dbConn;
            Commd.CommandType = CommandType.StoredProcedure;
            foreach (KeyValuePair<string, string> pValue in pram)
            {
                Commd.Parameters.AddWithValue(pValue.Key, pValue.Value);
            }
            iReturn = Commd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            clsoeConnection();
            throw ex;
        }
        finally { clsoeConnection(); }
        return iReturn;
    }
    /// <summary>
    ///This functin is used for excute store procedure and return datatable.
    /// </summary>
    /// <param name="pram">Diction type pram </param>

    /// <returns>Return datatable with data</returns>
    public static DataTable GetDataTable(Dictionary<string, string> pram)
    {
        DataTable DtResult = new DataTable();
        //
        try
        {
            openConnection();
            Commd = new SqlCommand();
            Commd.Connection = dbConn;
            Commd.CommandType = CommandType.StoredProcedure;
            foreach (KeyValuePair<string, string> pValue in pram)
            {
                Commd.Parameters.AddWithValue(pValue.Key, pValue.Value);
            }

            SqlDataAdapter da = new SqlDataAdapter(Commd);
            da.Fill(DtResult);

        }
        catch (Exception ex)
        {
            clsoeConnection();
            throw ex;
        }
        return DtResult;
    }


    /// <summary>
    ///This functin is used for excute store procedure and update data in database.
    /// </summary>
    /// <param name="pram">Diction type pram </param>

    /// <returns>Return interger type no of row affected</returns>
    public static int UpdateStoreProcedure(Dictionary<string, string> pram, string SPName = "")
    {

        int iReturn = 0;
        try
        {
            dbConn.Open();
            Commd = new SqlCommand();
            Commd.Connection = dbConn;
            Commd.CommandType = CommandType.StoredProcedure;
            Commd.CommandText = SPName;
            foreach (KeyValuePair<string, string> pValue in pram)
            {
                Commd.Parameters.AddWithValue(pValue.Key, pValue.Value);
            }
            iReturn = Commd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            dbConn.Close();
            throw ex;
        }
        finally { dbConn.Close(); }
        return iReturn;
    }
    public static object UpdateStoreProcedure_Return(Dictionary<string, string> pram, string SPName = "")
    {

        object iReturn = null;
        try
        {
            dbConn.Open();
            Commd = new SqlCommand();
            Commd.Connection = dbConn;
            Commd.CommandType = CommandType.StoredProcedure;
            Commd.CommandText = SPName;
            foreach (KeyValuePair<string, string> pValue in pram)
            {
                Commd.Parameters.AddWithValue(pValue.Key, pValue.Value);
            }
            iReturn = Commd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            dbConn.Close();
            throw ex;
        }
        finally { dbConn.Close(); }
        return iReturn;
    }
    public static string ExcuetStoreProcedure(Dictionary<string, string> pram, string SPName = "")
    {

        string iReturn = "";
        try
        {
            dbConn.Open();
            Commd = new SqlCommand();
            Commd.Connection = dbConn;
            Commd.CommandType = CommandType.StoredProcedure;
            Commd.CommandText = SPName;
            foreach (KeyValuePair<string, string> pValue in pram)
            {
                Commd.Parameters.AddWithValue(pValue.Key, pValue.Value);
            }
            iReturn = Commd.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            dbConn.Close();
            throw ex;
        }
        finally { dbConn.Close(); }
        return iReturn;
    }


    public static object GetDatastring(string Query, SqlTransaction sqlTrans, SqlConnection conn)
    {
        object sReturn = "";
        //
        try
        {
            //openConnection();
            Commd = new SqlCommand();
            Commd.Connection = conn;
            Commd.Transaction = sqlTrans;
            Commd.CommandText = Query;
            Commd.CommandType = CommandType.Text;
            sReturn = (object)Commd.ExecuteScalar();

        }
        catch (Exception ex)
        {
            clsoeConnection();
            throw ex;
        }
        return sReturn;
    }

    public static bool UpdateDataAdapter(string Query, DataTable dtUpdate)
    {
        int iReturn = 0;
        //
        try
        {
            openConnection();
            DataTable dtDummy = new DataTable();


            Commd = new SqlCommand();
            Commd.Connection = dbConn;

            Commd.CommandText = Query;
            Commd.CommandType = CommandType.Text;

            SqlDataAdapter da = new SqlDataAdapter(Commd);

            da.FillSchema(dtDummy, SchemaType.Source);
            da.Fill(dtDummy);

            SqlCommandBuilder combuild = new SqlCommandBuilder(da);
            da.InsertCommand = combuild.GetInsertCommand();
            da.UpdateCommand = combuild.GetUpdateCommand();
            da.DeleteCommand = combuild.GetDeleteCommand();
            dtDummy = dtUpdate.Copy();

            iReturn = da.Update(dtDummy);
            if (iReturn > 0) { clsoeConnection(); return true; } else { clsoeConnection(); return false; }


        }
        catch (Exception ex)
        {
            clsoeConnection();
            throw ex;
        }

    }
    public static void UpdateDataAdapter(string Query, DataTable dtUpdate, SqlTransaction sqlTrans)
    {

        //
        try
        {
            openConnection();
            DataTable dtDummy = new DataTable();


            Commd = new SqlCommand();
            Commd.Connection = dbConn;
            Commd.Transaction = sqlTrans;
            Commd.CommandText = Query;
            Commd.CommandType = CommandType.Text;

            SqlDataAdapter da = new SqlDataAdapter(Commd);

            da.FillSchema(dtDummy, SchemaType.Source);
            da.Fill(dtDummy);

            SqlCommandBuilder combuild = new SqlCommandBuilder(da);
            da.InsertCommand = combuild.GetInsertCommand();
            da.UpdateCommand = combuild.GetUpdateCommand();
            da.DeleteCommand = combuild.GetDeleteCommand();
            dtDummy = dtUpdate.Copy();

            da.Update(dtDummy);
            // if (iReturn > 0) { clsoeConnection(); return true; } else { clsoeConnection(); return false; }


        }
        catch (Exception ex)
        {
            clsoeConnection();
            throw ex;
        }

    }
    public static DataTable GetDataTable(string SqlQuery, SqlTransaction sqlTrans)
    {
        DataTable DtResult = new DataTable();
        //
        try
        {
            openConnection();
            Commd = new SqlCommand();
            Commd.Connection = dbConn;
            Commd.Transaction = sqlTrans;
            Commd.CommandText = SqlQuery;
            Commd.CommandType = CommandType.Text;

            SqlDa = new SqlDataAdapter(Commd);
            SqlDa.FillSchema(DtResult, SchemaType.Source);
            SqlDa.Fill(DtResult);
        }
        catch (Exception ex)
        {
            clsoeConnection();
            throw ex;
        }
       
        return DtResult;
    }
}

