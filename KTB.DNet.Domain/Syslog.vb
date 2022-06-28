#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SysLog Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 19/09/2007 - 11:20:16
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
    <Serializable(), TableInfo("SysLog")> _
    Public Class SysLog
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
        Private _status As String = String.Empty
        Private _moduleName As String = String.Empty
        Private _remoteIPAddress As String = String.Empty
        Private _userName As String = String.Empty
        Private _pages As String = String.Empty
        Private _blockName As String = String.Empty
        Private _subBlockName As String = String.Empty
        Private _action As String = String.Empty
        Private _resultCode As String = String.Empty
        Private _fullMessage As String = String.Empty
        Private _logTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property


        <ColumnInfo("ModuleName", "'{0}'")> _
        Public Property ModuleName() As String
            Get
                Return _moduleName
            End Get
            Set(ByVal value As String)
                _moduleName = value
            End Set
        End Property


        <ColumnInfo("RemoteIPAddress", "'{0}'")> _
        Public Property RemoteIPAddress() As String
            Get
                Return _remoteIPAddress
            End Get
            Set(ByVal value As String)
                _remoteIPAddress = value
            End Set
        End Property


        <ColumnInfo("UserName", "'{0}'")> _
        Public Property UserName() As String
            Get
                Return _userName
            End Get
            Set(ByVal value As String)
                _userName = value
            End Set
        End Property


        <ColumnInfo("Pages", "'{0}'")> _
        Public Property Pages() As String
            Get
                Return _pages
            End Get
            Set(ByVal value As String)
                _pages = value
            End Set
        End Property


        <ColumnInfo("BlockName", "'{0}'")> _
        Public Property BlockName() As String
            Get
                Return _blockName
            End Get
            Set(ByVal value As String)
                _blockName = value
            End Set
        End Property


        <ColumnInfo("SubBlockName", "'{0}'")> _
        Public Property SubBlockName() As String
            Get
                Return _subBlockName
            End Get
            Set(ByVal value As String)
                _subBlockName = value
            End Set
        End Property


        <ColumnInfo("Action", "'{0}'")> _
        Public Property Action() As String
            Get
                Return _action
            End Get
            Set(ByVal value As String)
                _action = value
            End Set
        End Property


        <ColumnInfo("ResultCode", "'{0}'")> _
        Public Property ResultCode() As String
            Get
                Return _resultCode
            End Get
            Set(ByVal value As String)
                _resultCode = value
            End Set
        End Property


        <ColumnInfo("FullMessage", "'{0}'")> _
        Public Property FullMessage() As String
            Get
                Return _fullMessage
            End Get
            Set(ByVal value As String)
                _fullMessage = value
            End Set
        End Property


        <ColumnInfo("LogTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LogTime() As DateTime
            Get
                Return _logTime
            End Get
            Set(ByVal value As DateTime)
                _logTime = value
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

