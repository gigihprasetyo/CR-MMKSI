
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : MSPMembership Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 21/03/2018 - 14:30:17
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
    <Serializable(), TableInfo("VWI_MSPExMembership")>
    Public Class VWI_MSPExMembership
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
        Private _mSPCustomerID As Integer
        Private _dealerId As Integer
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _chassisMasterID As Integer
        Private _mspCode As String = String.Empty
        Private _chassisNumber As String = String.Empty
        Private _colorCode As String = String.Empty
        Private _VehicleTypeCode As String = String.Empty
        Private _vehicleTypeDesc As String = String.Empty
        Private _mspKm As Integer
        Private _duration As Short
        Private _description As String = String.Empty
        Private _validUntil As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _registrationDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

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


        <ColumnInfo("MSPCustomerID", "{0}")>
        Public Property MSPCustomerID As Integer
            Get
                Return _mSPCustomerID
            End Get
            Set(ByVal value As Integer)
                _mSPCustomerID = value
            End Set
        End Property


        <ColumnInfo("DealerId", "{0}")>
        Public Property DealerId As Integer
            Get
                Return _dealerId
            End Get
            Set(ByVal value As Integer)
                _dealerId = value
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


        <ColumnInfo("DealerName", "'{0}'")>
        Public Property DealerName As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        <ColumnInfo("ChassisMasterID", "{0}")>
        Public Property ChassisMasterID As Integer
            Get
                Return _chassisMasterID
            End Get
            Set(ByVal value As Integer)
                _chassisMasterID = value
            End Set
        End Property


        <ColumnInfo("MSPCode", "'{0}'")>
        Public Property MSPCode As String
            Get
                Return _mspCode
            End Get
            Set(ByVal value As String)
                _mspCode = value
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


        <ColumnInfo("ColorCode", "{0}")>
        Public Property ColorCode As String
            Get
                Return _colorCode
            End Get
            Set(ByVal value As String)
                _colorCode = value
            End Set
        End Property

        <ColumnInfo("VehicleTypeCode", "{0}")>
        Public Property VehicleTypeCode As String
            Get
                Return _VehicleTypeCode
            End Get
            Set(ByVal value As String)
                _VehicleTypeCode = value
            End Set
        End Property

        <ColumnInfo("VehicleTypeDesc", "{0}")>
        Public Property VehicleTypeDesc As String
            Get
                Return _vehicleTypeDesc
            End Get
            Set(ByVal value As String)
                _vehicleTypeDesc = value
            End Set
        End Property


        <ColumnInfo("MSPKm", "{0}")>
        Public Property MSPKm As Integer
            Get
                Return _mspKm
            End Get
            Set(ByVal value As Integer)
                _mspKm = value
            End Set
        End Property


        <ColumnInfo("Duration", "{0}")>
        Public Property Duration As Short
            Get
                Return _duration
            End Get
            Set(ByVal value As Short)
                _duration = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")>
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("ValidUntil", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property ValidUntil As DateTime
            Get
                Return _validUntil
            End Get
            Set(ByVal value As DateTime)
                _validUntil = value
            End Set
        End Property


        <ColumnInfo("RegistrationDate", "'{0:yyyy/MM/dd}'")>
        Public Property RegistrationDate As DateTime
            Get
                Return _registrationDate
            End Get
            Set(ByVal value As DateTime)
                _registrationDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd}'")>
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = New DateTime(value.Year, value.Month, value.Day)
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

