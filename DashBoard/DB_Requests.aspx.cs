using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DashBoardForm : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adr;
    DataTable dt;
    protected static string DashBoard = ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
       if(!IsPostBack)
        {
            using (con = new SqlConnection(DashBoard))
            {
                using (cmd = new SqlCommand("GetAllDashboard", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);

                    rdFormLst.DataSource = dt;
                    rdFormLst.DataTextField = "FormName";
                    rdFormLst.DataValueField = "FormId";
                    rdFormLst.DataBind();

                }
            }

        }
     
    }




    protected void NextBtn_Click(object sender, EventArgs e)
    {
       
        int count = 0;
        using (con = new SqlConnection(DashBoard))
        {
          
            using (cmd = new SqlCommand("checkRequiredApproval", con))
            {
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@smallId", Session["StuSmallId"]);
                cmd.Parameters.AddWithValue("@FormId", rdFormLst.SelectedValue);
               
                adr.Fill(dt);
                count = int.Parse(dt.Rows[0]["NotValid"].ToString());
                adr.Dispose();
                dt.Dispose();
            }
        }
        if (count > 0)
        {
            divError.Visible = true;
            return;
        }
        else
        {
            using (con = new SqlConnection(DashBoard))
            {
                using (cmd = new SqlCommand("GetFormPageURL", con))
                {
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    cmd.CommandType = CommandType.StoredProcedure;                   
                    cmd.Parameters.AddWithValue("@FormId", rdFormLst.SelectedValue);
                    adr.Fill(dt);
                    if (!string.IsNullOrEmpty(dt.Rows[0]["PageURL"].ToString()))
                    {
                    Response.Redirect(dt.Rows[0]["PageURL"].ToString());

                    }
                }
            }

        }

    }
}