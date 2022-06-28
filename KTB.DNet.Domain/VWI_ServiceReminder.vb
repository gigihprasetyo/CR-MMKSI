
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceReminder Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 03/07/2020 - 12:24:21
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
    <Serializable(), TableInfo("VWI_ServiceReminder")>
    Public Class VWI_ServiceReminder
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
        Private _salesforceID As String = String.Empty
        Private _dealerCode As String
        Private _chassisNumber As String = String.Empty
        Private _chassisMasterID As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _engineNumber As String = String.Empty
        Private _vehicleType As String = String.Empty
        Private _wONumber As String = String.Empty
        Private _serviceReminderDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _maxFUDealerDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _bookingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _bookingTime As TimeSpan
        Private _caseNumber As String = String.Empty
        Private _assistServiceIncomingID As Integer
        Private _serviceActualDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _customerName As String = String.Empty
        Private _customerPhoneNumber As String = String.Empty
        Private _contactPersonName As String = String.Empty
        Private _contactPersonPhoneNumber As String = String.Empty
        Private _serviceType As String
        Private _transactionType As Byte
        Private _actualKM As Integer
        Private _status As Byte
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

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


        <ColumnInfo("SalesforceID", "'{0}'")>
        Public Property SalesforceID As String
            Get
                Return _salesforceID
            End Get
            Set(ByVal value As String)
                _salesforceID = value
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

        <ColumnInfo("ChassisNumber", "'{0}'")>
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
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

        <ColumnInfo("VehicleType", "'{0}'")>
        Public Property VehicleType As String
            Get
                Return _vehicleType
            End Get
            Set(ByVal value As String)
                _vehicleType = value
            End Set
        End Property


        <ColumnInfo("WONumber", "'{0}'")>
        Public Property WONumber As String
            Get
                Return _wONumber
            End Get
            Set(ByVal value As String)
                _wONumber = value
            End Set
        End Property


        <ColumnInfo("ServiceReminderDate", "'{0:yyyy/MM/dd}'")>
        Public Property ServiceReminderDate As DateTime
            Get
                Return _serviceReminderDate
            End Get
            Set(ByVal value As DateTime)
                _serviceReminderDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("MaxFUDealerDate", "'{0:yyyy/MM/dd}'")>
        Public Property MaxFUDealerDate As DateTime
            Get
                Return _maxFUDealerDate
            End Get
            Set(ByVal value As DateTime)
                _maxFUDealerDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("BookingDate", "'{0:yyyy/MM/dd}'")>
        Public Property BookingDate As DateTime
            Get
                Return _bookingDate
            End Get
            Set(ByVal value As DateTime)
                _bookingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("BookingTime", "{0}")>
        Public Property BookingTime As TimeSpan
            Get
                Return _bookingTime
            End Get
            Set(ByVal value As TimeSpan)
                _bookingTime = value
            End Set
        End Property


        <ColumnInfo("CaseNumber", "'{0}'")>
        Public Property CaseNumber As String
            Get
                Return _caseNumber
            End Get
            Set(ByVal value As String)
                _caseNumber = value
            End Set
        End Property


        <ColumnInfo("AssistServiceIncomingID", "{0}")>
        Public Property AssistServiceIncomingID As Integer
            Get
                Return _assistServiceIncomingID
            End Get
            Set(ByVal value As Integer)
                _assistServiceIncomingID = value
            End Set
        End Property

        <ColumnInfo("ServiceActualDate", "'{0:yyyy/MM/dd}'")>
        Public Property ServiceActualDate As DateTime
            Get
                Return _serviceActualDate
            End Get
            Set(ByVal value As DateTime)
                _serviceActualDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("CustomerName", "'{0}'")>
        Public Property CustomerName As String
            Get
                Return _customerName
            End Get
            Set(ByVal value As String)
                _customerName = value
            End Set
        End Property


        <ColumnInfo("CustomerPhoneNumber", "'{0}'")>
        Public Property CustomerPhoneNumber As String
            Get
                Return _customerPhoneNumber
            End Get
            Set(ByVal value As String)
                _customerPhoneNumber = value
            End Set
        End Property


        <ColumnInfo("ContactPersonName", "'{0}'")>
        Public Property ContactPersonName As String
            Get
                Return _contactPersonName
            End Get
            Set(ByVal value As String)
                _contactPersonName = value
            End Set
        End Property


        <ColumnInfo("ContactPersonPhoneNumber", "'{0}'")>
        Public Property ContactPersonPhoneNumber As String
            Get
                Return _contactPersonPhoneNumber
            End Get
            Set(ByVal value As String)
                _contactPersonPhoneNumber = value
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


        <ColumnInfo("TransactionType", "{0}")>
        Public Property TransactionType As Byte
            Get
                Return _transactionType
            End Get
            Set(ByVal value As Byte)
                _transactionType = value
            End Set
        End Property


        <ColumnInfo("ActualKM", "{0}")>
        Public Property ActualKM As Integer
            Get
                Return _actualKM
            End Get
            Set(ByVal value As Integer)
                _actualKM = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")>
        Public Property Status As Byte
            Get
                Return _status
            End Get
            Set(ByVal value As Byte)
                _status = value
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


