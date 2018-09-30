<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true"
    CodeFile="Query.aspx.cs" Inherits="Admin_Query" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Query
                </div>
                <div>
                    <div class="panel-search">
                        <div class="table-responsive">
                            <div class="entryformmain">
                                <div class="entryform">
                                <DInfo:DisplayInfo runat="server" ID="DInfo" />
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Query :
                                    </div>
                                    
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtBannerTitle" CssClass="form-control" Width="60%" TextMode="MultiLine"
                                            Rows="5" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVBannerTitle" runat="server"
                                            ErrorMessage="Enter Banner Title" ValidationGroup="save" Text="*" ControlToValidate="txtBannerTitle"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                    
                                </div>
                                <div class="entryform">
                                    <div class="controlstyle">
                                        <asp:Button runat="server" ID="btnClear" class="btn btn-primary" OnClick="btnClear_Click"
                                            Text="Clear"></asp:Button>
                                        <asp:Button runat="server" ID="btnExecute" class="btn btn-primary" OnClick="btnExecute_Click"
                                            Text="Execute" ValidationGroup="save"></asp:Button>
                                    </div>
                                </div>
                            </div>
                            <div id="QueryResult" runat="server" class="table-responsive">
                                <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover">
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    
                </div>
            </div>
            <asp:HiddenField ID="hdnPKID" Value="" runat="server" />
            <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                ValidationGroup="save" />
        </div>
</asp:Content>
