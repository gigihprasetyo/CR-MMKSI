#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MessageService Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/21/2020 - 10:17:55 AM
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
    <Serializable(), TableInfo("MessageService")> _
    Public Class MessageService
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
        Private _refID As String = String.Empty
        Private _userName As String = String.Empty
        Private _type As Short
        Private _time As String = String.Empty
        Private _sender As String = String.Empty
        Private _subject As String = String.Empty
        Private _message As String = String.Empty
        Private _backupEx As String = String.Empty
        Private _backupOn As String = String.Empty
        Private _attachment As String = String.Empty
        Private _reffSource As String = String.Empty
        Private _fID As Integer
        Private _status As Short
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty




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


        <ColumnInfo("RefID", "'{0}'")> _
        Public Property RefID As String
            Get
                Return _refID
            End Get
            Set(ByVal value As String)
                _refID = value
            End Set
        End Property


        <ColumnInfo("UserName", "'{0}'")> _
        Public Property UserName As String
            Get
                Return _userName
            End Get
            Set(ByVal value As String)
                _userName = value
            End Set
        End Property


        <ColumnInfo("Type", "{0}")> _
        Public Property Type As Short
            Get
                Return _type
            End Get
            Set(ByVal value As Short)
                _type = value
            End Set
        End Property


        <ColumnInfo("Time", "'{0}'")> _
        Public Property Time As String
            Get
                Return _time
            End Get
            Set(ByVal value As String)
                _time = value
            End Set
        End Property


        <ColumnInfo("Sender", "'{0}'")> _
        Public Property Sender As String
            Get
                Return _sender
            End Get
            Set(ByVal value As String)
                _sender = value
            End Set
        End Property


        <ColumnInfo("Subject", "'{0}'")> _
        Public Property Subject As String
            Get
                Return _subject
            End Get
            Set(ByVal value As String)
                _subject = value
            End Set
        End Property


        <ColumnInfo("Message", "'{0}'")> _
        Public Property Message As String
            Get
                Return _message
            End Get
            Set(ByVal value As String)
                _message = value
            End Set
        End Property


        <ColumnInfo("BackupEx", "'{0}'")> _
        Public Property BackupEx As String
            Get
                Return _backupEx
            End Get
            Set(ByVal value As String)
                _backupEx = value
            End Set
        End Property


        <ColumnInfo("BackupOn", "'{0}'")> _
        Public Property BackupOn As String
            Get
                Return _backupOn
            End Get
            Set(ByVal value As String)
                _backupOn = value
            End Set
        End Property


        <ColumnInfo("Attachment", "'{0}'")> _
        Public Property Attachment As String
            Get
                Return _attachment
            End Get
            Set(ByVal value As String)
                _attachment = value
            End Set
        End Property


        <ColumnInfo("ReffSource", "'{0}'")> _
        Public Property ReffSource As String
            Get
                Return _reffSource
            End Get
            Set(ByVal value As String)
                _reffSource = value
            End Set
        End Property


        <ColumnInfo("FID", "{0}")> _
        Public Property FID As Integer
            Get
                Return _fID
            End Get
            Set(ByVal value As Integer)
                _fID = value
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


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
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
