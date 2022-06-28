#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : POReceipt Dealer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 08/03/2018 - 22:03:46
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
    <Serializable(), TableInfo("VWI_POReceiptDealer")>
    Public Class VWI_POReceiptDealer
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _endCustomerID As Integer
        Private _chassisNumber As String = String.Empty
        Private _categoryID As Short
        Private _dONumber As String = String.Empty
        Private _sONumber As String = String.Empty
        Private _discountAmount As Decimal
        Private _engineNumber As String = String.Empty
        Private _serialNumber As String = String.Empty
        Private _pONumber As String = String.Empty
        Private _dODate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _gIDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _parkingAmount As Decimal
        Private _productionYear As Integer
        Private _vehicleColorCode As String = String.Empty
        Private _vehicleColorDesc As String = String.Empty
        Private _materialNumber As String = String.Empty
        Private _materialDescription As String = String.Empty
        Private _vehicleKindCode As String = String.Empty
        Private _VehicleKindDesc As String = String.Empty
        Private _vehicleTypeCode As String = String.Empty
        Private _vehicleTypeDesc As String = String.Empty
        Private _soldDealerCode As String = String.Empty
        Private _soldDealerName As String = String.Empty
        Private _aTDDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _eTADate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _aTADate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _segmentType As String = String.Empty
        Private _fuelType As String = String.Empty
        Private _transmitType As String = String.Empty
        Private _driveSystemType As String = String.Empty
        Private _variantType As String = String.Empty
        Private _speedType As String = String.Empty


#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")>
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property

        <ColumnInfo("EndCustomerID", "{0}")>
        Public Property EndCustomerID As Integer
            Get
                Return _endCustomerID
            End Get
            Set(ByVal value As Integer)
                _endCustomerID = value
            End Set
        End Property

        <ColumnInfo("ChassisNumber", "'{0}'")>
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property

        <ColumnInfo("CategoryID", "'{0}'")>
        Public Property CategoryID As Short
            Get
                Return _categoryID
            End Get
            Set(ByVal value As Short)
                _categoryID = value
            End Set
        End Property

        <ColumnInfo("DONumber", "'{0}'")>
        Public Property DONumber As String
            Get
                Return _dONumber
            End Get
            Set(ByVal value As String)
                _dONumber = value
            End Set
        End Property

        <ColumnInfo("SONumber", "'{0}'")>
        Public Property SONumber As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
            End Set
        End Property

        <ColumnInfo("EngineNumber", "'{0}'")>
        Public Property EngineNumber As String
            Get
                Return _engineNumber
            End Get
            Set(ByVal value As String)
                _engineNumber = value
            End Set
        End Property

        <ColumnInfo("SerialNumber", "'{0}'")>
        Public Property SerialNumber As String
            Get
                Return _serialNumber
            End Get
            Set(ByVal value As String)
                _serialNumber = value
            End Set
        End Property

        <ColumnInfo("PONumber", "'{0}'")>
        Public Property PONumber As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
            End Set
        End Property

        <ColumnInfo("DODate", "'{0:yyyy/MM/dd}'")>
        Public Property DODate As DateTime
            Get
                Return _dODate
            End Get
            Set(ByVal value As DateTime)
                _dODate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("GIDate", "'{0:yyyy/MM/dd}'")>
        Public Property GIDate As DateTime
            Get
                Return _gIDate
            End Get
            Set(ByVal value As DateTime)
                _gIDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("VehicleColorCode", "'{0}'")>
        Public Property VehicleColorCode As String
            Get
                Return _vehicleColorCode
            End Get
            Set(ByVal value As String)
                _vehicleColorCode = value
            End Set
        End Property

        <ColumnInfo("VehicleColorDesc", "'{0}'")>
        Public Property VehicleColorDesc As String
            Get
                Return _vehicleColorDesc
            End Get
            Set(ByVal value As String)
                _vehicleColorDesc = value
            End Set
        End Property

        <ColumnInfo("VehicleTypeCode", "'{0}'")>
        Public Property VehicleTypeCode As String
            Get
                Return _vehicleTypeCode
            End Get
            Set(ByVal value As String)
                _vehicleTypeCode = value
            End Set
        End Property

        <ColumnInfo("VehicleTypeDesc", "'{0}'")>
        Public Property VehicleTypeDesc As String
            Get
                Return _vehicleTypeDesc
            End Get
            Set(ByVal value As String)
                _vehicleTypeDesc = value
            End Set
        End Property

        <ColumnInfo("MaterialNumber", "'{0}'")>
        Public Property MaterialNumber As String
            Get
                Return _materialNumber
            End Get
            Set(ByVal value As String)
                _materialNumber = value
            End Set
        End Property

        <ColumnInfo("MaterialDescription", "'{0}'")>
        Public Property MaterialDescription As String
            Get
                Return _materialDescription
            End Get
            Set(ByVal value As String)
                _materialDescription = value
            End Set
        End Property

        <ColumnInfo("VehicleKindCode", "'{0}'")>
        Public Property VehicleKindCode As String
            Get
                Return _vehicleKindCode
            End Get
            Set(ByVal value As String)
                _vehicleKindCode = value
            End Set
        End Property

        <ColumnInfo("VehicleKindDesc", "'{0}'")>
        Public Property VehicleKindDesc As String
            Get
                Return _VehicleKindDesc
            End Get
            Set(ByVal value As String)
                _VehicleKindDesc = value
            End Set
        End Property

        <ColumnInfo("SoldDealerCode", "'{0}'")>
        Public Property SoldDealerCode As String
            Get
                Return _soldDealerCode
            End Get
            Set(ByVal value As String)
                _soldDealerCode = value
            End Set
        End Property

        <ColumnInfo("SoldDealerName", "'{0}'")>
        Public Property SoldDealerName As String
            Get
                Return _soldDealerName
            End Get
            Set(ByVal value As String)
                _soldDealerName = value
            End Set
        End Property

        <ColumnInfo("DiscountAmount", "#,##0")>
        Public Property DiscountAmount As Decimal
            Get
                Return _discountAmount
            End Get
            Set(ByVal value As Decimal)
                _discountAmount = value
            End Set
        End Property

        <ColumnInfo("ParkingAmount", "#,##0")>
        Public Property ParkingAmount As Decimal
            Get
                Return _parkingAmount
            End Get
            Set(ByVal value As Decimal)
                _parkingAmount = value
            End Set
        End Property

        <ColumnInfo("ProductionYear", "{0}")>
        Public Property ProductionYear As Integer
            Get
                Return _productionYear
            End Get
            Set(ByVal value As Integer)
                _productionYear = value
            End Set
        End Property

        <ColumnInfo("ATDDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property ATDDate As DateTime
            Get
                Return _aTDDate
            End Get
            Set(ByVal value As DateTime)
                _aTDDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("ETADate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property ETADate As DateTime
            Get
                Return _eTADate
            End Get
            Set(ByVal value As DateTime)
                _eTADate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("ATADate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property ATADate As DateTime
            Get
                Return _aTADate
            End Get
            Set(ByVal value As DateTime)
                _aTADate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("SegmentType", "'{0}'")>
        Public Property SegmentType As String
            Get
                Return _segmentType
            End Get
            Set(ByVal value As String)
                _segmentType = value
            End Set
        End Property

        <ColumnInfo("FuelType", "'{0}'")>
        Public Property FuelType As String
            Get
                Return _fuelType
            End Get
            Set(ByVal value As String)
                _fuelType = value
            End Set
        End Property

        <ColumnInfo("TransmitType", "'{0}'")>
        Public Property TransmitType As String
            Get
                Return _transmitType
            End Get
            Set(ByVal value As String)
                _transmitType = value
            End Set
        End Property

        <ColumnInfo("DriveSystemType", "'{0}'")>
        Public Property DriveSystemType As String
            Get
                Return _driveSystemType
            End Get
            Set(ByVal value As String)
                _driveSystemType = value
            End Set
        End Property

        <ColumnInfo("VariantType", "'{0}'")>
        Public Property VariantType As String
            Get
                Return _variantType
            End Get
            Set(ByVal value As String)
                _variantType = value
            End Set
        End Property

        <ColumnInfo("SpeedType", "'{0}'")>
        Public Property SpeedType As String
            Get
                Return _speedType
            End Get
            Set(ByVal value As String)
                _speedType = value
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