#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_AX_SLS_StockMutation  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/01/2020 10:25:21
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_AX_SLS_StockMutation
    {
        public Int64 IDRow { get; set; }

		public string COMPANYCODE { get; set; }

		public Int64 INVENTTRANSRECID { get; set; }

		public string Product { get; set; }

		public string UNITID { get; set; }

		public decimal QTY { get; set; }

		public string Site { get; set; }

		public string Warehouse { get; set; }

		public string STATUSISSUE { get; set; }

		public string STATUSRECEIPT { get; set; }

		public decimal TotalCost { get; set; }

		public decimal CostAdjustment { get; set; }

		public string REFERENCECATEGORY { get; set; }

		public string REFERENCEID { get; set; }

		public DateTime TransactionDate { get; set; }

		public Int64 INVENTTRANSORIGIN { get; set; }

		public string PACKINGSLIPID { get; set; }

		public string TRANSACTIONTYPE { get; set; }

		public string VALUEOPEN { get; set; }

		public DateTime INVENTTRANSMODIFIEDDATETIME { get; set; }

		public string DIMENSION1 { get; set; }

		public string DIMENSION2 { get; set; }

		public string DIMENSION3 { get; set; }

		public string DIMENSION4 { get; set; }

		public string DIMENSION5 { get; set; }

		public string DIMENSION6 { get; set; }

		public string INVENTSERIALID { get; set; }

		public string Account { get; set; }

		public string VIN { get; set; }

		public string ProductClassName { get; set; }

		public string ProductType { get; set; }

		public string ExteriorColorID { get; set; }

		public string InteriorColorID { get; set; }

		public string Company { get; set; }

		public Int64 RECID { get; set; }

		public string Period { get; set; }
    }
}
