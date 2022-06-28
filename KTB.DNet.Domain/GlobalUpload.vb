#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : GlobalUpload Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/20/2019 - 1:30:52 PM
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
    <Serializable(), TableInfo("GlobalUpload")> _
    Public Class GlobalUpload
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
        Private _displayName As String = String.Empty
        Private _privilegeName As String = String.Empty
        Private _objectName As String = String.Empty
        Private _facadeName As String = String.Empty
        Private _parserName As String = String.Empty
        Private _uploadMethodName As String = String.Empty
        Private _downloadMethodName As String = String.Empty
        Private _downloadFileName As String = String.Empty
        Private _maxFileSize As Integer = 0
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
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

        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property

        <ColumnInfo("DisplayName", "'{0}'")> _
        Public Property DisplayName As String
            Get
                Return _displayName
            End Get
            Set(ByVal value As String)
                _displayName = value
            End Set
        End Property


        <ColumnInfo("PrivilegeName", "'{0}'")> _
        Public Property PrivilegeName As String
            Get
                Return _privilegeName
            End Get
            Set(ByVal value As String)
                _privilegeName = value
            End Set
        End Property


        <ColumnInfo("ObjectName", "'{0}'")> _
        Public Property ObjectName As String
            Get
                Return _objectName
            End Get
            Set(ByVal value As String)
                _objectName = value
            End Set
        End Property


        <ColumnInfo("FacadeName", "'{0}'")> _
        Public Property FacadeName As String
            Get
                Return _facadeName
            End Get
            Set(ByVal value As String)
                _facadeName = value
            End Set
        End Property

        <ColumnInfo("ParserName", "'{0}'")> _
        Public Property ParserName As String
            Get
                Return _parserName
            End Get
            Set(ByVal value As String)
                _parserName = value
            End Set
        End Property


        <ColumnInfo("UploadMethodName", "'{0}'")> _
        Public Property UploadMethodName As String
            Get
                Return _uploadMethodName
            End Get
            Set(ByVal value As String)
                _uploadMethodName = value
            End Set
        End Property


        <ColumnInfo("DownloadMethodName", "'{0}'")> _
        Public Property DownloadMethodName As String
            Get
                Return _downloadMethodName
            End Get
            Set(ByVal value As String)
                _downloadMethodName = value
            End Set
        End Property


        <ColumnInfo("DownloadFileName", "'{0}'")> _
        Public Property DownloadFileName As String
            Get
                Return _downloadFileName
            End Get
            Set(ByVal value As String)
                _downloadFileName = value
            End Set
        End Property

        <ColumnInfo("MaxFileSize", "'{0}'")> _
        Public Property MaxFileSize As Integer
            Get
                Return _maxFileSize
            End Get
            Set(ByVal value As Integer)
                _maxFileSize = value
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


        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
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
