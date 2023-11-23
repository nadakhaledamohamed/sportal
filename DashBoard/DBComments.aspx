<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBComments.aspx.cs" Inherits="_DBComment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <title></title>
 <style type="text/css">
.hrDiv {
	width:50%;
    text-align:left;
    margin-left:0
	
}
.containerDiv {
	 width:80%;
     height:80%;   
    border-color:lightgrey;
     padding-left: 20px;
      padding-top: 20px;
      padding-bottom: 20px;
      padding-right: 20px;
      position:relative;
}
.MsgDiv {
	width:80%;
	border:solid;
    border-color:lightgrey;
     padding-left: 20px;
      padding-top: 20px;
      padding-bottom: 20px;
      padding-right: 20px;
}


</style>

</head>
<body>
    <form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">  
</asp:ScriptManager>  
    <div>
    
    </div>
          <div  style="width:80%;position:relative" >
                        
              <div id="HistoryComment" runat="server" class="containerDiv">                     
              </div>  
               <div id="newComment" runat="server" class="MsgDiv">   
              <asp:TextBox ID="txtcomment"  runat="server" MaxLength="500" Width="100%" Height="50px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtcomment"
                            ErrorMessage="*" InitialValue=""
                            Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
                   <p></p>
               <asp:Button CssClass="btn btn-primary" runat="server" ID="SendCommentBtn" Text="Send Comment" OnClick="SendCommentBtn_Click" ValidationGroup="NewCompany" CausesValidation="true"/>
              </div>         
          </div>
    </form>
</body>
</html>
