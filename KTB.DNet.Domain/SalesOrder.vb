
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesOrder Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/18/2008 - 2:06:55 PM
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
    <Serializable(), TableInfo("SalesOrder")> _
    Public Class SalesOrder
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
        Private _sONumber As String = String.Empty
        Private _sODate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _amount As Decimal
        Private _paymentRef As String = String.Empty
        Private _sOType As String = String.Empty
        Private _cashPayment As Decimal
        Private _TotalVH As Decimal
        Private _TotalPP As Decimal
        Private _TotalIT As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _pOHeader As POHeader

        Private _deliveryOrders As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _dailyPayments As System.Collections.ArrayList = New System.Collections.ArrayList

        Private _logisticDCHeader As LogisticDCHeader

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


        <ColumnInfo("SONumber", "'{0}'")> _
        Public Property SONumber() As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
            End Set
        End Property


        <ColumnInfo("SODate", "'{0:yyyy/MM/dd}'")> _
        Public Property SODate() As DateTime
            Get
                Return _sODate
            End Get
            Set(ByVal value As DateTime)
                _sODate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Amount", "#,##0")> _
        Public Property Amount() As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property


        <ColumnInfo("PaymentRef", "'{0}'")> _
        Public Property PaymentRef() As String
            Get
                Return _paymentRef
            End Get
            Set(ByVal value As String)
                _paymentRef = value
            End Set
        End Property


        <ColumnInfo("SOType", "'{0}'")> _
        Public Property SOType() As String
            Get
                Return _sOType
            End Get
            Set(ByVal value As String)
                _sOType = value
            End Set
        End Property


        <ColumnInfo("CashPayment", "{0}")> _
        Public Property CashPayment() As Decimal
            Get
                Return _cashPayment
            End Get
            Set(ByVal value As Decimal)
                _cashPayment = value
            End Set
        End Property


        <ColumnInfo("TotalVH", "{0}")> _
        Public Property TotalVH() As Decimal
            Get
                Return _TotalVH
            End Get
            Set(ByVal value As Decimal)
                _TotalVH = value
            End Set
        End Property



        <ColumnInfo("TotalPP", "{0}")> _
        Public Property TotalPP() As Decimal
            Get
                Return _TotalPP
            End Get
            Set(ByVal value As Decimal)
                _TotalPP = value
            End Set
        End Property

        <ColumnInfo("TotalIT", "{0}")> _
        Public Property TotalIT() As Decimal
            Get
                Return _TotalIT
            End Get
            Set(ByVal value As Decimal)
                _TotalIT = value
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


        <ColumnInfo("POHeaderID", "{0}"), _
        RelationInfo("POHeader", "ID", "SalesOrder", "POHeaderID")> _
        Public Property POHeader() As POHeader
            Get
                Try
                    If Not IsNothing(Me._pOHeader) AndAlso (Not Me._pOHeader.IsLoaded) Then

                        Me._pOHeader = CType(DoLoad(GetType(POHeader).ToString(), _pOHeader.ID), POHeader)
                        Me._pOHeader.MarkLoaded()

                    End If

                    Return Me._pOHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As POHeader)

                Me._pOHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pOHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("LogisticDCHeaderID", "{0}"), _
       RelationInfo("LogisticDCHeader", "ID", "SalesOrder", "LogisticDCHeaderID")> _
        Public Property LogisticDCHeader() As LogisticDCHeader
            Get
                Try
                    If Not IsNothing(Me._logisticDCHeader) AndAlso (Not Me._logisticDCHeader.IsLoaded) Then

                        Me._logisticDCHeader = CType(DoLoad(GetType(LogisticDCHeader).ToString(), _logisticDCHeader.ID), LogisticDCHeader)
                        Me._logisticDCHeader.MarkLoaded()

                    End If

                    Return Me._logisticDCHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As LogisticDCHeader)

                Me._logisticDCHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._logisticDCHeader.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("SalesOrder", "ID", "DeliveryOrder", "SalesOrderID")> _
         Public ReadOnly Property DeliveryOrders() As System.Collections.ArrayList
            Get
                Try
                    If (Me._deliveryOrders.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DeliveryOrder), "SalesOrder", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DeliveryOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._deliveryOrders = DoLoadArray(GetType(DeliveryOrder).ToString, criterias)
                    End If

                    Return Me._deliveryOrders

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        <RelationInfo("SalesOrder", "ID", "DailyPayment", "SalesOrderID")> _
         Public ReadOnly Property DailyPayments() As System.Collections.ArrayList
            Get
                Try
                    If (Me._dailyPayments.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DailyPayment), "SalesOrder", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._dailyPayments = DoLoadArray(GetType(DailyPayment).ToString, criterias)
                    End If

                    Return Me._dailyPayments

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
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

