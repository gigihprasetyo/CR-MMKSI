#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : StatusChangeHistorySendEmailFlag Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 16/11/2005 - 16:27:59
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
    <Serializable(), TableInfo("StatusChangeHistorySendEmailFlag")> _
    Public Class StatusChangeHistorySendEmailFlag
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Integer)
            _id = id
        End Sub

#End Region

#Region "Private Variables"

        Private _id As Integer
        Private _isSendEmail As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _statusChangeHistory As StatusChangeHistory

#End Region

#Region "Public Properties"

        <ColumnInfo("id", "{0}")> _
        Public Property id() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property


        <ColumnInfo("IsSendEmail", "{0}")> _
        Public Property IsSendEmail() As Short
            Get
                Return _isSendEmail
            End Get
            Set(ByVal value As Short)
                _isSendEmail = value
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


        <ColumnInfo("StatusChangeHistoryID", "{0}"), _
        RelationInfo("StatusChangeHistory", "ID", "StatusChangeHistorySendEmailFlag", "StatusChangeHistoryID")> _
        Public Property StatusChangeHistory As StatusChangeHistory
            Get
                Try
                    If Not IsNothing(Me._statusChangeHistory) AndAlso (Not Me._statusChangeHistory.IsLoaded) Then
                        If _statusChangeHistory.id > 0 Then
                            Me._statusChangeHistory = CType(DoLoad(GetType(StatusChangeHistory).ToString(), _statusChangeHistory.id), StatusChangeHistory)
                            Me._statusChangeHistory.MarkLoaded()
                        End If
                    End If

                    Return Me._statusChangeHistory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As StatusChangeHistory)

                Me._statusChangeHistory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._statusChangeHistory.MarkLoaded()
                End If
            End Set
        End Property





#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

