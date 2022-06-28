#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_AX_PRT_StockMovement  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 31/01/2020 8:18:25
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_AX_PRT_StockMovement
    {
        public Int64 IDRow { get; set; }

		public string Company { get; set; }

		public string Product { get; set; }

		public string ProductDescription { get; set; }

		public string TransType { get; set; }

		public string ReferenceID { get; set; }

		public string PackingSlipID { get; set; }

		public DateTime TransactionDate { get; set; }

		public string Unit { get; set; }

		public decimal BeginningBalance { get; set; }

		public decimal BeginningTotalCost { get; set; }

		public decimal TotalCost { get; set; }

		public decimal QTY { get; set; }

		public string Site { get; set; }

		public string Warehouse { get; set; }

		public string STATUSISSUE { get; set; }

		public string Account { get; set; }

		public string DIMENSION1 { get; set; }

		public string DIMENSION2 { get; set; }

		public string DIMENSION3 { get; set; }

		public string DIMENSION4 { get; set; }

		public string DIMENSION5 { get; set; }

		public string DIMENSION6 { get; set; }

		public string ProductClass { get; set; }

		public string ProductType { get; set; }

		public string Period { get; set; }
    }
}
