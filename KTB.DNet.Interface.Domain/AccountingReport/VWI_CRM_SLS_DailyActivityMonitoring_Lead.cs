#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_SLS_DailyActivityMonitoring_Lead  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2019 10:31:05
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_SLS_DailyActivityMonitoring_Lead
    {
        public Int64 IDRow { get; set; }

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
