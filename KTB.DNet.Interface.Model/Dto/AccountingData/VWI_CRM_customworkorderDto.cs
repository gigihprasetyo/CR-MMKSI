#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_customworkorderDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/01/2020 8:22:47
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
	public class VWI_CRM_customworkorderDto : DtoBase
	{
		public string company { get; set; }
		public string businessunitcode { get; set; }
		public Guid xts_contactpersonid { get; set; }
		public Guid xts_vehiclesizeclassid { get; set; }
		public int ktb_countedprint { get; set; }
		public DateTime xts_actualservicestartdateandtime { get; set; }
		public decimal xts_totalpartsamount { get; set; }
		public decimal xts_esttotaltaxamount_base { get; set; }
		public string ktb_vehiclestatename { get; set; }
		public decimal xts_esttotalothersalestaxamount_base { get; set; }
		public decimal xts_grandtotalamount_base { get; set; }
		public string ktb_say { get; set; }
		public decimal xts_esttotalworktaxamount_base { get; set; }
		public decimal xts_estgrandtotalamount { get; set; }
		public int xts_lastmileage { get; set; }
		public string ktb_svcincominginterfacestatusname { get; set; }
		public string xts_billtocustomeridyominame { get; set; }
		public bool xts_invoiced { get; set; }
		public decimal xts_totalpartstaxamount_base { get; set; }
		public decimal xts_totalcostamount_base { get; set; }
		public string xts_workorder { get; set; }
		public string ktb_workordermemo { get; set; }
		public DateTime xts_scheduledservicestartdateandtime { get; set; }
		public decimal xts_estgrandtotalamount_base { get; set; }
		public string xts_handling { get; set; }
		public int xts_originaloutsorcereferencelookuptype { get; set; }
		public decimal xts_totalothersalesnotallocated_base { get; set; }
		public Int64 versionnumber { get; set; }
		public decimal xts_grandtotalnotallocated { get; set; }
		public string xts_queuestatus { get; set; }
		public Guid xjp_originaloutsorcereferencepdiid { get; set; }
		public string ktb_saleschannelidname { get; set; }
		public decimal xts_esttotalpartstaxamount_base { get; set; }
		public Guid xts_productsegment3id { get; set; }
		public DateTime ktb_wodate { get; set; }
		public Guid xts_billtocustomerid { get; set; }
		public decimal xts_totalpaymentamount_base { get; set; }
		public string xts_billtocustomeridname { get; set; }
		public string xts_customernumber { get; set; }
		public decimal xts_esttotalpartstaxamount { get; set; }
		public Guid xts_productsegment1id { get; set; }
		public string ktb_liststatusinterface { get; set; }
		public string ktb_ordertypetranstype { get; set; }
		public string xts_workorderstatus { get; set; }
		public decimal ktb_totalpartamount { get; set; }
		public DateTime xts_actualservicefinishdateandtime { get; set; }
		public string xts_productdescription { get; set; }
		public Guid transactioncurrencyid { get; set; }
		public decimal xts_totalbaseamount_base { get; set; }
		public decimal xts_esttotaltaxamount { get; set; }
		public Guid xts_originaloutsorcereferenceoutsourcewoid { get; set; }
		public decimal xts_totalmiscchargebasenotallocated { get; set; }
		public bool xts_isworkorderform { get; set; }
		public decimal xts_esttotalpartsamount { get; set; }
		public decimal xts_esttotalpartsbaseamount_base { get; set; }
		public decimal xts_totalwithholdingtaxnotallocated { get; set; }
		public string xts_dimension10idname { get; set; }
		public decimal xts_totaltaxamount { get; set; }
		public decimal exchangerate { get; set; }
		public decimal xts_balance { get; set; }
		public string modifiedonbehalfbyyominame { get; set; }
		public string xts_pickupaddress1 { get; set; }
		public decimal xts_totalpartsbaseallocated { get; set; }
		public Guid xts_workorderid { get; set; }
		public decimal xts_totalpartstaxnotallocated_base { get; set; }
		public decimal xts_totalmiscchargeallocated_base { get; set; }
		public string xts_claimapprovalnumber { get; set; }
		public decimal xts_totalpartsbaseamount { get; set; }
		public decimal xts_grandtotalnotallocated_base { get; set; }
		public string ktb_paneldescription { get; set; }
		public decimal xts_downpaymentamount { get; set; }
		public Guid xts_serviceadvisorid { get; set; }
		public Guid xts_siteid { get; set; }
		public bool ktb_validatorhandling { get; set; }
		public string xts_contactpersonphone { get; set; }
		public string xts_dimension8idname { get; set; }
		public bool ktb_isinsurance { get; set; }
		public DateTime createdon { get; set; }
		public DateTime xts_invoicepostingdate { get; set; }
		public decimal xts_totalworktaxamount { get; set; }
		public decimal xts_totalothersalesallocated { get; set; }
		public decimal ktb_totalserviceamount { get; set; }
		public string xts_deliveryaddress4 { get; set; }
		public string xts_servicecategoryidname { get; set; }
		public int xts_currentmileage { get; set; }
		public string xts_claimstatus { get; set; }
		public decimal xts_totalpartsbasenotallocated { get; set; }
		public string xts_chassisnumber { get; set; }
		public decimal xts_totalothersalestaxamount { get; set; }
		public decimal xts_totalpartstaxnotallocated { get; set; }
		public bool ktb_isinterfaced { get; set; }
		public int xts_historymileage { get; set; }
		public decimal xts_totalworkbasenotallocated_base { get; set; }
		public decimal xts_grandtotalamount { get; set; }
		public int statecode { get; set; }
		public int statuscode { get; set; }
		public decimal xts_totalothersalesbaseamount { get; set; }
		public decimal xts_totalpartstaxamount { get; set; }
		public decimal xts_totalworkamount { get; set; }
		public DateTime xts_scheduledarrivaldateandtime { get; set; }
		public string ktb_statusinterface { get; set; }
		public DateTime xts_transactiondate { get; set; }
		public Guid owninguser { get; set; }
		public bool ktb_isfreebilltocustomer { get; set; }
		public decimal xts_totalmiscchargetaxnotallocated_base { get; set; }
		public decimal xts_esttotalbaseamount_base { get; set; }
		public decimal xts_totalpartsamount_base { get; set; }
		public string xts_downpaymentispaidname { get; set; }
		public string ktb_contactmobilephonenumber { get; set; }
		public Guid xts_vehicleidentificationid { get; set; }
		public decimal xts_totalpartsnotallocated_base { get; set; }
		public decimal xts_totalpartstaxallocated { get; set; }
		public decimal xts_esttotalbaseamount { get; set; }
		public Guid xts_exchangeratetypeid { get; set; }
		public DateTime xts_scheduledservicefinishdateandtime { get; set; }
		public string ktb_workorderdescription { get; set; }
		public decimal xts_totalpartsbaseamount_base { get; set; }
		public decimal xts_esttotalothersalesbaseamount { get; set; }
		public string xts_manufacturername { get; set; }
		public Guid xts_parentbusinessunitid { get; set; }
		public string xts_deliveryaddress1 { get; set; }
		public decimal xts_esttotalpartsbaseamount { get; set; }
		public decimal xts_totalothersalesnotallocated { get; set; }
		public decimal xts_totaltaxamount_base { get; set; }
		public decimal xts_totalpartsnotallocated { get; set; }
		public string xts_referralname { get; set; }
		public decimal xts_totalcostamount { get; set; }
		public decimal ktb_totaloilamount { get; set; }
		public int utcconversiontimezonecode { get; set; }
		public decimal xts_totalpartsbasenotallocated_base { get; set; }
		public Guid ktb_contactid { get; set; }
		public DateTime modifiedon { get; set; }
		public DateTime xts_scheduledfinishdateandtime { get; set; }
		public Guid xts_dimension2id { get; set; }
		public decimal xts_totalbaseamount { get; set; }
		public string ktb_dnetidwotype { get; set; }
		public string xts_originaloutsorcereferencesvcinstructionidname { get; set; }
		public decimal xts_esttotalpartsamount_base { get; set; }
		public decimal ktb_grandtotalinformationdetail { get; set; }
		public string msdyn_companycode { get; set; }
	}
}
