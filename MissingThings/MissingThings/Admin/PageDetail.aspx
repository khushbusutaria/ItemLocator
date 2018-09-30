<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Main.master" AutoEventWireup="true"
    CodeFile="PageDetail.aspx.cs" Inherits="PageDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .Message
        {
            font-size: 16Px;
            font-weight: normal;
            margin-top: -15Px;
        }
    </style>
    <script type="text/javascript">
        function on_load() {
            try {
                setUpCK('<%=ckePageContent.ClientID %>');
                TitleCountCharacter();
                SEOWordCountCharacter();
                SEODescriptionCountCharacter();
            }
            catch (e) {
            }
        }
        function TitleCountCharacter() {
            document.getElementById("<%=lblTitle.ClientID %>").innerHTML = document.getElementById("<%=txtPageTitle.ClientID %>").value.length;
        }
        function SEOWordCountCharacter() {
            document.getElementById("<%=lblSEOWord.ClientID %>").innerHTML = document.getElementById("<%=txtSEOWord.ClientID %>").value.length;
        }
        function SEODescriptionCountCharacter() {
            document.getElementById("<%=lblSEODescription.ClientID %>").innerHTML = document.getElementById("<%=txtSEODescription.ClientID %>").value.length;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.Application.add_load(on_load);
    </script>
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Page Detail
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
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Page Name :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtPageName" CssClass="form-control" Width="400" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="rfPage" runat="server" ErrorMessage="Page Name"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtPageName" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Page Header :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtPageHeader" CssClass="form-control" Width="400" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="rfPageName" runat="server" ErrorMessage="Page Header"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtPageHeader" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Page Browser Title :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtPageTitle" CssClass="form-control" Width="400" runat="server" onKeyUp="TitleCountCharacter();"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="rfPageTitle" runat="server" ErrorMessage="Page Title"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtPageTitle" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                <br />
                                                [Preferable Character Length: <b>10 to 70</b>] [Current Character Length:
                                                <asp:Label ID="lblTitle" Font-Bold="true" runat="server">0</asp:Label>]
                                            </div>
                                        </div>
                                        <div class="entryform" id="trdynParameters" runat="server">
                                            <div class="labelstyle">
                                                Page Dynamic Parameters :
                                            </div>
                                            <div class="controlstyle">
                                                <CKEditor:CKEditorControl ID="CkEditorDynParameters" runat="server"></CKEditor:CKEditorControl>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Page Alias :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtPageAlias" CssClass="form-control" Width="400" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVAlias" runat="server" ErrorMessage="Page Alias"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtPageAlias" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RXPageAliasValid" runat="server" ControlToValidate="txtPageAlias"
                                                    CssClass="ErrorLabelStyle" Display="Dynamic" SetFocusOnError="True" ValidationGroup="save">*</asp:RegularExpressionValidator>
                                                <asp:Button runat="server" ID="btnAutoAlias" class="btn btn-primary" Text="Auto Generate Alias"
                                                    CausesValidation="false" OnClick="btnAutoAlias_Click"></asp:Button>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                SEO Word :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtSEOWord" CssClass="form-control" Width="400" runat="server" TextMode="MultiLine"
                                                    onKeyUp="SEOWordCountCharacter();"></asp:TextBox>
                                                <br />
                                                [Preferable Character Lenth: <b>0 to 200</b>] [Current Character Length:
                                                <asp:Label ID="lblSEOWord" Font-Bold="true" runat="server">0</asp:Label>]
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                SEO Description :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtSEODescription" CssClass="form-control" Width="400" runat="server"
                                                    TextMode="MultiLine" onKeyUp="SEODescriptionCountCharacter();"></asp:TextBox>
                                                <br />
                                                [Preferable Character Lenth: <b>70 to 160</b>] [Current Character Length:
                                                <asp:Label ID="lblSEODescription" Font-Bold="true" runat="server">0</asp:Label>]
                                            </div>
                                        </div>
                                        <div class="entryform" id="trIsLink" runat="server">
                                            <div class="labelstyle">
                                                Is Link :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:CheckBox ID="ChkIsLink" runat="server" Checked="True" AutoPostBack="true" OnCheckedChanged="ChkIsLink_CheckedChanged" />
                                            </div>
                                        </div>
                                        <div class="entryform" id="trOpenInNewTab" runat="server">
                                            <div class="labelstyle">
                                                Open In New Tab :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:CheckBox ID="chkIsOpenInNewTab" runat="server" Checked="false" />
                                            </div>
                                        </div>
                                        <div class="entryform" id="trLinkUrl" runat="server">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Link URL :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtLinkURL" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfLinkUrl" runat="server" ControlToValidate="txtLinkURL"
                                                    CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Link URL" SetFocusOnError="true"
                                                    Text="*" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="reLinkUrl" runat="server" ControlToValidate="txtLinkURL"
                                                    CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Invalid Page Name: E.g xyz.aspx, http://www.abc.com "
                                                    SetFocusOnError="True" ValidationExpression='^(([a-z A-Z_.-0-9]*.aspx)|(http[s]?://www\..*))'
                                                    ValidationGroup="save">*</asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="entryform" id="trPageFormat" runat="server">
                                            <div class="labelstyle">
                                                PageFormat :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:DropDownList ID="ddlPageFormats" runat="server" AutoPostBack="true" CssClass="form-control"
                                                    OnSelectedIndexChanged="ddlPageFormats_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfPageFormats" runat="server" ControlToValidate="ddlPageFormats"
                                                    Display="Dynamic" InitialValue="0" ValidationGroup="save" SetFocusOnError="true"
                                                    ErrorMessage="Page Format" Text="*" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="entryform" id="trPageFormatImage" runat="server">
                                            <div class="labelstyle">
                                            </div>
                                            <div class="controlstyle">
                                                <asp:Image ID="imgPageFormat" runat="server" Height="150px" /></div>
                                        </div>
                                        <div class="entryform" id="trIsStatic" runat="server">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span>Is Static :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:CheckBox ID="chkIsStatic" runat="server" Checked="false" AutoPostBack="true"
                                                    OnCheckedChanged="chkIsStatic_CheckedChanged" />
                                            </div>
                                        </div>
                                        <div class="entryform" id="trDefaultPage" runat="server">
                                            <div class="labelstyle">
                                                Is Default Page :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:CheckBox ID="chkIsDefault" runat="server" Checked="false" />
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Content :
                                            </div>
                                            <div class="controlstyle">
                                                <CKEditor:CKEditorControl ID="ckePageContent" runat="server" TabIndex="9"></CKEditor:CKEditorControl>
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
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
