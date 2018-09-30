<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Main.master" AutoEventWireup="true"
    CodeFile="BlockDetail.aspx.cs" Inherits="BlockDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Load() {

            setUpCK('<%=ckeBlockContent.ClientID %>');
            IsShow();
        }
        function IsShow() {
            if ($("#<%= ChkIsShowContent.ClientID %>").prop('checked')) {
                document.getElementById("divContent").style.display = 'block';

            } else {
                document.getElementById("divContent").style.display = 'none';

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.Application.add_load(Load);
    </script>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Block Detail
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
                                        <span class="mandatory">*</span> Block Name&nbsp; :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtBlockName" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfBlockName" runat="server" ErrorMessage="Block Name"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtBlockName" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="entryform" id="divControlID" runat="server">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Control ID&nbsp; :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtControlID" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfControlId" runat="server" ErrorMessage="Control ID"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtControlID" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="entryform" id="divIsShowContent" runat="server">
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Is Show Content&nbsp; :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="ChkIsShowContent" runat="server" Checked="True" onchange="IsShow()" />
                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="entryform" id="divContent" >
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Contents :
                                        </div>
                                        <div class="controlstyle">
                                            <CKEditor:CKEditorControl ID="ckeBlockContent" runat="server"></CKEditor:CKEditorControl>
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
                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                    ValidationGroup="save" />
            </div>
        </div>
    </div>
</asp:Content>
