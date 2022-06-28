#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_purchaserequisitionhistoryParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 14/01/2020 9:03:03
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
    public class VWI_CRM_ktb_purchaserequisitionhistoryParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string ktb_dnetstatusnewname { get; set; }

		[AntiXss]
		public string ktb_dnetponumber { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime ktb_dnetpodate { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string ktb_purchaserequisitionidname { get; set; }

		[AntiXss]
		public DateTime ktb_estimationdeliverydate { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string ktb_sotype { get; set; }

		[AntiXss]
		public string ktb_expeditionname { get; set; }

		[AntiXss]
		public DateTime ktb_readyfordeliverydate { get; set; }

		[AntiXss]
		public DateTime ktb_packingdate { get; set; }

		[AntiXss]
		public Guid ktb_purchaserequisitionhistoryid { get; set; }

		[AntiXss]
		public string ktb_dnetstatus { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string ktb_businessunitidname { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string ktb_name { get; set; }

		[AntiXss]
		public DateTime ktb_eta { get; set; }

		[AntiXss]
		public DateTime ktb_atd { get; set; }

		[AntiXss]
		public string ktb_dnetstatusnew { get; set; }

		[AntiXss]
		public DateTime ktb_paymentdate { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string ktb_dnetdonumber { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public int ktb_totalamount { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public DateTime ktb_dnetdodate { get; set; }

		[AntiXss]
		public DateTime ktb_dnetsodate { get; set; }

		[AntiXss]
		public string ktb_dnetsonumber { get; set; }

		[AntiXss]
		public Guid ktb_businessunitid { get; set; }

		[AntiXss]
		public string ktb_sotypename { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public DateTime ktb_pickingdate { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public DateTime ktb_goodsissuedate { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public Guid ktb_purchaserequisitionid { get; set; }

		[AntiXss]
		public int ktb_discount { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string ktb_expeditionno { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
