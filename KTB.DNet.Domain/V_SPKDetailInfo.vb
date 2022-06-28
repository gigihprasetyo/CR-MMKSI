
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SPKDetailInfo Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 6/7/2012 - 5:29:37 PM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("V_SPKDetailInfo")> _
    Public Class V_SPKDetailInfo
        Inherits DomainObject


#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal SPKDetailID As Integer)
            _sPKDetailID = SPKDetailID
        End Sub

#End Region

#Region "Private Variables"

        Private _sPKDetailID As Integer
        Private _lineItem As Short
        Private _categoryID As Byte
        Private _vehicleColorID As Short
        Private _vehicleKindID As Integer
        Private _vehicleTypeCode As String = String.Empty
        Private _vehicleColorCode As String = String.Empty
        Private _vehicleColorName As String = String.Empty
        Private _profileDetailID As Integer
        Private _profileDescription As String = String.Empty
        Private _additional As Byte
        Private _remarks As String = String.Empty
        Private _quantity As Integer
        Private _amount As Decimal
        Private _totalAmount As Decimal
        Private _rejectedReasonDetail As String = String.Empty
        Private _detailStatus As Byte
        Private _sPKHeaderID As Integer
        Private _dealerID As Short
        Private _headerStatus As String = String.Empty
        Private _sPKNumber As String = String.Empty
        Private _indentNumber As String = String.Empty
        Private _dealerSPKNumber As String = String.Empty
        Private _planDeliveryMonth As Byte
        Private _planDeliveryYear As Short
        Private _planDeliveryDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _planInvoiceMonth As Byte
        Private _planInvoiceYear As Short
        Private _planInvoiceDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _customerRequestID As Integer
        Private _validateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerSPKDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validateBy As String = String.Empty
        Private _rejectedReasonHeader As String = String.Empty
        Private _salesmanHeaderID As Int32
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _customerID As Integer
        Private _code As String = String.Empty
        Private _reffCode As String = String.Empty
        Private _tipeCustomer As Short
        Private _tipePerusahaan As Short
        Private _name1 As String = String.Empty
        Private _name2 As String = String.Empty
        Private _name3 As String = String.Empty
        Private _alamat As String = String.Empty
        Private _kelurahan As String = String.Empty
        Private _kecamatan As String = String.Empty
        Private _postalCode As String = String.Empty
        Private _preArea As String = String.Empty
        Private _cityID As Short
        Private _printRegion As String = String.Empty
        Private _phoneNo As String = String.Empty
        Private _officeNo As String = String.Empty
        Private _homeNo As String = String.Empty
        Private _hpNo As String = String.Empty
        Private _email As String = String.Empty
        Private _custStatus As Integer
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _cityName As String = String.Empty
        Private _provinceID As Integer
        Private _provinceName As String = String.Empty
        Private _categoryCode As String = String.Empty
        Private _vechileTypeCode As String = String.Empty
        Private _description As String = String.Empty
        Private _colorEngName As String = String.Empty
        Private _salesmanId As Int32
        Private _salesmanCode As String = String.Empty
        Private _salesName As String = String.Empty
        Private _salesPosition As String = String.Empty
        Private _salesLevel As String = String.Empty
        Private _supervisor As String = String.Empty
        Private _manager As String = String.Empty
        Private _caraPembayaran As String = String.Empty
        Private _kepemilikanKendaraan As String = String.Empty
        Private _kendaraanSebagai As String = String.Empty
        Private _penggunaanUtamaKendaraan As String = String.Empty
        Private _usiaPemilik As String = String.Empty
        Private _bidangUsaha As String = String.Empty
        Private _daerahUtama As String = String.Empty
        Private _bentukBodyCV As String = String.Empty
        Private _bentukBodyLCV As String = String.Empty
        Private _jenisKendaraan As String = String.Empty
        Private _modelKendaraan As String = String.Empty
        Private _ktp As String = String.Empty
        Private _cityCustomer As String = String.Empty



#End Region

#Region "Public Properties"

        <ColumnInfo("SPKDetailID", "{0}")> _
        Public Property SPKDetailID() As Integer
            Get
                Return _sPKDetailID
            End Get
            Set(ByVal value As Integer)
                _sPKDetailID = value
            End Set
        End Property


        <ColumnInfo("LineItem", "{0}")> _
        Public Property LineItem() As Short
            Get
                Return _lineItem
            End Get
            Set(ByVal value As Short)
                _lineItem = value
            End Set
        End Property


        <ColumnInfo("CategoryID", "{0}")> _
        Public Property CategoryID() As Byte
            Get
                Return _categoryID
            End Get
            Set(ByVal value As Byte)
                _categoryID = value
            End Set
        End Property


        <ColumnInfo("VehicleColorID", "{0}")> _
        Public Property VehicleColorID() As Short
            Get
                Return _vehicleColorID
            End Get
            Set(ByVal value As Short)
                _vehicleColorID = value
            End Set
        End Property


        <ColumnInfo("VehicleKindID", "{0}")> _
        Public Property VehicleKindID() As Integer
            Get
                Return _vehicleKindID
            End Get
            Set(ByVal value As Integer)
                _vehicleKindID = value
            End Set
        End Property


        <ColumnInfo("VehicleTypeCode", "'{0}'")> _
        Public Property VehicleTypeCode() As String
            Get
                Return _vehicleTypeCode
            End Get
            Set(ByVal value As String)
                _vehicleTypeCode = value
            End Set
        End Property


        <ColumnInfo("VehicleColorCode", "'{0}'")> _
        Public Property VehicleColorCode() As String
            Get
                Return _vehicleColorCode
            End Get
            Set(ByVal value As String)
                _vehicleColorCode = value
            End Set
        End Property


        <ColumnInfo("VehicleColorName", "'{0}'")> _
        Public Property VehicleColorName() As String
            Get
                Return _vehicleColorName
            End Get
            Set(ByVal value As String)
                _vehicleColorName = value
            End Set
        End Property


        <ColumnInfo("ProfileDetailID", "{0}")> _
        Public Property ProfileDetailID() As Integer
            Get
                Return _profileDetailID
            End Get
            Set(ByVal value As Integer)
                _profileDetailID = value
            End Set
        End Property


        <ColumnInfo("ProfileDescription", "'{0}'")> _
        Public Property ProfileDescription() As String
            Get
                Return _profileDescription
            End Get
            Set(ByVal value As String)
                _profileDescription = value
            End Set
        End Property


        <ColumnInfo("Additional", "{0}")> _
        Public Property Additional() As Byte
            Get
                Return _additional
            End Get
            Set(ByVal value As Byte)
                _additional = value
            End Set
        End Property


        <ColumnInfo("Remarks", "'{0}'")> _
        Public Property Remarks() As String
            Get
                Return _remarks
            End Get
            Set(ByVal value As String)
                _remarks = value
            End Set
        End Property


        <ColumnInfo("Quantity", "{0}")> _
        Public Property Quantity() As Integer
            Get
                Return _quantity
            End Get
            Set(ByVal value As Integer)
                _quantity = value
            End Set
        End Property


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount() As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property


        <ColumnInfo("TotalAmount", "{0}")> _
        Public Property TotalAmount() As Decimal
            Get
                Return _totalAmount
            End Get
            Set(ByVal value As Decimal)
                _totalAmount = value
            End Set
        End Property


        <ColumnInfo("RejectedReasonDetail", "'{0}'")> _
        Public Property RejectedReasonDetail() As String
            Get
                Return _rejectedReasonDetail
            End Get
            Set(ByVal value As String)
                _rejectedReasonDetail = value
            End Set
        End Property


        <ColumnInfo("DetailStatus", "{0}")> _
        Public Property DetailStatus() As Byte
            Get
                Return _detailStatus
            End Get
            Set(ByVal value As Byte)
                _detailStatus = value
            End Set
        End Property


        <ColumnInfo("SPKHeaderID", "{0}")> _
        Public Property SPKHeaderID() As Integer
            Get
                Return _sPKHeaderID
            End Get
            Set(ByVal value As Integer)
                _sPKHeaderID = value
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID() As Short
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Short)
                _dealerID = value
            End Set
        End Property


        <ColumnInfo("HeaderStatus", "'{0}'")> _
        Public Property HeaderStatus() As String
            Get
                Return _headerStatus
            End Get
            Set(ByVal value As String)
                _headerStatus = value
            End Set
        End Property


        <ColumnInfo("SPKNumber", "'{0}'")> _
        Public Property SPKNumber() As String
            Get
                Return _sPKNumber
            End Get
            Set(ByVal value As String)
                _sPKNumber = value
            End Set
        End Property


        <ColumnInfo("IndentNumber", "'{0}'")> _
        Public Property IndentNumber() As String
            Get
                Return _indentNumber
            End Get
            Set(ByVal value As String)
                _indentNumber = value
            End Set
        End Property

        <ColumnInfo("DealerSPKNumber", "'{0}'")> _
        Public Property DealerSPKNumber() As String
            Get
                Return _dealerSPKNumber
            End Get
            Set(ByVal value As String)
                _dealerSPKNumber = value
            End Set
        End Property


        <ColumnInfo("PlanDeliveryMonth", "{0}")> _
        Public Property PlanDeliveryMonth() As Byte
            Get
                Return _planDeliveryMonth
            End Get
            Set(ByVal value As Byte)
                _planDeliveryMonth = value
            End Set
        End Property


        <ColumnInfo("PlanDeliveryYear", "{0}")> _
        Public Property PlanDeliveryYear() As Short
            Get
                Return _planDeliveryYear
            End Get
            Set(ByVal value As Short)
                _planDeliveryYear = value
            End Set
        End Property


        <ColumnInfo("PlanDeliveryDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PlanDeliveryDate() As DateTime
            Get
                Return _planDeliveryDate
            End Get
            Set(ByVal value As DateTime)
                _planDeliveryDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PlanInvoiceMonth", "{0}")> _
        Public Property PlanInvoiceMonth() As Byte
            Get
                Return _planInvoiceMonth
            End Get
            Set(ByVal value As Byte)
                _planInvoiceMonth = value
            End Set
        End Property


        <ColumnInfo("PlanInvoiceYear", "{0}")> _
        Public Property PlanInvoiceYear() As Short
            Get
                Return _planInvoiceYear
            End Get
            Set(ByVal value As Short)
                _planInvoiceYear = value
            End Set
        End Property


        <ColumnInfo("PlanInvoiceDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PlanInvoiceDate() As DateTime
            Get
                Return _planInvoiceDate
            End Get
            Set(ByVal value As DateTime)
                _planInvoiceDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("CustomerRequestID", "{0}")> _
        Public Property CustomerRequestID() As Integer
            Get
                Return _customerRequestID
            End Get
            Set(ByVal value As Integer)
                _customerRequestID = value
            End Set
        End Property


        <ColumnInfo("DealerSPKDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DealerSPKDate() As DateTime
            Get
                Return _dealerSPKDate
            End Get
            Set(ByVal value As DateTime)
                _dealerSPKDate = value
            End Set
        End Property

        <ColumnInfo("ValidateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidateTime() As DateTime
            Get
                Return _validateTime
            End Get
            Set(ByVal value As DateTime)
                _validateTime = value
            End Set
        End Property


        <ColumnInfo("ValidateBy", "'{0}'")> _
        Public Property ValidateBy() As String
            Get
                Return _validateBy
            End Get
            Set(ByVal value As String)
                _validateBy = value
            End Set
        End Property


        <ColumnInfo("RejectedReasonHeader", "'{0}'")> _
        Public Property RejectedReasonHeader() As String
            Get
                Return _rejectedReasonHeader
            End Get
            Set(ByVal value As String)
                _rejectedReasonHeader = value
            End Set
        End Property


        <ColumnInfo("SalesmanHeaderID", "{0}")> _
        Public Property SalesmanHeaderID() As Int32
            Get
                Return _salesmanHeaderID
            End Get
            Set(ByVal value As Int32)
                _salesmanHeaderID = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("CustomerID", "{0}")> _
        Public Property CustomerID() As Integer
            Get
                Return _customerID
            End Get
            Set(ByVal value As Integer)
                _customerID = value
            End Set
        End Property


        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property


        <ColumnInfo("ReffCode", "'{0}'")> _
        Public Property ReffCode() As String
            Get
                Return _reffCode
            End Get
            Set(ByVal value As String)
                _reffCode = value
            End Set
        End Property


        <ColumnInfo("TipeCustomer", "{0}")> _
        Public Property TipeCustomer() As Short
            Get
                Return _tipeCustomer
            End Get
            Set(ByVal value As Short)
                _tipeCustomer = value
            End Set
        End Property


        <ColumnInfo("TipePerusahaan", "{0}")> _
        Public Property TipePerusahaan() As Short
            Get
                Return _tipePerusahaan
            End Get
            Set(ByVal value As Short)
                _tipePerusahaan = value
            End Set
        End Property


        <ColumnInfo("Name1", "'{0}'")> _
        Public Property Name1() As String
            Get
                Return _name1
            End Get
            Set(ByVal value As String)
                _name1 = value
            End Set
        End Property


        <ColumnInfo("Name2", "'{0}'")> _
        Public Property Name2() As String
            Get
                Return _name2
            End Get
            Set(ByVal value As String)
                _name2 = value
            End Set
        End Property


        <ColumnInfo("Name3", "'{0}'")> _
        Public Property Name3() As String
            Get
                Return _name3
            End Get
            Set(ByVal value As String)
                _name3 = value
            End Set
        End Property


        <ColumnInfo("Alamat", "'{0}'")> _
        Public Property Alamat() As String
            Get
                Return _alamat
            End Get
            Set(ByVal value As String)
                _alamat = value
            End Set
        End Property


        <ColumnInfo("Kelurahan", "'{0}'")> _
        Public Property Kelurahan() As String
            Get
                Return _kelurahan
            End Get
            Set(ByVal value As String)
                _kelurahan = value
            End Set
        End Property


        <ColumnInfo("Kecamatan", "'{0}'")> _
        Public Property Kecamatan() As String
            Get
                Return _kecamatan
            End Get
            Set(ByVal value As String)
                _kecamatan = value
            End Set
        End Property


        <ColumnInfo("PostalCode", "'{0}'")> _
        Public Property PostalCode() As String
            Get
                Return _postalCode
            End Get
            Set(ByVal value As String)
                _postalCode = value
            End Set
        End Property


        <ColumnInfo("PreArea", "'{0}'")> _
        Public Property PreArea() As String
            Get
                Return _preArea
            End Get
            Set(ByVal value As String)
                _preArea = value
            End Set
        End Property


        <ColumnInfo("CityID", "{0}")> _
        Public Property CityID() As Short
            Get
                Return _cityID
            End Get
            Set(ByVal value As Short)
                _cityID = value
            End Set
        End Property


        <ColumnInfo("PrintRegion", "'{0}'")> _
        Public Property PrintRegion() As String
            Get
                Return _printRegion
            End Get
            Set(ByVal value As String)
                _printRegion = value
            End Set
        End Property


        <ColumnInfo("PhoneNo", "'{0}'")> _
        Public Property PhoneNo() As String
            Get
                Return _phoneNo
            End Get
            Set(ByVal value As String)
                _phoneNo = value
            End Set
        End Property


        <ColumnInfo("OfficeNo", "'{0}'")> _
        Public Property OfficeNo() As String
            Get
                Return _officeNo
            End Get
            Set(ByVal value As String)
                _officeNo = value
            End Set
        End Property


        <ColumnInfo("HomeNo", "'{0}'")> _
        Public Property HomeNo() As String
            Get
                Return _homeNo
            End Get
            Set(ByVal value As String)
                _homeNo = value
            End Set
        End Property


        <ColumnInfo("HpNo", "'{0}'")> _
        Public Property HpNo() As String
            Get
                Return _hpNo
            End Get
            Set(ByVal value As String)
                _hpNo = value
            End Set
        End Property


        <ColumnInfo("Email", "'{0}'")> _
        Public Property Email() As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property


        <ColumnInfo("CustStatus", "{0}")> _
        Public Property CustStatus() As Integer
            Get
                Return _custStatus
            End Get
            Set(ByVal value As Integer)
                _custStatus = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName() As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        <ColumnInfo("CityName", "'{0}'")> _
        Public Property CityName() As String
            Get
                Return _cityName
            End Get
            Set(ByVal value As String)
                _cityName = value
            End Set
        End Property


        <ColumnInfo("ProvinceID", "{0}")> _
        Public Property ProvinceID() As Integer
            Get
                Return _provinceID
            End Get
            Set(ByVal value As Integer)
                _provinceID = value
            End Set
        End Property


        <ColumnInfo("ProvinceName", "'{0}'")> _
        Public Property ProvinceName() As String
            Get
                Return _provinceName
            End Get
            Set(ByVal value As String)
                _provinceName = value
            End Set
        End Property


        <ColumnInfo("CategoryCode", "'{0}'")> _
        Public Property CategoryCode() As String
            Get
                Return _categoryCode
            End Get
            Set(ByVal value As String)
                _categoryCode = value
            End Set
        End Property


        <ColumnInfo("VechileTypeCode", "'{0}'")> _
        Public Property VechileTypeCode() As String
            Get
                Return _vechileTypeCode
            End Get
            Set(ByVal value As String)
                _vechileTypeCode = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("ColorEngName", "'{0}'")> _
        Public Property ColorEngName() As String
            Get
                Return _colorEngName
            End Get
            Set(ByVal value As String)
                _colorEngName = value
            End Set
        End Property


        <ColumnInfo("SalesmanId", "{0}")> _
        Public Property SalesmanId() As Int32
            Get
                Return _salesmanId
            End Get
            Set(ByVal value As Int32)
                _salesmanId = value
            End Set
        End Property


        <ColumnInfo("SalesmanCode", "'{0}'")> _
        Public Property SalesmanCode() As String
            Get
                Return _salesmanCode
            End Get
            Set(ByVal value As String)
                _salesmanCode = value
            End Set
        End Property


        <ColumnInfo("SalesName", "'{0}'")> _
        Public Property SalesName() As String
            Get
                Return _salesName
            End Get
            Set(ByVal value As String)
                _salesName = value
            End Set
        End Property


        <ColumnInfo("SalesPosition", "'{0}'")> _
        Public Property SalesPosition() As String
            Get
                Return _salesPosition
            End Get
            Set(ByVal value As String)
                _salesPosition = value
            End Set
        End Property


        <ColumnInfo("SalesLevel", "'{0}'")> _
        Public Property SalesLevel() As String
            Get
                Return _salesLevel
            End Get
            Set(ByVal value As String)
                _salesLevel = value
            End Set
        End Property


        <ColumnInfo("Supervisor", "'{0}'")> _
        Public Property Supervisor() As String
            Get
                Return _supervisor
            End Get
            Set(ByVal value As String)
                _supervisor = value
            End Set
        End Property


        <ColumnInfo("Manager", "'{0}'")> _
        Public Property Manager() As String
            Get
                Return _manager
            End Get
            Set(ByVal value As String)
                _manager = value
            End Set
        End Property


        <ColumnInfo("CaraPembayaran", "'{0}'")> _
        Public Property CaraPembayaran() As String
            Get
                Return _caraPembayaran
            End Get
            Set(ByVal value As String)
                _caraPembayaran = value
            End Set
        End Property


        <ColumnInfo("KepemilikanKendaraan", "'{0}'")> _
        Public Property KepemilikanKendaraan() As String
            Get
                Return _kepemilikanKendaraan
            End Get
            Set(ByVal value As String)
                _kepemilikanKendaraan = value
            End Set
        End Property


        <ColumnInfo("KendaraanSebagai", "'{0}'")> _
        Public Property KendaraanSebagai() As String
            Get
                Return _kendaraanSebagai
            End Get
            Set(ByVal value As String)
                _kendaraanSebagai = value
            End Set
        End Property


        <ColumnInfo("PenggunaanUtamaKendaraan", "'{0}'")> _
        Public Property PenggunaanUtamaKendaraan() As String
            Get
                Return _penggunaanUtamaKendaraan
            End Get
            Set(ByVal value As String)
                _penggunaanUtamaKendaraan = value
            End Set
        End Property


        <ColumnInfo("UsiaPemilik", "'{0}'")> _
        Public Property UsiaPemilik() As String
            Get
                Return _usiaPemilik
            End Get
            Set(ByVal value As String)
                _usiaPemilik = value
            End Set
        End Property


        <ColumnInfo("BidangUsaha", "'{0}'")> _
        Public Property BidangUsaha() As String
            Get
                Return _bidangUsaha
            End Get
            Set(ByVal value As String)
                _bidangUsaha = value
            End Set
        End Property


        <ColumnInfo("DaerahUtama", "'{0}'")> _
        Public Property DaerahUtama() As String
            Get
                Return _daerahUtama
            End Get
            Set(ByVal value As String)
                _daerahUtama = value
            End Set
        End Property


        <ColumnInfo("BentukBodyCV", "'{0}'")> _
        Public Property BentukBodyCV() As String
            Get
                Return _bentukBodyCV
            End Get
            Set(ByVal value As String)
                _bentukBodyCV = value
            End Set
        End Property


        <ColumnInfo("BentukBodyLCV", "'{0}'")> _
        Public Property BentukBodyLCV() As String
            Get
                Return _bentukBodyLCV
            End Get
            Set(ByVal value As String)
                _bentukBodyLCV = value
            End Set
        End Property


        <ColumnInfo("JenisKendaraan", "'{0}'")> _
        Public Property JenisKendaraan() As String
            Get
                Return _jenisKendaraan
            End Get
            Set(ByVal value As String)
                _jenisKendaraan = value
            End Set
        End Property


        <ColumnInfo("ModelKendaraan", "'{0}'")> _
        Public Property ModelKendaraan() As String
            Get
                Return _modelKendaraan
            End Get
            Set(ByVal value As String)
                _modelKendaraan = value
            End Set
        End Property

        <ColumnInfo("KTP", "'{0}'")> _
        Public Property KTP() As String
            Get
                Return _ktp
            End Get
            Set(ByVal value As String)
                _ktp = value
            End Set
        End Property

        <ColumnInfo("CityCustomer", "'{0}'")> _
        Public Property CityCustomer() As String
            Get
                Return _cityCustomer
            End Get
            Set(ByVal value As String)
                _cityCustomer = value
            End Set
        End Property


#End Region

#Region "Generated Method"
        Public Function GetStrDate(ByVal dateInput As DateTime, ByVal dateFormat As String) As String
            If dateInput = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                Return ""
            Else
                Return Format(dateInput, dateFormat)
            End If
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

