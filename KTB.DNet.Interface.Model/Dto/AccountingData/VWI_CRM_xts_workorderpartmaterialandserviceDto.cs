#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_workorderpartmaterialandserviceDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/01/2020 14:19:30
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_workorderpartmaterialandserviceDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public decimal xts_qtytransfer { get; set; }

		public string xts_incomingpdiandserviceinstructionidname { get; set; }

		public string xts_incomingoutsrcworkorderdetailreferenceidname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string xts_consumptiontax1idname { get; set; }

		public decimal xts_qtymanhourrequired { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public Guid xts_dimensionsales9id { get; set; }

		public string ktb_wscinterfacestatusname { get; set; }

		public string xts_maindealeridyominame { get; set; }

		public Guid xts_productcrossreferenceid { get; set; }

		public decimal xts_qtywarranty { get; set; }

		public int importsequencenumber { get; set; }

		public string xts_stockavail { get; set; }

		public Guid xts_withholdingtaxid { get; set; }

		public Guid xts_outsourceworkorderdetailreferenceid { get; set; }

		public string xts_partcodedescription { get; set; }

		public decimal exchangerate { get; set; }

		public string xts_commentidname { get; set; }

		public string xts_warehouseidname { get; set; }

		public decimal xts_warrantyclaimamount_base { get; set; }

		public Guid xts_maindealerid { get; set; }

		public decimal xts_transactionamount { get; set; }

		public string xts_commentdescription { get; set; }

		public string xts_siteidname { get; set; }

		public string statecodename { get; set; }

		public string ktb_accountreceivableinvoice { get; set; }

		public decimal xts_totalcostlaborcost_base { get; set; }

		public string xts_dimensiontax5idname { get; set; }

		public string xts_billtocustomeridyominame { get; set; }

		public string ktb_consumptiontax1basecalculationname { get; set; }

		public Guid xts_dimensiontax1id { get; set; }

		public string xts_dimensiondisc1idname { get; set; }

		public Guid xts_productid { get; set; }

		public bool xts_closeline { get; set; }

		public string xts_dimensiontax7idname { get; set; }

		public Guid xts_purchaseorderid { get; set; }

		public decimal xts_withholdingtaxamount { get; set; }

		public string xts_activateworkgroupname { get; set; }

		public string xts_originaloutsrceworkorderdetailreferenceidname { get; set; }

		public Guid xts_outsourceworkshopid { get; set; }

		public string xts_dimensiontax1idname { get; set; }

		public string xts_productsegment3idname { get; set; }

		public decimal ktb_consumption1taxrate { get; set; }

		public string xts_withholdingtax2idname { get; set; }

		public decimal xts_discountamount { get; set; }

		public string xts_servicecategorydescription { get; set; }

		public string xts_dimensioncogs6idname { get; set; }

		public string xts_stockavailname { get; set; }

		public string xts_dimensiontax3idname { get; set; }

		public decimal xts_withholdingtax2amount { get; set; }

		public string ktb_isfulfilledupdatedname { get; set; }

		public Guid xts_productsegment2id { get; set; }

		public Guid xts_dimensiondisc8id { get; set; }

		public Int64 versionnumber { get; set; }

		public string xts_dimensioncogs9idname { get; set; }

		public string xts_dimensioncogs7idname { get; set; }

		public decimal xts_qtymanhouractual { get; set; }

		public int xts_orderdisplay { get; set; }

		public string xts_reservationidname { get; set; }

		public string ktb_isinterfacedname { get; set; }

		public Guid xts_reasonid { get; set; }

		public Guid xts_productsegment3id { get; set; }

		public string bsi_productclassroundingname { get; set; }

		public string createdbyyominame { get; set; }

		public bool ktb_isfulfilledupdated { get; set; }

		public Guid xts_workcodeid { get; set; }

		public Guid xts_dimensiontax5id { get; set; }

		public string xts_workorderidname { get; set; }

		public Guid xts_servicetemplateid { get; set; }

		public decimal xts_baseqtymanhourrequired { get; set; }

		public Guid owningbusinessunit { get; set; }

		public Guid xts_billtocustomerid { get; set; }

		public string xts_dimensioncogs4idname { get; set; }

		public Guid xts_originaloutsrceworkorderdetailreferenceid { get; set; }

		public string xts_locationidname { get; set; }

		public Guid xts_dimensiondisc1id { get; set; }

		public string xts_billtocustomeridname { get; set; }

		public bool xts_receivingstatus { get; set; }

		public string ktb_vehiclemodelname { get; set; }

		public bool ktb_isfulfilled { get; set; }

		public string ktb_isfulfilledname { get; set; }

		public Guid xts_dimensiontax4id { get; set; }

		public string xts_warrantyclaimnumber { get; set; }

		public string xts_taxaccount1idname { get; set; }

		public Guid xts_productsegment1id { get; set; }

		public Guid xjp_pdidetailreferenceid { get; set; }

		public string xts_purchaseorderlineidname { get; set; }

		public decimal xts_withholdingtax2amount_base { get; set; }

		public Guid xts_unitid { get; set; }

		public decimal xts_consumptiontax1amount { get; set; }

		public Guid xts_incomingpdiandserviceinstructionid { get; set; }

		public decimal xts_unitpricelaborrate_base { get; set; }

		public Guid xts_dimensiontax3id { get; set; }

		public string createdonbehalfbyname { get; set; }

		public Guid xts_salesunitid { get; set; }

		public string xts_productdescription { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public string xts_salesaccountidname { get; set; }

		public decimal xts_withholdingtaxamount_base { get; set; }

		public Guid xts_dimensioncogs7id { get; set; }

		public Guid xts_discountaccountid { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public decimal xts_totalbaseamount_base { get; set; }

		public string owneridname { get; set; }

		public Guid xts_dimensiontax2id { get; set; }

		public string xts_producttypename { get; set; }

		public string xts_productsegment1idname { get; set; }

		public decimal xts_qtywarrantyapproved { get; set; }

		public string xts_producttype { get; set; }

		public decimal xts_totaltaxamount { get; set; }

		public string xts_maindealerinvoicegroup { get; set; }

		public Guid xts_locationid { get; set; }

		public string xts_productidname { get; set; }

		public Guid xts_dimensiontax9id { get; set; }

		public decimal xts_baseqtymanhouractual { get; set; }

		public decimal xts_consumptiontax2amount_base { get; set; }

		public Guid xts_dimensionsales10id { get; set; }

		public decimal xts_consumptiontax2amount { get; set; }

		public string xts_billabletypename { get; set; }

		public Guid xts_workorderid { get; set; }

		public Guid xts_warehouseid { get; set; }

		public string xts_productsegment2idname { get; set; }

		public string xts_originalworkorderdetailreferenceidname { get; set; }

		public DateTime xts_exchangeratedate { get; set; }

		public Guid xts_dimensioncogs3id { get; set; }

		public decimal xts_qtybackorder { get; set; }

		public string xts_activatefilterproductname { get; set; }

		public Guid bsi_producttypeid { get; set; }

		public Guid xts_dimensiontax8id { get; set; }

		public Guid xts_serviceinstructiondetailreferenceid { get; set; }

		public string xts_dimensioncogs1idname { get; set; }

		public string xts_partidname { get; set; }

		public string ktb_isqtyconfirmedname { get; set; }

		public int xts_runningnumber { get; set; }

		public Guid xts_dimensioncogs10id { get; set; }

		public Guid xts_siteid { get; set; }

		public Guid xts_originalworkorderdetailreferenceid { get; set; }

		public Guid xts_dimensioncogs6id { get; set; }

		public string xts_billabletype { get; set; }

		public decimal xts_baseqtyreturn { get; set; }

		public Guid xts_dimensiondisc5id { get; set; }

		public string xts_nonbillabletypename { get; set; }

		public decimal xts_baseqtyrequest { get; set; }

		public string xts_dimensiondisc6idname { get; set; }

		public Guid xts_dimensiontax6id { get; set; }

		public bool xts_activateworkgroup { get; set; }

		public DateTime createdon { get; set; }

		public decimal xts_totalamountbeforediscount_base { get; set; }

		public decimal xts_quantityrequiredvariance { get; set; }

		public string xts_productcrossreferenceidname { get; set; }

		public string xts_description { get; set; }

		public Guid xts_dimensioncogs4id { get; set; }

		public string xts_dimensiondisc8idname { get; set; }

		public string xts_workorderpartmaterialandservice { get; set; }

		public string xts_dimensiondisc2idname { get; set; }

		public Guid xts_dimensioncogs1id { get; set; }

		public string xts_servicecategoryidname { get; set; }

		public string xts_locking { get; set; }

		public decimal ktb_latestpurchaseprice_base { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string xts_dimensionsales3idname { get; set; }

		public Guid xts_dimensiondisc2id { get; set; }

		public string xts_dimensioncogs8idname { get; set; }

		public Guid xts_productsegment4id { get; set; }

		public Guid xts_dimensiondisc6id { get; set; }

		public string xts_outsource { get; set; }

		public bool ktb_isinterfaced { get; set; }

		public string createdbyname { get; set; }

		public string xts_purchaseorderidname { get; set; }

		public bool xts_billable { get; set; }

		public string xts_dimensiondisc5idname { get; set; }

		public int statecode { get; set; }

		public int statuscode { get; set; }

		public Guid xts_dimensiondisc7id { get; set; }

		public string bsi_producttypeidname { get; set; }

		public Guid xts_dimensionsales4id { get; set; }

		public string xts_dimensioncogs5idname { get; set; }

		public Guid xts_inventoryunitid { get; set; }

		public string xts_dimensionsales7idname { get; set; }

		public string xts_discountaccountidname { get; set; }

		public Guid xts_dimensioncogs5id { get; set; }

		public decimal xts_totalcostlaborcost { get; set; }

		public string xts_dimensionsales4idname { get; set; }

		public Guid xts_originalproductid { get; set; }

		public string xts_reasonidname { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public Guid xts_dimensionsales5id { get; set; }

		public decimal xts_unitpricelaborrate { get; set; }

		public string statuscodename { get; set; }

		public Guid owninguser { get; set; }

		public string xts_dimensioncogs10idname { get; set; }

		public Guid xts_workorderpartmaterialandserviceid { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public Guid xts_dimensiondisc10id { get; set; }

		public string modifiedbyname { get; set; }

		public Guid xts_consumptiontax1id { get; set; }

		public string owneridtype { get; set; }

		public string xjp_pdidetailreferenceidname { get; set; }

		public Guid xts_businessunitid { get; set; }

		public Guid xts_dimensionsales7id { get; set; }

		public string xts_workcodeidname { get; set; }

		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		public Guid xts_dimensioncogs9id { get; set; }

		public decimal xts_discountbaseamount { get; set; }

		public string xts_nonbillabletype { get; set; }

		public decimal xts_qtyreturn { get; set; }

		public string xts_dimensiondisc10idname { get; set; }

		public Guid xts_dimensiondisc3id { get; set; }

		public Guid xts_consumptiontax2id { get; set; }

		public decimal xts_consumptiontax1amount_base { get; set; }

		public Guid xts_dimensionsales3id { get; set; }

		public Guid xts_servicecategoryid { get; set; }

		public string xts_originalproductidname { get; set; }

		public string xts_warranty { get; set; }

		public decimal xts_totalamountbeforediscount { get; set; }

		public Guid xts_reservationid { get; set; }

		public string xts_dimensionsales9idname { get; set; }

		public Guid xts_taxaccount1id { get; set; }

		public Guid xts_incomingoutsrcworkorderdetailreferenceid { get; set; }

		public Guid xts_dimensiondisc4id { get; set; }

		public bool bsi_productclassrounding { get; set; }

		public string xts_dimensioncogs2idname { get; set; }

		public string xts_dimensiontax8idname { get; set; }

		public string xts_outsourceworkshopidname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string xts_salesunitidname { get; set; }

		public decimal xts_warrantyclaimamount { get; set; }

		public Guid xts_taxaccount2id { get; set; }

		public decimal xts_exchangerateamount { get; set; }

		public string xts_dimensioncogs3idname { get; set; }

		public Guid ownerid { get; set; }

		public Guid xts_dimensionsales1id { get; set; }

		public string xts_warrantyname { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string xts_maindealeridname { get; set; }

		public Guid owningteam { get; set; }

		public string xts_cogsaccountidname { get; set; }

		public decimal xts_qtyrequest { get; set; }

		public string xts_costcenteridname { get; set; }

		public decimal ktb_latestpurchaseprice { get; set; }

		public string xts_dimensiondisc9idname { get; set; }

		public string xts_billablename { get; set; }

		public Guid xts_dimensionsales6id { get; set; }

		public string owneridyominame { get; set; }

		public decimal xts_discountpercent { get; set; }

		public string xts_closelinename { get; set; }

		public string xts_dimensionsales10idname { get; set; }

		public decimal xts_totaltransactionamount { get; set; }

		public string ktb_consumptiontax1basecalculation { get; set; }

		public string xts_dimensiondisc3idname { get; set; }

		public string xts_activatefilterproduct { get; set; }

		public string xts_outsourcename { get; set; }

		public Guid xts_dimensioncogs2id { get; set; }

		public decimal xts_discountamount_base { get; set; }

		public string xts_dimensiontax4idname { get; set; }

		public string xts_outsourceworkorderdetailreferenceidname { get; set; }

		public Guid xts_partid { get; set; }

		public string xts_inventoryunitidname { get; set; }

		public Guid xts_costcenterid { get; set; }

		public Guid xts_commentid { get; set; }

		public Guid modifiedby { get; set; }

		public Guid ktb_accountreceivableinvoiceid { get; set; }

		public decimal xts_baseqtytransfer { get; set; }

		public Guid xts_salesaccountid { get; set; }

		public Guid xts_cogsaccountid { get; set; }

		public decimal xts_totalconsumptiontaxamount { get; set; }

		public string xts_dimensiontax6idname { get; set; }

		public decimal xts_totaltaxamount_base { get; set; }

		public string xts_maindealerinvoicegroupname { get; set; }

		public string xts_dimensionsales1idname { get; set; }

		public Guid xts_dimensioncogs8id { get; set; }

		public string xts_serviceinstructiondetailreferenceidname { get; set; }

		public string ktb_accountreceivableinvoiceidname { get; set; }

		public string xts_taxaccount2idname { get; set; }

		public string xts_withholdingtaxidname { get; set; }

		public string xts_dimensionsales2idname { get; set; }

		public decimal xts_totaltransactionamount_base { get; set; }

		public string xts_unitidname { get; set; }

		public decimal xts_transactionamount_base { get; set; }

		public string xts_consumptiontax2idname { get; set; }

		public Guid xts_withholdingtax2id { get; set; }

		public string xts_dimensiondisc4idname { get; set; }

		public string xts_outsourcevendoridname { get; set; }

		public string xts_dimensiontax2idname { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string xts_dimensionsales5idname { get; set; }

		public decimal xts_discountbaseamount_base { get; set; }

		public Guid xts_dimensionsales2id { get; set; }

		public string xts_receivingstatusname { get; set; }

		public string xts_dimensionsales6idname { get; set; }

		public string xts_servicetemplateidname { get; set; }

		public Guid xts_purchaseorderlineid { get; set; }

		public string xts_dimensiondisc7idname { get; set; }

		public Guid xts_dimensiondisc9id { get; set; }

		public string xts_workorderdetail { get; set; }

		public DateTime modifiedon { get; set; }

		public string xts_dimensionsales8idname { get; set; }

		public string xts_productsegment4idname { get; set; }

		public string xts_businessunitidname { get; set; }

		public bool ktb_isqtyconfirmed { get; set; }

		public string modifiedbyyominame { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string ktb_wscinterfacestatus { get; set; }

		public decimal xts_totalbaseamount { get; set; }

		public string xts_dimensiontax9idname { get; set; }

		public Guid xts_outsourcevendorid { get; set; }

		public decimal xts_exchangerateamount_base { get; set; }

		public Guid createdby { get; set; }

		public Guid xts_dimensiontax7id { get; set; }

		public Guid xts_dimensionsales8id { get; set; }

		public string msdyn_companycode { get; set; }

		public string xts_product { get; set; }

		public string ktb_externalcode { get; set; }

	}
}
