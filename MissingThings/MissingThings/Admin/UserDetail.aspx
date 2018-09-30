<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Main.master" AutoEventWireup="true"
    CodeFile="UserDetail.aspx.cs" Inherits="UserDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    User Detail
                </div>
                <div>
                    <div class="panel-search right-content">
                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click" />&nbsp;
                        <div class="btn-group">
                            <asp:Button runat="server" ID="btnSave" class="btn btn-primary" OnClick="btnSave_Click"
                                Text="Save" ValidationGroup="save"></asp:Button>
                            <button data-toggle="dropdown" class="btn btn-primary dropdown-toggle down-arrow">
                            </button>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkSaveAndClose" OnClick="lnkSaveAndClose_Click"
                                        Text="Save & Close" ValidationGroup="save"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkSaveAndAddnew" OnClick="lnkSaveAndAddnew_Click"
                                        Text="Save & Add New" ValidationGroup="save"></asp:LinkButton></li>
                            </ul>
                        </div>
                    </div>
                    <hr class="nomargin" />
                    <DInfo:DisplayInfo runat="server" ID="DInfo" />
                    <div class="panel-search">
                        <div class="table-responsive">
                            <div class="entryformmain">
                                <div class="page-inner-title">
                                    Login Info
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> User Name :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtUserName" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVUserName" runat="server" ErrorMessage="Enter User Name"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtUserName" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="entryform">
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
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        Role Name :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:DropDownList ID="ddlRoleName" runat="server" AutoPostBack="false" CssClass="form-control"
                                            >
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvRoleName" runat="server" ErrorMessage="Select Role Name"
                                            ValidationGroup="save" Text="*" ControlToValidate="ddlRoleName" SetFocusOnError="true"
                                            InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="page-inner-title">
                                    Personal Info
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Full Name :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtFullName" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvFullName" runat="server" ErrorMessage="Enter Full Name"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtFullName" SetFocusOnError="true"
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
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Is Active :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" />
                                        </div>
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
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Description :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtDescription" CssClass="form-control" Width="200" runat="server"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </div>
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
