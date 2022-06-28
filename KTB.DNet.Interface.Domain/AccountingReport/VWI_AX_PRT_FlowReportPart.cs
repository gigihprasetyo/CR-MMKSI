#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_AX_PRT_FlowReportPart  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/02/2020 9:04:16
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_AX_PRT_FlowReportPart
    {
        public Int64 IDRow { get; set; }

		public string Company { get; set; }

		public string Product { get; set; }

		public string ProductDescription { get; set; }

		public string Site { get; set; }

		public string Warehouse { get; set; }

		public string Period { get; set; }

		public string StockUOM { get; set; }

		public string TransactionDateFrom { get; set; }

		public string TransactionDateTo { get; set; }

		public decimal BeginningQty { get; set; }

		public decimal BeginningAmount { get; set; }

		public decimal QtyIn { get; set; }

		public decimal AmountIn { get; set; }

		public decimal QtyOut { get; set; }

		public decimal AmountOut { get; set; }

		public decimal QtyEnding { get; set; }

		public decimal AmountEnding { get; set; }
    }
}
