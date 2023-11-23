<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Theme="Default" StylesheetTheme="Default" CodeFile="Student_DB_Requests.aspx.cs" Inherits="St_DB_Requests" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>  
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" />

    <style type="text/css">  
        .Background  
        {  
            background-color: Black;  
            filter: alpha(opacity=90);  
            opacity: 0.8;  
        }  
        .Popup  
        {  
            background-color: #FFFFFF;  
            border-width: 3px;  
            border-style: solid;  
            border-color: black;  
            padding-top: 10px;  
            padding-left: 10px;  
            width: 50%;  
            height: 80%;  
        }  
        .lbl  
        {  
            font-size:16px;  
            font-style:italic;  
            font-weight:bold;  
        }  
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" Visible="true">
<%--<asp:ScriptManager EnablePageMethods="true" ID="ScriptManager" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="true"></asp:ScriptManager>--%>
<asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none" Width="100%">  
    <iframe style=" width: 100%; height: 100%;" id="irm1" src="about:blank" runat="server"></iframe>  
   <br/>  
    <asp:Button ID="Button2" runat="server" Text="Close"   OnClientClick="UpdateComment()"/>  
</asp:Panel>
    <div id="RequestDiv" runat="server" visible="false">
         <div class="row">
                        <div class="col-md-12">
                            <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-left: 5px;"
                                ID="NewType" OnClick="NewType_Click" Text="New"></asp:Button>
                        </div>
                    </div>
        <div class="box-content"  style="overflow-x: scroll; overflow-y: scroll;">
            <asp:GridView runat="server" ID="gvForms" AutoGenerateColumns="false"
                CssClass="table table-bordered table- striped table-hover table-heading table-datatable" Width="100%"
                OnRowDataBound="gvForms_RowDataBound"  OnRowCommand="gvTypes_RowCommand">
                <Columns>
            
                    <asp:BoundField HeaderText="Request Name" DataField="formName" />
                    
                    <asp:BoundField HeaderText="Created Date" DataField="CreatedDate"  DataFormatString="{0:dd/MM/yyyy hh:mm:ss}"/>
                    <asp:BoundField HeaderText="Status" DataField="StatusName" />
                    <asp:BoundField HeaderText="Hold By" DataField="HoldBy" />
                                    <asp:TemplateField HeaderText="Comments">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="comment" CommandArgument='<%# Eval("RequestId") %>' runat="server" ID="btnComment" OnClientClick="clickbtn2(this);" data-Detailid='<%# Eval("RequestId") %>' data-faid='0'> <i class="fas fa-comment-alt fa-lg"  title="Comments"></i>
                            </asp:LinkButton>
                             <asp:HiddenField ID="hfform_approval" runat="server" Value='0' />                         
                            <asp:Label ID="txtNotification" runat="server" ForeColor="Green"></asp:Label>
                            <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panl1" TargetControlID="btnComment"
                                CancelControlID="Button2" BackgroundCssClass="Background">
                            </cc1:ModalPopupExtender>
                            
                            <%--                          <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnprint"  
    CancelControlID="Button2" BackgroundCssClass="Background">  
</cc1:ModalPopupExtender>  --%>
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <script>
        function clickbtn(ctrl)
        {
            debugger;
            var Detail = ctrl.getAttribute("data-Detailid");
            debugger;
          //var src = "http://localhost:64132//RequestReport.aspx?detid=" + Detail
            var src = location.href.substring(0, location.href.lastIndexOf("/") + 1)+"RequestReport.aspx?detid=" + Detail
            $('iframe').attr('src', src);
            //$('#irm1').attr('src', src);           
           // $('#irm1').attr('src', src);
        }
        function clickbtn2(ctrl) {
           
            //debugger;

            debugger;
            var Det = { Deatail: ctrl.getAttribute("data-Detailid"), Approval: ctrl.getAttribute("data-faid") };
            $.ajax({
                type: "POST",
                url: "DBComments.aspx/SetSessions",
                data: JSON.stringify(Det),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var src = location.href.substring(0, location.href.lastIndexOf("/") + 1) + "DBComments.aspx"
                    $('iframe').attr('src', src);
                }
            });
                   
        }
        function clickbtn3(ctrl) {

            debugger;
            // alert("hellooo")
            var Det = { Deatail: ctrl.getAttribute("data-Detailid") };
            $.ajax({
                type: "POST",
                url: "DBApprovalList.aspx/SetSessions",
                data: JSON.stringify(Det),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var src = location.href.substring(0, location.href.lastIndexOf("/") + 1) + "DBApprovalList.aspx"
                    $('iframe').attr('src', src);
                }
            });
        }
        function UpdateComment() {
            location.reload();


        }
    </script>

</asp:Content>

