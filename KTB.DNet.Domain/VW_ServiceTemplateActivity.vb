
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VW_ServiceTemplateActivity Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/10/2018 - 14:13:52
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
    <Serializable(), TableInfo("VW_ServiceTemplateActivity")>
    Public Class VW_ServiceTemplateActivity
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As String)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"
        Private _iD As Integer
        Private _serviceType As String = String.Empty
        Private _serviceTemplateHeaderID As Integer
        Private _kindID As Integer
        Private _vechileTypeID As Integer
        Private _validFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _kindCode As String = String.Empty
        Private _kindDescription As String = String.Empty
        Private _vehicleTypeCode As String = String.Empty
        Private _actName As String = String.Empty
        Private _actSequence As String = String.Empty
        Private _duration As Decimal
        Private _svcTemplateActivity As String = String.Empty

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


        <ColumnInfo("ServiceType", "'{0}'")>
        Public Property ServiceType As String
            Get
                Return _serviceType
            End Get
            Set(ByVal value As String)
                _serviceType = value
            End Set
        End Property

        <ColumnInfo("ServiceTemplateHeaderID", "{0}")>
        Public Property ServiceTemplateHeaderID As Integer
            Get
                Return _serviceTemplateHeaderID
            End Get
            Set(ByVal value As Integer)
                _serviceTemplateHeaderID = value
            End Set
        End Property

        <ColumnInfo("KindID", "{0}")>
        Public Property KindID As Integer
            Get
                Return _kindID
            End Get
            Set(ByVal value As Integer)
                _kindID = value
            End Set
        End Property

        <ColumnInfo("VechileTypeID", "{0}")>
        Public Property VechileTypeID As Integer
            Get
                Return _vechileTypeID
            End Get
            Set(ByVal value As Integer)
                _vechileTypeID = value
            End Set
        End Property

        <ColumnInfo("ValidFrom", "'{0:yyyy/MM/dd}'")>
        Public Property ValidFrom As DateTime
            Get
                Return _validFrom
            End Get
            Set(ByVal value As DateTime)
                _validFrom = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("KindCode", "'{0}'")>
        Public Property KindCode As String
            Get
                Return _kindCode
            End Get
            Set(ByVal value As String)
                _kindCode = value
            End Set
        End Property

        <ColumnInfo("KindDescription", "'{0}'")>
        Public Property KindDescription As String
            Get
                Return _kindDescription
            End Get
            Set(ByVal value As String)
                _kindDescription = value
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

        <ColumnInfo("ActName", "'{0}'")>
        Public Property ActName As String
            Get
                Return _actName
            End Get
            Set(ByVal value As String)
                _actName = value
            End Set
        End Property

        <ColumnInfo("ActSequence", "'{0}'")>
        Public Property ActSequence As String
            Get
                Return _actSequence
            End Get
            Set(ByVal value As String)
                _actSequence = value
            End Set
        End Property

        <ColumnInfo("Duration", "{0}")>
        Public Property Duration As Decimal
            Get
                Return _duration
            End Get
            Set(ByVal value As Decimal)
                _duration = value
            End Set
        End Property

        <ColumnInfo("SVCTemplateActivity", "'{0}'")>
        Public Property SVCTemplateActivity As String
            Get
                Return _svcTemplateActivity
            End Get
            Set(ByVal value As String)
                _svcTemplateActivity = value
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

