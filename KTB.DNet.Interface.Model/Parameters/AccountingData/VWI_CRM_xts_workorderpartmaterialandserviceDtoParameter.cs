#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_workorderpartmaterialandserviceParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_workorderpartmaterialandserviceParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public decimal xts_qtytransfer { get; set; }

		[AntiXss]
		public string xts_incomingpdiandserviceinstructionidname { get; set; }

		[AntiXss]
		public string xts_incomingoutsrcworkorderdetailreferenceidname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_consumptiontax1idname { get; set; }

		[AntiXss]
		public decimal xts_qtymanhourrequired { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_dimensionsales9id { get; set; }

		[AntiXss]
		public string ktb_wscinterfacestatusname { get; set; }

		[AntiXss]
		public string xts_maindealeridyominame { get; set; }

		[AntiXss]
		public Guid xts_productcrossreferenceid { get; set; }

		[AntiXss]
		public decimal xts_qtywarranty { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_stockavail { get; set; }

		[AntiXss]
		public Guid xts_withholdingtaxid { get; set; }

		[AntiXss]
		public Guid xts_outsourceworkorderdetailreferenceid { get; set; }

		[AntiXss]
		public string xts_partcodedescription { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xts_commentidname { get; set; }

		[AntiXss]
		public string xts_warehouseidname { get; set; }

		[AntiXss]
		public decimal xts_warrantyclaimamount_base { get; set; }

		[AntiXss]
		public Guid xts_maindealerid { get; set; }

		[AntiXss]
		public decimal xts_transactionamount { get; set; }

		[AntiXss]
		public string xts_commentdescription { get; set; }

		[AntiXss]
		public string xts_siteidname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string ktb_accountreceivableinvoice { get; set; }

		[AntiXss]
		public decimal xts_totalcostlaborcost_base { get; set; }

		[AntiXss]
		public string xts_dimensiontax5idname { get; set; }

		[AntiXss]
		public string xts_billtocustomeridyominame { get; set; }

		[AntiXss]
		public string ktb_consumptiontax1basecalculationname { get; set; }

		[AntiXss]
		public Guid xts_dimensiontax1id { get; set; }

		[AntiXss]
		public string xts_dimensiondisc1idname { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public bool xts_closeline { get; set; }

		[AntiXss]
		public string xts_dimensiontax7idname { get; set; }

		[AntiXss]
		public Guid xts_purchaseorderid { get; set; }

		[AntiXss]
		public decimal xts_withholdingtaxamount { get; set; }

		[AntiXss]
		public string xts_activateworkgroupname { get; set; }

		[AntiXss]
		public string xts_originaloutsrceworkorderdetailreferenceidname { get; set; }

		[AntiXss]
		public Guid xts_outsourceworkshopid { get; set; }

		[AntiXss]
		public string xts_dimensiontax1idname { get; set; }

		[AntiXss]
		public string xts_productsegment3idname { get; set; }

		[AntiXss]
		public decimal ktb_consumption1taxrate { get; set; }

		[AntiXss]
		public string xts_withholdingtax2idname { get; set; }

		[AntiXss]
		public decimal xts_discountamount { get; set; }

		[AntiXss]
		public string xts_servicecategorydescription { get; set; }

		[AntiXss]
		public string xts_dimensioncogs6idname { get; set; }

		[AntiXss]
		public string xts_stockavailname { get; set; }

		[AntiXss]
		public string xts_dimensiontax3idname { get; set; }

		[AntiXss]
		public decimal xts_withholdingtax2amount { get; set; }

		[AntiXss]
		public string ktb_isfulfilledupdatedname { get; set; }

		[AntiXss]
		public Guid xts_productsegment2id { get; set; }

		[AntiXss]
		public Guid xts_dimensiondisc8id { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public string xts_dimensioncogs9idname { get; set; }

		[AntiXss]
		public string xts_dimensioncogs7idname { get; set; }

		[AntiXss]
		public decimal xts_qtymanhouractual { get; set; }

		[AntiXss]
		public int xts_orderdisplay { get; set; }

		[AntiXss]
		public string xts_reservationidname { get; set; }

		[AntiXss]
		public string ktb_isinterfacedname { get; set; }

		[AntiXss]
		public Guid xts_reasonid { get; set; }

		[AntiXss]
		public Guid xts_productsegment3id { get; set; }

		[AntiXss]
		public string bsi_productclassroundingname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public bool ktb_isfulfilledupdated { get; set; }

		[AntiXss]
		public Guid xts_workcodeid { get; set; }

		[AntiXss]
		public Guid xts_dimensiontax5id { get; set; }

		[AntiXss]
		public string xts_workorderidname { get; set; }

		[AntiXss]
		public Guid xts_servicetemplateid { get; set; }

		[AntiXss]
		public decimal xts_baseqtymanhourrequired { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public Guid xts_billtocustomerid { get; set; }

		[AntiXss]
		public string xts_dimensioncogs4idname { get; set; }

		[AntiXss]
		public Guid xts_originaloutsrceworkorderdetailreferenceid { get; set; }

		[AntiXss]
		public string xts_locationidname { get; set; }

		[AntiXss]
		public Guid xts_dimensiondisc1id { get; set; }

		[AntiXss]
		public string xts_billtocustomeridname { get; set; }

		[AntiXss]
		public bool xts_receivingstatus { get; set; }

		[AntiXss]
		public string ktb_vehiclemodelname { get; set; }

		[AntiXss]
		public bool ktb_isfulfilled { get; set; }

		[AntiXss]
		public string ktb_isfulfilledname { get; set; }

		[AntiXss]
		public Guid xts_dimensiontax4id { get; set; }

		[AntiXss]
		public string xts_warrantyclaimnumber { get; set; }

		[AntiXss]
		public string xts_taxaccount1idname { get; set; }

		[AntiXss]
		public Guid xts_productsegment1id { get; set; }

		[AntiXss]
		public Guid xjp_pdidetailreferenceid { get; set; }

		[AntiXss]
		public string xts_purchaseorderlineidname { get; set; }

		[AntiXss]
		public decimal xts_withholdingtax2amount_base { get; set; }

		[AntiXss]
		public Guid xts_unitid { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount { get; set; }

		[AntiXss]
		public Guid xts_incomingpdiandserviceinstructionid { get; set; }

		[AntiXss]
		public decimal xts_unitpricelaborrate_base { get; set; }

		[AntiXss]
		public Guid xts_dimensiontax3id { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public Guid xts_salesunitid { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public string xts_salesaccountidname { get; set; }

		[AntiXss]
		public decimal xts_withholdingtaxamount_base { get; set; }

		[AntiXss]
		public Guid xts_dimensioncogs7id { get; set; }

		[AntiXss]
		public Guid xts_discountaccountid { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount_base { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public Guid xts_dimensiontax2id { get; set; }

		[AntiXss]
		public string xts_producttypename { get; set; }

		[AntiXss]
		public string xts_productsegment1idname { get; set; }

		[AntiXss]
		public decimal xts_qtywarrantyapproved { get; set; }

		[AntiXss]
		public string xts_producttype { get; set; }

		[AntiXss]
		public decimal xts_totaltaxamount { get; set; }

		[AntiXss]
		public string xts_maindealerinvoicegroup { get; set; }

		[AntiXss]
		public Guid xts_locationid { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public Guid xts_dimensiontax9id { get; set; }

		[AntiXss]
		public decimal xts_baseqtymanhouractual { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount_base { get; set; }

		[AntiXss]
		public Guid xts_dimensionsales10id { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount { get; set; }

		[AntiXss]
		public string xts_billabletypename { get; set; }

		[AntiXss]
		public Guid xts_workorderid { get; set; }

		[AntiXss]
		public Guid xts_warehouseid { get; set; }

		[AntiXss]
		public string xts_productsegment2idname { get; set; }

		[AntiXss]
		public string xts_originalworkorderdetailreferenceidname { get; set; }

		[AntiXss]
		public DateTime xts_exchangeratedate { get; set; }

		[AntiXss]
		public Guid xts_dimensioncogs3id { get; set; }

		[AntiXss]
		public decimal xts_qtybackorder { get; set; }

		[AntiXss]
		public string xts_activatefilterproductname { get; set; }

		[AntiXss]
		public Guid bsi_producttypeid { get; set; }

		[AntiXss]
		public Guid xts_dimensiontax8id { get; set; }

		[AntiXss]
		public Guid xts_serviceinstructiondetailreferenceid { get; set; }

		[AntiXss]
		public string xts_dimensioncogs1idname { get; set; }

		[AntiXss]
		public string xts_partidname { get; set; }

		[AntiXss]
		public string ktb_isqtyconfirmedname { get; set; }

		[AntiXss]
		public int xts_runningnumber { get; set; }

		[AntiXss]
		public Guid xts_dimensioncogs10id { get; set; }

		[AntiXss]
		public Guid xts_siteid { get; set; }

		[AntiXss]
		public Guid xts_originalworkorderdetailreferenceid { get; set; }

		[AntiXss]
		public Guid xts_dimensioncogs6id { get; set; }

		[AntiXss]
		public string xts_billabletype { get; set; }

		[AntiXss]
		public decimal xts_baseqtyreturn { get; set; }

		[AntiXss]
		public Guid xts_dimensiondisc5id { get; set; }

		[AntiXss]
		public string xts_nonbillabletypename { get; set; }

		[AntiXss]
		public decimal xts_baseqtyrequest { get; set; }

		[AntiXss]
		public string xts_dimensiondisc6idname { get; set; }

		[AntiXss]
		public Guid xts_dimensiontax6id { get; set; }

		[AntiXss]
		public bool xts_activateworkgroup { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public decimal xts_totalamountbeforediscount_base { get; set; }

		[AntiXss]
		public decimal xts_quantityrequiredvariance { get; set; }

		[AntiXss]
		public string xts_productcrossreferenceidname { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public Guid xts_dimensioncogs4id { get; set; }

		[AntiXss]
		public string xts_dimensiondisc8idname { get; set; }

		[AntiXss]
		public string xts_workorderpartmaterialandservice { get; set; }

		[AntiXss]
		public string xts_dimensiondisc2idname { get; set; }

		[AntiXss]
		public Guid xts_dimensioncogs1id { get; set; }

		[AntiXss]
		public string xts_servicecategoryidname { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public decimal ktb_latestpurchaseprice_base { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_dimensionsales3idname { get; set; }

		[AntiXss]
		public Guid xts_dimensiondisc2id { get; set; }

		[AntiXss]
		public string xts_dimensioncogs8idname { get; set; }

		[AntiXss]
		public Guid xts_productsegment4id { get; set; }

		[AntiXss]
		public Guid xts_dimensiondisc6id { get; set; }

		[AntiXss]
		public string xts_outsource { get; set; }

		[AntiXss]
		public bool ktb_isinterfaced { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xts_purchaseorderidname { get; set; }

		[AntiXss]
		public bool xts_billable { get; set; }

		[AntiXss]
		public string xts_dimensiondisc5idname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public Guid xts_dimensiondisc7id { get; set; }

		[AntiXss]
		public string bsi_producttypeidname { get; set; }

		[AntiXss]
		public Guid xts_dimensionsales4id { get; set; }

		[AntiXss]
		public string xts_dimensioncogs5idname { get; set; }

		[AntiXss]
		public Guid xts_inventoryunitid { get; set; }

		[AntiXss]
		public string xts_dimensionsales7idname { get; set; }

		[AntiXss]
		public string xts_discountaccountidname { get; set; }

		[AntiXss]
		public Guid xts_dimensioncogs5id { get; set; }

		[AntiXss]
		public decimal xts_totalcostlaborcost { get; set; }

		[AntiXss]
		public string xts_dimensionsales4idname { get; set; }

		[AntiXss]
		public Guid xts_originalproductid { get; set; }

		[AntiXss]
		public string xts_reasonidname { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid xts_dimensionsales5id { get; set; }

		[AntiXss]
		public decimal xts_unitpricelaborrate { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string xts_dimensioncogs10idname { get; set; }

		[AntiXss]
		public Guid xts_workorderpartmaterialandserviceid { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public Guid xts_dimensiondisc10id { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax1id { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xjp_pdidetailreferenceidname { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public Guid xts_dimensionsales7id { get; set; }

		[AntiXss]
		public string xts_workcodeidname { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		[AntiXss]
		public Guid xts_dimensioncogs9id { get; set; }

		[AntiXss]
		public decimal xts_discountbaseamount { get; set; }

		[AntiXss]
		public string xts_nonbillabletype { get; set; }

		[AntiXss]
		public decimal xts_qtyreturn { get; set; }

		[AntiXss]
		public string xts_dimensiondisc10idname { get; set; }

		[AntiXss]
		public Guid xts_dimensiondisc3id { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax2id { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount_base { get; set; }

		[AntiXss]
		public Guid xts_dimensionsales3id { get; set; }

		[AntiXss]
		public Guid xts_servicecategoryid { get; set; }

		[AntiXss]
		public string xts_originalproductidname { get; set; }

		[AntiXss]
		public string xts_warranty { get; set; }

		[AntiXss]
		public decimal xts_totalamountbeforediscount { get; set; }

		[AntiXss]
		public Guid xts_reservationid { get; set; }

		[AntiXss]
		public string xts_dimensionsales9idname { get; set; }

		[AntiXss]
		public Guid xts_taxaccount1id { get; set; }

		[AntiXss]
		public Guid xts_incomingoutsrcworkorderdetailreferenceid { get; set; }

		[AntiXss]
		public Guid xts_dimensiondisc4id { get; set; }

		[AntiXss]
		public bool bsi_productclassrounding { get; set; }

		[AntiXss]
		public string xts_dimensioncogs2idname { get; set; }

		[AntiXss]
		public string xts_dimensiontax8idname { get; set; }

		[AntiXss]
		public string xts_outsourceworkshopidname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_salesunitidname { get; set; }

		[AntiXss]
		public decimal xts_warrantyclaimamount { get; set; }

		[AntiXss]
		public Guid xts_taxaccount2id { get; set; }

		[AntiXss]
		public decimal xts_exchangerateamount { get; set; }

		[AntiXss]
		public string xts_dimensioncogs3idname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public Guid xts_dimensionsales1id { get; set; }

		[AntiXss]
		public string xts_warrantyname { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xts_maindealeridname { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_cogsaccountidname { get; set; }

		[AntiXss]
		public decimal xts_qtyrequest { get; set; }

		[AntiXss]
		public string xts_costcenteridname { get; set; }

		[AntiXss]
		public decimal ktb_latestpurchaseprice { get; set; }

		[AntiXss]
		public string xts_dimensiondisc9idname { get; set; }

		[AntiXss]
		public string xts_billablename { get; set; }

		[AntiXss]
		public Guid xts_dimensionsales6id { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public decimal xts_discountpercent { get; set; }

		[AntiXss]
		public string xts_closelinename { get; set; }

		[AntiXss]
		public string xts_dimensionsales10idname { get; set; }

		[AntiXss]
		public decimal xts_totaltransactionamount { get; set; }

		[AntiXss]
		public string ktb_consumptiontax1basecalculation { get; set; }

		[AntiXss]
		public string xts_dimensiondisc3idname { get; set; }

		[AntiXss]
		public string xts_activatefilterproduct { get; set; }

		[AntiXss]
		public string xts_outsourcename { get; set; }

		[AntiXss]
		public Guid xts_dimensioncogs2id { get; set; }

		[AntiXss]
		public decimal xts_discountamount_base { get; set; }

		[AntiXss]
		public string xts_dimensiontax4idname { get; set; }

		[AntiXss]
		public string xts_outsourceworkorderdetailreferenceidname { get; set; }

		[AntiXss]
		public Guid xts_partid { get; set; }

		[AntiXss]
		public string xts_inventoryunitidname { get; set; }

		[AntiXss]
		public Guid xts_costcenterid { get; set; }

		[AntiXss]
		public Guid xts_commentid { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid ktb_accountreceivableinvoiceid { get; set; }

		[AntiXss]
		public decimal xts_baseqtytransfer { get; set; }

		[AntiXss]
		public Guid xts_salesaccountid { get; set; }

		[AntiXss]
		public Guid xts_cogsaccountid { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount { get; set; }

		[AntiXss]
		public string xts_dimensiontax6idname { get; set; }

		[AntiXss]
		public decimal xts_totaltaxamount_base { get; set; }

		[AntiXss]
		public string xts_maindealerinvoicegroupname { get; set; }

		[AntiXss]
		public string xts_dimensionsales1idname { get; set; }

		[AntiXss]
		public Guid xts_dimensioncogs8id { get; set; }

		[AntiXss]
		public string xts_serviceinstructiondetailreferenceidname { get; set; }

		[AntiXss]
		public string ktb_accountreceivableinvoiceidname { get; set; }

		[AntiXss]
		public string xts_taxaccount2idname { get; set; }

		[AntiXss]
		public string xts_withholdingtaxidname { get; set; }

		[AntiXss]
		public string xts_dimensionsales2idname { get; set; }

		[AntiXss]
		public decimal xts_totaltransactionamount_base { get; set; }

		[AntiXss]
		public string xts_unitidname { get; set; }

		[AntiXss]
		public decimal xts_transactionamount_base { get; set; }

		[AntiXss]
		public string xts_consumptiontax2idname { get; set; }

		[AntiXss]
		public Guid xts_withholdingtax2id { get; set; }

		[AntiXss]
		public string xts_dimensiondisc4idname { get; set; }

		[AntiXss]
		public string xts_outsourcevendoridname { get; set; }

		[AntiXss]
		public string xts_dimensiontax2idname { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_dimensionsales5idname { get; set; }

		[AntiXss]
		public decimal xts_discountbaseamount_base { get; set; }

		[AntiXss]
		public Guid xts_dimensionsales2id { get; set; }

		[AntiXss]
		public string xts_receivingstatusname { get; set; }

		[AntiXss]
		public string xts_dimensionsales6idname { get; set; }

		[AntiXss]
		public string xts_servicetemplateidname { get; set; }

		[AntiXss]
		public Guid xts_purchaseorderlineid { get; set; }

		[AntiXss]
		public string xts_dimensiondisc7idname { get; set; }

		[AntiXss]
		public Guid xts_dimensiondisc9id { get; set; }

		[AntiXss]
		public string xts_workorderdetail { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public string xts_dimensionsales8idname { get; set; }

		[AntiXss]
		public string xts_productsegment4idname { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public bool ktb_isqtyconfirmed { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string ktb_wscinterfacestatus { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount { get; set; }

		[AntiXss]
		public string xts_dimensiontax9idname { get; set; }

		[AntiXss]
		public Guid xts_outsourcevendorid { get; set; }

		[AntiXss]
		public decimal xts_exchangerateamount_base { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid xts_dimensiontax7id { get; set; }

		[AntiXss]
		public Guid xts_dimensionsales8id { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }
		[AntiXss]
		public string xts_product { get; set; }

		public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
