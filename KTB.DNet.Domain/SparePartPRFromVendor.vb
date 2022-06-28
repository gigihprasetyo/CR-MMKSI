
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : SparePartPRFromVendor Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/03/2018 - 15:10:56
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
    <Serializable(), TableInfo("SparePartPRFromVendor")> _
    Public Class SparePartPRFromVendor
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
        Private _pRNumber As String = String.Empty
        Private _pONumber As String = String.Empty
        Private _owner As String = String.Empty
        Private _aPVoucherNumber As String = String.Empty
        Private _assignLandedCost As Boolean
        Private _autoInvoiced As Boolean
        Private _dealerCode As String = String.Empty
        Private _deliveryOrderDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _deliveryOrderNumber As String = String.Empty
        Private _eventData As String = String.Empty
        Private _eventData2 As String = String.Empty
        Private _grandTotal As Decimal
        Private _handling As Short
        Private _loadData As Boolean
        Private _packingSlipDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _packingSlipNumber As String = String.Empty
        Private _pRReferenceRequired As Boolean
        Private _returnPRNumber As String = String.Empty
        Private _state As Short
        Private _totalBaseAmount As Decimal
        Private _totalConsumptionTax1Amount As Decimal
        Private _totalConsumptionTax2Amount As Decimal
        Private _totalConsumptionTaxAmount As Decimal
        Private _totalTitleRegistrationFree As Decimal
        Private _transactionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _transferOrderRequestingNumber As String = String.Empty
        Private _type As Short
        Private _vendorDescription As String = String.Empty
        Private _vendor As String = String.Empty
        Private _vendorInvoiceNumber As String = String.Empty
        Private _wONumber As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _sparePartPRDetailFromVendors As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("PRNumber", "'{0}'")> _
        Public Property PRNumber As String
            Get
                Return _pRNumber
            End Get
            Set(ByVal value As String)
                _pRNumber = value
            End Set
        End Property


        <ColumnInfo("PONumber", "'{0}'")> _
        Public Property PONumber As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
            End Set
        End Property


        <ColumnInfo("Owner", "'{0}'")> _
        Public Property Owner As String
            Get
                Return _owner
            End Get
            Set(ByVal value As String)
                _owner = value
            End Set
        End Property


        <ColumnInfo("APVoucherNumber", "'{0}'")> _
        Public Property APVoucherNumber As String
            Get
                Return _aPVoucherNumber
            End Get
            Set(ByVal value As String)
                _aPVoucherNumber = value
            End Set
        End Property


        <ColumnInfo("AssignLandedCost", "{0}")> _
        Public Property AssignLandedCost As Boolean
            Get
                Return _assignLandedCost
            End Get
            Set(ByVal value As Boolean)
                _assignLandedCost = value
            End Set
        End Property


        <ColumnInfo("AutoInvoiced", "{0}")> _
        Public Property AutoInvoiced As Boolean
            Get
                Return _autoInvoiced
            End Get
            Set(ByVal value As Boolean)
                _autoInvoiced = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("DeliveryOrderDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DeliveryOrderDate As DateTime
            Get
                Return _deliveryOrderDate
            End Get
            Set(ByVal value As DateTime)
                _deliveryOrderDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DeliveryOrderNumber", "'{0}'")> _
        Public Property DeliveryOrderNumber As String
            Get
                Return _deliveryOrderNumber
            End Get
            Set(ByVal value As String)
                _deliveryOrderNumber = value
            End Set
        End Property


        <ColumnInfo("EventData", "'{0}'")> _
        Public Property EventData As String
            Get
                Return _eventData
            End Get
            Set(ByVal value As String)
                _eventData = value
            End Set
        End Property


        <ColumnInfo("EventData2", "'{0}'")> _
        Public Property EventData2 As String
            Get
                Return _eventData2
            End Get
            Set(ByVal value As String)
                _eventData2 = value
            End Set
        End Property


        <ColumnInfo("GrandTotal", "{0}")> _
        Public Property GrandTotal As Decimal
            Get
                Return _grandTotal
            End Get
            Set(ByVal value As Decimal)
                _grandTotal = value
            End Set
        End Property


        <ColumnInfo("Handling", "{0}")> _
        Public Property Handling As Short
            Get
                Return _handling
            End Get
            Set(ByVal value As Short)
                _handling = value
            End Set
        End Property


        <ColumnInfo("LoadData", "{0}")> _
        Public Property LoadData As Boolean
            Get
                Return _loadData
            End Get
            Set(ByVal value As Boolean)
                _loadData = value
            End Set
        End Property


        <ColumnInfo("PackingSlipDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PackingSlipDate As DateTime
            Get
                Return _packingSlipDate
            End Get
            Set(ByVal value As DateTime)
                _packingSlipDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PackingSlipNumber", "'{0}'")> _
        Public Property PackingSlipNumber As String
            Get
                Return _packingSlipNumber
            End Get
            Set(ByVal value As String)
                _packingSlipNumber = value
            End Set
        End Property


        <ColumnInfo("PRReferenceRequired", "{0}")> _
        Public Property PRReferenceRequired As Boolean
            Get
                Return _pRReferenceRequired
            End Get
            Set(ByVal value As Boolean)
                _pRReferenceRequired = value
            End Set
        End Property


        <ColumnInfo("ReturnPRNumber", "'{0}'")> _
        Public Property ReturnPRNumber As String
            Get
                Return _returnPRNumber
            End Get
            Set(ByVal value As String)
                _returnPRNumber = value
            End Set
        End Property


        <ColumnInfo("State", "{0}")> _
        Public Property State As Short
            Get
                Return _state
            End Get
            Set(ByVal value As Short)
                _state = value
            End Set
        End Property


        <ColumnInfo("TotalBaseAmount", "{0}")> _
        Public Property TotalBaseAmount As Decimal
            Get
                Return _totalBaseAmount
            End Get
            Set(ByVal value As Decimal)
                _totalBaseAmount = value
            End Set
        End Property


        <ColumnInfo("TotalConsumptionTax1Amount", "{0}")> _
        Public Property TotalConsumptionTax1Amount As Decimal
            Get
                Return _totalConsumptionTax1Amount
            End Get
            Set(ByVal value As Decimal)
                _totalConsumptionTax1Amount = value
            End Set
        End Property


        <ColumnInfo("TotalConsumptionTax2Amount", "{0}")> _
        Public Property TotalConsumptionTax2Amount As Decimal
            Get
                Return _totalConsumptionTax2Amount
            End Get
            Set(ByVal value As Decimal)
                _totalConsumptionTax2Amount = value
            End Set
        End Property


        <ColumnInfo("TotalConsumptionTaxAmount", "{0}")> _
        Public Property TotalConsumptionTaxAmount As Decimal
            Get
                Return _totalConsumptionTaxAmount
            End Get
            Set(ByVal value As Decimal)
                _totalConsumptionTaxAmount = value
            End Set
        End Property


        <ColumnInfo("TotalTitleRegistrationFree", "{0}")> _
        Public Property TotalTitleRegistrationFree As Decimal
            Get
                Return _totalTitleRegistrationFree
            End Get
            Set(ByVal value As Decimal)
                _totalTitleRegistrationFree = value
            End Set
        End Property


        <ColumnInfo("TransactionDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransactionDate As DateTime
            Get
                Return _transactionDate
            End Get
            Set(ByVal value As DateTime)
                _transactionDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("TransferOrderRequestingNumber", "'{0}'")> _
        Public Property TransferOrderRequestingNumber As String
            Get
                Return _transferOrderRequestingNumber
            End Get
            Set(ByVal value As String)
                _transferOrderRequestingNumber = value
            End Set
        End Property


        <ColumnInfo("Type", "{0}")> _
        Public Property Type As Short
            Get
                Return _type
            End Get
            Set(ByVal value As Short)
                _type = value
            End Set
        End Property


        <ColumnInfo("VendorDescription", "'{0}'")> _
        Public Property VendorDescription As String
            Get
                Return _vendorDescription
            End Get
            Set(ByVal value As String)
                _vendorDescription = value
            End Set
        End Property


        <ColumnInfo("Vendor", "'{0}'")> _
        Public Property Vendor As String
            Get
                Return _vendor
            End Get
            Set(ByVal value As String)
                _vendor = value
            End Set
        End Property


        <ColumnInfo("VendorInvoiceNumber", "'{0}'")> _
        Public Property VendorInvoiceNumber As String
            Get
                Return _vendorInvoiceNumber
            End Get
            Set(ByVal value As String)
                _vendorInvoiceNumber = value
            End Set
        End Property


        <ColumnInfo("WONumber", "'{0}'")> _
        Public Property WONumber As String
            Get
                Return _wONumber
            End Get
            Set(ByVal value As String)
                _wONumber = value
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



        <RelationInfo("SparePartPRFromVendor", "ID", "SparePartPRDetailFromVendor", "SparePartPRID")> _
        Public ReadOnly Property SparePartPRDetailFromVendors As System.Collections.ArrayList
            Get
                Try
                    If (Me._sparePartPRDetailFromVendors.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SparePartPRDetailFromVendor), "SparePartPRFromVendor", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SparePartPRDetailFromVendor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sparePartPRDetailFromVendors = DoLoadArray(GetType(SparePartPRDetailFromVendor).ToString, criterias)
                    End If

                    Return Me._sparePartPRDetailFromVendors

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