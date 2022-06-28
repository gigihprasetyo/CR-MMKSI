#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_PRT_StockMovementParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 31/01/2020 8:18:25
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
    public class VWI_AX_PRT_StockMovementParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string Company { get; set; }

		[AntiXss]
		public string Product { get; set; }

		[AntiXss]
		public string ProductDescription { get; set; }

		[AntiXss]
		public string TransType { get; set; }

		[AntiXss]
		public string ReferenceID { get; set; }

		[AntiXss]
		public string PackingSlipID { get; set; }

		[AntiXss]
		public DateTime TransactionDate { get; set; }

		[AntiXss]
		public string Unit { get; set; }

		[AntiXss]
		public decimal BeginningBalance { get; set; }

		[AntiXss]
		public decimal BeginningTotalCost { get; set; }

		[AntiXss]
		public decimal TotalCost { get; set; }

		[AntiXss]
		public decimal QTY { get; set; }

		[AntiXss]
		public string Site { get; set; }

		[AntiXss]
		public string Warehouse { get; set; }

		[AntiXss]
		public string STATUSISSUE { get; set; }

		[AntiXss]
		public string Account { get; set; }

		[AntiXss]
		public string DIMENSION1 { get; set; }

		[AntiXss]
		public string DIMENSION2 { get; set; }

		[AntiXss]
		public string DIMENSION3 { get; set; }

		[AntiXss]
		public string DIMENSION4 { get; set; }

		[AntiXss]
		public string DIMENSION5 { get; set; }

		[AntiXss]
		public string DIMENSION6 { get; set; }

		[AntiXss]
		public string ProductClass { get; set; }

		[AntiXss]
		public string ProductType { get; set; }

		[AntiXss]
		public string Period { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
