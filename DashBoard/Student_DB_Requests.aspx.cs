using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;




public partial class St_DB_Requests : System.Web.UI.Page
{


   
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adr;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {

        //AssesmentPetiton_Div
        if (!IsPostBack)
        {
            #region Master

            if (Page.Master != null)
            {
                //((HtmlAnchor)Page.Master.FindControl("hlforms")).Attributes["class"] = "dropdown-toggle active-parent active";
                //((HtmlControl)Page.Master.FindControl("menu5")).Attributes["style"] = "display: block;";
                //((HtmlAnchor)Page.Master.FindControl("hlApp22")).Attributes["class"] = "ajax-link active-parent active";
            }

            #endregion
            FillGv(int.Parse(Session["StuSmallId"].ToString()));
        }
    }







    protected void gvForms_PreRender(object sender, EventArgs e)
    {

    }
    protected void GridView_PreRender(object sender, EventArgs e)
    {
        if (((GridView)sender).HeaderRow != null)
        {
            ((GridView)sender).HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (((GridView)sender).FooterRow != null)
        {
            ((GridView)sender).FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void gvTypes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      
    }
    private void FillGv(Int64 id)
    {
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString))
        {
            using (cmd = new SqlCommand("Get_student_DB_Requests", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SmallId", id);
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }


        }
        gvForms.DataSource = dt;
        gvForms.DataBind();
        RequestDiv.Visible = true;


    }
    public void FillDetailsGridView(Int64 id)
    {


    }

 

    protected void NewType_Click(object sender, EventArgs e)
    {
        Response.Redirect("DB_Requests.aspx", false);
    }
    protected void gvForms_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:               
                DataRowView myDataRowView = (DataRowView)e.Row.DataItem;
           
                Label not = (Label)e.Row.FindControl("txtNotification");
                if (not != null)
                {
                    if (myDataRowView["UnReadComment"].ToString() != "0")
                    {
                        not.Text = "(" + myDataRowView["UnReadComment"].ToString() + ")";
                    }

                }
                break;
        }

    }



    [System.Web.Services.WebMethod]
    public static void SetUserName(Int64 Detid)
    {
        //Page objPage = new Page();
        //objPage.Session["DetailId"] = Detid;

    }




  
}
