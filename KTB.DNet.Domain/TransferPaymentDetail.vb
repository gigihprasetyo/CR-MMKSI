
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TransferPaymentDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 28/07/2016 - 14:22:18
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
    <Serializable(), TableInfo("TransferPaymentDetail")> _
    Public Class TransferPaymentDetail
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
        Private _amount As Decimal
        Private _transferPaymentNewID As Integer
        Private _isCanceled As Short
        Private _ActualAmount As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'Private _transferPaymentID As Integer
        'Private _salesOrderID As Integer

        Private _transferPayment As TransferPayment
        Private _salesOrder As SalesOrder

        Private _debitNumber As String = String.Empty
        Private _factoring As String = String.Empty

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


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property


        <ColumnInfo("TransferPaymentNewID", "{0}")> _
        Public Property TransferPaymentNewID As Integer ' SALAH NAMA KOLOM, FIELD INI DIGUNAKAN UTK LAST(ACCELERATED) TRANSFERPAYMENTDETAIL.ID 
            Get
                Return _transferPaymentNewID
            End Get
            Set(ByVal value As Integer)
                _transferPaymentNewID = value
            End Set
        End Property


        <ColumnInfo("IsCanceled", "{0}")> _
        Public Property IsCanceled As Short
            Get
                Return _isCanceled
            End Get
            Set(ByVal value As Short)
                _isCanceled = value
            End Set
        End Property


        <ColumnInfo("ActualAmount", "{0}")> _
        Public Property ActualAmount As Decimal
            Get
                Return _ActualAmount
            End Get
            Set(ByVal value As Decimal)
                _ActualAmount = value
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


        '    <ColumnInfo("TransferPaymentID", "{0}")> _
        '    Public Property TransferPaymentID As Integer

        '        Get
        'return _transferPaymentID}
        '        End Get
        '        Set(ByVal value As Integer)
        '            _transferPaymentID = value
        '        End Set
        '    End Property


        <ColumnInfo("TransferPaymentID", "{0}"), _
        RelationInfo("TransferPayment", "ID", "TransferPaymentDetail", "TransferPaymentID")> _
        Public Property TransferPayment() As TransferPayment
            Get
                Try
                    If Not IsNothing(Me._transferPayment) AndAlso (Not Me._transferPayment.IsLoaded) Then

                        Me._transferPayment = CType(DoLoad(GetType(TransferPayment).ToString(), _transferPayment.ID), TransferPayment)
                        Me._transferPayment.MarkLoaded()

                    End If

                    Return Me._transferPayment

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TransferPayment)

                Me._transferPayment = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._transferPayment.MarkLoaded()
                End If
            End Set
        End Property




        '    <ColumnInfo("SalesOrderID", "{0}")> _
        '    Public Property SalesOrderID As Integer

        '        Get
        'return _salesOrderID}
        '        End Get
        '        Set(ByVal value As Integer)
        '            _salesOrderID = value
        '        End Set
        '    End Property


        <ColumnInfo("SalesOrderID", "{0}"), _
        RelationInfo("SalesOrder", "ID", "TransferPaymentDetail", "SalesOrderID")> _
        Public Property SalesOrder() As SalesOrder
            Get
                Try
                    If Not IsNothing(Me._salesOrder) AndAlso (Not Me._salesOrder.IsLoaded) Then

                        Me._salesOrder = CType(DoLoad(GetType(SalesOrder).ToString(), _salesOrder.ID), SalesOrder)
                        Me._salesOrder.MarkLoaded()

                    End If

                    Return Me._salesOrder

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesOrder)

                Me._salesOrder = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesOrder.MarkLoaded()
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
        Public ReadOnly Property LastTransferPaymentDetail As TransferPaymentDetail
            Get
                Dim oTPD As TransferPaymentDetail = DoLoad(GetType(TransferPaymentDetail).ToString(), Me.TransferPaymentNewID)

                If IsNothing(oTPD) OrElse oTPD.ID < 1 Then
                    oTPD = New TransferPaymentDetail()
                End If

                Return oTPD
            End Get
        End Property
        Public ReadOnly Property LastTransferPayment As TransferPayment
            Get
                Dim oTPD As TransferPaymentDetail = Me.LastTransferPaymentDetail

                If IsNothing(oTPD) OrElse oTPD.ID < 1 Then
                    Return New TransferPayment
                Else
                    Return oTPD.TransferPayment
                End If
                'Dim cTPD As New CriteriaComposite(New Criteria(GetType(TransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'cTPD.opAnd(New Criteria(GetType(TransferPaymentDetail), "TransferPaymentNewID", MatchType.Exact, Me.TransferPayment.ID))

                'Dim aTPDs As ArrayList = DoLoadArray(GetType(TransferPaymentDetail).ToString(), cTPD)

                'If aTPDs.Count > 0 Then
                '    Return aTPDs(0)
                'End If

                Return New TransferPayment
            End Get
        End Property

        Public Property DebitNumber As String
            Get
                Return _debitNumber
            End Get
            Set(ByVal value As String)
                _debitNumber = value
            End Set
        End Property

        Public Property Factoring As String
            Get
                Return _factoring
            End Get
            Set(ByVal value As String)
                _factoring = value
            End Set
        End Property

#End Region

    End Class
End Namespace

