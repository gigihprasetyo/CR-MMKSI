Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class EnumPaymentStatus
        Public Enum PaymentStatus
            Baru = 0
            Validasi = 1
            Selesai = 2
        End Enum

        Public Shared Function GetStringValue(ByVal PaymentStatus As Integer) As String
            Dim str As String = ""
            If PaymentStatus = EnumPaymentStatus.PaymentStatus.Baru Then str = EnumPaymentStatus.PaymentStatus.Baru.ToString
            If PaymentStatus = EnumPaymentStatus.PaymentStatus.Validasi Then str = EnumPaymentStatus.PaymentStatus.Validasi.ToString
            If PaymentStatus = EnumPaymentStatus.PaymentStatus.Selesai Then str = EnumPaymentStatus.PaymentStatus.Selesai.ToString
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sPaymentStatus As String) As Integer
            Dim Rsl As Integer = 0
            If sPaymentStatus.ToUpper = EnumPaymentStatus.PaymentStatus.Baru.ToString.ToUpper Then Rsl = EnumPaymentStatus.PaymentStatus.Baru
            If sPaymentStatus.ToUpper = EnumPaymentStatus.PaymentStatus.Validasi.ToString.ToUpper Then Rsl = EnumPaymentStatus.PaymentStatus.Validasi
            If sPaymentStatus.ToUpper = EnumPaymentStatus.PaymentStatus.Selesai.ToString.ToUpper Then Rsl = EnumPaymentStatus.PaymentStatus.Selesai
            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList

            arl.Add(New ListItem(EnumPaymentStatus.PaymentStatus.Baru.ToString, EnumPaymentStatus.PaymentStatus.Baru))
            arl.Add(New ListItem(EnumPaymentStatus.PaymentStatus.Validasi.ToString, EnumPaymentStatus.PaymentStatus.Validasi))
            arl.Add(New ListItem(EnumPaymentStatus.PaymentStatus.Selesai.ToString, EnumPaymentStatus.PaymentStatus.Selesai))
            Return arl
        End Function
    End Class
End Namespace
