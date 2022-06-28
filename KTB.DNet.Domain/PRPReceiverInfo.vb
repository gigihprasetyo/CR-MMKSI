#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PRPReceiverInfo Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 1/23/2006 - 11:34:22 AM
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
    <Serializable(), TableInfo("PRPReceiverInfo")> _
    Public Class PRPReceiverInfo
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
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _pRPUserEmail As PRPUserEmail
        Private _pRPSenderInfo As PRPSenderInfo



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




        <ColumnInfo("PRPUserEmailID", "{0}"), _
        RelationInfo("PRPUserEmail", "ID", "PRPReceiverInfo", "PRPUserEmailID")> _
        Public Property PRPUserEmail() As PRPUserEmail
            Get
                Try
                    If Not isnothing(Me._pRPUserEmail) AndAlso (Not Me._pRPUserEmail.IsLoaded) Then

                        Me._pRPUserEmail = CType(DoLoad(GetType(PRPUserEmail).ToString(), _pRPUserEmail.ID), PRPUserEmail)
                        Me._pRPUserEmail.MarkLoaded()

                    End If

                    Return Me._pRPUserEmail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PRPUserEmail)

                Me._pRPUserEmail = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pRPUserEmail.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("PRPSenderInfoID", "{0}"), _
        RelationInfo("PRPSenderInfo", "ID", "PRPReceiverInfo", "PRPSenderInfoID")> _
        Public Property PRPSenderInfo() As PRPSenderInfo
            Get
                Try
                    If Not isnothing(Me._pRPSenderInfo) AndAlso (Not Me._pRPSenderInfo.IsLoaded) Then

                        Me._pRPSenderInfo = CType(DoLoad(GetType(PRPSenderInfo).ToString(), _pRPSenderInfo.ID), PRPSenderInfo)
                        Me._pRPSenderInfo.MarkLoaded()

                    End If

                    Return Me._pRPSenderInfo

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PRPSenderInfo)

                Me._pRPSenderInfo = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pRPSenderInfo.MarkLoaded()
                End If
            End Set
        End Property




#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

