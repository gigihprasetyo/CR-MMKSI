
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DN
'// PURPOSE       : TrInhouseMember Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/7/2009 - 11:34:58 AM
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
    <Serializable(), TableInfo("TrInhouseMember")> _
    Public Class TrInhouseMember
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _inhouseID As Integer
        Private _courseID As Integer
        Private _traineeID As Integer
        Private _classID As Integer
        Private _result As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _iD As Integer


        Private _trInhouse As TrInhouse
        Private _trCourse As TrCourse
        Private _trClass As TrClass
        Private _trTrainee As TrTrainee

#End Region

#Region "Public Properties"

        <ColumnInfo("InhouseID", "{0}")> _
        Public Property InhouseID() As Integer
            Get
                Return _inhouseID
            End Get
            Set(ByVal value As Integer)
                _inhouseID = value
            End Set
        End Property



        <ColumnInfo("CourseID", "{0}")> _
        Public Property CourseID() As Integer
            Get
                Return _courseID
            End Get
            Set(ByVal value As Integer)
                _courseID = value
            End Set
        End Property


        <ColumnInfo("TraineeID", "{0}")> _
        Public Property TraineeID() As Integer
            Get
                Return _traineeID
            End Get
            Set(ByVal value As Integer)
                _traineeID = value
            End Set
        End Property


        <ColumnInfo("ClassID", "{0}")> _
        Public Property ClassID() As Integer
            Get
                Return _classID
            End Get
            Set(ByVal value As Integer)
                _classID = value
            End Set
        End Property


        <ColumnInfo("Result", "#,##0")> _
        Public Property Result() As Decimal
            Get
                Return _result
            End Get
            Set(ByVal value As Decimal)
                _result = value
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


        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer

            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property

        <ColumnInfo("InhouseID", "{0}"), _
                RelationInfo("TrInhouse", "ID", "TrInhouseInformation", "InhouseID")> _
                Public Property TrInhouse() As TrInhouse
            Get
                Try
                    If IsNothing(Me._trInhouse) Then ' AndAlso (Not Me._trInhouse.IsLoaded) Then

                        Me._trInhouse = CType(DoLoad(GetType(TrInhouse).ToString(), Me._inhouseID), TrInhouse)
                        Me._trInhouse.MarkLoaded()

                    End If

                    Return Me._trInhouse

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrInhouse)

                Me._trInhouse = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trInhouse.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CourseID", "{0}"), _
        RelationInfo("TrCourse", "ID", "TrInhouseMember", "CourseID")> _
        Public Property TrCourse() As TrCourse
            Get
                Try
                    If IsNothing(Me._trCourse) Then '  AndAlso (Not Me._TrCourse.IsLoaded) Then
                        Me._trCourse = CType(DoLoad(GetType(TrCourse).ToString(), Me._courseID), TrCourse)
                        Me._trCourse.MarkLoaded()

                    End If

                    Return Me._trCourse

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrCourse)

                Me._trCourse = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trCourse.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ClassID", "{0}"), _
        RelationInfo("TrClass", "ID", "TrInhouseClass", "ClassID")> _
        Public Property TrClass() As TrClass
            Get
                Try
                    If IsNothing(Me._trClass) Then '  AndAlso (Not Me._trClass.IsLoaded) Then

                        Me._trClass = CType(DoLoad(GetType(TrClass).ToString(), Me._classID), TrClass)
                        Me._trClass.MarkLoaded()

                    End If

                    Return Me._trClass

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrClass)

                Me._trClass = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trClass.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("TraineeID", "{0}"), _
        RelationInfo("TrTrainee", "ID", "TrInhouseMember", "TraineeID")> _
        Public Property TrTrainee() As TrTrainee
            Get
                Try
                    If IsNothing(Me._trTrainee) Then ' AndAlso (Not Me._trTrainee.IsLoaded) Then

                        Me._trTrainee = CType(DoLoad(GetType(TrTrainee).ToString(), Me._traineeID), TrTrainee)
                        Me._trTrainee.MarkLoaded()

                    End If

                    Return Me._trTrainee

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrTrainee)

                Me._trTrainee = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trTrainee.MarkLoaded()
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

