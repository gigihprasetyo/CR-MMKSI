#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : POOtherVendorDetailDto  class
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
    public class POOtherVendorDetailDto : DtoBase
    {
        public int ID { get; set; }
        public int POOtherVendorID { get; set; }
        public string Owner { get; set; }
        public string DealerCode { get; set; }
        public Boolean CloseLine { get; set; }
        public string CloseReason { get; set; }
        public Boolean Completed { get; set; }
        public Decimal ConsumptionTax1Amount { get; set; }
        public string ConsumptionTax1 { get; set; }
        public Decimal ConsumptionTax2Amount { get; set; }
        public string ConsumptionTax2 { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public Decimal DiscountAmount { get; set; }
        public Double DiscountPercentage { get; set; }
        public string EventData { get; set; }
        public int FormSource { get; set; }
        public Double BaseQtyOrder { get; set; }
        public Double BaseQtyReceipt { get; set; }
        public Double BaseQtyReturn { get; set; }
        public string InventoryUnit { get; set; }
        public string ProductCrossReference { get; set; }
        public string ProductDescription { get; set; }
        public string Product { get; set; }
        public string ProductSubstitute { get; set; }
        public string ProductVariant { get; set; }
        public Double ProductVolume { get; set; }
        public Double ProductWeight { get; set; }
        public DateTime PromisedDate { get; set; }
        public int PurchaseFor { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string PurchaseRequisitionDetail { get; set; }
        public string PurchaseUnit { get; set; }
        public Double QtyOrder { get; set; }
        public Double QtyReceipt { get; set; }
        public Double QtyReturn { get; set; }
        public Boolean RecallProduct { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime RequiredDate { get; set; }
        public string SalesOrderDetail { get; set; }
        public DateTime ScheduledShippingDate { get; set; }
        public string ServicePartsAndMaterial { get; set; }
        public DateTime ShippingDate { get; set; }
        public string Site { get; set; }
        public string StockNumber { get; set; }
        public Decimal TitleRegistrationFee { get; set; }
        public Decimal TotalAmount { get; set; }
        public Decimal TotalAmountBeforeDiscount { get; set; }
        public Decimal TotalBaseAmount { get; set; }
        public Decimal TotalConsumptionTaxAmount { get; set; }
        public Double TotalVolume { get; set; }
        public Double TotalWeight { get; set; }
        public Decimal TransactionAmount { get; set; }
        public Decimal UnitCost { get; set; }
        public string Warehouse { get; set; }
    }
}

