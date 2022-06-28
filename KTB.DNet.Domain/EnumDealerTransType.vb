
Namespace KTB.DNet.Domain
    Public Class EnumDealerTransType
        Public Enum DealerTransKind
            PKBulanan = 0
            PKTambahan = 1
            POBulanan = 2
            POTambahan = 3
            POSparePart = 4
            DaftarTraining = 5
            CreditControl = 6
            DataKendaraanLama = 7
            HargaPasar = 8
            BlokPO = 9
            PesanSeragam = 10
            FilterPengajuanPO = 11
            FilterAlokasi = 12
            RedemptionPlan = 13
            Factoring = 14
            InputGyro = 15
            FilterPengajuanPOMMC = 16
            FactoringMMC = 17
            FilterAlokasiMMC = 18
            FreezePK = 19
            AutoCustomer = 20
            TemporaryFaktur = 21
            TOPFreeDaysPO = 22
            BenefitCashback = 23
            DataSiswaBerbayar = 24
            PilotingPDI = 25
            PilotingPKT = 26
            PilotingWA = 27
            PilotingSPKMatching = 28
            PilotingServiceBooking = 29
            PilotingListServiceBooking = 30
            PilotingServiceStandardTime = 31
            PilotingUploadServiceStandardTime = 32
            PilotingStall = 33
            PilotingUploadMaintainGeneralRepair = 34
            BackdatePPN = 35
        End Enum

        Public Function RetrieveTransType(Optional ByVal companyCode As String = "") As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTransType
            sts = New EnumTransType("0", "PK Bulanan")
            al.Add(sts)
            sts = New EnumTransType("1", "PK Tambahan")
            al.Add(sts)
            sts = New EnumTransType("2", "PO Harian")
            al.Add(sts)
            sts = New EnumTransType("3", "PO Tambahan")
            al.Add(sts)
            sts = New EnumTransType("4", "PO SparePart")
            al.Add(sts)
            sts = New EnumTransType("5", "Daftar Training")
            al.Add(sts)
            sts = New EnumTransType("6", "Credit Control")
            al.Add(sts)
            sts = New EnumTransType("7", "Data Kendaraan Lama")
            al.Add(sts)
            ' Modified by Ikhsan, 20081202
            ' Requested by Rina as Part Of CR
            ' To add item in TrancationControl
            ' Start -----
            sts = New EnumTransType("8", "Harga Pasar")
            al.Add(sts)
            ' End -------
            'Start BlokPO
            sts = New EnumTransType("9", "List Blok PO")
            al.Add(sts)
            sts = New EnumTransType("10", "Pesan Seragam")
            al.Add(sts)
            'End BlockPO
            sts = New EnumTransType("13", "Redemption Plan")
            al.Add(sts)
            sts = New EnumTransType("15", "Input Gyro")
            al.Add(sts)
            If companyCode.Trim = "" Then
                sts = New EnumTransType("11", "Filter Pengajuan PO MFTBC")
                al.Add(sts)
                sts = New EnumTransType("12", "Filter Alokasi MFTBC")
                al.Add(sts)
                sts = New EnumTransType("14", "Factoring MFTBC")
                al.Add(sts)
                sts = New EnumTransType("16", "Filter Pengajuan PO MMC")
                al.Add(sts)
                sts = New EnumTransType("18", "Filter Alokasi MMC")
                al.Add(sts)
                sts = New EnumTransType("17", "Factoring MMC")
                al.Add(sts)
            End If
            If companyCode.Trim.ToUpper = "MFTBC" Then
                sts = New EnumTransType("11", "Filter Pengajuan PO MFTBC")
                al.Add(sts)
                sts = New EnumTransType("12", "Filter Alokasi MFTBC")
                al.Add(sts)
                sts = New EnumTransType("14", "Factoring MFTBC")
                al.Add(sts)
            End If

            If companyCode.Trim.ToUpper = "MMC" Then
                sts = New EnumTransType("16", "Filter Pengajuan PO MMC")
                al.Add(sts)
                sts = New EnumTransType("18", "Filter Alokasi MMC")
                al.Add(sts)
                sts = New EnumTransType("17", "Factoring MMC")
                al.Add(sts)
                sts = New EnumTransType("19", "Freeze PK Tambahan")
                al.Add(sts)
                sts = New EnumTransType("20", "Auto Customer")
                al.Add(sts)
                sts = New EnumTransType("21", "Temporary Faktur")
                al.Add(sts)
                sts = New EnumTransType("22", "TOP Free Days PO")
                al.Add(sts)
                sts = New EnumTransType("23", "Benefit Cashback")
                al.Add(sts)
            End If

            sts = New EnumTransType("24", "Daftar Siswa Berbayar ASS")
            al.Add(sts)
            sts = New EnumTransType("25", "Piloting PDI")
            al.Add(sts)
            sts = New EnumTransType("26", "Piloting PKT")
            al.Add(sts)
            sts = New EnumTransType("27", "PilotingWA")
            al.Add(sts)
            sts = New EnumTransType("28", "Piloting SPK Matching")
            al.Add(sts)
            sts = New EnumTransType("29", "Piloting Service Booking")
            al.Add(sts)
            sts = New EnumTransType("30", "Piloting List Service Booking")
            al.Add(sts)
            sts = New EnumTransType("31", "Piloting Service Stanidard Time")
            al.Add(sts)
            sts = New EnumTransType("32", "Piloting Upload Service Standard Time")
            al.Add(sts)
            sts = New EnumTransType("33", "Piloting Stall")
            al.Add(sts)
            sts = New EnumTransType("34", "Piloting Upload General Maintain Repair")
            al.Add(sts)
            sts = New EnumTransType("35", "Backdate PPN")
            al.Add(sts)
            Return al
        End Function
    End Class


    Public Class EnumTransType
        Private _val As String
        Private _Name As String


        Public Sub New(ByVal val As String, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValTransType() As String
            Get
                Return _val
            End Get
            Set(ByVal Value As String)
                _val = Value
            End Set
        End Property

        Property NameTransType() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class
End Namespace
