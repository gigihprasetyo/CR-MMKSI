#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_APBalanceParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/01/2020 14:05:28
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
    public class VWI_CRM_SLS_APBalanceParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string IdentificationType { get; set; }

		[AntiXss]
		public string APVoucherNo { get; set; }

		[AntiXss]
		public DateTime TransactionDate { get; set; }

		[AntiXss]
		public string Vendor { get; set; }

		[AntiXss]
		public string TransactionType { get; set; }

		[AntiXss]
		public DateTime DueDate { get; set; }

		[AntiXss]
		public string VendorInvoiceNumber { get; set; }

		[AntiXss]
		public string APVoucherDetailNo { get; set; }

		[AntiXss]
		public string PaymentTerms { get; set; }

		[AntiXss]
		public decimal GrandTotal { get; set; }

		[AntiXss]
		public decimal Balance { get; set; }

		[AntiXss]
		public decimal PaymentAmount { get; set; }

		[AntiXss]
		public decimal ConsumptionTax1Amount { get; set; }

		[AntiXss]
		public decimal ConsumptionTax2Amount { get; set; }

		[AntiXss]
		public string Description { get; set; }

		[AntiXss]
		public string DocState { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
