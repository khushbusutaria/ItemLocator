Imports System.IO
Imports System.IO.DirectoryInfo
Imports System.Web.HttpRequest

Partial Class CkeditorPlugin_BrowseImage
    Inherits System.Web.UI.Page
    Public strServerURL As String = PageBase.GetServerURL() & "/"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call LoadDataList()
        End If
    End Sub
    Public Sub LoadDataList()
        Dim dr As DirectoryInfo = New DirectoryInfo(Server.MapPath("~/Uploads/CKEditorImages"))
        Dim file As FileInfo() = dr.GetFiles()
        Dim List As ArrayList = New ArrayList()
        For Each F In file
            If F.Extension = ".jpg" Or F.Extension = ".jpeg" Or F.Extension = ".gif" Or F.Extension = ".png" Then
                List.Add(F)
            End If
        Next
        dtImageGallery.DataSource = List
        dtImageGallery.DataBind()

    End Sub

    Protected Sub dtImageGallery_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dtImageGallery.ItemCommand
        If e.CommandName = "Select" Then
            Dim StrUrl As String = strServerURL + "Uploads/CKEditorImages/" + e.CommandArgument.ToString()
            Response.Write("<script>window.opener.CKEDITOR.tools.callFunction(" + Request.QueryString.Get("CKEditorFuncNum") + ", """ + StrUrl + """);window.self.close();</script>")
            Response.End()
        End If
    End Sub

    Protected Sub dtImageGallery_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dtImageGallery.ItemDataBound
        Dim itemtype As DataControlRowType = e.Item.ItemType
        Select Case itemtype
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim img As ImageButton = e.Item.FindControl("imgbtn")
                img.ImageUrl = strServerURL + "Uploads/CKEditorImages/" + img.ImageUrl
        End Select
    End Sub
End Class
