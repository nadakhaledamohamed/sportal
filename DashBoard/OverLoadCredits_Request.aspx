<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" Theme="Default" StylesheetTheme="Default" 
    CodeFile="OverLoadCredits_Request.aspx.cs" Inherits="DashBoard_OverLoadCredits_Request" 
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" 
    TagPrefix="ajaxToolKit" %>
<%@ Register Src="~/DashBoard/studentData.ascx" TagPrefix="uc1" TagName="studentData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="Server" Visible="true">
      <asp:HiddenField ID="hfyear" runat="server" />
     <asp:HiddenField ID="hfsem" runat="server" />
    
    <div class="row">
        <div class="col-xs-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-list"></i>
                        <span><strong> Year/ Semester  </strong></span>
                    </div>
                    <div class="box-icons">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                        <a class="expand-link">
                            <i class="fa fa-expand"></i>
                        </a>
                    </div>
                    <div class="no-move"></div>
                </div>
                </div>
            </div>
        </div>
     <uc1:studentData ID="studentData1" runat="server" />  
  <div  runat="server" style="padding:30px ; " >
      <div class="row" style="text-align:center">
        <section class="col col-lg-12" style="height:50px">
             <label > <strong>Request Details</strong></label>      
        </section>
    </div>
   <div class="row">
         <section class="col col-lg-2" style="height:50px">
              <label>Year / Semester </label>
          </section>
        <section class="col col-lg-10" style="height:50px">           
            <label id="lblys" runat="server" ></label>          
        </section>
    </div>
    <div class="row">
         <section class="col col-lg-2" style="height:50px">
              <label>Student Reason </label>
          </section>
        <section class="col col-lg-10" style="height:50px">           
           <asp:TextBox ID="txtReason" runat="server" CssClass="form-control"
               ValidationGroup="NewCompany" Width="50%" MaxLength="250"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReason"
                ErrorMessage="*" InitialValue=""
                Text="*" ValidationGroup="NewCompany"
                SetFocusOnError="True" Style="font-weight: bold; 
        font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>  
        </section>
    </div>

          <div class="row">
         <section class="col col-lg-2" style="height:50px">
              <label>Course Name / Course Code </label>
          </section>
        <section class="col col-lg-10" style="height:50px">           
           <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"
               ValidationGroup="NewCompany" Width="50%" MaxLength="250"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtReason"
                ErrorMessage="*" InitialValue=""
                Text="*" ValidationGroup="NewCompany"
                SetFocusOnError="True" Style="font-weight: bold; 
        font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>  
        </section>
    </div>
      <div class="row" runat="server">
            <section class="col col-lg-12">
                  <asp:Button runat="server"
                      CssClass="btn btn-primary" Style="margin-left: 5px;" 
                      ValidationGroup="NewCompany" ID="btnSubmit" Text="Submit" 
                      OnClick="btnSubmit_Click1" UseSubmitBehavior="false" 
                      Visible="true"></asp:Button>
            </section>
            </div>
      </div>
    <script>

        $(document).ready(function () {
            console.log("ready!");
            debugger;
         
        });
    </script>
</asp:Content>

