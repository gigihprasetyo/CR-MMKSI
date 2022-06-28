#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Babit Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/22/2007 - 1:20:02 PM
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
    <Serializable(), TableInfo("Babit")> _
    Public Class Babit
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
        Private _allocationType As Integer
        Private _startPeriod As Integer
        Private _endPeriod As Integer
        Private _babitYear As Integer
        Private _babitYearEnd As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _babitAllocations As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("AllocationType", "{0}")> _
        Public Property AllocationType() As Integer
            Get
                Return _allocationType
            End Get
            Set(ByVal value As Integer)
                _allocationType = value
            End Set
        End Property


        <ColumnInfo("StartPeriod", "{0}")> _
        Public Property StartPeriod() As Integer
            Get
                Return _startPeriod
            End Get
            Set(ByVal value As Integer)
                _startPeriod = value
            End Set
        End Property


        <ColumnInfo("EndPeriod", "{0}")> _
        Public Property EndPeriod() As Integer
            Get
                Return _endPeriod
            End Get
            Set(ByVal value As Integer)
                _endPeriod = value
            End Set
        End Property


        <ColumnInfo("BabitYear", "{0}")> _
        Public Property BabitYear() As Integer
            Get
                Return _babitYear
            End Get
            Set(ByVal value As Integer)
                _babitYear = value
            End Set
        End Property


        <ColumnInfo("BabitYearEnd", "{0}")> _
        Public Property BabitYearEnd() As Integer
            Get
                Return _babitYearEnd
            End Get
            Set(ByVal value As Integer)
                _babitYearEnd = value
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



        <RelationInfo("Babit", "ID", "BabitAllocation", "BabitID")> _
        Public ReadOnly Property BabitAllocations() As System.Collections.ArrayList
            Get
                Try
                    If (Me._babitAllocations.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BabitAllocation), "Babit", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._babitAllocations = DoLoadArray(GetType(BabitAllocation).ToString, criterias)
                    End If

                    Return Me._babitAllocations

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

