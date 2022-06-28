#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_PurchaseParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 27/01/2020 1:23:07
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_PRT_PurchaseParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string PurchaseOrderNo { get; set; }

		[AntiXss]
		public string PurchaseReceiptNo { get; set; }

		[AntiXss]
		public DateTime PurchaseReceiptDate { get; set; }

		[AntiXss]
		public string VendorInvoiceNumber { get; set; }

		[AntiXss]
		public string PurchaseReceiptType { get; set; }

		[AntiXss]
		public string PRPOType { get; set; }

		[AntiXss]
		public string PurchaseReceiptStatus { get; set; }

		[AntiXss]
		public string Vendor { get; set; }

		[AntiXss]
		public string Product { get; set; }

		[AntiXss]
		public string ProductDescription { get; set; }

		[AntiXss]
		public decimal ReceivedQuantity { get; set; }

		[AntiXss]
		public string PurchaseUnit { get; set; }

		[AntiXss]
		public decimal UnitCost { get; set; }

		[AntiXss]
		public decimal DiscountPercentage { get; set; }

		[AntiXss]
		public decimal DiscountAmount { get; set; }

		[AntiXss]
		public string ConsumptionTax1 { get; set; }

		[AntiXss]
		public decimal ConsumptionTax1Amount { get; set; }

		[AntiXss]
		public string ConsumptionTax2 { get; set; }

		[AntiXss]
		public decimal ConsumptionTax2Amount { get; set; }

		[AntiXss]
		public decimal TotalConsumptionTaxAmount { get; set; }

		[AntiXss]
		public decimal TotalBaseAmount { get; set; }

		[AntiXss]
		public decimal TransactionAmount { get; set; }

		[AntiXss]
		public string Site { get; set; }

		[AntiXss]
		public string Warehouse { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
