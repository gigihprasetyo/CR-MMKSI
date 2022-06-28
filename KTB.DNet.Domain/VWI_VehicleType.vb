#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ActivityType Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2009 - 10:41:42 AM
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
    <Serializable(), TableInfo("VWI_VehicleType")> _
    Public Class VWI_VehicleType
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Friend AlertMaster As AlertMaster

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"
        
        Private _iD As Integer
        Private _VehicleDesc As String = String.Empty
        Private _VehicleType As String = String.Empty
        Private _ProductCategory As String = String.Empty
        Private _VehicleModel_S1 As String = String.Empty
        Private _VehicleCategory_S2 As String = String.Empty
        Private _ProductSegment_S3 As String = String.Empty
        Private _DriveSystem_S4 As String = String.Empty
        Private _Status As Integer
        Private _VariantType As String = String.Empty
        Private _TransmitType As String = String.Empty
        Private _DriveSystemType As String = String.Empty
        Private _SpeedType As String = String.Empty
        Private _FuelType As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("VehicleType", "'{0}'")> _
        Public Property VehicleType() As String
            Get
                Return _VehicleType
            End Get
            Set(ByVal value As String)
                _VehicleType = value
            End Set
        End Property

        <ColumnInfo("VehicleDesc", "'{0}'")> _
        Public Property VehicleDesc() As String
            Get
                Return _VehicleDesc
            End Get
            Set(ByVal value As String)
                _VehicleDesc = value
            End Set
        End Property

        <ColumnInfo("ProductCategory", "'{0}'")> _
        Public Property ProductCategory() As String
            Get
                Return _ProductCategory
            End Get
            Set(ByVal value As String)
                _ProductCategory = value
            End Set
        End Property

        <ColumnInfo("VehicleModel_S1", "'{0}'")> _
        Public Property VehicleModel_S1() As String
            Get
                Return _VehicleModel_S1
            End Get
            Set(ByVal value As String)
                _VehicleModel_S1 = value
            End Set
        End Property

        <ColumnInfo("VehicleCategory_S2", "'{0}'")> _
        Public Property VehicleCategory_S2() As String
            Get
                Return _VehicleCategory_S2
            End Get
            Set(ByVal value As String)
                _VehicleCategory_S2 = value
            End Set
        End Property

        <ColumnInfo("ProductSegment_S3", "'{0}'")> _
        Public Property ProductSegment_S3() As String
            Get
                Return _ProductSegment_S3
            End Get
            Set(ByVal value As String)
                _ProductSegment_S3 = value
            End Set
        End Property

        <ColumnInfo("DriveSystem_S4", "'{0}'")> _
        Public Property DriveSystem_S4() As String
            Get
                Return _DriveSystem_S4
            End Get
            Set(ByVal value As String)
                _DriveSystem_S4 = value
            End Set
        End Property

        <ColumnInfo("VariantType", "'{0}'")> _
        Public Property VariantType() As String
            Get
                Return _VariantType
            End Get
            Set(ByVal value As String)
                _VariantType = value
            End Set
        End Property

        <ColumnInfo("TransmitType", "'{0}'")> _
        Public Property TransmitType() As String
            Get
                Return _TransmitType
            End Get
            Set(ByVal value As String)
                _TransmitType = value
            End Set
        End Property

        <ColumnInfo("DriveSystemType", "'{0}'")> _
        Public Property DriveSystemType() As String
            Get
                Return _DriveSystemType
            End Get
            Set(ByVal value As String)
                _DriveSystemType = value
            End Set
        End Property

        <ColumnInfo("SpeedType", "'{0}'")> _
        Public Property SpeedType() As String
            Get
                Return _SpeedType
            End Get
            Set(ByVal value As String)
                _SpeedType = value
            End Set
        End Property

        <ColumnInfo("FuelType", "'{0}'")> _
        Public Property FuelType() As String
            Get
                Return _FuelType
            End Get
            Set(ByVal value As String)
                _FuelType = value
            End Set
        End Property

        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Integer
            Get
                Return _Status
            End Get
            Set(ByVal value As Integer)
                _Status = value
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


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
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


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
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

