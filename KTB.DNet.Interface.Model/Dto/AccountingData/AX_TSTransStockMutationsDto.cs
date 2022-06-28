#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : AX_TSTransStockMutationsDto  class
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
#endregion

namespace KTB.DNet.Interface.Model
{
    public class AX_TSTransStockMutationsDto : DtoBase
    {
        public string CompanyCode { get; set; }

		public Int64 InventTransRecId { get; set; }

		public string ItemId { get; set; }

		public string UnitId { get; set; }

		public decimal Qty { get; set; }

		public string InventSiteId { get; set; }

		public string InventLocationId { get; set; }

		public string WMSLocationId { get; set; }

		public string StatusIssue { get; set; }

		public string StatusReceipt { get; set; }

		public decimal CostAmountPhysical { get; set; }

		public decimal CostAmountAdjustment { get; set; }

		public string ReferenceCategory { get; set; }

		public string ReferenceId { get; set; }

		public DateTime DatePhysical { get; set; }

		public Int64 InventTransOrigin { get; set; }

		public string PackingSlipId { get; set; }

		public string TransactionType { get; set; }

		public string ValueOpen { get; set; }

		public DateTime InventTransModifiedDateTime { get; set; }

		public string Dimension1 { get; set; }

		public string Dimension2 { get; set; }

		public string Dimension3 { get; set; }

		public string Dimension4 { get; set; }

		public string Dimension5 { get; set; }

		public string Dimension6 { get; set; }

		public string inventSerialId { get; set; }

		public string MainAccount { get; set; }

		public string xtsvin { get; set; }

		public string DMSProductClassName { get; set; }

		public string DMSProductType { get; set; }

		public string configId { get; set; }

		public string InventColorId { get; set; }

		public string InventSizeId { get; set; }

		public string InventStyleId { get; set; }

		public string dataAreaId { get; set; }

		public Int64 PARTITION { get; set; }

		public Int64 RecordId { get; set; }

		public Int64 RECVERSION { get; set; }

		public string TranPeriodID { get; set; }

		//public string DealerCode { get; set; }

		//public string SourceType { get; set; }
    }
}
