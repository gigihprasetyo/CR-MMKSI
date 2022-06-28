Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class enumFactoringStatus
        Public Enum FactoringStatus
            Active = 1
            InActive = 0
        End Enum

        Public Shared Function GetStringValue(ByVal FactoringStatus As Integer) As String
            Dim str As String = ""
            If FactoringStatus = enumFactoringStatus.FactoringStatus.InActive Then str = "Tidak Aktif"
            If FactoringStatus = enumFactoringStatus.FactoringStatus.Active Then str = "Aktif"

            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sFactoringStatus As String) As Integer
            Dim Rsl As Integer = 0
            If sFactoringStatus.ToUpper = "AKTIF" Then Rsl = enumFactoringStatus.FactoringStatus.Active
            If sFactoringStatus.ToUpper = "TIDAK AKTIF" Then Rsl = enumFactoringStatus.FactoringStatus.InActive
            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList

            arl.Add(New ListItem("Aktif", enumFactoringStatus.FactoringStatus.Active))
            arl.Add(New ListItem("Tidak Aktif", enumFactoringStatus.FactoringStatus.InActive))

            Return arl
        End Function
    End Class
End Namespace
