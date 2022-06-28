#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_accountpayablevoucherDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 13/01/2020 14:30:17
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_accountpayablevoucherDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xts_transactiontypename { get; set; }

		public string xts_accountpayablevouchernumber { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Int64 versionnumber { get; set; }

		public DateTime createdon { get; set; }

		public decimal xts_totallandedcost_base { get; set; }

		public string statuscodename { get; set; }

		public string xts_status { get; set; }

		public decimal xts_paymentamount_base { get; set; }

		public string modifiedbyyominame { get; set; }

		public string ktb_taxinvoiceno { get; set; }

		public string owneridtype { get; set; }

		public string xts_vendorinvoicenumber { get; set; }

		public decimal xts_totalvariance_base { get; set; }

		public DateTime ktb_invoicedate { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string modifiedbyname { get; set; }

		public decimal xts_totalmisccharges { get; set; }

		public string owneridname { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public DateTime xts_documentdate { get; set; }

		public Guid ktb_kontrabonid { get; set; }

		public string xts_type { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal exchangerate { get; set; }

		public string ktb_description { get; set; }

		public Guid xts_apvoucherreferencenumberid { get; set; }

		public decimal xts_paymentamount { get; set; }

		public decimal xts_balance { get; set; }

		public int statecode { get; set; }

		public string xts_transactiontype { get; set; }

		public decimal xts_consumptiontax1amount_base { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public Guid xts_accountpayablevoucherid { get; set; }

		public string xts_apvoucherreferencenumberidname { get; set; }

		public string xts_locking { get; set; }

		public string xts_handling { get; set; }

		public string ktb_say { get; set; }

		public decimal xts_totalmisccharges_base { get; set; }

		public decimal xts_totalconsumptiontax_base { get; set; }

		public decimal xts_totalamount_base { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string ktb_kontrabonidname { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public decimal xts_totalwitholdingtaxamount { get; set; }

		public string xts_statusname { get; set; }

		public string xts_typename { get; set; }

		public string xts_hasreturnname { get; set; }

		public Guid ownerid { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public string owneridyominame { get; set; }

		public bool xts_hasreturn { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public string createdbyname { get; set; }

		public decimal xts_totallandedcost { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public DateTime xts_duedate { get; set; }

		public Guid xts_paymenttermsid { get; set; }

		public decimal xts_balance_base { get; set; }

		public decimal xts_totalvariance { get; set; }

		public string xts_handlingname { get; set; }

		public decimal xts_grandtotal { get; set; }

		public decimal xts_grandtotal_base { get; set; }

		public decimal xts_consumptiontax2amount { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public Guid owningteam { get; set; }

		public Guid createdby { get; set; }

		public Guid modifiedby { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public string createdbyyominame { get; set; }

		public Guid owninguser { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string xts_paymenttermsidname { get; set; }

		public Guid ktb_prpotypeid { get; set; }

		public Guid owningbusinessunit { get; set; }

		public decimal xts_consumptiontax1amount { get; set; }

		public decimal xts_consumptiontax2amount_base { get; set; }

		public Guid xts_vendorid { get; set; }

		public decimal xts_totalamount { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public decimal xts_totalconsumptiontax { get; set; }

		public string xts_vendordescription { get; set; }

		public decimal xts_totalwitholdingtaxamount_base { get; set; }

		public string ktb_prpotypeidname { get; set; }

		public string statecodename { get; set; }

		public string xts_businessunitidname { get; set; }

		public string xts_vendoridname { get; set; }

		public bool ktb_isallowcancel { get; set; }

		public string ktb_externalcode { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
