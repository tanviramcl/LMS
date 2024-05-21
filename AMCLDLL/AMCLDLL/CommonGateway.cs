using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using System.Collections.Generic;
using AMCLDLL;
using AMCLBL;

/// <summary>
/// Summary description for CommonGateway
/// </summary>
public class CommonGateway
{
    DBConnector dbConectorObj = new DBConnector();
    private OracleTransaction Trans;
    private OracleConnection AppConn = new OracleConnection(ConfigReader.SecurityAnalysis);
    private OracleCommand Cmnd;
    public CommonGateway()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Insert(Hashtable hashTable, string SourceTable)
    {

        OracleConnection oracleConn = dbConectorObj.GetConnection;
        OpenAppConnection();
        try
        {
            OracleCommand oracleComd = new OracleCommand("SELECT * FROM " + SourceTable + " WHERE NOT(1=1)", oracleConn);
            OracleDataAdapter ODP = new OracleDataAdapter(oracleComd);
            DataSet DS = new DataSet();
            ODP.Fill(DS, SourceTable);
            DataRow drAddrow = DS.Tables[0].NewRow();

            foreach (object OBJ in hashTable.Keys)
            {
                string colName = Convert.ToString(OBJ);
                drAddrow[colName] = hashTable[OBJ];
            }
            DS.Tables[0].Rows.Add(drAddrow);
            OracleCommandBuilder ocmd = new OracleCommandBuilder(ODP);
            ODP.InsertCommand = ocmd.GetInsertCommand();
            return ODP.Update(DS, SourceTable);
        }
        catch (OracleException ex)
        {
            if (Trans != null)
            {

                Trans.Rollback();
                Trans = null;
            }
            throw ex;
        }
        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }
            throw Ex;

        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }
        }

    }



    public int _INSERT<T>(List<T> data, string SourceTable)
    {

        OracleConnection oracleConn = dbConectorObj.GetConnection;
        OpenAppConnection();
        try
        {
            OracleCommand oracleComd = new OracleCommand("SELECT * FROM " + SourceTable + " WHERE NOT(1=1)", oracleConn);
            OracleDataAdapter ODP = new OracleDataAdapter(oracleComd);
            DataSet DS = DataTableToList.ToDataSet(data);
            ODP.Fill(DS, SourceTable);
            OracleCommandBuilder ocmd = new OracleCommandBuilder(ODP);
            ODP.InsertCommand = ocmd.GetInsertCommand();
            return ODP.Update(DS, SourceTable);
        }
        catch (OracleException ex)
        {
            if (Trans != null)
            {

                Trans.Rollback();
                Trans = null;
            }
            throw ex;
        }
        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }
            throw Ex;

        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }
        }

    }









    public short Update(Hashtable HTable, string SourceTableName, string Filter)
    {
        OpenAppConnection();
        try
        {

            if (Trans == null)
            {
                Cmnd = new OracleCommand("SELECT * FROM " + SourceTableName + " WHERE " + Filter, AppConn);
            }
            else
            {
                Cmnd.CommandText = "SELECT * FROM " + SourceTableName + " WHERE " + Filter;
            }


            OracleDataAdapter ADP = new OracleDataAdapter(Cmnd);
            DataSet DS = new DataSet();
            ADP.Fill(DS, SourceTableName);
            int rowNumber = 0;
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                DataRow DR_UPDATE = DS.Tables[0].Rows[rowNumber];
                foreach (object OBJ in HTable.Keys)
                {
                    string COLUMN_NAME = Convert.ToString(OBJ);
                    DR_UPDATE[COLUMN_NAME] = HTable[OBJ];
                }
                OracleCommandBuilder BLD = new OracleCommandBuilder(ADP);
                ADP.UpdateCommand = BLD.GetUpdateCommand();
                ADP.Update(DS, SourceTableName);
                rowNumber++;

            }

            return 1;

        }
        catch (OracleException Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;
        }

        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;
        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }

        }
    }
    public int DeleteByCommand(string SourceTableName, string Filter)
    {
        OpenAppConnection();
        try
        {

            if (Trans == null)
            {
                Cmnd = new OracleCommand("DELETE FROM " + SourceTableName + " WHERE " + Filter, AppConn);
            }
            else
            {
                Cmnd.CommandText = "DELETE FROM " + SourceTableName + " WHERE " + Filter;
            }

            int affectedRow = Cmnd.ExecuteNonQuery();
            return affectedRow;
        }
        catch (OracleException Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;

        }
        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;

        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }

        }
    }
    public DataTable Select(string queryString)
    {
        OracleConnection oracleConn = dbConectorObj.GetConnection;
        OracleCommand oraclecmd = new OracleCommand(queryString, oracleConn);
        oraclecmd.CommandType = CommandType.Text;
        DataSet ds = new DataSet();
        OracleDataAdapter adp = new OracleDataAdapter();
        adp.SelectCommand = oraclecmd;
         adp.Fill(ds);
        return ds.Tables[0];
    }
    public void BeginTransaction()
    {
        OpenAppConnection();
        if (Trans == null)
        {   
            Cmnd = AppConn.CreateCommand();
            Trans = AppConn.BeginTransaction();
            Cmnd.Transaction = Trans;
        }
    }
    public void CommitTransaction()
    {
        if (Trans != null)
        {
            Trans.Commit();
            Trans = null;
        }
        CloseAppConnection();
    }
    public void RollbackTransaction()
    {
        if (Trans != null)
        {
            Trans.Rollback();
            Trans = null;
        }
        CloseAppConnection();
    }
    private void OpenAppConnection()
    {
        string ConnectionString = ConfigReader.SecurityAnalysis;
        if (!ConnectionString.Equals(""))
        {

            if (AppConn.State != ConnectionState.Open)
            {
                AppConn.Open();
            }
        }
    }
    private void CloseAppConnection()
    {
        if (AppConn.State == ConnectionState.Open)
        {
            AppConn.Close();

        }
    }


    public int ExecuteNonQuery(string strSQL)
    {
        OpenAppConnection();
        try
        {
            if (Trans == null)
            {
                Cmnd = new OracleCommand(strSQL, AppConn);
            }
            else
            {
                Cmnd.CommandText = strSQL;
            }

            int affectedRow = Cmnd.ExecuteNonQuery();

            return affectedRow;
        }

        catch (OracleException Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }
            throw Ex;
        }
        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;
        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }

        }
    }



    public long GetMaxNo(string tableName, string columnName)
    {
        OpenAppConnection();

        try
        {
            if (Trans == null)
            {
                Cmnd = new OracleCommand(string.Format("SELECT MAX({0})AS ID FROM {1}", columnName, tableName), AppConn);
            }
            else
            {
                Cmnd.CommandText = string.Format("SELECT MAX({0}) AS ID FROM {1}", columnName, tableName);
            }
            long lngMaxno = Convert.ToInt64(Cmnd.ExecuteScalar());

            return lngMaxno;
        }
        catch (OracleException Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }
            throw Ex;

        }
        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }
            throw Ex;

        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }

        }
    }
}
