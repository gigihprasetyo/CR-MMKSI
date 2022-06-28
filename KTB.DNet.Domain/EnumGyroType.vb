Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class EnumGyroType
        Public Enum GyroType
            Normal = 0
            GantiGyro = 1
            Percepatan = 2
            Tolakan = 3
        End Enum

        Public Shared Function GetStringValue(ByVal GyroType As Integer) As String
            Dim str As String = ""
            If GyroType = 0 Then str = "Normal"
            If GyroType = 1 Then str = "Ganti Gyro"
            If GyroType = 2 Then str = "Percepatan TOP"
            If GyroType = 3 Then str = "Pembayaran Tolakan"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sGyroType As String) As Integer
            Dim Rsl As Integer = 0
            If sGyroType.ToUpper = "Normal".ToUpper Then Rsl = GyroType.Normal
            If sGyroType.ToUpper = "Ganti Gyro".ToUpper Then Rsl = GyroType.GantiGyro
            If sGyroType.ToUpper = "Percepatan TOP".ToUpper Then Rsl = GyroType.Percepatan
            If sGyroType.ToUpper = "Pembayaran Tolakan".ToUpper Then Rsl = GyroType.Tolakan
            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList

            arl.Add(New ListItem(GetStringValue(GyroType.Normal), GyroType.Normal))
            arl.Add(New ListItem(GetStringValue(GyroType.GantiGyro), GyroType.GantiGyro))
            arl.Add(New ListItem(GetStringValue(GyroType.Percepatan), GyroType.Percepatan))
            arl.Add(New ListItem(GetStringValue(GyroType.Tolakan), GyroType.Tolakan))
            Return arl
        End Function
    End Class
End Namespace
