Namespace KTB.DNet.Domain
    Public Class EnumSPKCustomer
        Public Enum ReadyForUpdate
            No
            Yes
        End Enum
    End Class

    Public Class EnumStatusSPKCustomer
        Public Enum TipePengajuanSPKCustomer
            Baru
            Validasi
            Proses
            Block
            Selesai
        End Enum
        Public Function RetrieveStatusVerifikasi() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumDescStatusSPKCustomer
            sts = New EnumDescStatusSPKCustomer(1, "Validasi")
            al.Add(sts)
            sts = New EnumDescStatusSPKCustomer(2, "Proses")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveType() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumDescStatusSPKCustomer
            sts = New EnumDescStatusSPKCustomer(0, "Baru")
            al.Add(sts)
            sts = New EnumDescStatusSPKCustomer(1, "Validasi")
            al.Add(sts)
            sts = New EnumDescStatusSPKCustomer(2, "Proses")
            al.Add(sts)
            sts = New EnumDescStatusSPKCustomer(3, "Block")
            al.Add(sts)
            sts = New EnumDescStatusSPKCustomer(4, "Selesai")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveName(ByVal EnumIndex As Integer) As String
            Select Case EnumIndex
                Case 0
                    Return "Baru"
                Case 1
                    Return "Validasi"
                Case 2
                    Return "Proses"
                Case 3
                    Return "Block"
                Case 4
                    Return "Selesai"
            End Select

        End Function


    End Class

    Public Class EnumDescStatusSPKCustomer
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValTipe() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameTipe() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class

    Public Class EnumTipePelangganSPKCustomer
        Public Enum TipePelangganSPKCustomer
            Perorangan
            Perusahaan
            BUMN_Pemerintah
            Lainnya
        End Enum

        Public Function RetrieveType() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTipePelanggan
            sts = New EnumTipePelanggan(0, "Perorangan")
            al.Add(sts)
            sts = New EnumTipePelanggan(1, "Perusahaan")
            al.Add(sts)
            sts = New EnumTipePelanggan(2, "BUMN/Pemerintah")
            al.Add(sts)
            sts = New EnumTipePelanggan(3, "Lainnya")
            al.Add(sts)
            Return al
        End Function
    End Class
End Namespace

