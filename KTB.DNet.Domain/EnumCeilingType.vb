Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class enumCeilingType
        Public Enum CeilingType
            ProposedPO = 1
            LiquifiedPO = 2
            Outstanding = 3
        End Enum

        Public Shared Function GetStringValue(ByVal CeilingType As Integer) As String
            Dim str As String = ""
            If CeilingType = 1 Then str = "ProposedPO"
            If CeilingType = 2 Then str = "LiquifiedPO"
            If CeilingType = 3 Then str = "Outstanding"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sCeilingType As String) As Integer
            Dim Rsl As Integer = 0
            If sCeilingType.ToUpper = "ProposedPO" Then Rsl = 1
            If sCeilingType.ToUpper = "LiquifiedPO" Then Rsl = 2
            If sCeilingType.ToUpper = "Outstanding" Then Rsl = 3
            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList

            arl.Add(New ListItem("ProposedPO", 1))
            arl.Add(New ListItem("LiquifiedPO", 2))
            arl.Add(New ListItem("Outstanding", 3))

            Return arl
        End Function



    End Class
End Namespace
