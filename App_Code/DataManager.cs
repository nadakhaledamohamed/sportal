using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

/// <summary>
/// Summary description for DataManager
/// </summary>
public class DataManager
{
    public DataManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //---------------------------------------------------------------------------------------------------------
    /// <summary>
    /// this function gets the value of the connection string from the web.config file
    /// the connection in the web.config must be named (DatabaseConnectionString)
    /// </summary>
    /// <returns></returns>
    public string ConnectionString()
    {
        //return System.Configuration.ConfigurationManager.ConnectionStrings["MyString"].ConnectionString;
        return "provider=Microsoft.Jet.OLEDB.4.0;data source =" + HttpContext.Current.Server.MapPath("~/app_Data/BioMDB.mdb") + ";Persist Security Info=True;";
    }
    //---------------------------------------------------------------------------------------------------------
    /// <summary>
    /// This function used to insert data to the sql database or to update existing data 
    /// Passing Parameter without (@) mark
    /// </summary>
    /// <param name="Procedure">the Stored Procedure name that you want to execute </param>
    /// <param name="OleDbParameterNamesandValues">pass the names and then pass the values</param>
    public void ExecuteNonQuery(string Procedure, params string[] OleDbCommandParameters)
    {
        OleDbConnection con = new OleDbConnection(ConnectionString());
        OleDbCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        for (int i = 0; i < (OleDbCommandParameters.Length / 2); i++)
        {
            cmd.Parameters.Add(new OleDbParameter("@" + OleDbCommandParameters[i], OleDbCommandParameters[i + (OleDbCommandParameters.Length / 2)]));
        }
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }
    }
    //---------------------------------------------------------------------------------------------------------
    /// <summary>
    /// This function used to insert data to the sql database or to update existing data 
    /// Passing Parameter without (@) mark    
    /// </summary>
    /// <param name="Procedure">the Stored Procedure name that you want to execute</param>
    public void ExecuteNonQuery(string Procedure)
    {
        OleDbConnection con = new OleDbConnection(ConnectionString());
        OleDbCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }
    }
    //---------------------------------------------------------------------------------------------------------
    /// <summary>
    /// this function returns a data table from the database
    /// </summary>
    /// <param name="Procedure">the Stored Procedure name that you want to execute </param>
    public DataTable ExecuteDataTableQuery(string Procedure)
    {
        OleDbConnection con = new OleDbConnection(ConnectionString());
        OleDbCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds, "ReturnTable");
        return ds.Tables["ReturnTable"];
    }
    //---------------------------------------------------------------------------------------------------------
    /// <summary>
    /// this function returns a data table from the database
    /// </summary>
    /// <param name="Procedure">the Stored Procedure name that you want to execute </param>
    public DataTable ExecuteDataTable(string Procedure)
    {
        OleDbConnection con = new OleDbConnection(ConnectionString());
        OleDbCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds, "ReturnTable");
        return ds.Tables["ReturnTable"];
    }
    //---------------------------------------------------------------------------------------------------------
    /// <summary>
    /// this function returns a data table from the database
    /// Passing the parameters without (@) mark
    /// </summary>
    /// <param name="Procedure">the Stored Procedure name that you want to execute </param>
    /// <param name="OleDbParameterNamesandValues">pass the names and then pass the values</param>
    /// <returns></returns>
    public DataTable ExecuteDataTable(string Procedure, params string[] OleDbCommandParameters)
    {
        OleDbConnection con = new OleDbConnection(ConnectionString());
        OleDbCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        for (int i = 0; i < (OleDbCommandParameters.Length / 2); i++)
        {
            cmd.Parameters.Add(new OleDbParameter("@" + OleDbCommandParameters[i], OleDbCommandParameters[i + (OleDbCommandParameters.Length / 2)]));
        }
        DataSet ds = new DataSet();
        da.Fill(ds, "ReturnTable");
        return ds.Tables["ReturnTable"];

    }
    //---------------------------------------------------------------------------------------------------------
    /// <summary>
    /// This function used to return one string value from database
    /// the value is the first column in the first row of the return data
    /// Passing Parameter without (@) mark
    /// </summary>
    /// <param name="Procedure">the Stored Procedure name that you want to execute </param>
    public string ExecuteScalar(string Procedure)
    {
        OleDbConnection con = new OleDbConnection(ConnectionString());
        OleDbCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        string _value = "";
        try
        {
            con.Open();
            if (cmd.ExecuteScalar() != null)
            {
                _value = cmd.ExecuteScalar().ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }
        return _value;
    }
    //---------------------------------------------------------------------------------------------------------
    /// <summary>
    /// This function used to return one string value from database
    /// the value is the first column in the first row of the return data
    /// </summary>
    /// <param name="Procedure">the Stored Procedure name that you want to execute </param>
    /// <param name="OleDbParameterNamesandValues">pass the names and then pass the values</param>
    public string ExecuteScalar(string Procedure, params string[] OleDbCommandParameters)
    {
        OleDbConnection con = new OleDbConnection(ConnectionString());
        OleDbCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        string _value = "";
        for (int i = 0; i < (OleDbCommandParameters.Length / 2); i++)
        {
            cmd.Parameters.Add(new OleDbParameter("@" + OleDbCommandParameters[i], OleDbCommandParameters[i + (OleDbCommandParameters.Length / 2)]));
        }
        try
        {
            con.Open();
            if (cmd.ExecuteScalar() != null)
            {
                _value = cmd.ExecuteScalar().ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }
        return _value;
    }
    //---------------------------------------------------------------------------------------------------------
    /// <summary>
    /// This function used to return one string value from database
    /// the value is the first column in the first row of the return data
    /// Passing Parameter without (@) mark
    /// </summary>
    /// <param name="Procedure">the Stored Procedure name that you want to execute </param>
    /// <param name="con">The Connection that the OleDbDataReader will use</param>
    public OleDbDataReader ExecuteDataReader(OleDbConnection con, string Procedure)
    {
        OleDbCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        OleDbDataReader dr;
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            dr = cmd.ExecuteReader();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dr;
    }
    //---------------------------------------------------------------------------------------------------------
    /// <summary>
    /// This function used to return one string value from database
    /// the value is the first column in the first row of the return data
    /// </summary>
    /// <param name="Procedure">the Stored Procedure name that you want to execute </param>
    /// <param name="OleDbParameterNamesandValues">pass the names and then pass the values</param>
    /// <param name="con">The Connection that the OleDbDataReader will use</param>
    public OleDbDataReader ExecuteDataReader(OleDbConnection con, string Procedure, params string[] OleDbCommandParameters)
    {
        OleDbCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        OleDbDataReader dr;
        for (int i = 0; i < (OleDbCommandParameters.Length / 2); i++)
        {
            cmd.Parameters.Add(new OleDbParameter("@" + OleDbCommandParameters[i], OleDbCommandParameters[i + (OleDbCommandParameters.Length / 2)]));
        }
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            dr = cmd.ExecuteReader();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dr;
    }

}