#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_pdireceiptParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:51
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
    public class VWI_CRM_xjp_pdireceiptParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string ktb_spkidname { get; set; }

		[AntiXss]
		public string xjp_idempotentmessage { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public DateTime xjp_vendorinvoicedate { get; set; }

		[AntiXss]
		public string xjp_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public decimal xjp_totalsubcontractfee_base { get; set; }

		[AntiXss]
		public decimal xjp_totalpartsfee { get; set; }

		[AntiXss]
		public string xjp_handlingname { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xjp_personinchargeidname { get; set; }

		[AntiXss]
		public string xjp_vendoridname { get; set; }

		[AntiXss]
		public Guid xjp_parentbusinessunitid { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xjp_transactiontype { get; set; }

		[AntiXss]
		public decimal xjp_totalfee { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public Guid xjp_vendorid { get; set; }

		[AntiXss]
		public DateTime xjp_transactiondate { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xjp_pdireceiptreferenceidname { get; set; }

		[AntiXss]
		public decimal xjp_totalservicefee_base { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public bool ktb_iskaroseri { get; set; }

		[AntiXss]
		public Guid ktb_spkid { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public DateTime xjp_duedate { get; set; }

		[AntiXss]
		public string ktb_spkdetailidname { get; set; }

		[AntiXss]
		public string xjp_handling { get; set; }

		[AntiXss]
		public decimal xjp_totalsubcontractfee { get; set; }

		[AntiXss]
		public Guid xjp_businessunitid { get; set; }

		[AntiXss]
		public string xjp_vendorinvoicenumber { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string ktb_chassisnumber { get; set; }

		[AntiXss]
		public string xjp_pdireceiptnumber { get; set; }

		[AntiXss]
		public string xjp_pdireceiptstatus { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public decimal xjp_totalfee_base { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string xjp_pdiidname { get; set; }

		[AntiXss]
		public Guid ktb_spkdetailid { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public decimal xjp_totalpartsfee_base { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid xjp_pdireceiptreferenceid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public Guid xjp_personinchargeid { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public string ktb_blankospkidname { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public decimal xjp_totalservicefee { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xjp_businessunitidname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public Guid ktb_blankospkid { get; set; }

		[AntiXss]
		public string ktb_iskaroseriname { get; set; }

		[AntiXss]
		public Guid xjp_pdiid { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xjp_pdireceiptstatusname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xjp_transactiontypename { get; set; }

		[AntiXss]
		public Guid xjp_pdireceiptid { get; set; }

		[AntiXss]
		public string xjp_locking { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
