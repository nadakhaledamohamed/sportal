<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Theme="Default" StylesheetTheme="Default" CodeFile="DB_Requests.aspx.cs" Inherits="DashBoardForm " MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" Visible="true">
  
    <div id="FormRequestDiv" runat="server" style="overflow-x: scroll; overflow-y: scroll;">
        <div class="row" runat="server">
            <section class="col col-lg-12">
                <label style="font-weight: bold">Available Requests</label>
                <hr />
              <asp:RadioButtonList ID="rdFormLst" runat="server"></asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdFormLst"
                ErrorMessage="*" InitialValue=""
                Text="Select Request" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
         
            </section>
        </div>
        <hr />
        <div class="row" runat="server">
            <section class="col col-lg-12">
                  <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-left: 5px;" ValidationGroup="NewCompany" ID="NextBtn" Text="Next" OnClick="NextBtn_Click" UseSubmitBehavior="false" Visible="true"></asp:Button>
            </section>
        </div>
    </div> 
     <div id="divError" runat="server" style="overflow-x: scroll; overflow-y: scroll; color:red ;" visible="false" >
       <strong>  Some Data is missing  , refer to Registrar Office to Complete .</strong>
      </div> 
    <script>

        $(document).ready(function () {
            console.log("ready!");
            debugger;
         
        });
    </script>
</asp:Content>

