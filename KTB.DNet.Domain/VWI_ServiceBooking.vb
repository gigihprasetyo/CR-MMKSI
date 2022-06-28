#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_ServiceBooking Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/20/2020 - 12:23:06 PM
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
    <Serializable(), TableInfo("VWI_ServiceBooking")> _
    Public Class VWI_ServiceBooking
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
        Private _serviceBookingCode As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _chassisNumber As String = String.Empty
        Private _stallMasterID As Integer
        Private _stallCode As String = String.Empty
        Private _vehicleTypeCode As String = String.Empty
        Private _vechileModelDescription As String = String.Empty
        Private _vechileTypeDescription As String = String.Empty
        Private _plateNumber As String = String.Empty
        Private _customerName As String = String.Empty
        Private _customerPhoneNumber As String = String.Empty
        Private _odometer As Integer
        Private _serviceAdvisorID As Integer
        Private _serviceAdvisorName As String = String.Empty
        Private _incomingPlan As String = String.Empty
        Private _stallServiceType As String = String.Empty
        Private _standardTime As Decimal
        Private _incomingDateStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _incomingDateEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _workingTimeStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _workingTimeEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _isMitsubishi As String = String.Empty
        Private _status As String = String.Empty
        Private _notes As String = String.Empty
        Private _serviceBookingActivities As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
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

        <ColumnInfo("ServiceBookingCode", "'{0}'")> _
        Public Property ServiceBookingCode As String
            Get
                Return _serviceBookingCode
            End Get
            Set(ByVal value As String)
                _serviceBookingCode = value
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

        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property

        <ColumnInfo("StallMasterID", "{0}")> _
        Public Property StallMasterID As String
            Get
                Return _stallMasterID
            End Get
            Set(ByVal value As String)
                _stallMasterID = value
            End Set
        End Property

        <ColumnInfo("StallCode", "'{0}'")> _
        Public Property StallCode As String
            Get
                Return _stallCode
            End Get
            Set(ByVal value As String)
                _stallCode = value
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

        <ColumnInfo("VechileModelDescription", "'{0}'")> _
        Public Property VechileModelDescription As String
            Get
                Return _vechileModelDescription
            End Get
            Set(ByVal value As String)
                _vechileModelDescription = value
            End Set
        End Property

        <ColumnInfo("VechileTypeDescription", "'{0}'")> _
        Public Property VechileTypeDescription As String
            Get
                Return _vechileTypeDescription
            End Get
            Set(ByVal value As String)
                _vechileTypeDescription = value
            End Set
        End Property

        <ColumnInfo("PlateNumber", "'{0}'")> _
        Public Property PlateNumber As String
            Get
                Return _plateNumber
            End Get
            Set(ByVal value As String)
                _plateNumber = value
            End Set
        End Property

        <ColumnInfo("CustomerName", "'{0}'")> _
        Public Property CustomerName As String
            Get
                Return _customerName
            End Get
            Set(ByVal value As String)
                _customerName = value
            End Set
        End Property

        <ColumnInfo("CustomerPhoneNumber", "{0}")> _
        Public Property CustomerPhoneNumber As String
            Get
                Return _customerPhoneNumber
            End Get
            Set(ByVal value As String)
                _customerPhoneNumber = value
            End Set
        End Property

        <ColumnInfo("Odometer", "{0}")> _
        Public Property Odometer As Integer
            Get
                Return _odometer
            End Get
            Set(ByVal value As Integer)
                _odometer = value
            End Set
        End Property

        <ColumnInfo("ServiceAdvisorID", "{0}")> _
        Public Property ServiceAdvisorID As String
            Get
                Return _serviceAdvisorID
            End Get
            Set(ByVal value As String)
                _serviceAdvisorID = value
            End Set
        End Property

        <ColumnInfo("ServiceAdvisorName", "'{0}'")> _
        Public Property ServiceAdvisorName As String
            Get
                Return _serviceAdvisorName
            End Get
            Set(ByVal value As String)
                _serviceAdvisorName = value
            End Set
        End Property

        <ColumnInfo("IncomingPlan", "'{0}'")> _
        Public Property IncomingPlan As String
            Get
                Return _incomingPlan
            End Get
            Set(ByVal value As String)
                _incomingPlan = value
            End Set
        End Property

        <ColumnInfo("StallServiceType", "'{0}'")> _
        Public Property StallServiceType As String
            Get
                Return _stallServiceType
            End Get
            Set(ByVal value As String)
                _stallServiceType = value
            End Set
        End Property

        <ColumnInfo("StandardTime", "{0}")> _
        Public Property StandardTime As Decimal
            Get
                Return _standardTime
            End Get
            Set(ByVal value As Decimal)
                _standardTime = value
            End Set
        End Property

        <ColumnInfo("IncomingDateStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property IncomingDateStart() As DateTime
            Get
                Return _incomingDateStart
            End Get
            Set(ByVal value As DateTime)
                _incomingDateStart = value
            End Set
        End Property

        <ColumnInfo("IncomingDateEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property IncomingDateEnd() As DateTime
            Get
                Return _incomingDateEnd
            End Get
            Set(ByVal value As DateTime)
                _incomingDateEnd = value
            End Set
        End Property

        <ColumnInfo("WorkingTimeStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property WorkingTimeStart() As DateTime
            Get
                Return _workingTimeStart
            End Get
            Set(ByVal value As DateTime)
                _workingTimeStart = value
            End Set
        End Property

        <ColumnInfo("WorkingTimeEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property WorkingTimeEnd() As DateTime
            Get
                Return _workingTimeEnd
            End Get
            Set(ByVal value As DateTime)
                _workingTimeEnd = value
            End Set
        End Property

        <ColumnInfo("IsMitsubishi", "'{0}'")> _
        Public Property IsMitsubishi As String
            Get
                Return _isMitsubishi
            End Get
            Set(ByVal value As String)
                _isMitsubishi = value
            End Set
        End Property

        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property

        <ColumnInfo("Notes", "'{0}'")> _
        Public Property Notes As String
            Get
                Return _notes
            End Get
            Set(ByVal value As String)
                _notes = value
            End Set
        End Property

        <ColumnInfo("ServiceBookingActivities", "'{0}'")> _
        Public Property ServiceBookingActivities As String
            Get
                Return _serviceBookingActivities
            End Get
            Set(ByVal value As String)
                _serviceBookingActivities = value
            End Set
        End Property

        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property

        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime As DateTime
            Get
                Return _lastUpdatedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedTime = value
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
