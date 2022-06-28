#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehiclePurchaseDetailDto  class
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
    public class VehiclePurchaseDetailDto : DtoBase
    {
        public int? ID { get; set; }
        public string BUCode { get; set; }
        public string BUName { get; set; }
        public bool CloseLine { get; set; }
        public string CloseLineName { get; set; }
        public string CloseReason { get; set; }
        public bool Completed { get; set; }
        public string CompletedName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductName { get; set; }
        public string ProductVariantName { get; set; }
        public string PODetail { get; set; }
        public string POName { get; set; }
        public string PRDetailName { get; set; }
        public string PurchaseUnitName { get; set; }
        public double QtyOrder { get; set; }
        public double QtyReceipt { get; set; }
        public double QtyReturn { get; set; }
        public bool RecallProduct { get; set; }
        public string RecallProductName { get; set; }
        public string SODetailName { get; set; }
        public DateTime ScheduledShippingDate { get; set; }
        public string ServicePartsAndMaterial { get; set; }
        public DateTime ShippingDate { get; set; }
        public string Site { get; set; }
        public string StockNumberName { get; set; }
    }
}

