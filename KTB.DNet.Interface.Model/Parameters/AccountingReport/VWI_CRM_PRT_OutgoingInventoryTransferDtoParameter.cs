#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_OutgoingInventoryTransferParameterDto  class
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
    public class VWI_CRM_PRT_OutgoingInventoryTransferParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_inventorytransfernumber { get; set; }

		[AntiXss]
		public string xts_product { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public decimal xts_quantity { get; set; }

		[AntiXss]
		public decimal ktb_cogstrx { get; set; }

		[AntiXss]
		public string transactiontypename { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public string fromwarehousename { get; set; }

		[AntiXss]
		public string towarehousename { get; set; }

		[AntiXss]
		public string fromsitename { get; set; }

		[AntiXss]
		public string tositename { get; set; }

		[AntiXss]
		public string tobusinessunitcode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
