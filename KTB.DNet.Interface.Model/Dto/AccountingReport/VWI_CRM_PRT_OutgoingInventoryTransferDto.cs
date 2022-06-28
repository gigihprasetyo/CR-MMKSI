#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_OutgoingInventoryTransferDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2019 10:31:04
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_PRT_OutgoingInventoryTransferDto : DtoBase
    {
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
