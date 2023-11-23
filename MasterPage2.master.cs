using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    readonly SqlConnection _connection =
        new SqlConnection(ConfigurationManager.ConnectionStrings["NGUAuthentication"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["EmployeeId"].ToString()))
        {
            Response.Redirect("~/Login.aspx");
        }

        if (!IsPostBack)
        {            
            if (string.IsNullOrEmpty(Session["EmployeeId"].ToString()))
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                litUsername.Text = Session["EmployeeName"].ToString();
                // display menue
                MenueShow();
            }
        }
    }


    private void MenueShow()
    {
        financial_menu.Visible = false;
        registrar_menu.Visible = false;
        SchoolMenu.Visible = false;
        SqlCommand cmd = new SqlCommand("GetUserPagePermissions", _connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@UserId", Session["EmployeeId"].ToString());
        cmd.Parameters.AddWithValue("@ApplicationId", "7");
        if (_connection.State == ConnectionState.Closed)
            _connection.Open();

        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            switch (reader["PageId"].ToString())
            {
                case "19":
                    registrar_menu.Visible = true;
                    break;
                case "20":
                    financial_menu.Visible = true;
                    break;
                case "21":
                    SchoolMenu.Visible = true;
                    break;
                default:
                    break;
            }
        }
        reader.Close();
        _connection.Close();
    }
    private void CheckPassChange()
    {
        SqlCommand cmd = new SqlCommand("SP_CheckPasswordChange", _connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@userID", Session["EmploNum"].ToString());
        if (_connection.State == ConnectionState.Closed)
            _connection.Open();


        var res = cmd.ExecuteScalar();
        _connection.Close();

        if (res == "0")
        {
            Response.Write("<script language='javascript'>window.alert('You must change your default password');window.location='Pass/Default.aspx';</script>");
        }
    }
}
