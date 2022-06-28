#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_SparepartSalesParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 29/01/2020 10:52:17
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
    public class VWI_CRM_PRT_SparepartSalesParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public DateTime TransactionDate { get; set; }

		[AntiXss]
		public string Customer { get; set; }

		[AntiXss]
		public string CustomerDescription { get; set; }

		[AntiXss]
		public string CustomerType { get; set; }

		[AntiXss]
		public string SalesPerson { get; set; }

		[AntiXss]
		public string SOorWoNo { get; set; }

		[AntiXss]
		public string InvoiceNo { get; set; }

		[AntiXss]
		public string ProductCode { get; set; }

		[AntiXss]
		public string ProductDescription { get; set; }

		[AntiXss]
		public string Model { get; set; }

		[AntiXss]
		public decimal Quantity { get; set; }

		[AntiXss]
		public decimal CapitalPrice { get; set; }

		[AntiXss]
		public decimal RetailPrice { get; set; }

		[AntiXss]
		public decimal DiscountAmount { get; set; }

		[AntiXss]
		public decimal Total { get; set; }

		[AntiXss]
		public decimal Tax { get; set; }

		[AntiXss]
		public decimal TotalAmount { get; set; }

		[AntiXss]
		public decimal TotalCOGS { get; set; }

		[AntiXss]
		public decimal Laba { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
