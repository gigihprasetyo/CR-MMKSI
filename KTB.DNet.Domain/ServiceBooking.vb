
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceBooking Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/17/2018 - 9:25:52 AM
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
    <Serializable(), TableInfo("ServiceBooking")> _
    Public Class ServiceBooking
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
        Private _serviceBookingCode As String
        Private _chassisNumber As String
        Private _plateNumber As String
        Private _customerName As String
        Private _customerPhoneNumber As String
        Private _odoMeter As Integer
        Private _pickupType As Short
        Private _stallServiceType As Short
        Private _standardTime As Decimal
        Private _incomingDateStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _incomingDateEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _workingTimeStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _workingTimeEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _isMitsubishi As Short
        Private _vehicleTypeDescription As String
        Private _notes As String
        Private _status As String
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _vechileModel As VechileModel
        Private _chassisMaster As ChassisMaster
        Private _dealer As Dealer
        Private _stallMaster As StallMaster
        Private _vechileType As VechileType
        Private _trTrainee As TrTrainee

        Private _serviceBookingActivities As New ArrayList
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

        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber() As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property

        <ColumnInfo("ServiceBookingCode", "'{0}'")> _
        Public Property ServiceBookingCode() As String
            Get
                Return _serviceBookingCode
            End Get
            Set(ByVal value As String)
                _serviceBookingCode = value
            End Set
        End Property

        <ColumnInfo("PlateNumber", "'{0}'")> _
        Public Property PlateNumber() As String
            Get
                Return _plateNumber
            End Get
            Set(ByVal value As String)
                _plateNumber = value
            End Set
        End Property

        <ColumnInfo("CustomerName", "'{0}'")> _
        Public Property CustomerName() As String
            Get
                Return _customerName
            End Get
            Set(ByVal value As String)
                _customerName = value
            End Set
        End Property

        <ColumnInfo("CustomerPhoneNumber", "'{0}'")> _
        Public Property CustomerPhoneNumber() As String
            Get
                Return _customerPhoneNumber
            End Get
            Set(ByVal value As String)
                _customerPhoneNumber = value
            End Set
        End Property

        <ColumnInfo("OdoMeter", "{0}")> _
        Public Property OdoMeter() As Integer
            Get
                Return _odoMeter
            End Get
            Set(ByVal value As Integer)
                _odoMeter = value
            End Set
        End Property

        <ColumnInfo("PickupType", "{0}")> _
        Public Property PickupType() As Short
            Get
                Return _pickupType
            End Get
            Set(ByVal value As Short)
                _pickupType = value
            End Set
        End Property

        <ColumnInfo("StallServiceType", "{0}")> _
        Public Property StallServiceType() As Short
            Get
                Return _stallServiceType
            End Get
            Set(ByVal value As Short)
                _stallServiceType = value
            End Set
        End Property

        <ColumnInfo("StandardTime", "{0}")> _
        Public Property StandardTime() As Decimal
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

        <ColumnInfo("IsMitsubishi", "{0}")> _
        Public Property IsMitsubishi() As Short
            Get
                Return _isMitsubishi
            End Get
            Set(ByVal value As Short)
                _isMitsubishi = value
            End Set
        End Property

        <ColumnInfo("VehicleTypeDescription", "'{0}'")> _
        Public Property VehicleTypeDescription() As String
            Get
                Return _vehicleTypeDescription
            End Get
            Set(ByVal value As String)
                _vehicleTypeDescription = value
            End Set
        End Property

        <ColumnInfo("Notes", "'{0}'")> _
        Public Property Notes() As String
            Get
                Return _notes
            End Get
            Set(ByVal value As String)
                _notes = value
            End Set
        End Property

        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
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

        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy() As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
            End Set
        End Property

        <ColumnInfo("LastUpdatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime() As DateTime
            Get
                Return _lastUpdatedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedTime = value
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "ServiceBooking", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "ServiceBooking", "ChassisMasterID")> _
        Public Property ChassisMaster() As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then

                        Me._chassisMaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _chassisMaster.ID), ChassisMaster)
                        If Not IsNothing(Me._chassisMaster) Then
                            Me._chassisMaster.MarkLoaded()
                        End If

                    End If

                    Return Me._chassisMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMaster)

                Me._chassisMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("VechileModelID", "{0}"), _
        RelationInfo("VechileModel", "ID", "ServiceBooking", "VechileModelID")> _
        Public Property VechileModel() As VechileModel
            Get
                Try
                    If Not IsNothing(Me._vechileModel) AndAlso (Not Me._vechileModel.IsLoaded) Then

                        Me._vechileModel = CType(DoLoad(GetType(VechileModel).ToString(), _vechileModel.ID), VechileModel)
                        Me._vechileModel.MarkLoaded()

                    End If

                    Return Me._vechileModel

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileModel)

                Me._vechileModel = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileModel.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("StallMasterID", "{0}"), _
        RelationInfo("StallMaster", "ID", "ServiceBooking", "StallMasterID")> _
        Public Property StallMaster() As StallMaster
            Get
                Try
                    If Not IsNothing(Me._stallMaster) AndAlso (Not Me._stallMaster.IsLoaded) Then

                        Me._stallMaster = CType(DoLoad(GetType(StallMaster).ToString(), _stallMaster.ID), StallMaster)
                        Me._stallMaster.MarkLoaded()

                    End If

                    Return Me._stallMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As StallMaster)

                Me._stallMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._stallMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("VechileTypeID", "{0}"), _
        RelationInfo("VechileType", "ID", "ServiceBooking", "VechileTypeID")> _
        Public Property VechileType() As VechileType
            Get
                Try
                    If Not IsNothing(Me._vechileType) AndAlso (Not Me._vechileType.IsLoaded) Then

                        Me._vechileType = CType(DoLoad(GetType(VechileType).ToString(), _vechileType.ID), VechileType)
                        Me._vechileType.MarkLoaded()

                    End If

                    Return Me._vechileType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileType)

                Me._vechileType = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileType.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("TrTrainee", "{0}"), _
        RelationInfo("TrTrainee", "ID", "ServiceBooking", "ServiceAdvisorID")> _
        Public Property TrTrainee() As TrTrainee
            Get
                Try
                    If Not IsNothing(Me._trTrainee) AndAlso (Not Me._trTrainee.IsLoaded) Then

                        Me._trTrainee = CType(DoLoad(GetType(TrTrainee).ToString(), _trTrainee.ID), TrTrainee)
                        Me._trTrainee.MarkLoaded()

                    End If

                    Return Me._trTrainee

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrTrainee)

                Me._trTrainee = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trTrainee.MarkLoaded()
                End If
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

#Region "Non_generated Properties"
#End Region

#Region "Custom Method"
        <RelationInfo("ServiceBooking", "ID", "ServiceBookingActivity", "ServiceBookingID")> _
        Public ReadOnly Property ServiceBookingActivities() As System.Collections.ArrayList
            Get
                Try
                    If (Me._serviceBookingActivities.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ServiceBookingActivity), "ServiceBooking", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ServiceBookingActivity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        Me._serviceBookingActivities = DoLoadArray(GetType(ServiceBookingActivity).ToString, criterias)
                    End If
                    Return Me._serviceBookingActivities
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
        End Property

        Public ReadOnly Property ServiceReminderFollowUp() As ServiceReminderFollowUp
            Get
                Try
                    If (Me._iD > 0) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ServiceReminderFollowUp), "ServiceBooking", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ServiceReminderFollowUp), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(ServiceReminderFollowUp), "FollowUpStatus", MatchType.InSet, "(1,2)"))
                        Dim sort As Sort = New Sort(GetType(ServiceReminderFollowUp), "ID", sort.SortDirection.DESC)
                        Dim sorts As SortCollection = New SortCollection
                        sorts.Add(sort)
                        Dim result As ArrayList = DoLoadArray(GetType(ServiceReminderFollowUp).ToString, criterias, sorts)
                        If result.Count > 0 Then
                            Return CType(result(0), ServiceReminderFollowUp)
                        Else
                            Return Nothing
                        End If
                    Else
                        Return Nothing
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
        End Property

        Public ReadOnly Property CustomerCaseResponse() As CustomerCaseResponse
            Get
                Try
                    If (Me._iD > 0) Then
                        Dim _criteria As Criteria = New Criteria(GetType(CustomerCaseResponse), "ServiceBooking", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(CustomerCaseResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'criterias.opAnd(New Criteria(GetType(CustomerCaseResponse), "IsSend", MatchType.Exact, 0))
                        Dim sort As Sort = New Sort(GetType(CustomerCaseResponse), "ID", sort.SortDirection.DESC)
                        Dim sorts As SortCollection = New SortCollection
                        sorts.Add(sort)
                        Dim result As ArrayList = DoLoadArray(GetType(CustomerCaseResponse).ToString, criterias, sorts)
                        If result.Count > 0 Then
                            Return CType(result(0), CustomerCaseResponse)
                        Else
                            Return Nothing
                        End If
                    Else
                        Return Nothing
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
        End Property

        Public ReadOnly Property StatusStr() As String
            Get
                Try
                    If (Not String.IsNullOrEmpty(Me.Status)) Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.Status"))
                        criterias.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, Me.Status))
                        Dim arrLst As ArrayList = DoLoadArray(GetType(StandardCode).ToString, criterias)
                        If arrLst.Count > 0 Then
                            Return CType(arrLst(0), StandardCode).ValueDesc
                        End If
                    End If
                    Return ""
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
        End Property
#End Region


    End Class
End Namespace
