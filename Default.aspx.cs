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

public partial class _Default : System.Web.UI.Page
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

        

        }

    }
 
}