
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Invoice Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/22/2008 - 2:39:07 PM
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
    <Serializable(), TableInfo("Invoice")> _
    Public Class Invoice
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
        Private _invoiceNumber As String = String.Empty
        Private _invoiceDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _amount As Decimal
        Private _invoiceType As String = String.Empty
        Private _invoiceRef As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _invoiceKind As Integer



        Private _salesOrder As SalesOrder
        Private _logisticDN As LogisticDN



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

        <ColumnInfo("InvoiceKind", "{0}")> _
        Public Property InvoiceKind() As Integer
            Get
                Return _invoiceKind
            End Get
            Set(ByVal value As Integer)
                _invoiceKind = value
            End Set
        End Property


        <ColumnInfo("InvoiceNumber", "'{0}'")> _
        Public Property InvoiceNumber() As String
            Get
                Return _invoiceNumber
            End Get
            Set(ByVal value As String)
                _invoiceNumber = value
            End Set
        End Property


        <ColumnInfo("InvoiceDate", "'{0:yyyy/MM/dd}'")> _
        Public Property InvoiceDate() As DateTime
            Get
                Return _invoiceDate
            End Get
            Set(ByVal value As DateTime)
                _invoiceDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount() As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property


        <ColumnInfo("InvoiceType", "'{0}'")> _
        Public Property InvoiceType() As String
            Get
                Return _invoiceType
            End Get
            Set(ByVal value As String)
                _invoiceType = value
            End Set
        End Property

        <ColumnInfo("InvoiceRef", "'{0}'")> _
     Public Property InvoiceRef() As String
            Get
                Return _invoiceRef
            End Get
            Set(ByVal value As String)
                _invoiceRef = value
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


        <ColumnInfo("SalesOrderID", "{0}"), _
        RelationInfo("SalesOrder", "ID", "Invoice", "SalesOrderID")> _
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

        <ColumnInfo("LogisticDNID", "{0}"), _
       RelationInfo("LogisticDN", "ID", "Invoice", "LogisticDNID")> _
        Public Property LogisticDN() As LogisticDN
            Get
                Try
                    If Not IsNothing(Me._logisticDN) AndAlso (Not Me._logisticDN.IsLoaded) Then

                        Me._logisticDN = CType(DoLoad(GetType(LogisticDN).ToString(), _logisticDN.ID), LogisticDN)
                        Me._logisticDN.MarkLoaded()

                    End If

                    Return Me._logisticDN

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As LogisticDN)

                Me._logisticDN = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._logisticDN.MarkLoaded()
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

