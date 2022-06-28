
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_QuickProduct Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 18/04/2018 - 9:24:03
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
    <Serializable(), TableInfo("VWI_QuickProductAll")>
    Public Class VWI_QuickProductAll
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Long)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Long
        Private _dealerCode As String = String.Empty
        Private _vehicleType As String = String.Empty
        Private _vehicleDesc As String = String.Empty
        Private _productCategory As String = String.Empty
        Private _vehicleCatDesc As String = String.Empty
        Private _colorCode As String = String.Empty
        Private _colorDescription As String = String.Empty
        Private _vehicleBrand As String = String.Empty
        Private _vehicleModel_S1 As String = String.Empty
        Private _vehicleCategory_S2 As String = String.Empty
        Private _productSegment_S3 As String = String.Empty
        Private _driveSystem_S4 As String = String.Empty
        Private _variantType As String = String.Empty
        Private _transmitType As String = String.Empty
        Private _driveSystemType As String = String.Empty
        Private _speedType As String = String.Empty
        Private _fuelType As String = String.Empty
        Private _specialFlag As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As Integer
        Private _modelYear As Integer

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")>
        Public Property ID As Long
            Get
                Return _iD
            End Get
            Set(ByVal value As Long)
                _iD = value
            End Set
        End Property

        <ColumnInfo("DealerCode", "'{0}'")>
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property

        <ColumnInfo("VehicleType", "'{0}'")>
        Public Property VehicleType As String
            Get
                Return _vehicleType
            End Get
            Set(ByVal value As String)
                _vehicleType = value
            End Set
        End Property


        <ColumnInfo("VehicleDesc", "'{0}'")>
        Public Property VehicleDesc As String
            Get
                Return _vehicleDesc
            End Get
            Set(ByVal value As String)
                _vehicleDesc = value
            End Set
        End Property


        <ColumnInfo("ProductCategory", "'{0}'")>
        Public Property ProductCategory As String
            Get
                Return _productCategory
            End Get
            Set(ByVal value As String)
                _productCategory = value
            End Set
        End Property


        <ColumnInfo("VehicleCatDesc", "'{0}'")>
        Public Property VehicleCatDesc As String
            Get
                Return _vehicleCatDesc
            End Get
            Set(ByVal value As String)
                _vehicleCatDesc = value
            End Set
        End Property


        <ColumnInfo("ColorCode", "'{0}'")>
        Public Property ColorCode As String
            Get
                Return _colorCode
            End Get
            Set(ByVal value As String)
                _colorCode = value
            End Set
        End Property


        <ColumnInfo("ColorDescription", "'{0}'")>
        Public Property ColorDescription As String
            Get
                Return _colorDescription
            End Get
            Set(ByVal value As String)
                _colorDescription = value
            End Set
        End Property


        <ColumnInfo("VehicleBrand", "'{0}'")>
        Public Property VehicleBrand As String
            Get
                Return _vehicleBrand
            End Get
            Set(ByVal value As String)
                _vehicleBrand = value
            End Set
        End Property


        <ColumnInfo("VehicleModel_S1", "'{0}'")>
        Public Property VehicleModel_S1 As String
            Get
                Return _vehicleModel_S1
            End Get
            Set(ByVal value As String)
                _vehicleModel_S1 = value
            End Set
        End Property


        <ColumnInfo("VehicleCategory_S2", "'{0}'")>
        Public Property VehicleCategory_S2 As String
            Get
                Return _vehicleCategory_S2
            End Get
            Set(ByVal value As String)
                _vehicleCategory_S2 = value
            End Set
        End Property


        <ColumnInfo("ProductSegment_S3", "'{0}'")>
        Public Property ProductSegment_S3 As String
            Get
                Return _productSegment_S3
            End Get
            Set(ByVal value As String)
                _productSegment_S3 = value
            End Set
        End Property


        <ColumnInfo("DriveSystem_S4", "'{0}'")>
        Public Property DriveSystem_S4 As String
            Get
                Return _driveSystem_S4
            End Get
            Set(ByVal value As String)
                _driveSystem_S4 = value
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

        <ColumnInfo("SpeedType", "'{0}'")>
        Public Property SpeedType As String
            Get
                Return _speedType
            End Get
            Set(ByVal value As String)
                _speedType = value
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

        <ColumnInfo("SpecialFlag", "'{0}'")>
        Public Property SpecialFlag As String
            Get
                Return _specialFlag
            End Get
            Set(ByVal value As String)
                _specialFlag = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")>
        Public Property Status As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
            End Set
        End Property

        <ColumnInfo("ModelYear", "{0}")>
        Public Property ModelYear As Integer
            Get
                Return _modelYear
            End Get
            Set(ByVal value As Integer)
                _modelYear = value
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
