#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_SLS_StockMutationParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/01/2020 10:25:21
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
    public class VWI_AX_SLS_StockMutationParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string COMPANYCODE { get; set; }

		[AntiXss]
		public Int64 INVENTTRANSRECID { get; set; }

		[AntiXss]
		public string Product { get; set; }

		[AntiXss]
		public string UNITID { get; set; }

		[AntiXss]
		public decimal QTY { get; set; }

		[AntiXss]
		public string Site { get; set; }

		[AntiXss]
		public string Warehouse { get; set; }

		[AntiXss]
		public string STATUSISSUE { get; set; }

		[AntiXss]
		public string STATUSRECEIPT { get; set; }

		[AntiXss]
		public decimal TotalCost { get; set; }

		[AntiXss]
		public decimal CostAdjustment { get; set; }

		[AntiXss]
		public string REFERENCECATEGORY { get; set; }

		[AntiXss]
		public string REFERENCEID { get; set; }

		[AntiXss]
		public DateTime TransactionDate { get; set; }

		[AntiXss]
		public Int64 INVENTTRANSORIGIN { get; set; }

		[AntiXss]
		public string PACKINGSLIPID { get; set; }

		[AntiXss]
		public string TRANSACTIONTYPE { get; set; }

		[AntiXss]
		public string VALUEOPEN { get; set; }

		[AntiXss]
		public DateTime INVENTTRANSMODIFIEDDATETIME { get; set; }

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
		public string INVENTSERIALID { get; set; }

		[AntiXss]
		public string Account { get; set; }

		[AntiXss]
		public string VIN { get; set; }

		[AntiXss]
		public string ProductClassName { get; set; }

		[AntiXss]
		public string ProductType { get; set; }

		[AntiXss]
		public string ExteriorColorID { get; set; }

		[AntiXss]
		public string InteriorColorID { get; set; }

		[AntiXss]
		public string Company { get; set; }

		[AntiXss]
		public Int64 RECID { get; set; }

		[AntiXss]
		public string Period { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
