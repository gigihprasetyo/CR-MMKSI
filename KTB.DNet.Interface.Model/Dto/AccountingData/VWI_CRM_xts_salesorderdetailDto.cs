#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_salesorderdetailDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:16:03
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_salesorderdetailDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public decimal xts_discountpercentage { get; set; }

		public Guid xts_exteriorcolorid { get; set; }

		public Guid xts_consumptiontax2id { get; set; }

		public Guid xts_consumptiontax1id { get; set; }

		public decimal xts_quantityreturnedbaseunit { get; set; }

		public Guid xts_originalproductid { get; set; }

		public string ktb_isinterfacename { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public Guid xts_salesunitid { get; set; }

		public decimal xts_totalamount { get; set; }

		public string statuscodename { get; set; }

		public string xts_status { get; set; }

		public decimal xts_unitprice_base { get; set; }

		public bool ktb_isinterface { get; set; }

		public decimal xts_totalamount_base { get; set; }

		public string modifiedbyyominame { get; set; }

		public decimal bsi_maximumprice { get; set; }

		public string transactioncurrencyidname { get; set; }

		public decimal xts_transactionamount_base { get; set; }

		public decimal xts_discountamount_base { get; set; }

		public Guid owningbusinessunit { get; set; }

		public Guid xts_warehouseavailabilityid { get; set; }

		public string xts_productcrossreferenceidname { get; set; }

		public decimal xts_amountbeforediscount_base { get; set; }

		public decimal xts_quantitydeliveredbaseunit { get; set; }

		public Guid xts_locationid { get; set; }

		public int statuscode { get; set; }

		public decimal xts_consumptiontax1amount { get; set; }

		public string xts_productdescription { get; set; }

		public string xts_warehouseidname { get; set; }

		public decimal xts_quantityorderbaseunit { get; set; }

		public Guid xts_salesorderid { get; set; }

		public Guid xts_interiorcolorid { get; set; }

		public string xts_configurationidname { get; set; }

		public string xts_consumptiontax1idname { get; set; }

		public Int64 versionnumber { get; set; }

		public string xts_warehouseavailabilityidname { get; set; }

		public string statecodename { get; set; }

		public string xts_closereason { get; set; }

		public decimal exchangerate { get; set; }

		public DateTime modifiedon { get; set; }

		public DateTime xts_requestdate { get; set; }

		public Guid xts_withholdingtax2id { get; set; }

		public string xts_businessunitidname { get; set; }

		public DateTime xts_promisedate { get; set; }

		public DateTime createdon { get; set; }

		public string xts_withholdingtaxidname { get; set; }

		public decimal ktb_cogs { get; set; }

		public decimal ktb_baseamountimp { get; set; }

		public string ktb_sotype { get; set; }

		public string xts_statusname { get; set; }

		public decimal xts_withholdingtax2amount_base { get; set; }

		public string xts_inventoryunitidname { get; set; }

		public string bsi_category { get; set; }

		public decimal xts_totalconsumptiontaxamount { get; set; }

		public DateTime ktb_latestpurchasepricedate { get; set; }

		public decimal xts_quantityorder { get; set; }

		public Guid xts_productid { get; set; }

		public string owneridyominame { get; set; }

		public string xts_interiorcoloridname { get; set; }

		public decimal xts_baseamount { get; set; }

		public Guid modifiedby { get; set; }

		public string xts_salesorderidname { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal xts_consumptiontax1amount_base { get; set; }

		public Guid xts_withholdingtaxid { get; set; }

		public string xts_stockavail { get; set; }

		public string ktb_sotypename { get; set; }

		public Guid owningteam { get; set; }

		public decimal xts_discountamount { get; set; }

		public string createdonbehalfbyname { get; set; }

		public Guid owninguser { get; set; }

		public string owneridtype { get; set; }

		public string xts_salesorderdetailnumber { get; set; }

		public string xts_salesunitidname { get; set; }

		public decimal bsi_maximumprice_base { get; set; }

		public string owneridname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		public decimal xts_amountbeforediscount { get; set; }

		public decimal xts_qtyrequest { get; set; }

		public decimal xts_withholdingtax2amount { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public decimal xts_quantitydelivered { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public Guid xts_siteid { get; set; }

		public string xts_withholdingtax2idname { get; set; }

		public Guid createdonbehalfby { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string xts_eventdata { get; set; }

		public decimal xts_baseqtyrequest { get; set; }

		public Guid xts_styleid { get; set; }

		public string xts_productidname { get; set; }

		public string xts_stockavailname { get; set; }

		public Guid xts_configurationid { get; set; }

		public decimal xts_consumptiontax2amount { get; set; }

		public Guid xts_warehouseid { get; set; }

		public string xts_originalproductidname { get; set; }

		public decimal xts_discountbaseamount_base { get; set; }

		public Guid ownerid { get; set; }

		public string xts_exteriorcoloridname { get; set; }

		public Guid xts_salesorderdetailid { get; set; }

		public string bsi_iseditname { get; set; }

		public string createdbyname { get; set; }

		public decimal xts_quantityreturned { get; set; }

		public int statecode { get; set; }

		public string xts_siteidname { get; set; }

		public string xts_locationidname { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public Guid createdby { get; set; }

		public bool bsi_isedit { get; set; }

		public string bsi_categoryname { get; set; }

		public decimal ktb_markuppercentage { get; set; }

		public Guid xts_inventoryunitid { get; set; }

		public decimal xts_unitprice { get; set; }

		public decimal xts_discountbaseamount { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string xts_completedname { get; set; }

		public decimal bsi_originalprice { get; set; }

		public bool xts_completed { get; set; }

		public decimal bsi_originalprice_base { get; set; }

		public int importsequencenumber { get; set; }

		public string xts_locking { get; set; }

		public string xts_styleidname { get; set; }

		public string xts_consumptiontax2idname { get; set; }

		public Guid xts_productcrossreferenceid { get; set; }

		public decimal xts_consumptiontax2amount_base { get; set; }

		public string ktb_modelcode { get; set; }

		public decimal ktb_baseamountimp_base { get; set; }

		public Guid xts_businessunitid { get; set; }

		public decimal ktb_cogs_base { get; set; }

		public bool xts_closeline { get; set; }

		public string modifiedbyname { get; set; }

		public string createdbyyominame { get; set; }

		public string xts_closelinename { get; set; }

		public decimal xts_transactionamount { get; set; }

		public decimal xts_baseamount_base { get; set; }

		public decimal xts_withholdingtaxamount_base { get; set; }

		public decimal xts_withholdingtaxamount { get; set; }

		public int xts_runningnumber { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
