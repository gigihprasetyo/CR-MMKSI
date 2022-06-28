#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartSalesOrderDetailDto  class
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
    public class SparePartSalesOrderDetailDto : DtoBase
    {
        public int ID { get; set; }
        public int SparePartSalesOrderID { get; set; }
        public string Owner { get; set; }
        public int Status { get; set; }
        public Decimal AmountBeforeDiscount { get; set; }
        public Decimal BaseAmount { get; set; }
        public string KodeDealer { get; set; }
        public Decimal ConsumptionTax1Amount { get; set; }
        public string ConsumptionTax1 { get; set; }
        public Decimal ConsumptionTax2Amount { get; set; }
        public string ConsumptionTax2 { get; set; }
        public Decimal DiscountAmount { get; set; }
        public Decimal DiscountPercentAge { get; set; }
        public string ProductCrossReference { get; set; }
        public string ProductDescription { get; set; }
        public string Product { get; set; }
        public DateTime PromiseDate { get; set; }
        public double QtyDelivered { get; set; }
        public double QtyOrder { get; set; }
        public DateTime RequestDate { get; set; }
        public string SalesOrderDetailID { get; set; }
        public string SalesOrderNo { get; set; }
        public string SalesUnit { get; set; }
        public string Site { get; set; }
        public Decimal TotalAmount { get; set; }
        public Decimal TotalConsumptionTaxAmount { get; set; }
        public Decimal TransactionAmount { get; set; }
        public Decimal UnitPrice { get; set; }
        public string Warehouse { get; set; }
    }
}

