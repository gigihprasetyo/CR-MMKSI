Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class EnumSparePartActiveStatus
        Public Enum SparePartActiveStatus
            Active = 0
            InActive = 1
        End Enum

        Public Shared Function GetStringValue(ByVal ActiveStatus As Integer) As String
            Dim str As String = ""
            If ActiveStatus = 0 Then str = "Aktif"
            If ActiveStatus = 1 Then str = "Tidak Aktif"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sActiveStatus As String) As Integer
            Dim Rsl As Integer = 0
            If sActiveStatus.ToUpper = "Aktif".ToUpper Then Rsl = 0
            If sActiveStatus.ToUpper = "Tidak Aktif".ToUpper Then Rsl = 1
            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList

            arl.Add(New ListItem("Aktif", 0))
            arl.Add(New ListItem("Tidak Aktif", 1))

            Return arl
        End Function



    End Class
End Namespace
