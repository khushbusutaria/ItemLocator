<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExportFile.ascx.cs" Inherits="UserControls_ExportFile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
 <style type="text/css">
        .MoveBtn
        {
            padding: 5px;
        }
    </style>
<input type="button" runat="server" id="btnPopExport" style="display: none;" />
<cc1:ModalPopupExtender ID="mpeExport" runat="server" TargetControlID="btnPopExport"
    PopupControlID="divExport" BackgroundCssClass="modalbackground" DropShadow="false"
    CancelControlID="imgbtnDueDeligenceClose">
</cc1:ModalPopupExtender>
<div id="divExport" class="modalpopup_ panel panel-default" runat="server" style="display: none;
    width: 600px;">
    <asp:LinkButton ID="imgbtnDueDeligenceClose" CssClass="modalbclose" runat="server">
                                        <span class="action-icon set-icon"><i class="fa fa-minus"></i></span>
    </asp:LinkButton>
    <div class="modalheader_ panel-heading">
        Export
    </div>
    <div class="modaldetail">
        <div class="panel-search">
            <asp:UpdatePanel runat="server" ID="upDueDate">
                <ContentTemplate>
                    <div class="panel-search right-content">
                        <DInfo:DisplayInfo runat="server" ID="DInfoExport" />
                    </div>
                    <div class="panel-search">
                        <div class="table-responsive">
                            <div class="fleft" style="width: 40%; text-align: center;">
                                UnSelected Columns
                            </div>
                            <div class="fleft" style="width: 10%; text-align: center;">
                                &nbsp;
                            </div>
                            <div class="fleft" style="width: 40%; text-align: center;">
                                Selected Columns
                            </div>
                            <div class="fclear">
                            </div>
                        </div>
                        <hr class="nomargin" />
                        <div class="table-responsive" style="margin-top: 5px;">
                            <div class="fleft" style="width: 40%; text-align: center;">
                                <asp:ListBox ID="lstAllColumn" runat="server" Height="150px" SelectionMode="Multiple"
                                    Width="150px" CssClass="LSTStyle" TabIndex="14"></asp:ListBox>
                            </div>
                            <div class="fleft" style="width: 10%; text-align: center;">
                                <div class="MoveBtn">
                                    <asp:LinkButton ID="lnkAllRight" runat="server" ToolTip="Move All Columns To Right"
                                        OnClick="lnkAllRight_Click">
                                                                     <span class="action-icon set-icon"><i class="fa fa-angle-double-right"></i></span>
                                    </asp:LinkButton>
                                </div>
                                <div class="MoveBtn">
                                    <asp:LinkButton ID="lnkRight" runat="server" ToolTip="Move Selected Columns To Left"
                                        OnClick="lnkRight_Click">
                                                                     <span class="action-icon set-icon"><i class="fa fa-angle-right"></i></span>
                                    </asp:LinkButton>
                                </div>
                                <div class="MoveBtn">
                                    <asp:LinkButton ID="lnkLeft" runat="server" ToolTip="Move Selected Columns To Right"
                                        OnClick="lnkLeft_Click">
                                                                     <span class="action-icon set-icon"><i class="fa fa-angle-left"></i></span>
                                    </asp:LinkButton>
                                </div>
                                <div class="MoveBtn">
                                    <asp:LinkButton ID="lnkAllLeft" runat="server" ToolTip="Move All Columns To Left"
                                        OnClick="lnkAllLeft_Click">
                                                                     <span class="action-icon set-icon"><i class="fa fa-angle-double-left"></i></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="fleft" style="width: 40%; text-align: center;">
                                <asp:ListBox ID="lstSelectedColumn" CssClass="LSTStyle" runat="server" Height="150px"
                                    SelectionMode="Multiple" Width="150px" TabIndex="19"></asp:ListBox>
                            </div>
                            <div class="fclear">
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <hr class="nomargin" />
            <div class="panel-search" style="text-decoration: none; text-align: Center;">
                <asp:LinkButton ID="lnkExcel" runat="server" ToolTip="Export Excel" Width="100" OnClick="lnkExcel_Click">
                                                            <div style="text-decoration:none;text-align:Center;"><i class="fa fa-file-text-o fa-5x"></i></div>
                                                            <div style="text-decoration:none;text-align:Center;"> Excel</div>
                </asp:LinkButton>
                <asp:LinkButton ID="lnkPdf" runat="server" ToolTip="Export PDF" Width="100" OnClick="lnkPdf_Click">
                                                            <div style="text-decoration:none;text-align:Center;"><i class="fa fa-file-text-o fa-5x"></i></div>
                                                            <div style="text-decoration:none;text-align:Center;"> Pdf</div>
                </asp:LinkButton>
                <asp:LinkButton ID="lnkCsv" runat="server" ToolTip="Export CSV" Width="100" OnClick="lnkCsv_Click">
                                                            <div style="text-decoration:none;text-align:Center;"><i class="fa fa-file-text-o fa-5x"></i></div>
                                                            <div style="text-decoration:none;text-align:Center;"> CSV</div>
                </asp:LinkButton>
            </div>
        </div>
    </div>
</div>
