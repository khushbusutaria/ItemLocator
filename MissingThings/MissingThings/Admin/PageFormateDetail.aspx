<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Main.master" AutoEventWireup="true"
    CodeFile="PageFormateDetail.aspx.cs" Inherits="PageFormateDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Page Formate Detail
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
                            <div class="table-responsive">
                                <div class="table-responsive" style="float: left; width: 50%;">
                                    <div class="entryformmain">
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> PageFormat Name :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtPageFormatName" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVPageFormatName" runat="server"
                                                    ErrorMessage="Enter Page Format Name" ValidationGroup="save" Text="*" ControlToValidate="txtPageFormatName"
                                                    SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Server Page Name&nbsp; :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtPageName" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="rePageName" runat="server" ControlToValidate="txtPageName"
                                                    CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Invalid WebPage Name : E.g xyz.aspx "
                                                    SetFocusOnError="True" ValidationExpression='^([a-z A-Z_.-0-9]*.aspx)$' ValidationGroup="save">*
                                                </asp:RegularExpressionValidator>
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
                                                Image :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:FileUpload ID="FileUploadImg" runat="server" />
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Description :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Columns="25" Rows="5"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="table-responsive" style="float: right; width: 50%;">
                                    <asp:Image ID="imgCurrent" runat="server" Height="260px" Width="378px" Style="font-weight: 700" />
                                </div>
                                <div style="clear: both;">
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
