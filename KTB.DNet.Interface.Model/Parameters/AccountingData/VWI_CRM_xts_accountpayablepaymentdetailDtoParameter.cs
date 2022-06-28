#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_accountpayablepaymentdetailParameterDto  class
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
    public class VWI_CRM_xts_accountpayablepaymentdetailParameterDto : ParameterDtoBase, IValidatableObject
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
		public Guid xts_cashandbankid { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xts_ordervendortransactionaxaptaidname { get; set; }

		[AntiXss]
		public Guid xts_transactiondocumentid { get; set; }

		[AntiXss]
		public string xts_externaldocumenttype { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public Guid xts_orderuvsoreferralid { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid xts_orderoutsourceworkorderid { get; set; }

		[AntiXss]
		public decimal xts_outstandingbalance_base { get; set; }

		[AntiXss]
		public string xts_ordernvsoreferralidname { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_orderoutsourceworkorderidname { get; set; }

		[AntiXss]
		public string xts_cashandbankidname { get; set; }

		[AntiXss]
		public Guid xts_accountpayablepaymentid { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid xts_ordervendortransactionaxaptaid { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xts_orderpurchaseorderidname { get; set; }

		[AntiXss]
		public string xts_ordernvsalesorderidname { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public int xts_orderlookuptype { get; set; }

		[AntiXss]
		public decimal xts_paymenamount_base { get; set; }

		[AntiXss]
		public string xts_transactiondocumentidname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_externaldocumentnumber { get; set; }

		[AntiXss]
		public string xts_sourcetype { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public decimal xts_remainingbalance { get; set; }

		[AntiXss]
		public string xts_accountpayablepaymentdetailnumber { get; set; }

		[AntiXss]
		public Guid xts_ordernvsoreferralid { get; set; }

		[AntiXss]
		public decimal xts_outstandingbalance { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public decimal xts_paymenamount { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_orderapvoucheridname { get; set; }

		[AntiXss]
		public Guid xts_cashandbankaccountid { get; set; }

		[AntiXss]
		public bool xts_receiptfromvendor { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public Guid xts_ordernvsalesorderid { get; set; }

		[AntiXss]
		public decimal xts_differencevalue { get; set; }

		[AntiXss]
		public string xts_accountpayablepaymentidname { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public decimal xts_changeamount { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public Guid xts_orderpurchaseorderid { get; set; }

		[AntiXss]
		public string xts_orderuvsalesorderidname { get; set; }

		[AntiXss]
		public string xts_orderlookupname { get; set; }

		[AntiXss]
		public string xts_paymentslipnumber { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_externaldocumenttypename { get; set; }

		[AntiXss]
		public Guid xts_accountpayablepaymentdetailid { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string xts_cashandbankaccountidname { get; set; }

		[AntiXss]
		public Guid xts_vendorid { get; set; }

		[AntiXss]
		public Guid xts_orderuvsalesorderid { get; set; }

		[AntiXss]
		public decimal xts_remainingbalance_base { get; set; }

		[AntiXss]
		public string xts_sourcetypename { get; set; }

		[AntiXss]
		public Guid xts_orderapvoucherid { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public decimal xts_changeamount_base { get; set; }

		[AntiXss]
		public string xts_orderuvsoreferralidname { get; set; }

		[AntiXss]
		public string xts_receiptfromvendorname { get; set; }

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
