Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class EnumGyroEntryType
        Public Enum EntryType
            Gyro = 1
            Transfer = 2
        End Enum

        Public Shared Function GetStringValue(ByVal EntryType As Integer) As String
            Dim str As String = ""
            If EntryType = 1 Then str = "Gyro"
            If EntryType = 2 Then str = "Transfer"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sEntryType As String) As Integer
            Dim Rsl As Integer = 0
            If sEntryType.ToUpper = EntryType.Gyro.ToString.ToUpper Then Rsl = EntryType.Gyro
            If sEntryType.ToUpper = EntryType.Transfer.ToString.ToUpper Then Rsl = EntryType.Transfer
            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList

            arl.Add(New ListItem(EntryType.Gyro.ToString, EntryType.Gyro))
            arl.Add(New ListItem(EntryType.Transfer.ToString, EntryType.Transfer))
            Return arl
        End Function
    End Class
End Namespace
