#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_PRT_SparepartSales  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 29/01/2020 10:52:17
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_PRT_SparepartSales
    {
        public Int64 IDRow { get; set; }

		public string company { get; set; }

		public string businessunitcode { get; set; }

		public DateTime TransactionDate { get; set; }

		public string Customer { get; set; }

		public string CustomerDescription { get; set; }

		public string CustomerType { get; set; }

		public string SalesPerson { get; set; }

		public string SOorWoNo { get; set; }

		public string InvoiceNo { get; set; }

		public string ProductCode { get; set; }

		public string ProductDescription { get; set; }

		public string Model { get; set; }
		public string Warehouse { get; set; }

		public decimal Quantity { get; set; }

		public decimal CapitalPrice { get; set; }

		public decimal RetailPrice { get; set; }

		public decimal DiscountAmount { get; set; }

		public decimal Total { get; set; }

		public decimal Tax { get; set; }

		public decimal TotalAmount { get; set; }

		public decimal TotalCOGS { get; set; }

		public decimal Laba { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
