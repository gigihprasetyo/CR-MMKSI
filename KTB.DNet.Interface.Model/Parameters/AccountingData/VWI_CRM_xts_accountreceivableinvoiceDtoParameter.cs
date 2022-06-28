#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_accountreceivableinvoiceParameterDto  class
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
    public class VWI_CRM_xts_accountreceivableinvoiceParameterDto : ParameterDtoBase, IValidatableObject
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
		public string xts_ordernumber { get; set; }

		[AntiXss]
		public Guid ktb_parentbusinessunitid { get; set; }

		[AntiXss]
		public decimal xts_financingcompanyinvoiceamount { get; set; }

		[AntiXss]
		public string xts_originalreferencenumberidname { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public Guid xts_customerid { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xts_type { get; set; }

		[AntiXss]
		public string xts_billabletype { get; set; }

		[AntiXss]
		public string xts_taxregistrationnumber { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string ktb_origarinvoicenumber { get; set; }

		[AntiXss]
		public string xts_financingcompanyidname { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid xts_salesorderid { get; set; }

		[AntiXss]
		public Guid xts_newvehiclesalesorderid { get; set; }

		[AntiXss]
		public string xts_salesorderidname { get; set; }

		[AntiXss]
		public string ktb_sourcetypenumbering { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_deliveryorderid { get; set; }

		[AntiXss]
		public string xts_newvehicledeliveryorderidname { get; set; }

		[AntiXss]
		public decimal xts_totalreceiptamount_base { get; set; }

		[AntiXss]
		public string xts_financingcompanynumber { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xts_workorderidname { get; set; }

		[AntiXss]
		public Guid xts_usedvehiclesalesorderid { get; set; }

		[AntiXss]
		public string xts_customeridyominame { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_taxinvoicenumber { get; set; }

		[AntiXss]
		public string xts_termofpayment { get; set; }

		[AntiXss]
		public Guid xts_newvehicledeliveryorderid { get; set; }

		[AntiXss]
		public decimal xts_financingcompanybalance { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string ktb_sourcetypenumberingname { get; set; }

		[AntiXss]
		public Guid ktb_spkid { get; set; }

		[AntiXss]
		public string xts_sourcetype { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid xts_serviceproportionalinvoiceid { get; set; }

		[AntiXss]
		public decimal xts_financingcompanybalance_base { get; set; }

		[AntiXss]
		public Guid xts_writeoffbalanceid { get; set; }

		[AntiXss]
		public Guid ktb_productid { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public string ktb_chassisnumber { get; set; }

		[AntiXss]
		public string xts_customernumber { get; set; }

		[AntiXss]
		public string xts_reversingname { get; set; }

		[AntiXss]
		public string xts_originalreferencenumber { get; set; }

		[AntiXss]
		public Guid xts_usedvehicledeliveryorderid { get; set; }

		[AntiXss]
		public string xts_accountreceivableinvoice { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string ktb_ordertypeidname { get; set; }

		[AntiXss]
		public string xts_newvehiclesalesorderidname { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_billabletypename { get; set; }

		[AntiXss]
		public decimal xts_financingcompanyreceiptamount_base { get; set; }

		[AntiXss]
		public string xts_deliveryordernumber { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public string xts_typename { get; set; }

		[AntiXss]
		public string ktb_enginenumber { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string xts_writeoffbalanceidname { get; set; }

		[AntiXss]
		public decimal xts_balance { get; set; }

		[AntiXss]
		public string xts_customeridname { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public decimal xts_invoiceamount { get; set; }

		[AntiXss]
		public Guid xts_workorderid { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid ktb_ordertypeid { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public DateTime xts_duedate { get; set; }

		[AntiXss]
		public decimal xts_balance_base { get; set; }

		[AntiXss]
		public string ktb_productidname { get; set; }

		[AntiXss]
		public string ktb_interfaceprocessname { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public Guid xts_originalreferencenumberid { get; set; }

		[AntiXss]
		public string xts_serviceproportionalinvoiceidname { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string ktb_customerdescription { get; set; }

		[AntiXss]
		public string xts_usedvehicledeliveryorderidname { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string xts_deliveryorderidname { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public decimal xts_financingcompanyinvoiceamount_base { get; set; }

		[AntiXss]
		public bool xts_reversing { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public Guid xts_financingcompanyid { get; set; }

		[AntiXss]
		public string ktb_vehicleproductionyear { get; set; }

		[AntiXss]
		public decimal xts_invoiceamount_base { get; set; }

		[AntiXss]
		public string ktb_vehiclecolordescription { get; set; }

		[AntiXss]
		public string xts_sourcetypename { get; set; }

		[AntiXss]
		public Guid xts_accountreceivableinvoiceid { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string ktb_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string ktb_say { get; set; }

		[AntiXss]
		public decimal xts_totalreceiptamount { get; set; }

		[AntiXss]
		public string xts_financingcompanyidyominame { get; set; }

		[AntiXss]
		public bool ktb_interfaceprocess { get; set; }

		[AntiXss]
		public string xts_usedvehiclesalesorderidname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public decimal xts_financingcompanyreceiptamount { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
