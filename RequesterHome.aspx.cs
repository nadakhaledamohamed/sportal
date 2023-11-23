using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;

public partial class RequesterHome : System.Web.UI.Page
{
    protected static string FormRequestConStr = ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString;
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adr;
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // this.test();
        }
    }
    private void test()
    {
        //for (int ix = 20; ix <= 20; ix++)
        //{
        //    dt = new DataTable();
        //    using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
        //    {
        //        using (cmd = new SqlCommand("QR.AddQRData_TempTest2", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@DetID", ix.ToString());
        //            adr = new SqlDataAdapter(cmd);

        //            adr.Fill(dt);

        //        }
        //    }
        //    if (dt.Rows.Count > 0)
        //    {
        //        //  Create QR Code for Graduated

        //        string code = dt.Rows[0]["PageURL"].ToString() + "?Id=" + dt.Rows[0]["GUIDKey"].ToString();
        //        QRCodeEncoder encoder = new QRCodeEncoder();
        //        encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
        //        encoder.QRCodeScale = 5;

        //        Bitmap img = encoder.Encode(code);
        //        string FName = dt.Rows[0]["GUIDKey"].ToString() + ".jpg";
        //        string path = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), FName);
        //        System.Drawing.Image logo = System.Drawing.Image.FromFile(System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), "Untitledz.png"));

        //        Graphics g = Graphics.FromImage(img);
        //        int left = (img.Width / 2) - (logo.Width / 2);
        //        int top = (img.Height / 2) - (logo.Height / 2);
        //        g.DrawImage(logo, new Point(left, top));
        //        img.Save(path, ImageFormat.Jpeg);

        //        //Create QR code for visitors_1

        //        string code_1 = dt.Rows[0]["PageURL"].ToString() + "?Id=" + dt.Rows[0]["VisitorId_1"].ToString();
        //        QRCodeEncoder encoder_1 = new QRCodeEncoder();
        //        encoder_1.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
        //        encoder_1.QRCodeScale = 5;

        //        Bitmap img_1 = encoder_1.Encode(code_1);
        //        string FName_1 = dt.Rows[0]["VisitorId_1"].ToString() + ".jpg";
        //        string path_1 = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), FName_1);
        //        System.Drawing.Image logo_1 = System.Drawing.Image.FromFile(System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), "logo_A_2.jpg"));

        //        Graphics g_1 = Graphics.FromImage(img_1);
        //        int left_1 = (img_1.Width / 2) - (logo_1.Width / 2);
        //        int top_1 = (img_1.Height / 2) - (logo_1.Height / 2);
        //        g_1.DrawImage(logo_1, new Point(left_1, top_1));
        //        img_1.Save(path_1, ImageFormat.Jpeg);

        //        //Create QR code for visitors_2

        //        string code_2 = dt.Rows[0]["PageURL"].ToString() + "?Id=" + dt.Rows[0]["VisitorId_2"].ToString();
        //        QRCodeEncoder encoder_2 = new QRCodeEncoder();
        //        encoder_2.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
        //        encoder_2.QRCodeScale = 5;

        //        Bitmap img_2 = encoder_2.Encode(code_2);
        //        string FName_2 = dt.Rows[0]["VisitorId_2"].ToString() + ".jpg";
        //        string path_2 = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), FName_2);
        //        System.Drawing.Image logo_2 = System.Drawing.Image.FromFile(System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), "logo_B_2.jpg"));

        //        Graphics g_2 = Graphics.FromImage(img_2);
        //        int left_2 = (img_2.Width / 2) - (logo_2.Width / 2);
        //        int top_2 = (img_2.Height / 2) - (logo_2.Height / 2);
        //        g_2.DrawImage(logo_2, new Point(left_2, top_2));
        //        img_2.Save(path_2, ImageFormat.Jpeg);

        //        //Create QR code for visitors_3

        //        string code_3 = dt.Rows[0]["PageURL"].ToString() + "?Id=" + dt.Rows[0]["VisitorId_3"].ToString();
        //        QRCodeEncoder encoder_3 = new QRCodeEncoder();
        //        encoder_3.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
        //        encoder_3.QRCodeScale = 5;

        //        Bitmap img_3 = encoder_3.Encode(code_3);
        //        string FName_3 = dt.Rows[0]["VisitorId_3"].ToString() + ".jpg";
        //        string path_3 = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), FName_3);
        //        System.Drawing.Image logo_3 = System.Drawing.Image.FromFile(System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), "logo_C_2.jpg"));

        //        Graphics g_3 = Graphics.FromImage(img_3);
        //        int left_3 = (img_3.Width / 2) - (logo_3.Width / 2);
        //        int top_3 = (img_3.Height / 2) - (logo_3.Height / 2);
        //        g_3.DrawImage(logo_3, new Point(left_3, top_3));
        //        img_3.Save(path_3, ImageFormat.Jpeg);

        //        ExceptionLog l = new ExceptionLog();
        //        l.sendQREmail(dt.Rows[0]["Email"].ToString(), path, dt.Rows[0]["StudentName"].ToString(), dt.Rows[0]["StudentId"].ToString(), dt.Rows[0]["SchoolName"].ToString(), path_1, path_2, path_3,);


        //        img.Dispose();
        //        img_1.Dispose();
        //        img_2.Dispose();
        //        img_3.Dispose();

        //        g.Dispose();
        //        g_1.Dispose();
        //        g_2.Dispose();
        //        g_3.Dispose();

        //        GC.Collect();
        //    }
        //}



    }
    private void CreateQRBatch()
    {
        dt = new DataTable();
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
        {
            using (cmd = new SqlCommand("QR.GetQRBatch", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                adr = new SqlDataAdapter(cmd);
                adr.Fill(dt);
            }
        }
        if (dt.Rows.Count > 0)
        {

            foreach (DataRow r in dt.Rows)
            {
                string code = r["PageURL"].ToString() + "?Id=" + r["GUIDKey"].ToString();
                QRCodeEncoder encoder = new QRCodeEncoder();
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                encoder.QRCodeScale = 5;

                Bitmap img = encoder.Encode(code);
                string FName = r["GUIDKey"].ToString() + ".jpg";
                string path = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), FName);
                System.Drawing.Image logo = System.Drawing.Image.FromFile(System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), "Untitledz.png"));

                Graphics g = Graphics.FromImage(img);
                int left = (img.Width / 2) - (logo.Width / 2);
                int top = (img.Height / 2) - (logo.Height / 2);
                g.DrawImage(logo, new Point(left, top));
                img.Save(path, ImageFormat.Jpeg);

                //Create QR code for visitors_1

                string code_1 = r["PageURL"].ToString() + "?Id=" + r["VisitorId_1"].ToString();
                QRCodeEncoder encoder_1 = new QRCodeEncoder();
                encoder_1.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                encoder_1.QRCodeScale = 5;

                Bitmap img_1 = encoder_1.Encode(code_1);
                string FName_1 = r["VisitorId_1"].ToString() + ".jpg";
                string path_1 = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), FName_1);
                System.Drawing.Image logo_1 = System.Drawing.Image.FromFile(System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), "logo_A_2.jpg"));

                Graphics g_1 = Graphics.FromImage(img_1);
                int left_1 = (img_1.Width / 2) - (logo_1.Width / 2);
                int top_1 = (img_1.Height / 2) - (logo_1.Height / 2);
                g_1.DrawImage(logo_1, new Point(left_1, top_1));
                img_1.Save(path_1, ImageFormat.Jpeg);

                //Create QR code for visitors_2

                string code_2 = r["PageURL"].ToString() + "?Id=" + r["VisitorId_2"].ToString();
                QRCodeEncoder encoder_2 = new QRCodeEncoder();
                encoder_2.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                encoder_2.QRCodeScale = 5;

                Bitmap img_2 = encoder_2.Encode(code_2);
                string FName_2 = r["VisitorId_2"].ToString() + ".jpg";
                string path_2 = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), FName_2);
                System.Drawing.Image logo_2 = System.Drawing.Image.FromFile(System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), "logo_B_2.jpg"));

                Graphics g_2 = Graphics.FromImage(img_2);
                int left_2 = (img_2.Width / 2) - (logo_2.Width / 2);
                int top_2 = (img_2.Height / 2) - (logo_2.Height / 2);
                g_2.DrawImage(logo_2, new Point(left_2, top_2));
                img_2.Save(path_2, ImageFormat.Jpeg);

                //  Create QR code for visitors_3

                string code_3 = r["PageURL"].ToString() + "?Id=" + r["VisitorId_3"].ToString();
                QRCodeEncoder encoder_3 = new QRCodeEncoder();
                encoder_3.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                encoder_3.QRCodeScale = 5;

                Bitmap img_3 = encoder_3.Encode(code_3);
                string FName_3 = r["VisitorId_3"].ToString() + ".jpg";
                string path_3 = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), FName_3);
                System.Drawing.Image logo_3 = System.Drawing.Image.FromFile(System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), "logo_C_2.jpg"));

                Graphics g_3 = Graphics.FromImage(img_3);
                int left_3 = (img_3.Width / 2) - (logo_3.Width / 2);
                int top_3 = (img_3.Height / 2) - (logo_3.Height / 2);
                g_3.DrawImage(logo_3, new Point(left_3, top_3));
                img_3.Save(path_3, ImageFormat.Jpeg);

                //Create QR code for visitors_4
                string code_4 = r["PageURL"].ToString() + "?Id=" + r["VisitorId_4"].ToString();
                QRCodeEncoder encoder_4 = new QRCodeEncoder();
                encoder_4.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                encoder_4.QRCodeScale = 5;

                Bitmap img_4 = encoder_4.Encode(code_4);
                string FName_4 = r["VisitorId_4"].ToString() + ".jpg";
                string path_4 = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), FName_4);
                System.Drawing.Image logo_4 = System.Drawing.Image.FromFile(System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), "logo_C_2.jpg"));

                Graphics g_4 = Graphics.FromImage(img_4);
                int left_4 = (img_4.Width / 2) - (logo_4.Width / 2);
                int top_4 = (img_4.Height / 2) - (logo_4.Height / 2);
                g_4.DrawImage(logo_4, new Point(left_4, top_4));
                img_4.Save(path_4, ImageFormat.Jpeg);
            }
            ExceptionLog l;
            foreach (DataRow r2 in dt.Rows)
            {

                try
                {
                    string FName = r2["GUIDKey"].ToString() + ".jpg";
                    string path = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), FName);
                    string FName_1 = r2["VisitorId_1"].ToString() + ".jpg";
                    string path_1 = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), FName_1);
                    string FName_2 = r2["VisitorId_2"].ToString() + ".jpg";
                    string path_2 = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), FName_2);
                    string FName_3 = r2["VisitorId_3"].ToString() + ".jpg";
                    string path_3 = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), FName_3);
                    string FName_4 = r2["VisitorId_4"].ToString() + ".jpg";
                    string path_4 = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/QRImages/"), FName_4);
                    l = new ExceptionLog();

                    l.sendQREmail(dt.Rows[0]["Email"].ToString(), path, dt.Rows[0]["StudentName"].ToString(), dt.Rows[0]["StudentId"].ToString(), dt.Rows[0]["SchoolName"].ToString(), path_1, path_2, path_3,path_4);
                    using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                    {
                        using (cmd = new SqlCommand("QR.UpdateBatchStatus", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StudentId", int.Parse(r2["StudentId"].ToString()));
                            cmd.Parameters.AddWithValue("@IsSucess", true);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            con.Dispose();
                        }
                    }

                }
                catch (Exception)
                {

                    using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                    {
                        using (cmd = new SqlCommand("QR.UpdateBatchStatus", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StudentId", int.Parse(r2["StudentId"].ToString()));
                            cmd.Parameters.AddWithValue("@IsSucess", false);
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

}
