#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_VehicleStockParameterDto  class
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
    public class VWI_CRM_SLS_VehicleStockParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_stocknumber { get; set; }

		[AntiXss]
		public string xts_warehouse { get; set; }

		[AntiXss]
		public string xts_productexteriorcolor { get; set; }

		[AntiXss]
		public decimal xts_totalvalue { get; set; }

		[AntiXss]
		public string xts_product { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public string ktb_applicationno { get; set; }

		[AntiXss]
		public string xts_keynumber { get; set; }

		[AntiXss]
		public string xts_chassisnumber { get; set; }

		[AntiXss]
		public Guid ktb_vendorid { get; set; }

		[AntiXss]
		public string xts_vendor { get; set; }

		[AntiXss]
		public string iskaroseri { get; set; }

		[AntiXss]
		public decimal xts_purchaseprice { get; set; }

		[AntiXss]
		public decimal ktb_caroseriesamount { get; set; }

		[AntiXss]
		public string description { get; set; }

		[AntiXss]
		public string ktb_bucompany { get; set; }

		[AntiXss]
		public string stockstatus { get; set; }

		[AntiXss]
		public string availabilitystatus { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
