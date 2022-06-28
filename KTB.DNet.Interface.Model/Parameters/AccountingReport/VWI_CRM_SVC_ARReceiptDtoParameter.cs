#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SVC_ARReceiptParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2019 10:31:05
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
    public class VWI_CRM_SVC_ARReceiptParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_accountreceivableinvoice { get; set; }

		[AntiXss]
		public string xts_type { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public string xts_reversing { get; set; }

		[AntiXss]
		public string xts_ordernumber { get; set; }

		[AntiXss]
		public decimal xts_invoiceamount { get; set; }

		[AntiXss]
		public string xts_customernumber { get; set; }

		[AntiXss]
		public decimal xts_balance { get; set; }

		[AntiXss]
		public string address1_line1 { get; set; }

		[AntiXss]
		public decimal xts_totalworkamount { get; set; }

		[AntiXss]
		public decimal xts_totalpartsamount { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargeamount { get; set; }

		[AntiXss]
		public string xts_platenumber { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
