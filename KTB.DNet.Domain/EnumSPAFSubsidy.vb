Namespace KTB.DNet.Domain

    Public Class EnumSPAFSubsidy

        Public Enum SPAFDocStatus
            Baru = 0
            Validasi = 1
            Disetujui = 2
            Deleted = 3
            Ditolak = 4
            Proses = 5
        End Enum

        Public Enum Action
            Hapus = 0
            Validasi = 1
            Proses = 2
            BatalValidasi = 3
            BatalProses = 4
            Disetujui = 5
            Ditolak = 6
        End Enum

        Public Enum DocumentType
            SPAF = 0
            Subsidi = 1
        End Enum

        Public Function SPAF() As String
            Return DocumentType.SPAF
        End Function

        Public Function Subsidy() As String
            Return DocumentType.Subsidi
        End Function

        Public Function SPAFValue() As String
            Return "SPAF"
        End Function

        Public Function SubsidyValue() As String
            Return "Subsidi"
        End Function

        Public Function RetrieveSPAFDocStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumSPAF
            sts = New EnumSPAF(0, "Baru")
            al.Add(sts)
            sts = New EnumSPAF(1, "Validasi")
            al.Add(sts)
            sts = New EnumSPAF(5, "Proses")
            al.Add(sts)
            sts = New EnumSPAF(2, "Disetujui")
            al.Add(sts)
            sts = New EnumSPAF(4, "Ditolak")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveAction() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumSPAF
            'sts = New EnumSPAF(-1, "Silahkan Pilih")
            'al.Add(sts)
            sts = New EnumSPAF(0, "Hapus")
            al.Add(sts)
            sts = New EnumSPAF(1, "Validasi")
            al.Add(sts)
            sts = New EnumSPAF(2, "Proses")
            al.Add(sts)
            sts = New EnumSPAF(3, "Batal Validasi")
            al.Add(sts)
            sts = New EnumSPAF(4, "Batal Proses")
            al.Add(sts)
            sts = New EnumSPAF(5, "Disetujui")
            al.Add(sts)
            sts = New EnumSPAF(6, "Ditolak")
            al.Add(sts)

            Return al
        End Function


        Public Function RetrieveSPAFType() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumSPAF
            '---modify by r0nny,'Silahkan Pilih' is useless
            'sts = New EnumSPAF(-1, "Silahkan Pilih")
            'al.Add(sts)
            '---end modify
            sts = New EnumSPAF(0, "SPAF")
            al.Add(sts)
            sts = New EnumSPAF(1, "Subsidi")
            al.Add(sts)
            Return al
        End Function
    End Class

    Public Class EnumSPAF
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
