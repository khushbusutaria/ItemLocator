<%@ Page Title="Role Detail" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true"
    CodeFile="RoleDetail.aspx.cs" Inherits="RoleDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .dtheader
        {
            /*font-size: 16px;
            font-weight: normal;
            padding-left: 8px;
            padding-right: 8px;
            border-width: 0px;
            border-color: #DE5521;
            border-style: solid;
            background-image: url(Images/button-tail.gif);
            background-repeat: repeat-x;
            background-color: #fff;
            height: 30px;
            color: #fff;*/
        }
        .DatalistChild1 {
/* border-bottom: 1px solid #DE5521; */
padding: 0 0 0 10Px;
}

.DatalistChild2 {
/* border-bottom: 1px solid #DE5521; */
padding: 0 0 0 10Px;
}

    .table td
    {
        padding:5Px 0 !important; 
    }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.Application.add_load(checkTDs); 
    </script>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Role Details
                </div>
                <div>
                    <div class="panel-search right-content">
                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" TabIndex="15"
                            OnClick="btnBack_Click" />
                        &nbsp;
                        <asp:Button ID="btnSaveAndClose" runat="server" ValidationGroup="save" CssClass="btn btn-primary"
                            Text="Save & Close" TabIndex="12" OnClick="btnSaveAndClose_Click" />
                        &nbsp;
                        <asp:Button ID="btnSaveAndAddnew" runat="server" ValidationGroup="save" CssClass="btn btn-primary"
                            Text="Save & Add New" TabIndex="13" OnClick="btnSaveAndAddnew_Click" />
                        &nbsp;
                        <asp:Button ID="btnClear" runat="server" CssClass="btn btn-primary" Text="Clear"
                            TabIndex="14" OnClick="btnClear_Click" />
                        &nbsp;
                    </div>
                    <hr class="nomargin" />
                    <DInfo:DisplayInfo runat="server" ID="DInfo" />
                    <div class="panel-search">
                        <div class="table-responsive">
                            <div class="entryformmain">
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Role Name :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtRoleName" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfRoleName" runat="server" ErrorMessage="Role Name"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtRoleName" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        Role Description :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <asp:DataList runat="server" ID="dlMain" DataKeyField="appTabID" CellPadding="0"
                                CellSpacing="0" Width="100%" OnItemDataBound="dlMain_ItemDataBound" CssClass="table table-bordered table-hover">
                                <HeaderTemplate>
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <th scope="col" class="dtheader">
                                                Tab Name
                                            </th>
                                            <th style="width:100Px; text-align: center;" scope="col" class="dtheader">
                                                Can View
                                            </th>
                                            <th style="width:100Px; text-align: center;" scope="col" class="dtheader">
                                                Can Add
                                            </th>
                                            <th style="width:100Px; text-align: center;" scope="col" class="dtheader">
                                                Can Edit
                                            </th>
                                            <th style="width:100Px; text-align: center;" scope="col" class="dtheader">
                                                Can Delete
                                            </th>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%" cellpadding="0" cellspacing="0" class="Datalistmain">
                                        <%--<tr>
                                                <td>
                                                </td>
                                                <td width="100">
                                                    All
                                                </td>
                                                <td width="100">
                                                    All
                                                </td>
                                                <td width="100">
                                                    All
                                                </td>
                                                <td width="100">
                                                    All
                                                </td>
                                            </tr>--%>
                                        <tr>
                                            <td style="padding-left: 10Px !important;">
                                                <%#Eval("appTabName")%>
                                            </td>
                                            <td width="100" runat="server" id="tdIsView" style="text-align: center;">
                                                <asp:CheckBox runat="server" ID="chkIsView" Checked='<%# Convert.ToBoolean(Eval("appIsView")) %>' />
                                            </td>
                                            <td width="100" id="tdIsAdd" runat="server" style="text-align: center;">
                                                <asp:CheckBox runat="server" ID="ChkIsAdd" Checked='<%# Convert.ToBoolean(Eval("appIsAdd")) %>' Visible='<%# Convert.ToBoolean(Eval("appIsTabAdd")) %>' />
                                            </td>
                                            <td width="100" id="tdIsEdit" runat="server" style="text-align: center;">
                                                <asp:CheckBox runat="server" ID="ChkIsEdit" Checked='<%# Convert.ToBoolean(Eval("appIsEdit")) %>' Visible='<%# Convert.ToBoolean(Eval("appIsTabEdit")) %>' />
                                            </td>
                                            <td width="100" id="tdIsDelete" runat="server" style="text-align: center;">
                                                <asp:CheckBox runat="server" ID="ChkIsDelete" Checked='<%# Convert.ToBoolean(Eval("appIsDelete")) %>'
                                                    Visible='<%# Convert.ToBoolean(Eval("appIsTabDelete")) %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="padding-left: 100Px !important;">
                                                <asp:DataList runat="server" ID="dlChild" Width="100%" DataKeyField="appTabID" OnItemDataBound="dlChild_ItemDataBound"
                                                    CellPadding="0" CellSpacing="0" CssClass="table table-bordered table-hover">
                                                    <ItemTemplate>
                                                        <table width="100%" cellpadding="0" cellspacing="0" class="DatalistChild1 ">
                                                            <tr>
                                                                <td style="padding-left: 10Px !important;">
                                                                    <%#Eval("appTabName")%>
                                                                </td>
                                                                <td width="100" runat="server" id="tdIsView" style="text-align: center;">
                                                                    <asp:CheckBox runat="server" ID="chkIsView" Checked='<%# Convert.ToBoolean(Eval("appIsView")) %>' />
                                                                </td>
                                                                <td width="100" runat="server" id="tdIsAdd" style="text-align: center;">
                                                                    <asp:CheckBox runat="server" ID="ChkIsAdd" Checked='<%# Convert.ToBoolean(Eval("appIsAdd")) %>' Visible='<%# Convert.ToBoolean(Eval("appIsTabAdd")) %>' />
                                                                </td>
                                                                <td width="100" id="tdIsEdit" runat="server" style="text-align: center;">
                                                                    <asp:CheckBox runat="server" ID="ChkIsEdit" Checked='<%# Convert.ToBoolean(Eval("appIsEdit")) %>' Visible='<%# Convert.ToBoolean(Eval("appIsTabEdit")) %>' />
                                                                </td>
                                                                <td width="100" id="tdIsDelete" runat="server" style="text-align: center;">
                                                                    <asp:CheckBox runat="server" ID="ChkIsDelete" Checked='<%# Convert.ToBoolean(Eval("appIsDelete")) %>'
                                                                        Visible='<%# Convert.ToBoolean(Eval("appIsTabDelete")) %>' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" style="padding-left: 100Px !important;">
                                                                    <asp:DataList runat="server" ID="dlChild2" Width="100%" CellPadding="0" CellSpacing="0"
                                                                        OnItemDataBound="dlChild2_ItemDataBound" DataKeyField="appTabID" CssClass="table table-bordered table-hover">
                                                                        <ItemTemplate>
                                                                            <table width="100%" cellpadding="0" cellspacing="0" class="DatalistChild2">
                                                                                <tr>
                                                                                    <td style="padding-left: 10Px !important;">
                                                                                        <%#Eval("appTabName")%>
                                                                                    </td>
                                                                                    <td width="100" runat="server" id="tdIsView" style="text-align: center;">
                                                                                        <asp:CheckBox runat="server" ID="chkIsView" Checked='<%# Convert.ToBoolean(Eval("appIsView")) %>' />
                                                                                    </td>
                                                                                    <td width="100" runat="server" id="tdIsAdd" style="text-align: center;">
                                                                                        <asp:CheckBox runat="server" ID="ChkIsAdd" Checked='<%# Convert.ToBoolean(Eval("appIsAdd")) %>' Visible='<%# Convert.ToBoolean(Eval("appIsTabAdd")) %>' />
                                                                                    </td>
                                                                                    <td width="100" id="tdIsEdit" runat="server" style="text-align: center;">
                                                                                        <asp:CheckBox runat="server" ID="ChkIsEdit" Checked='<%# Convert.ToBoolean(Eval("appIsEdit")) %>' Visible='<%# Convert.ToBoolean(Eval("appIsTabEdit")) %>' />
                                                                                    </td>
                                                                                    <td width="100" id="tdIsDelete" runat="server" style="text-align: center;">
                                                                                        <asp:CheckBox runat="server" ID="ChkIsDelete" Checked='<%# Convert.ToBoolean(Eval("appIsDelete")) %>'
                                                                                            Visible='<%# Convert.ToBoolean(Eval("appIsTabDelete")) %>' />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ItemTemplate>
                                                                    </asp:DataList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnPKID" runat="server" Value="" />
    <asp:HiddenField ID="hdnSortDir" Value="Asc" runat="server" />
    <asp:HiddenField ID="hdnSortCol" Value="" runat="server" />
    <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
    <asp:HiddenField ID="hdnType" Value="0" runat="server" />
    <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
        HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
        ValidationGroup="save" />
</asp:Content>
