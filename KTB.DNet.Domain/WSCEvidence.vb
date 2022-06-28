#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCEvidence Domain Object.
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
    <Serializable(), TableInfo("WSCEvidence")> _
    Public Class WSCEvidence
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
        Private _wSCHeader As WSCHeader
        Private _isFromPQR As Boolean = False
        Private _pQRFilePath As String = String.Empty
        Private _tempFilePath As String = String.Empty


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




        <ColumnInfo("WSCHeaderID", "{0}"), _
        RelationInfo("WSCHeader", "ID", "WSCEvidence", "WSCHeaderID")> _
        Public Property WSCHeader() As WSCHeader
            Get
                Try
                    If Not IsNothing(Me._wSCHeader) AndAlso (Not Me._wSCHeader.IsLoaded) Then

                        Me._wSCHeader = CType(DoLoad(GetType(WSCHeader).ToString(), _wSCHeader.ID), WSCHeader)
                        Me._wSCHeader.MarkLoaded()

                    End If

                    Return Me._wSCHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As WSCHeader)

                Me._wSCHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._wSCHeader.MarkLoaded()
                End If
            End Set
        End Property

        Public Property IsFromPQR() As Boolean
            Get
                Return _isFromPQR
            End Get
            Set(value As Boolean)
                _isFromPQR = value
            End Set
        End Property

        Public Property PQRFilePath() As String
            Get
                Return _pQRFilePath
            End Get
            Set(value As String)
                _pQRFilePath = value
            End Set
        End Property

        Public Property TempFilePath() As String
            Get
                Return _tempFilePath
            End Get
            Set(value As String)
                _tempFilePath = value
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

