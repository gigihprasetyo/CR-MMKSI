#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_xts_purchaseorder  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 1/9/2020 1:13:13 PM
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_xts_purchaseorder
    {
        public Int64 IDRow { get; set; }

		public string company { get; set; }

		public string businessunitcode { get; set; }

		public string ktb_dealerpknumber { get; set; }

		public string xts_prpotypeidname { get; set; }

		public string xts_taxablename { get; set; }

		public decimal xts_totalwithholdingtaxamount_base { get; set; }

		public string ktb_documenttype { get; set; }

		public string ktb_purchaseorderno { get; set; }

		public decimal ktb_redeemamount { get; set; }

		public DateTime modifiedon { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public bool xts_downpaymentispaid { get; set; }

		public string statuscodename { get; set; }

		public string xts_status { get; set; }

		public Guid xts_partsforecastid { get; set; }

		public decimal xts_totaltitleregistrationfee_base { get; set; }

		public string xts_handling { get; set; }

		public string xts_workorderidname { get; set; }

		public decimal xts_downpaymentamountpaid_base { get; set; }

		public Guid ktb_methodofpaymentid { get; set; }

		public DateTime ktb_salesorderdate { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string xts_vendordescription { get; set; }

		public string xts_postalcode { get; set; }

		public string ktb_salesorderno { get; set; }

		public decimal xts_balance_base { get; set; }

		public string xts_termsofpaymentdescription { get; set; }

		public Guid xts_workorderid { get; set; }

		public Guid xts_personinchargeid { get; set; }

		public Guid owningbusinessunit { get; set; }

		public decimal xts_totalamountbeforediscount { get; set; }

		public string ktb_salesordertype { get; set; }

		public bool ktb_isbodypaint { get; set; }

		public bool ktb_isinterfaced { get; set; }

		public Guid xts_salesorderid { get; set; }

		public decimal xts_totalpaymentamount { get; set; }

		public int ktb_numberofinstallment { get; set; }

		public string xts_formsourcename { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal xts_totalpaymentamount_base { get; set; }

		public int statuscode { get; set; }

		public string ktb_ordertypename { get; set; }

		public string ktb_purchasetypename { get; set; }

		public string xts_warehouseidname { get; set; }

		public string ktb_orderno { get; set; }

		public decimal ktb_pph22amount { get; set; }

		public decimal xts_totaldiscountamount_base { get; set; }

		public Guid xts_vendorcountryid { get; set; }

		public string ktb_purchasepriorityname { get; set; }

		public string xts_stockreferencenumber { get; set; }

		public string ktb_dnetsalesordertype { get; set; }

		public string ktb_identificationtype { get; set; }

		public string statecodename { get; set; }

		public string bsi_isfactoringname { get; set; }

		public string xts_closereason { get; set; }

		public string ktb_documenttypename { get; set; }

		public string xts_newvehiclesalesorderidname { get; set; }

		public decimal exchangerate { get; set; }

		public bool xts_includetax { get; set; }

		public Guid ktb_salespersonid { get; set; }

		public DateTime ktb_duedate { get; set; }

		public decimal ktb_pph22amount_base { get; set; }

		public string xts_termsofpaymentidname { get; set; }

		public DateTime createdon { get; set; }

		public string ktb_orderconfirmationno { get; set; }

		public string ktb_purchasepriority { get; set; }

		public string xts_purchaseordernumber { get; set; }

		public string xts_statusname { get; set; }

		public decimal ktb_landedcost_base { get; set; }

		public decimal xts_totalbaseamount_base { get; set; }

		public decimal xts_totalconsumptiontaxamount { get; set; }

		public string xts_externaldocumentnumber { get; set; }

		public Guid processid { get; set; }

		public string owneridyominame { get; set; }

		public Guid xts_countryid { get; set; }

		public string xts_eventdata2 { get; set; }

		public Guid modifiedby { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string createdbyname { get; set; }

		public string xts_address1 { get; set; }

		public string xts_includetaxname { get; set; }

		public string xts_personinchargeidname { get; set; }

		public decimal xts_totalamountbeforediscount_base { get; set; }

		public decimal xts_totalwithholdingtaxamount { get; set; }

		public string xts_priority { get; set; }

		public string traversedpath { get; set; }

		public decimal xts_grandtotal { get; set; }

		public Guid owninguser { get; set; }

		public string xts_taxable { get; set; }

		public string owneridtype { get; set; }

		public string ktb_interfacestatusname { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public Guid xts_termsofpaymentid { get; set; }

		public string xts_partsforecastidname { get; set; }

		public string owneridname { get; set; }

		public string ktb_salesorderidname { get; set; }

		public string ktb_isinterfacedname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string ktb_dnetsalesordertypename { get; set; }

		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		public Guid owningteam { get; set; }

		public string xts_vendoraddress2 { get; set; }

		public Guid ktb_salesorderid { get; set; }

		public decimal xts_grandtotal_base { get; set; }

		public Guid xts_prpotypeid { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public Guid xts_siteid { get; set; }

		public decimal xts_downpayment_base { get; set; }

		public decimal xts_totalbaseamount { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string ktb_ordertype { get; set; }

		public string xts_countryidname { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string xts_eventdata { get; set; }

		public Guid xts_cityid { get; set; }

		public decimal xts_downpayment { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string xts_vendorprovinceidname { get; set; }

		public string xts_vendoraddress3 { get; set; }

		public bool bsi_isfactoring { get; set; }

		public decimal ktb_vehicletotalamount { get; set; }

		public Guid xts_warehouseid { get; set; }

		public string xts_priorityname { get; set; }

		public string modifiedbyyominame { get; set; }

		public Guid ownerid { get; set; }

		public string ktb_identificationtypename { get; set; }

		public decimal ktb_interesttotalamount_base { get; set; }

		public decimal ktb_interesttotalamount { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string xts_provinceidname { get; set; }

		public string ktb_say { get; set; }

		public string xts_vendoridname { get; set; }

		public string xts_deliverymethodname { get; set; }

		public string ktb_isbodypaintname { get; set; }

		public string xts_businessunitidname { get; set; }

		public string xts_description { get; set; }

		public decimal xts_totaltitleregistrationfee { get; set; }

		public string xts_siteidname { get; set; }

		public decimal ktb_landedcost { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public Guid createdby { get; set; }

		public string xts_vendorcityidname { get; set; }

		public decimal xts_downpaymentamountpaid { get; set; }

		public decimal ktb_vehicletotalamount_base { get; set; }

		public Guid xts_purchaseorderid { get; set; }

		public decimal xts_balance { get; set; }

		public decimal ktb_redeemamount_base { get; set; }

		public string xjp_idempotentmessage { get; set; }

		public decimal xts_totaldiscountamount { get; set; }

		public Guid stageid { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public Guid xts_vendorid { get; set; }

		public Guid xts_vendorcityid { get; set; }

		public Guid xts_provinceid { get; set; }

		public string ktb_interfacestatus { get; set; }

		public string xts_vendorpostalcode { get; set; }

		public int importsequencenumber { get; set; }

		public string xts_locking { get; set; }

		public string xts_vendoraddress1 { get; set; }

		public string xts_vendorcountryidname { get; set; }

		public string xts_downpaymentispaidname { get; set; }

		public string ktb_methodofpaymentidname { get; set; }

		public string xts_paymentgroupname { get; set; }

		public string xts_address2 { get; set; }

		public Guid xts_businessunitid { get; set; }

		public Guid xts_vendorprovinceid { get; set; }

		public string xts_allocationperiod { get; set; }

		public string ktb_purchasetype { get; set; }

		public string xts_vendoraddress4 { get; set; }

		public string xts_salesorderidname { get; set; }

		public string xts_handlingname { get; set; }

		public string xts_deliverymethod { get; set; }

		public string xts_paymentgroup { get; set; }

		public string modifiedbyname { get; set; }

		public string createdbyyominame { get; set; }

		public Guid xts_newvehiclesalesorderid { get; set; }

		public string xts_address4 { get; set; }

		public string xts_cityidname { get; set; }

		public string xts_address3 { get; set; }

		public string xts_formsource { get; set; }

		public string ktb_salespersonidname { get; set; }

		public Int64 versionnumber { get; set; }

		public int statecode { get; set; }

		public string RowStatus { get; set; }

		public DateTime ktb_etd { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
