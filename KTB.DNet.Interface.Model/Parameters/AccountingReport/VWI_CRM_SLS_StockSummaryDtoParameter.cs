#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_StockSummaryParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 21/01/2020 14:18:45
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
    public class VWI_CRM_SLS_StockSummaryParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_stocknumber { get; set; }

		[AntiXss]
		public string xts_product { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public string xts_productinteriorcolor { get; set; }

		[AntiXss]
		public string xts_productexteriorcolor { get; set; }

		[AntiXss]
		public DateTime xts_scheduledshipmentdate { get; set; }

		[AntiXss]
		public string xts_referencenumber { get; set; }

		[AntiXss]
		public DateTime xts_receivingdate { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
