#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_StockSummaryDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 21/01/2020 14:18:44
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_SLS_StockSummaryDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xts_stocknumber { get; set; }

		public string xts_product { get; set; }

		public string xts_description { get; set; }

		public string xts_productinteriorcolor { get; set; }

		public string xts_productexteriorcolor { get; set; }

		public DateTime xts_scheduledshipmentdate { get; set; }

		public string xts_referencenumber { get; set; }

		public DateTime xts_receivingdate { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
