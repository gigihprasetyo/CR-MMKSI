Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class EnumPaymentRemarkStatus
        Public Enum PaymentRemarkStatus
            NotCleared = 0
            Cleared = 1
            PT = 2
            PTOffset = 3
            RejectPaid = 4
            Reject = 5
        End Enum

        Public Shared Function GetStringValue(ByVal PaymentRemarkStatus As Integer) As String
            Dim str As String = ""
            If PaymentRemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.NotCleared Then str = "Not Cleared" ' EnumPaymentRemarkStatus.PaymentRemarkStatus.NotCleared.ToString
            If PaymentRemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.Cleared Then str = "Cleared" 'EnumPaymentRemarkStatus.PaymentRemarkStatus.Cleared.ToString
            If PaymentRemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.PT Then str = "Pre-Termination" ' EnumPaymentRemarkStatus.PaymentRemarkStatus.PT.ToString
            If PaymentRemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.PTOffset Then str = "PT Offset" ' EnumPaymentRemarkStatus.PaymentRemarkStatus.PTOffset.ToString
            If PaymentRemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.RejectPaid Then str = "Reject-Paid" 'EnumPaymentRemarkStatus.PaymentRemarkStatus.RejectPaid.ToString
            If PaymentRemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.Reject Then str = "Reject" ' EnumPaymentRemarkStatus.PaymentRemarkStatus.Reject.ToString
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sPaymentRemarkStatus As String) As Integer
            Dim Rsl As Integer = 0
            If sPaymentRemarkStatus.ToUpper = EnumPaymentRemarkStatus.GetStringValue(EnumPaymentRemarkStatus.PaymentRemarkStatus.NotCleared).ToUpper Then Rsl = EnumPaymentRemarkStatus.PaymentRemarkStatus.NotCleared
            If sPaymentRemarkStatus.ToUpper = EnumPaymentRemarkStatus.GetStringValue(EnumPaymentRemarkStatus.PaymentRemarkStatus.Cleared).ToUpper Then Rsl = EnumPaymentRemarkStatus.PaymentRemarkStatus.Cleared
            If sPaymentRemarkStatus.ToUpper = EnumPaymentRemarkStatus.GetStringValue(EnumPaymentRemarkStatus.PaymentRemarkStatus.PT).ToUpper Then Rsl = EnumPaymentRemarkStatus.PaymentRemarkStatus.PT
            If sPaymentRemarkStatus.ToUpper = EnumPaymentRemarkStatus.GetStringValue(EnumPaymentRemarkStatus.PaymentRemarkStatus.PTOffset).ToUpper Then Rsl = EnumPaymentRemarkStatus.PaymentRemarkStatus.PTOffset
            If sPaymentRemarkStatus.ToUpper = EnumPaymentRemarkStatus.GetStringValue(EnumPaymentRemarkStatus.PaymentRemarkStatus.RejectPaid).ToUpper Then Rsl = EnumPaymentRemarkStatus.PaymentRemarkStatus.RejectPaid
            If sPaymentRemarkStatus.ToUpper = EnumPaymentRemarkStatus.GetStringValue(EnumPaymentRemarkStatus.PaymentRemarkStatus.Reject).ToUpper Then Rsl = EnumPaymentRemarkStatus.PaymentRemarkStatus.Reject
            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList

            arl.Add(New ListItem(EnumPaymentRemarkStatus.GetStringValue(EnumPaymentRemarkStatus.PaymentRemarkStatus.NotCleared), EnumPaymentRemarkStatus.PaymentRemarkStatus.NotCleared))
            arl.Add(New ListItem(EnumPaymentRemarkStatus.GetStringValue(EnumPaymentRemarkStatus.PaymentRemarkStatus.Cleared), EnumPaymentRemarkStatus.PaymentRemarkStatus.Cleared))
            arl.Add(New ListItem(EnumPaymentRemarkStatus.GetStringValue(EnumPaymentRemarkStatus.PaymentRemarkStatus.PT), EnumPaymentRemarkStatus.PaymentRemarkStatus.PT))
            arl.Add(New ListItem(EnumPaymentRemarkStatus.GetStringValue(EnumPaymentRemarkStatus.PaymentRemarkStatus.PTOffset), EnumPaymentRemarkStatus.PaymentRemarkStatus.PTOffset))
            arl.Add(New ListItem(EnumPaymentRemarkStatus.GetStringValue(EnumPaymentRemarkStatus.PaymentRemarkStatus.RejectPaid), EnumPaymentRemarkStatus.PaymentRemarkStatus.RejectPaid))
            arl.Add(New ListItem(EnumPaymentRemarkStatus.GetStringValue(EnumPaymentRemarkStatus.PaymentRemarkStatus.Reject), EnumPaymentRemarkStatus.PaymentRemarkStatus.Reject))
            Return arl
        End Function
    End Class
End Namespace
