using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Signout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["UserId"] = "";
        Session["DisplayName"] = "";
        Session["UserIp"] = "";
        Response.Redirect("~/Login.aspx");
    }
}