#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_PRT_OutgoingInventoryTransfer  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2019 10:31:04
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_PRT_OutgoingInventoryTransfer
    {
        public Int64 IDRow { get; set; }

		public string businessunitcode { get; set; }

		public string xts_inventorytransfernumber { get; set; }

		public string xts_product { get; set; }

		public string xts_productdescription { get; set; }

		public decimal xts_quantity { get; set; }

		public decimal ktb_cogstrx { get; set; }

		public string transactiontypename { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public string fromwarehousename { get; set; }

		public string towarehousename { get; set; }

		public string fromsitename { get; set; }

		public string tositename { get; set; }

		public string tobusinessunitcode { get; set; }
    }
}
