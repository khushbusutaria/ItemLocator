<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DisplayInfo.ascx.vb"
    Inherits="UserControls_DisplayInfo" %>
<style type="text/css">
    .Message
    {
        font-size: 14Px;
        font-weight: normal;
    }
    
    .information
    {
        background-color: #BDE5F8;
        border: solid 1Px #00529B;
        color: #00529B;
        padding: 3Px 0Px 3Px 0Px;
    }
    
    .warning
    {
        background-color: #FEEFB3;
        border: solid 1Px #9F6000;
        color: #9F6000;
        padding: 3Px 0Px 3Px 0Px;
    }
    
    .error
    {
        background-color: #FCAEAF;
        border: solid 1Px #DE2630;
        color: #DE2630;
        padding: 3Px 0Px 3Px 0Px;
    }
    
    .successfull
    {
        background-color: #DFF2BF;
        border: solid 1Px #4F8A10;
        color: #4F8A10;
        padding: 3Px 0Px 3Px 0Px;
    }
</style>
<div style="margin: 5Px;" runat="server" id="tblMsg" enableviewstate="false">
    <div style="width: 100%; text-align: center; -moz-box-sizing: border-box">
        <asp:Label runat="server" CssClass="Message" ID="lblDisplayInfo" EnableViewState="false"></asp:Label>
    </div>
</div>
<%--<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td align="center" valign="middle" id="tdMsg" runat="server" enableviewstate="false">
        </td>
    </tr>
</table>--%>
