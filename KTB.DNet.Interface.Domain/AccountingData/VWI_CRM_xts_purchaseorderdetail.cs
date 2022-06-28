#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_xts_purchaseorderdetail  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:16:00
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_xts_purchaseorderdetail
    {
        public Int64 IDRow { get; set; }

		public string company { get; set; }

		public string businessunitcode { get; set; }

		public Guid xts_salesorderdetailsid { get; set; }

		public DateTime xts_shippingdate { get; set; }

		public decimal xts_discountpercentage { get; set; }

		public decimal bsi_discountreward_base { get; set; }

		public decimal bsi_interest_base { get; set; }

		public Guid xts_consumptiontax2id { get; set; }

		public string xts_salesorderdetailidname { get; set; }

		public string xts_withholdingtaxidname { get; set; }

		public Guid xts_purchaserequisitiondetailid { get; set; }

		public Guid xts_originalproductid { get; set; }

		public DateTime modifiedon { get; set; }

		public string xts_purchaseorderidname { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public decimal xts_totalamount { get; set; }

		public string statuscodename { get; set; }

		public decimal ktb_consumptiontax1amountinterface_base { get; set; }

		public string ktb_bucompanyname { get; set; }

		public decimal ktb_logisticcost_base { get; set; }

		public decimal ktb_basepriceamount_base { get; set; }

		public decimal ktb_interest_base { get; set; }

		public string xts_productdescription { get; set; }

		public decimal xts_productweight { get; set; }

		public decimal xts_quantityorder { get; set; }

		public decimal xts_inventoryquantityreturn { get; set; }

		public decimal ktb_discountreward { get; set; }

		public string ktb_customeridyominame { get; set; }

		public string ktb_discountname { get; set; }

		public string transactioncurrencyidname { get; set; }

		public DateTime xts_requireddate { get; set; }

		public decimal xts_transactionamount_base { get; set; }

		public string xts_salesorderdetailsidname { get; set; }

		public decimal xts_unitcost_base { get; set; }

		public decimal xts_discountamount_base { get; set; }

		public decimal ktb_interestamount { get; set; }

		public string ktb_materialdecription { get; set; }

		public decimal ktb_logisticcost { get; set; }

		public Guid ktb_customerid { get; set; }

		public string xts_originalproductidname { get; set; }

		public string xts_purchaseorderdetail { get; set; }

		public string ktb_materialnumber { get; set; }

		public decimal xts_totalamountbeforediscount { get; set; }

		public string xts_productcrossreferenceidname { get; set; }

		public decimal xts_quantityreceipt { get; set; }

		public decimal ktb_ppnamount { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public int statuscode { get; set; }

		public decimal xts_consumptiontax1amount { get; set; }

		public decimal xts_inventoryquantityorder { get; set; }

		public string xts_warehouseidname { get; set; }

		public decimal ktb_netpricednet_base { get; set; }

		public string ktb_orderno { get; set; }

		public decimal ktb_pph22amount { get; set; }

		public decimal xts_totalvolume { get; set; }

		public Guid xts_purchaseorderdetailid { get; set; }

		public string xts_consumptiontax1idname { get; set; }

		public string xts_recallproductname { get; set; }

		public string statecodename { get; set; }

		public Guid xts_stockid { get; set; }

		public string xts_closereason { get; set; }

		public decimal exchangerate { get; set; }

		public decimal xts_totalamount_base { get; set; }

		public Guid xts_productexteriorcolorid { get; set; }

		public string xts_purchasefor { get; set; }

		public decimal ktb_totaldepositdnet { get; set; }

		public string xts_stockidname { get; set; }

		public decimal ktb_pph22amount_base { get; set; }

		public Guid xts_reasoncodeid { get; set; }

		public DateTime createdon { get; set; }

		public string xts_productvariantidname { get; set; }

		public string ktb_orderconfirmationno { get; set; }

		public Guid owningbusinessunit { get; set; }

		public decimal xts_totalbaseamount_base { get; set; }

		public Guid ktb_bucompany { get; set; }

		public decimal xts_totalconsumptiontaxamount { get; set; }

		public string xts_servicepartandmaterialidname { get; set; }

		public Guid xts_productvariantid { get; set; }

		public Guid xts_productid { get; set; }

		public string owneridyominame { get; set; }

		public string xts_purchaseunitidname { get; set; }

		public Guid modifiedby { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string xts_productexteriorcoloridname { get; set; }

		public Guid xts_productinteriorcolorid { get; set; }

		public string xts_nvsoaccessoriesidname { get; set; }

		public decimal xts_consumptiontax1amount_base { get; set; }

		public Guid xts_withholdingtaxid { get; set; }

		public decimal xts_totalamountbeforediscount_base { get; set; }

		public string ktb_isapprovedname { get; set; }

		public Guid xts_consumptiontax1id { get; set; }

		public string xts_departmentidname { get; set; }

		public decimal xts_unitcost { get; set; }

		public string ktb_customeridname { get; set; }

		public string xts_productsubstituteidname { get; set; }

		public decimal xts_productvolume { get; set; }

		public decimal xts_discountamount { get; set; }

		public Guid xts_servicepartandmaterialid { get; set; }

		public decimal ktb_pph23amount { get; set; }

		public string createdonbehalfbyname { get; set; }

		public Guid owninguser { get; set; }

		public Guid xts_departmentid { get; set; }

		public Guid xts_nvsoaccessoriesid { get; set; }

		public decimal xts_totalweight { get; set; }

		public string owneridtype { get; set; }

		public string xts_purchaseforname { get; set; }

		public string xts_formsourcename { get; set; }

		public decimal bsi_discountreward { get; set; }

		public decimal ktb_interestamount_base { get; set; }

		public string xts_productconfigurationidname { get; set; }

		public decimal xts_withholdingtaxamount { get; set; }

		public string owneridname { get; set; }

		public string ktb_salesorderidname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		public string xts_inventoryunitidname { get; set; }

		public decimal xts_inventoryquantityreceipt { get; set; }

		public DateTime xts_promiseddate { get; set; }

		public Guid owningteam { get; set; }

		public Guid ktb_salesorderid { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public Guid xts_siteid { get; set; }

		public decimal ktb_nettinterest_base { get; set; }

		public decimal xts_totalbaseamount { get; set; }

		public Guid createdonbehalfby { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public decimal ktb_basepriceamount { get; set; }

		public decimal ktb_discountdnet_base { get; set; }

		public decimal xts_quantityreturn { get; set; }

		public string xts_description { get; set; }

		public decimal ktb_pph23amount_base { get; set; }

		public string xts_productidname { get; set; }

		public decimal ktb_discountreward_base { get; set; }

		public decimal xts_consumptiontax2amount { get; set; }

		public Guid xts_warehouseid { get; set; }

		public string xts_formsource { get; set; }

		public string modifiedbyyominame { get; set; }

		public Guid ownerid { get; set; }

		public decimal ktb_nettinterest { get; set; }

		public Guid xts_salesorderdetailid { get; set; }

		public Guid xts_productsubstituteid { get; set; }

		public decimal xts_titleregistrationfee { get; set; }

		public int ktb_endpoint { get; set; }

		public string createdbyname { get; set; }

		public string xts_businessunitidname { get; set; }

		public int statecode { get; set; }

		public string xts_siteidname { get; set; }

		public string xts_productinteriorcoloridname { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public Guid createdby { get; set; }

		public decimal ktb_consumptiontax1amountinterface { get; set; }

		public Guid xts_purchaseorderid { get; set; }

		public decimal ktb_netpricednet { get; set; }

		public Guid xts_inventoryunitid { get; set; }

		public string xts_referencenumber { get; set; }

		public decimal ktb_interest { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string xts_completedname { get; set; }

		public decimal ktb_discountdnet { get; set; }

		public Guid xts_productstyleid { get; set; }

		public string ktb_applicationno { get; set; }

		public bool xts_recallproduct { get; set; }

		public decimal bsi_interest { get; set; }

		public bool xts_completed { get; set; }

		public int importsequencenumber { get; set; }

		public string xts_locking { get; set; }

		public string xts_consumptiontax2idname { get; set; }

		public decimal ktb_totaldepositdnet_base { get; set; }

		public Guid xts_productcrossreferenceid { get; set; }

		public decimal ktb_depositamount_base { get; set; }

		public decimal xts_consumptiontax2amount_base { get; set; }

		public Int64 versionnumber { get; set; }

		public Guid xts_businessunitid { get; set; }

		public Guid xts_purchaseunitid { get; set; }

		public decimal xts_titleregistrationfee_base { get; set; }

		public string xts_purchaserequisitiondetailidname { get; set; }

		public bool xts_closeline { get; set; }

		public Guid xts_productconfigurationid { get; set; }

		public decimal ktb_depositamount { get; set; }

		public string modifiedbyname { get; set; }

		public string createdbyyominame { get; set; }

		public string xts_closelinename { get; set; }

		public decimal xts_transactionamount { get; set; }

		public decimal xts_withholdingtaxamount_base { get; set; }

		public string ktb_productsap { get; set; }

		public bool ktb_isapproved { get; set; }

		public decimal ktb_ppnamount_base { get; set; }

		public string xts_eventdata { get; set; }

		public DateTime xts_scheduledshippingdate { get; set; }

		public string xts_reasoncodeidname { get; set; }

		public string xts_productstyleidname { get; set; }

		public string RowStatus { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
