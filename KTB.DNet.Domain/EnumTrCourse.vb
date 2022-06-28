Namespace KTB.DNet.Domain
    Public Class EnumTrCourse
        Public Enum PaymentType
            FREE = 1
            CHARGE = 2
        End Enum

        Public Shared Function GetPaymentStringValue(ByVal Type As Integer) As String
            Dim str As String = ""
            If Type = 1 Then str = "FREE"
            If Type = 2 Then str = "CHARGE"
            Return str
        End Function
    End Class
End Namespace
