
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitEventReportHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 27/05/2019 - 9:49:43
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
    <Serializable(), TableInfo("BabitEventReportHeader")> _
    Public Class BabitEventReportHeader
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
        Private _eventReportName As String = String.Empty
        Private _periodStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _periodEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _locationName As String = String.Empty
        Private _invitationQty As Integer
        Private _attendeeQty As Integer
        Private _notes As String = String.Empty
        Private _notesMMKSI As String = String.Empty
        Private _status As Short
        Private _collaborateDealer As String = String.Empty
        Private _approvalNumber As String = String.Empty
        Private _confirmedBudget As Decimal
        Private _approvedBudget As Decimal


        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _dealerBranch As DealerBranch
        Private _babitEventProposalHeader As BabitEventProposalHeader
        Private _city As City

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


        <ColumnInfo("EventReportName", "'{0}'")> _
        Public Property EventReportName As String
            Get
                Return _eventReportName
            End Get
            Set(ByVal value As String)
                _eventReportName = value
            End Set
        End Property


        <ColumnInfo("PeriodStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PeriodStart As DateTime
            Get
                Return _periodStart
            End Get
            Set(ByVal value As DateTime)
                _periodStart = value
            End Set
        End Property


        <ColumnInfo("PeriodEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PeriodEnd As DateTime
            Get
                Return _periodEnd
            End Get
            Set(ByVal value As DateTime)
                _periodEnd = value
            End Set
        End Property


        <ColumnInfo("LocationName", "'{0}'")> _
        Public Property LocationName As String
            Get
                Return _locationName
            End Get
            Set(ByVal value As String)
                _locationName = value
            End Set
        End Property


        <ColumnInfo("InvitationQty", "{0}")> _
        Public Property InvitationQty As Integer
            Get
                Return _invitationQty
            End Get
            Set(ByVal value As Integer)
                _invitationQty = value
            End Set
        End Property


        <ColumnInfo("AttendeeQty", "{0}")> _
        Public Property AttendeeQty As Integer
            Get
                Return _attendeeQty
            End Get
            Set(ByVal value As Integer)
                _attendeeQty = value
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


        <ColumnInfo("NotesMMKSI", "'{0}'")> _
        Public Property NotesMMKSI As String
            Get
                Return _notesMMKSI
            End Get
            Set(ByVal value As String)
                _notesMMKSI = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property


        <ColumnInfo("CollaborateDealer", "'{0}'")> _
        Public Property CollaborateDealer As String
            Get
                Return _collaborateDealer
            End Get
            Set(ByVal value As String)
                _collaborateDealer = value
            End Set
        End Property


        <ColumnInfo("ApprovalNumber", "'{0}'")> _
        Public Property ApprovalNumber As String
            Get
                Return _approvalNumber
            End Get
            Set(ByVal value As String)
                _approvalNumber = value
            End Set
        End Property

        <ColumnInfo("ConfirmedBudget", "'{0}'")> _
        Public Property ConfirmedBudget As Decimal
            Get
                Return _confirmedBudget
            End Get
            Set(ByVal value As Decimal)
                _confirmedBudget = value
            End Set
        End Property

        <ColumnInfo("ApprovedBudget", "'{0}'")> _
        Public Property ApprovedBudget As Decimal
            Get
                Return _approvedBudget
            End Get
            Set(ByVal value As Decimal)
                _approvedBudget = value
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


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
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


        <ColumnInfo("DealerBranchID", "{0}"), _
        RelationInfo("DealerBranch", "ID", "BabitEventReportHeader", "DealerBranchID")> _
        Public Property DealerBranch As DealerBranch
            Get
                Try
                    If Not IsNothing(Me._dealerBranch) AndAlso (Not Me._dealerBranch.IsLoaded) Then

                        Me._dealerBranch = CType(DoLoad(GetType(DealerBranch).ToString(), _dealerBranch.ID), DealerBranch)
                        Me._dealerBranch.MarkLoaded()

                    End If

                    Return Me._dealerBranch

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DealerBranch)

                Me._dealerBranch = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBranch.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "BabitEventReportHeader", "DealerID")> _
        Public Property Dealer As Dealer
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


        <ColumnInfo("BabitEventProposalHeaderID", "{0}"), _
        RelationInfo("BabitEventProposalHeader", "ID", "BabitEventReportHeader", "BabitEventProposalHeaderID")> _
        Public Property BabitEventProposalHeader As BabitEventProposalHeader
            Get
                Try
                    If Not IsNothing(Me._babitEventProposalHeader) AndAlso (Not Me._babitEventProposalHeader.IsLoaded) Then

                        Me._babitEventProposalHeader = CType(DoLoad(GetType(BabitEventProposalHeader).ToString(), _babitEventProposalHeader.ID), BabitEventProposalHeader)
                        Me._babitEventProposalHeader.MarkLoaded()

                    End If

                    Return Me._babitEventProposalHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BabitEventProposalHeader)

                Me._babitEventProposalHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitEventProposalHeader.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("CityID", "{0}"), _
        RelationInfo("City", "ID", "BabitEventReportHeader", "CityID")> _
        Public Property City As City
            Get
                Try
                    If Not IsNothing(Me._city) AndAlso (Not Me._city.IsLoaded) Then

                        Me._city = CType(DoLoad(GetType(City).ToString(), _city.ID), City)
                        Me._city.MarkLoaded()

                    End If

                    Return Me._city

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As City)

                Me._city = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._city.MarkLoaded()
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

#Region "Custom Method"

#End Region

    End Class
End Namespace

