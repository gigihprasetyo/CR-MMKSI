#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SVC_ARReceiptDetailBasedOnWOParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_SVC_ARReceiptDetailBasedOnWOParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_workorder { get; set; }

		[AntiXss]
		public string xts_site { get; set; }

		[AntiXss]
		public string xts_ordertype { get; set; }

		[AntiXss]
		public string xts_leaseapprovalnumber { get; set; }

		[AntiXss]
		public string xts_claimapprovalnumber { get; set; }

		[AntiXss]
		public string xts_servicecategory { get; set; }

		[AntiXss]
		public Guid xts_customerid { get; set; }

		[AntiXss]
		public Guid xts_billtocustomerid { get; set; }

		[AntiXss]
		public Guid xts_vehicleidentificationid { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public string xts_product { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public string xts_platenumber { get; set; }

		[AntiXss]
		public Guid xts_servicepersoninchargeid { get; set; }

		[AntiXss]
		public Guid xts_salespersonid { get; set; }

		[AntiXss]
		public decimal xts_esttotalpartsamount { get; set; }

		[AntiXss]
		public decimal xts_totalpartsamount { get; set; }

		[AntiXss]
		public decimal xts_totalworkamount { get; set; }

		[AntiXss]
		public decimal xts_esttotalworkamount { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargeamount { get; set; }

		[AntiXss]
		public decimal xts_esttotalmiscchargeamount { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesamount { get; set; }

		[AntiXss]
		public decimal xts_esttotalothersalesamount { get; set; }

		[AntiXss]
		public decimal xts_totalpaymentamount { get; set; }

		[AntiXss]
		public decimal xts_grandtotalamount { get; set; }

		[AntiXss]
		public Guid xts_incomingoutsourcebusinessunitid { get; set; }

		[AntiXss]
		public Guid xts_originaloutsorcereferenceoutsourcewoid { get; set; }

		[AntiXss]
		public Guid xts_originalworkorderreferencenumberid { get; set; }

		[AntiXss]
		public Guid xts_incomingoutsourcereferenceid { get; set; }

		[AntiXss]
		public string xts_claimstatus { get; set; }

		[AntiXss]
		public DateTime xts_scheduledarrivaldateandtime { get; set; }

		[AntiXss]
		public DateTime xts_actualarrivaldateandtime { get; set; }

		[AntiXss]
		public DateTime xts_scheduledservicestartdateandtime { get; set; }

		[AntiXss]
		public DateTime xts_actualservicestartdateandtime { get; set; }

		[AntiXss]
		public DateTime xts_scheduledservicefinishdateandtime { get; set; }

		[AntiXss]
		public DateTime xts_actualservicefinishdateandtime { get; set; }

		[AntiXss]
		public DateTime xts_scheduledfinishdateandtime { get; set; }

		[AntiXss]
		public DateTime xts_actualfinishdateandtime { get; set; }

		[AntiXss]
		public bool xts_autocreateoutsourceworkorderreceipt { get; set; }

		[AntiXss]
		public DateTime ktb_wodate { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
