<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Main.master" AutoEventWireup="true"
    CodeFile="MenuItemsDetail.aspx.cs" Inherits="MenuItemsDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function checkBoxStatus() {

            $("#<%= chkIsLink.ClientID %>").click(checkLink);
            checkCreatePage();
            setUpCK();
            ValidationGroupEnable();


            $("#btnMenuPageAlias").click(function (e) {

                if (($("#<%= ddlPages.ClientID %>").children(':selected').val() != "0" && $("#<%= ddlPages.ClientID %>").children(':selected').val() != undefined)) {
                    var newValue = $("#<%= hdnAliasPrefix.ClientID %>").val() + $("#<%= ddlPages.ClientID %>").children(':selected').text().replace(/ /g, '-');
                    $("#<%= txtMenuPageAlias.ClientID %>").val(newValue);
                } else {
                    alert('Please select the page first');
                }

            });

            $("#btnPageAutoAlias").unbind("click").click(function (e) {

                if ($('#<%= txtPageName.ClientID %>').val() != "") {
                    var newValue = $("#<%= hdnAliasPrefix.ClientID %>").val() + $("#<%= txtPageName.ClientID %>").val().replace(/ /g, '-');
                    $("#<%= txtNewPageAlias.ClientID %>").val(newValue);
                } else {
                    alert('Please enter the page first');
                }

            });



            $("#<%= chkCreatePage.ClientID %>").unbind("change").change(function () {
                checkCreatePage();
                ValidationGroupEnable();
            });
        }


        function checkLink() {

            if ($("#<%= chkIsLink.ClientID %>").prop('checked') == true) {
                $("#<%= trOpenInNewTab.ClientID %>").find('*').css('visibility', 'visible');
                $("#<%= trLinkUrl.ClientID %>").find('*').css('visibility', 'visible');
                $("#<%= trPageFormat.ClientID %>").find('*').css('visibility', 'hidden');
                $("#<%= trPageFormatImage.ClientID %>").find('*').css('visibility', 'hidden');
                $("#<%= trPageFormat.ClientID %>").hide();
                $("#<%= trPageFormatImage.ClientID %>").hide();
                $("#<%= trOpenInNewTab.ClientID %>").show();
                $("#<%= trLinkUrl.ClientID %>").show();
            }
            else {
                $("#<%= trOpenInNewTab.ClientID %>").hide();
                $("#<%= trLinkUrl.ClientID %>").hide();
                $("#<%= trOpenInNewTab.ClientID %>").find('*').css('visibility', 'hidden');
                $("#<%= trLinkUrl.ClientID %>").find('*').css('visibility', 'hidden');

                if ($("#<%= chkCreatePage.ClientID %>").prop('checked') == true) {
                    $("#<%= trPageFormat.ClientID %>").show();
                    $("#<%= trPageFormatImage.ClientID %>").show();
                    $("#<%= trPageFormat.ClientID %>").find('*').css('visibility', 'visible');
                    $("#<%= trPageFormatImage.ClientID %>").find('*').css('visibility', 'visible');
                }

            }

            ValidationGroupEnable();
        }

        function checkCreatePage() {

            if ($("#<%= chkCreatePage.ClientID %>").prop('checked') == false) {
                $("#<%= trPageDetails.ClientID %>").find('*').css('visibility', 'hidden');
                $("#<%= trPageAliasOptions.ClientID %>").find('*').css('visibility', 'hidden');
                $("#<%= trPageOptions.ClientID %>").find('*').css('visibility', 'hidden');
                $("#<%= divSubtitle.ClientID %>").find('*').css('visibility', 'hidden');

            } else {
                $("#<%= trPageDetails.ClientID %>").find('*').css('visibility', 'visible');
                $("#<%= trPageAliasOptions.ClientID %>").find('*').css('visibility', 'visible');
                $("#<%= trPageOptions.ClientID %>").find('*').css('visibility', 'visible');
                $("#<%= divSubtitle.ClientID %>").find('*').css('visibility', 'Visible');
            }

            checkLink();

        }

        function ValidationGroupEnable() {
            for (i = 0; i < Page_Validators.length; i++) {
                var visible = $('#' + Page_Validators[i].controltovalidate).css('visibility');

                if (visible == 'visible') {
                    ValidatorEnable(Page_Validators[i], true);
                } else {
                    ValidatorEnable(Page_Validators[i], false);
                }
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

        function On_Load() {
            setUpCK('<%=txtDescription.ClientID %>');
            setUpCK('<%=CkEditorDynParameters.ClientID %>');

        }


        function pageLoad() {
            try {
                TitleCountCharacter();
                SEOWordCountCharacter();
                SEODescriptionCountCharacter();
            }
            catch (e)
            { }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.Application.add_load(On_Load);

    </script>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Menu Item Detail
                </div>
                <div>
                    <div class="panel-search">
                        <asp:Literal ID="siteMapLiteral" runat="server"></asp:Literal>
                    </div>
                    <hr class="nomargin" />
                    <div class="panel-search right-content">
                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click" TabIndex="31" />&nbsp;
                        <div class="btn-group">
                            <asp:Button runat="server" ID="btnSave" class="btn btn-primary" OnClick="btnSave_Click"
                                Text="Save" ValidationGroup="save" TabIndex="28"></asp:Button>
                            <button data-toggle="dropdown" class="btn btn-primary dropdown-toggle down-arrow">
                            </button>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkSaveAndClose" OnClick="lnkSaveAndClose_Click"
                                        Text="Save & Close" ValidationGroup="save" TabIndex="29"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkSaveAndAddnew" OnClick="lnkSaveAndAddnew_Click"
                                        Text="Save & Add New" ValidationGroup="save" TabIndex="30"></asp:LinkButton></li>
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
                                        <span class="mandatory">*</span> Main :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:DropDownList runat="server" ID="ddlMenuType" AutoPostBack="True" TabIndex="1"
                                            CssClass="form-control" OnSelectedIndexChanged="ddlMenuType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfMenuType" runat="server" ErrorMessage="Menu Type"
                                            ValidationGroup="save" Text="*" ControlToValidate="ddlMenuType" SetFocusOnError="true"
                                            InitialValue="0" CssClass="ErrorLabelStyle" />
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Parent :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:DropDownList runat="server" ID="ddlParent" CssClass="form-control" TabIndex="2"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlParent_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        Manu Item Type :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:DropDownList runat="server" ID="ddlMenuItemType" TabIndex="3" CssClass="form-control"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Name :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtMenuItemName" Width="300" runat="server" TabIndex="4" 
                                            CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfMenuItem" runat="server" ErrorMessage="MenuItem Name"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtMenuItemName" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        Is Active :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:CheckBox ID="ChkIsActive" runat="server" Text="" Checked="true" 
                                            TabIndex="5">
                                        </asp:CheckBox>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        Create Page :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:CheckBox ID="chkCreatePage" runat="server" Text="" AutoPostBack="true" TabIndex="6"
                                            OnCheckedChanged="chkCreatePage_CheckedChanged"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="entryform" id="trPageOptions" runat="server">
                                    <div class="labelstyle">
                                    </div>
                                    <div class="controlstyle">
                                        <asp:RadioButton Text="Create new page" runat="server" ID="rdbCreateNewPage" GroupName="pageSelection"
                                            AutoPostBack="True" TabIndex="7" 
                                            OnCheckedChanged="rdbCreateNewPage_CheckedChanged" />
                                        <asp:RadioButton Text="Use Existing" runat="server" ID="rdbUseExistingPage" GroupName="pageSelection"
                                            AutoPostBack="true" TabIndex="8" 
                                            OnCheckedChanged="rdbUseExistingPage_CheckedChanged" />
                                        <asp:RadioButton Text="Edit Existing" runat="server" ID="rdbEditExisting" GroupName="pageSelection"
                                            AutoPostBack="true" Visible="false" TabIndex="9" 
                                            OnCheckedChanged="rdbEditExisting_CheckedChanged" />
                                    </div>
                                </div>
                                <div class="entryform" id="trPageAliasOptions" runat="server">
                                    <div class="labelstyle">
                                        Enter Alias For The Page:
                                    </div>
                                    <div class="controlstyle">
                                        <asp:DropDownList runat="server" ID="ddlPages" Visible="false" TabIndex="10" 
                                            CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfddlPages" runat="server" ErrorMessage="Page Name"
                                            ValidationGroup="save" Text="*" ControlToValidate="ddlPages" SetFocusOnError="true"
                                            InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtMenuPageAlias" Width="300" runat="server" TabIndex="11"
                                            CssClass="form-control" />
                                        &nbsp;
                                        <%--<input type="button" id="btnMenuPageAlias" class="button" value="Auto Generate" onclick="return btnMenuPageAlias_onclick()" />--%>
                                        <asp:Button runat="server" ID="btnAutoAlias" class="btn btn-primary" Text="Auto Generate Alias"
                                            CausesValidation="false" OnClick="btnAutoAlias_Click" TabIndex="12"></asp:Button>
                                    </div>
                                </div>
                                <div class="page-inner-title" id="divSubtitle" runat="server">
                                    Page Detail
                                </div>
                                <div class="entryformmain" id="trPageDetails" runat="server">
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Page Name :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtPageName" runat="server" TabIndex="13" Width="500" 
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfPage" runat="server" ControlToValidate="txtPageName"
                                                CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Page Name" SetFocusOnError="true"
                                                Text="*" ValidationGroup="save"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Page Header :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtPageHeader" runat="server" TabIndex="14" Width="500" 
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfPageName" runat="server" ControlToValidate="txtPageHeader"
                                                CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Page Header" SetFocusOnError="true"
                                                Text="*" ValidationGroup="save"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Page Browser Title :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtPageTitle" runat="server" TabIndex="15" Width="500" CssClass="form-control"
                                                onKeyUp="TitleCountCharacter();"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfPageTitle" runat="server" ControlToValidate="txtPageTitle"
                                                CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Page Title" SetFocusOnError="true"
                                                Text="*" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            <br />
                                                [Preferable Character Length: <b>10 to 70</b>] [Current Character Length:
                                            <asp:Label ID="lblTitle" runat="server">0</asp:Label>
                                            ]
                                        </div>
                                    </div>
                                    <div class="entryform" id="trdynParameters" runat="server">
                                        <div class="labelstyle">
                                            Page Dynamic Parameters :
                                        </div>
                                        <div class="controlstyle">
                                            <CKEditor:CKEditorControl ID="CkEditorDynParameters" runat="server" 
                                                TabIndex="16"></CKEditor:CKEditorControl>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Page Alias :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtNewPageAlias" runat="server" TabIndex="17" Width="500" 
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RXPageAliasValid" runat="server" ControlToValidate="txtNewPageAlias"
                                                CssClass="ErrorLabelStyle" Display="Dynamic" SetFocusOnError="True" ValidationGroup="save">*</asp:RegularExpressionValidator>
                                            <%--<input type="button" id="btnPageAutoAlias" class="button" value="Auto Generate" />--%>
                                            <asp:Button ID="btnPageAutoAlias" runat="server" class="btn btn-primary" Text="Auto Generate Alias"
                                                CausesValidation="false" OnClick="btnPageAutoAlias_Click" TabIndex="18" />
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            SEO Word :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtSEOWord" runat="server" CssClass="form-control" TextMode="MultiLine"
                                                Width="500" onKeyUp="SEOWordCountCharacter();" TabIndex="19"></asp:TextBox>
                                           <br /> [Preferable Character Lenth: <b>0 to 200</b>] [Current Character Length:
                                            <asp:Label ID="lblSEOWord" runat="server">0</asp:Label>
                                            ]
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            SEO Description :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtSEODescription" runat="server" CssClass="form-control" TabIndex="20"
                                                TextMode="MultiLine" Width="500" 
                                                onKeyUp="SEODescriptionCountCharacter();" />
                                            <br /> [Preferable Character Lenth: <b>70 to 160</b>] [Current Character Length:
                                            <asp:Label ID="lblSEODescription" runat="server">0</asp:Label>
                                            ]
                                        </div>
                                    </div>
                                    <div class="entryform" id="trIsLink" runat="server">
                                        <div class="labelstyle">
                                            Is Link:
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkIsLink" runat="server" Checked="false" AutoPostBack="true" TabIndex="21"
                                                Text="" OnCheckedChanged="ChkIsLink_CheckedChanged" />
                                        </div>
                                    </div>
                                    <div class="entryform" id="trOpenInNewTab" runat="server">
                                        <div class="labelstyle">
                                            Open In New Tab:
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkIsOpenInNewTab" runat="server" Checked="false" TabIndex="22"
                                                Text="" />
                                        </div>
                                    </div>
                                    <div class="entryform" id="trLinkUrl" runat="server">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Link URL :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtLinkURL" runat="server" TabIndex="23" CssClass="form-control"
                                                Width="500"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfLinkUrl" runat="server" ControlToValidate="txtLinkURL"
                                                CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Link URL" SetFocusOnError="true"
                                                Text="*" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="reLinkUrl" runat="server" ControlToValidate="txtLinkURL"
                                                CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Invalid Page Name: E.g xyz.aspx, http://www.abc.com "
                                                SetFocusOnError="True" ValidationExpression="^(([a-z A-Z_.-0-9]*.aspx)|(http[s]?://www\..*))"
                                                ValidationGroup="save">*</asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="entryform" id="trPageFormat" runat="server">
                                        <div class="labelstyle">
                                            PageFormat:
                                        </div>
                                        <div class="controlstyle">
                                            <asp:DropDownList ID="ddlPageFormats" runat="server" CssClass="form-control" AutoPostBack="True"
                                                TabIndex="24">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfPageFormats" runat="server" ControlToValidate="ddlPageFormats"
                                                CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Page Format" InitialValue="0"
                                                SetFocusOnError="true" Text="*" ValidationGroup="save"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="entryform" id="trPageFormatImage" runat="server">
                                        <div class="labelstyle">
                                        </div>
                                        <div class="controlstyle">
                                            <asp:Image ID="imgPageFormat" runat="server" Height="150px" />
                                        </div>
                                    </div>
                                    <div class="entryform" id="trIsStatic" runat="server">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span>Is Static :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkIsStatic" runat="server" Text="" Checked="false" TabIndex="25"
                                                AutoPostBack="true" OnCheckedChanged="chkIsStatic_CheckedChanged"></asp:CheckBox>
                                        </div>
                                    </div>
                                    <div class="entryform" id="trDefaultPage" runat="server">
                                        <div class="labelstyle">
                                            Is Default Page :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkIsDefault" runat="server" Checked="false" TabIndex="26" 
                                                Text="" />
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Content :
                                        </div>
                                        <div class="controlstyle">
                                            <CKEditor:CKEditorControl ID="txtDescription" Height="500" runat="server" 
                                                TabIndex="27"></CKEditor:CKEditorControl>
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
                <asp:HiddenField ID="hdnMenuTypeId" Value="" runat="server" />
                <asp:HiddenField ID="hdnPageId" runat="server" Value="" />
                <asp:HiddenField ID="hdnAliasPrefix" runat="server" Value="" />
                <asp:HiddenField ID="hdnPageName" runat="server" Value="" />
                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                    ValidationGroup="save" />
            </div>
        </div>
    </div>
</asp:Content>
