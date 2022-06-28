Public Class EnumCBUReturn

    Public Enum StatusClaim
        Baru = 0
        Validasi = 1
        Revisi = 2
        Konfirmasi = 3
        Tolak = 4
        Proses = 5
        Selesai = 6
    End Enum

    Public Function StatusClaimList() As ArrayList
        Dim arrList As ArrayList = New ArrayList
        arrList.Add(New EnumCBUReturnList(0, "Baru"))
        arrList.Add(New EnumCBUReturnList(1, "Validasi"))
        arrList.Add(New EnumCBUReturnList(2, "Revisi"))
        arrList.Add(New EnumCBUReturnList(3, "Konfirmasi"))
        arrList.Add(New EnumCBUReturnList(4, "Tolak"))
        arrList.Add(New EnumCBUReturnList(5, "Proses"))
        arrList.Add(New EnumCBUReturnList(6, "Selesai"))
        Return arrList
    End Function

    Public Enum TipeClaim
        Gores = 1
        Penyok = 2
        Karat = 3
        Rusak = 4
        Retak = 5
        Lubang = 6
        Noda = 7
        Hilang_Aksesoris = 8
        Bahan_Bakar_Kosong = 9
        Penundaan = 10
        Mobil_Hilang = 11
    End Enum

    Public Function TipeClaimList() As ArrayList
        Dim arrList As ArrayList = New ArrayList
        arrList.Add(New EnumCBUReturnList(1, "Gores"))
        arrList.Add(New EnumCBUReturnList(2, "Penyok"))
        arrList.Add(New EnumCBUReturnList(3, "Karat"))
        arrList.Add(New EnumCBUReturnList(4, "Rusak"))
        arrList.Add(New EnumCBUReturnList(5, "Retak"))
        arrList.Add(New EnumCBUReturnList(6, "Lubang"))
        arrList.Add(New EnumCBUReturnList(7, "Noda"))
        arrList.Add(New EnumCBUReturnList(8, "Hilang Aksesoris"))
        arrList.Add(New EnumCBUReturnList(9, "Bahan Bakar Kosong"))
        arrList.Add(New EnumCBUReturnList(10, "Penundaan"))
        arrList.Add(New EnumCBUReturnList(11, "Mobil Hilang"))
        Return arrList
    End Function

    Public Enum RespondClaim
        Ganti_Unit = 1
        Asuransi = 2
        Perbaikan_MMKSI = 3
        Perbaikan_Dealer = 4
        Ganti_Uang = 5
    End Enum

    Public Function RespondClaimList() As ArrayList
        Dim arrList As ArrayList = New ArrayList
        arrList.Add(New EnumCBUReturnList(1, "Ganti Unit"))
        arrList.Add(New EnumCBUReturnList(2, "Asuransi"))
        arrList.Add(New EnumCBUReturnList(3, "Perbaikan MMKSI"))
        arrList.Add(New EnumCBUReturnList(4, "Perbaikan Dealer"))
        arrList.Add(New EnumCBUReturnList(5, "Ganti Uang"))
        Return arrList
    End Function

    Public Enum StatusProsesRetur
        Send_To_SAP = 0
        Faktur_sudah_di_print = 1
        Cancel_Faktur = 2
        Cancel_Billing = 3
        Reverse_DO = 4
        Sales_Replacement = 5
        Proses_Faktur_Chassis_Pengganti = 6
    End Enum

    Public Function StatusProsesReturList() As ArrayList
        Dim arrList As ArrayList = New ArrayList
        arrList.Add(New EnumCBUReturnList(0, "Send To SAP"))
        arrList.Add(New EnumCBUReturnList(1, "Faktur sudah di print"))
        arrList.Add(New EnumCBUReturnList(2, "Cancel Faktur"))
        arrList.Add(New EnumCBUReturnList(3, "Cancel Billing"))
        arrList.Add(New EnumCBUReturnList(4, "Reverse DO"))
        arrList.Add(New EnumCBUReturnList(5, "Sales Retur"))
        arrList.Add(New EnumCBUReturnList(6, "Sales Replacement"))
        arrList.Add(New EnumCBUReturnList(7, "Proses Faktur Chassis Pengganti"))
        Return arrList
    End Function

    Public Enum StatusStockDMS
        Available = 1
        Not_Available = 2
    End Enum

    Public Function StatusStockDMSList() As ArrayList
        Dim arrList As ArrayList = New ArrayList
        arrList.Add(New EnumCBUReturnList(1, "Available"))
        arrList.Add(New EnumCBUReturnList(2, "Not Available"))
        Return arrList
    End Function

    Public Class EnumCBUReturnList
        Private _id As Integer
        Private _val As String

        Public Sub New(ByVal id As Integer, ByVal val As String)
            _id = id
            _val = val
        End Sub
        Public ReadOnly Property ID() As Integer
            Get
                Return _id
            End Get
        End Property

        Public ReadOnly Property Value() As String
            Get
                Return _val
            End Get
        End Property

    End Class
End Class
