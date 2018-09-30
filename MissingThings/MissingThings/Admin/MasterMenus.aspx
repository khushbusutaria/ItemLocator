<%@ Page Title="Tab List" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true"
    CodeFile="MasterMenus.aspx.cs" Inherits="MasterMenus" %>

<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Menu Type List
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="panel-search">
                                Select Criteria :
                                <asp:DropDownList runat="server" ID="ddlFields" CssClass="form-control" TabIndex="3">
                                    <asp:ListItem Text="MenuType Name" Value="appMenuTypeName"></asp:ListItem>
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
                                        OnClientClick="return ConfirmMessage('Menu','delete')" OnClick="btnDelete_Click" />
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
                                        AllowSorting="true" DataKeyNames="appMenuTypeId,appIsActive,appChildCount" CssClass="table table-striped table-bordered table-hover"
                                        HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                        PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvGridView_PageIndexChanging"
                                        OnRowCreated="dgvGridView_RowCreated" OnRowDataBound="dgvGridView_RowDataBound"
                                        OnSorting="dgvGridView_Sorting" OnRowCommand="dgvGridView_RowCommand">
                                        <PagerSettings Position="Top" />
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
                                                    <a href='<%# "MasterMenusDetail.aspx?ID=" + objEncrypt.Encrypt(Eval("appMenuTypeId").ToString(), appFunctions.strKey) %>'
                                                        title="Edit"><span class="action-icon set-icon"><i class="fa fa-pencil"></i></span>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField HeaderText="MenuType Name" DataField="appMenuTypeName" />--%>
                                            <asp:TemplateField HeaderText="MenuType Name" ItemStyle-CssClass="row" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="40%" HeaderStyle-Width="40%" SortExpression="appMenuTypeName">
                                                <ItemTemplate>
                                                    <a href='<%# "MenuItems.aspx?ID=" + objEncrypt.Encrypt(Eval("appMenuTypeId").ToString(), appFunctions.strKey) + "&type=mtype" %>'
                                                        title="MenuType Name">
                                                        <%#Eval("appMenuTypeName") %></a>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40%" />
                                                <ItemStyle CssClass="row" HorizontalAlign="Left" Width="40%" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Maximum No. Of Level" DataField="appNoOfLevel" SortExpression="appNoOfLevel">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Child Menu Items" ItemStyle-CssClass="row" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <a href='<%# "MenuItems.aspx?ID=" + objEncrypt.Encrypt(Eval("appMenuTypeId").ToString(), appFunctions.strKey) + "&type=mtype" %>'
                                                        title="Child View">
                                                        <asp:Label ID="lblSubCategory" runat="server" Font-Bold="true" Width="50" Text='<%#Eval("appChildCount") %>'></asp:Label></a>
                                                </ItemTemplate>
                                                <HeaderStyle Width="1%" />
                                                <ItemStyle CssClass="row" HorizontalAlign="Center" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Add New" ItemStyle-CssClass="row" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="1%" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <a href='<%# "MenuItemsDetail.aspx?TID=" + objEncrypt.Encrypt(Eval("appMenuTypeId").ToString(), appFunctions.strKey)+" &Page=MasterMenus.aspx" %>'
                                                        title="Add New"><span class="action-icon set-icon"><i class="fa fa-plus"></i></span>
                                                    </a>
                                                </ItemTemplate>
                                                <HeaderStyle Width="1%" />
                                                <ItemStyle CssClass="row" HorizontalAlign="Center" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkIsActive" CommandName="IsActive" CommandArgument='<%#Eval("appMenuTypeId") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsActive").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsActive").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                    </asp:LinkButton>
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
