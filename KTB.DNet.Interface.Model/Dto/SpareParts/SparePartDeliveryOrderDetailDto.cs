#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDeliveryOrderDetailDto  class
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
    public class SparePartDeliveryOrderDetailDto : DtoBase
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        public Decimal AmountBeforeDiscount { get; set; }
        public Decimal BaseAmount { get; set; }
        public Double BaseQtyDelivered { get; set; }
        public Double BaseQtyOrder { get; set; }
        public string BatchNo { get; set; }
        public string BU { get; set; }
        public Decimal ConsumptionTax1Amount { get; set; }
        public string ConsumptionTax1 { get; set; }
        public string DeliveryOrderDetail { get; set; }
        public string DeliveryOrderNo { get; set; }
        public Decimal DiscountAmount { get; set; }
        public Decimal DiscountBaseAmount { get; set; }
        public Decimal ConsumptionTax2Amount { get; set; }
        public string ConsumptionTax2 { get; set; }
        public Double DiscountPercentage { get; set; }
        public string Location { get; set; }
        public string ProductCrossReference { get; set; }
        public string ProductDescription { get; set; }
        public string Product { get; set; }
        public DateTime PromiseDate { get; set; }
        public Double QtyDelivered { get; set; }
        public Double QtyOrder { get; set; }
        public DateTime RequestDate { get; set; }
        public int RunningNumber { get; set; }
        public string SalesOrderDetail { get; set; }
        public string SalesUnit { get; set; }
        public string Site { get; set; }
        public Decimal TotalAmount { get; set; }
        public Decimal TotalConsumptionTaxAmount { get; set; }
        public Decimal TransactionAmount { get; set; }
        public Decimal UnitPrice { get; set; }
        public string Warehouse { get; set; }
    }
}

