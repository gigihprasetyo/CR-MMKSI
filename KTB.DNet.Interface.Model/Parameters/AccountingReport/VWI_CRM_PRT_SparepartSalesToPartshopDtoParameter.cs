#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_SparepartSalesToPartshopParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/01/2020 9:18:42
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
    public class VWI_CRM_PRT_SparepartSalesToPartshopParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string Site { get; set; }

		[AntiXss]
		public string SalesOrderNo { get; set; }

		[AntiXss]
		public string DeliveryOrderNo { get; set; }

		[AntiXss]
		public DateTime TransactionDate { get; set; }

		[AntiXss]
		public string CustomerNo { get; set; }

		[AntiXss]
		public string Customer { get; set; }

		[AntiXss]
		public string Product { get; set; }

		[AntiXss]
		public string ProductDescription { get; set; }

		[AntiXss]
		public string SalesUnit { get; set; }

		[AntiXss]
		public string TermOfPayment { get; set; }

		[AntiXss]
		public decimal QuantityDelivered { get; set; }

		[AntiXss]
		public decimal QuantityReturned { get; set; }

		[AntiXss]
		public decimal UnitPrice { get; set; }

		[AntiXss]
		public decimal DiscountPercentage { get; set; }

		[AntiXss]
		public decimal DiscountAmount { get; set; }

		[AntiXss]
		public decimal TotalConsumptionTaxAmount { get; set; }

		[AntiXss]
		public string ConsumptionTax1 { get; set; }

		[AntiXss]
		public decimal ConsumptionTax1Amount { get; set; }

		[AntiXss]
		public decimal TotalAmount { get; set; }

		[AntiXss]
		public decimal COGSTrx { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
