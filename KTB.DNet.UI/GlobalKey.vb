Imports KTB.DNet.Utility

Namespace KTB.DNet.UI
    Public Class GlobalKey
        Private Shared _sessionHelper As New SessionHelper
        Public Shared Function IsKeyValid(ByVal QueryString As String) As Boolean
            If QueryString = _sessionHelper.GetSession("KEY") Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shared Function GenerateKey() As String
            Dim Key As String = DateTime.Now.Month & DateTime.Now.Millisecond & DateTime.Now.Day & DateTime.Now.Year & DateTime.Now.Minute & DateTime.Now.Hour & DateTime.Now.Second
            _sessionHelper.SetSession("KEY", Key)
            Return Key
        End Function
    End Class
End Namespace

