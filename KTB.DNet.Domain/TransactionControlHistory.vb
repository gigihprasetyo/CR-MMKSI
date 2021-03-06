
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TransactionControlHistory Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/9/2006 - 9:18:41 AM
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
    <Serializable(), TableInfo("TransactionControlHistory")> _
    Public Class TransactionControlHistory
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
        Private _statusFrom As Integer
        Private _statusTo As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _transactionControl As TransactionControl



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


        <ColumnInfo("StatusFrom", "{0}")> _
        Public Property StatusFrom() As Integer
            Get
                Return _statusFrom
            End Get
            Set(ByVal value As Integer)
                _statusFrom = value
            End Set
        End Property


        <ColumnInfo("StatusTo", "{0}")> _
        Public Property StatusTo() As Integer
            Get
                Return _statusTo
            End Get
            Set(ByVal value As Integer)
                _statusTo = value
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


        <ColumnInfo("TransactionControlID", "{0}"), _
        RelationInfo("TransactionControl", "ID", "TransactionControlHistory", "TransactionControlID")> _
        Public Property TransactionControl() As TransactionControl
            Get
                Try
                    If Not isnothing(Me._transactionControl) AndAlso (Not Me._transactionControl.IsLoaded) Then

                        Me._transactionControl = CType(DoLoad(GetType(TransactionControl).ToString(), _transactionControl.ID), TransactionControl)
                        Me._transactionControl.MarkLoaded()

                    End If

                    Return Me._transactionControl

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TransactionControl)

                Me._transactionControl = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._transactionControl.MarkLoaded()
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

