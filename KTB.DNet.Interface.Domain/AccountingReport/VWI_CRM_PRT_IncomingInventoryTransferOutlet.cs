#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_PRT_IncomingInventoryTransferOutlet  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/01/2020 16:30:06
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_PRT_IncomingInventoryTransferOutlet
    {
        public Int64 IDRow { get; set; }

		public string company { get; set; }

		public string businessunitcode { get; set; }

		public string InventoryTransactionNo { get; set; }

		public string InventoryTransferNo { get; set; }

		public string FromBU { get; set; }

		public DateTime TransactionDate { get; set; }

		public string Product { get; set; }

		public string ProductDescription { get; set; }

		public string Site { get; set; }

		public string Warehouse { get; set; }

		public string Location { get; set; }

		public decimal Quantity { get; set; }

		public string TransactionUnit { get; set; }

		public decimal UnitCost { get; set; }

		public decimal TotalCost { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
