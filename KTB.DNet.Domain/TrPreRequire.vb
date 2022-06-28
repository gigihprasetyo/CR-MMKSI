#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrPreRequire Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/11/2005 - 2:13:58 PM
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
    <Serializable(), TableInfo("TrPreRequire")> _
    Public Class TrPreRequire
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
        Private _description As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _Prerequireduration As Integer
        Private _preRequireCourseID As Integer
        Private _trCourse As TrCourse
        Private _preRequireCourse As TrCourse
        Private _requireType As Short



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


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        <ColumnInfo("RequireType", "{0}")> _
        Public Property RequireType() As Short
            Get
                Return _requireType
            End Get
            Set(ByVal value As Short)
                _requireType = value
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


        <ColumnInfo("Prerequireduration", "{0}")> _
        Public Property Prerequireduration() As Integer

            Get
                Return _Prerequireduration
            End Get
            Set(ByVal value As Integer)
                _Prerequireduration = value
            End Set
        End Property

        <ColumnInfo("PreRequireCourseID", "{0}")> _
        Public Property PreRequireCourseID() As Decimal

            Get
                Return _preRequireCourseID
            End Get
            Set(ByVal value As Decimal)
                _preRequireCourseID = value
            End Set
        End Property

        <ColumnInfo("CourseID", "{0}"), _
        RelationInfo("TrCourse", "ID", "TrPreRequire", "CourseID")> _
        Public Property TrCourse() As TrCourse
            Get
                Try
                    If Not IsNothing(Me._trCourse) AndAlso (Not Me._trCourse.IsLoaded) Then

                        Me._trCourse = CType(DoLoad(GetType(TrCourse).ToString(), _trCourse.ID), TrCourse)
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


        '<ColumnInfo("PreRequireCourseID", "{0}"), _
        'RelationInfo("TrCourse", "ID", "TrPreRequire", "PreRequireCourseID")> _
        'Public Property PreRequireCourse() As TrCourse
        '    Get
        '        Try
        '            If Not IsNothing(Me._preRequireCourse) AndAlso (Not Me._preRequireCourse.IsLoaded) Then

        '                Me._preRequireCourse = CType(DoLoad(GetType(TrCourse).ToString(), _preRequireCourse.ID), TrCourse)
        '                Me._preRequireCourse.MarkLoaded()

        '            End If

        '            Return Me._preRequireCourse

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing
        '    End Get

        '    Set(ByVal value As TrCourse)

        '        Me._preRequireCourse = value
        '        If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
        '            Me._preRequireCourse.MarkLoaded()
        '        End If
        '    End Set
        'End Property

#End Region

#Region "Non Generate Property"

        Private _PreRequireCourseCode As String = String.Empty

        Public Property PreRequireCourseCode() As String
            Get
                Return _PreRequireCourseCode
            End Get
            Set(ByVal value As String)
                _PreRequireCourseCode = value
            End Set
        End Property

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

