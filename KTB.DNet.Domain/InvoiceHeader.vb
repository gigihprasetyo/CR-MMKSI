
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : InvoiceHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 11/27/2008 - 4:40:56 PM
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
    <Serializable(), TableInfo("InvoiceHeader")> _
    Public Class InvoiceHeader
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
        Private _cancelled As String = String.Empty
        Private _invoiceType As String = String.Empty
        Private _paymentRef As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _pOHeader As POHeader

        Private _invoiceDetails As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Cancelled", "'{0}'")> _
        Public Property Cancelled() As String
            Get
                Return _cancelled
            End Get
            Set(ByVal value As String)
                _cancelled = value
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


        <ColumnInfo("PaymentRef", "'{0}'")> _
        Public Property PaymentRef() As String
            Get
                Return _paymentRef
            End Get
            Set(ByVal value As String)
                _paymentRef = value
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
        RelationInfo("POHeader", "ID", "InvoiceHeader", "POHeaderID")> _
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


        <RelationInfo("InvoiceHeader", "ID", "InvoiceDetail", "InvoiceHeaderID")> _
        Public ReadOnly Property InvoiceDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._invoiceDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(InvoiceDetail), "InvoiceHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(InvoiceDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._invoiceDetails = DoLoadArray(GetType(InvoiceDetail).ToString, criterias)
                    End If

                    Return Me._invoiceDetails

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

