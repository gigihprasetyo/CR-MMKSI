#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_purchaseorderdetailParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:16:01
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
    public class VWI_CRM_xts_purchaseorderdetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public Guid xts_salesorderdetailsid { get; set; }

		[AntiXss]
		public DateTime xts_shippingdate { get; set; }

		[AntiXss]
		public decimal xts_discountpercentage { get; set; }

		[AntiXss]
		public decimal bsi_discountreward_base { get; set; }

		[AntiXss]
		public decimal bsi_interest_base { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax2id { get; set; }

		[AntiXss]
		public string xts_salesorderdetailidname { get; set; }

		[AntiXss]
		public string xts_withholdingtaxidname { get; set; }

		[AntiXss]
		public Guid xts_purchaserequisitiondetailid { get; set; }

		[AntiXss]
		public Guid xts_originalproductid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public string xts_purchaseorderidname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public decimal xts_totalamount { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public decimal ktb_consumptiontax1amountinterface_base { get; set; }

		[AntiXss]
		public string ktb_bucompanyname { get; set; }

		[AntiXss]
		public decimal ktb_logisticcost_base { get; set; }

		[AntiXss]
		public decimal ktb_basepriceamount_base { get; set; }

		[AntiXss]
		public decimal ktb_interest_base { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public decimal xts_productweight { get; set; }

		[AntiXss]
		public decimal xts_quantityorder { get; set; }

		[AntiXss]
		public decimal xts_inventoryquantityreturn { get; set; }

		[AntiXss]
		public decimal ktb_discountreward { get; set; }

		[AntiXss]
		public string ktb_customeridyominame { get; set; }

		[AntiXss]
		public string ktb_discountname { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public DateTime xts_requireddate { get; set; }

		[AntiXss]
		public decimal xts_transactionamount_base { get; set; }

		[AntiXss]
		public string xts_salesorderdetailsidname { get; set; }

		[AntiXss]
		public decimal xts_unitcost_base { get; set; }

		[AntiXss]
		public decimal xts_discountamount_base { get; set; }

		[AntiXss]
		public decimal ktb_interestamount { get; set; }

		[AntiXss]
		public string ktb_materialdecription { get; set; }

		[AntiXss]
		public decimal ktb_logisticcost { get; set; }

		[AntiXss]
		public Guid ktb_customerid { get; set; }

		[AntiXss]
		public string xts_originalproductidname { get; set; }

		[AntiXss]
		public string xts_purchaseorderdetail { get; set; }

		[AntiXss]
		public string ktb_materialnumber { get; set; }

		[AntiXss]
		public decimal xts_totalamountbeforediscount { get; set; }

		[AntiXss]
		public string xts_productcrossreferenceidname { get; set; }

		[AntiXss]
		public decimal xts_quantityreceipt { get; set; }

		[AntiXss]
		public decimal ktb_ppnamount { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount { get; set; }

		[AntiXss]
		public decimal xts_inventoryquantityorder { get; set; }

		[AntiXss]
		public string xts_warehouseidname { get; set; }

		[AntiXss]
		public decimal ktb_netpricednet_base { get; set; }

		[AntiXss]
		public string ktb_orderno { get; set; }

		[AntiXss]
		public decimal ktb_pph22amount { get; set; }

		[AntiXss]
		public decimal xts_totalvolume { get; set; }

		[AntiXss]
		public Guid xts_purchaseorderdetailid { get; set; }

		[AntiXss]
		public string xts_consumptiontax1idname { get; set; }

		[AntiXss]
		public string xts_recallproductname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public Guid xts_stockid { get; set; }

		[AntiXss]
		public string xts_closereason { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public decimal xts_totalamount_base { get; set; }

		[AntiXss]
		public Guid xts_productexteriorcolorid { get; set; }

		[AntiXss]
		public string xts_purchasefor { get; set; }

		[AntiXss]
		public decimal ktb_totaldepositdnet { get; set; }

		[AntiXss]
		public string xts_stockidname { get; set; }

		[AntiXss]
		public decimal ktb_pph22amount_base { get; set; }

		[AntiXss]
		public Guid xts_reasoncodeid { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xts_productvariantidname { get; set; }

		[AntiXss]
		public string ktb_orderconfirmationno { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount_base { get; set; }

		[AntiXss]
		public Guid ktb_bucompany { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount { get; set; }

		[AntiXss]
		public string xts_servicepartandmaterialidname { get; set; }

		[AntiXss]
		public Guid xts_productvariantid { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string xts_purchaseunitidname { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string xts_productexteriorcoloridname { get; set; }

		[AntiXss]
		public Guid xts_productinteriorcolorid { get; set; }

		[AntiXss]
		public string xts_nvsoaccessoriesidname { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount_base { get; set; }

		[AntiXss]
		public Guid xts_withholdingtaxid { get; set; }

		[AntiXss]
		public decimal xts_totalamountbeforediscount_base { get; set; }

		[AntiXss]
		public string ktb_isapprovedname { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax1id { get; set; }

		[AntiXss]
		public string xts_departmentidname { get; set; }

		[AntiXss]
		public decimal xts_unitcost { get; set; }

		[AntiXss]
		public string ktb_customeridname { get; set; }

		[AntiXss]
		public string xts_productsubstituteidname { get; set; }

		[AntiXss]
		public decimal xts_productvolume { get; set; }

		[AntiXss]
		public decimal xts_discountamount { get; set; }

		[AntiXss]
		public Guid xts_servicepartandmaterialid { get; set; }

		[AntiXss]
		public decimal ktb_pph23amount { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public Guid xts_departmentid { get; set; }

		[AntiXss]
		public Guid xts_nvsoaccessoriesid { get; set; }

		[AntiXss]
		public decimal xts_totalweight { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xts_purchaseforname { get; set; }

		[AntiXss]
		public string xts_formsourcename { get; set; }

		[AntiXss]
		public decimal bsi_discountreward { get; set; }

		[AntiXss]
		public decimal ktb_interestamount_base { get; set; }

		[AntiXss]
		public string xts_productconfigurationidname { get; set; }

		[AntiXss]
		public decimal xts_withholdingtaxamount { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string ktb_salesorderidname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		[AntiXss]
		public string xts_inventoryunitidname { get; set; }

		[AntiXss]
		public decimal xts_inventoryquantityreceipt { get; set; }

		[AntiXss]
		public DateTime xts_promiseddate { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public Guid ktb_salesorderid { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public Guid xts_siteid { get; set; }

		[AntiXss]
		public decimal ktb_nettinterest_base { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public decimal ktb_basepriceamount { get; set; }

		[AntiXss]
		public decimal ktb_discountdnet_base { get; set; }

		[AntiXss]
		public decimal xts_quantityreturn { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public decimal ktb_pph23amount_base { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public decimal ktb_discountreward_base { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount { get; set; }

		[AntiXss]
		public Guid xts_warehouseid { get; set; }

		[AntiXss]
		public string xts_formsource { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public decimal ktb_nettinterest { get; set; }

		[AntiXss]
		public Guid xts_salesorderdetailid { get; set; }

		[AntiXss]
		public Guid xts_productsubstituteid { get; set; }

		[AntiXss]
		public decimal xts_titleregistrationfee { get; set; }

		[AntiXss]
		public int ktb_endpoint { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_siteidname { get; set; }

		[AntiXss]
		public string xts_productinteriorcoloridname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public decimal ktb_consumptiontax1amountinterface { get; set; }

		[AntiXss]
		public Guid xts_purchaseorderid { get; set; }

		[AntiXss]
		public decimal ktb_netpricednet { get; set; }

		[AntiXss]
		public Guid xts_inventoryunitid { get; set; }

		[AntiXss]
		public string xts_referencenumber { get; set; }

		[AntiXss]
		public decimal ktb_interest { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_completedname { get; set; }

		[AntiXss]
		public decimal ktb_discountdnet { get; set; }

		[AntiXss]
		public Guid xts_productstyleid { get; set; }

		[AntiXss]
		public string ktb_applicationno { get; set; }

		[AntiXss]
		public bool xts_recallproduct { get; set; }

		[AntiXss]
		public decimal bsi_interest { get; set; }

		[AntiXss]
		public bool xts_completed { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_consumptiontax2idname { get; set; }

		[AntiXss]
		public decimal ktb_totaldepositdnet_base { get; set; }

		[AntiXss]
		public Guid xts_productcrossreferenceid { get; set; }

		[AntiXss]
		public decimal ktb_depositamount_base { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount_base { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public Guid xts_purchaseunitid { get; set; }

		[AntiXss]
		public decimal xts_titleregistrationfee_base { get; set; }

		[AntiXss]
		public string xts_purchaserequisitiondetailidname { get; set; }

		[AntiXss]
		public bool xts_closeline { get; set; }

		[AntiXss]
		public Guid xts_productconfigurationid { get; set; }

		[AntiXss]
		public decimal ktb_depositamount { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string xts_closelinename { get; set; }

		[AntiXss]
		public decimal xts_transactionamount { get; set; }

		[AntiXss]
		public decimal xts_withholdingtaxamount_base { get; set; }

		[AntiXss]
		public string ktb_productsap { get; set; }

		[AntiXss]
		public bool ktb_isapproved { get; set; }

		[AntiXss]
		public decimal ktb_ppnamount_base { get; set; }

		[AntiXss]
		public string xts_eventdata { get; set; }

		[AntiXss]
		public DateTime xts_scheduledshippingdate { get; set; }

		[AntiXss]
		public string xts_reasoncodeidname { get; set; }

		[AntiXss]
		public string xts_productstyleidname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
