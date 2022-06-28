#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AuditScheduleDealer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 29/09/2007 - 13:58:00
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
    <Serializable(), TableInfo("AuditScheduleDealer")> _
    Public Class AuditScheduleDealer
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
        Private _isRilisReport As Byte
        Private _assessmentFile As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _auditSchedule As AuditSchedule
        Private _dealer As Dealer
        Private _auditScheduleAuditor As AuditScheduleAuditor

        Private _auditScheduleDealerReports As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _auditScheduleDealerReportForms As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("IsRilisReport", "{0}")> _
        Public Property IsRilisReport() As Byte
            Get
                Return _isRilisReport
            End Get
            Set(ByVal value As Byte)
                _isRilisReport = value
            End Set
        End Property


        <ColumnInfo("AssessmentFile", "'{0}'")> _
        Public Property AssessmentFile() As String
            Get
                Return _assessmentFile
            End Get
            Set(ByVal value As String)
                _assessmentFile = value
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


        <ColumnInfo("AuditScheduleID", "{0}"), _
        RelationInfo("AuditSchedule", "ID", "AuditScheduleDealer", "AuditScheduleID")> _
        Public Property AuditSchedule() As AuditSchedule
            Get
                Try
                    If Not isnothing(Me._auditSchedule) AndAlso (Not Me._auditSchedule.IsLoaded) Then

                        Me._auditSchedule = CType(DoLoad(GetType(AuditSchedule).ToString(), _auditSchedule.ID), AuditSchedule)
                        Me._auditSchedule.MarkLoaded()

                    End If

                    Return Me._auditSchedule

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AuditSchedule)

                Me._auditSchedule = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._auditSchedule.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "AuditScheduleDealer", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("AuditScheduleAuditorID", "{0}"), _
        RelationInfo("AuditScheduleAuditor", "ID", "AuditScheduleDealer", "AuditScheduleAuditorID")> _
        Public Property AuditScheduleAuditor() As AuditScheduleAuditor
            Get
                Try
                    If Not isnothing(Me._auditScheduleAuditor) AndAlso (Not Me._auditScheduleAuditor.IsLoaded) Then

                        Me._auditScheduleAuditor = CType(DoLoad(GetType(AuditScheduleAuditor).ToString(), _auditScheduleAuditor.ID), AuditScheduleAuditor)
                        Me._auditScheduleAuditor.MarkLoaded()

                    End If

                    Return Me._auditScheduleAuditor

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AuditScheduleAuditor)

                Me._auditScheduleAuditor = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._auditScheduleAuditor.MarkLoaded()
                End If
            End Set
        End Property
        <RelationInfo("AuditScheduleDealer", "ID", "AuditScheduleDealerReport", "AuditScheduleDealerID")> _
                        Public ReadOnly Property AuditScheduleDealerReports() As System.Collections.ArrayList
            Get
                Try
                    If (Me._auditScheduleDealerReports.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(AuditScheduleDealerReport), "AuditScheduleDealerID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(AuditScheduleDealerReport), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._auditScheduleDealerReports = DoLoadArray(GetType(AuditScheduleDealerReport).ToString, criterias)
                    End If

                    Return Me._auditScheduleDealerReports

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        <RelationInfo("AuditScheduleDealer", "ID", "AuditScheduleDealerReportForm", "AuditScheduleDealerID")> _
        Public ReadOnly Property AuditScheduleDealerReportForms() As System.Collections.ArrayList
            Get
                Try
                    If (Me._auditScheduleDealerReportForms.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(AuditScheduleDealerReportForm), "AuditScheduleDealer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(AuditScheduleDealerReportForm), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._auditScheduleDealerReportForms = DoLoadArray(GetType(AuditScheduleDealerReportForm).ToString, criterias)
                    End If

                    Return Me._auditScheduleDealerReportForms

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

