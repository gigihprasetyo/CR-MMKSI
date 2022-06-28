#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrScheduleUpload Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2005 - 8:35:11 AM
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
    <Serializable(), TableInfo("TrScheduleUpload")> _
    Public Class TrScheduleUpload
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
        Private _scheduleYear As String = String.Empty
        Private _name As String = String.Empty
        Private _description As String = String.Empty
        Private _uploadDate As DateTime = System.DateTime.MinValue
        Private _filePath As String = String.Empty
        Private _status As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = System.DateTime.MinValue
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = System.DateTime.MinValue
        Private _jobPositionCategory As JobPositionCategory


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


        <ColumnInfo("ScheduleYear", "'{0}'")> _
        Public Property ScheduleYear() As String
            Get
                Return _scheduleYear
            End Get
            Set(ByVal value As String)
                _scheduleYear = value
            End Set
        End Property


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
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


        <ColumnInfo("UploadDate", "'{0:yyyy/MM/dd}'")> _
        Public Property UploadDate() As DateTime
            Get
                Return _uploadDate
            End Get
            Set(ByVal value As DateTime)
                _uploadDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("FilePath", "'{0}'")> _
        Public Property FilePath() As String
            Get
                Return _filePath
            End Get
            Set(ByVal value As String)
                _filePath = value
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

        <ColumnInfo("JobPositionCategoryID", "{0}"), _
        RelationInfo("JobPositionCategory", "ID", "TrScheduleUpload", "JobPositionCategoryID")> _
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

#Region "Custom Method"

#End Region

    End Class
End Namespace

