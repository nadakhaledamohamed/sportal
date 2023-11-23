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

public partial class ReAdmission_Request : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adr;
    DataTable dt;
    protected static string DashBoard = ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            using (con = new SqlConnection(DashBoard))
            {
                using (cmd = new SqlCommand("GetCurrentYearSemester", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                    if (dt.Rows.Count != 0)
                    {
                        lblys.InnerText = dt.Rows[0]["cYear"].ToString() + " / " + dt.Rows[0]["cTerm"].ToString();
                        hfyear.Value = dt.Rows[0]["cYear"].ToString();
                        hfsem.Value = dt.Rows[0]["cTerm"].ToString();
                    }


                }
            }
        }

    }

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        TextBox t = studentData1.FindControl("txtMobile") as TextBox;
        string mobile = t.Text;
        TextBox txtEmail = studentData1.FindControl("txtMail") as TextBox;
        HiddenField hfschool = studentData1.FindControl("hfschool") as HiddenField;
        HiddenField hflevel = studentData1.FindControl("hflevel") as HiddenField;
        string Email = txtEmail.Text;
        long Requestid = 0;

        if (Page.IsValid)
        {
            using (con = new SqlConnection(DashBoard))
            {
                using (cmd = new SqlCommand("Add_DB_Request", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@smallId", Session["StuSmallId"]);
                    cmd.Parameters.AddWithValue("@FormId", 2);
                    cmd.Parameters.AddWithValue("@Year", hfyear.Value);
                    cmd.Parameters.AddWithValue("@Semester", hfsem.Value);
                    cmd.Parameters.AddWithValue("@MobileNo", mobile);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@SchoolId", hfschool.Value);
                    cmd.Parameters.AddWithValue("@StudentYear", hflevel.Value);
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                    if (dt.Rows.Count != 0)
                    {
                        Requestid = long.Parse(dt.Rows[0]["Id"].ToString());

                    }
                }
            }
            using (con = new SqlConnection(DashBoard))
            {
                using (cmd = new SqlCommand("Add_ReAdmission_Request", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Requestid", Requestid);
                    cmd.Parameters.AddWithValue("@Student_Petition", txtReason.Text);

                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                    Response.Redirect("Student_DB_Requests.aspx");
                }
            }
        }
    }
}