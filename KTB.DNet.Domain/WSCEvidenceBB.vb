#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCEvidenceBB Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 10/26/2005 - 2:36:57 PM
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
    <Serializable(), TableInfo("WSCEvidenceBB")> _
    Public Class WSCEvidenceBB
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
        Private _evidenceType As Short
        Private _description As String = String.Empty
        Private _status As Short
        Private _uploadDate As Short
        Private _uploadMonth As Short
        Private _uploadYear As Short
        Private _pathFile As String = String.Empty
        Private _downloadDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _downloadBy As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _AttachmentData As System.Web.HttpPostedFile
        Private _WSCHeaderBB As WSCHeaderBB



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

        <ColumnInfo("EvidenceType", "{0}")> _
                Public Property EvidenceType() As Short
            Get
                Return _evidenceType
            End Get
            Set(ByVal value As Short)
                _evidenceType = value
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


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property


        <ColumnInfo("UploadDate", "{0}")> _
        Public Property UploadDate() As Short
            Get
                Return _uploadDate
            End Get
            Set(ByVal value As Short)
                _uploadDate = value
            End Set
        End Property


        <ColumnInfo("UploadMonth", "{0}")> _
        Public Property UploadMonth() As Short
            Get
                Return _uploadMonth
            End Get
            Set(ByVal value As Short)
                _uploadMonth = value
            End Set
        End Property


        <ColumnInfo("UploadYear", "{0}")> _
        Public Property UploadYear() As Short
            Get
                Return _uploadYear
            End Get
            Set(ByVal value As Short)
                _uploadYear = value
            End Set
        End Property


        <ColumnInfo("PathFile", "'{0}'")> _
        Public Property PathFile() As String
            Get
                Return _pathFile
            End Get
            Set(ByVal value As String)
                _pathFile = value
            End Set
        End Property


        <ColumnInfo("DownloadDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DownloadDate() As DateTime
            Get
                Return _downloadDate
            End Get
            Set(ByVal value As DateTime)
                _downloadDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DownloadBy", "'{0}'")> _
        Public Property DownloadBy() As String
            Get
                Return _downloadBy
            End Get
            Set(ByVal value As String)
                _downloadBy = value
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




        <ColumnInfo("WSCHeaderBBID", "{0}"), _
        RelationInfo("WSCHeaderBB", "ID", "WSCEvidenceBB", "WSCHeaderBBID")> _
        Public Property WSCHeaderBB() As WSCHeaderBB
            Get
                Try
                    If Not IsNothing(Me._WSCHeaderBB) AndAlso (Not Me._WSCHeaderBB.IsLoaded) Then

                        Me._WSCHeaderBB = CType(DoLoad(GetType(WSCHeaderBB).ToString(), _WSCHeaderBB.ID), WSCHeaderBB)
                        Me._WSCHeaderBB.MarkLoaded()

                    End If

                    Return Me._WSCHeaderBB

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As WSCHeaderBB)

                Me._WSCHeaderBB = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._WSCHeaderBB.MarkLoaded()
                End If
            End Set
        End Property




#End Region

#Region "Custom Method"
        Public Property AttachmentData() As System.Web.HttpPostedFile
            Get
                Return _AttachmentData
            End Get

            Set(ByVal value As System.Web.HttpPostedFile)
                _AttachmentData = value
            End Set
        End Property

#End Region

    End Class
End Namespace

