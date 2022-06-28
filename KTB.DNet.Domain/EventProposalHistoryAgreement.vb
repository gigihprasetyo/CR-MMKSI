#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventProposalHistoryAgreement Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 9/15/2009 - 10:34:04 AM
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
    <Serializable(), TableInfo("EventProposalHistoryAgreement")> _
    Public Class EventProposalHistoryAgreement
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
        Private _eventName As String = String.Empty
        Private _activitySchedule As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _proposedCost As Decimal
        Private _approvedCost As Decimal
        Private _updateBy As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _eventProposal As EventProposal



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


        <ColumnInfo("EventName", "'{0}'")> _
        Public Property EventName() As String
            Get
                Return _eventName
            End Get
            Set(ByVal value As String)
                _eventName = value
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


        <ColumnInfo("ProposedCost", "{0}")> _
        Public Property ProposedCost() As Decimal
            Get
                Return _proposedCost
            End Get
            Set(ByVal value As Decimal)
                _proposedCost = value
            End Set
        End Property


        <ColumnInfo("ApprovedCost", "{0}")> _
        Public Property ApprovedCost() As Decimal
            Get
                Return _approvedCost
            End Get
            Set(ByVal value As Decimal)
                _approvedCost = value
            End Set
        End Property


        <ColumnInfo("UpdateBy", "'{0}'")> _
        Public Property UpdateBy() As String
            Get
                Return _updateBy
            End Get
            Set(ByVal value As String)
                _updateBy = value
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


        <ColumnInfo("EventProposalID", "{0}"), _
        RelationInfo("EventProposal", "ID", "EventProposalHistoryAgreement", "EventProposalID")> _
        Public Property EventProposal() As EventProposal
            Get
                Try
                    If Not isnothing(Me._eventProposal) AndAlso (Not Me._eventProposal.IsLoaded) Then

                        Me._eventProposal = CType(DoLoad(GetType(EventProposal).ToString(), _eventProposal.ID), EventProposal)
                        Me._eventProposal.MarkLoaded()

                    End If

                    Return Me._eventProposal

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EventProposal)

                Me._eventProposal = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._eventProposal.MarkLoaded()
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

