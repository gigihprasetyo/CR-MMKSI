#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_campaignParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:44
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
    public class VWI_CRM_campaignParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public int ktb_luaspameran { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Int64 entityimage_timestamp { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string objective { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string tmpregardingobjectid { get; set; }

		[AntiXss]
		public decimal xts_totalwonopportunities { get; set; }

		[AntiXss]
		public int xts_totalwonopportunities_state { get; set; }

		[AntiXss]
		public string entityimage_url { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xts_salesobjective { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public decimal ktb_approvalsewatempat_base { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string promotioncodename { get; set; }

		[AntiXss]
		public string ktb_statuscampaign { get; set; }

		[AntiXss]
		public string message { get; set; }

		[AntiXss]
		public string ktb_kategoriprodukname { get; set; }

		[AntiXss]
		public decimal totalactualcost_base { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public decimal ktb_approvalbabit_base { get; set; }

		[AntiXss]
		public DateTime xts_totalwonopportunities_date { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public decimal xts_totalwonopportunities_base { get; set; }

		[AntiXss]
		public int xts_totalcampaignactivities_state { get; set; }

		[AntiXss]
		public decimal xts_totalvalueopportunities_base { get; set; }

		[AntiXss]
		public int xts_totalcampaignactivities { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public decimal ktb_biayaoperasional_base { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public decimal ktb_biayasewa_base { get; set; }

		[AntiXss]
		public decimal expectedrevenue_base { get; set; }

		[AntiXss]
		public DateTime xts_totalcampaignresponse_date { get; set; }

		[AntiXss]
		public decimal budgetedcost_base { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public decimal ktb_biayasewa { get; set; }

		[AntiXss]
		public int xts_totalopportunities_state { get; set; }

		[AntiXss]
		public DateTime xts_convertedleads_date { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public decimal ktb_approvalnonbabit_base { get; set; }

		[AntiXss]
		public decimal expectedrevenue { get; set; }

		[AntiXss]
		public int xts_totalcampaignresponse_state { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public int xts_totalmembers { get; set; }

		[AntiXss]
		public string pricelistname { get; set; }

		[AntiXss]
		public DateTime xts_totalcampaignactivities_date { get; set; }

		[AntiXss]
		public DateTime xts_totalvalueopportunities_date { get; set; }

		[AntiXss]
		public decimal ktb_approvalbabit { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public decimal othercost { get; set; }

		[AntiXss]
		public string emailaddress { get; set; }

		[AntiXss]
		public string typecodename { get; set; }

		[AntiXss]
		public int expectedresponse { get; set; }

		[AntiXss]
		public decimal ktb_approvalsewatempat { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public DateTime actualstart { get; set; }

		[AntiXss]
		public decimal ktb_proposalclaimdealer { get; set; }

		[AntiXss]
		public int xts_convertedleads { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public int xts_convertedleads_state { get; set; }

		[AntiXss]
		public DateTime xts_totalmembers_date { get; set; }

		[AntiXss]
		public string ktb_statusreason { get; set; }

		[AntiXss]
		public string istemplatename { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string ktb_statuscampaignname { get; set; }

		[AntiXss]
		public int xts_totalvalueopportunities_state { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public string description { get; set; }

		[AntiXss]
		public int ktb_targetspk { get; set; }

		[AntiXss]
		public DateTime proposedstart { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public int xts_totalcampaignresponse { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string xts_salesobjectivename { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public Guid pricelistid { get; set; }

		[AntiXss]
		public string entityimage { get; set; }

		[AntiXss]
		public Guid xts_purposeid { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public decimal xts_totalvalueopportunities { get; set; }

		[AntiXss]
		public string codename { get; set; }

		[AntiXss]
		public DateTime proposedend { get; set; }

		[AntiXss]
		public DateTime ktb_transactiondate { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public decimal totalcampaignactivityactualcost { get; set; }

		[AntiXss]
		public Guid campaignid { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public int xts_totalopportunities { get; set; }

		[AntiXss]
		public string xts_website { get; set; }

		[AntiXss]
		public decimal totalactualcost { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public DateTime xts_totalopportunities_date { get; set; }

		[AntiXss]
		public DateTime actualend { get; set; }

		[AntiXss]
		public bool istemplate { get; set; }

		[AntiXss]
		public string xts_purposeidname { get; set; }

		[AntiXss]
		public string ktb_statusreasonname { get; set; }

		[AntiXss]
		public decimal ktb_approvalnonbabit { get; set; }

		[AntiXss]
		public int xts_totalmembers_state { get; set; }

		[AntiXss]
		public string ktb_kategoriproduk { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string ktb_venuepameran { get; set; }

		[AntiXss]
		public string name { get; set; }

		[AntiXss]
		public decimal totalcampaignactivityactualcost_base { get; set; }

		[AntiXss]
		public decimal ktb_biayaoperasional { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public decimal budgetedcost { get; set; }

		[AntiXss]
		public decimal othercost_base { get; set; }

		[AntiXss]
		public decimal ktb_proposalclaimdealer_base { get; set; }

		[AntiXss]
		public Guid entityimageid { get; set; }

		[AntiXss]
		public Guid xts_categoryid { get; set; }

		[AntiXss]
		public string typecode { get; set; }

		[AntiXss]
		public int ktb_targetprospek { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string xts_categoryidname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
