#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_xts_accountpayablepayment  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 13/01/2020 9:21:47
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_xts_accountpayablepayment
    {
        public Int64 IDRow { get; set; }

		public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xts_idempotentmessage { get; set; }

		public string xts_vendordescription { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Int64 versionnumber { get; set; }

		public DateTime createdon { get; set; }

		public Guid owninguser { get; set; }

		public decimal xts_totalremainingbalance_base { get; set; }

		public string xts_methodofpaymentidname { get; set; }

		public string statuscodename { get; set; }

		public string xts_status { get; set; }

		public Guid xts_cashandbankid { get; set; }

		public decimal xts_totalpaymentamount { get; set; }

		public Guid xts_accountpayablevoucherreferenceid { get; set; }

		public string xts_cancelledname { get; set; }

		public Guid xts_accountpayablereferenceid { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string modifiedbyname { get; set; }

		public string owneridname { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public decimal xts_paymentsettlement_base { get; set; }

		public decimal xts_totalotherexpenses_base { get; set; }

		public string xts_cashandbankidname { get; set; }

		public string xts_type { get; set; }

		public Guid xts_accountpayablepaymentid { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal exchangerate { get; set; }

		public string ktb_isinterfacedname { get; set; }

		public Guid owningteam { get; set; }

		public int statecode { get; set; }

		public string xts_sourcetype { get; set; }

		public string xts_chequenumber { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public decimal xts_totalpaymentamount_base { get; set; }

		public string xts_locking { get; set; }

		public string xts_handling { get; set; }

		public decimal xts_totaloutstandingbalance_base { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string ktb_girono { get; set; }

		public decimal xts_totalotherexpenses { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xts_statusname { get; set; }

		public string xts_typename { get; set; }

		public DateTime xts_chequedate { get; set; }

		public Guid ownerid { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public string owneridyominame { get; set; }

		public string ktb_cancelreason { get; set; }

		public string modifiedbyyominame { get; set; }

		public decimal xts_appliedtodocument_base { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public bool xts_cancelled { get; set; }

		public string ktb_salesorderno { get; set; }

		public string createdbyname { get; set; }

		public decimal xts_appliedtodocument { get; set; }

		public Guid xts_methodofpaymentid { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string ktb_bankgiro { get; set; }

		public string xts_postinglayername { get; set; }

		public string xts_handlingname { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public DateTime ktb_giroduedate { get; set; }

		public Guid createdby { get; set; }

		public Guid modifiedby { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public string createdbyyominame { get; set; }

		public decimal xts_totaloutstandingbalance { get; set; }

		public string transactioncurrencyidname { get; set; }

		public decimal xts_totalchangeamount_base { get; set; }

		public DateTime ktb_actualpaymentdate { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string xts_postinglayer { get; set; }

		public Guid xts_vendorid { get; set; }

		public string xts_accountpayablepaymentnumber { get; set; }

		public decimal xts_paymentsettlement { get; set; }

		public string xts_accountpayablevoucherreferenceidname { get; set; }

		public string xts_sourcetypename { get; set; }

		public string owneridtype { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public string ktb_invoiceno { get; set; }

		public string ktb_say { get; set; }

		public decimal xts_totalremainingbalance { get; set; }

		public string xts_accountpayablereferenceidname { get; set; }

		public decimal xts_totalchangeamount { get; set; }

		public bool ktb_isinterfaced { get; set; }

		public string statecodename { get; set; }

		public string xts_businessunitidname { get; set; }

		public string xts_vendoridname { get; set; }

		public string RowStatus { get; set; }

		public string ktb_externalcode { get; set; }

		public string msdyn_companycode { get; set; }
    }
}