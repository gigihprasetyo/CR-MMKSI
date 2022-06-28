#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_OutgoingInventoryTransferWarehouseParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/01/2020 16:30:07
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
    public class VWI_CRM_PRT_OutgoingInventoryTransferWarehouseParameterDto : ParameterDtoBase, IValidatableObject
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
		public string WorkOrderNO { get; set; }

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
		public string UOM { get; set; }

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
