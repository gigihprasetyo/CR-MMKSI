Namespace KTB.DNet.Domain
    Public Class EnumClaimProgress
        Public Enum ClaimProgressKTB
            BelumDikirim = 0
            Baru = 1
            Diproses = 2
            Selesai = 3
            Complete_Selesai = 4
        End Enum

        Public Function RetrieveStatusKTB() As ArrayList
            Dim al As New ArrayList
            Dim sts As ClaimProgressDealer
            sts = New ClaimProgressDealer(1, "Baru")
            al.Add(sts)
            sts = New ClaimProgressDealer(2, "Diproses")
            al.Add(sts)
            sts = New ClaimProgressDealer(3, "Selesai")
            al.Add(sts)
            sts = New ClaimProgressDealer(4, "Complete Selesai")
            al.Add(sts)
            Return al
        End Function
    End Class

    Public Class ClaimProgressDealer
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
