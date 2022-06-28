#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_apvdetailParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 1/9/2020 2:44:54 PM
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
    public class VWI_CRM_xts_apvdetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public decimal xts_billamount { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public string xts_servicereceiptidname { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xts_usedvehiclesalesreferralidname { get; set; }

		[AntiXss]
		public decimal xts_billbaseamount { get; set; }

		[AntiXss]
		public Guid xts_withholdingtaxid { get; set; }

		[AntiXss]
		public Guid xts_newvehiclesalesreferralid { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax1id { get; set; }

		[AntiXss]
		public string xts_reasonidname { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public decimal xts_titleregistrationbillfeeamount { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid xts_outsourceworkorderreceiptid { get; set; }

		[AntiXss]
		public string xts_outsourceworkorderreceiptidname { get; set; }

		[AntiXss]
		public Guid xts_dimension5id { get; set; }

		[AntiXss]
		public Guid xts_usedvehiclegoodsreceiptid { get; set; }

		[AntiXss]
		public Guid xts_reasonid { get; set; }

		[AntiXss]
		public decimal xts_varianceamount_base { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xts_purchasereceiptidname { get; set; }

		[AntiXss]
		public decimal xts_billamount_base { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid xts_outsourceworkorderid { get; set; }

		[AntiXss]
		public Guid xts_dimension2id { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string xts_purchaseorderidname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_dimension1id { get; set; }

		[AntiXss]
		public string xts_dimension5idname { get; set; }

		[AntiXss]
		public decimal xts_varianceamount { get; set; }

		[AntiXss]
		public string xts_dimension6idname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_billbaseamount_base { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax2id { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public Guid xts_dimension4id { get; set; }

		[AntiXss]
		public decimal xts_withholdingtaxamount { get; set; }

		[AntiXss]
		public decimal xts_titleregistrationfee { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_accountidname { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount_base { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid xts_accountpayablevoucherid { get; set; }

		[AntiXss]
		public Guid xjp_pdireceiptid { get; set; }

		[AntiXss]
		public string xts_prpotypeidname { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public Guid xts_prpotypeid { get; set; }

		[AntiXss]
		public string xts_dimension2idname { get; set; }

		[AntiXss]
		public string xts_consumptiontax1idname { get; set; }

		[AntiXss]
		public Guid xts_purchasepricevarianceaccountid { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_consumptiontax2idname { get; set; }

		[AntiXss]
		public string xts_apvdetailnumber { get; set; }

		[AntiXss]
		public Guid xts_titleregistrationfeereferenceid { get; set; }

		[AntiXss]
		public string xts_purchasepricevarianceaccountidname { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_newvehiclesalesreferralidname { get; set; }

		[AntiXss]
		public string xts_usedvehiclegoodsreceiptidname { get; set; }

		[AntiXss]
		public decimal xts_titleregistrationfee_base { get; set; }

		[AntiXss]
		public string xts_assessmentidname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public Guid xts_dimension6id { get; set; }

		[AntiXss]
		public decimal xts_receiptamount_base { get; set; }

		[AntiXss]
		public Guid xts_assessmentid { get; set; }

		[AntiXss]
		public string xjp_pdireceiptidname { get; set; }

		[AntiXss]
		public string xts_dimension3idname { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_outsourceworkorderidname { get; set; }

		[AntiXss]
		public Guid xts_purchasereceiptid { get; set; }

		[AntiXss]
		public Guid xts_usedvehiclesalesreferralid { get; set; }

		[AntiXss]
		public string xts_titleregistrationfeereferenceidname { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid xts_apvdetailid { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public decimal xts_withholdingtaxamount_base { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_dimension4idname { get; set; }

		[AntiXss]
		public Guid xts_servicereceiptid { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string xts_dimension1idname { get; set; }

		[AntiXss]
		public Guid xts_purchaseorderid { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount_base { get; set; }

		[AntiXss]
		public string xts_eventdata { get; set; }

		[AntiXss]
		public decimal xts_titleregistrationbillfeeamount_base { get; set; }

		[AntiXss]
		public string xts_accountpayablevoucheridname { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid xts_accountid { get; set; }

		[AntiXss]
		public string xts_withholdingtaxidname { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public decimal xts_receiptamount { get; set; }

		[AntiXss]
		public Guid xts_dimension3id { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
