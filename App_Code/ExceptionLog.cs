using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for ExceptionLog
/// </summary>
public class SessionData
{
    public long SessionId;
    public string CurentPW;

}
public class AttendanceParam
{
    public DateTime AttDate;
    public Guid RecordID;
}
public static class Attendance
{
    public static List<SessionData> Sessions = new List<SessionData>();

}

public class ExceptionLog
{
    public static readonly Dictionary<int, string> TicketData
    = new Dictionary<int, string>
{
    { 1, "Error One" },
    { 2, "Error Two" },
    { 3, "Error Two" },
    { 4, "Error Two" }
};
    protected static int Graduation_TicketsNo = 4;
    protected static string FormRequestConStr = ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString;
    protected static string FinanceMail = "finance@ngu.edu.eg";
    protected static string RegistrarEmail = "Onlinerequest@ngu.edu.eg";
    public static string credentialUserName = "Onlinerequest@ngu.edu.eg";
    public static string credentialPassword = "96nR7TnW";
    public static string onlineFormApplication = "Onlinerequest@ngu.edu.eg";
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adr;
    DataTable dt;
    public ExceptionLog()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void SendEmail(MailMessage EMail, string RefID = "", string DBName = "FormRequest")
    {
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.office365.com";

        smtp.Port = 587;

        smtp.UseDefaultCredentials = false;

        smtp.EnableSsl = true;
        smtp.Credentials = new System.Net.NetworkCredential(credentialUserName, credentialPassword);
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        try
        {
            smtp.Send(EMail);

        }
        catch (Exception ex)
        {

            try
            {
                smtp.Send(EMail);
            }
            catch (Exception ex1)
            {

                try
                {
                    smtp.Send(EMail);
                }
                catch (Exception ex2)
                {

                    using (con = new SqlConnection(FormRequestConStr))
                    {
                        using (cmd = new SqlCommand("AddPendingMail", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (!string.IsNullOrEmpty(RefID))
                            {
                                cmd.Parameters.AddWithValue("@RefID", RefID);
                            }
                            // cmd.Parameters.AddWithValue("@FromAccount", EMail.From);
                            cmd.Parameters.AddWithValue("@ToAccount", EMail.To);
                            cmd.Parameters.AddWithValue("@Subject", EMail.Subject);
                            cmd.Parameters.AddWithValue("@Body", EMail.Body);
                            cmd.Parameters.AddWithValue("@DBName", DBName);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            con.Dispose();
                        }
                    }
                }
            }
        }

    }
    public void SendDBCommentEmail(string Comment, int type, Int64 RequestId)
    {
        string FormName = "";
        string studentName = "";
        string StudentEmail = "";
        string Subject = "New Comment has been added to Dashboard form application";
        string body = "";
        string CommentBy = "";
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress(onlineFormApplication);
        mail.IsBodyHtml = true;
        dt = new DataTable();
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["dashboard"].ConnectionString))
        {
            using (cmd = new SqlCommand("GeDBtCommentNotificationData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestId", RequestId);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.CommandType = CommandType.StoredProcedure;
                adr = new SqlDataAdapter(cmd);
                adr.Fill(dt);
            }
        }
        if (dt.Rows.Count > 0)
        {
            FormName = dt.Rows[0]["FormName"].ToString();
            studentName = dt.Rows[0]["StudentName"].ToString();
            StudentEmail = dt.Rows[0]["StudentEmail"].ToString();
            List<string> Emails = new List<string>();
            switch (type)
            {
                case 0:
                    CommentBy = " Student " ;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        mail.To.Add(dt.Rows[i]["Email"].ToString());
                    }

                    break;
                default:
                    CommentBy = dt.Rows[0]["CommentBy"].ToString();
                    mail.To.Add(StudentEmail);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        mail.To.Add(dt.Rows[i]["Email"].ToString());

                    }
                    break;
            }
            body = @"<html>
                                        <body>
                                        <p>Dear,</p>
                                        <p>Kindly , New online Comment has been sent by " + CommentBy + @" For  " + FormName + " Requested by student : " + dt.Rows[0]["StudentId"].ToString() + " - " + studentName + " as Follow : </p> " + Comment + @" 
                                        </body>
                                        </html>
                                       ";
            mail.Subject = Subject;

            mail.Body = body;
            SendEmail(mail, "R" + RequestId.ToString(), "DashBoard");


        }

    }
    //public void sendQREmail(string StudentEmail, string Path, string StudentName = null)
    //{
    //    sendQREmail3(StudentEmail, Path);
    //    return;
    //        MailMessage mail = new MailMessage();
    //        SmtpClient smtp = new SmtpClient();
    //        ExceptionLog l = new ExceptionLog();
    //        smtp.Host = "smtp.office365.com";

    //        smtp.Port = 587;

    //        smtp.UseDefaultCredentials = false;

    //        smtp.EnableSsl = true;
    //        smtp.Credentials = new System.Net.NetworkCredential(ExceptionLog.credentialUserName, ExceptionLog.credentialPassword);
    //        mail.To.Add(StudentEmail);
    //        mail.CC.Clear();
    //        mail.IsBodyHtml = true;
    //        mail.From = new MailAddress(ExceptionLog.onlineFormApplication);
    //        mail.Subject = "Graduation Ceremony QR Code";
          
    //    string html = "<p><h2><u>Welcome to NGU Graduation Ceremony</h2></u></p>";
    //    mail.Body = html;
    //    //mail.AlternateViews.Add(Mail_Body(Path));

    //    l.SendEmail(mail);
        
    //}
    public static AlternateView Mail_Body(string path,string stName,string StId,string school,string VisitorPath)
    {
        // string path = "C:/SendingMail/Welcome Email.jpg";
       LinkedResource Img = new LinkedResource( path, MediaTypeNames.Image.Jpeg);
        LinkedResource Img2 = new LinkedResource(VisitorPath, MediaTypeNames.Image.Jpeg);
        Img.ContentId = "MyImage";
        Img2.ContentId = "MyImage2";
        string str = @"  
            <table style='width:100%; position:absolute'>  
                  <tr>  
                    <td style='text-align: center;' >  
                      <h2>Welcome to NGU Graduation Ceremony</h2> 
                    </td>  
                    </tr>
                <tr>                
                <td style='text-align: center;'>  
     <p><span><strong>Graduated QR Code</strong></span></p>
                    <p>  <img src=cid:MyImage  id='img' alt='Graduated QR Code' />   </p>                        
                    </td>                                 
                </tr>
                 <tr>                
                <td style='text-align: center;'>  
                     </br>
                     </br>
                    </td>                                 
                </tr>
                <tr>                
                <td style='text-align: center;'>  
                      <p> <span><strong>Visitors QR Code</strong></span></p> 
                     <p> <img src=cid:MyImage2  id='img2' alt='Visitors QR Code'/> </p>  
                
                    </td>                                 
                </tr>
               
                 <tr>  
                    <td style='text-align: center;'>  
                    <br/>
                      <h3><u>Graduated Information</u></h3> 
                    </td>  
                </tr>
                  <tr>  
    <td style=style='text-align: center;'>
      <table style='width:80%;position:absolute'>
             <tr>
              <td style='width:25%'>
                    </td>
                    <td style='width:20%'>  
                     <lable ><h4><u>ID Number:</u></h4></lable>
                    </td>  
                     <td style='width:30%'>" + StId + @"
                    </td> 
                  <td style='width:25%'>
                    </td>   
           </tr>
      </table 
      </td>       
     </tr>
<tr>  
 <td style=style='text-align: center;'>
      <table style='width:80%;position:absolute'>
             <tr>   <td style='width:25%'>
                    </td>
                    <td style='width:20%'>  
                     <lable ><h4><u>Name:</u></h4></lable>
                    </td>  
                     <td style='width:30%'>" + stName + @"
                    </td> 
                    <td style='width:25%'>
                    </td> 
                </tr>
      </table 
      </td>       
     </tr>
<tr>  
<td style=style='text-align: center;'>
      <table style='width:80%;position:absolute'>
             <tr>
<td style='width:25%'>
                    </td>
                    <td style='width:20%'>  
                     <lable ><h4><u>Faculty:</u></h4></lable></td>  
                     <td tyle='width:30%'>" + school + @"
                    </td>
                    <td style='width:25%'>
                    </td>   
               </tr>
      </table 
      </td>       
     </tr>
</table>  
            ";
        AlternateView AV = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
        AV.LinkedResources.Add(Img);
        AV.LinkedResources.Add(Img2);
        return AV;
    }
    public void sendQREmail(string StudentEmail, string StudentPath, string StudentName ,string StId , string school,string VistorPath)
    {

        MailMessage mail = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        ExceptionLog l = new ExceptionLog();
        smtp.Host = "smtp.office365.com";

        smtp.Port = 587;

        smtp.UseDefaultCredentials = false;

        smtp.EnableSsl = true;
        smtp.Credentials = new System.Net.NetworkCredential(ExceptionLog.credentialUserName, ExceptionLog.credentialPassword);
        mail.To.Add(StudentEmail);
        mail.CC.Clear();
        mail.IsBodyHtml = true;
        mail.From = new MailAddress(ExceptionLog.onlineFormApplication);
        mail.Subject = "Graduation Ceremony QR Code";
        string html = "<p><h2><u>Welcome to NGU Graduation Ceremony</h2></u></p>";
        mail.Body = html;
       mail.AlternateViews.Add(Mail_Body(StudentPath, StudentName,StId,school,VistorPath));
        System.Net.Mail.Attachment  attachment = new System.Net.Mail.Attachment(StudentPath);
        attachment.Name = "Graduated QR.Jpg";
        System.Net.Mail.Attachment attachment2 = new System.Net.Mail.Attachment(VistorPath);
        attachment2.Name = "Visitors QR Code.Jpg";
        mail.Attachments.Add(attachment);
        mail.Attachments.Add(attachment2);
        l.SendEmail(mail);
    }
    //    public static AlternateView Mail_Body(string path, string stName, string StId, string school)
    //    {
    //        // string path = "C:/SendingMail/Welcome Email.jpg";
    //        LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
    //        Img.ContentId = "MyImage";
    //        string str = @"  
    //            <table style='width:100%; position:absolute'>  
    //                <tr>  
    //                    <td style='text-align: center;'>  
    //                      <img src=cid:MyImage  id='img' alt='Graduation Ceremony QR Code'/>   
    //                    </td>  
    //                </tr>
    //                 <tr>  
    //                    <td style='text-align: center;' >  
    //                      <h2>Welcome to NGU Graduation Ceremony</h2> 
    //                    </td>  
    //                </tr>
    //                 <tr>  
    //                    <td style='text-align: center;'>  
    //                    <br/>
    //                      <h3><u>Graduated Information</u></h3> 
    //                    </td>  
    //                </tr>
    //                  <tr>  
    //    <td style=style='text-align: center;'>
    //      <table style='width:80%;position:absolute'>
    //             <tr>
    //              <td style='width:25%'>
    //                    </td>
    //                    <td style='width:20%'>  
    //                     <lable ><h4><u>ID Number:</u></h4></lable>
    //                    </td>  
    //                     <td style='width:30%'>" + StId + @"
    //                    </td> 
    //                  <td style='width:25%'>
    //                    </td>   
    //           </tr>
    //      </table 
    //      </td>       
    //     </tr>
    //<tr>  
    // <td style=style='text-align: center;'>
    //      <table style='width:80%;position:absolute'>
    //             <tr>   <td style='width:25%'>
    //                    </td>
    //                    <td style='width:20%'>  
    //                     <lable ><h4><u>Name:</u></h4></lable>
    //                    </td>  
    //                     <td style='width:30%'>" + stName + @"
    //                    </td> 
    //                    <td style='width:25%'>
    //                    </td> 
    //                </tr>
    //      </table 
    //      </td>       
    //     </tr>
    //<tr>  
    //<td style=style='text-align: center;'>
    //      <table style='width:80%;position:absolute'>
    //             <tr>
    //<td style='width:25%'>
    //                    </td>
    //                    <td style='width:20%'>  
    //                     <lable ><h4><u>Faculty:</u></h4></lable></td>  
    //                     <td tyle='width:30%'>" + school + @"
    //                    </td>
    //                    <td style='width:25%'>
    //                    </td>   
    //               </tr>
    //      </table 
    //      </td>       
    //     </tr>
    //</table>  
    //            ";
    //        AlternateView AV = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
    //        AV.LinkedResources.Add(Img);
    //        return AV;
    //    }
    public void sendQREmail(string StudentEmail, string StudentPath, string StudentName, string StId, string school, string VistorPath_1, string VistorPath_2, string VistorPath_3, string VistorPath_4)
    {

        MailMessage mail = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        ExceptionLog l = new ExceptionLog();
        smtp.Host = "smtp.office365.com";

        smtp.Port = 587;

        smtp.UseDefaultCredentials = false;

        smtp.EnableSsl = true;
        smtp.Credentials = new System.Net.NetworkCredential(ExceptionLog.credentialUserName, ExceptionLog.credentialPassword);
        mail.To.Add(StudentEmail);
        mail.CC.Clear();
        mail.IsBodyHtml = true;
        mail.From = new MailAddress(ExceptionLog.onlineFormApplication);
        mail.Subject = "Graduation Ceremony QR Code For Test";
        string html = "<p><h2><u>Welcome to NGU Graduation Ceremony test session</h2></u></p><br/>";
        mail.Body = html;
        mail.AlternateViews.Add(Mail_Body(StudentPath, StudentName, StId, school, VistorPath_1, VistorPath_2, VistorPath_3, VistorPath_4));
        System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(StudentPath);
        attachment.Name = "Graduated QR.Jpg";

        System.Net.Mail.Attachment attachment_1 = new System.Net.Mail.Attachment(VistorPath_1);
        attachment_1.Name = "Visitors (1) QR Code.Jpg";

        System.Net.Mail.Attachment attachment_2 = new System.Net.Mail.Attachment(VistorPath_2);
        attachment_2.Name = "Visitors (2) QR Code.Jpg";

        System.Net.Mail.Attachment attachment_3 = new System.Net.Mail.Attachment(VistorPath_3);
        attachment_3.Name = "Visitors (3) QR Code.Jpg";

        System.Net.Mail.Attachment attachment_4 = new System.Net.Mail.Attachment(VistorPath_4);
        attachment_4.Name = "Visitors (4) QR Code.Jpg";


        mail.Attachments.Add(attachment);
        mail.Attachments.Add(attachment_1);
        mail.Attachments.Add(attachment_2);
        mail.Attachments.Add(attachment_3);
        mail.Attachments.Add(attachment_4);
        mail.Priority = MailPriority.High;
        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
        l.SendEmail(mail);
    }

    public static AlternateView Mail_Body(string path, string stName, string StId, string school, string VisitorPath_1, string VisitorPath_2, string VisitorPath_3, string VisitorPath_4)
    {
        // string path = "C:/SendingMail/Welcome Email.jpg";
        LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
        LinkedResource Img_1 = new LinkedResource(VisitorPath_1, MediaTypeNames.Image.Jpeg);
        LinkedResource Img_2 = new LinkedResource(VisitorPath_2, MediaTypeNames.Image.Jpeg);
        LinkedResource Img_3 = new LinkedResource(VisitorPath_3, MediaTypeNames.Image.Jpeg);
        LinkedResource Img_4 = new LinkedResource(VisitorPath_4, MediaTypeNames.Image.Jpeg);
        Img.ContentId = "MyImage";
        Img_1.ContentId = "MyImage_1";
        Img_2.ContentId = "MyImage_2";
        Img_3.ContentId = "MyImage_3";
       Img_4.ContentId = "MyImage_4";
        string str = @"  
            <table style='width:100%; position:absolute'>  
                  <tr>  
                    <td style='text-align: center;' >  
                      <h2>Welcome to NGU Graduation Ceremony</h2> 
                    </td>  
                    </tr>
                <tr>                
                <td style='text-align: center;'>  
     <p><span><strong>Graduated QR Code</strong></span></p>
                    <p>  <img src=cid:MyImage  id='img' alt='Graduated QR Code' />   </p>                        
                    </td>                                 
                </tr>
                 <tr>                
                <td style='text-align: center;'>  
                     </br>
                     </br>
                    </td>                                 
                </tr>
                <tr>                
                <td style='text-align: center;'>  
                      <p> <span><strong>Visitors (1) QR Code</strong></span></p> 
                     <p> <img src=cid:MyImage_1  id='img_1' alt='Visitors  (1) QR Code'/> </p>  
                
                    </td>                                 
                </tr>
               <tr>                
                <td style='text-align: center;'>  
                      <p> <span><strong>Visitors (2) QR Code</strong></span></p> 
                     <p> <img src=cid:MyImage_2  id='img_2' alt='Visitors  (2) QR Code'/> </p>  
                
                    </td>                                 
                </tr>
                <tr>                
                <td style='text-align: center;'>  
                      <p> <span><strong>Visitors (3) QR Code</strong></span></p> 
                     <p> <img src=cid:MyImage_3  id='img__3' alt='Visitors  (3) QR Code'/> </p>  
                
                    </td>                                 
                </tr>
                 <tr>                
                <td style='text-align: center;'>  
                      <p> <span><strong>Visitors (4) QR Code</strong></span></p> 
                     <p> <img src=cid:MyImage_4  id='img__4' alt='Visitors  (4) QR Code'/> </p>  
                
                    </td>                                 
                </tr>

                 <tr>  
                    <td style='text-align: center;'>  
                    <br/>
                      <h3><u>Graduated Information</u></h3> 
                    </td>  
                </tr>
                  <tr>  
    <td style=style='text-align: center;'>
      <table style='width:80%;position:absolute'>
             <tr>
              <td style='width:25%'>
                    </td>
                    <td style='width:20%'>  
                     <lable ><h4><u>ID Number:</u></h4></lable>
                    </td>  
                     <td style='width:30%'>" + StId + @"
                    </td> 
                  <td style='width:25%'>
                    </td>   
           </tr>
      </table 
      </td>       
     </tr>
<tr>  
 <td style=style='text-align: center;'>
      <table style='width:80%;position:absolute'>
             <tr>   <td style='width:25%'>
                    </td>
                    <td style='width:20%'>  
                     <lable ><h4><u>Name:</u></h4></lable>
                    </td>  
                     <td style='width:30%'>" + stName + @"
                    </td> 
                    <td style='width:25%'>
                    </td> 
                </tr>
      </table 
      </td>       
     </tr>
<tr>  
<td style=style='text-align: center;'>
      <table style='width:80%;position:absolute'>
             <tr>
<td style='width:25%'>
                    </td>
                    <td style='width:20%'>  
                     <lable ><h4><u>Faculty:</u></h4></lable></td>  
                     <td tyle='width:30%'>" + school + @"
                    </td>
                    <td style='width:25%'>
                    </td>   
               </tr>
      </table 
      </td>       
     </tr>
</table>  
            ";
        AlternateView AV = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
        AV.LinkedResources.Add(Img);
        AV.LinkedResources.Add(Img_1);
        AV.LinkedResources.Add(Img_2);
        AV.LinkedResources.Add(Img_3);
        AV.LinkedResources.Add(Img_4);
        return AV;
    }



}