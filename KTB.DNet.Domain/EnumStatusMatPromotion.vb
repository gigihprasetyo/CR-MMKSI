Namespace KTB.DNet.Domain

    Public Class EnumStatusMatPromotion

        Public Enum StatusMatPromotion
            Baru = 0
            Batal = 1
            Validasi = 2
            Disetujui = 3
            Ditolak = 4
        End Enum

        Public Shared Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumStatusMatPromotionProp
            sts = New EnumStatusMatPromotionProp(0, "Baru")
            al.Add(sts)
            sts = New EnumStatusMatPromotionProp(1, "Batal")
            al.Add(sts)
            sts = New EnumStatusMatPromotionProp(2, "Validasi")
            al.Add(sts)
            sts = New EnumStatusMatPromotionProp(3, "Disetujui")
            al.Add(sts)
            sts = New EnumStatusMatPromotionProp(4, "Ditolak")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class EnumStatusMatPromotionProp
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

