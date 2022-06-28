#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SVC_FreeServiceToBeInvoicedDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2019 10:31:06
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_SVC_FreeServiceToBeInvoicedDto : DtoBase
    {
        public string businessunitcode { get; set; }

		public string xts_workorder { get; set; }

		public string xts_workorderstatus { get; set; }

		public string xts_servicecategory { get; set; }

		public string xts_product { get; set; }

		public string xts_platenumber { get; set; }

		public string xts_ordertype { get; set; }

		public string customername { get; set; }

		public string billtocustomername { get; set; }

		public DateTime ktb_wodate { get; set; }
    }
}
