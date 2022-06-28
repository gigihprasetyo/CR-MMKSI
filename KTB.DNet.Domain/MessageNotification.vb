#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MessageNotification Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/16/2021 - 11:08:25 AM
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
    <Serializable(), TableInfo("MessageNotification")> _
    Public Class MessageNotification
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
        Private _phoneNumber As String = String.Empty
        Private _message As String = String.Empty
        Private _sendDateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _ProcessTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _transactionID As String = String.Empty
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _messageTemplate As MessageTemplate



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


        <ColumnInfo("MessageTemplateID", "{0}"), _
       RelationInfo("MessageTemplate", "ID", "MessageNotification", "MessageTemplateID")> _
        Public Property MessageTemplate() As MessageTemplate
            Get
                Try
                    If Not IsNothing(Me._messageTemplate) AndAlso (Not Me._messageTemplate.IsLoaded) Then

                        Me._messageTemplate = CType(DoLoad(GetType(MessageTemplate).ToString(), _messageTemplate.ID), MessageTemplate)
                        Me._messageTemplate.MarkLoaded()

                    End If

                    Return Me._messageTemplate

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MessageTemplate)

                Me._messageTemplate = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._messageTemplate.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("PhoneNumber", "'{0}'")> _
        Public Property PhoneNumber As String
            Get
                Return _phoneNumber
            End Get
            Set(ByVal value As String)
                _phoneNumber = value
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


        <ColumnInfo("ProcessTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ProcessTime As DateTime
            Get
                Return _ProcessTime
            End Get
            Set(ByVal value As DateTime)
                _ProcessTime = value
            End Set
        End Property

        <ColumnInfo("SendDateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property SendDateTime As DateTime
            Get
                Return _sendDateTime
            End Get
            Set(ByVal value As DateTime)
                _sendDateTime = value
            End Set
        End Property


        <ColumnInfo("TransactionID", "'{0}'")> _
        Public Property TransactionID As String
            Get
                Return _transactionID
            End Get
            Set(ByVal value As String)
                _transactionID = value
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
