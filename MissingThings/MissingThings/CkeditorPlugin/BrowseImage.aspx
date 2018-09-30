<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BrowseImage.aspx.vb" Inherits="CkeditorPlugin_BrowseImage" EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Browse Image</title>
    <link href="../Admin/CSS/layout.css" rel="stylesheet" type="text/css" />
    <link href="../Admin/CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Admin/CSS/cssUpdateProgress.css" rel="stylesheet" type="text/css" />
    <link href="../Admin/CSS/Default.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 10px;">
        <div class="mainbox" style="width: 100%;">
            <div class="main_title">
                Select Image from Gallery
            </div>
            <div align="center">
                <asp:DataList ID="dtImageGallery" runat="server" Width="100%" RepeatColumns="7">
                    <ItemTemplate>
                        <div class="newshome bodytypetrans boxshadowonhover mostpopularlogo" style="width: 120px">
                            <center>
                                <asp:HiddenField ID="hidImageName" runat="server" Value='<%# Eval("Name") %>' />
                                <%--<asp:ImageButton ID="IBSelect" runat="server" CommandName="Select" CommandArgument='<%# Eval("Name") %>' Width="150px" BorderStyle="None" 
                                      ImageUrl="<%=strServerURL%>Admin/Uploads/CKEditorImages/<%# Eval("Name") %>" />--%>
                                <%--<img src="<%=strServerURL%>Admin/Uploads/CKEditorImages/<%# Eval("Name") %>" width="150px" />--%>
                                <asp:ImageButton ID="imgbtn" runat="server" ImageUrl='<%# Eval("Name") %>' Width="120px" CommandName="Select" CommandArgument='<%# Eval("Name") %>' />
                            </center>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
