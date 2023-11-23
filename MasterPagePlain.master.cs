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
        if (!IsPostBack)
        {
            if (Session["StuSmallId"] == null)
                Response.Redirect("~/Login.aspx");

            var o = Session["StuSmallId"];
            if (o != null && string.IsNullOrEmpty(o.ToString()))
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
            //    menu_admin.Visible = true;
              litUsername.Text = Session["StuDisplayName"].ToString();
			  
               // Response.Redirect("Student_Affairs_Requests.aspx");
                //CheckPassChange();
            }

            
        }
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
