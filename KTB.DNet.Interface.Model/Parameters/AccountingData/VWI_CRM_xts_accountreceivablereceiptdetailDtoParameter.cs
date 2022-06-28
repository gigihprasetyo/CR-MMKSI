#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_accountreceivablereceiptdetailParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:52
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
    public class VWI_CRM_xts_accountreceivablereceiptdetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public DateTime xts_orderdate { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xts_methodofpaymentidname { get; set; }

		[AntiXss]
		public Guid xts_transactiondocumentid { get; set; }

		[AntiXss]
		public Guid xts_receivabledimension1id { get; set; }

		[AntiXss]
		public string xts_invoiceidname { get; set; }

		[AntiXss]
		public string xts_eventdata { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public Guid xts_customerid { get; set; }

		[AntiXss]
		public Guid xts_cashandbankid { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xts_receivabledimension4idname { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xts_ordernvsoidname { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public Guid xts_orderworkorderid { get; set; }

		[AntiXss]
		public Guid xts_accountreceivablereceiptdetailid { get; set; }

		[AntiXss]
		public string xts_ordersalesorderidname { get; set; }

		[AntiXss]
		public decimal xts_outstandingbalance_base { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_receivabledimension2idname { get; set; }

		[AntiXss]
		public Guid xts_ordertypeid { get; set; }

		[AntiXss]
		public string xts_cashandbankidname { get; set; }

		[AntiXss]
		public string xts_orderwriteoffbalanceidname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid xts_receivabledimension2id { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public Guid xts_orderwriteoffbalanceid { get; set; }

		[AntiXss]
		public string xts_receivabledimension5idname { get; set; }

		[AntiXss]
		public string xts_customeridyominame { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public int xts_orderlookuptype { get; set; }

		[AntiXss]
		public string xts_transactiondocumentidname { get; set; }

		[AntiXss]
		public DateTime xts_exchangeratedate { get; set; }

		[AntiXss]
		public string xts_paidbacktocustomername { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_invoicenumbertemp { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public decimal xts_remainingbalance { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid ktb_workorderid { get; set; }

		[AntiXss]
		public decimal xts_exchangerateamount { get; set; }

		[AntiXss]
		public Guid xts_invoiceid { get; set; }

		[AntiXss]
		public Guid xts_ordersalesorderid { get; set; }

		[AntiXss]
		public Guid xts_ordernvsoid { get; set; }

		[AntiXss]
		public decimal xts_outstandingbalance { get; set; }

		[AntiXss]
		public Guid xts_receivabledimension4id { get; set; }

		[AntiXss]
		public string xts_exchangeratetypename { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_accountreceivablereceiptidname { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public Guid xts_receivabledimension3id { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public decimal xts_receiptamount_base { get; set; }

		[AntiXss]
		public decimal xts_differencevalue { get; set; }

		[AntiXss]
		public string xts_customeridname { get; set; }

		[AntiXss]
		public Guid xts_accountreceivablereceiptid { get; set; }

		[AntiXss]
		public string xts_ordertypeidname { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public decimal xts_changeamount { get; set; }

		[AntiXss]
		public Guid xts_methodofpaymentid { get; set; }

		[AntiXss]
		public string xts_accountreceivablereceiptdetail { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_source { get; set; }

		[AntiXss]
		public string xts_sourcename { get; set; }

		[AntiXss]
		public string xts_orderuvsoidname { get; set; }

		[AntiXss]
		public Guid xts_orderuvsoid { get; set; }

		[AntiXss]
		public Guid xts_receivableaccountid { get; set; }

		[AntiXss]
		public string ktb_workorderidname { get; set; }

		[AntiXss]
		public string xts_orderlookupname { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public bool xts_paidbacktocustomer { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_ordercustomertransactionaxaptaidname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public Guid xts_receivabledimension5id { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string xts_receivabledimension6idname { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public Guid xts_receivabledimension6id { get; set; }

		[AntiXss]
		public string xts_receivableaccountidname { get; set; }

		[AntiXss]
		public decimal xts_remainingbalance_base { get; set; }

		[AntiXss]
		public string xts_receivabledimension3idname { get; set; }

		[AntiXss]
		public string xts_receivabledimension1idname { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public decimal xts_changeamount_base { get; set; }

		[AntiXss]
		public Guid xts_exchangeratetype { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_withholdingtaxpaymentmethod { get; set; }

		[AntiXss]
		public decimal xts_receiptamount { get; set; }

		[AntiXss]
		public string xts_withholdingtaxpaymentmethodname { get; set; }

		[AntiXss]
		public Guid xts_ordercustomertransactionaxaptaid { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string xts_orderworkorderidname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
