Imports BusinessLayer
Partial Class UserControls_DisplayInfo
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub ShowMessage(ByVal strMessage As String, ByVal objMessageType As Enums.MessageType)
        lblDisplayInfo.Text = strMessage

        Select Case objMessageType
            Case Enums.MessageType.Information
                tblMsg.Attributes.Add("class", "information")
                'imgMessage.Src = "../images/information.png"
            Case Enums.MessageType.Error
                tblMsg.Attributes.Add("class", "error")
                'imgMessage.Src = "../images/exclamation.png"
            Case Enums.MessageType.Successfull
                tblMsg.Attributes.Add("class", "successfull")
                'imgMessage.Src = "../images/tick.png"
            Case Enums.MessageType.Warning
                tblMsg.Attributes.Add("class", "warning")
                'imgMessage.Src = "../images/error.png"
        End Select
    End Sub

 
End Class
