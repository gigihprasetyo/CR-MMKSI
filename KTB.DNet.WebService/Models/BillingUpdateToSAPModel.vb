Public Class BillingUpdateToSAPModel
    Public Sub New()
    End Sub
    Public Property Message As String
    Public Property Status As Integer
    Public Property ObjectResult As Object
End Class

Public Class BillingUpdate
    Public Sub New()
    End Sub
    Public Property BillingNo As String
    Public Property AccountingNo As String
    Public Property TransferDate As String
    Public Property TaxNo As String
    Public Property Text As String
    Public Property BankAccount As String
End Class