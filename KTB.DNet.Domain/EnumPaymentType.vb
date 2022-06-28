Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class enumPaymentType
        Public Enum PaymentType
            COD = 1
            TOP = 2
            RTGS = 3
        End Enum

        Public Shared Function GetStringValue(ByVal PaymentType As Integer) As String
            Dim str As String = ""
            If PaymentType = 1 Then str = "COD"
            If PaymentType = 2 Then str = "TOP"
            If PaymentType = 3 Then str = "RTGS"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sPaymentType As String) As Integer
            Dim Rsl As Integer = 0
            If sPaymentType.ToUpper = "COD" Then Rsl = 1
            If sPaymentType.ToUpper = "TOP" Then Rsl = 2
            If sPaymentType.ToUpper = "RTGS" Then Rsl = 3
            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList

            arl.Add(New ListItem("COD", 1))
            arl.Add(New ListItem("TOP", 2))
            arl.Add(New ListItem("RTGS", 3))

            Return arl
        End Function



    End Class
End Namespace
