#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_PRT_OutgoingInventoryTransferWarehouse  class
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
    public class VWI_CRM_PRT_OutgoingInventoryTransferWarehouse
    {
        public Int64 IDRow { get; set; }

		public string company { get; set; }

		public string businessunitcode { get; set; }

		public string InventoryTransferNo { get; set; }

		public DateTime TransactionDate { get; set; }

		public string TransactionType { get; set; }

		public string WorkOrderNO { get; set; }

		public string FromSite { get; set; }

		public string FromWarehouse { get; set; }

		public string ToWarehouse { get; set; }

		public string Product { get; set; }

		public string ProductDescription { get; set; }

		public decimal Quantity { get; set; }

		public string UOM { get; set; }

		public decimal COGSTrx { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
