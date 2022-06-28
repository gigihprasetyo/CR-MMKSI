#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_accountpayablepaymentParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 13/01/2020 9:21:48
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
    public class VWI_CRM_xts_accountpayablepaymentParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_idempotentmessage { get; set; }

		[AntiXss]
		public string xts_vendordescription { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public decimal xts_totalremainingbalance_base { get; set; }

		[AntiXss]
		public string xts_methodofpaymentidname { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public Guid xts_cashandbankid { get; set; }

		[AntiXss]
		public decimal xts_totalpaymentamount { get; set; }

		[AntiXss]
		public Guid xts_accountpayablevoucherreferenceid { get; set; }

		[AntiXss]
		public string xts_cancelledname { get; set; }

		[AntiXss]
		public Guid xts_accountpayablereferenceid { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public decimal xts_paymentsettlement_base { get; set; }

		[AntiXss]
		public decimal xts_totalotherexpenses_base { get; set; }

		[AntiXss]
		public string xts_cashandbankidname { get; set; }

		[AntiXss]
		public string xts_type { get; set; }

		[AntiXss]
		public Guid xts_accountpayablepaymentid { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string ktb_isinterfacedname { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_sourcetype { get; set; }

		[AntiXss]
		public string xts_chequenumber { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public decimal xts_totalpaymentamount_base { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public decimal xts_totaloutstandingbalance_base { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string ktb_girono { get; set; }

		[AntiXss]
		public decimal xts_totalotherexpenses { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public string xts_typename { get; set; }

		[AntiXss]
		public DateTime xts_chequedate { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string ktb_cancelreason { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public decimal xts_appliedtodocument_base { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public bool xts_cancelled { get; set; }

		[AntiXss]
		public string ktb_salesorderno { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public decimal xts_appliedtodocument { get; set; }

		[AntiXss]
		public Guid xts_methodofpaymentid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string ktb_bankgiro { get; set; }

		[AntiXss]
		public string xts_postinglayername { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public DateTime ktb_giroduedate { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public decimal xts_totaloutstandingbalance { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public decimal xts_totalchangeamount_base { get; set; }

		[AntiXss]
		public DateTime ktb_actualpaymentdate { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string xts_postinglayer { get; set; }

		[AntiXss]
		public Guid xts_vendorid { get; set; }

		[AntiXss]
		public string xts_accountpayablepaymentnumber { get; set; }

		[AntiXss]
		public decimal xts_paymentsettlement { get; set; }

		[AntiXss]
		public string xts_accountpayablevoucherreferenceidname { get; set; }

		[AntiXss]
		public string xts_sourcetypename { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string ktb_invoiceno { get; set; }

		[AntiXss]
		public string ktb_say { get; set; }

		[AntiXss]
		public decimal xts_totalremainingbalance { get; set; }

		[AntiXss]
		public string xts_accountpayablereferenceidname { get; set; }

		[AntiXss]
		public decimal xts_totalchangeamount { get; set; }

		[AntiXss]
		public bool ktb_isinterfaced { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string xts_vendoridname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
