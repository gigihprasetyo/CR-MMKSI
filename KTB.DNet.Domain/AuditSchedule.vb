#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AuditSchedule Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/27/2007 - 12:48:05 PM
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
    <Serializable(), TableInfo("AuditSchedule")> _
    Public Class AuditSchedule
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
        Private _isRilis As Byte
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _auditParameter As AuditParameter

        Private _auditScheduleDealers As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _auditScheduleAuditors As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("IsRilis", "{0}")> _
        Public Property IsRilis() As Byte
            Get
                Return _isRilis
            End Get
            Set(ByVal value As Byte)
                _isRilis = value
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


        <ColumnInfo("AuditParameterID", "{0}"), _
        RelationInfo("AuditParameter", "ID", "AuditSchedule", "AuditParameterID")> _
        Public Property AuditParameter() As AuditParameter
            Get
                Try
                    If Not isnothing(Me._auditParameter) AndAlso (Not Me._auditParameter.IsLoaded) Then

                        Me._auditParameter = CType(DoLoad(GetType(AuditParameter).ToString(), _auditParameter.ID), AuditParameter)
                        Me._auditParameter.MarkLoaded()

                    End If

                    Return Me._auditParameter

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AuditParameter)

                Me._auditParameter = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._auditParameter.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("AuditSchedule", "ID", "AuditScheduleDealer", "AuditScheduleID")> _
        Public ReadOnly Property AuditScheduleDealers() As System.Collections.ArrayList
            Get
                Try
                    If (Me._auditScheduleDealers.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(AuditScheduleDealer), "AuditSchedule", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(AuditScheduleDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._auditScheduleDealers = DoLoadArray(GetType(AuditScheduleDealer).ToString, criterias)
                    End If

                    Return Me._auditScheduleDealers

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("AuditSchedule", "ID", "AuditScheduleAuditor", "AuditScheduleID")> _
        Public ReadOnly Property AuditScheduleAuditors() As System.Collections.ArrayList
            Get
                Try
                    If (Me._auditScheduleAuditors.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(AuditScheduleAuditor), "AuditSchedule", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(AuditScheduleAuditor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._auditScheduleAuditors = DoLoadArray(GetType(AuditScheduleAuditor).ToString, criterias)
                    End If

                    Return Me._auditScheduleAuditors

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
        Public ReadOnly Property dealerList() As String
            Get
                Dim _str As String = String.Empty
                For Each _itemDealer As Dealer In Me.AuditScheduleDealers
                    _str = _str & _itemDealer.DealerCode & ";"
                Next
                Return _str
            End Get
        End Property
#End Region

    End Class
End Namespace

