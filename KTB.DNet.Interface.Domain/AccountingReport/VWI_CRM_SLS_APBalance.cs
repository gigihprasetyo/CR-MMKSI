#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_SLS_APBalance  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/01/2020 14:05:28
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_SLS_APBalance
    {
        public Int64 IDRow { get; set; }

		public string company { get; set; }

		public string businessunitcode { get; set; }

		public string IdentificationType { get; set; }

		public string APVoucherNo { get; set; }

		public DateTime TransactionDate { get; set; }

		public string Vendor { get; set; }

		public string TransactionType { get; set; }

		public DateTime DueDate { get; set; }

		public string VendorInvoiceNumber { get; set; }

		public string APVoucherDetailNo { get; set; }

		public string PaymentTerms { get; set; }

		public decimal GrandTotal { get; set; }

		public decimal Balance { get; set; }

		public decimal PaymentAmount { get; set; }

		public decimal ConsumptionTax1Amount { get; set; }

		public decimal ConsumptionTax2Amount { get; set; }

		public string Description { get; set; }

		public string DocState { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
