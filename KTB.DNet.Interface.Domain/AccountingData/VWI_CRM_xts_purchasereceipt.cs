#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_xts_purchasereceipt  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/01/2020 8:22:49
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_xts_purchasereceipt
    {
        public Int64 IDRow { get; set; }

		public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xts_vendordescription { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Int64 versionnumber { get; set; }

		public DateTime createdon { get; set; }

		public bool ktb_updatetosparepartstock { get; set; }

		public Guid ktb_purchaserequisitionid { get; set; }

		public string ktb_isfactoringname { get; set; }

		public Guid xts_provinceid { get; set; }

		public DateTime ktb_packingdate { get; set; }

		public string statuscodename { get; set; }

		public string xts_status { get; set; }

		public DateTime xts_vendorinvoicedate { get; set; }

		public string modifiedbyyominame { get; set; }

		public string ktb_interfacehandling { get; set; }

		public string owneridtype { get; set; }

		public string xts_vendorinvoicenumber { get; set; }

		public string ktb_identificationtypename { get; set; }

		public decimal xts_totalbaseamount_base { get; set; }

		public Guid xts_termsofpaymentid { get; set; }

		public bool xts_autoinvoiced { get; set; }

		public string xts_loaddataname { get; set; }

		public string modifiedbyname { get; set; }

		public decimal xts_totalwithholdingtaxamount { get; set; }

		public string xts_accountpayablevoucheridname { get; set; }

		public string ktb_interfacestatusname { get; set; }

		public bool xts_purchasereceiptreferencerequired { get; set; }

		public string owneridname { get; set; }

		public string xts_provinceidname { get; set; }

		public bool xts_assignlandedcostperdetail { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string xts_typename { get; set; }

		public string xts_address2 { get; set; }

		public string xts_type { get; set; }

		public string xts_eventdata { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal exchangerate { get; set; }

		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		public string xts_address4 { get; set; }

		public Guid xts_returnpurchasereceiptid { get; set; }

		public Guid owningteam { get; set; }

		public decimal xts_totalbaseamount { get; set; }

		public int statecode { get; set; }

		public decimal xts_totalwithholdingtaxamount_base { get; set; }

		public string ktb_updatetosparepartstockname { get; set; }

		public string xts_termsofpaymentidname { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public Guid xts_accountpayablevoucherid { get; set; }

		public DateTime xts_deliveryorderdate { get; set; }

		public DateTime ktb_paymentdate { get; set; }

		public DateTime ktb_goodissuedate { get; set; }

		public DateTime ktb_ata { get; set; }

		public bool xts_loaddata { get; set; }

		public decimal xts_totalconsumptiontaxamount { get; set; }

		public string xts_locking { get; set; }

		public string xts_purchasereceiptnumber { get; set; }

		public string ktb_salesorderno { get; set; }

		public DateTime ktb_pickingdate { get; set; }

		public string xts_assignlandedcostperdetailname { get; set; }

		public decimal xts_totaltitleregistrationfee_base { get; set; }

		public Guid xts_countryid { get; set; }

		public DateTime ktb_duedate { get; set; }

		public string xts_returnpurchasereceiptidname { get; set; }

		public string traversedpath { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string xts_handling { get; set; }

		public Guid xts_cityid { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string xts_autoinvoicedname { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string xts_deliveryordernumber { get; set; }

		public string xts_statusname { get; set; }

		public decimal xts_totaltitleregistrationfee { get; set; }

		public Guid ownerid { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public string owneridyominame { get; set; }

		public string xts_workorderidname { get; set; }

		public string xts_countryidname { get; set; }

		public Guid xts_transferorderrequestingid { get; set; }

		public decimal xts_totalconsumptiontax1amount { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public bool ktb_isfactoring { get; set; }

		public string xts_purchaseorderidname { get; set; }

		public Guid xts_workorderid { get; set; }

		public string xts_eventdata2 { get; set; }

		public string createdbyname { get; set; }

		public string ktb_interfacestatus { get; set; }

		public Guid xts_businessunitid { get; set; }

		public DateTime ktb_deliveryfordeliverydate { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string xts_address3 { get; set; }

		public DateTime ktb_invoicedate { get; set; }

		public DateTime ktb_estimationdeliverydate { get; set; }

		public string xts_cityidname { get; set; }

		public Guid xts_purchasereceiptid { get; set; }

		public bool xts_bigdata { get; set; }

		public string ktb_ribbondataproductwarehouse { get; set; }

		public Guid processid { get; set; }

		public string xts_handlingname { get; set; }

		public decimal xts_grandtotal { get; set; }

		public decimal xts_grandtotal_base { get; set; }

		public string xts_postalcode { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public DateTime xts_packingslipdate { get; set; }

		public string xts_businessunitidname { get; set; }

		public Guid createdby { get; set; }

		public bool ktb_isinterfaced { get; set; }

		public Guid modifiedby { get; set; }

		public string xts_address1 { get; set; }

		public string ktb_ribbondata { get; set; }

		public Guid stageid { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public string ktb_identificationtype { get; set; }

		public string createdbyyominame { get; set; }

		public Guid owninguser { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid ktb_prpotypeid { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string xts_packingslipnumber { get; set; }

		public Guid xts_purchaseorderid { get; set; }

		public string ktb_purchaserequisitionidname { get; set; }

		public string ktb_isinterfacedname { get; set; }

		public decimal xts_totalconsumptiontax2amount { get; set; }

		public string ktb_interfacehandlingname { get; set; }

		public Guid xts_vendorid { get; set; }

		public string xts_bigdataname { get; set; }

		public string ktb_expeditionnumber { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public decimal ktb_conversiondays { get; set; }

		public string xts_purchasereceiptreferencerequiredname { get; set; }

		public decimal xts_totalconsumptiontax2amount_base { get; set; }

		public int ktb_discountpercentage { get; set; }

		public string xts_transferorderrequestingidname { get; set; }

		public string ktb_returnreason { get; set; }

		public string ktb_prpotypeidname { get; set; }

		public string statecodename { get; set; }

		public decimal xts_totalconsumptiontax1amount_base { get; set; }

		public string xts_vendoridname { get; set; }

		public string RowStatus { get; set; }

		public DateTime ktb_eta { get; set; }

		public DateTime ktb_atd { get; set; }

		public string ktb_kondisikendaraan { get; set; }

		public int ktb_endpoint { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
