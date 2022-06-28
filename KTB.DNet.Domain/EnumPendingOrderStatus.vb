Namespace KTB.DNet.Domain
    Public Class EnumPendingOrderStatus

        Public Enum PendingOrderStatus
            Pending = 0
            Complete = 1
        End Enum

        Public Function Pending() As String
            Return PendingOrderStatus.Pending
        End Function

        Public Function Complete() As String
            Return PendingOrderStatus.Complete
        End Function

        Public Function RetrievePendingOrderStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumPendingOrderStatusProp
            sts = New EnumPendingOrderStatusProp(0, "Pending")
            al.Add(sts)
            sts = New EnumPendingOrderStatusProp(1, "Complete")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class EnumPendingOrderStatusProp
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

