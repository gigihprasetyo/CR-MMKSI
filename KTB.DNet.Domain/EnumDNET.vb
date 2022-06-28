Public Class EnumDNET
    'enum dealer stock report
    Public Enum enumStockReport
        Stok
        Terkirim
    End Enum

    Public Function RetrieveStockReport() As ArrayList
        Dim al As New ArrayList
        Dim srType As StockReportType
        srType = New StockReportType("Terkirim", "Terkirim")
        al.Add(srType)
        srType = New StockReportType("Stok", "Stok")
        al.Add(srType)

        Return al
    End Function

    'enum SAP CustomerList
    Public Enum enumSAPCustomer
        HotProspect = 1
        Prospect = 2
        Suspect = 3
        Deal_SPK = 4
    End Enum

    Public Function RetrieveSAPCustomerList() As ArrayList
        Dim al As New ArrayList
        Dim srType As RetrieveGeneralEnum
        srType = New RetrieveGeneralEnum(1, "HotProspect")
        al.Add(srType)
        srType = New RetrieveGeneralEnum(2, "Prospect")
        al.Add(srType)
        srType = New RetrieveGeneralEnum(3, "Suspect")
        al.Add(srType)
        srType = New RetrieveGeneralEnum(4, "Deal/SPK")
        al.Add(srType)
        srType = New RetrieveGeneralEnum(5, "No Prospect")
        al.Add(srType)
        srType = New RetrieveGeneralEnum(6, "Canceled")
        al.Add(srType)
        Return al
    End Function

    'enum PRPCategory
    Public Enum PRPCategory
        Aktif = 0
        Tidak_Aktif = 1
    End Enum

    Public Function RetrievePRP() As ArrayList
        Dim al As New ArrayList
        Dim srType As RetrieveGeneralEnum
        srType = New RetrieveGeneralEnum(0, "Aktif")
        al.Add(srType)
        srType = New RetrieveGeneralEnum(1, "Tidak Aktif")
        al.Add(srType)

        Return al
    End Function

    'enum Sales - Status Faktur Kendaraan
    Public Enum enumFakturKendaraan
        Validasi = 1
        Konfirmasi = 2
        Proses = 3
        Selesai = 4
    End Enum

    Public Function RetrieveStatusFakturKendaraan() As ArrayList
        Dim arrReturn As New ArrayList
        Dim srItem As RetrieveEnum
        srItem = New RetrieveEnum(enumFakturKendaraan.Validasi, enumFakturKendaraan.Validasi.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumFakturKendaraan.Konfirmasi, enumFakturKendaraan.Konfirmasi.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumFakturKendaraan.Proses, enumFakturKendaraan.Proses.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumFakturKendaraan.Selesai, enumFakturKendaraan.Selesai.ToString)
        arrReturn.Add(srItem)

        Return arrReturn
    End Function

    'enum Sales - Status Faktur Kendaraan
    Public Enum enumBapiLogKind
        IR = 1
        PO = 2
        MSP = 3
    End Enum

    Public Function RetrieveBapiLogKind() As ArrayList
        Dim arrReturn As New ArrayList
        Dim srItem As RetrieveEnum
        srItem = New RetrieveEnum(enumBapiLogKind.IR, enumBapiLogKind.IR.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumBapiLogKind.PO, enumBapiLogKind.PO.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumBapiLogKind.MSP, enumBapiLogKind.MSP.ToString)
        arrReturn.Add(srItem)

        Return arrReturn
    End Function

    'enum Sales - Status Faktur Kendaraan Revision
    Public Enum enumFakturKendaraanRev
        Baru = 0
        Validasi = 1
        Konfirmasi = 2
        Proses = 3
        Selesai = 4
    End Enum

    Public Function RetrieveStatusFakturKendaraanRev() As ArrayList
        Dim arrReturn As New ArrayList
        Dim srItem As RetrieveEnum
        srItem = New RetrieveEnum(enumFakturKendaraanRev.Baru, enumFakturKendaraanRev.Baru.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumFakturKendaraanRev.Validasi, enumFakturKendaraanRev.Validasi.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumFakturKendaraanRev.Konfirmasi, enumFakturKendaraanRev.Konfirmasi.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumFakturKendaraanRev.Proses, enumFakturKendaraanRev.Proses.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumFakturKendaraanRev.Selesai, enumFakturKendaraanRev.Selesai.ToString)
        arrReturn.Add(srItem)

        Return arrReturn
    End Function

    'enum Sales - Status Pembayaran Faktur Kendaraan Revision
    Public Enum enumPaymentFakturKendaraanRev
        Baru = 0
        Validasi = 1
        Konfirmasi = 2
        Proses = 3
        Selesai = 4
    End Enum

    Public Shared Function GetPaymentTypeStringValue(ByVal PaymentType As String) As String
        Dim str As String = ""
        If PaymentType = "0" Then str = "Baru"
        If PaymentType = "1" Then str = "Validasi"
        If PaymentType = "2" Then str = "Konfirmasi"
        If PaymentType = "3" Then str = "Proses"
        If PaymentType = "4" Then str = "Selesai"
        Return str
    End Function

    Public Function RetrieveStatusPaymentFakturKendaraanRev() As ArrayList
        Dim arrReturn As New ArrayList
        Dim srItem As RetrieveEnum
        srItem = New RetrieveEnum(enumFakturKendaraanRev.Baru, enumFakturKendaraanRev.Baru.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumFakturKendaraanRev.Validasi, enumFakturKendaraanRev.Validasi.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumFakturKendaraanRev.Konfirmasi, enumFakturKendaraanRev.Konfirmasi.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumFakturKendaraanRev.Proses, enumFakturKendaraanRev.Proses.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumFakturKendaraanRev.Selesai, enumFakturKendaraanRev.Selesai.ToString)
        arrReturn.Add(srItem)

        Return arrReturn
    End Function

    Public Enum enumRevType
        'swap rs & rn sesuai table RevisionType
        RN = 1
        RS = 2
        RT = 3
        RR = 4
    End Enum

    Public Function RetrieveRevType() As ArrayList
        Dim arrReturn As New ArrayList
        Dim srItem As RetrieveEnum
        srItem = New RetrieveEnum(enumRevType.RS, enumRevType.RS.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumRevType.RN, enumRevType.RN.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumRevType.RT, enumRevType.RT.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumRevType.RR, enumRevType.RR.ToString)
        arrReturn.Add(srItem)

        Return arrReturn
    End Function

    'enum Payment Option - Revision Faktur
    Public Enum enumPaymentOption
        Bayar = 1
        TidakBayar = 0
    End Enum

    Public Function RetrievePaymentOption() As ArrayList
        Dim arrReturn As New ArrayList
        Dim srItem As RetrieveEnum
        srItem = New RetrieveEnum(enumPaymentOption.Bayar, enumPaymentOption.Bayar.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumPaymentOption.TidakBayar, enumPaymentOption.TidakBayar.ToString)
        arrReturn.Add(srItem)

        Return arrReturn
    End Function

    'enum Sales - Status Faktur Kendaraan
    Public Enum enumDaftarDokumenService
        Semua = 0
        Belum_Didownload = 1
        Sudah_Didownload = 2
    End Enum

    Public Function RetrieveStatusDaftarDokumenService() As ArrayList
        Dim arrReturn As New ArrayList
        Dim srItem As RetrieveEnum
        srItem = New RetrieveEnum(enumDaftarDokumenService.Semua, enumDaftarDokumenService.Semua.ToString.Replace("_", " "))
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumDaftarDokumenService.Belum_Didownload, enumDaftarDokumenService.Belum_Didownload.ToString.Replace("_", " "))
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumDaftarDokumenService.Sudah_Didownload, enumDaftarDokumenService.Sudah_Didownload.ToString.Replace("_", " "))
        arrReturn.Add(srItem)

        Return arrReturn
    End Function

    'enum Sales - Status Dokument Payment Faktur Kendaraan
    Public Enum enumDaftarDokumenPayment
        Semua = 0
        Belum_Upload = 1
        Sudah_Upload = 2
    End Enum

    Public Function RetrieveStatusDaftarDokumenPayment() As ArrayList
        Dim arrReturn As ArrayList = New ArrayList
        Dim srItem As RetrieveEnum
        srItem = New RetrieveEnum(enumDaftarDokumenPayment.Semua, enumDaftarDokumenPayment.Semua.ToString.Replace("_", " "))
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumDaftarDokumenPayment.Belum_Upload, enumDaftarDokumenPayment.Belum_Upload.ToString.Replace("_", " "))
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumDaftarDokumenPayment.Sudah_Upload, enumDaftarDokumenPayment.Sudah_Upload.ToString.Replace("_", " "))
        arrReturn.Add(srItem)

        Return arrReturn
    End Function

    'enum Form mode
    Public Enum enumFormMode
        View = 0
        Add = 1
        Edit = 2
    End Enum

    Public Function RetrieveFormMode() As ArrayList
        Dim arrReturn As New ArrayList
        Dim srItem As RetrieveEnum
        srItem = New RetrieveEnum(enumFormMode.View, enumFormMode.View.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumFormMode.Add, enumFormMode.Add.ToString)
        arrReturn.Add(srItem)
        srItem = New RetrieveEnum(enumFormMode.Edit, enumFormMode.Edit.ToString)
        arrReturn.Add(srItem)
        Return arrReturn
    End Function

    'Enum TOP SP Transfer OutStanding
    Public Enum EnumTOPSPTransferOutStanding
        TOP = 1
        COD = 2
    End Enum

    Public Shared Function GetTOPSPTransferOutStandingStringValue(ByVal IDTransaction As Short) As String
        Dim str As String = ""
        If IDTransaction = "1" Then str = "TOP"
        If IDTransaction = "2" Then str = "COD"
        Return str
    End Function
End Class

Public Class StockReportType
    Private _val As String
    Private _Name As String

    Public Sub New(ByVal val As String, ByVal name As String)
        _val = val
        _Name = name
    End Sub

    Public Property ValType() As String
        Get
            Return _val
        End Get
        Set(ByVal Value As String)
            _val = Value
        End Set
    End Property

    Property NameType() As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value
        End Set
    End Property
End Class

Public Class RetrieveGeneralEnum
    Private _val As Integer
    Private _Name As String

    Public Sub New(ByVal val As Integer, ByVal name As String)
        _val = val
        _Name = name
    End Sub

    Public Property ValType() As Integer
        Get
            Return _val
        End Get
        Set(ByVal Value As Integer)
            _val = Value
        End Set
    End Property

    Property NameType() As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value
        End Set
    End Property
End Class

Public Class RetrieveEnum
    Private _val As Integer
    Private _Name As String

    Public Sub New(ByVal val As Integer, ByVal name As String)
        _val = val
        _Name = name
    End Sub

    Public Property ValType() As Integer
        Get
            Return _val
        End Get
        Set(ByVal Value As Integer)
            _val = Value
        End Set
    End Property

    Property NameType() As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value
        End Set
    End Property
End Class