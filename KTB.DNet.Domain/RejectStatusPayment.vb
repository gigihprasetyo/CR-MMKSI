Namespace KTB.DNet.Domain

    Public Class RejectStatusPayment

        Public Enum RejectStatusEnum
            Silahkan_Pilih
            Reschedule
            ReClearing
            Replacing
        End Enum

        Public Shared Function RetrieveRejectStatusPayment() As ArrayList
            Dim al As New ArrayList
            Dim prs As PaymentRejectStatusEnum
            prs = New PaymentRejectStatusEnum(0, CType(0, RejectStatusEnum).ToString)
            al.Add(prs)
            prs = New PaymentRejectStatusEnum(1, CType(1, RejectStatusEnum).ToString)
            al.Add(prs)
            prs = New PaymentRejectStatusEnum(2, CType(2, RejectStatusEnum).ToString)
            al.Add(prs)
            prs = New PaymentRejectStatusEnum(3, CType(3, RejectStatusEnum).ToString)
            al.Add(prs)
            Return al
        End Function


        Public Function RetrieveRejectStatusPaymentAlert() As ArrayList
            Dim al As New ArrayList
            Dim prs As PaymentRejectStatusEnum
            prs = New PaymentRejectStatusEnum(1, CType(1, RejectStatusEnum).ToString)
            al.Add(prs)
            prs = New PaymentRejectStatusEnum(2, CType(2, RejectStatusEnum).ToString)
            al.Add(prs)
            prs = New PaymentRejectStatusEnum(3, CType(3, RejectStatusEnum).ToString)
            al.Add(prs)
            Return al
        End Function
    End Class

    Public Class PaymentRejectStatusEnum

        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub

        Public Property ValStatus() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameStatus() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class

End Namespace

