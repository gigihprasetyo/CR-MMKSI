#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_DailyActivityMonitoring_LeadDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2019 10:31:05
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_SLS_DailyActivityMonitoring_LeadDto : DtoBase
    {
        public string businessunitcode { get; set; }

		public string subject { get; set; }

		public string fullname { get; set; }

		public string statuscode { get; set; }

		public DateTime xts_leaddate { get; set; }

		public string xts_productsegment2 { get; set; }

		public string xts_employee { get; set; }

		public string ktb_superiors { get; set; }
    }
}
