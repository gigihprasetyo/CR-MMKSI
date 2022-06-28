#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_PRT_FlowReportPartParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/02/2020 9:04:16
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
    public class VWI_AX_PRT_FlowReportPartParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string Company { get; set; }

		[AntiXss]
		public string Product { get; set; }

		[AntiXss]
		public string ProductDescription { get; set; }

		[AntiXss]
		public string Site { get; set; }

		[AntiXss]
		public string Warehouse { get; set; }

		[AntiXss]
		public string Period { get; set; }

		[AntiXss]
		public string StockUOM { get; set; }

		[AntiXss]
		public string TransactionDateFrom { get; set; }

		[AntiXss]
		public string TransactionDateTo { get; set; }

		[AntiXss]
		public decimal BeginningQty { get; set; }

		[AntiXss]
		public decimal BeginningAmount { get; set; }

		[AntiXss]
		public decimal QtyIn { get; set; }

		[AntiXss]
		public decimal AmountIn { get; set; }

		[AntiXss]
		public decimal QtyOut { get; set; }

		[AntiXss]
		public decimal AmountOut { get; set; }

		[AntiXss]
		public decimal QtyEnding { get; set; }

		[AntiXss]
		public decimal AmountEnding { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
