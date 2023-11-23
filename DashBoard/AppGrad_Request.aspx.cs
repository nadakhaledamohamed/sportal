using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DashBoard_AppGrad_Request : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adr;
    DataTable dt;
    protected static string DashBoard = ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        Year();
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
  
    protected void FileUploadCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {

        if (photo_FileUploadControl.HasFile)
        {
            if (photo_FileUploadControl.PostedFile.ContentLength / 1048576 >= 2)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
   
        

    protected void Year()
    {

        for (int i = 2010; i <= 2025; i++)
        {
            yeartxt.Items.Add(i.ToString());


        }
        yeartxt.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
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
        Page.Validate("photocomp");
       
            if (Page.IsValid)
        {
            
                //Continue with your logic

                List<String> FilesName = new List<string>();
                string FName = "";
                if (photo_FileUploadControl.HasFiles & photo_FileUploadControl.PostedFile.ContentLength / 1048576 < 2)
                {
                    try
                    {
                        foreach (HttpPostedFile uploadedFile in photo_FileUploadControl.PostedFiles)
                        {

                            string test = Path.GetExtension(uploadedFile.FileName);
                            FName = Session["Requestid"].ToString();
                            //FName = Session["StuSmallId"].ToString() ;
                            FilesName.Add(FName);
                            uploadedFile.SaveAs(System.IO.Path.Combine(Server.MapPath("~/DashBoard/Photo_Path/"), FName));

                        }
                        StatusLabel.Text = "Upload status: File uploaded!";
                        Session["FileUpload"] = FilesName;
                    }
                    catch (Exception ex)
                    {
                        StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                    }
                }
            }
            using (con = new SqlConnection(DashBoard))
            {
                using (cmd = new SqlCommand("Add_DB_Request", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@smallId", Session["StuSmallId"]);
                    cmd.Parameters.AddWithValue("@FormId", 14);
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
                using (cmd = new SqlCommand("Add_Grad_Request", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Requestid", Requestid);
                    cmd.Parameters.AddWithValue("@Name_English", ENametxt.Text);
                    cmd.Parameters.AddWithValue("@Name_Arabic", ANametxt.Text);
                    cmd.Parameters.AddWithValue("@Personal_Email", PEmailtxt.Text);
                    cmd.Parameters.AddWithValue("@Date_of_Birth", BDate.Text);
                    cmd.Parameters.AddWithValue("@Gender", GenderDropDown.Text);
                    cmd.Parameters.AddWithValue("@National_ID", NationalIdtxt.Text);
                    cmd.Parameters.AddWithValue("@Nationality", Nationalitytxt.Text);
                    cmd.Parameters.AddWithValue("@Major", Majordropdown.Text);
                    cmd.Parameters.AddWithValue("@High_School_Name", HSchoolNametxt.Text);
                    cmd.Parameters.AddWithValue("@Previous_Degree", HschoolDropDown.Text);
                        
                    cmd.Parameters.AddWithValue("@Year_Awarded", yeartxt.Text);
                    cmd.Parameters.AddWithValue("@Name_Ceremonial", CerNametxt.Text);
                    cmd.Parameters.AddWithValue("@Name_Calling", CallingNametxt.Text);
                    cmd.Parameters.AddWithValue("@Emrg_Contact", Emrgtxt.Text);
                    cmd.Parameters.AddWithValue("@Personal_Photo", photo_FileUploadControl.FileName);
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                
                Response.Redirect("Student_DB_Requests.aspx");
                }
            }
        }
    }


//DateTime.Parse(Date).ToString("yyyy-MM-dd)