Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class enumSPAFUpload
        Public Enum SPAFUpload
            Initialization = 0
            Finish = 1
        End Enum

        Public Shared Function GetStringValue(ByVal SPAFUpload As Integer) As String
            Dim str As String = ""
            If SPAFUpload = 0 Then str = "Initialization"
            If SPAFUpload = 1 Then str = "Finish"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sSPAFUpload As String) As Integer
            Dim Rsl As Integer = 0
            If sSPAFUpload.ToUpper = "Initialization".ToUpper Then Rsl = 0
            If sSPAFUpload.ToUpper = "Finish".ToUpper Then Rsl = 1
            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList

            arl.Add(New ListItem("Initialization", 0))
            arl.Add(New ListItem("Finish", 1))

            Return arl
        End Function



    End Class
End Namespace
