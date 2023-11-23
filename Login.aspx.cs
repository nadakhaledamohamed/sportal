using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private string GetIpAddress()
    {
        var context = HttpContext.Current;
        var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (string.IsNullOrEmpty(ipAddress)) return context.Request.ServerVariables["REMOTE_ADDR"];
        var addresses = ipAddress.Split(',');
        return addresses.Length != 0 ? addresses[0] : context.Request.ServerVariables["REMOTE_ADDR"];
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //if (txtUsername.Value == "161355")
        //{
        //    Session["StuSmallId"] = "161355";
        //    Session["StuDisplayName"] = "Abdel Rahman Yousry Afify	";
        //    Session["StuUserIP"] = GetIpAddress();
        //    Session["StuschoolId"] = "1";
        //    Session["StubatchId"] = "1";
        //    Session["StuId"] = "161355";
        //    Session["EmploNum"] = "161355";

        //    lblMessage.Visible = false;
        //    Response.Redirect("~/Welcome.aspx");
        //}

        var userIp = GetIpAddress();
        var priv = new Privilages();
        var smallId = 0;
        var displayanem = "";
        var userid = 0;
        var schoolId = 0;
        var batchId = 0;
        var EmpId = 0;


        /* 1- check username and password in AD
         * 2- if not authenticated display fail, otherwise
         * 3- check in student data
         * 4- if student, fill student sessions and goto student default page, otherwise
         * 5- check in application authentication 
         * 6- if has access to use application open staff default page, otherwise display not allow to use application. 
         */

        //1
        bool isAuthenticate = priv.IsAuthenticated(txtUsername.Value.ToLower().Replace("@ngu.edu.eg", ""), txtPassword.Value);
        if (isAuthenticate)
        {
            // Session["StuSmallId"] = 2102639;
            // Response.Redirect("~/Welcome.aspx");
            //3
            if (priv.GetStudentData(txtUsername.Value.ToLower().Replace("@ngu.edu.eg", ""), ref displayanem, ref smallId, ref userid, ref schoolId, ref batchId, ref EmpId))
            {
                
              //  Session["StuSmallId"] = 212769;
                 Session["StuSmallId"] = smallId;
              
                Session["StuschoolId"] = 4;
                Session["StuDisplayName"] = displayanem;
                Session["StuUserIP"] = userIp;
               // Session["StuschoolId"] = schoolId;
                Session["StubatchId"] = batchId;
                Session["StuId"] = userid;
              // Session["StuId"] = 182540;
                Session["EmploNum"] = EmpId;

                lblMessage.Visible = false;
                Response.Redirect("~/Welcome.aspx");
                //if (txtUsername.Value.ToLower().Replace("@ngu.edu.eg", "").Trim() == txtPassword.Value.ToLower().Trim())
                //{
                //    Response.Write("<script language='javascript'>window.alert('You must change your default password');window.location='Pass/Default.aspx';</script>");
                //}
                //else
                //{
                //    Response.Redirect("~/Welcome.aspx");
                //}
            }            

        }
        else
        {
            //2
            lblMessage.Visible = true;
            lblMessage.Text = "invalid username or password";
            priv.AddStudentLogToDatabase(0, 1, userIp, txtUsername.Value);
        }



        ///////////////////////////////
        ///////////////////////////////
        var islogined = priv.IsAuthenticatedStudent(txtUsername.Value.ToLower().Replace("@ngu.edu.eg", ""), txtPassword.Value, ref displayanem, ref smallId, ref userid, ref schoolId, ref batchId, ref EmpId);
        //  islogined = true;
        if (islogined)
        {
            Session["StuSmallId"] = smallId;        
            Session["StuDisplayName"] = displayanem;
            Session["StuUserIP"] = userIp;
            Session["StuschoolId"] = schoolId;
            Session["StubatchId"] = batchId;
            Session["StuId"] = userid;
            Session["EmploNum"] = EmpId;

            lblMessage.Visible = false;

            if (txtUsername.Value.ToLower().Replace("@ngu.edu.eg", "").Trim() == txtPassword.Value.ToLower().Trim())
            {
                Response.Write("<script language='javascript'>window.alert('You must change your default password');window.location='Pass/Default.aspx';</script>");
                //Response.Write("~/Pass/Default.aspx");

            }
            else
            {
                Response.Redirect("~/Welcome.aspx");
            }
        }
        else
        {
            lblMessage.Visible = true;
            priv.AddStudentLogToDatabase(0, 1, userIp, txtUsername.Value);
        }
    }
}