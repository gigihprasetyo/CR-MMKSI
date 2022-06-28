Namespace KTB.DNet.Domain

    Public Class EnumPaymentObligation
        Public Enum PaymentObligationStatus
            Baru = 0
            Validasi1 = 1
            Validasi2 = 2
        End Enum

        Public Function PaymentObligationStatusDesc(ByVal _status As Integer) As String
            Select Case _status
                Case 0
                    Return "Baru"
                Case 1
                    Return "Validasi 1"
                Case 2
                    Return "Validasi 2"
                Case Else
                    Return String.Empty
            End Select
        End Function

        Public Function RetrievePaymentObligationStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumObligationTypeStatus
            sts = New EnumObligationTypeStatus(0, "Baru")
            al.Add(sts)
            sts = New EnumObligationTypeStatus(1, "Validasi 1")
            al.Add(sts)
            sts = New EnumObligationTypeStatus(2, "Validasi 2")
            al.Add(sts)
            Return al
        End Function
    End Class

    Public Class EnumPaymentObligationStatus
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public ReadOnly Property ValStatus() As Integer
            Get
                Return _val
            End Get
        End Property

        Public ReadOnly Property NameStatus() As String
            Get
                Return _Name
            End Get
        End Property
    End Class
End Namespace

