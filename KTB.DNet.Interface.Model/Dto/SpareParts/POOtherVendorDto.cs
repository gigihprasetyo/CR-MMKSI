#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : POOtherVendorDto  class
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
    public class POOtherVendorDto : DtoBase
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string AllocationPeriod { get; set; }
        public Decimal Balance { get; set; }
        public string DealerCode { get; set; }
        public string City { get; set; }
        public string CloseRespon { get; set; }
        public string Country { get; set; }
        public int DeliveryMethod { get; set; }
        public string Description { get; set; }
        public Decimal DownPayment { get; set; }
        public Decimal DownPaymentAmountPaid { get; set; }
        public Boolean DownPaymentIsPaid { get; set; }
        public string EventDate { get; set; }
        public string ExternalDocNo { get; set; }
        public int FormSource { get; set; }
        public Decimal GrandTotal { get; set; }
        public int PaymentGroup { get; set; }
        public string PersonInCharge { get; set; }
        public string PostalCode { get; set; }
        public int Priority { get; set; }
        public string Province { get; set; }
        public string PRPOType { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string SONo { get; set; }
        public string Site { get; set; }
        public int State { get; set; }
        public string StockReferenceNo { get; set; }
        public int Taxable { get; set; }
        public string TermsOfPayment { get; set; }
        public Decimal TotalAmountBeforeDiscount { get; set; }
        public Decimal TotalBaseAmount { get; set; }
        public Decimal TotalConsumptionTaxAmount { get; set; }
        public Decimal TotalDiscountAmount { get; set; }
        public Decimal TotalTitleRegistrationFee { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public string VendorDescription { get; set; }
        public string Vendor { get; set; }
        public string Warehouse { get; set; }
        public string WONo { get; set; }
    }
}

