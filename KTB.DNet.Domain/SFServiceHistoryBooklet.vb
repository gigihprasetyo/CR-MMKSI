#region "Summary"
'// ===========================================================================
'// AUTHOR        : dnet
'// PURPOSE       : SFServiceHistoryBooklet Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 10/15/2021 - 9:51:53 AM
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
    <Serializable(), TableInfo("SFServiceHistoryBooklet")> _
    Public Class SFServiceHistoryBooklet
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
        Private _keyID As String = String.Empty
        Private _pMHeaderID As Integer
        Private _assistServiceIncomingID As Integer
        Private _isSynchronizeSF As Boolean
        Private _synchronizeDateSF As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _isSynchronizeMMID As Boolean
        Private _synchronizeDateMMID As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _isActive As Boolean
        Private _sFID As String = String.Empty
        Private _eNDCustomerID As Integer
        Private _rowStatus As Short
        Private _retrySF As Short
        Private _retryMMID As Short
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


        <ColumnInfo("KeyID", "'{0}'")> _
        Public Property KeyID As String
            Get
                Return _keyID
            End Get
            Set(ByVal value As String)
                _keyID = value
            End Set
        End Property
        <ColumnInfo("EndCustomerID", "{0}")> _
        Public Property EndCustomerID As Integer
            Get
                Return _eNDCustomerID
            End Get
            Set(ByVal value As Integer)
                _eNDCustomerID = value
            End Set
        End Property


        <ColumnInfo("PMHeaderID", "{0}")> _
        Public Property PMHeaderID As Integer
            Get
                Return _pMHeaderID
            End Get
            Set(ByVal value As Integer)
                _pMHeaderID = value
            End Set
        End Property


        <ColumnInfo("AssistServiceIncomingID", "{0}")> _
        Public Property AssistServiceIncomingID As Integer
            Get
                Return _assistServiceIncomingID
            End Get
            Set(ByVal value As Integer)
                _assistServiceIncomingID = value
            End Set
        End Property


        <ColumnInfo("IsSynchronizeSF", "{0}")> _
        Public Property IsSynchronizeSF As Boolean
            Get
                Return _isSynchronizeSF
            End Get
            Set(ByVal value As Boolean)
                _isSynchronizeSF = value
            End Set
        End Property


        <ColumnInfo("SynchronizeDateSF", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property SynchronizeDateSF As DateTime
            Get
                Return _synchronizeDateSF
            End Get
            Set(ByVal value As DateTime)
                _synchronizeDateSF = value
            End Set
        End Property


        <ColumnInfo("IsSynchronizeMMID", "{0}")> _
        Public Property IsSynchronizeMMID As Boolean
            Get
                Return _isSynchronizeMMID
            End Get
            Set(ByVal value As Boolean)
                _isSynchronizeMMID = value
            End Set
        End Property


        <ColumnInfo("SynchronizeDateMMID", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property SynchronizeDateMMID As DateTime
            Get
                Return _synchronizeDateMMID
            End Get
            Set(ByVal value As DateTime)
                _synchronizeDateMMID = value
            End Set
        End Property


        <ColumnInfo("IsActive", "{0}")> _
        Public Property IsActive As Boolean
            Get
                Return _isActive
            End Get
            Set(ByVal value As Boolean)
                _isActive = value
            End Set
        End Property


        <ColumnInfo("SFID", "'{0}'")> _
        Public Property SFID As String
            Get
                Return _sFID
            End Get
            Set(ByVal value As String)
                _sFID = value
            End Set
        End Property

        <ColumnInfo("RetrySF", "{0}")> _
        Public Property RetrySF As Short
            Get
                Return _retrySF
            End Get
            Set(ByVal value As Short)
                _retrySF = value
            End Set
        End Property
        <ColumnInfo("RetryMMID", "{0}")> _
        Public Property RetryMMID As Short
            Get
                Return _retryMMID
            End Get
            Set(ByVal value As Short)
                _retryMMID = value
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
