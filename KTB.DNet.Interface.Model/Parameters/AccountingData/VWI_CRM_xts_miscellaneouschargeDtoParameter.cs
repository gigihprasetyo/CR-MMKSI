#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_miscellaneouschargeParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/01/2020 14:19:26
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
    public class VWI_CRM_xts_miscellaneouschargeParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_miscchargedimension3idname { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public string xts_tax { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public decimal xts_amount { get; set; }

		[AntiXss]
		public string xts_miscchargedimension10idname { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax1id { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_miscellaneouscharge { get; set; }

		[AntiXss]
		public string xts_miscchargedimension1from { get; set; }

		[AntiXss]
		public string ktb_chargetypename { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xts_miscchargedimension3fromname { get; set; }

		[AntiXss]
		public string xts_miscchargedimension2fromname { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_type { get; set; }

		[AntiXss]
		public Guid xts_miscchargedimension8id { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xts_miscchargedimension6fromname { get; set; }

		[AntiXss]
		public string xts_miscchargedimension1idname { get; set; }

		[AntiXss]
		public Guid xts_miscellaneouschargeid { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax2id { get; set; }

		[AntiXss]
		public string xts_miscellaneouschargeaccountidname { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_miscchargedimension6from { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public decimal xjp_estimatecost_base { get; set; }

		[AntiXss]
		public string xts_miscchargedimension3from { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string xts_miscchargedimension7idname { get; set; }

		[AntiXss]
		public Guid xts_miscchargedimension2id { get; set; }

		[AntiXss]
		public Guid xts_miscchargedimension1id { get; set; }

		[AntiXss]
		public string xts_miscchargedimension5idname { get; set; }

		[AntiXss]
		public string xts_taxname { get; set; }

		[AntiXss]
		public string xts_miscchargedimension4from { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_miscchargedimension6idname { get; set; }

		[AntiXss]
		public decimal xts_amount_base { get; set; }

		[AntiXss]
		public string xts_consumptiontax1idname { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_consumptiontax2idname { get; set; }

		[AntiXss]
		public decimal xjp_estimatecost { get; set; }

		[AntiXss]
		public Guid xts_miscchargedimension4id { get; set; }

		[AntiXss]
		public string xts_miscchargedimension2from { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public Guid xts_miscchargedimension9id { get; set; }

		[AntiXss]
		public string xts_miscchargedimension4fromname { get; set; }

		[AntiXss]
		public string xts_typename { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public Guid xts_miscellaneouschargeaccountid { get; set; }

		[AntiXss]
		public Guid xts_miscchargedimension3id { get; set; }

		[AntiXss]
		public string xts_miscchargedescription { get; set; }

		[AntiXss]
		public string xts_miscchargedimension8idname { get; set; }

		[AntiXss]
		public string ktb_chargetype { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_salesflagname { get; set; }

		[AntiXss]
		public string xts_pkcombinationkey { get; set; }

		[AntiXss]
		public string xts_miscchargedimension9idname { get; set; }

		[AntiXss]
		public Guid xts_miscchargedimension10id { get; set; }

		[AntiXss]
		public string xts_miscchargedimension4idname { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string xts_miscchargedimension1fromname { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid xts_miscchargedimension5id { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xts_miscchargedimension5fromname { get; set; }

		[AntiXss]
		public string xts_miscchargedimension5from { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_miscchargedimension2idname { get; set; }

		[AntiXss]
		public bool xts_salesflag { get; set; }

		[AntiXss]
		public Guid xts_miscchargedimension7id { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public Guid xts_miscchargedimension6id { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
