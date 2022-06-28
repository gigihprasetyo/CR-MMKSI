
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrCourseCategory Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 20/06/2019 - 8:48:41
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
    <Serializable(), TableInfo("TrCourseCategory")> _
    Public Class TrCourseCategory
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
        Private _code As String = String.Empty
        Private _description As String = String.Empty
        Private _salesmanLevel As SalesmanLevel
        Private _isMandatory As Boolean
        Private _jobPositionCategory As JobPositionCategory
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _listOfJobPosition As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        <ColumnInfo("SalesmanLevelID", "{0}"), _
       RelationInfo("SalesmanLevel", "ID", "TrCourseCategory", "SalesmanLevelID")> _
        Public Property SalesmanLevel() As SalesmanLevel
            Get
                Try
                    If Not IsNothing(Me._salesmanLevel) AndAlso (Not Me._salesmanLevel.IsLoaded) Then

                        Me._salesmanLevel = CType(DoLoad(GetType(SalesmanLevel).ToString(), _salesmanLevel.ID), SalesmanLevel)
                        Me._salesmanLevel.MarkLoaded()

                    End If

                    Return Me._salesmanLevel

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanLevel)

                Me._salesmanLevel = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanLevel.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("IsMandatory", "{0}")> _
        Public Property IsMandatory As Boolean
            Get
                Return _isMandatory
            End Get
            Set(ByVal value As Boolean)
                _isMandatory = value
            End Set
        End Property

        <ColumnInfo("JobPositionCategoryID", "{0}"), _
     RelationInfo("JobPositionCategory", "ID", "TrCourseCategory", "JobPositionCategoryID")> _
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


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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

        <RelationInfo("TrCourseCategory", "ID", "TrCourseCategoryToJobPosition", "TrCourseCategoryID")> _
        Public ReadOnly Property ListOfJobPosition() As System.Collections.ArrayList
            Get
                Try
                    If (Me._listOfJobPosition.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TrCourseCategoryToJobPosition), "TrCourseCategory.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(TrCourseCategoryToJobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._listOfJobPosition = DoLoadArray(GetType(TrCourseCategoryToJobPosition).ToString, criterias)
                    End If

                    Return Me._listOfJobPosition

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

