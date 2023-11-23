<%@ Control Language="C#" AutoEventWireup="true" CodeFile="studentData.ascx.cs" Inherits="DashBoard_studentData" %>

<div id="CommaanDataDiv" runat="server" style="border:dotted ;padding:30px ; " >
    <div class="row" style="text-align:center">
        <section class="col col-lg-12" style="height:50px">
             <label > <strong>Student Data</strong></label>      
        </section>
    </div>
     <asp:HiddenField ID="hfschool" runat="server" />
     <asp:HiddenField ID="hflevel" runat="server" />
    <div class="row">
        <section class="col col-lg-2" style="height:50px">
            <label> ID Number</label>
           
        </section>
         <section class="col col-lg-10" style="height:50px">           
            <asp:TextBox ID="txtStudentID" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Width="200px" Enabled="false" Font-Bold="true" MaxLength="9"></asp:TextBox>
        </section>
    </div>
    <div class="row">
         <section class="col col-lg-2" style="height:50px">
              <label>Student Name</label>
          </section>
        <section class="col col-lg-10" style="height:50px">           
            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Font-Bold="true" Width="50%"  Enabled="false"></asp:TextBox>           
        </section>
    </div>
    <div class="row">
        <section class="col col-lg-2"  style="height:50px">
             <label >School</label>
            </section>
        <section class="col col-lg-10"  style="height:50px">          
            <asp:TextBox ID="txtSchoolName" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Font-Bold="true" Enabled="false" Width="50%"></asp:TextBox>
        </section>
    </div>
    <div class="row">
        <section class="col col-lg-2"  style="height:50px">
             <asp:Label  runat="server" Text="Year"></asp:Label>
            </section>
        <section class="col col-lg-10"  style="height:50px">          
            <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Width="50%" Enabled="False" ></asp:TextBox>
        </section>
    </div>
    <div class="row">
        <section class="col col-lg-2"  style="height:50px">
             <asp:Label ID="Label1" runat="server" Text="Email"></asp:Label>
        </section>
        <section class="col col-lg-10"  style="height:50px">           
            <asp:TextBox ID="txtMail" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Width="50%" MaxLength="250"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMail"
                ErrorMessage="*" InitialValue=""
                Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtMail" ErrorMessage="Invalid Email Format" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RegularExpressionValidator>

        </section>
    </div>
     <div class="row">
        <section class="col col-lg-2"  style="height:50px">
              <asp:Label  runat="server" Text="Mobile No"></asp:Label>
        </section>
         <section class="col col-lg-10"  style="height:50px">
              <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Width="50%" MaxLength="13"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMobile"
                ErrorMessage="*" InitialValue=""
                Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                ControlToValidate="txtMobile" ErrorMessage="Invalid Mobile No. Format"
                ValidationExpression="[1-9]\d*|0\d+" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RegularExpressionValidator>
         </section>
         </div>


</div>
