#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_OutgoingInventoryTransferWarehouseDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/01/2020 16:30:07
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_PRT_OutgoingInventoryTransferWarehouseDto : DtoBase
    {
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
