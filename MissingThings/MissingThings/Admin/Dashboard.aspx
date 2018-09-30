<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="_Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>
                Dashboard</h2>
            <h5>
                Welcome
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
                , Love to see you back.
            </h5>
        </div>
    </div>
    <!-- /. ROW  -->
    <hr />
    <asp:Repeater runat="server" ID="rptDashBoardLinks">
        <ItemTemplate>
            <div style="float: left; margin: 10Px; padding: 10Px; border: 1px solid #eee; width: 122Px;
                text-align: center; background-color: #f8f8f8;">
                <a style="display: block;" href="<%# Eval("appWebPageName")%>">
                    <div style="width: 100Px; height: 100Px; text-align: center; overflow: hidden">
                        <img src="<%# Eval("appIconPath").ToString()=="" ? "images/NoImg.png" : Eval("appIconPath").ToString()%>"
                            style="max-width: 100Px;" />
                    </div>
                    <div style="line-height: 20Px; height: 40Px;">
                        <%#Eval("appTabName") %>
                    </div>
                </a>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div style="clear: both;">
    </div>
    <%--<div class="row" id="DashBord" runat="server">
        <div class="col-md-1 col-sm-3 col-xs-3">
            sadsad
        </div>
        <div class="col-md-1 col-sm-3 col-xs-3">
            sadsad
        </div>
    </div>--%>
    <hr />
    <div class="row" id="divInquiry" runat="server">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Pending Inquiry List
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <hr class="nomargin" />
                            <div class="fclear">
                            </div>
                            </div>
                            <hr class="nomargin" />
                            <DInfo:DisplayInfo runat="server" ID="DisplayInfo1" />
                            <div class="panel-search">
                                <div class="table-responsive">
                                    <div class="table-responsive" style="height: 200px; margin: 0px;">
                                        <asp:GridView ID="dgvInInquiry" Width="100%" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="appInquiryID" CssClass="table table-striped table-bordered table-hover"
                                            HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                            PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvInInquiry_PageIndexChanging"
                                            OnRowCommand="dgvInInquiry_RowCommand">
                                            <PagerSettings Position="Top" />
                                            <Columns>
                                                <asp:BoundField DataField="appName" HeaderText="Name " SortExpression="appName" />
                                                <asp:BoundField DataField="appEmail" HeaderText="Email " SortExpression="appEmail" />
                                                <asp:BoundField DataField="appMobile" HeaderText="Mobile No " SortExpression="appMobile" />
                                                <asp:BoundField DataField="appMessage" HeaderText="Message" SortExpression="appMessage" />
                                                <asp:TemplateField HeaderText="Is Read" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                    HeaderStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkIsRead" CommandName="IsRead" CommandArgument='<%#Eval("appInquiryID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsRead").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsRead").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdnPKID" runat="server" Value="" />
                                        <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" EnableClientScript="true"
                                            HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                            ValidationGroup="Search" />
                                    </div>
                                </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <%--  <div class="col-md-6 col-sm-12 col-xs-12">
            <div class="panel panel-back noti-box">
                <span class="icon-box bg-color-blue"><i class="fa fa-warning"></i></span>
                <div class="text-box">
                    <p class="main-text">
                        Important Issues to Fix
                    </p>
                    <p class="text-muted">
                        Please fix these issues as soon as possible</p>
                    <p class="text-muted">
                        Time Left: 4 hrs</p>
                    <hr />
                    <p class="text-muted">
                        <span class="text-muted color-bottom-txt"><i class="fa fa-edit"></i>This is sample text
                            to get user idea, how original text will look like.</span>
                    </p>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-sm-12 col-xs-12">
            <div class="panel back-dash">
                <i class="fa fa-dashboard fa-3x"></i><strong>&nbsp; SPEED</strong>
                <p class="text-muted">
                    Lorem ipsum dolor sit amet, consectetur adipiscing sit ametsit amet elit ftr. Lorem
                    ipsum dolor sit amet, consectetur adipiscing elit.
                </p>
            </div>
        </div>
        <div class="col-md-3 col-sm-12 col-xs-12 ">
            <div class="panel ">
                <div class="main-temp-back">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-6">
                                <i class="fa fa-cloud fa-3x"></i>Newyork City
                            </div>
                            <div class="col-xs-6">
                                <div class="text-temp">
                                    10°
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-back noti-box">
                <span class="icon-box bg-color-green set-icon"><i class="fa fa-desktop"></i></span>
                <div class="text-box">
                    <p class="main-text">
                        Display</p>
                    <p class="text-muted">
                        Looking Good</p>
                </div>
            </div>
        </div>
    </div>--%>
    <!-- /. ROW  -->
    <%-- <div class="row">
        <div class="col-md-9 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Lead Chart
                </div>
                <div class="panel-body">
                    <div id="morris-bar-chart">
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-sm-12 col-xs-12">
            <div class="panel panel-primary text-center no-boder bg-color-green">
                <div class="panel-body">
                    <i class="fa fa-bar-chart-o fa-5x"></i>
                    <h3>
                        205
                    </h3>
                </div>
                <div class="panel-footer back-footer-green">
                    Ordered
                </div>
            </div>
            <div class="panel panel-primary text-center no-boder bg-color-red">
                <div class="panel-body">
                    <i class="fa fa-edit fa-5x"></i>
                    <h3>
                        158
                    </h3>
                </div>
                <div class="panel-footer back-footer-red">
                    Lead Rejected
                </div>
            </div>
        </div>
        <!-- /. ROW  -->
        <hr />
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12">
                <div class="panel panel-primary text-center no-boder bg-color-green">
                    <div class="panel-body">
                        <i class="fa fa-comments-o fa-5x"></i>
                        <h4>
                            200 New Comments
                        </h4>
                        <h4>
                            See All Comments
                        </h4>
                    </div>
                    <div class="panel-footer back-footer-green">
                        <i class="fa fa-rocket fa-5x"></i>Lorem ipsum dolor sit amet sit sit, consectetur
                        adipiscing elitsit sit gthn ipsum dolor sit amet ipsum dolor sit amet
                    </div>
                </div>
            </div>
            <%--   <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Recent User's Login
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive" style="height: 200px; margin: 0px;">
                            <asp:GridView runat="server" ID="dgvGridView" CssClass="table table-striped table-bordered table-hover"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="appFullName" HeaderText="Name" />
                                    <asp:BoundField DataField="appMobile" HeaderText="Mobile" />
                                    <asp:BoundField DataField="appEmail" HeaderText="Email" />
                                    <asp:BoundField DataField="appLastLoginTime" HeaderText="Last Login time" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <%--<div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>
                                        #
                                    </th>
                                    <th>
                                        First Name
                                    </th>
                                    <th>
                                        Last Name
                                    </th>
                                    <th>
                                        Username
                                    </th>
                                    <th>
                                        Date & Time
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        1
                                    </td>
                                    <td>
                                        First Name 1
                                    </td>
                                    <td>
                                        Last Name 1
                                    </td>
                                    <td>
                                        User 1
                                    </td>
                                    <td>
                                        7/10/2014 11:30 AM
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        2
                                    </td>
                                    <td>
                                        First Name 2
                                    </td>
                                    <td>
                                        Last Name 2
                                    </td>
                                    <td>
                                        User 2
                                    </td>
                                    <td>
                                        7/10/2014 12:30 AM
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        3
                                    </td>
                                    <td>
                                        First Name 3
                                    </td>
                                    <td>
                                        Last Name 3
                                    </td>
                                    <td>
                                        User 3
                                    </td>
                                    <td>
                                        8/10/2014 11:30 AM
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        4
                                    </td>
                                    <td>
                                        First Name 4
                                    </td>
                                    <td>
                                        Last Name 4
                                    </td>
                                    <td>
                                        User 4
                                    </td>
                                    <td>
                                        8/10/2014 10:30 AM
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        5
                                    </td>
                                    <td>
                                        First Name 5
                                    </td>
                                    <td>
                                        Last Name 5
                                    </td>
                                    <td>
                                        User 5
                                    </td>
                                    <td>
                                        7/10/2014 11:30 AM
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        6
                                    </td>
                                    <td>
                                        First Name 6
                                    </td>
                                    <td>
                                        Last Name 6
                                    </td>
                                    <td>
                                        User 6
                                    </td>
                                    <td>
                                        6/10/2014 12:30 AM
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
    </div> </div> </div> </div>
    <!-- /. ROW -->
    <div class="row">
        <div class="col-md-6 col-sm-12
    col-xs-12">
            <div class="chat-panel panel panel-default chat-boder chat-panel-head">
                <div class="panel-heading">
                    <i class="fa fa-comments fa-fw"></i>Chat Box
                    <div class="btn-group
    pull-right">
                        <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <ul class="dropdown-menu
    slidedown">
                            <li><a href="#"><i class="fa fa-refresh fa-fw"></i>Refresh </a></li>
                            <li><a href="#"><i class="fa fa-check-circle fa-fw"></i>Available </a></li>
                            <li><a href="#"><i class="fa fa-times fa-fw"></i>Busy </a></li>
                            <li><a href="#"><i class="fa
    fa-clock-o fa-fw"></i>Away </a></li>
                            <li class="divider"></li>
                            <li><a href="#"><i class="fa fa-sign-out fa-fw"></i>Sign Out </a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <ul class="chat-box">
                        <li class="left clearfix"><span class="chat-img pull-left">
                            <img src="assets/img/1.png" alt="User" class="img-circle" />
                        </span>
                            <div class="chat-body">
                                <strong>Jack Sparrow</strong> <small class="pull-right text-muted"><i class="fa
    fa-clock-o fa-fw"></i>12 mins ago </small>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                        <li class="right clearfix"><span class="chat-img pull-right">
                            <img src="assets/img/2.png" alt="User" class="img-circle" />
                        </span>
                            <div class="chat-body
    clearfix">
                                <small class=" text-muted"><i class="fa fa-clock-o fa-fw"></i>13 mins ago</small>
                                <strong class="pull-right">Jhonson Deed</strong>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                        <li class="left clearfix"><span class="chat-img
    pull-left">
                            <img src="assets/img/3.png" alt="User" class="img-circle" />
                        </span>
                            <div class="chat-body clearfix">
                                <strong>Jack Sparrow</strong> <small class="pull-right
    text-muted"><i class="fa fa-clock-o fa-fw"></i>14 mins ago</small>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                        <li class="right clearfix"><span class="chat-img
    pull-right">
                            <img src="assets/img/4.png" alt="User" class="img-circle" />
                        </span>
                            <div class="chat-body clearfix">
                                <small class=" text-muted"><i class="fa fa-clock-o
    fa-fw"></i>15 mins ago</small> <strong class="pull-right">Jhonson Deed</strong>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                        <li class="left
    clearfix"><span class="chat-img pull-left">
        <img src="assets/img/1.png" alt="User" class="img-circle" />
    </span>
                            <div class="chat-body">
                                <strong>Jack Sparrow</strong> <small class="pull-right text-muted"><i class="fa fa-clock-o fa-fw">
                                </i>12 mins ago </small>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                        <li class="right
    clearfix"><span class="chat-img pull-right">
        <img src="assets/img/2.png" alt="User" class="img-circle" />
    </span>
                            <div class="chat-body clearfix">
                                <small class=" text-muted"><i class="fa fa-clock-o fa-fw"></i>13 mins ago</small>
                                <strong class="pull-right">Jhonson Deed</strong>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="panel-footer">
                    <div class="input-group">
                        <input id="btn-input" type="text" class="form-control input-sm" placeholder="Type your message to send..." />
                        <span class="input-group-btn">
                            <button class="btn btn-warning btn-sm" id="btn-chat">
                                Send
                            </button>
                        </span>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6 col-sm-12
    col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Label Examples
                </div>
                <div class="panel-body">
                    <span class="label label-default">Default</span> <span class="label label-primary">Primary</span>
                    <span class="label label-success">Success</span> <span class="label label-info">Info</span>
                    <span class="label label-warning">Warning</span> <span class="label label-danger">Danger</span>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    Donut Chart Example
                </div>
                <div class="panel-body">
                    <div id="morris-donut-chart">
                    </div>
                </div>
            </div>
        </div>
    </div>
    --%>
    <!-- /. ROW -->
    <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
</asp:Content>
