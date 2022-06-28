#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitProposal Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/30/2007 - 10:14:37 AM
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
    <Serializable(), TableInfo("BabitProposal")> _
    Public Class BabitProposal
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
        Private _status As Integer
        Private _noSuratDealer As String = String.Empty
        Private _noPengajuan As String = String.Empty
        Private _noPersetujuan As String = String.Empty
        Private _activityType As Integer
        Private _tglTerimaEvidance As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _kTBApprovalAmount As Decimal
        Private _babitKhususAmount As Decimal
        Private _fileName As String = String.Empty
        Private _description As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _babitRealizationFile As String = String.Empty
        Private _bPPameran As BPPameran
        Private _gLAccount As GLAccount
        Private _bPEvent As BPEvent
        Private _babitAllocation As BabitAllocation
        Private _dealer As Dealer

        Private _babitProposalHistorys As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _bPIklans As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _babitPayments As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
            End Set
        End Property


        <ColumnInfo("NoSuratDealer", "'{0}'")> _
        Public Property NoSuratDealer() As String
            Get
                Return _noSuratDealer
            End Get
            Set(ByVal value As String)
                _noSuratDealer = value
            End Set
        End Property


        <ColumnInfo("NoPengajuan", "'{0}'")> _
        Public Property NoPengajuan() As String
            Get
                Return _noPengajuan
            End Get
            Set(ByVal value As String)
                _noPengajuan = value
            End Set
        End Property


        <ColumnInfo("NoPersetujuan", "'{0}'")> _
        Public Property NoPersetujuan() As String
            Get
                Return _noPersetujuan
            End Get
            Set(ByVal value As String)
                _noPersetujuan = value
            End Set
        End Property


        <ColumnInfo("ActivityType", "{0}")> _
        Public Property ActivityType() As Integer
            Get
                Return _activityType
            End Get
            Set(ByVal value As Integer)
                _activityType = value
            End Set
        End Property


        <ColumnInfo("TglTerimaEvidance", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property TglTerimaEvidance() As DateTime
            Get
                Return _tglTerimaEvidance
            End Get
            Set(ByVal value As DateTime)
                _tglTerimaEvidance = value
            End Set
        End Property


        <ColumnInfo("KTBApprovalAmount", "{0}")> _
        Public Property KTBApprovalAmount() As Decimal
            Get
                Return _kTBApprovalAmount
            End Get
            Set(ByVal value As Decimal)
                _kTBApprovalAmount = value
            End Set
        End Property


        <ColumnInfo("BabitKhususAmount", "{0}")> _
        Public Property BabitKhususAmount() As Decimal
            Get
                Return _babitKhususAmount
            End Get
            Set(ByVal value As Decimal)
                _babitKhususAmount = value
            End Set
        End Property


        <ColumnInfo("FileName", "'{0}'")> _
        Public Property FileName() As String
            Get
                Return _fileName
            End Get
            Set(ByVal value As String)
                _fileName = value
            End Set
        End Property
        <ColumnInfo("BabitRealizationFile", "'{0}'")> _
         Public Property BabitRealizationFile() As String
            Get
                Return _babitRealizationFile
            End Get
            Set(ByVal value As String)
                _babitRealizationFile = value
            End Set
        End Property

        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
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


        <ColumnInfo("BPPameranID", "{0}"), _
        RelationInfo("BPPameran", "ID", "BabitProposal", "BPPameranID")> _
        Public Property BPPameran() As BPPameran
            Get
                Try
                    If Not IsNothing(Me._bPPameran) AndAlso (Not Me._bPPameran.IsLoaded) Then

                        Me._bPPameran = CType(DoLoad(GetType(BPPameran).ToString(), _bPPameran.ID), BPPameran)
                        Me._bPPameran.MarkLoaded()

                    End If

                    Return Me._bPPameran

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BPPameran)

                Me._bPPameran = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._bPPameran.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("GLAccountID", "{0}"), _
        RelationInfo("GLAccount", "ID", "BabitProposal", "GLAccountID")> _
        Public Property GLAccount() As GLAccount
            Get
                Try
                    If Not IsNothing(Me._gLAccount) AndAlso (Not Me._gLAccount.IsLoaded) Then

                        Me._gLAccount = CType(DoLoad(GetType(GLAccount).ToString(), _gLAccount.ID), GLAccount)
                        Me._gLAccount.MarkLoaded()

                    End If

                    Return Me._gLAccount

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As GLAccount)

                Me._gLAccount = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._gLAccount.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("BPEventID", "{0}"), _
        RelationInfo("BPEvent", "ID", "BabitProposal", "BPEventID")> _
        Public Property BPEvent() As BPEvent
            Get
                Try
                    If Not IsNothing(Me._bPEvent) AndAlso (Not Me._bPEvent.IsLoaded) Then

                        Me._bPEvent = CType(DoLoad(GetType(BPEvent).ToString(), _bPEvent.ID), BPEvent)
                        Me._bPEvent.MarkLoaded()

                    End If

                    Return Me._bPEvent

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BPEvent)

                Me._bPEvent = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._bPEvent.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("BabitAllocationID", "{0}"), _
        RelationInfo("BabitAllocation", "ID", "BabitProposal", "BabitAllocationID")> _
        Public Property BabitAllocation() As BabitAllocation
            Get
                Try
                    If Not IsNothing(Me._babitAllocation) AndAlso (Not Me._babitAllocation.IsLoaded) Then

                        Me._babitAllocation = CType(DoLoad(GetType(BabitAllocation).ToString(), _babitAllocation.ID), BabitAllocation)
                        Me._babitAllocation.MarkLoaded()

                    End If

                    Return Me._babitAllocation

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BabitAllocation)

                Me._babitAllocation = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitAllocation.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "BabitProposal", "DealerID")> _
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


        <RelationInfo("BabitProposal", "ID", "BabitProposalHistory", "BabitProposalID")> _
        Public ReadOnly Property BabitProposalHistorys() As System.Collections.ArrayList
            Get
                Try
                    If (Me._babitProposalHistorys.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BabitProposalHistory), "BabitProposal", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BabitProposalHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._babitProposalHistorys = DoLoadArray(GetType(BabitProposalHistory).ToString, criterias)
                    End If

                    Return Me._babitProposalHistorys

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("BabitProposal", "ID", "BPIklan", "BabitProposalID")> _
        Public ReadOnly Property BPIklans() As System.Collections.ArrayList
            Get
                Try
                    If (Me._bPIklans.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BPIklan), "BabitProposal", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BPIklan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._bPIklans = DoLoadArray(GetType(BPIklan).ToString, criterias)
                    End If

                    Return Me._bPIklans

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("BabitProposal", "ID", "BabitPayment", "BabitProposalID")> _
        Public ReadOnly Property BabitPayments() As System.Collections.ArrayList
            Get
                Try
                    If (Me._babitPayments.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BabitPayment), "BabitProposal", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BabitPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._babitPayments = DoLoadArray(GetType(BabitPayment).ToString, criterias)
                    End If

                    Return Me._babitPayments

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

#End Region

    End Class
End Namespace

