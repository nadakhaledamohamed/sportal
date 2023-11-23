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

public partial class DashBoard_studentData : System.Web.UI.UserControl
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adr;
    DataTable dt;
    protected static string DashBoard = ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

        FillstudentData();

    }
    public void FillstudentData()
    {

        if (Session["StuSmallId"] != null)
        {
            int studentid = (int)Session["StuSmallId"];
            using (con = new SqlConnection(DashBoard))
            {
                using (cmd = new SqlCommand("GetStudentData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SmallId", studentid);
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        txtFullName.Text = dt.Rows[0][1].ToString();
                        txtStudentID.Text = dt.Rows[0][0].ToString();                    
                        txtSchoolName.Text = dt.Rows[0][8].ToString();
                        hfschool.Value = dt.Rows[0][7].ToString();
                        hflevel.Value= dt.Rows[0][10].ToString();
                        txtMail.Text = dt.Rows[0][9].ToString() + "@ngu.edu.eg";
                        txtYear.Text = dt.Rows[0][10].ToString();
                    }
                }
            }


        }


    }   
}