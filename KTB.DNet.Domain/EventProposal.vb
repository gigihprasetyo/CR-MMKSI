#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventProposal Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 11/23/2009 - 2:57:52 PM
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
    <Serializable(), TableInfo("EventProposal")> _
    Public Class EventProposal
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
        Private _activitySchedule As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _activityPlace As String = String.Empty
        Private _invitationNumber As Integer
        Private _attendantNumber As Integer
        Private _eventProposalStatus As Byte
        Private _eventAgreementStatus As Byte
        Private _comment As String = String.Empty
        Private _ravine As String = String.Empty
        Private _subDistrict As String = String.Empty
        Private _owner As Integer
        Private _driver As Integer
        Private _approveCost As Decimal
        Private _subsidiFile As String = String.Empty
        Private _ownerAttendant As Integer
        Private _driverAttendant As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _eventParameter As EventParameter
        Private _activityType As ActivityType
        Private _dealer As Dealer

        Private _eventReports As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _eventProposalDetails As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _eventProposalHistorys As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _eventProposalHistoryAgreements As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("ActivitySchedule", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ActivitySchedule() As DateTime
            Get
                Return _activitySchedule
            End Get
            Set(ByVal value As DateTime)
                _activitySchedule = value
            End Set
        End Property


        <ColumnInfo("ActivityPlace", "'{0}'")> _
        Public Property ActivityPlace() As String
            Get
                Return _activityPlace
            End Get
            Set(ByVal value As String)
                _activityPlace = value
            End Set
        End Property


        <ColumnInfo("InvitationNumber", "{0}")> _
        Public Property InvitationNumber() As Integer
            Get
                Return _invitationNumber
            End Get
            Set(ByVal value As Integer)
                _invitationNumber = value
            End Set
        End Property


        <ColumnInfo("AttendantNumber", "{0}")> _
        Public Property AttendantNumber() As Integer
            Get
                Return _attendantNumber
            End Get
            Set(ByVal value As Integer)
                _attendantNumber = value
            End Set
        End Property


        <ColumnInfo("EventProposalStatus", "{0}")> _
        Public Property EventProposalStatus() As Byte
            Get
                Return _eventProposalStatus
            End Get
            Set(ByVal value As Byte)
                _eventProposalStatus = value
            End Set
        End Property


        <ColumnInfo("EventAgreementStatus", "{0}")> _
        Public Property EventAgreementStatus() As Byte
            Get
                Return _eventAgreementStatus
            End Get
            Set(ByVal value As Byte)
                _eventAgreementStatus = value
            End Set
        End Property


        <ColumnInfo("Comment", "'{0}'")> _
        Public Property Comment() As String
            Get
                Return _comment
            End Get
            Set(ByVal value As String)
                _comment = value
            End Set
        End Property


        <ColumnInfo("Ravine", "'{0}'")> _
        Public Property Ravine() As String
            Get
                Return _ravine
            End Get
            Set(ByVal value As String)
                _ravine = value
            End Set
        End Property


        <ColumnInfo("SubDistrict", "'{0}'")> _
        Public Property SubDistrict() As String
            Get
                Return _subDistrict
            End Get
            Set(ByVal value As String)
                _subDistrict = value
            End Set
        End Property


        <ColumnInfo("Owner", "{0}")> _
        Public Property Owner() As Integer
            Get
                Return _owner
            End Get
            Set(ByVal value As Integer)
                _owner = value
            End Set
        End Property


        <ColumnInfo("Driver", "{0}")> _
        Public Property Driver() As Integer
            Get
                Return _driver
            End Get
            Set(ByVal value As Integer)
                _driver = value
            End Set
        End Property


        <ColumnInfo("ApproveCost", "{0}")> _
        Public Property ApproveCost() As Decimal
            Get
                Return _approveCost
            End Get
            Set(ByVal value As Decimal)
                _approveCost = value
            End Set
        End Property


        <ColumnInfo("SubsidiFile", "'{0}'")> _
        Public Property SubsidiFile() As String
            Get
                Return _subsidiFile
            End Get
            Set(ByVal value As String)
                _subsidiFile = value
            End Set
        End Property


        <ColumnInfo("OwnerAttendant", "{0}")> _
        Public Property OwnerAttendant() As Integer
            Get
                Return _ownerAttendant
            End Get
            Set(ByVal value As Integer)
                _ownerAttendant = value
            End Set
        End Property


        <ColumnInfo("DriverAttendant", "{0}")> _
        Public Property DriverAttendant() As Integer
            Get
                Return _driverAttendant
            End Get
            Set(ByVal value As Integer)
                _driverAttendant = value
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


        <ColumnInfo("EventParameterID", "{0}"), _
        RelationInfo("EventParameter", "ID", "EventProposal", "EventParameterID")> _
        Public Property EventParameter() As EventParameter
            Get
                Try
                    If Not IsNothing(Me._eventParameter) AndAlso (Not Me._eventParameter.IsLoaded) Then

                        Me._eventParameter = CType(DoLoad(GetType(EventParameter).ToString(), _eventParameter.ID), EventParameter)
                        Me._eventParameter.MarkLoaded()

                    End If

                    Return Me._eventParameter

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EventParameter)

                Me._eventParameter = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._eventParameter.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ActivityTypeID", "{0}"), _
        RelationInfo("ActivityType", "ID", "EventProposal", "ActivityTypeID")> _
        Public Property ActivityType() As ActivityType
            Get
                Try
                    If Not IsNothing(Me._activityType) AndAlso (Not Me._activityType.IsLoaded) Then

                        Me._activityType = CType(DoLoad(GetType(ActivityType).ToString(), _activityType.ID), ActivityType)
                        Me._activityType.MarkLoaded()

                    End If

                    Return Me._activityType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ActivityType)

                Me._activityType = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._activityType.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "EventProposal", "DealerID")> _
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


        <RelationInfo("EventProposal", "ID", "EventReport", "EventProposalID")> _
        Public ReadOnly Property EventReports() As System.Collections.ArrayList
            Get
                Try
                    If (Me._eventReports.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(EventReport), "EventProposal", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(EventReport), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._eventReports = DoLoadArray(GetType(EventReport).ToString, criterias)
                    End If

                    Return Me._eventReports

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("EventProposal", "ID", "EventProposalDetail", "EventProposalID")> _
        Public ReadOnly Property EventProposalDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._eventProposalDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(EventProposalDetail), "EventProposal", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(EventProposalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._eventProposalDetails = DoLoadArray(GetType(EventProposalDetail).ToString, criterias)
                    End If

                    Return Me._eventProposalDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("EventProposal", "ID", "EventProposalHistory", "EventProposalID")> _
        Public ReadOnly Property EventProposalHistorys() As System.Collections.ArrayList
            Get
                Try
                    If (Me._eventProposalHistorys.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(EventProposalHistory), "EventProposal", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(EventProposalHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._eventProposalHistorys = DoLoadArray(GetType(EventProposalHistory).ToString, criterias)
                    End If

                    Return Me._eventProposalHistorys

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("EventProposal", "ID", "EventProposalHistoryAgreement", "EventProposalID")> _
        Public ReadOnly Property EventProposalHistoryAgreements() As System.Collections.ArrayList
            Get
                Try
                    If (Me._eventProposalHistoryAgreements.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(EventProposalHistoryAgreement), "EventProposal", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(EventProposalHistoryAgreement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._eventProposalHistoryAgreements = DoLoadArray(GetType(EventProposalHistoryAgreement).ToString, criterias)
                    End If

                    Return Me._eventProposalHistoryAgreements

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
        Public ReadOnly Property PercentageAttendent() As Decimal
            Get
                If (Me.Driver > 0 And Me.OwnerAttendant > 0) Then
                    Me.InvitationNumber = Me.Driver + Me.Owner
                End If

                If (Me.AttendantNumber > 0 And Me.OwnerAttendant > 0) Then
                    Me.AttendantNumber = Me.DriverAttendant + Me.OwnerAttendant
                End If

                If (Me.InvitationNumber = 0 Or Me.AttendantNumber = 0) Then Return 0
                Dim percen As Decimal = (Me.AttendantNumber / Me.InvitationNumber) * 100
                Return Math.Round(percen, 2)
            End Get
        End Property

        Public ReadOnly Property PercentageAttendentDriver() As Decimal
            Get
                If (Me.Driver = 0 Or Me.DriverAttendant = 0) Then Return 0
                Dim percen As Decimal = (Me.DriverAttendant / Me.Driver) * 100
                Return Math.Round(percen, 2)
            End Get
        End Property

        Public ReadOnly Property PercentageAttendentOwner() As Decimal
            Get
                If (Me.Owner = 0 Or Me.OwnerAttendant = 0) Then Return 0
                Dim percen As Decimal = (Me.OwnerAttendant / Me.Owner) * 100
                Return Math.Round(percen, 2)
            End Get
        End Property

        Private _TotalCost As Decimal = 0
        Public Property TotalCost() As Decimal
            Get
                Return _TotalCost
            End Get
            Set(ByVal Value As Decimal)
                _TotalCost = Value
            End Set
        End Property
#End Region

    End Class
End Namespace

