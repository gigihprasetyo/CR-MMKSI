
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_VehicleSpecification Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 27/07/2018 - 10:38:25
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
    <Serializable(), TableInfo("VWI_VehicleSpecification")> _
    Public Class VWI_VehicleSpecification
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
        Private _vehicleCategory_S1 As String = String.Empty
        Private _classificationNumber As String = String.Empty
        Private _vehicleDesc As String = String.Empty
        Private _productCategory As String = String.Empty
        Private _vehicleCatDesc As String = String.Empty
        Private _vehicleBrand As String = String.Empty
        Private _speedType As String = String.Empty
        Private _fuelType As String = String.Empty
        Private _transmition As String = String.Empty
        Private _drivesystem As String = String.Empty
        Private _segmentType As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As Integer




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


        <ColumnInfo("VehicleCategory_S1", "'{0}'")> _
        Public Property VehicleCategory_S1 As String
            Get
                Return _vehicleCategory_S1
            End Get
            Set(ByVal value As String)
                _vehicleCategory_S1 = value
            End Set
        End Property


        <ColumnInfo("ClassificationNumber", "'{0}'")> _
        Public Property ClassificationNumber As String
            Get
                Return _classificationNumber
            End Get
            Set(ByVal value As String)
                _classificationNumber = value
            End Set
        End Property


        <ColumnInfo("VehicleDesc", "'{0}'")> _
        Public Property VehicleDesc As String
            Get
                Return _vehicleDesc
            End Get
            Set(ByVal value As String)
                _vehicleDesc = value
            End Set
        End Property


        <ColumnInfo("ProductCategory", "'{0}'")> _
        Public Property ProductCategory As String
            Get
                Return _productCategory
            End Get
            Set(ByVal value As String)
                _productCategory = value
            End Set
        End Property


        <ColumnInfo("VehicleCatDesc", "'{0}'")> _
        Public Property VehicleCatDesc As String
            Get
                Return _vehicleCatDesc
            End Get
            Set(ByVal value As String)
                _vehicleCatDesc = value
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


        <ColumnInfo("FuelType", "'{0}'")> _
        Public Property FuelType As String
            Get
                Return _fuelType
            End Get
            Set(ByVal value As String)
                _fuelType = value
            End Set
        End Property


        <ColumnInfo("Transmition", "'{0}'")> _
        Public Property Transmition As String
            Get
                Return _transmition
            End Get
            Set(ByVal value As String)
                _transmition = value
            End Set
        End Property


        <ColumnInfo("Drivesystem", "'{0}'")> _
        Public Property Drivesystem As String
            Get
                Return _drivesystem
            End Get
            Set(ByVal value As String)
                _drivesystem = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
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

