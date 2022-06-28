#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_SLS_DailyActivityMonitoring_VDO  class
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
    public class VWI_CRM_SLS_DailyActivityMonitoring_VDO
    {
        public Int64 IDRow { get; set; }

		public string businessunitcode { get; set; }

		public string xts_newvehicledeliveryordernumber { get; set; }

		public string xts_newvehiclesalesordernumber { get; set; }

		public string personincharge { get; set; }

		public string salesperson { get; set; }

		public Guid xts_productsegment2id { get; set; }
    }
}
