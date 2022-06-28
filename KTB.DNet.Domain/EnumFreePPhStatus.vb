Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class enumFreePPhStatus
        Public Enum FreePPhStatus
            None = 0
            Approved = 1
            Rejected = 2
        End Enum

        Public Shared Function GetStringValue(ByVal PaymentType As Integer) As String
            Dim str As String = ""
            If PaymentType = 0 Then str = "None"
            If PaymentType = 1 Then str = "Approved"
            If PaymentType = 2 Then str = "Rejected"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sPaymentType As String) As Integer
            Dim Rsl As Integer = 0
            If sPaymentType.ToUpper = "None" Then Rsl = 0
            If sPaymentType.ToUpper = "Approved" Then Rsl = 1
            If sPaymentType.ToUpper = "Rejected" Then Rsl = 2
            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList

            arl.Add(New ListItem("None", 0))
            arl.Add(New ListItem("Approved", 1))
            arl.Add(New ListItem("Rejected", 2))

            Return arl
        End Function



    End Class
End Namespace
