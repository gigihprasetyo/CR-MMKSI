#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_PRT_PurchaseReturn  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 27/01/2020 1:23:07
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_PRT_PurchaseReturn
    {
        public Int64 IDRow { get; set; }

		public string company { get; set; }

		public string businessunitcode { get; set; }

		public string PurchaseOrderNo { get; set; }

		public string PurchaseReceiptNo { get; set; }

		public DateTime TransactionDate { get; set; }

		public string PurchaseReceiptType { get; set; }

		public string PRPOType { get; set; }

		public string PurchaseReceiptStatus { get; set; }

		public string Vendor { get; set; }

		public string Product { get; set; }

		public string ProductDescription { get; set; }

		public decimal ReceivedQuantity { get; set; }

		public string PurchaseUnit { get; set; }

		public decimal UnitCost { get; set; }

		public decimal DiscountPercentage { get; set; }

		public decimal DiscountAmount { get; set; }

		public string ConsumptionTax1 { get; set; }

		public decimal ConsumptionTax1Amount { get; set; }

		public string ConsumptionTax2 { get; set; }

		public decimal ConsumptionTax2Amount { get; set; }

		public decimal TotalConsumptionTaxAmount { get; set; }

		public decimal TotalBaseAmount { get; set; }

		public decimal TransactionAmount { get; set; }

		public string Site { get; set; }

		public string Warehouse { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
