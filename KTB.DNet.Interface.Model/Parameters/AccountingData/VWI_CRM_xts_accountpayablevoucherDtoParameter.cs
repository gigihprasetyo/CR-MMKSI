#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_accountpayablevoucherParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_accountpayablevoucherParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_transactiontypename { get; set; }

		[AntiXss]
		public string xts_accountpayablevouchernumber { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public decimal xts_totallandedcost_base { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public decimal xts_paymentamount_base { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string ktb_taxinvoiceno { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xts_vendorinvoicenumber { get; set; }

		[AntiXss]
		public decimal xts_totalvariance_base { get; set; }

		[AntiXss]
		public DateTime ktb_invoicedate { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public decimal xts_totalmisccharges { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public DateTime xts_documentdate { get; set; }

		[AntiXss]
		public Guid ktb_kontrabonid { get; set; }

		[AntiXss]
		public string xts_type { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string ktb_description { get; set; }

		[AntiXss]
		public Guid xts_apvoucherreferencenumberid { get; set; }

		[AntiXss]
		public decimal xts_paymentamount { get; set; }

		[AntiXss]
		public decimal xts_balance { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_transactiontype { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount_base { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid xts_accountpayablevoucherid { get; set; }

		[AntiXss]
		public string xts_apvoucherreferencenumberidname { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public string ktb_say { get; set; }

		[AntiXss]
		public decimal xts_totalmisccharges_base { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontax_base { get; set; }

		[AntiXss]
		public decimal xts_totalamount_base { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string ktb_kontrabonidname { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public decimal xts_totalwitholdingtaxamount { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public string xts_typename { get; set; }

		[AntiXss]
		public string xts_hasreturnname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public bool xts_hasreturn { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public decimal xts_totallandedcost { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public DateTime xts_duedate { get; set; }

		[AntiXss]
		public Guid xts_paymenttermsid { get; set; }

		[AntiXss]
		public decimal xts_balance_base { get; set; }

		[AntiXss]
		public decimal xts_totalvariance { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public decimal xts_grandtotal { get; set; }

		[AntiXss]
		public decimal xts_grandtotal_base { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

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
		public string xts_paymenttermsidname { get; set; }

		[AntiXss]
		public Guid ktb_prpotypeid { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount_base { get; set; }

		[AntiXss]
		public Guid xts_vendorid { get; set; }

		[AntiXss]
		public decimal xts_totalamount { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontax { get; set; }

		[AntiXss]
		public string xts_vendordescription { get; set; }

		[AntiXss]
		public decimal xts_totalwitholdingtaxamount_base { get; set; }

		[AntiXss]
		public string ktb_prpotypeidname { get; set; }

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
