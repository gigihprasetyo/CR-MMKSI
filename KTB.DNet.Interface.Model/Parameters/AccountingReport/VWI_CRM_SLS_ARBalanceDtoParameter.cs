#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_ARBalanceParameterDto  class
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
    public class VWI_CRM_SLS_ARBalanceParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string InvoiceNumber { get; set; }

		[AntiXss]
		public DateTime TransactionDate { get; set; }

		[AntiXss]
		public DateTime DueDate { get; set; }

		[AntiXss]
		public string DocState { get; set; }

		[AntiXss]
		public string DocType { get; set; }

		[AntiXss]
		public string SourceType { get; set; }

		[AntiXss]
		public string OrderNo { get; set; }

		[AntiXss]
		public string DeliveryOrderNo { get; set; }

		[AntiXss]
		public string CustomerNo { get; set; }

		[AntiXss]
		public string Customer { get; set; }

		[AntiXss]
		public decimal InvoiceAmount { get; set; }

		[AntiXss]
		public decimal Balance { get; set; }

		[AntiXss]
		public string FinancingCompanyNo { get; set; }

		[AntiXss]
		public string FinancingCompany { get; set; }

		[AntiXss]
		public decimal FinancingCompanyInvoiceAmount { get; set; }

		[AntiXss]
		public decimal FinancingCompanyBalance { get; set; }

		[AntiXss]
		public string Reversing { get; set; }

		[AntiXss]
		public string SPKNo { get; set; }

		[AntiXss]
		public string ChassisNo { get; set; }

		[AntiXss]
		public string EngineNumber { get; set; }

		[AntiXss]
		public string ColorDescription { get; set; }

		[AntiXss]
		public string Product { get; set; }

		[AntiXss]
		public string ProductionYear { get; set; }

		[AntiXss]
		public string ProductDescription { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
