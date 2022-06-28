#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ForumPM Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/4/2007 - 10:31:41 AM
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
    <Serializable(), TableInfo("ForumPM")> _
    Public Class ForumPM
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
        Private _subject As String = String.Empty
        Private _message As String = String.Empty
        Private _userFrom As Integer
        Private _isRead As Byte
        Private _isDeletedInbox As Byte
        Private _isDeletedOutBox As Byte
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _userInfo As UserInfo



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


        <ColumnInfo("Subject", "'{0}'")> _
        Public Property Subject() As String
            Get
                Return _subject
            End Get
            Set(ByVal value As String)
                _subject = value
            End Set
        End Property


        <ColumnInfo("Message", "'{0}'")> _
        Public Property Message() As String
            Get
                Return _message
            End Get
            Set(ByVal value As String)
                _message = value
            End Set
        End Property


        <ColumnInfo("UserFrom", "{0}")> _
        Public Property UserFrom() As Integer
            Get
                Return _userFrom
            End Get
            Set(ByVal value As Integer)
                _userFrom = value
            End Set
        End Property


        <ColumnInfo("isRead", "{0}")> _
        Public Property isRead() As Byte
            Get
                Return _isRead
            End Get
            Set(ByVal value As Byte)
                _isRead = value
            End Set
        End Property


        <ColumnInfo("isDeletedInbox", "{0}")> _
        Public Property isDeletedInbox() As Byte
            Get
                Return _isDeletedInbox
            End Get
            Set(ByVal value As Byte)
                _isDeletedInbox = value
            End Set
        End Property


        <ColumnInfo("isDeletedOutBox", "{0}")> _
        Public Property isDeletedOutBox() As Byte
            Get
                Return _isDeletedOutBox
            End Get
            Set(ByVal value As Byte)
                _isDeletedOutBox = value
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


        <ColumnInfo("UserInfoID", "{0}"), _
        RelationInfo("UserInfo", "ID", "ForumPM", "UserInfoID")> _
        Public Property UserInfo() As UserInfo
            Get
                Try
                    If Not isnothing(Me._userInfo) AndAlso (Not Me._userInfo.IsLoaded) Then

                        Me._userInfo = CType(DoLoad(GetType(UserInfo).ToString(), _userInfo.ID), UserInfo)
                        Me._userInfo.MarkLoaded()

                    End If

                    Return Me._userInfo

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As UserInfo)

                Me._userInfo = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._userInfo.MarkLoaded()
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

