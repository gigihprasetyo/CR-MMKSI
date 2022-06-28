#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartSalesOrderDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.Model
{
    public class SparePartSalesOrderDto : DtoBase
    {
        public int ID { get; set; }
        public int SalesChannel { get; set; }
        public string Owner { get; set; }
        public int Status { get; set; }
        public string DealerCode { get; set; }
        public string Customer { get; set; }
        public string CustomerNo { get; set; }
        public Decimal DownPaymentAmount { get; set; }
        public Decimal DownPaymentAmountReceived { get; set; }
        public Boolean DownPaymentIsPaid { get; set; }
        public string ExternalReferenceNo { get; set; }
        public Decimal GrandTotal { get; set; }
        public int Handling { get; set; }
        public string MethodOfPayment { get; set; }
        public string OrderType { get; set; }
        public string SalesOrderNo { get; set; }
        public string SalesPerson { get; set; }
        public string ShipmentType { get; set; }
        public string State { get; set; }
        public string TermOfPayment { get; set; }
        public Decimal TotalAmountBeforeDiscount { get; set; }
        public Decimal TotalBaseAmount { get; set; }
        public Decimal TotalConsumptionTaxAmount { get; set; }
        public Decimal TotalDiscountAmount { get; set; }
        public Decimal TotalReceipt { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}

