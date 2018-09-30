<%@ WebHandler Language="VB" Class="UploadHandler" %>

Imports System
Imports System.Web


Public Class UploadHandler : Implements IHttpHandler

    Dim fileAppend As String = DateTime.Now().Date.Day.ToString() + "_" + DateTime.Now().Date.Month.ToString() + DateTime.Now().Date.Year.ToString() + DateTime.Now().TimeOfDay.Hours.ToString() + "_" + DateTime.Now().TimeOfDay.Minutes.ToString() + DateTime.Now().TimeOfDay.Seconds.ToString() + "_"
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim uploads As HttpPostedFile
        uploads = context.Request.Files("upload")
        Dim fileName As String = System.IO.Path.GetFileNameWithoutExtension(uploads.FileName).ToString()
        Dim fileExtension As String = System.IO.Path.GetExtension(uploads.FileName).ToString()
        Dim uploadedFileName = fileName + "_" + fileAppend + fileExtension
        Dim CKEditorFuncNum As String = context.Request("CKEditorFuncNum")
        uploads.SaveAs(context.Server.MapPath(".") + "\Uploads\CKEditorImages\" + uploadedFileName)
        
        Dim imageUrl As String = PageBase.GetServerURL() + "/Uploads/CKEditorImages/" + uploadedFileName
        context.Response.Write("<script type='text/javascript'>window.parent.CKEDITOR.tools.callFunction('" + CKEditorFuncNum + "','\" + imageUrl + "');</script>")
        context.Response.End()
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class