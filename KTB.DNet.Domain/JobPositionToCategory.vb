
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : JobPositionToCategory Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 30/04/2019 - 12:42:03
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
    <Serializable(), TableInfo("JobPositionToCategory")> _
    Public Class JobPositionToCategory
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
        Private _jobPositionID As Integer
        Private _jobPosition As JobPosition
        Private _jobPositionCategory As JobPositionCategory
        Private _categoryID As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

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

        <ColumnInfo("JobPositionID", "{0}"), _
       RelationInfo("JobPosition", "ID", "JobPositionToCategory", "JobPositionID")> _
        Public Property JobPosition() As JobPosition
            Get
                Try
                    If Not IsNothing(Me._jobPosition) AndAlso (Not Me._jobPosition.IsLoaded) Then

                        Me._jobPosition = CType(DoLoad(GetType(JobPosition).ToString(), _jobPosition.ID), JobPosition)
                        Me._jobPosition.MarkLoaded()

                    End If

                    Return Me._jobPosition

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As JobPosition)

                Me._jobPosition = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._jobPosition.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("JobPositionID", "{0}")> _
        Public Property JobPositionID As Integer
            Get
                Return _jobPositionID
            End Get
            Set(ByVal value As Integer)
                _jobPositionID = value
            End Set
        End Property

        <ColumnInfo("CategoryID", "{0}")> _
        Public Property CategoryID() As Integer
            Get
                Return _categoryID
            End Get
            Set(ByVal value As Integer)
                _categoryID = value
            End Set
        End Property

        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("JobPositionCategory", "ID", "JobPositionToCategory", "CategoryID")> _
        Public Property JobPositionCategory As JobPositionCategory
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

