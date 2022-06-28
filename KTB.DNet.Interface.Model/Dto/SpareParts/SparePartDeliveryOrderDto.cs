#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDeliveryOrderDto  class
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
    public class SparePartDeliveryOrderDto : DtoBase
    {
        public int? ID { get; set; }
        public string Owner { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string BusinessPhone { get; set; }
        public string BU { get; set; }
        public DateTime CancellationDate { get; set; }
        public string City { get; set; }
        public string CustomerContacts { get; set; }
        public string Customer { get; set; }
        public string CustomerNo { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryOrderNo { get; set; }
        public int DeliveryType { get; set; }
        public string ExternalReferenceNo { get; set; }
        public Decimal GrandTotal { get; set; }
        public int Status { get; set; }
        public string MethodofPayment { get; set; }
        public string OrderType { get; set; }
        public string ReferenceNo { get; set; }
        public string Salesperson { get; set; }
        public int State { get; set; }
        public string TermofPayment { get; set; }
        public Decimal TotalAmountBeforeDiscount { get; set; }
        public Decimal TotalBaseAmount { get; set; }
        public Decimal TotalDiscountAmount { get; set; }
        public Decimal TotalMiscChargeBaseAmount { get; set; }
        public Decimal TotalMiscChargeConsumptionTaxAmount { get; set; }
        public Decimal TotalReceipt { get; set; }
        public Decimal TotalConsumptionTaxAmount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}

