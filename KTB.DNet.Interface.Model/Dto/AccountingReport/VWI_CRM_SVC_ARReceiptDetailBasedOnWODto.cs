#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SVC_ARReceiptDetailBasedOnWODto  class
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
    public class VWI_CRM_SVC_ARReceiptDetailBasedOnWODto : DtoBase
    {
        public string businessunitcode { get; set; }

		public string xts_workorder { get; set; }

		public string xts_site { get; set; }

		public string xts_ordertype { get; set; }

		public string xts_leaseapprovalnumber { get; set; }

		public string xts_claimapprovalnumber { get; set; }

		public string xts_servicecategory { get; set; }

		public Guid xts_customerid { get; set; }

		public Guid xts_billtocustomerid { get; set; }

		public Guid xts_vehicleidentificationid { get; set; }

		public Guid xts_productid { get; set; }

		public string xts_product { get; set; }

		public string xts_productdescription { get; set; }

		public string xts_platenumber { get; set; }

		public Guid xts_servicepersoninchargeid { get; set; }

		public Guid xts_salespersonid { get; set; }

		public decimal xts_esttotalpartsamount { get; set; }

		public decimal xts_totalpartsamount { get; set; }

		public decimal xts_totalworkamount { get; set; }

		public decimal xts_esttotalworkamount { get; set; }

		public decimal xts_totalmiscchargeamount { get; set; }

		public decimal xts_esttotalmiscchargeamount { get; set; }

		public decimal xts_totalothersalesamount { get; set; }

		public decimal xts_esttotalothersalesamount { get; set; }

		public decimal xts_totalpaymentamount { get; set; }

		public decimal xts_grandtotalamount { get; set; }

		public Guid xts_incomingoutsourcebusinessunitid { get; set; }

		public Guid xts_originaloutsorcereferenceoutsourcewoid { get; set; }

		public Guid xts_originalworkorderreferencenumberid { get; set; }

		public Guid xts_incomingoutsourcereferenceid { get; set; }

		public string xts_claimstatus { get; set; }

		public DateTime xts_scheduledarrivaldateandtime { get; set; }

		public DateTime xts_actualarrivaldateandtime { get; set; }

		public DateTime xts_scheduledservicestartdateandtime { get; set; }

		public DateTime xts_actualservicestartdateandtime { get; set; }

		public DateTime xts_scheduledservicefinishdateandtime { get; set; }

		public DateTime xts_actualservicefinishdateandtime { get; set; }

		public DateTime xts_scheduledfinishdateandtime { get; set; }

		public DateTime xts_actualfinishdateandtime { get; set; }

		public bool xts_autocreateoutsourceworkorderreceipt { get; set; }

		public DateTime ktb_wodate { get; set; }
    }
}
