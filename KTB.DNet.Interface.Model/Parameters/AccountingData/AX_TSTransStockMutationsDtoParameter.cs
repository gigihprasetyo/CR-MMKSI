#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : AX_TSTransStockMutationsParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 29/03/2022 9:17:20
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
    public class AX_TSTransStockMutationsParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string CompanyCode { get; set; }

		[AntiXss]
		public Int64 InventTransRecId { get; set; }

		[AntiXss]
		public string ItemId { get; set; }

		[AntiXss]
		public string UnitId { get; set; }

		[AntiXss]
		public decimal Qty { get; set; }

		[AntiXss]
		public string InventSiteId { get; set; }

		[AntiXss]
		public string InventLocationId { get; set; }

		[AntiXss]
		public string WMSLocationId { get; set; }

		[AntiXss]
		public string StatusIssue { get; set; }

		[AntiXss]
		public string StatusReceipt { get; set; }

		[AntiXss]
		public decimal CostAmountPhysical { get; set; }

		[AntiXss]
		public decimal CostAmountAdjustment { get; set; }

		[AntiXss]
		public string ReferenceCategory { get; set; }

		[AntiXss]
		public string ReferenceId { get; set; }

		[AntiXss]
		public DateTime DatePhysical { get; set; }

		[AntiXss]
		public Int64 InventTransOrigin { get; set; }

		[AntiXss]
		public string PackingSlipId { get; set; }

		[AntiXss]
		public string TransactionType { get; set; }

		[AntiXss]
		public string ValueOpen { get; set; }

		[AntiXss]
		public DateTime InventTransModifiedDateTime { get; set; }

		[AntiXss]
		public string Dimension1 { get; set; }

		[AntiXss]
		public string Dimension2 { get; set; }

		[AntiXss]
		public string Dimension3 { get; set; }

		[AntiXss]
		public string Dimension4 { get; set; }

		[AntiXss]
		public string Dimension5 { get; set; }

		[AntiXss]
		public string Dimension6 { get; set; }

		[AntiXss]
		public string inventSerialId { get; set; }

		[AntiXss]
		public string MainAccount { get; set; }

		[AntiXss]
		public string xtsvin { get; set; }

		[AntiXss]
		public string DMSProductClassName { get; set; }

		[AntiXss]
		public string DMSProductType { get; set; }

		[AntiXss]
		public string configId { get; set; }

		[AntiXss]
		public string InventColorId { get; set; }

		[AntiXss]
		public string InventSizeId { get; set; }

		[AntiXss]
		public string InventStyleId { get; set; }

		[AntiXss]
		public string dataAreaId { get; set; }

		[AntiXss]
		public Int64 PARTITION { get; set; }

		[AntiXss]
		public Int64 RecordId { get; set; }

		[AntiXss]
		public Int64 RECVERSION { get; set; }

		[AntiXss]
		public string TranPeriodID { get; set; }

		[AntiXss]
		public string DealerCode { get; set; }

		[AntiXss]
		public string SourceType { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
