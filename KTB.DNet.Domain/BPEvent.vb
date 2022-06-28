#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BPEvent Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 9/8/2006 - 12:26:10 PM
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
    <Serializable(), TableInfo("BPEvent")> _
    Public Class BPEvent
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
        Private _place As String = String.Empty
        Private _startEventDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _endEventDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _eventSize As String = String.Empty
        Private _numberOfDay As Integer
        Private _salesTarget As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _eventActivitys As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _babitProposals As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Place", "'{0}'")> _
        Public Property Place() As String
            Get
                Return _place
            End Get
            Set(ByVal value As String)
                _place = value
            End Set
        End Property


        <ColumnInfo("StartEventDate", "'{0:yyyy/MM/dd}'")> _
        Public Property StartEventDate() As DateTime
            Get
                Return _startEventDate
            End Get
            Set(ByVal value As DateTime)
                _startEventDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("EndEventDate", "'{0:yyyy/MM/dd}'")> _
        Public Property EndEventDate() As DateTime
            Get
                Return _endEventDate
            End Get
            Set(ByVal value As DateTime)
                _endEventDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("EventSize", "'{0}'")> _
        Public Property EventSize() As String
            Get
                Return _eventSize
            End Get
            Set(ByVal value As String)
                _eventSize = value
            End Set
        End Property


        <ColumnInfo("NumberOfDay", "{0}")> _
        Public Property NumberOfDay() As Integer
            Get
                Return _numberOfDay
            End Get
            Set(ByVal value As Integer)
                _numberOfDay = value
            End Set
        End Property


        <ColumnInfo("SalesTarget", "{0}")> _
        Public Property SalesTarget() As Decimal
            Get
                Return _salesTarget
            End Get
            Set(ByVal value As Decimal)
                _salesTarget = value
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



        <RelationInfo("BPEvent", "ID", "EventActivity", "BPEventID")> _
        Public ReadOnly Property EventActivitys() As System.Collections.ArrayList
            Get
                Try
                    If (Me._eventActivitys.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(EventActivity), "BPEvent", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(EventActivity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._eventActivitys = DoLoadArray(GetType(EventActivity).ToString, criterias)
                    End If

                    Return Me._eventActivitys

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("BPEvent", "ID", "BabitProposal", "BPEventID")> _
        Public ReadOnly Property BabitProposals() As System.Collections.ArrayList
            Get
                Try
                    If (Me._babitProposals.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BabitProposal), "BPEvent", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._babitProposals = DoLoadArray(GetType(BabitProposal).ToString, criterias)
                    End If

                    Return Me._babitProposals

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

        Public ReadOnly Property TotalExpense() As Decimal
            Get
                Dim total As Decimal = 0
                For Each item As EventActivity In Me.EventActivitys
                    total += item.Comsumption + item.Entertainment + item.Equipment + item.Place + item.Others
                Next
                Return total
            End Get
        End Property

        Public ReadOnly Property PlaceExpense() As Decimal
            Get
                Dim total As Decimal = 0
                For Each item As EventActivity In Me.EventActivitys
                    total += item.Place
                Next
                Return total
            End Get
        End Property


#End Region

    End Class
End Namespace

