<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequestReport.aspx.cs" Inherits="test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">  
</asp:ScriptManager>  
    <div>
    
    </div>
          <div  style="width:100%;position:relative" >
                        
                            <rsweb:ReportViewer ID="ReportViewer1"  Visible="false" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="600px" Width="90%">
                                <LocalReport ReportPath="Reports\MilitaryRpt.rdlc">
           
                                </LocalReport>
                            </rsweb:ReportViewer>
                            
          </div>
    </form>
</body>
</html>
