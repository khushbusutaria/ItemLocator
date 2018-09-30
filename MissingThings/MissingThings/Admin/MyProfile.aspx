<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true"
    CodeFile="MyProfile.aspx.cs" Inherits="MyProfile" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    My Profile
                </div>
                <div>
                    <div class="panel-search right-content">
                        <div class="btn-group">
                            <asp:Button runat="server" ID="btnSave" class="btn btn-primary" OnClick="btnSave_Click"
                                Text="Save" ValidationGroup="save"></asp:Button>
                        </div>
                    </div>
                    <hr class="nomargin" />
                    <DInfo:DisplayInfo runat="server" ID="DInfo" />
                    <div class="panel-search">
                        <div class="table-responsive">
                            <div class="entryformmain">
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Name :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtFullName" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvFullName" runat="server" ErrorMessage="Enter Employee Name"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtFullName" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <%--  <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Password :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtPassword" CssClass="form-control" Width="200" runat="server"
                                            TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvPassword" runat="server" ErrorMessage="Enter Password"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtPassword" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                </div>--%>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Mobile No :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtMobileNo" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvMobileNo" runat="server" ErrorMessage="Enter Mobile No"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtMobileNo" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Email Address :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtEmailAddress" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvEmailAddress" runat="server"
                                            ErrorMessage="Enter Email Address" ValidationGroup="save" Text="*" ControlToValidate="txtEmailAddress"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RXEmailValid" runat="server" ControlToValidate="txtEmailAddress"
                                            CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Invalid EmailAddress : Ex:abc@xyz.com"
                                            SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="save">*</asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        Address :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtAddress" CssClass="form-control" Width="200" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator Display="Dynamic" ID="rfvAddress" runat="server" ErrorMessage="Enter Address"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtAddress" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                          
                                <div class="entryform">
                                    <div class="labelstyle">
                                        User Photo :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:FileUpload ID="FileUploadImg" runat="server" />
                                        <%--<asp:RegularExpressionValidator ID="revFileImg" runat="server" ControlToValidate="FileUploadImg"
                                            ErrorMessage="Upload Image" ValidationGroup="save">
                                        </asp:RegularExpressionValidator>--%>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                    </div>
                                    <div class="controlstyle">
                                        <asp:Image ID="imgUserPhoto" runat="server" Height="100px" Width="100px" />
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                    </div>
                                    <div class="controlstyle">
                                        <asp:LinkButton ID="lnkbtnRemovePhoto" runat="server" OnClientClick="return confirm('Are you sure to delete?')"
                                            OnClick="lnkbtnRemovePhoto_Click">Remove Photo</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <asp:HiddenField ID="hdnPKID" Value="" runat="server" />
                <asp:HiddenField ID="hdnPhoto" runat="server" />
                <asp:HiddenField ID="hdnImage" runat="server" />
                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                    ValidationGroup="save" />
            </div>
        </div>
</asp:Content>
