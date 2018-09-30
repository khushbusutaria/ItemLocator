<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login | Jet API</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <!-- BOOTSTRAP STYLES-->
    <link href="css/bootstrap.css" rel="stylesheet" />
    <!-- FONTAWESOME STYLES-->
    <link href="css/font-awesome.css" rel="stylesheet" />
    <!-- MORRIS CHART STYLES-->
    <link href="JS/morris/morris-0.4.3.min.css" rel="stylesheet" />
    <!-- CUSTOM STYLES-->
    <link href="css/custom.css" rel="stylesheet" />
    <!-- GOOGLE FONTS-->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <nav class="navbar navbar-default navbar-cls-top " role="navigation" style="margin-bottom: 0">
            <div class="navbar-header">
                <%--<button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".sidebar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>--%>
                <a class="navbar-brand" href="#">Jet API</a> 
            </div>
              <div style="color: white;
            padding: 15px 15px 13px;
            float: right;
            font-size: 16px;"> Welcome, Guest</div>
        </nav>
            <div class="row">
                <div style="margin: 200Px auto; float: none; width: 405Px;">
                    <!-- Form Elements -->
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-user fa-1x"></i>&nbsp;User Login
                        </div>
                        <div class="panel-body" style="padding: 30Px;">
                            <div class="form-group input-group">
                                <span class="input-group-addon txt-lbl-width-130"><i class="fa fa-envelope-o fa-1x">
                                </i>&nbsp;User Name</span>
                                <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" Width="200"
                                    Style="margin: 0 !important;"></asp:TextBox>&nbsp;
                                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="User Name"
                                    Text="*" ControlToValidate="txtUserName" CssClass="ErrorLabelStyle" ValidationGroup="Login"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group input-group">
                                <span class="input-group-addon txt-lbl-width-130"><i class="fa fa-key fa-1x"></i>&nbsp;Password</span>
                                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control"
                                    Width="200" Style="margin: 0 !important;"></asp:TextBox>&nbsp;
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Password"
                                    Text="*" ControlToValidate="txtPassword" CssClass="ErrorLabelStyle" ValidationGroup="Login"></asp:RequiredFieldValidator>
                            </div>
                            <div>
                                <div class="fleft">
                                    <a href="ForgetPassword.aspx">Forgot password?</a>
                                </div>
                                <div class="fright">
                                    <asp:CheckBox ID="chkRemeber" Text="" runat="server" />
                                    Remember&nbsp;<asp:Button runat="server" ID="btnLogin" CssClass="btn btn-success"
                                        Text="Login" ValidationGroup="Login" OnClick="btnLogIn_Click" />
                                    <%--<asp:Button runat="server" ID="btnCancel" CssClass="btn btn-default" Text="Cancel"
                                        CausesValidation="false" OnClick="btnCancel_Click" />--%>
                                </div>
                                <div class="fclear">
                                </div>
                                <asp:Label ID="lblMsg" runat="server" Text="" Font-Size="14px" ForeColor="Red" Font-Bold="false"></asp:Label>
                            </div>
                            <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                                HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                ValidationGroup="Login" />
                        </div>
                    </div>
                    <!-- End Form Elements -->
                </div>
            </div>
            <!-- /. PAGE WRAPPER  -->
        </div>
        <!-- /. WRAPPER  -->
        <!-- SCRIPTS -AT THE BOTOM TO REDUCE THE LOAD TIME-->
        <!-- JQUERY SCRIPTS -->
        <script src="js/jquery-1.10.2.js"></script>
        <!-- BOOTSTRAP SCRIPTS -->
        <script src="js/bootstrap.min.js"></script>
        <!-- METISMENU SCRIPTS -->
        <script src="js/jquery.metisMenu.js"></script>
        <!-- MORRIS CHART SCRIPTS -->
        <script src="js/morris/raphael-2.1.0.min.js"></script>
        <script src="js/morris/morris.js"></script>
        <!-- CUSTOM SCRIPTS -->
        <script src="js/custom.js"></script>
    </div>
    </form>
</body>
</html>
