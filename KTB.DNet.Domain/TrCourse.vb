
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrCourse Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/3/2006 - 11:00:41 AM
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
    <Serializable(), TableInfo("TrCourse")> _
    Public Class TrCourse
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
        Private _courseCode As String = String.Empty
        Private _courseName As String = String.Empty
        Private _description As String = String.Empty
        Private _requireWorkDate As Boolean
        Private _status As String = String.Empty
        Private _passingScore As Decimal
        Private _notes As String = String.Empty
        Private _category As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _trPreRequires As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _trClasss As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _classCode As String = String.Empty
        Private _workdate As Integer
        Private _paymentType As Short
        Private _jobPositionCategory As JobPositionCategory
        Private _trCourseCategory As TrCourseCategory
        Private _trTraineeLevel As TrTraineeLevel

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


        <ColumnInfo("CourseCode", "'{0}'")> _
        Public Property CourseCode() As String
            Get
                Return _courseCode
            End Get
            Set(ByVal value As String)
                _courseCode = value
            End Set
        End Property


        <ColumnInfo("CourseName", "'{0}'")> _
        Public Property CourseName() As String
            Get
                Return _courseName
            End Get
            Set(ByVal value As String)
                _courseName = value
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

        <ColumnInfo("ClassCode", "'{0}'")> _
        Public Property ClassCode() As String
            Get
                Return _classCode
            End Get
            Set(ByVal value As String)
                _classCode = value
            End Set
        End Property


        <ColumnInfo("RequireWorkDate", "{0}")> _
        Public Property RequireWorkDate() As Boolean
            Get
                Return _requireWorkDate
            End Get
            Set(ByVal value As Boolean)
                _requireWorkDate = value
            End Set
        End Property

        <ColumnInfo("WorkDate", "{0}")> _
        Public Property WorkDate() As Integer
            Get
                Return _workdate
            End Get
            Set(ByVal value As Integer)
                _workdate = value
            End Set
        End Property

        <ColumnInfo("PaymentType", "{0}")> _
        Public Property PaymentType() As Short
            Get
                Return _paymentType
            End Get
            Set(ByVal value As Short)
                _paymentType = value
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


        <ColumnInfo("PassingScore", "#,##0")> _
        Public Property PassingScore() As Decimal
            Get
                Return _passingScore
            End Get
            Set(ByVal value As Decimal)
                _passingScore = value
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


        <ColumnInfo("Category", "{0}"), _
        RelationInfo("TrCourseCategory", "ID", "TrCourse", "Category")> _
        Public Property Category() As TrCourseCategory
            Get
                Try
                    If Not IsNothing(Me._trCourseCategory) AndAlso (Not Me._trCourseCategory.IsLoaded) Then

                        Me._trCourseCategory = CType(DoLoad(GetType(TrCourseCategory).ToString(), _trCourseCategory.ID), TrCourseCategory)
                        Me._trCourseCategory.MarkLoaded()

                    End If

                    Return Me._trCourseCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
            Set(ByVal value As TrCourseCategory)
                Me._trCourseCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trCourseCategory.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("JobPositionCategoryID", "{0}"), _
        RelationInfo("JobPositionCategory", "ID", "TrCourse", "JobPositionCategoryID")> _
        Public Property JobPositionCategory() As JobPositionCategory
            Get
                Try
                    If Not IsNothing(Me._jobPositionCategory) AndAlso (Not Me._jobPositionCategory.IsLoaded) Then

                        Me._jobPositionCategory = CType(DoLoad(GetType(JobPositionCategory).ToString(), _jobPositionCategory.ID), JobPositionCategory)
                        Me._jobPositionCategory.MarkLoaded()

                    End If

                    Return Me._jobPositionCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As JobPositionCategory)

                Me._jobPositionCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._jobPositionCategory.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("TrTraineeLevelID", "{0}"), _
       RelationInfo("TrTraineeLevel", "ID", "TrCourse", "TrTraineeLevelID")> _
        Public Property TrTraineeLevel() As TrTraineeLevel
            Get
                Try
                    If Not IsNothing(Me._trTraineeLevel) AndAlso (Not Me._trTraineeLevel.IsLoaded) Then

                        Me._trTraineeLevel = CType(DoLoad(GetType(TrTraineeLevel).ToString(), _trTraineeLevel.ID), TrTraineeLevel)
                        Me._trTraineeLevel.MarkLoaded()

                    End If

                    Return Me._trTraineeLevel

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrTraineeLevel)

                Me._trTraineeLevel = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trTraineeLevel.MarkLoaded()
                End If
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



        <RelationInfo("TrCourse", "ID", "TrPreRequire", "CourseID")> _
        Public ReadOnly Property TrPreRequires() As System.Collections.ArrayList
            Get
                Try
                    If (Me._trPreRequires.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TrPreRequire), "TrCourse", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(TrPreRequire), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._trPreRequires = DoLoadArray(GetType(TrPreRequire).ToString, criterias)
                    End If

                    Return Me._trPreRequires

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("TrCourse", "ID", "TrClass", "CourseID")> _
        Public ReadOnly Property TrClasss() As System.Collections.ArrayList
            Get
                Try
                    If (Me._trClasss.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TrClass), "TrCourse", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._trClasss = DoLoadArray(GetType(TrClass).ToString, criterias)
                    End If

                    Return Me._trClasss

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

