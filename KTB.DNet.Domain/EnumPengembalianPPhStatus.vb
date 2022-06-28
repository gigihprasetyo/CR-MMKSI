Namespace KTB.DNet.Domain
    Public Class EnumPengembalianPPhStatus

        Public Enum PengembalianPPhStatus
            Baru = 0
            Validasi = 1
            Konfirmasi = 2
            Batal = 3
            Selesai = 4
        End Enum

        Public Function Baru() As String
            Return PengembalianPPhStatus.Baru
        End Function

        Public Function Validasi() As String
            Return PengembalianPPhStatus.Validasi
        End Function

        Public Function Batal() As String
            Return PengembalianPPhStatus.Batal
        End Function

        Public Function Selesai() As String
            Return PengembalianPPhStatus.Selesai
        End Function

        Public Function Konfirmasi() As String
            Return PengembalianPPhStatus.Konfirmasi
        End Function

        Public Shared Function GetStringValue(ByVal PengembalianPPhStatus As Integer) As String
            Dim str As String = ""
            If PengembalianPPhStatus = EnumPengembalianPPhStatus.PengembalianPPhStatus.Baru Then str = EnumPengembalianPPhStatus.PengembalianPPhStatus.Baru.ToString
            If PengembalianPPhStatus = EnumPengembalianPPhStatus.PengembalianPPhStatus.Validasi Then str = EnumPengembalianPPhStatus.PengembalianPPhStatus.Validasi.ToString
            If PengembalianPPhStatus = EnumPengembalianPPhStatus.PengembalianPPhStatus.Konfirmasi Then str = EnumPengembalianPPhStatus.PengembalianPPhStatus.Konfirmasi.ToString
            If PengembalianPPhStatus = EnumPengembalianPPhStatus.PengembalianPPhStatus.Batal Then str = EnumPengembalianPPhStatus.PengembalianPPhStatus.Batal.ToString
            If PengembalianPPhStatus = EnumPengembalianPPhStatus.PengembalianPPhStatus.Selesai Then str = EnumPengembalianPPhStatus.PengembalianPPhStatus.Selesai.ToString
            Return str
        End Function

        Public Function RetrievePengembalianPPhStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumPengembalianPPhStatusProp
            sts = New EnumPengembalianPPhStatusProp(0, "Baru")
            al.Add(sts)
            sts = New EnumPengembalianPPhStatusProp(1, "Validasi")
            al.Add(sts)
            sts = New EnumPengembalianPPhStatusProp(2, "Konfirmasi")
            al.Add(sts)
            sts = New EnumPengembalianPPhStatusProp(3, "Batal")
            al.Add(sts)
            sts = New EnumPengembalianPPhStatusProp(4, "Selesai")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class EnumPengembalianPPhStatusProp
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

