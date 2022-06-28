Namespace KTB.DNet.UI.Helper
    <Serializable()> _
    Public Class DonwloadFile
        Private _url As String
        Private _respon As HttpResponse
        Public Sub New(ByVal url As String, ByVal respon As HttpResponse)
            _url = url
            _respon = respon
        End Sub

        Public Sub RedirectURL()
            _respon.Redirect(_url)
        End Sub
    End Class

End Namespace