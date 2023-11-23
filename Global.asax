<%@ Application Language="C#" %>

<script RunAt="server">


    void Application_BeginRequest(object sender, EventArgs e)
    {
        //if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http:"))
        //{
        //    Response.Redirect(HttpContext.Current.Request.Url.ToString().ToLower().Replace("http:", "https:"));
        //}
    }

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        Session["StuSmallId"] = "";
        Session["StuDisplayName"] = "";
        Session["StuUserIP"] = "";
        Session["StuschoolId"] = "";
        Session["StubatchId"] = "";
        Session["StuId"] = "";
        Session["Requeststatus"] = null;
        Session["SelectedForm"] = null;
        Session["FileUpload"] = null;
        Session["EmployeeId"] = "";
        Session["EmployeeName"] = "";
        Session["DetailId"] = null;
        Console.WriteLine("hi");
    }

    void Session_End(object sender, EventArgs e)
    {

    }
</script>
