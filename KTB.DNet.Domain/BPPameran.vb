#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BPPameran Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/23/2007 - 4:49:11 PM
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
    <Serializable(), TableInfo("BPPameran")> _
    Public Class BPPameran
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
        Private _startExhibitionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _endExhibitionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _exhibitionSize As String = String.Empty
        Private _numberOfDay As Integer
        Private _salesTarget As Decimal
        Private _expense As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _babitProposals As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _pameranDisplays As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("StartExhibitionDate", "'{0:yyyy/MM/dd}'")> _
        Public Property StartExhibitionDate() As DateTime
            Get
                Return _startExhibitionDate
            End Get
            Set(ByVal value As DateTime)
                _startExhibitionDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("EndExhibitionDate", "'{0:yyyy/MM/dd}'")> _
        Public Property EndExhibitionDate() As DateTime
            Get
                Return _endExhibitionDate
            End Get
            Set(ByVal value As DateTime)
                _endExhibitionDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ExhibitionSize", "'{0}'")> _
        Public Property ExhibitionSize() As String
            Get
                Return _exhibitionSize
            End Get
            Set(ByVal value As String)
                _exhibitionSize = value
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


        <ColumnInfo("Expense", "{0}")> _
        Public Property Expense() As Decimal
            Get
                Return _expense
            End Get
            Set(ByVal value As Decimal)
                _expense = value
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



        <RelationInfo("BPPameran", "ID", "BabitProposal", "BPPameranID")> _
        Public ReadOnly Property BabitProposals() As System.Collections.ArrayList
            Get
                Try
                    If (Me._babitProposals.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BabitProposal), "BPPameran", Me.ID)
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

        <RelationInfo("BPPameran", "ID", "PameranDisplay", "BPPameranID")> _
        Public ReadOnly Property PameranDisplays() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pameranDisplays.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PameranDisplay), "BPPameran", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PameranDisplay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pameranDisplays = DoLoadArray(GetType(PameranDisplay).ToString, criterias)
                    End If

                    Return Me._pameranDisplays

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
                Dim _totalExpense As Decimal = 0
                For Each item As PameranDisplay In Me.PameranDisplays
                    totalExpense += item.Others
                Next
                _totalExpense += _expense
                Return _totalExpense
            End Get
        End Property
#End Region

    End Class
End Namespace

