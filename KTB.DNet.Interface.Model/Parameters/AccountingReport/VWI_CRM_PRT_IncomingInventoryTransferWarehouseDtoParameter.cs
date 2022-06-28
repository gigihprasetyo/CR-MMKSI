#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_IncomingInventoryTransferWarehouseParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 27/01/2020 17:07:57
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
    public class VWI_CRM_PRT_IncomingInventoryTransferWarehouseParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string InventoryTransferNo { get; set; }

		[AntiXss]
		public DateTime TransactionDate { get; set; }

		[AntiXss]
		public string TransactionType { get; set; }

		[AntiXss]
		public string WorkOrderNo { get; set; }

		[AntiXss]
		public string FromSite { get; set; }

		[AntiXss]
		public string FromWarehouse { get; set; }

		[AntiXss]
		public string ToWarehouse { get; set; }

		[AntiXss]
		public string Product { get; set; }

		[AntiXss]
		public string ProductDescription { get; set; }

		[AntiXss]
		public decimal Quantity { get; set; }

		[AntiXss]
		public string Unit { get; set; }

		[AntiXss]
		public decimal COGSTrx { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

		[AntiXss]
		public Guid xts_inventorytransferdetailid { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
