#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_IncomingInventoryTransferDto  class
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
    public class VWI_CRM_PRT_IncomingInventoryTransferDto : DtoBase
    {
        public string businessunitcode { get; set; }

		public string xts_inventorytransactionnumber { get; set; }

		public string xts_product { get; set; }

		public string xts_productdescription { get; set; }

		public decimal xts_quantity { get; set; }

		public string transactionunitname { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public string warehousename { get; set; }

		public string sitename { get; set; }

		public string frombusinessunitcode { get; set; }
    }
}
