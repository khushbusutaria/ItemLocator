<%@ Page Title="Tab Detail" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true"
    CodeFile="TabDetail.aspx.cs" Inherits="TabDetail" %>

<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">

        function setUpHandlers() {
            chkHasAddChanged();
            chkIsMenuChanged();
            chkIsShowOnDashboard();
            checkWebPageName();

            $("#<%= chkHasAddOption.ClientID %>").click(chkHasAddChanged);
            $("#<%= chkIsMenu.ClientID %>").click(chkIsMenuChanged);
            $("#<%= chkIsShowOnDashboard.ClientID %>").click(chkIsShowOnDashboard);
            $("#<%= txtWebPageName.ClientID %>").bind('keyup', checkWebPageName);
        }

        function checkWebPageName() {

            if ($("#<%= txtWebPageName.ClientID %>").val() == "") {
                $("#<%= chkIsShowOnDashboard.ClientID %>").prop('checked', false);
                $("#<%= chkIsShowOnDashboard.ClientID %>").prop('disabled', true);
            } else {
                $("#<%= chkIsShowOnDashboard.ClientID %>").prop('disabled', false);
            }

            chkIsShowOnDashboard();
        }

        function chkHasAddChanged() {

            if ($("#<%= chkHasAddOption.ClientID %>").prop('checked') == true) {

                $("#trPageAdd").show();

                ValidatorEnable(document.getElementById("ContentPlaceHolder1_reAddPage"), true);

                ValidatorEnable(document.getElementById("ContentPlaceHolder1_rfAddPage"), true);
            } else {
                $("#trPageAdd").hide();
                ValidatorEnable(document.getElementById("ContentPlaceHolder1_reAddPage"), false);
                ValidatorEnable(document.getElementById("ContentPlaceHolder1_rfAddPage"), false);
            }

        }

        function chkIsShowOnDashboard() {

            if ($("#<%= chkIsShowOnDashboard.ClientID %>").prop('checked') == true) {
                $("#trUploadImage").show();

                if ($("#<%= imgIconPreview.ClientID %>").prop('src') == location.href) {
                    $("#trPreviewIcon").hide('slow');
                    ValidatorEnable(document.getElementById("ContentPlaceHolder1_RegexpImageValid"), true);
                    ValidatorEnable(document.getElementById("ContentPlaceHolder1_reImage"), true);
                }
                else {
                    $("#trPreviewIcon").show();
                    ValidatorEnable(document.getElementById("ContentPlaceHolder1_RegexpImageValid"), false);
                    ValidatorEnable(document.getElementById("ContentPlaceHolder1_reImage"), false);
                }

            } else {
                $("#trUploadImage").hide();
                $("#trPreviewIcon").hide();
                ValidatorEnable(document.getElementById("ContentPlaceHolder1_reImage"), false);
            }
        }

        function chkIsMenuChanged() {
            if ($("#<%= chkIsMenu.ClientID %>").prop('checked') == true) {
                $("#<%= lblWebPageNameMsg.ClientID %>").attr('class', 'visible');
                $("#<%= lblAddPageMsg.ClientID %>").attr('class', 'visible');
                ValidatorEnable(document.getElementById("ContentPlaceHolder1_rfWebPageName"), true);
            } else {
                ValidatorEnable(document.getElementById("ContentPlaceHolder1_rfWebPageName"), false);
                $("#<%= lblWebPageNameMsg.ClientID %>").attr('class', 'hidden');
                $("#<%= lblAddPageMsg.ClientID %>").attr('class', 'hidden');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(setUpHandlers);
            </script>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Tab Detail
                        </div>
                        <div>
                            <div class="panel-search right-content">
                                <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click" />
                                <asp:Button ID="btnSaveAndClose" runat="server" ValidationGroup="save" CssClass="btn btn-primary"
                                    Text="Save & Close" OnClick="btnSaveAndClose_Click" />
                                <asp:Button ID="btnSaveAndAddnew" runat="server" ValidationGroup="save" CssClass="btn btn-primary"
                                    Text="Save & Add New" OnClick="btnSaveAndAddnew_Click" />
                                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-primary" Text="Clear"
                                    OnClick="btnClear_Click" />
                            </div>
                            <hr class="nomargin" />
                            <DInfo:DisplayInfo runat="server" ID="DInfo" />
                            <div class="panel-search">
                                <div class="table-responsive">
                                    <div class="entryformmain">
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Tab Name :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtTabName" Width="200" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="rfTabName" runat="server" ErrorMessage="Tab Name"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtTabName" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Web Page Name :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtWebPageName" Width="200" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RXPageAliasValid" runat="server" ControlToValidate="txtWebPageName"
                                                    CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Invalid WebPage Name : E.g xyz.aspx "
                                                    SetFocusOnError="True" ValidationExpression='^([a-z A-Z_.-0-9]*.aspx)?$' ValidationGroup="save">*</asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfWebPageName" runat="server" ControlToValidate="txtWebPageName"
                                                    CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Web Page Name" SetFocusOnError="True"
                                                    ValidationGroup="save">*</asp:RequiredFieldValidator>
                                                <asp:Label ID="lblWebPageNameMsg" runat="server" CssClass="hidden" Text="The Name of the webpage that opens for displaying list of menu items"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Parent Tab :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:DropDownList ID="ddlParent" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Is Active :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:CheckBox ID="ChkIsActive" runat="server" Text="" Checked="true"></asp:CheckBox>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Has Add Facility :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:CheckBox ID="chkIsAdd" runat="server" />
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Has Edit Facility :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:CheckBox ID="chkIsEdit" runat="server" />
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Has Delete Facility :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:CheckBox ID="chkIsDelete" runat="server" />
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Has Add Option :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:CheckBox ID="chkHasAddOption" runat="server" Checked="true" />
                                            </div>
                                        </div>
                                        <div class="entryform" id="trPageAdd">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Detail Page :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtAddPage" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="rfAddPage" runat="server" ErrorMessage="Page That Opens For Add"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtAddPage" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="reAddPage" runat="server" ControlToValidate="txtAddPage"
                                                    CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Invalid Page Name For Add: E.g xyz.aspx "
                                                    SetFocusOnError="True" ValidationExpression='^([a-z A-Z_.-0-9]*.aspx)?$' ValidationGroup="save">*</asp:RegularExpressionValidator>
                                                <asp:Label ID="lblAddPageMsg" runat="server" CssClass="hidden" Text="The Name of the webpage that opens for adding new menu item"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Is Menu :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:CheckBox ID="chkIsMenu" runat="server" />
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Is Show On DashBoard :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:CheckBox ID="chkIsShowOnDashboard" runat="server" Checked="true" />
                                            </div>
                                        </div>
                                        <div class="entryform" id="trUploadImage">
                                            <div class="labelstyle">
                                                Select Icon :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:FileUpload ID="FileUploadIcon" runat="server" />
                                                <asp:RegularExpressionValidator ID="RegexpImageValid" runat="server" ControlToValidate="FileUploadIcon"
                                                    Text="*" ErrorMessage="Only ICON & PNG images are allowed" ValidationExpression="(.*\.(([iI][cC][oO])|([pP][nN][gG])))"
                                                    ValidationGroup="save">
                                                </asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="reImage" runat="server" ErrorMessage="Choose Icon To Show On Dashboard"
                                                    ValidationGroup="save" Text="*" ControlToValidate="FileUploadIcon" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="entryform" id="trPreviewIcon">
                                            <div class="labelstyle">
                                                Preview Icon :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:Image ID="imgIconPreview" runat="server" Height="100px" Width="100px" />
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
                    <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
                    <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                        HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                        ValidationGroup="save" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveAndClose" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
