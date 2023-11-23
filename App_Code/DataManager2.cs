using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DataManager2
/// </summary>
public class DataManager2
{
    public DataManager2()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public class Parameters
    {
        public Parameters(string _ParametersName, SqlDbType _FieldType, string _ParameterValue)
        {
            ParametersName = _ParametersName;


            ParameterValue = _ParameterValue;


            FieldType = _FieldType;
        }
        #region Properties
        private string _Name;
        private string _Value;
        private SqlDbType _Type;
        public string ParametersName
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }

        }
        public string ParameterValue
        {
            get
            {
                return _Value;
            }

            set
            {
                _Value = value;
            }

        }
        public SqlDbType FieldType
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }

        }

        #endregion
    }
    //---------------------------------------------------------------------------------------------------------
    /// <summary>
    /// this function gets the value of the connection string from the web.config file
    /// the connection in the web.config must be named (DatabaseConnectionString)
    /// </summary>
    /// <returns></returns>
    public string ConnectionString()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["MyString"].ConnectionString;
        //return "Data Source=.;Initial Catalog=attend;Persist Security Info=True;User ID=sa;Password=Wizard123;Max Pool Size=100;Connect Timeout=60;";
    }
	public string ContactConnectionString()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["ContactConnectionString"].ConnectionString;
        //return "Data Source=.;Initial Catalog=attend;Persist Security Info=True;User ID=sa;Password=Wizard123;Max Pool Size=100;Connect Timeout=60;";
    }
    //---------------------------------------------------------------------------------------------------------
    /// <summary>
    /// This function used to insert data to the sql database or to update existing data 
    /// Passing Parameter without (@) mark
    /// </summary>
    /// <param name="Procedure">the Stored Procedure name that you want to execute </param>
    /// <param name="SqlParameterNamesandValues">pass the names and then pass the values</param>
    public void ExecuteNonQuery(string Procedure, params string[] SqlCommandParameters)
    {
        SqlConnection con = new SqlConnection(ConnectionString());
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        for (int i = 0; i < (SqlCommandParameters.Length / 2); i++)
        {
            cmd.Parameters.Add(new SqlParameter("@" + SqlCommandParameters[i], SqlCommandParameters[i + (SqlCommandParameters.Length / 2)]));
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
        SqlConnection con = new SqlConnection(ConnectionString());
        SqlCommand cmd = con.CreateCommand();
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
        SqlConnection con = new SqlConnection(ConnectionString());
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
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
        SqlConnection con = new SqlConnection(ConnectionString());
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds, "ReturnTable");
        return ds.Tables["ReturnTable"];
    }

    public DataTable ExecuteDataTable(string Procedure, CommandType type)
    {
        SqlConnection con = new SqlConnection(ConnectionString());
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = type;
        cmd.CommandText = Procedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
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
    /// <param name="SqlParameterNamesandValues">pass the names and then pass the values</param>
    /// <returns></returns>
    public DataTable ExecuteDataTable(string Procedure, params string[] SqlCommandParameters)
    {
        SqlConnection con = new SqlConnection(ConnectionString());
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        for (int i = 0; i < (SqlCommandParameters.Length / 2); i++)
        {
            cmd.Parameters.Add(new SqlParameter("@" + SqlCommandParameters[i], SqlCommandParameters[i + (SqlCommandParameters.Length / 2)]));
        }
        DataSet ds = new DataSet();
        da.Fill(ds, "ReturnTable");
        return ds.Tables["ReturnTable"];

    }

    public DataTable ExecuteDataTable(string connection,string Procedure, CommandType type, DataTable SqlCommandParameters)
    {
        SqlConnection con = new SqlConnection(connection);
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.CommandText = Procedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        foreach (DataRow parm in SqlCommandParameters.Rows)
        {
            cmd.Parameters.Add(new SqlParameter(parm[0].ToString(), parm[1].ToString()));
        }
        DataSet ds = new DataSet();
        da.Fill(ds, "ReturnTable");
        return ds.Tables["ReturnTable"];

    }
	
	public DataTable ExecuteDataTable(string Procedure, CommandType type, DataTable SqlCommandParameters)
    {
        SqlConnection con = new SqlConnection(ConnectionString());
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.CommandText = Procedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        foreach (DataRow parm in SqlCommandParameters.Rows)
        {
            cmd.Parameters.Add(new SqlParameter(parm[0].ToString(), parm[1].ToString()));
        }
        DataSet ds = new DataSet();
        da.Fill(ds, "ReturnTable");
        return ds.Tables["ReturnTable"];

    }

    public bool ExecuteNonQuery(string Procedure, CommandType type, DataTable SqlCommandParameters)
    {
        try
        {

            SqlConnection con = new SqlConnection(ConnectionString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.CommandText = Procedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            foreach (DataRow parm in SqlCommandParameters.Rows)
            {
                cmd.Parameters.Add(new SqlParameter(parm[0].ToString(), parm[1].ToString()));
            }

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            return true;

        }
        catch (Exception)
        {

            return false;
        }

    }
	
	public bool ExecuteNonQuery(string connection,string Procedure, CommandType type, DataTable SqlCommandParameters)
    {
        try
        {

            SqlConnection con = new SqlConnection(connection);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.CommandText = Procedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            foreach (DataRow parm in SqlCommandParameters.Rows)
            {
                cmd.Parameters.Add(new SqlParameter(parm[0].ToString(), parm[1].ToString()));
            }

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            return true;

        }
        catch (Exception)
        {

            return false;
        }

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
        SqlConnection con = new SqlConnection(ConnectionString());
        SqlCommand cmd = con.CreateCommand();
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
    /// <param name="SqlParameterNamesandValues">pass the names and then pass the values</param>
    public string ExecuteScalar(string Procedure, params string[] SqlCommandParameters)
    {
        SqlConnection con = new SqlConnection(ConnectionString());
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        string _value = "";
        for (int i = 0; i < (SqlCommandParameters.Length / 2); i++)
        {
            cmd.Parameters.Add(new SqlParameter("@" + SqlCommandParameters[i], SqlCommandParameters[i + (SqlCommandParameters.Length / 2)]));
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
    /// <param name="con">The Connection that the SqlDataReader will use</param>
    public SqlDataReader ExecuteDataReader(SqlConnection con, string Procedure)
    {
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        SqlDataReader dr;
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
    /// <param name="SqlParameterNamesandValues">pass the names and then pass the values</param>
    /// <param name="con">The Connection that the SqlDataReader will use</param>
    public SqlDataReader ExecuteDataReader(SqlConnection con, string Procedure, params string[] SqlCommandParameters)
    {
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = Procedure;
        SqlDataReader dr;
        for (int i = 0; i < (SqlCommandParameters.Length / 2); i++)
        {
            cmd.Parameters.Add(new SqlParameter("@" + SqlCommandParameters[i], SqlCommandParameters[i + (SqlCommandParameters.Length / 2)]));
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

    public string ExecuteNonQueryOut(string Procedure, string PrimaryKey, params DataManager2.Parameters[] SqlCommandParameters)
    {
        SqlConnection con = new SqlConnection(new DataManager2().ConnectionString());
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = Procedure;
        string _value = "";
        try
        {
            con.Open();
            SqlParameter parameter = new SqlParameter(PrimaryKey, SqlDbType.Int);
            parameter.Direction = ParameterDirection.Output;
            parameter.Value = "1";
            cmd.Parameters.Add(parameter);
            foreach (DataManager2.Parameters prm in SqlCommandParameters)
            {
                cmd.Parameters.Add(prm.ParametersName, prm.FieldType).Value = prm.ParameterValue;
            }
            cmd.ExecuteNonQuery();
            _value = parameter.Value.ToString();
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
}