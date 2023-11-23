<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NGU Student Portal</title>
    <link href="css/SurveyLogin.css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
</head>
<body>
    <div class="login-page">
        <div class="form">
            <div style="text-align: center">
                
                <asp:Image ID="Image1" runat="server" ImageUrl="~/img/logo.jpg" Height="50%" Width="50%" />
            </div>
              <div style="text-align: center">
            <form id="form1" runat="server">
                <input type="text" placeholder="username" runat="server" id="txtUsername" />
                <input type="password" placeholder="password" runat="server" id="txtPassword"/>
                <asp:Button ID="btnLogin" runat="server" Text="login" CssClass="btn" OnClick="btnLogin_Click" />
                <p class="message">
                    <asp:Label ID="lblMessage" runat="server" Visible="false" Text="Invalid User Name Or Password Or Not Allowed To Use This Application"></asp:Label>
                </p>
            </form>
                  </div>
        </div>
    </div>
</body>
</html>
