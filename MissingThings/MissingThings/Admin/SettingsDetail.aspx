<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true"
    CodeFile="SettingsDetail.aspx.cs" Inherits="SettingsDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <%-- <script type="text/javascript">
        function checkLink() {
            if ($("#<%= chkDataBaseSettings.ClientId %>").prop('checked') == true) {
                $("#isDatasource").show();
                $("#divDatasource").find('*').css('visibility', 'visible');
            }
            else {
                $("#divDatasource").find('*').css('visibility', 'hidden');
                $("#isDatasource").hide();
            }
            ValidationGroupEnable();
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
         
            
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript">
        Sys.Application.add_load(checkLink);
    </script>--%>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                Global Settings&nbsp;
                            </div>
                            <div class="panel-body">
                                <div class="panel-search right-content">
                               <%--     <asp:CheckBox ID="chkDataBaseSettings" runat="server" Text="Change Database Settings" />--%>
                                    <div class="btn-group">
                                        <asp:Button runat="server" ID="btnSaveAndClose" class="btn btn-primary" OnClick="btnSaveAndClose_Click"
                                            Text="Save" ValidationGroup="save"></asp:Button>
                                    </div>
                                </div>
                                <hr class="nomargin" />
                                <DInfo:DisplayInfo runat="server" ID="DInfo" />
                                <ul class="nav nav-tabs">
                                    <li class="active"><a href="#tabs-1" data-toggle="tab">Site</a></li>
                                    <li class=""><a href="#tabs-2" data-toggle="tab">Site Offline</a></li>
                                    <li class=""><a href="#tabs-3" data-toggle="tab">Email Settings</a></li>
                                    <%--<li id="isDatasource" class=""><a href="#divDatasource" data-toggle="tab">Datasource
                                        System</a></li>--%>
                                    <li class=""><a href="#tabs-5" data-toggle="tab">Basic Settings</a></li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane fade active in" id="tabs-1">
                                        <div class="entryformmain">
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Site Name&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtSiteName" CssClass="form-control" Width="200" runat="server"
                                                        TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="rfTxtSiteName" runat="server" ErrorMessage="Site Name"
                                                        ValidationGroup="save" Text="*" ControlToValidate="txtSiteName" SetFocusOnError="true"
                                                        CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    <span class="mandatory">*</span>Client Site Url&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtClientSiteUrl" CssClass="form-control" Width="200" runat="server"
                                                        TabIndex="2"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rdClientSiteUrl" runat="server" ControlToValidate="txtClientSiteUrl"
                                                        CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Client-Site Url Is Required"
                                                        SetFocusOnError="True" ValidationGroup="save" Text="*"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Site Logo&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:FileUpload ID="FileUploadLogo" runat="server" TabIndex="3" />
                                                    <asp:RegularExpressionValidator ID="reFileLogo" runat="server" ControlToValidate="FileUploadLogo"
                                                        ErrorMessage="Only JPEG and PNG images allowed images are allowed" ValidationExpression="(.*\.([Jj][Pp][Gg])|.*\.([Jj][Pp][Ee][Gg])|.*\.([pP][nN][Gg])$)"
                                                        ValidationGroup="save">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Current Logo&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:Image ID="ImgLogo" runat="server" Height="128px" Width="213px" TabIndex="4" />
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Site Favicon&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:FileUpload ID="FileUploadFavicon" runat="server" TabIndex="5" />
                                                    <asp:RegularExpressionValidator ID="RegexpImageValid" runat="server" ControlToValidate="FileUploadFavicon"
                                                        ErrorMessage="Only ICON images are allowed" ValidationExpression="(.*\.([iI][cC][oO]))"
                                                        ValidationGroup="save">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Current Favicon&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:Image ID="ImgFavicon" runat="server" Height="32px" Width="34px" TabIndex="6" />
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Site TagLine&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtSiteTagLine" Width="200" runat="server" CssClass="form-control"
                                                        TabIndex="7"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Site Footer&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtFooter" Width="200" runat="server" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="tabs-2">
                                        <div class="entryformmain">
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Site Offline&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:CheckBox ID="chkSiteOffline" runat="server" Text="" Checked="False" TabIndex="9">
                                                    </asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Site Offline Message&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtSiteOfflineMessage" Width="200" runat="server" TabIndex="10"
                                                        Rows="2" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Site Offline Image&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:FileUpload ID="FileUploadOfflineImage" runat="server" TabIndex="11" />
                                                    <asp:RegularExpressionValidator ID="reOfflineImage" runat="server" ControlToValidate="FileUploadOfflineImage"
                                                        ErrorMessage="Only JPEG images are allowed" ValidationExpression="(.*\.([Jj][Pp][Gg])|.*\.([Jj][Pp][Ee][Gg])$)"
                                                        ValidationGroup="save">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Current Offline Image&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:Image ID="imgOfflineImage" runat="server" Height="127px" Width="218px" TabIndex="12" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="tabs-3">
                                        <div class="entryformmain">
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    <span class="mandatory">*</span>Received on email&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtRecepientEmail" Width="200" runat="server" CssClass="form-control"
                                                        TabIndex="13"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="reRecepientEmail" runat="server" ControlToValidate="txtRecepientEmail"
                                                        CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Invalid EmailAddress : Ex:abc@xyz.com"
                                                        SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="save">*</asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    <span class="mandatory">*</span>Send from email&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtSiteEmail" Width="200" runat="server" CssClass="form-control"
                                                        TabIndex="14"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RXEmailValid" runat="server" ControlToValidate="txtSiteEmail"
                                                        CssClass="ErrorLabelStyle" Display="Dynamic" ErrorMessage="Invalid EmailAddress : Ex:abc@xyz.com"
                                                        SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="save">*</asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Send from email - SMTP&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtSMTP" Width="200" runat="server" CssClass="form-control" TabIndex="15"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Send from email - Password&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtEmailPassword" Width="200" runat="server" TextMode="Password"
                                                        CssClass="form-control" TabIndex="15"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Send from email - Port Number&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtPortNumber" Width="200" runat="server" CssClass="form-control"
                                                        TextMode="SingleLine" MaxLength="5" TabIndex="16"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                  <%--  <div class="tab-pane fade" id="divDatasource">
                                        <div class="entryformmain">
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    <span class="mandatory">*</span>Datasource User Name&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtDatasourceUserName" Width="200px" runat="server" CssClass="form-control"
                                                        TabIndex="17"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="rfTxtDatasourceUserName" runat="server"
                                                        ErrorMessage="Datasource UserName" ValidationGroup="save" Text="*" ControlToValidate="txtDatasourceUserName"
                                                        SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Datasource Password&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtDatasourcePassword" Width="200px" runat="server" CssClass="form-control"
                                                        TabIndex="18" TextMode="Password"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    <span class="mandatory">*</span> Datasource Name&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtDatasourceName" Width="200px" runat="server" CssClass="form-control"
                                                        TabIndex="19"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="rfTxtDatasourceName" runat="server"
                                                        ErrorMessage="Datasource Name" ValidationGroup="save" Text="*" ControlToValidate="txtDatasourceName"
                                                        SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    <span class="mandatory">*</span> DataBase Name&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtDataBaseName" Width="200px" runat="server" CssClass="form-control"
                                                        TabIndex="20"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="rfDatabaseName" runat="server"
                                                        ErrorMessage="DataBase Name" ValidationGroup="save" Text="*" ControlToValidate="txtDataBaseName"
                                                        SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="tab-pane fade" id="tabs-5">
                                        <div class="entryformmain">
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    <span class="mandatory">*</span> Path To Folder&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtPathToFolder" Width="200" runat="server" CssClass="form-control"
                                                        TabIndex="21"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="rfTxtPathToFolder" runat="server"
                                                        ErrorMessage="Path To Folder" ValidationGroup="save" Text="*" ControlToValidate="txtPathToFolder"
                                                        SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Site Default List Limit&nbsp; :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:DropDownList ID="ddlPerMenu" runat="server" CssClass="DDLStyle" TabIndex="22">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                    ValidationGroup="save" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSaveAndClose" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
