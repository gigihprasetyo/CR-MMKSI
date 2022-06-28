#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_purchaseorderParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 1/9/2020 1:13:14 PM
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
    public class VWI_CRM_xts_purchaseorderParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string ktb_dealerpknumber { get; set; }

		[AntiXss]
		public string xts_prpotypeidname { get; set; }

		[AntiXss]
		public string xts_taxablename { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxamount_base { get; set; }

		[AntiXss]
		public string ktb_documenttype { get; set; }

		[AntiXss]
		public string ktb_purchaseorderno { get; set; }

		[AntiXss]
		public decimal ktb_redeemamount { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public bool xts_downpaymentispaid { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public Guid xts_partsforecastid { get; set; }

		[AntiXss]
		public decimal xts_totaltitleregistrationfee_base { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public string xts_workorderidname { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamountpaid_base { get; set; }

		[AntiXss]
		public Guid ktb_methodofpaymentid { get; set; }

		[AntiXss]
		public DateTime ktb_salesorderdate { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_vendordescription { get; set; }

		[AntiXss]
		public string xts_postalcode { get; set; }

		[AntiXss]
		public string ktb_salesorderno { get; set; }

		[AntiXss]
		public decimal xts_balance_base { get; set; }

		[AntiXss]
		public string xts_termsofpaymentdescription { get; set; }

		[AntiXss]
		public Guid xts_workorderid { get; set; }

		[AntiXss]
		public Guid xts_personinchargeid { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public decimal xts_totalamountbeforediscount { get; set; }

		[AntiXss]
		public string ktb_salesordertype { get; set; }

		[AntiXss]
		public bool ktb_isbodypaint { get; set; }

		[AntiXss]
		public bool ktb_isinterfaced { get; set; }

		[AntiXss]
		public Guid xts_salesorderid { get; set; }

		[AntiXss]
		public decimal xts_totalpaymentamount { get; set; }

		[AntiXss]
		public int ktb_numberofinstallment { get; set; }

		[AntiXss]
		public string xts_formsourcename { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal xts_totalpaymentamount_base { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string ktb_ordertypename { get; set; }

		[AntiXss]
		public string ktb_purchasetypename { get; set; }

		[AntiXss]
		public string xts_warehouseidname { get; set; }

		[AntiXss]
		public string ktb_orderno { get; set; }

		[AntiXss]
		public decimal ktb_pph22amount { get; set; }

		[AntiXss]
		public decimal xts_totaldiscountamount_base { get; set; }

		[AntiXss]
		public Guid xts_vendorcountryid { get; set; }

		[AntiXss]
		public string ktb_purchasepriorityname { get; set; }

		[AntiXss]
		public string xts_stockreferencenumber { get; set; }

		[AntiXss]
		public string ktb_dnetsalesordertype { get; set; }

		[AntiXss]
		public string ktb_identificationtype { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string bsi_isfactoringname { get; set; }

		[AntiXss]
		public string xts_closereason { get; set; }

		[AntiXss]
		public string ktb_documenttypename { get; set; }

		[AntiXss]
		public string xts_newvehiclesalesorderidname { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public bool xts_includetax { get; set; }

		[AntiXss]
		public Guid ktb_salespersonid { get; set; }

		[AntiXss]
		public DateTime ktb_duedate { get; set; }

		[AntiXss]
		public decimal ktb_pph22amount_base { get; set; }

		[AntiXss]
		public string xts_termsofpaymentidname { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string ktb_orderconfirmationno { get; set; }

		[AntiXss]
		public string ktb_purchasepriority { get; set; }

		[AntiXss]
		public string xts_purchaseordernumber { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public decimal ktb_landedcost_base { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount { get; set; }

		[AntiXss]
		public string xts_externaldocumentnumber { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public Guid xts_countryid { get; set; }

		[AntiXss]
		public string xts_eventdata2 { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xts_address1 { get; set; }

		[AntiXss]
		public string xts_includetaxname { get; set; }

		[AntiXss]
		public string xts_personinchargeidname { get; set; }

		[AntiXss]
		public decimal xts_totalamountbeforediscount_base { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxamount { get; set; }

		[AntiXss]
		public string xts_priority { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public decimal xts_grandtotal { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string xts_taxable { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string ktb_interfacestatusname { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public Guid xts_termsofpaymentid { get; set; }

		[AntiXss]
		public string xts_partsforecastidname { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string ktb_salesorderidname { get; set; }

		[AntiXss]
		public string ktb_isinterfacedname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string ktb_dnetsalesordertypename { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_vendoraddress2 { get; set; }

		[AntiXss]
		public Guid ktb_salesorderid { get; set; }

		[AntiXss]
		public decimal xts_grandtotal_base { get; set; }

		[AntiXss]
		public Guid xts_prpotypeid { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public Guid xts_siteid { get; set; }

		[AntiXss]
		public decimal xts_downpayment_base { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string ktb_ordertype { get; set; }

		[AntiXss]
		public string xts_countryidname { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string xts_eventdata { get; set; }

		[AntiXss]
		public Guid xts_cityid { get; set; }

		[AntiXss]
		public decimal xts_downpayment { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_vendorprovinceidname { get; set; }

		[AntiXss]
		public string xts_vendoraddress3 { get; set; }

		[AntiXss]
		public bool bsi_isfactoring { get; set; }

		[AntiXss]
		public decimal ktb_vehicletotalamount { get; set; }

		[AntiXss]
		public Guid xts_warehouseid { get; set; }

		[AntiXss]
		public string xts_priorityname { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public string ktb_identificationtypename { get; set; }

		[AntiXss]
		public decimal ktb_interesttotalamount_base { get; set; }

		[AntiXss]
		public decimal ktb_interesttotalamount { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_provinceidname { get; set; }

		[AntiXss]
		public string ktb_say { get; set; }

		[AntiXss]
		public string xts_vendoridname { get; set; }

		[AntiXss]
		public string xts_deliverymethodname { get; set; }

		[AntiXss]
		public string ktb_isbodypaintname { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public decimal xts_totaltitleregistrationfee { get; set; }

		[AntiXss]
		public string xts_siteidname { get; set; }

		[AntiXss]
		public decimal ktb_landedcost { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string xts_vendorcityidname { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamountpaid { get; set; }

		[AntiXss]
		public decimal ktb_vehicletotalamount_base { get; set; }

		[AntiXss]
		public Guid xts_purchaseorderid { get; set; }

		[AntiXss]
		public decimal xts_balance { get; set; }

		[AntiXss]
		public decimal ktb_redeemamount_base { get; set; }

		[AntiXss]
		public string xjp_idempotentmessage { get; set; }

		[AntiXss]
		public decimal xts_totaldiscountamount { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public Guid xts_vendorid { get; set; }

		[AntiXss]
		public Guid xts_vendorcityid { get; set; }

		[AntiXss]
		public Guid xts_provinceid { get; set; }

		[AntiXss]
		public string ktb_interfacestatus { get; set; }

		[AntiXss]
		public string xts_vendorpostalcode { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_vendoraddress1 { get; set; }

		[AntiXss]
		public string xts_vendorcountryidname { get; set; }

		[AntiXss]
		public string xts_downpaymentispaidname { get; set; }

		[AntiXss]
		public string ktb_methodofpaymentidname { get; set; }

		[AntiXss]
		public string xts_paymentgroupname { get; set; }

		[AntiXss]
		public string xts_address2 { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public Guid xts_vendorprovinceid { get; set; }

		[AntiXss]
		public string xts_allocationperiod { get; set; }

		[AntiXss]
		public string ktb_purchasetype { get; set; }

		[AntiXss]
		public string xts_vendoraddress4 { get; set; }

		[AntiXss]
		public string xts_salesorderidname { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public string xts_deliverymethod { get; set; }

		[AntiXss]
		public string xts_paymentgroup { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid xts_newvehiclesalesorderid { get; set; }

		[AntiXss]
		public string xts_address4 { get; set; }

		[AntiXss]
		public string xts_cityidname { get; set; }

		[AntiXss]
		public string xts_address3 { get; set; }

		[AntiXss]
		public string xts_formsource { get; set; }

		[AntiXss]
		public string ktb_salespersonidname { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public DateTime ktb_etd { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
