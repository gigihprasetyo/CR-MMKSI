#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_PRT_IncomingTransaction  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 27/01/2020 1:23:07
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_PRT_IncomingTransaction
    {
        public Int64 IDRow { get; set; }

		public string company { get; set; }

		public string businessunitcode { get; set; }

		public string InventoryTransactionNo { get; set; }

		public DateTime TransactionDate { get; set; }

		public string TransactionType { get; set; }

		public string ReasonCode { get; set; }

		public string Product { get; set; }

		public string ProductDescription { get; set; }

		public string TransactionUnit { get; set; }

		public decimal Quantity { get; set; }

		public decimal UnitCost { get; set; }

		public decimal TotalCost { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
