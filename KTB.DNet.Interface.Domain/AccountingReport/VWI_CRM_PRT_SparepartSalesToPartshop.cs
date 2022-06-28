#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_PRT_SparepartSalesToPartshop  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/01/2020 9:18:42
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_PRT_SparepartSalesToPartshop
    {
        public Int64 IDRow { get; set; }

		public string company { get; set; }

		public string businessunitcode { get; set; }

		public string Site { get; set; }

		public string SalesOrderNo { get; set; }

		public string DeliveryOrderNo { get; set; }

		public DateTime TransactionDate { get; set; }

		public string CustomerNo { get; set; }

		public string Customer { get; set; }

		public string Product { get; set; }

		public string ProductDescription { get; set; }

		public string SalesUnit { get; set; }

		public string TermOfPayment { get; set; }

		public decimal QuantityDelivered { get; set; }

		public decimal QuantityReturned { get; set; }

		public decimal UnitPrice { get; set; }

		public decimal DiscountPercentage { get; set; }

		public decimal DiscountAmount { get; set; }

		public decimal TotalConsumptionTaxAmount { get; set; }

		public string ConsumptionTax1 { get; set; }

		public decimal ConsumptionTax1Amount { get; set; }

		public decimal TotalAmount { get; set; }

		public decimal COGSTrx { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
