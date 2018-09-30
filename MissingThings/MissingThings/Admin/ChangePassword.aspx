<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Change Password
                        </div>
                        <div>
                            <div class="panel-search right-content">
                                <asp:Button ID="btnSaveAndClose" runat="server" ValidationGroup="save" CssClass="btn btn-primary"
                                    Text="Save" TabIndex="5" OnClick="btnSaveAndClose_Click" />
                            </div>
                            <hr class="nomargin" />
                            <DInfo:DisplayInfo runat="server" ID="DInfo" />
                            <div class="panel-search">
                                <div class="table-responsive">
                                    <div class="entryformmain">
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Old Password :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtOldPassword" Width="200" runat="server" CssClass="form-control"
                                                    TabIndex="1" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="rfOldPassword" runat="server" ErrorMessage="Enter Old Password"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtOldPassword" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> New Password :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtNewPassword" Width="200" runat="server" CssClass="form-control"
                                                    TabIndex="1" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="rfNewPassword" runat="server" ErrorMessage="Enter New Password"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtNewPassword" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Confirm Password :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtConfirmPassword" Width="200" runat="server" CssClass="form-control"
                                                    TabIndex="1" TextMode="Password"></asp:TextBox>
                                                       <asp:RequiredFieldValidator Display="Dynamic" ID="RFV" runat="server" ErrorMessage="Enter Confirm Password "
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtConfirmPassword" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cmpConfirmPassword" runat="server" ControlToCompare="txtNewPassword"
                                                    ControlToValidate="txtConfirmPassword" ErrorMessage="New Password and Confirm Password Don't Match"
                                                    ValidationGroup="save" Text="*" CssClass="ErrorLabelStyle" Display="Dynamic"></asp:CompareValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnPKID" Value="" runat="server" />
            <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
            <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                ValidationGroup="save" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
