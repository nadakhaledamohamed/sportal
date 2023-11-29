<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master"

    AutoEventWireup="true" Theme="Default" StylesheetTheme="Default"

    CodeFile="AppGrad_Request.aspx.cs"

    Inherits="DashBoard_AppGrad_Request"

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

        <section class="col col-lg-15" style="height:50px">

             <label > <strong>Request Details</strong></label>     

        </section>

    </div>

      </div>

   <div class="row">

         <section class="col col-lg-2" style="height:50px">

              <label>Apply Graduation Year/ Semester </label>

          </section>

        <section class="col col-lg-10" style="height:50px">          

            <label id="lblys" runat="server" ></label>         

        </section>

    </div>

 

      <%-- Student Full English Name : -------------------------------------------------%>

          <div class="row">

         <section class="col col-lg-2" style="height:50px">

              <label>Student Full English Name:   </label>

          </section>

        <section class="col col-lg-10" style="height:50px">          

           <asp:TextBox ID="ENametxt" runat="server" CssClass="form-control"

               ValidationGroup="NewCompany" Width="50%" MaxLength="250"></asp:TextBox>

            <asp:RequiredFieldValidator

                ID="RequiredFieldValidator1" runat="server" ControlToValidate="ENametxt"

                ErrorMessage="*" InitialValue=""

                Text="*" ValidationGroup="NewCompany"

                SetFocusOnError="True" Style="font-weight: bold;

        font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator> 

        </section>

    </div>



 <%-- -----------------------------Student Full Arabic Name: ------------------------------------------------ --%>



            <div class="row">

         <section class="col col-lg-2" style="height:50px">

              <label>Student Full Arabic Name:   </label>

          </section>

        <section class="col col-lg-10" style="height:50px">          

           <asp:TextBox ID="ANametxt" runat="server" CssClass="form-control"

               ValidationGroup="NewCompany" Width="50%" MaxLength="250"></asp:TextBox>

            <asp:RequiredFieldValidator

                ID="RequiredFieldValidator2" runat="server" ControlToValidate="ANametxt"

                ErrorMessage="*" InitialValue=""

                Text="*" ValidationGroup="NewCompany"

                SetFocusOnError="True" Style="font-weight: bold;

        font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator> 

        </section>

    </div>


    <%-- ----------------------------Personal Email:----------------------------------------- --%>

            <div class="row">

         <section class="col col-lg-2" style="height:50px">

              <label>Personal Email:   </label>

          </section>

        <section class="col col-lg-10" style="height:50px">          

           <asp:TextBox ID="PEmailtxt" runat="server" CssClass="form-control"

               ValidationGroup="NewCompany" Width="50%" MaxLength="250"></asp:TextBox>

            <asp:RequiredFieldValidator

                ID="RequiredFieldValidator3" runat="server" ControlToValidate="PEmailtxt"

                ErrorMessage="*" InitialValue=""

                Text="*" ValidationGroup="NewCompany"

                SetFocusOnError="True" Style="font-weight: bold;

        font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator> 

        </section>

    </div>

 
    <%-- ---------------------------------------BirthDate:  ------------------------------------%>
 

       <div class="row">

         <section class="col col-lg-2" style="height:50px">

              <label>BirthDate:   </label>

          </section>

           <section class="col col-lg-10" style="height:50px">  

        

          <asp:TextBox ID="BDate" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Width="50%" MaxLength="250"></asp:TextBox>

          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"  ControlToValidate="BDate"

              ErrorMessage="*" InitialValue=""

              Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>

          <ajaxToolkit:CalendarExtender Format="MM/dd/yyyy" ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="BDate"></ajaxToolkit:CalendarExtender>

        </section>

       </div>

     

  <%-- -------------------------------------Gender:---------------------------------------------- --%>

 

       <div class="row">

         <section class="col col-lg-2" style="height:50px">

              <label> Gender:  </label>

          </section>

        <section class="col col-lg-10" style="height:50px">    

         <asp:DropDownList ID="GenderDropDown" runat="server" Width="50%">

          <asp:ListItem Text="Select an option"  Value = "" />

          <asp:ListItem>Male</asp:ListItem>

          <asp:ListItem>Female</asp:ListItem>

        

      </asp:DropDownList>

            <asp:RequiredFieldValidator

                ID="RequiredFieldValidator9" runat="server" ControlToValidate="GenderDropDown"

                ErrorMessage="*" InitialValue=""

                Text="*" ValidationGroup="NewCompany"

                SetFocusOnError="True" Style="font-weight: bold;

        font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator> 

        </section>

    </div>

 

 

 <%-- ---------------------------------------National ID :-------------------------------------------- --%>

            <div class="row">

         <section class="col col-lg-2" style="height:50px">

              <label>National ID :   </label>

          </section>

        <section class="col col-lg-10" style="height:50px">          

           <asp:TextBox ID="NationalIdtxt" runat="server" CssClass="form-control"

               ValidationGroup="NewCompany" Width="50%" MaxLength="250"></asp:TextBox>

            <asp:RequiredFieldValidator

                ID="RequiredFieldValidator4" runat="server" ControlToValidate="NationalIdtxt"

                ErrorMessage="*" InitialValue=""

                Text="*" ValidationGroup="NewCompany"

                SetFocusOnError="True" Style="font-weight: bold;

        font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator> 

        </section>

    </div>

 <%-- -------------------------------------Nationality:---------------------------------------------- --%>

       <div class="row">

         <section class="col col-lg-2" style="height:50px">

              <label>Nationality:   </label>

          </section>

        <section class="col col-lg-10" style="height:50px">          

           <asp:TextBox ID="Nationalitytxt" runat="server" CssClass="form-control"

               ValidationGroup="NewCompany" Width="50%" MaxLength="250"></asp:TextBox>

            <asp:RequiredFieldValidator

                ID="RequiredFieldValidator5" runat="server" ControlToValidate="Nationalitytxt"

                ErrorMessage="*" InitialValue=""

                Text="*" ValidationGroup="NewCompany"

                SetFocusOnError="True" Style="font-weight: bold;

        font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator> 

        </section>

    </div>

 

 

 

 

 

 <%-- -------------------------------Major:  ----------------------------------------- --%>

   

        <div class="row">

         <section class="col col-lg-2" style="height:50px">

              <label> Major:  </label>

          </section>

        <section class="col col-lg-10" style="height:50px">    

         <asp:DropDownList ID="Majordropdown" runat="server" Width="50%">
         
      <asp:ListItem Text="Select an option"  Value = "" />

     <asp:ListItem>  Accounting                                            </asp:ListItem>
             
     <asp:ListItem>  Finance and Banking                                   </asp:ListItem>

     <asp:ListItem>  Entrepreneurship & Innovation                         </asp:ListItem>

     <asp:ListItem>  Human Resourses Management                            </asp:ListItem>

     <asp:ListItem>  International Business                                </asp:ListItem>

     <asp:ListItem>  Marketing                                             </asp:ListItem>

     <asp:ListItem>  Al and Data Science                                   </asp:ListItem>

     <asp:ListItem>  Computer Science                                      </asp:ListItem>

     <asp:ListItem>  Bioinformatics                                        </asp:ListItem>

     <asp:ListItem>  Economics                                             </asp:ListItem>

     <asp:ListItem>  Politics                                              </asp:ListItem>

     <asp:ListItem>  Graphic Design                                        </asp:ListItem>

     <asp:ListItem>  Fashion Design                                        </asp:ListItem>

     <asp:ListItem>  Interior Design                                       </asp:ListItem>

     <asp:ListItem>  Computers, Communications and Autonomous Systems      </asp:ListItem>

     <asp:ListItem>  Architecture Engineering                              </asp:ListItem>

     <asp:ListItem>  Mechanical Engineering                                </asp:ListItem>
     
         </asp:DropDownList>

        <asp:RequiredFieldValidator

                ID="RequiredFieldValidator7" runat="server" ControlToValidate="Majordropdown"

                ErrorMessage="*" InitialValue=""

                Text="*" ValidationGroup="NewCompany"

                SetFocusOnError="True" Style="font-weight: bold;

        font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator> 

        </section>

    </div>

 
    <%-- ------------------------------High School Name:--------------------------------- --%>


                  <div class="row">

         <section class="col col-lg-2" style="height:50px">

              <label> High School Name:  </label>

          </section>

        <section class="col col-lg-10" style="height:50px">          

           <asp:TextBox ID="HSchoolNametxt" runat="server" CssClass="form-control"

               ValidationGroup="NewCompany" Width="50%" MaxLength="250">

 

           </asp:TextBox>

            <asp:RequiredFieldValidator

                ID="RequiredFieldValidator" runat="server" ControlToValidate="HSchoolNametxt"

                ErrorMessage="*" InitialValue=""

                Text="*" ValidationGroup="NewCompany"

                SetFocusOnError="True" Style="font-weight: bold;

        font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator> 

        </section>

    </div>

 
<%-- --------------------------year:----------------------------------- --%>
        <div class="row">
         <section class="col col-lg-2" style="height:50px">
              <label> year:  </label>
          </section>
        <section class="col col-lg-10" style="height:50px">    
         <asp:DropDownList ID="yeartxt" runat="server" Width="50%">
      </asp:DropDownList>
            <asp:RequiredFieldValidator

                ID="RequiredFieldValidator10" runat="server" ControlToValidate="yeartxt"

                ErrorMessage="*" InitialValue=""

                Text="*" ValidationGroup="NewCompany"

                SetFocusOnError="True" Style="font-weight: bold;

        font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator> 

        </section>

    </div>

<%-- ------------------------------Previous Degree (High School):------------------------------------ --%>    
 
      <div class="row">
            <section class="col col-lg-2" style="height: 50px">
                <label>Previous Degree (High School):</label>
            </section>
            <section class="col col-lg-10" style="height: 50px">
                <asp:DropDownList ID="HschoolDropDown" runat="server" Width="50%" AutoPostBack="true" OnSelectedIndexChanged="HschoolDropDown_SelectedIndexChanged">
                    <asp:ListItem Text="Select an option"  Value = "" />
                    <asp:ListItem Text="Thanaweya Amma"    Value = "ThanaweyaAmma" />
                    <asp:ListItem Text="IGCSE"             Value = "IGCSE" />
                    <asp:ListItem Text="American diploma"  Value = "AmericanDiploma" />
                    <asp:ListItem Text="if Other Specify"     Value = "OtherSpecify"  />
                </asp:DropDownList>

                <asp:TextBox ID="Text1" runat="server" Enabled="false" aria-required="true" />

 
                <asp:RequiredFieldValidator
                    ID="RequiredFieldValidator8" runat="server" ControlToValidate="HschoolDropDown"
                    ErrorMessage="*" InitialValue=""
                    Text="*" ValidationGroup="NewCompany"
                    SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red">

                </asp:RequiredFieldValidator>
               
                
            <asp:CustomValidator
            ID="CustomValidator_Grad" runat="server"
            ControlToValidate="Text1"
            OnServerValidate="ValidateText_Grad"
            ErrorMessage="The 'Other Specify' field cannot be empty."
            ValidationGroup="NewCompany"
            SetFocusOnError="True" Style="font-weight: bold; font-size: 15px" ForeColor="Red">

            </asp:CustomValidator>

            </section>
        </div>

    


         <%--------------------------------Name in Ceremonial Certificate:---------------------------------------%>

 
             <div class="row">

         <section class="col col-lg-2" style="height:50px">

              <label>Name in Ceremonial Certificate:   </label>

          </section>

        <section class="col col-lg-10" style="height:50px">          

           <asp:TextBox ID="CerNametxt" runat="server" CssClass="form-control"

               ValidationGroup="NewCompany" Width="50%" MaxLength="250"></asp:TextBox>

            <asp:RequiredFieldValidator

                ID="RequiredFieldValidator11" runat="server" ControlToValidate="CerNametxt"

                ErrorMessage="*" InitialValue=""

                Text="*" ValidationGroup="NewCompany"

                SetFocusOnError="True" Style="font-weight: bold;

        font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator> 

        </section>

    </div>

 

    <%-- ----------------------Calling Name : ---------------------------- --%>

 

         <div class="row">

             <section class="col col-lg-2" style="height:50px">

                <label>Calling Name :   </label>

              </section>

        <section class="col col-lg-10" style="height:50px">          

           <asp:TextBox ID="CallingNametxt" runat="server" CssClass="form-control"

               ValidationGroup="NewCompany" Width="50%" MaxLength="250"></asp:TextBox>

            <asp:RequiredFieldValidator

                ID="RequiredFieldValidator12" runat="server" ControlToValidate="CallingNametxt"

                ErrorMessage="*" InitialValue=""

                Text="*" ValidationGroup="NewCompany"

                SetFocusOnError="True" Style="font-weight: bold;

                 font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator> 

        </section>

    </div>

    <%-- ------------------------Emergency Contact + Relation: ------------------------- --%>

        <div class="row">

         <section class="col col-lg-2" style="height:50px">

              <label>Emergency Contact + Relation:   </label>

          </section>

        <section class="col col-lg-10" style="height:50px">          

           <asp:TextBox ID="Emrgtxt" runat="server" CssClass="form-control"

               ValidationGroup="NewCompany" Width="50%" MaxLength="250"></asp:TextBox>

            <asp:RequiredFieldValidator

                ID="RequiredFieldValidator13" runat="server" ControlToValidate="Emrgtxt"

                ErrorMessage="*" InitialValue=""

                Text="*" ValidationGroup="NewCompany"

                SetFocusOnError="True" Style="font-weight: bold;

        font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator> 

        </section>

    </div>

 

    <%-- -------------------------NOTEEEEEEEEEEEEEEEE------------------------------ --%>

 

     <div class="row">

        <section class="col col-lg-2" style="height:50px"aria-required="true">

            <label><strong>Read this carefully:</strong></label>

        </section>

        <section class="col col-lg-10" style="height:50px"> 

 

            <div style="border: 1px solid #ccc; padding: 10px; margin-bottom: 10px;">

                <p><strong>It is important to understand that filling out this form does not guarantee graduation this year.

                    While this form is an important step in the process,

                    there are additional requirements and evaluations that need to be met in order to graduate.</strong></p>

            </div>

 

            <!-- Checkbox to confirm reading -->

            <label>
                  <asp:CheckBox ValidationGroup="NewCompany" ID="CheckBoxID_Grad" runat="server"  AutoPostBack="false"/>

                      I have read and understood the information.
            </label>

         

       <asp:CustomValidator runat="server" ID="Cust_TermValidation"

           ValidationGroup="NewCompany" SetFocusOnError="True"

           Style="font-weight: bold; font-size: 18px" ForeColor="Red"

           OnServerValidate="CheckBoxRequired_ServerValidate1"

           ErrorMessage="Please accept the terms..." />

        </section>

    </div>

   <br />

   <br />

   <br />

 

 

    <%-- --------------------------Personal photo---------------------------------- --%>


         <div class="row" runat="server">

                    <section class="col col-lg-12">

 

                        <label style="font-weight: bold;">Please attach Personal photo (professional photo white background) </label>

                        <br />

                        <div style="float: left">

                            <asp:FileUpload ID="photo_FileUploadControl" runat="server" AllowMultiple="true" ValidationGroup="photocomp"

                                onchange="IsFileSelected()" CssClass="FileTest" />

                              <asp:RequiredFieldValidator
                ID="RequiredFieldValidator20" runat="server" ControlToValidate="photo_FileUploadControl"
                ErrorMessage="No file selected or file size exceeds the limit." InitialValue=""
                Text="No file selected or file size exceeds the limit." ValidationGroup="NewCompany"
                SetFocusOnError="True" Style="font-weight: bold;
                font-size: 14px" ForeColor="Red">
                        </asp:RequiredFieldValidator> 
                       
                         

                            <asp:RegularExpressionValidator ID="RegExValFileUploadFileType" runat="server"

                                ControlToValidate="photo_FileUploadControl" ForeColor="Red"

                                ErrorMessage="Only .jpg,.png,.jpeg,.gif , .pdf Files are allowed" Font-Bold="True" ValidationGroup="photocomp"

                                Font-Size="Medium"

                                ValidationExpression="(.*?)\.(pdf|jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$"></asp:RegularExpressionValidator>

                            
                            <asp:CustomValidator ID="FileUploadCustomValidator" runat="server" ControlToValidate="photo_FileUploadControl"

                                OnServerValidate="FileUploadCustomValidator_ServerValidate" ErrorMessage="*File must be less then 2 mb"

                                ForeColor="Red" Display="Dynamic" ValidationGroup="photocomp"></asp:CustomValidator>

                        </div>

                        <br />

                        <asp:Label runat="server" ID="StatusLabel" Font-Bold="True" ForeColor="Green" Text=" Select Required files and click upload(Max.Size:2MB)"/>
                    
                      
                   
                        </section>
                </div>

 

      <%-- end File feild --%>

      

 

       <div class="row" runat="server">

            <section class="col col-lg-12">

                  <asp:Button runat="server"

                      CssClass="btn btn-primary" Style="margin-left: 5px;"

                      ValidationGroup="NewCompany" ID="btnSubmit" Text="Submit"

                      OnClick="btnSubmit_Click1" UseSubmitBehavior="false"

                      Visible="true"></asp:Button>

            </section>

            </div>




    <script>

        $(document).ready(function () {

            console.log("ready!");

            debugger;

        });

    </script>



  </asp:Content>

