using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _DBComment : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adr;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Int64 Id = 0;
            int faid = 0;
           
            try
            {
              Id = Int64.Parse(Session["DetailId"].ToString());
                faid= int.Parse(Session["FormApprovalId"].ToString());

                // Id = Int64.Parse(Request.QueryString["detid"].ToString());
                // type = int.Parse(Request.QueryString["type"].ToString()); ;

                Session["DetailId"] = Id;                
                GetComments(Id,faid);
            }
            catch (Exception ex)
            {
                Id = 0;
                // throw;

                // Response.Redirect("Student_Affairs_Requests.aspx",false);
            }


        }

    }
    public void GetComments(Int64 id,int faid)
    {
        if (id != 0)
        {
         
         //   usertype = getUsertype();
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString))
            {
                using (cmd = new SqlCommand("GetDBHistoryComments", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RequestID", id);
                    cmd.Parameters.AddWithValue("@formApproval", faid);
                    adr = new SqlDataAdapter(cmd);
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        HistoryComment.Controls
                                  .Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            System.Web.UI.HtmlControls.HtmlGenericControl createDiv =
       new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                            //  createDiv.ID = "createDiv";
                            string dv1 = "<div class='row'><div class='col-lg-8' style='color:cornflowerblue' ><b> From :</b>" + dt.Rows[i]["CommentBy"] + "</div><div class='col-lg-4' style='color:cornflowerblue' >" + dt.Rows[i]["CommentDate"] + "</div><div class='row'><div class='col-lg-12'><p><p>"+ dt.Rows[i]["CommentText"] + "</p></p><hr class='hrDiv'></div></div>";
                            //     createDiv.InnerHtml = " <b> From :</b>" + dt.Rows[i]["CommentBy"] + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Date:</b>" + dt.Rows[i]["CommentDate"] + "<p>" + dt.Rows[i]["CommentText"] + "<hr class='hrDiv'>";
                            createDiv.InnerHtml = dv1;
                            HistoryComment.Controls
                                .Add(createDiv);
                        }
                    }
                    else
                    {
                        System.Web.UI.HtmlControls.HtmlGenericControl createDiv =
new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                        createDiv.InnerHtml = "NO previous comments Exist";
                        HistoryComment.Controls
                               .Add(createDiv);

                    }
                }

            }


        }

    }

    protected void SendCommentBtn_Click(object sender, EventArgs e)
    {      
        SendCommentBtn.Visible = false;
        ExceptionLog l = new ExceptionLog();
        int type = 0;
        try
        {
            Int64 Id = Int64.Parse(Session["DetailId"].ToString());
           type =0; 

            if (Id != 0)
            {
                using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString))
                {
                    using (cmd = new SqlCommand("AddDBComment", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RequestId", Id);
                        cmd.Parameters.AddWithValue("@Comment", txtcomment.Text);
                        cmd.Parameters.AddWithValue("@FormApprovalId", type);
                        cmd.Parameters.AddWithValue("@userid", Int64.Parse(Session["StuSmallId"].ToString()));
                        con.Open();
                        cmd.ExecuteNonQuery();
                        //try
                        //{
                            
                        //}
                        //catch (Exception)
                        //{

                        //    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Failed to Save Comment" + "');", true);
                        //}
                    
                        con.Close();
                        con.Dispose();
                        GetComments(Id, 0);
                        try
                        {
                            l.SendDBCommentEmail(txtcomment.Text, type, Id);
                        }
                        catch (Exception ex)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Failed to send notification" + "');", true);
                            ExceptionLog log = new ExceptionLog();
                           // log.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                        }                      
                        SendCommentBtn.Visible = true;                                           
                        txtcomment.Text = "";


                    }
                }
            }
        }
        catch (Exception ex)
        {
           // l.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        }


    }
    [WebMethod]
    public static void SetSessions(string Deatail , string Approval)
    {

        Page objPage = new Page();
        objPage.Session["DetailId"] = Deatail;
        objPage.Session["FormApprovalId"] = Approval;

    }

   
}
