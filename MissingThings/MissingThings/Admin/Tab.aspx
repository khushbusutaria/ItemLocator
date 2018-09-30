<%@ Page Title="Tab List" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true"
    CodeFile="Tab.aspx.cs" Inherits="Tab" %>

<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Tab List
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="panel-search">
                                <asp:Literal runat="server" ID="litSiteMap">
                                </asp:Literal>
                            </div>
                            <hr class="nomargin" />
                            <div class="panel-search">
                                Select Criteria :
                                <asp:DropDownList runat="server" ID="ddlFields" CssClass="form-control" TabIndex="3">
                                    <asp:ListItem Text="Tab Name" Value="appTabName"></asp:ListItem>
                                    <asp:ListItem Text="Form Name" Value="appWebPageName"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ValidationGroup="Search"
                                    ErrorMessage="Search Text" Display="None" Font-Bold="True" SetFocusOnError="true"
                                    ControlToValidate="txtSearch" CssClass="ErrorLabelStyle" Text="*">
                                </asp:RequiredFieldValidator>
                                <asp:Button ID="btnGO" runat="server" CssClass="btn btn-primary" Text="Search" ValidationGroup="Search"
                                    TabIndex="2" OnClick="btnGO_Click" />
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                    TabIndex="4" OnClick="btnReset_Click" />
                            </div>
                            <hr class="nomargin" />
                            <div class="panel-search">
                                <div class="fleft">
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete"
                                        OnClientClick="return ConfirmMessage('Tab','delete')" OnClick="btnDelete_Click" />
                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add New"
                                        TabIndex="6" OnClick="btnAdd_Click" />
                                </div>
                                <div class="fright">
                                    Total&nbsp;Records&nbsp;:&nbsp; <span class="RecordCount">
                                        <asp:Label ID="lblCount" runat="server" Text="0"> </asp:Label>
                                    </span>
                                    <div class="Separator">
                                        &nbsp;
                                    </div>
                                    Per Page :
                                    <asp:DropDownList ID="ddlPerPage" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlPerPage_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="fclear">
                                </div>
                            </div>
                            <hr class="nomargin" />
                            <DInfo:DisplayInfo runat="server" ID="DInfo" />
                            <div class="panel-search">
                                <div class="table-responsive">
                                    <asp:GridView ID="dgvGridView" Width="100%" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="appTabID,appIsAdd,appIsEdit,appIsDelete,appIsActive,appDisplayOrder"
                                        CssClass="table table-striped table-bordered table-hover" HeaderStyle-Wrap="false"
                                        PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-Mode="NumericFirstLast"
                                        PageSize="10" OnPageIndexChanging="dgvGridView_PageIndexChanging" OnRowCreated="dgvGridView_RowCreated"
                                        OnRowDataBound="dgvGridView_RowDataBound" OnSorting="dgvGridView_Sorting" OnRowCommand="dgvGridView_RowCommand">
                                        <PagerSettings Position="Top" />
                                        <PagerStyle CssClass="pagination" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" onclick="SelectAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectRow" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%--<asp:ImageButton ID="imgbtnEdit" runat="server" ImageUrl="images/Edit.png" CommandName="Edit"
                                                        ToolTip="Edit" AlternateText=" " CommandArgument='<%#Eval("appTabId") %>' />--%>
                                                    <a href='<%# "TabDetail.aspx?ID=" + objEncrypt.Encrypt(Eval("appTabId").ToString(), appFunctions.strKey) %>'
                                                        title="Edit"><span class="action-icon set-icon"><i class="fa fa-pencil"></i></span>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:BoundField HeaderText="Tab Name" DataField="appTabName" SortExpression="appTabName"/>--%>
                                            <asp:TemplateField HeaderText="Tab Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%"
                                                HeaderStyle-Width="15%" SortExpression="appMenuItem">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkGetChildMenus" runat="server" Text='<%#Eval("appTabName") %>'
                                                        CommandName="ListChildMenus" CommandArgument='<%#Eval("appTabID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Form Name" DataField="appWebPageName" SortExpression="appWebPageName" />
                                            <asp:TemplateField HeaderText="Has Add" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%--<asp:ImageButton ID="imgbtnIsAdd" runat="server" ImageUrl="images/cross.png" CommandName="IsAdd"
                                                        ToolTip="IsAdd" AlternateText=" " CommandArgument='<%#Eval("appTabID") %>' />--%>
                                                    <asp:LinkButton runat="server" ID="lnkIsAdd" CommandName="IsAdd" CommandArgument='<%#Eval("appTabID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsAdd").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsAdd").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Has Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%--<asp:ImageButton ID="imgbtnIsEdit" runat="server" ImageUrl="images/cross.png" CommandName="IsEdit"
                                                        ToolTip="IsEdit" AlternateText=" " CommandArgument='<%#Eval("appTabID") %>' />--%>
                                                    <asp:LinkButton runat="server" ID="lnkIsEdit" CommandName="IsEdit" CommandArgument='<%#Eval("appTabID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsEdit").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsEdit").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Has Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%--<asp:ImageButton ID="imgbtnIsDelete" runat="server" ImageUrl="images/cross.png" CommandName="IsDelete"
                                                        ToolTip="IsDelete" AlternateText=" " CommandArgument='<%#Eval("appTabID") %>' />--%>
                                                    <asp:LinkButton runat="server" ID="lnkIsDelete" CommandName="IsDelete" CommandArgument='<%#Eval("appTabID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsDelete").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsDelete").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%--<asp:ImageButton ID="imgbtnIsActive" runat="server" ImageUrl="images/cross.png" CommandName="IsActive"
                                                        ToolTip="IsActive" AlternateText=" " CommandArgument='<%#Eval("appTabID") %>' />--%>
                                                    <asp:LinkButton runat="server" ID="lnkIsActive" CommandName="IsActive" CommandArgument='<%#Eval("appTabID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsActive").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsActive").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Show On Dashbord" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                  
                                                    <asp:LinkButton runat="server" ID="lnkIsDashbord" CommandName="IsDashbord" CommandArgument='<%#Eval("appTabID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsShowOnDashboard").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsShowOnDashboard").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Display Order" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="1%" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkUp" CommandName="Up" CommandArgument='<%#Eval("appTabID") %>' ToolTip="Up">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-up"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkDown" CommandName="Down" CommandArgument='<%#Eval("appTabID") %>' ToolTip="Down">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-down"></i></span>
                                                    </asp:LinkButton>
                                                    <%--<asp:ImageButton ID="imgbtnUp" runat="server" AlternateText="Up" CommandArgument='<%#Eval("appTabID") %>'
                                                        CommandName="Up" ImageUrl="images/up1.PNG" ToolTip="Up" />
                                                    <asp:ImageButton ID="imgbtnDown" runat="server" AlternateText="Down" CommandArgument='<%#Eval("appTabID") %>'
                                                        CommandName="Down" ImageUrl="images/down1.PNG" ToolTip="Down" />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:HiddenField ID="hdnPKID" runat="server" Value="" />
                                <asp:HiddenField ID="hdnSortDir" Value="Asc" runat="server" />
                                <asp:HiddenField ID="hdnSortCol" Value="" runat="server" />
                                <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
                                <asp:HiddenField ID="hdnCurrentTabID" Value="" runat="server" />
                                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="Search" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
