using System;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Configuration;
using System.Data;
using System.Runtime.CompilerServices;

/// <summary>
/// Summary description for Privilages
/// </summary>
public class Privilages
{
    //string Connection = new Connection().ConnercionString;
    readonly SqlConnection _connection =
        new SqlConnection(ConfigurationManager.ConnectionStrings["Authentication"].ConnectionString);

    readonly SqlConnection _attendconnection =
          new SqlConnection(ConfigurationManager.ConnectionStrings["MyString"].ConnectionString);

    string _displayName = "",
        _mail = "",
        _sAmAccountName = "",
        _company = "",
        _department = "",
        _physicalDeliveryOfficeName = "",
        _telephoneNumber = "",
        _title = "",
        _employeeId = "",
        _description = "";

     public bool IsAuthenticated(string username, string pwd)
    {
        return true;
        var isTrue = false;
        const string ldapPath = "LDAP://DC=ngu,DC=local";

        var entry = new DirectoryEntry(ldapPath, username+"@ngu.edu.eg", pwd);
        try
        {
            //Bind to the native AdsObject to force authentication.
            Object obj ;
            DirectorySearcher search;
            try{
                obj = entry.NativeObject;
            }
            catch {
                obj = null;
            }
            try{
                search = new DirectorySearcher(entry) { Filter = "(SAMAccountName=" + username + ")" };
            }
            catch {
                search = new DirectorySearcher(entry) { Filter = "(mail=" + username + "@ngu.edu.eg)" };
            }

            search.PropertiesToLoad.Add("cn");

            search.PropertiesToLoad.Add("displayName");
            search.PropertiesToLoad.Add("mail");
            search.PropertiesToLoad.Add("sAMAccountName");
            search.PropertiesToLoad.Add("COMPANY");
            search.PropertiesToLoad.Add("DEPARTMENT");
            search.PropertiesToLoad.Add("physicalDeliveryOfficeName");
            search.PropertiesToLoad.Add("telephoneNumber");
            search.PropertiesToLoad.Add("title");
            search.PropertiesToLoad.Add("employeeID");
            search.PropertiesToLoad.Add("description");

            SearchResult result = null ;
            result = search.FindOne();

            if (result==null)
            {
                search = new DirectorySearcher(entry) { Filter = "(mail=" + username + "@ngu.edu.eg)" };
                search.PropertiesToLoad.Add("cn");

                search.PropertiesToLoad.Add("displayName");
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("sAMAccountName");
                search.PropertiesToLoad.Add("COMPANY");
                search.PropertiesToLoad.Add("DEPARTMENT");
                search.PropertiesToLoad.Add("physicalDeliveryOfficeName");
                search.PropertiesToLoad.Add("telephoneNumber");
                search.PropertiesToLoad.Add("title");
                search.PropertiesToLoad.Add("employeeID");
                search.PropertiesToLoad.Add("description");

                result = search.FindOne();
            }
            var filterAttribute = (string)result.Properties["cn"][0];

            try
            {
                _displayName = Convert.ToString(result.Properties["displayName"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _mail = Convert.ToString(result.Properties["mail"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _sAmAccountName = Convert.ToString(result.Properties["sAMAccountName"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _company = Convert.ToString(result.Properties["COMPANY"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _department = Convert.ToString(result.Properties["DEPARTMENT"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _physicalDeliveryOfficeName = Convert.ToString(result.Properties["physicalDeliveryOfficeName"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _telephoneNumber = Convert.ToString(result.Properties["telephoneNumber"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _title = Convert.ToString(result.Properties["title"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _employeeId = Convert.ToString(result.Properties["employeeID"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _description = Convert.ToString(result.Properties["description"][0]);
            }
            catch
            {
                // ignored
            }


            isTrue = true;
        }
        catch (Exception ex)
        {
            isTrue = false;
        }

       return isTrue;
  
       
    }

    public bool IsApplicationAllowed(int applicationId, string username, string password, ref string displayname,
        ref int userid)
    {
        var isAllowed = false;
        if (IsAuthenticated(username, password))
        {
            bool x = isAllowedInDB(applicationId, username, ref displayname, ref userid);

            isAllowed = x;
        }
        else
        {

        }
        return isAllowed;
    }


    public bool isAllowedInDB(int applicationId, string username, ref string userNameEn, ref int userid)
    {
        bool isAllowedInDb = false;
        SqlCommand cmd = new SqlCommand("CheckUserApplicationAllow", _connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.Add("applicationId", SqlDbType.Int).Value = applicationId;
        cmd.Parameters.Add("username", SqlDbType.NVarChar).Value = username;
        if (_connection.State == ConnectionState.Closed)
            _connection.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                userNameEn = reader["userNameEn"].ToString();
                userid = int.Parse(reader["userId"].ToString());
                isAllowedInDb = true;
            }
            reader.Close();
            _connection.Close();
        }
        else
        {
            userNameEn = null;
            reader.Close();
            _connection.Close();
        }
        return isAllowedInDb;
    }

    public bool isHasPermission(int actionID, int userid)
    {
        bool isAllowedInDb = false;
        SqlCommand cmd = new SqlCommand("CheckUserPermissionAllow", _connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.Add("@pageActionID", SqlDbType.Int).Value = actionID;
        cmd.Parameters.Add("@userID", SqlDbType.NVarChar).Value = userid;
        if (_connection.State == ConnectionState.Closed)
            _connection.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            isAllowedInDb = true;

            reader.Close();
            _connection.Close();
        }
        else
        {
            isAllowedInDb = false;

            reader.Close();
            _connection.Close();
        }
        return isAllowedInDb;
    }

    public void AddLogToDatabase(int userId, int pageActionId, string pageActionDetailsOptional, string logIp)
    {
        try
        {



            var cmd = new SqlCommand("InsertLog", _connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            var pUserId = cmd.Parameters.Add("@UserId", SqlDbType.Int);
            var pPageActionId = cmd.Parameters.Add("@PageActionId", SqlDbType.Int);
            var ppageActionDetailsOptional = cmd.Parameters.Add("@PageActionDetails", SqlDbType.NVarChar, 500);
            var pLogIp = cmd.Parameters.Add("@LogIp", SqlDbType.NVarChar, 500);


            pUserId.Value = userId;
            pPageActionId.Value = pageActionId;
            ppageActionDetailsOptional.Value = pageActionDetailsOptional;
            pLogIp.Value = logIp;

            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            var strRowAffect = cmd.ExecuteNonQuery().ToString();
            _connection.Close();
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
            //throw;
        }
    }

    public void AddStudentLogToDatabase(int studentId, int studentPageId, string logIp, string Description)
    {
        try
        {
            var cmd = new SqlCommand("InsertStudentLog", _connection) { CommandType = CommandType.StoredProcedure };
            var pStudentId = cmd.Parameters.Add("@StudentId", SqlDbType.Int);
            var pStudentPageId = cmd.Parameters.Add("@StudentPageId", SqlDbType.Int);
            var pLogIp = cmd.Parameters.Add("@Ip", SqlDbType.NVarChar, 500);
            var pDescription = cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 500);


            pStudentId.Value = studentId;
            pStudentPageId.Value = studentPageId;
            pLogIp.Value = logIp;
            pDescription.Value = Description;
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            var strRowAffect = cmd.ExecuteNonQuery().ToString();
            _connection.Close();
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
            //throw;
        }
    }

    public bool IsAuthenticatedStudent(string username, string password, ref string displayname, ref int smallId, ref int id, ref int schoolId, ref int batchId, ref int empId)
    {
        
        bool isok = false;
        if (IsAuthenticated(username, password))
        {
            SqlCommand cmd = new SqlCommand("GetStudentDataForLogin", _attendconnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("username", SqlDbType.NVarChar).Value = username;
            if (_attendconnection.State == ConnectionState.Closed)
                _attendconnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                
				 while (reader.Read())
                {
                    smallId = int.Parse(reader["SmallId"].ToString());
                    displayname = reader["Namee"].ToString();
                    id = int.Parse(reader["Id"].ToString());
                    if (string.IsNullOrEmpty(Convert.ToString(reader["schoolId"])))
                    {
                        schoolId = 0;
                    }
                    else
                    {
                        schoolId = int.Parse(reader["schoolId"].ToString());
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(reader["fallId"])))
                    {
                        batchId = 0;
                    }
                    else
                    {
                        batchId = int.Parse(reader["fallId"].ToString());
                    }
                    
                    empId = int.Parse(reader["Id"].ToString());
                }
                isok = true;
                reader.Close();
                _attendconnection.Close();
            }
            //isok = false;
            reader.Close();
            _attendconnection.Close();
        }
        //isok = false;
        return isok;
    }

    public bool GetStudentData(string username, ref string displayname, ref int smallId, ref int id, ref int schoolId, ref int batchId, ref int empId)
    {
     // return true;
       bool isok = false;
        SqlCommand cmd = new SqlCommand("GetStudentDataForLogin", _attendconnection)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.Add("username", SqlDbType.NVarChar).Value = username;
        if (_attendconnection.State == ConnectionState.Closed)
            _attendconnection.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {

            while (reader.Read())
            {
                smallId = int.Parse(reader["SmallId"].ToString());
                displayname = reader["Namee"].ToString();
                id = int.Parse(reader["Id"].ToString());
                if (string.IsNullOrEmpty(Convert.ToString(reader["schoolId"])))
                {
                    schoolId = 0;
                }
                else
                {
                    schoolId = int.Parse(reader["schoolId"].ToString());
                }
                if (string.IsNullOrEmpty(Convert.ToString(reader["fallId"])))
                {
                    batchId = 0;
                }
                else
                {
                    batchId = int.Parse(reader["fallId"].ToString());
                }

                empId = int.Parse(reader["Id"].ToString());
            }
            isok = true;
            reader.Close();
            _attendconnection.Close();
        }
        //isok = false;
        reader.Close();
        _attendconnection.Close();
        //isok = false;
        return isok;
    }

}