
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TransferCeilingDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 28/07/2016 - 11:04:38
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
    <Serializable(), TableInfo("TransferCeilingDetail")> _
    Public Class TransferCeilingDetail
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
        'Private _salesOrderID As Integer
        'Private _transferPaymentID As Integer
        Private _amount As Decimal
        Private _isIncome As Short
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'Private _transferCeilingID As Integer
        Private _transferCeiling As TransferCeiling
        Private _SalesOrder As SalesOrder
        Private _TransferPayment As TransferPayment



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


        '<ColumnInfo("SalesOrderID", "{0}")> _
        'Public Property SalesOrderID As Integer
        '    Get
        '        Return _salesOrderID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _salesOrderID = value
        '    End Set
        'End Property


        '<ColumnInfo("TransferPaymentID", "{0}")> _
        'Public Property TransferPaymentID As Integer
        '    Get
        '        Return _transferPaymentID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _transferPaymentID = value
        '    End Set
        'End Property


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property


        <ColumnInfo("IsIncome", "{0}")> _
        Public Property IsIncome As Short
            Get
                Return _isIncome
            End Get
            Set(ByVal value As Short)
                _isIncome = value
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


        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime As DateTime
            Get
                Return _lastUpdatedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedTime = value
            End Set
        End Property


        '    <ColumnInfo("TransferCeilingID", "{0}")> _
        '    Public Property TransferCeilingID As Integer

        '        Get
        'return _transferCeilingID}
        '        End Get
        '        Set(ByVal value As Integer)
        '            _transferCeilingID = value
        '        End Set
        '    End Property


        <ColumnInfo("TransferCeilingID", "{0}"), _
        RelationInfo("TransferCeiling", "ID", "TransferCeilingDetail", "TransferCeilingID")> _
        Public Property TransferCeiling() As TransferCeiling
            Get
                Try
                    If Not IsNothing(Me._TransferCeiling) AndAlso (Not Me._TransferCeiling.IsLoaded) Then

                        Me._TransferCeiling = CType(DoLoad(GetType(TransferCeiling).ToString(), _TransferCeiling.ID), TransferCeiling)
                        Me._TransferCeiling.MarkLoaded()

                    End If

                    Return Me._TransferCeiling

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TransferCeiling)

                Me._TransferCeiling = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._TransferCeiling.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("SalesOrderID", "{0}"), _
        RelationInfo("SalesOrder", "ID", "TransferCeilingDetail", "SalesOrderID")> _
        Public Property SalesOrder() As SalesOrder
            Get
                Try
                    If Not IsNothing(Me._SalesOrder) AndAlso (Not Me._SalesOrder.IsLoaded) Then

                        Me._SalesOrder = CType(DoLoad(GetType(SalesOrder).ToString(), _SalesOrder.ID), SalesOrder)
                        Me._SalesOrder.MarkLoaded()

                    End If

                    Return Me._SalesOrder

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesOrder)

                Me._SalesOrder = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._SalesOrder.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("TransferPaymentID", "{0}"), _
        RelationInfo("TransferPayment", "ID", "TransferCeilingDetail", "TransferPaymentID")> _
        Public Property TransferPayment() As TransferPayment
            Get
                Try
                    If Not IsNothing(Me._TransferPayment) AndAlso (Not Me._TransferPayment.IsLoaded) Then

                        Me._TransferPayment = CType(DoLoad(GetType(TransferPayment).ToString(), _TransferPayment.ID), TransferPayment)
                        Me._TransferPayment.MarkLoaded()

                    End If

                    Return Me._TransferPayment

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TransferPayment)

                Me._TransferPayment = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._TransferPayment.MarkLoaded()
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

