#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_DailyActivityMonitoring_VDOParameterDto  class
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
    public class VWI_CRM_SLS_DailyActivityMonitoring_VDOParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_newvehicledeliveryordernumber { get; set; }

		[AntiXss]
		public string xts_newvehiclesalesordernumber { get; set; }

		[AntiXss]
		public string personincharge { get; set; }

		[AntiXss]
		public string salesperson { get; set; }

		[AntiXss]
		public Guid xts_productsegment2id { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
