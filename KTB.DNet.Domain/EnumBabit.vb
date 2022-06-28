Namespace KTB.DNet.Domain

    Public Class DealerProposalAction

        Private m_PreReq As String

        Public Sub New(ByVal preReq As String)
            m_PreReq = preReq
        End Sub

        Public ReadOnly Property Create() As Integer
            Get
                Return EnumBabit.StatusBabitProposal.Baru
            End Get
        End Property

        Public ReadOnly Property Submit() As Integer
            Get
                If m_PreReq <> EnumBabit.StatusBabitProposal.Baru Then
                    Return -1
                End If
                Return EnumBabit.StatusBabitProposal.Validasi
            End Get
        End Property

        Public ReadOnly Property Batal() As Integer
            Get
                If m_PreReq <> EnumBabit.StatusBabitProposal.Baru Then
                    Return -1
                End If
                Return EnumBabit.StatusBabitProposal.Batal
            End Get
        End Property

        Public ReadOnly Property Verify() As Integer
            Get
                If m_PreReq <> EnumBabit.StatusBabitProposal.Validasi Then
                    Return -1
                End If
                Return EnumBabit.StatusBabitProposal.Proses
            End Get
        End Property

        Public ReadOnly Property Reject() As Integer
            Get
                If m_PreReq <> EnumBabit.StatusBabitProposal.Baru Or m_PreReq <> EnumBabit.StatusBabitProposal.Proses Then
                    Return -1
                End If
                Return EnumBabit.StatusBabitProposal.Tolak
            End Get
        End Property

        Public ReadOnly Property Approve() As Integer
            Get
                If m_PreReq <> EnumBabit.StatusBabitProposal.Proses Then
                    Return -1
                End If
                Return EnumBabit.StatusBabitProposal.Disetujui
            End Get
        End Property
    End Class


    Public Class EnumBabit
        Private m_Action As String

        Public ReadOnly Property Action() As DealerProposalAction
            Get
                Return New DealerProposalAction(m_Action)
            End Get
        End Property

        Public Enum StatusBabitProposal
            Baru = 0
            Batal = 1
            Disetujui = 2
            Proses = 3
            Tolak = 4
            Validasi = 5
        End Enum

        Public Enum StatusBabitProposalItem
            Baru = 0
            Batal = 1
            Disetujui = 2
            Tolak = 3
        End Enum

        Public ReadOnly Property StatusBabitProposalListDealer() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As BabitItem
                'item = New BabitItem("1", "Batal")
                'list.Add(item)
                item = New BabitItem("5", "Validasi")
                list.Add(item)
                Return list
            End Get
        End Property

        Public ReadOnly Property StatusBabitProposalListKTB() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As BabitItem
                item = New BabitItem("3", "Proses")
                list.Add(item)
                item = New BabitItem("4", "Tolak")
                list.Add(item)
                item = New BabitItem("2", "Disetujui")
                list.Add(item)
                item = New BabitItem("5", "Validasi")
                list.Add(item)
                Return list
            End Get
        End Property

        Public ReadOnly Property StatusGroupware() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As BabitItem
                item = New BabitItem("0", "Baru")
                list.Add(item)
                item = New BabitItem("1", "Validasi")
                list.Add(item)
                item = New BabitItem("2", "Konfirmasi")
                list.Add(item)
                item = New BabitItem("3", "Revisi")
                list.Add(item)
                item = New BabitItem("4", "Proses")
                list.Add(item)
                item = New BabitItem("5", "Setuju")
                list.Add(item)
                item = New BabitItem("6", "Tidak Setuju")
                list.Add(item)
                item = New BabitItem("7", "Proses Groupware")
                list.Add(item)
                Return list
            End Get
        End Property

        Public ReadOnly Property StatusBabitProposalList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As BabitItem
                item = New BabitItem("0", "Baru")
                list.Add(item)
                item = New BabitItem("1", "Batal")
                list.Add(item)
                item = New BabitItem("5", "Validasi")
                list.Add(item)
                item = New BabitItem("3", "Proses")
                list.Add(item)
                item = New BabitItem("2", "Disetujui")
                list.Add(item)
                item = New BabitItem("4", "Tolak")
                list.Add(item)
                Return list
            End Get
        End Property

        Public Function StatusBabitProposalListAlert() As ArrayList
            Dim list As ArrayList = New ArrayList
            Dim item As BabitItem
            item = New BabitItem("0", "Baru")
            list.Add(item)
            item = New BabitItem("1", "Batal")
            list.Add(item)
            item = New BabitItem("5", "Validasi")
            list.Add(item)
            item = New BabitItem("3", "Proses")
            list.Add(item)
            item = New BabitItem("2", "Disetujui")
            list.Add(item)
            item = New BabitItem("4", "Tolak")
            list.Add(item)
            Return list
        End Function

        Public Sub New()

        End Sub

        Public Enum BabitType
            Regular = 0
            Khusus = 1
        End Enum

        Public ReadOnly Property BabitTypeList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As BabitItem
                item = New BabitItem("0", "Regular")
                list.Add(item)
                item = New BabitItem("1", "Khusus")
                list.Add(item)
                Return list
            End Get
        End Property

        Public Enum BabitAllocationType
            Alokasi_Reguler = 0
            Alokasi_Tambahan = 1
            Babit_Khusus = 2
        End Enum

        Public ReadOnly Property BabitAllocationTypeList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As BabitItem
                item = New BabitItem("0", "Alokasi_Reguler")
                list.Add(item)
                item = New BabitItem("1", "Alokasi_Tambahan")
                list.Add(item)
                item = New BabitItem("2", "Babit_Khusus")
                list.Add(item)
                Return list
            End Get
        End Property

        Public Enum BabitAllocationStatus
            Baru = 0
            Rilis = 1
        End Enum

        Public ReadOnly Property BabitAllocationStatusList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As BabitItem
                item = New BabitItem("0", "Baru")
                list.Add(item)
                item = New BabitItem("1", "Rilis")
                list.Add(item)
                Return list
            End Get
        End Property

        Public Enum BabitProposalType
            Pameran = 0
            Iklan = 1
            Even = 2
        End Enum

        Public ReadOnly Property BabitProposalTypeList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As BabitItem
                item = New BabitItem("0", "Pameran")
                list.Add(item)
                item = New BabitItem("1", "Iklan")
                list.Add(item)
                item = New BabitItem("2", "Even")
                list.Add(item)
                Return list
            End Get
        End Property

        Public Enum MediaType
            Elektronik = 0
            Cetak = 1
        End Enum

        Public ReadOnly Property MediaTypeList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As BabitItem
                item = New BabitItem("0", "Elektronik")
                list.Add(item)
                item = New BabitItem("1", "Cetak")
                list.Add(item)
                Return list
            End Get
        End Property

        Public Enum JenisKegiatan
            Awal = 0
            Tambahan = 1
        End Enum

        Public ReadOnly Property JenisKegiatanList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As BabitItem
                item = New BabitItem("0", "Awal")
                list.Add(item)
                item = New BabitItem("1", "Tambahan")
                list.Add(item)
                Return list
            End Get
        End Property



        Public Enum PengajuanDesignIklanStatus
            Baru = 0
            Rilis = 1
        End Enum

        Public ReadOnly Property PengajuanDesignIklanStatusList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As BabitItem
                item = New BabitItem("0", "Baru")
                list.Add(item)
                item = New BabitItem("1", "Rilis")
                list.Add(item)
                Return list
            End Get
        End Property

        Public Enum PaymentReleaseStatus
            Semua = 0
            Complete = 1
            OutStanding
        End Enum

        Public Enum StatusPembayaran
            Paid = 0
            UnPaid = 1
        End Enum

        Public ReadOnly Property StatusPembayaranList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As BabitItem
                item = New BabitItem("0", "Paid")
                list.Add(item)
                item = New BabitItem("1", "UnPaid")
                list.Add(item)
                Return list
            End Get
        End Property

        Public ReadOnly Property PaymentReleaseStatusList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As BabitItem
                item = New BabitItem("0", "Semua")
                list.Add(item)
                item = New BabitItem("1", "Complete")
                list.Add(item)
                item = New BabitItem("2", "OutStanding")
                list.Add(item)
                Return list
            End Get
        End Property

        Public Shared Function PaymentReleaseStatusLst(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim EmBabitItem As BabitItem

            If (isIncludeBlank) Then
                EmBabitItem = New BabitItem("", "99")
                arr.Add(EmBabitItem)
            End If
            EmBabitItem = New BabitItem("Semua", "0")
            arr.Add(EmBabitItem)

            EmBabitItem = New BabitItem("Complete", "1")
            arr.Add(EmBabitItem)

            EmBabitItem = New BabitItem("OutStanding", "2")
            arr.Add(EmBabitItem)
            Return arr
        End Function

        Public Enum BabitParameterCategory
            Biaya = 0
            MaterialPromosi = 1
            ProfilPengunjung = 2
            LokasiPameran = 3
            Media = 4
        End Enum
    End Class

    Public Class BabitItem
        Private m_Babitcode As String
        Private m_BabitValue As String

        Public Sub New()

        End Sub

        Public Sub New(ByVal code As String, ByVal value As String)
            m_Babitcode = code
            m_BabitValue = value
        End Sub

        Property BabitCode() As String
            Get
                Return m_Babitcode
            End Get
            Set(ByVal Value As String)
                m_Babitcode = Value
            End Set
        End Property

        Public Property BabitValue() As String
            Get
                Return m_BabitValue
            End Get
            Set(ByVal Value As String)
                m_BabitValue = Value
            End Set
        End Property
    End Class
End Namespace
