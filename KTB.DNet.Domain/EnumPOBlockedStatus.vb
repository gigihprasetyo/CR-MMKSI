Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class enumPOBlockedStatus
        Public Enum POBlockedStatus
            PassedAndConfirmed = 1
            PassedNotConfirmed = 2
            Blocked = 3
        End Enum

        Public Shared Function GetStringValue(ByVal BlockedStatus As Integer) As String
            Dim str As String = ""
            If BlockedStatus = 1 Then str = "PassedAndConfirmed"
            If BlockedStatus = 2 Then str = "PassedNotConfirmed"
            If BlockedStatus = 3 Then str = "Blocked"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sBlockedStatus As String) As Integer
            Dim Rsl As Integer = 0
            If sBlockedStatus.ToUpper = "PassedAndConfirmed".ToUpper Then Rsl = 1
            If sBlockedStatus.ToUpper = "PassedNotConfirmed".ToUpper Then Rsl = 2
            If sBlockedStatus.ToUpper = "Blocked".ToUpper Then Rsl = 3
            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList

            arl.Add(New ListItem("PassedAndConfirmed", 1))
            arl.Add(New ListItem("PassedNotConfirmed", 2))
            arl.Add(New ListItem("Blocked", 3))

            Return arl
        End Function



    End Class
End Namespace
