#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_SparepartPurchaseParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2019 10:31:04
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
    public class VWI_CRM_PRT_SparepartPurchaseParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_purchaseordernumber { get; set; }

		[AntiXss]
		public string xts_accountpayablevouchernumber { get; set; }

		[AntiXss]
		public DateTime TransactionDate { get; set; }

		[AntiXss]
		public DateTime PurchaseReceiptDate { get; set; }

		[AntiXss]
		public string xts_packingslipnumber { get; set; }

		[AntiXss]
		public string xts_vendor { get; set; }

		[AntiXss]
		public string xts_product { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public string ktb_modelcode { get; set; }

		[AntiXss]
		public string xts_uom { get; set; }

		[AntiXss]
		public decimal xts_receivedquantity { get; set; }

		[AntiXss]
		public decimal xts_unitcost { get; set; }

		[AntiXss]
		public decimal xts_discountamount { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount { get; set; }

		[AntiXss]
		public decimal xts_totalamount { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
