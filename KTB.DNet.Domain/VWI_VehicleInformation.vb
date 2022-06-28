
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_VehicleInformation Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 01/10/2018 - 15:17:14
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
    <Serializable(), TableInfo("VWI_VehicleInformation")> _
    Public Class VWI_VehicleInformation
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
        Private _chassisNumber As String = String.Empty
        Private _isBB As Integer
        Private _categoryCode As String = String.Empty
        Private _categoryDesc As String = String.Empty
        Private _colorCode As String = String.Empty
        Private _colorIndName As String = String.Empty
        Private _colorEngName As String = String.Empty
        Private _materialDescription As String = String.Empty
        Private _vehicleTypeCode As String = String.Empty
        Private _vehicleTypeDesc As String = String.Empty
        Private _modelSearchTerm1 As String = String.Empty
        Private _modelSearchTerm2 As String = String.Empty
        Private _segmentType As String = String.Empty
        Private _fuelType As String = String.Empty
        Private _transmitType As String = String.Empty
        Private _driveSystemType As String = String.Empty
        Private _variantType As String = String.Empty
        Private _vehicleBrand As String = String.Empty
        Private _speedType As String = String.Empty
        Private _vehicleKindID As Integer
        Private _code As String = String.Empty
        Private _vehicleKindDesc As String = String.Empty
        Private _soldDealerID As Short
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        'Private _engineNumber As String = String.Empty
        Private _serialNumber As String = String.Empty
        Private _productionYear As Short
        Private _fleetCode As String = String.Empty
        Private _openFakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fSExtended As String = String.Empty
        Private _fSProgram As String = String.Empty
        Private _pKTDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _wSCDuration As Integer
        Private _wSCStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _wSCEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _webModel As String = String.Empty
        Private _webVariant As String = String.Empty
        Private _colorWeb As String = String.Empty

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property


        <ColumnInfo("IsBB", "{0}")> _
        Public Property IsBB As Integer
            Get
                Return _isBB
            End Get
            Set(ByVal value As Integer)
                _isBB = value
            End Set
        End Property


        <ColumnInfo("CategoryCode", "'{0}'")> _
        Public Property CategoryCode As String
            Get
                Return _categoryCode
            End Get
            Set(ByVal value As String)
                _categoryCode = value
            End Set
        End Property


        <ColumnInfo("CategoryDesc", "'{0}'")> _
        Public Property CategoryDesc As String
            Get
                Return _categoryDesc
            End Get
            Set(ByVal value As String)
                _categoryDesc = value
            End Set
        End Property


        <ColumnInfo("ColorCode", "'{0}'")> _
        Public Property ColorCode As String
            Get
                Return _colorCode
            End Get
            Set(ByVal value As String)
                _colorCode = value
            End Set
        End Property


        <ColumnInfo("ColorIndName", "'{0}'")> _
        Public Property ColorIndName As String
            Get
                Return _colorIndName
            End Get
            Set(ByVal value As String)
                _colorIndName = value
            End Set
        End Property


        <ColumnInfo("ColorEngName", "'{0}'")> _
        Public Property ColorEngName As String
            Get
                Return _colorEngName
            End Get
            Set(ByVal value As String)
                _colorEngName = value
            End Set
        End Property


        <ColumnInfo("MaterialDescription", "'{0}'")> _
        Public Property MaterialDescription As String
            Get
                Return _materialDescription
            End Get
            Set(ByVal value As String)
                _materialDescription = value
            End Set
        End Property


        <ColumnInfo("VehicleTypeCode", "'{0}'")> _
        Public Property VehicleTypeCode As String
            Get
                Return _vehicleTypeCode
            End Get
            Set(ByVal value As String)
                _vehicleTypeCode = value
            End Set
        End Property


        <ColumnInfo("VehicleTypeDesc", "'{0}'")> _
        Public Property VehicleTypeDesc As String
            Get
                Return _vehicleTypeDesc
            End Get
            Set(ByVal value As String)
                _vehicleTypeDesc = value
            End Set
        End Property


        <ColumnInfo("ModelSearchTerm1", "'{0}'")> _
        Public Property ModelSearchTerm1 As String
            Get
                Return _modelSearchTerm1
            End Get
            Set(ByVal value As String)
                _modelSearchTerm1 = value
            End Set
        End Property


        <ColumnInfo("ModelSearchTerm2", "'{0}'")> _
        Public Property ModelSearchTerm2 As String
            Get
                Return _modelSearchTerm2
            End Get
            Set(ByVal value As String)
                _modelSearchTerm2 = value
            End Set
        End Property


        <ColumnInfo("SegmentType", "'{0}'")> _
        Public Property SegmentType As String
            Get
                Return _segmentType
            End Get
            Set(ByVal value As String)
                _segmentType = value
            End Set
        End Property


        <ColumnInfo("FuelType", "'{0}'")> _
        Public Property FuelType As String
            Get
                Return _fuelType
            End Get
            Set(ByVal value As String)
                _fuelType = value
            End Set
        End Property


        <ColumnInfo("TransmitType", "'{0}'")> _
        Public Property TransmitType As String
            Get
                Return _transmitType
            End Get
            Set(ByVal value As String)
                _transmitType = value
            End Set
        End Property


        <ColumnInfo("DriveSystemType", "'{0}'")> _
        Public Property DriveSystemType As String
            Get
                Return _driveSystemType
            End Get
            Set(ByVal value As String)
                _driveSystemType = value
            End Set
        End Property


        <ColumnInfo("VariantType", "'{0}'")> _
        Public Property VariantType As String
            Get
                Return _variantType
            End Get
            Set(ByVal value As String)
                _variantType = value
            End Set
        End Property


        <ColumnInfo("VehicleBrand", "'{0}'")> _
        Public Property VehicleBrand As String
            Get
                Return _vehicleBrand
            End Get
            Set(ByVal value As String)
                _vehicleBrand = value
            End Set
        End Property


        <ColumnInfo("SpeedType", "'{0}'")> _
        Public Property SpeedType As String
            Get
                Return _speedType
            End Get
            Set(ByVal value As String)
                _speedType = value
            End Set
        End Property


        <ColumnInfo("VehicleKindID", "{0}")> _
        Public Property VehicleKindID As Integer
            Get
                Return _vehicleKindID
            End Get
            Set(ByVal value As Integer)
                _vehicleKindID = value
            End Set
        End Property


        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property


        <ColumnInfo("VehicleKindDesc", "'{0}'")> _
        Public Property VehicleKindDesc As String
            Get
                Return _vehicleKindDesc
            End Get
            Set(ByVal value As String)
                _vehicleKindDesc = value
            End Set
        End Property


        <ColumnInfo("SoldDealerID", "{0}")> _
        Public Property SoldDealerID As Short
            Get
                Return _soldDealerID
            End Get
            Set(ByVal value As Short)
                _soldDealerID = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        '<ColumnInfo("EngineNumber", "'{0}'")> _
        'Public Property EngineNumber As String
        '    Get
        '        Return _engineNumber
        '    End Get
        '    Set(ByVal value As String)
        '        _engineNumber = value
        '    End Set
        'End Property


        <ColumnInfo("SerialNumber", "'{0}'")> _
        Public Property SerialNumber As String
            Get
                Return _serialNumber
            End Get
            Set(ByVal value As String)
                _serialNumber = value
            End Set
        End Property


        <ColumnInfo("ProductionYear", "{0}")> _
        Public Property ProductionYear As Short
            Get
                Return _productionYear
            End Get
            Set(ByVal value As Short)
                _productionYear = value
            End Set
        End Property


        <ColumnInfo("FleetCode", "'{0}'")> _
        Public Property FleetCode As String
            Get
                Return _fleetCode
            End Get
            Set(ByVal value As String)
                _fleetCode = value
            End Set
        End Property


        <ColumnInfo("OpenFakturDate", "'{0:yyyy/MM/dd}'")> _
        Public Property OpenFakturDate As DateTime
            Get
                Return _openFakturDate
            End Get
            Set(ByVal value As DateTime)
                _openFakturDate = value
            End Set
        End Property


        <ColumnInfo("FakturDate", "'{0:yyyy/MM/dd}'")> _
        Public Property FakturDate As DateTime
            Get
                Return _fakturDate
            End Get
            Set(ByVal value As DateTime)
                _fakturDate = value
            End Set
        End Property


        <ColumnInfo("FSExtended", "'{0}'")> _
        Public Property FSExtended As String
            Get
                Return _fSExtended
            End Get
            Set(ByVal value As String)
                _fSExtended = value
            End Set
        End Property

        <ColumnInfo("FSProgram", "'{0}'")> _
        Public Property FSProgram As String
            Get
                Return _fSProgram
            End Get
            Set(ByVal value As String)
                _fSProgram = value
            End Set
        End Property


        <ColumnInfo("PKTDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PKTDate As DateTime
            Get
                Return _pKTDate
            End Get
            Set(ByVal value As DateTime)
                _pKTDate = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property

        <ColumnInfo("WSCDuration", "{0}")> _
        Public Property WSCDuration As Integer
            Get
                Return _wSCDuration
            End Get
            Set(ByVal value As Integer)
                _wSCDuration = value
            End Set
        End Property

        <ColumnInfo("WSCStart", "'{0:yyyy/MM/dd}'")> _
        Public Property WSCStart As DateTime
            Get
                Return _wSCStart
            End Get
            Set(ByVal value As DateTime)
                _wSCStart = value
            End Set
        End Property

        <ColumnInfo("WSCEnd", "'{0:yyyy/MM/dd}'")> _
        Public Property WSCEnd As DateTime
            Get
                Return _wSCEnd
            End Get
            Set(ByVal value As DateTime)
                _wSCEnd = value
            End Set
        End Property

        <ColumnInfo("WebModel", "'{0}'")> _
        Public Property WebModel As String
            Get
                Return _webModel
            End Get
            Set(ByVal value As String)
                _webModel = value
            End Set
        End Property

        <ColumnInfo("WebVariant", "'{0}'")> _
        Public Property WebVariant As String
            Get
                Return _webVariant
            End Get
            Set(ByVal value As String)
                _webVariant = value
            End Set
        End Property

        <ColumnInfo("ColorWeb", "'{0}'")> _
        Public Property ColorWeb As String
            Get
                Return _colorWeb
            End Get
            Set(ByVal value As String)
                _colorWeb = value
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

