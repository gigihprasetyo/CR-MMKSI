
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : LogisticDN Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 9/12/2017 - 2:33:30 PM
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
    <Serializable(), TableInfo("LogisticDN")> _
    Public Class LogisticDN
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
        Private _debitMemoNo As String = String.Empty
        Private _billingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _documentType As String = String.Empty
        Private _totalAmount As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _salesOrder As SalesOrder
        Private _logisticDCHeader As LogisticDCHeader
        Private _logisticFee As LogisticFee

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


        <ColumnInfo("DebitMemoNo", "'{0}'")> _
        Public Property DebitMemoNo As String
            Get
                Return _debitMemoNo
            End Get
            Set(ByVal value As String)
                _debitMemoNo = value
            End Set
        End Property


        <ColumnInfo("BillingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property BillingDate As DateTime
            Get
                Return _billingDate
            End Get
            Set(ByVal value As DateTime)
                _billingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DocumentType", "'{0}'")> _
        Public Property DocumentType As String
            Get
                Return _documentType
            End Get
            Set(ByVal value As String)
                _documentType = value
            End Set
        End Property


        <ColumnInfo("TotalAmount", "{0}")> _
        Public Property TotalAmount As Decimal
            Get
                Return _totalAmount
            End Get
            Set(ByVal value As Decimal)
                _totalAmount = value
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

        <ColumnInfo("SalesOrderID", "{0}"), _
        RelationInfo("SalesOrder", "ID", "LogisticDN", "SalesOrderID")> _
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

        <ColumnInfo("LogisticDCHeaderID", "{0}"), _
       RelationInfo("LogisticDCHeader", "ID", "LogisticDN", "LogisticDCHeaderID")> _
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

        Public Property LogisticFee() As LogisticFee
            Get
                Try
                    If IsNothing(Me._logisticFee) Then
                        Dim cLF As New CriteriaComposite(New Criteria(GetType(LogisticFee), "LogisticDN.ID", MatchType.Exact, Me.ID))
                        Dim aLFs As ArrayList

                        cLF.opAnd(New Criteria(GetType(LogisticFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        aLFs = DoLoadArray(GetType(LogisticFee).ToString, cLF)
                        If (aLFs.Count > 0) Then
                            _logisticFee = CType(aLFs(0), LogisticFee)
                        End If
                    End If

                    Return Me._logisticFee

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As LogisticFee)
                Me._logisticFee = value
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

