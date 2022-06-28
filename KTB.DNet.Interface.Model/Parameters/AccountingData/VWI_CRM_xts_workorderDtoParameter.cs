#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_workorderParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/01/2020 14:19:29
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
    public class VWI_CRM_xts_workorderParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargeamount { get; set; }

		[AntiXss]
		public Guid xts_contactpersonid { get; set; }

		[AntiXss]
		public string ktb_statusinterfaceassistpartsalesdnet { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargebaseallocated { get; set; }

		[AntiXss]
		public Guid xts_vehiclesizeclassid { get; set; }

		[AntiXss]
		public string ktb_isinterfacedname { get; set; }

		[AntiXss]
		public decimal xts_esttotalothersalesbaseamount_base { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public int ktb_countedprint { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public DateTime xts_actualservicestartdateandtime { get; set; }

		[AntiXss]
		public string xts_invoicedname { get; set; }

		[AntiXss]
		public Guid xts_dimension5id { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesbaseallocated_base { get; set; }

		[AntiXss]
		public string ktb_isprintgatepassreadytobeinvoicename { get; set; }

		[AntiXss]
		public Guid xts_symptomcauseid { get; set; }

		[AntiXss]
		public string ktb_isfreebilltocustomername { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_claimstatusname { get; set; }

		[AntiXss]
		public string xts_pickupaddress4 { get; set; }

		[AntiXss]
		public string xts_queuestatusname { get; set; }

		[AntiXss]
		public string xts_workordermemo { get; set; }

		[AntiXss]
		public string ktb_statusinterfacefreeservicednet { get; set; }

		[AntiXss]
		public decimal xjp_estcaliamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalpartsamount { get; set; }

		[AntiXss]
		public decimal xts_esttotaltaxamount_base { get; set; }

		[AntiXss]
		public string ktb_vehiclestatename { get; set; }

		[AntiXss]
		public decimal xts_esttotalothersalestaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_grandtotalamount_base { get; set; }

		[AntiXss]
		public string ktb_say { get; set; }

		[AntiXss]
		public decimal xts_esttotalworktaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargebaseamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalpartsallocated_base { get; set; }

		[AntiXss]
		public string xts_woinsurancereceiptnumber { get; set; }

		[AntiXss]
		public decimal xts_esttotalothersalestaxamount { get; set; }

		[AntiXss]
		public string xts_incomingoutsourcebusinessunitidname { get; set; }

		[AntiXss]
		public string xts_siteidname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public decimal xts_paymentreceivedamount_base { get; set; }

		[AntiXss]
		public decimal xts_estgrandtotalamount { get; set; }

		[AntiXss]
		public int xts_lastmileage { get; set; }

		[AntiXss]
		public string ktb_svcincominginterfacestatusname { get; set; }

		[AntiXss]
		public string xts_billtocustomeridyominame { get; set; }

		[AntiXss]
		public string ktb_origworkordernumber { get; set; }

		[AntiXss]
		public string xts_dimension5idname { get; set; }

		[AntiXss]
		public Guid xts_originaloutsorcereferencesvcinstructionid { get; set; }

		[AntiXss]
		public string xts_araccountidname { get; set; }

		[AntiXss]
		public bool xts_invoiced { get; set; }

		[AntiXss]
		public string xts_deliverypostalcode { get; set; }

		[AntiXss]
		public string xts_originalworkorderreferencenumberidname { get; set; }

		[AntiXss]
		public Guid ktb_paneltypeid { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public decimal xts_totalpartstaxamount_base { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public decimal xts_totalcostamount_base { get; set; }

		[AntiXss]
		public decimal xjp_caliallocated { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargenotallocated { get; set; }

		[AntiXss]
		public string xts_customeridyominame { get; set; }

		[AntiXss]
		public decimal xts_totalpartsbaseallocated_base { get; set; }

		[AntiXss]
		public string xts_servicepersoninchargeidname { get; set; }

		[AntiXss]
		public decimal xts_totalworktaxnotallocated_base { get; set; }

		[AntiXss]
		public string xts_workorder { get; set; }

		[AntiXss]
		public string xts_workgroupidname { get; set; }

		[AntiXss]
		public decimal xts_totalworktaxallocated_base { get; set; }

		[AntiXss]
		public string ktb_workordermemo { get; set; }

		[AntiXss]
		public string ktb_svcincominginterfacestatus { get; set; }

		[AntiXss]
		public string xts_pickuppostalcode { get; set; }

		[AntiXss]
		public string xts_vehiclemodelname { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public Guid xts_globalserviceid { get; set; }

		[AntiXss]
		public string xts_productsegment3idname { get; set; }

		[AntiXss]
		public DateTime xts_scheduledservicestartdateandtime { get; set; }

		[AntiXss]
		public int ktb_countprintinvoice { get; set; }

		[AntiXss]
		public bool xts_vehicleincludedintherecall { get; set; }

		[AntiXss]
		public Guid ktb_servicetypeid { get; set; }

		[AntiXss]
		public string xts_warrantycontractavailablename { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargebaseamount { get; set; }

		[AntiXss]
		public string ktb_contactidname { get; set; }

		[AntiXss]
		public bool ktb_isprintgatepassreadytobeinvoice { get; set; }

		[AntiXss]
		public decimal xts_totalworkbaseallocated { get; set; }

		[AntiXss]
		public string xts_servicecategorydescription { get; set; }

		[AntiXss]
		public string xts_originaloutsorcereferenceoutsourcewoidname { get; set; }

		[AntiXss]
		public decimal xts_estgrandtotalamount_base { get; set; }

		[AntiXss]
		public Guid xts_loadinggroupid { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public string ktb_statusinterfaceregcheckdnetname { get; set; }

		[AntiXss]
		public int xts_originaloutsorcereferencelookuptype { get; set; }

		[AntiXss]
		public Guid xts_productsegment2id { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesnotallocated_base { get; set; }

		[AntiXss]
		public Guid xts_incomingoutsourcebusinessunitid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid xts_originalcustomerid { get; set; }

		[AntiXss]
		public decimal xts_grandtotalnotallocated { get; set; }

		[AntiXss]
		public string xts_queuestatus { get; set; }

		[AntiXss]
		public Guid xjp_originaloutsorcereferencepdiid { get; set; }

		[AntiXss]
		public string ktb_saleschannelidname { get; set; }

		[AntiXss]
		public string xts_reservationidname { get; set; }

		[AntiXss]
		public decimal ktb_totalpartamount_base { get; set; }

		[AntiXss]
		public decimal xts_reservationmanhour { get; set; }

		[AntiXss]
		public string xts_referencenumberlookupname { get; set; }

		[AntiXss]
		public string xts_symptomcauseidname { get; set; }

		[AntiXss]
		public decimal xts_esttotalpartstaxamount_base { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public Guid xts_productsegment3id { get; set; }

		[AntiXss]
		public decimal xts_esttotalwithholdingtaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesamount { get; set; }

		[AntiXss]
		public DateTime ktb_wodate { get; set; }

		[AntiXss]
		public string xts_originaloutsorcereferencelookupname { get; set; }

		[AntiXss]
		public string ktb_panelcategoryname { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamountreceived { get; set; }

		[AntiXss]
		public Guid xts_servicetemplateid { get; set; }

		[AntiXss]
		public Guid xts_billtocustomerid { get; set; }

		[AntiXss]
		public string xts_platenumber { get; set; }

		[AntiXss]
		public decimal xts_totalpaymentamount_base { get; set; }

		[AntiXss]
		public string xts_billtocustomeridname { get; set; }

		[AntiXss]
		public decimal xts_totalworkamount_base { get; set; }

		[AntiXss]
		public string xts_isworkorderformname { get; set; }

		[AntiXss]
		public decimal xjp_caliamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesamount_base { get; set; }

		[AntiXss]
		public decimal xjp_calinotallocated_base { get; set; }

		[AntiXss]
		public string xts_customernumber { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargenotallocated_base { get; set; }

		[AntiXss]
		public string ktb_eventdata { get; set; }

		[AntiXss]
		public decimal xts_esttotalpartstaxamount { get; set; }

		[AntiXss]
		public DateTime xjp_caliexpirationdate { get; set; }

		[AntiXss]
		public Guid xts_productsegment1id { get; set; }

		[AntiXss]
		public string ktb_liststatusinterface { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_manufactureridname { get; set; }

		[AntiXss]
		public string ktb_ordertypetranstype { get; set; }

		[AntiXss]
		public string xts_workorderstatus { get; set; }

		[AntiXss]
		public decimal ktb_totalpartamount { get; set; }

		[AntiXss]
		public DateTime xts_actualservicefinishdateandtime { get; set; }

		[AntiXss]
		public string xts_dimension6idname { get; set; }

		[AntiXss]
		public string ktb_interfacehandlingdnet { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public string xts_arrivalpattern { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesallocated_base { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargeallocated { get; set; }

		[AntiXss]
		public string xts_technicaladvisoridname { get; set; }

		[AntiXss]
		public string ktb_contactidyominame { get; set; }

		[AntiXss]
		public decimal xts_totalworktaxallocated { get; set; }

		[AntiXss]
		public string xts_overcreditlimitonholdname { get; set; }

		[AntiXss]
		public decimal ktb_totalsuborderamount_base { get; set; }

		[AntiXss]
		public string xts_maintenancemodelidname { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount_base { get; set; }

		[AntiXss]
		public decimal xts_esttotaltaxamount { get; set; }

		[AntiXss]
		public Guid xts_originaloutsorcereferenceoutsourcewoid { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargebasenotallocated { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargetaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_esttotalwithholdingtaxamount { get; set; }

		[AntiXss]
		public string xts_dimension7idname { get; set; }

		[AntiXss]
		public decimal xts_esttotalworkamount_base { get; set; }

		[AntiXss]
		public string ktb_interfacehandling { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesbaseallocated { get; set; }

		[AntiXss]
		public bool xts_isworkorderform { get; set; }

		[AntiXss]
		public decimal ktb_totalserviceamount_base { get; set; }

		[AntiXss]
		public DateTime xts_actualarrivaldateandtime { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxallocated_base { get; set; }

		[AntiXss]
		public decimal xts_totalworknotallocated_base { get; set; }

		[AntiXss]
		public decimal xts_esttotalpartsamount { get; set; }

		[AntiXss]
		public decimal xts_esttotalpartsbaseamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxnotallocated { get; set; }

		[AntiXss]
		public string xts_dimension10idname { get; set; }

		[AntiXss]
		public string xts_servicecampaignidname { get; set; }

		[AntiXss]
		public string xts_incomingoutsourcereferenceidname { get; set; }

		[AntiXss]
		public decimal xts_totaltaxamount { get; set; }

		[AntiXss]
		public Guid xts_mainpartcauseid { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public decimal xts_balance { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_pickupaddress1 { get; set; }

		[AntiXss]
		public DateTime xts_vehicleinspectionexpirationdate { get; set; }

		[AntiXss]
		public Guid xts_productsegment4id { get; set; }

		[AntiXss]
		public string xts_servicetemplatecalculationmethod { get; set; }

		[AntiXss]
		public string xts_dimension4idname { get; set; }

		[AntiXss]
		public decimal xts_totalpartsbaseallocated { get; set; }

		[AntiXss]
		public string xts_exchangeratetypeidname { get; set; }

		[AntiXss]
		public decimal xts_totalworktaxamount_base { get; set; }

		[AntiXss]
		public string ktb_statusinterfacednet { get; set; }

		[AntiXss]
		public Guid xts_workorderreferenceid { get; set; }

		[AntiXss]
		public Guid xts_workorderid { get; set; }

		[AntiXss]
		public string xts_behaviorname { get; set; }

		[AntiXss]
		public string xts_servicetemplatecalculationmethodname { get; set; }

		[AntiXss]
		public string ktb_paneltypeidname { get; set; }

		[AntiXss]
		public Guid ktb_saleschannelid { get; set; }

		[AntiXss]
		public Guid xts_reservationclassid { get; set; }

		[AntiXss]
		public decimal xts_totalworkbaseamount_base { get; set; }

		[AntiXss]
		public DateTime xts_insuranceinspectiondate { get; set; }

		[AntiXss]
		public decimal xts_exchangerateamount { get; set; }

		[AntiXss]
		public string xts_cancelreason { get; set; }

		[AntiXss]
		public DateTime xts_exchangeratedate { get; set; }

		[AntiXss]
		public string xts_finishpatternname { get; set; }

		[AntiXss]
		public decimal xts_totalpartstaxnotallocated_base { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargeallocated_base { get; set; }

		[AntiXss]
		public string xts_claimapprovalnumber { get; set; }

		[AntiXss]
		public decimal xjp_caliallocated_base { get; set; }

		[AntiXss]
		public decimal xts_totalothersalestaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalpartsbaseamount { get; set; }

		[AntiXss]
		public decimal xts_grandtotalnotallocated_base { get; set; }

		[AntiXss]
		public string ktb_paneldescription { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamount { get; set; }

		[AntiXss]
		public Guid xts_serviceadvisorid { get; set; }

		[AntiXss]
		public int xjp_estcaliperiodmonths { get; set; }

		[AntiXss]
		public Guid xts_siteid { get; set; }

		[AntiXss]
		public decimal xts_esttotalmiscchargetaxamount { get; set; }

		[AntiXss]
		public string xts_dimension9idname { get; set; }

		[AntiXss]
		public string xts_globalserviceidname { get; set; }

		[AntiXss]
		public string ktb_servicelocationidname { get; set; }

		[AntiXss]
		public bool ktb_validatorhandling { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargetaxamount { get; set; }

		[AntiXss]
		public string ktb_servicetypeidname { get; set; }

		[AntiXss]
		public string xts_log { get; set; }

		[AntiXss]
		public string xts_contactpersonphone { get; set; }

		[AntiXss]
		public decimal ktb_totaloilamount_base { get; set; }

		[AntiXss]
		public string xts_dimension8idname { get; set; }

		[AntiXss]
		public Guid ktb_methodofpaymentid { get; set; }

		[AntiXss]
		public string xts_reservationclassidname { get; set; }

		[AntiXss]
		public string ktb_paneldeskripsi { get; set; }

		[AntiXss]
		public bool ktb_isinsurance { get; set; }

		[AntiXss]
		public string xjp_firstregistration { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public DateTime xts_invoicepostingdate { get; set; }

		[AntiXss]
		public decimal xts_totalworktaxamount { get; set; }

		[AntiXss]
		public decimal xts_totalpartstaxallocated_base { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesallocated { get; set; }

		[AntiXss]
		public decimal ktb_totalserviceamount { get; set; }

		[AntiXss]
		public string xts_autocreateoutsourceworkorderreceiptname { get; set; }

		[AntiXss]
		public string xts_aliasname { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargetaxallocated { get; set; }

		[AntiXss]
		public string ktb_validatorhandlingname { get; set; }

		[AntiXss]
		public string ktb_statusinterfaceregcheckdnet { get; set; }

		[AntiXss]
		public decimal ktb_totalsuborderamount { get; set; }

		[AntiXss]
		public string xts_deliveryaddress4 { get; set; }

		[AntiXss]
		public Guid xts_servicepersoninchargeid { get; set; }

		[AntiXss]
		public string ktb_notificationvalidatorservice { get; set; }

		[AntiXss]
		public decimal xts_esttotalworkbaseamount_base { get; set; }

		[AntiXss]
		public string ktb_dnetidserviceincoming { get; set; }

		[AntiXss]
		public Guid xts_vehiclemodelid { get; set; }

		[AntiXss]
		public string xts_servicecategoryidname { get; set; }

		[AntiXss]
		public int xts_currentmileage { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_claimstatus { get; set; }

		[AntiXss]
		public string xts_workorderreferenceidname { get; set; }

		[AntiXss]
		public decimal xts_totalpartsbasenotallocated { get; set; }

		[AntiXss]
		public string xts_chassisnumber { get; set; }

		[AntiXss]
		public decimal xts_totalothersalestaxamount { get; set; }

		[AntiXss]
		public string xts_symptomidname { get; set; }

		[AntiXss]
		public bool ktb_cogsupdated { get; set; }

		[AntiXss]
		public decimal xts_totalpartstaxnotallocated { get; set; }

		[AntiXss]
		public bool ktb_isinterfaced { get; set; }

		[AntiXss]
		public int xts_historymileage { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public decimal xts_totalworkbasenotallocated_base { get; set; }

		[AntiXss]
		public DateTime ktb_tanggalinputmerimen { get; set; }

		[AntiXss]
		public string xts_dimension1idname { get; set; }

		[AntiXss]
		public decimal xts_grandtotalamount { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamount_base { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public Guid xts_araccountid { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string ktb_notificationvalidatormessage { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesbaseamount { get; set; }

		[AntiXss]
		public decimal xts_totalworktaxnotallocated { get; set; }

		[AntiXss]
		public decimal xts_esttotalworkamount { get; set; }

		[AntiXss]
		public int xjp_caliperiodmonths { get; set; }

		[AntiXss]
		public decimal xts_totalpartstaxamount { get; set; }

		[AntiXss]
		public decimal xts_totalothersalestaxnotallocated_base { get; set; }

		[AntiXss]
		public decimal xts_totalworkamount { get; set; }

		[AntiXss]
		public DateTime xts_scheduledarrivaldateandtime { get; set; }

		[AntiXss]
		public string xts_dimension2idname { get; set; }

		[AntiXss]
		public string xts_originalcustomeridyominame { get; set; }

		[AntiXss]
		public string xts_contactpersonidyominame { get; set; }

		[AntiXss]
		public DateTime xts_accidentdate { get; set; }

		[AntiXss]
		public Guid xts_servicetemplatevehiclepriceclassid { get; set; }

		[AntiXss]
		public DateTime ktb_tanggalterbitspk { get; set; }

		[AntiXss]
		public decimal xts_exchangerate { get; set; }

		[AntiXss]
		public decimal xts_paymentreceivedamount { get; set; }

		[AntiXss]
		public string xts_originalcustomeridname { get; set; }

		[AntiXss]
		public decimal xts_esttotalmiscchargetaxamount_base { get; set; }

		[AntiXss]
		public string xts_arrivalpatternname { get; set; }

		[AntiXss]
		public string ktb_statusinterface { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public decimal xts_totalothersalestaxnotallocated { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesbasenotallocated { get; set; }

		[AntiXss]
		public decimal ktb_totalsubmaterialamount_base { get; set; }

		[AntiXss]
		public string ktb_interfacehandlingname { get; set; }

		[AntiXss]
		public string xts_customeridname { get; set; }

		[AntiXss]
		public string xts_checkstockagainstname { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public string xts_modelspecificationnumber { get; set; }

		[AntiXss]
		public string xts_checkstockagainst { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public Guid xts_dimension4id { get; set; }

		[AntiXss]
		public string xts_dslinternalnumber { get; set; }

		[AntiXss]
		public string xts_servicetemplatevehiclepriceclassidname { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public decimal xts_totalothersalestaxallocated_base { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public Guid xts_dimension9id { get; set; }

		[AntiXss]
		public decimal xts_totalworkallocated_base { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string xts_pickupaddress2 { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxnotallocated_base { get; set; }

		[AntiXss]
		public string ktb_vehiclestate { get; set; }

		[AntiXss]
		public bool ktb_isfreebilltocustomer { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargetaxnotallocated_base { get; set; }

		[AntiXss]
		public int ktb_panel { get; set; }

		[AntiXss]
		public decimal xts_esttotalbaseamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalpartsamount_base { get; set; }

		[AntiXss]
		public string xts_downpaymentispaidname { get; set; }

		[AntiXss]
		public string ktb_contactmobilephonenumber { get; set; }

		[AntiXss]
		public Guid xts_dimension10id { get; set; }

		[AntiXss]
		public string xts_deliveryaddress2 { get; set; }

		[AntiXss]
		public Guid xts_vehicleidentificationid { get; set; }

		[AntiXss]
		public Guid xts_incomingoutsourcereferenceid { get; set; }

		[AntiXss]
		public DateTime ktb_fullpaymentdate { get; set; }

		[AntiXss]
		public decimal xts_totalpartsnotallocated_base { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargetaxnotallocated { get; set; }

		[AntiXss]
		public decimal xts_totalpartstaxallocated { get; set; }

		[AntiXss]
		public decimal xts_esttotalbaseamount { get; set; }

		[AntiXss]
		public Guid xts_servicecategoryid { get; set; }

		[AntiXss]
		public Guid xts_exchangeratetypeid { get; set; }

		[AntiXss]
		public string xjp_originaloutsorcereferencepdiidname { get; set; }

		[AntiXss]
		public string ktb_statusinterfaceassistpartsalesdnetname { get; set; }

		[AntiXss]
		public string xts_dimension3idname { get; set; }

		[AntiXss]
		public string ktb_statusinterfacefreeservicednetname { get; set; }

		[AntiXss]
		public int xts_meterexchangemileage { get; set; }

		[AntiXss]
		public string ktb_cogsupdatedname { get; set; }

		[AntiXss]
		public string ktb_methodofpaymentidname { get; set; }

		[AntiXss]
		public string xts_ordertypeidname { get; set; }

		[AntiXss]
		public DateTime xts_scheduledservicefinishdateandtime { get; set; }

		[AntiXss]
		public string xts_chassismodel { get; set; }

		[AntiXss]
		public string ktb_workorderdescription { get; set; }

		[AntiXss]
		public Guid xts_reservationid { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public decimal xts_esttotalothersalesamount { get; set; }

		[AntiXss]
		public string xts_registrationmodel { get; set; }

		[AntiXss]
		public decimal xts_totalpartsallocated { get; set; }

		[AntiXss]
		public decimal xts_totalworkallocated { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamountreceived_base { get; set; }

		[AntiXss]
		public string xts_mainpartcauseidname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public string ktb_mechanicalleaderemployeeidname { get; set; }

		[AntiXss]
		public Guid xts_dimension8id { get; set; }

		[AntiXss]
		public decimal xts_totalworkbaseamount { get; set; }

		[AntiXss]
		public decimal xts_esttotalmiscchargebaseamount_base { get; set; }

		[AntiXss]
		public bool xts_warrantycontractavailable { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xts_servicepackagecontractavailablename { get; set; }

		[AntiXss]
		public Guid xts_dimension6id { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string ktb_isinsurancename { get; set; }

		[AntiXss]
		public string ktb_statusinterfacednetname { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesbaseamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxallocated { get; set; }

		[AntiXss]
		public string xts_vehicleincludedintherecallname { get; set; }

		[AntiXss]
		public decimal xts_totalpartsbaseamount_base { get; set; }

		[AntiXss]
		public string xts_readytobeinvoiced { get; set; }

		[AntiXss]
		public decimal xts_esttotalothersalesbaseamount { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxamount { get; set; }

		[AntiXss]
		public string xts_vehicleidentificationidname { get; set; }

		[AntiXss]
		public Guid xts_customerid { get; set; }

		[AntiXss]
		public string xts_pickupaddress3 { get; set; }

		[AntiXss]
		public string xts_manufacturername { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public decimal ktb_totalsubmaterialamount { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public string xts_billallocationmethodname { get; set; }

		[AntiXss]
		public decimal xts_grandtotalallocated_base { get; set; }

		[AntiXss]
		public string xts_serviceadvisoridname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public string xts_deliveryaddress3 { get; set; }

		[AntiXss]
		public bool xts_overcreditlimitonhold { get; set; }

		[AntiXss]
		public string xts_vehiclesizeclassidname { get; set; }

		[AntiXss]
		public Guid xts_ordertypeid { get; set; }

		[AntiXss]
		public Guid xts_salespersonid { get; set; }

		[AntiXss]
		public string xts_finishpattern { get; set; }

		[AntiXss]
		public Guid xts_symptomid { get; set; }

		[AntiXss]
		public decimal xts_balance_base { get; set; }

		[AntiXss]
		public decimal xts_esttotalworkbaseamount { get; set; }

		[AntiXss]
		public string xts_deliveryaddress1 { get; set; }

		[AntiXss]
		public Guid xts_maintenancemodelid { get; set; }

		[AntiXss]
		public string xjp_idempotentmessage { get; set; }

		[AntiXss]
		public string ktb_isvalidationfromdnet { get; set; }

		[AntiXss]
		public string xts_classificationnumber { get; set; }

		[AntiXss]
		public decimal xts_esttotalworktaxamount { get; set; }

		[AntiXss]
		public string xts_contactpersonidname { get; set; }

		[AntiXss]
		public Guid xts_dimension3id { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargetaxallocated_base { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public decimal xts_totalworknotallocated { get; set; }

		[AntiXss]
		public decimal xts_esttotalpartsbaseamount { get; set; }

		[AntiXss]
		public DateTime xts_actualfinishdateandtime { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesnotallocated { get; set; }

		[AntiXss]
		public decimal xts_totaltaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalpartsnotallocated { get; set; }

		[AntiXss]
		public string xts_referralname { get; set; }

		[AntiXss]
		public decimal xts_totalcostamount { get; set; }

		[AntiXss]
		public Guid xts_manufacturerid { get; set; }

		[AntiXss]
		public bool xts_servicepackagecontractavailable { get; set; }

		[AntiXss]
		public decimal xts_totalworkbasenotallocated { get; set; }

		[AntiXss]
		public Guid xts_termofpaymentid { get; set; }

		[AntiXss]
		public decimal xjp_calinotallocated { get; set; }

		[AntiXss]
		public string xts_readytobeinvoicedname { get; set; }

		[AntiXss]
		public string xts_leaseapprovalnumber { get; set; }

		[AntiXss]
		public string xts_behavior { get; set; }

		[AntiXss]
		public Guid xts_servicecampaignid { get; set; }

		[AntiXss]
		public decimal ktb_totaloilamount { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargeamount_base { get; set; }

		[AntiXss]
		public string ktb_interfacehandlingdnetname { get; set; }

		[AntiXss]
		public string xts_productsegment1idname { get; set; }

		[AntiXss]
		public decimal xts_totalworkbaseallocated_base { get; set; }

		[AntiXss]
		public string xts_billallocationmethod { get; set; }

		[AntiXss]
		public decimal xts_grandtotalallocated { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_salespersonidname { get; set; }

		[AntiXss]
		public decimal xjp_caliamount { get; set; }

		[AntiXss]
		public string ktb_ordertypetranstypename { get; set; }

		[AntiXss]
		public decimal xts_totalpartsbasenotallocated_base { get; set; }

		[AntiXss]
		public decimal xts_esttotalmiscchargeamount_base { get; set; }

		[AntiXss]
		public Guid ktb_contactid { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargebaseallocated_base { get; set; }

		[AntiXss]
		public string ktb_interfacemessage { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxamount_base { get; set; }

		[AntiXss]
		public Guid xts_dimension7id { get; set; }

		[AntiXss]
		public string xts_workorderstatusname { get; set; }

		[AntiXss]
		public Guid xts_dimension1id { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargebasenotallocated_base { get; set; }

		[AntiXss]
		public string xts_servicetemplateidname { get; set; }

		[AntiXss]
		public string xts_vehiclemodelidname { get; set; }

		[AntiXss]
		public Guid ktb_mechanicalleaderemployeeid { get; set; }

		[AntiXss]
		public bool xts_autocreateoutsourceworkorderreceipt { get; set; }

		[AntiXss]
		public decimal xjp_estcaliamount { get; set; }

		[AntiXss]
		public decimal xts_totalpaymentamount { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public string xts_termofpaymentidname { get; set; }

		[AntiXss]
		public bool xts_downpaymentispaid { get; set; }

		[AntiXss]
		public decimal xts_esttotalmiscchargeamount { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesbasenotallocated_base { get; set; }

		[AntiXss]
		public decimal xts_esttotalmiscchargebaseamount { get; set; }

		[AntiXss]
		public string xts_productsegment2idname { get; set; }

		[AntiXss]
		public string xts_productsegment4idname { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string ktb_panelcategory { get; set; }

		[AntiXss]
		public DateTime xts_scheduledfinishdateandtime { get; set; }

		[AntiXss]
		public Guid xts_dimension2id { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public Guid xts_originalworkorderreferencenumberid { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount { get; set; }

		[AntiXss]
		public DateTime ktb_estimationfinishdatebysa { get; set; }

		[AntiXss]
		public string ktb_dnetidwotype { get; set; }

		[AntiXss]
		public decimal xts_totalothersalestaxallocated { get; set; }

		[AntiXss]
		public string xts_originaloutsorcereferencesvcinstructionidname { get; set; }

		[AntiXss]
		public Guid xts_technicaladvisorid { get; set; }

		[AntiXss]
		public Guid ktb_servicelocationid { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string xts_loadinggroupidname { get; set; }

		[AntiXss]
		public decimal xts_esttotalothersalesamount_base { get; set; }

		[AntiXss]
		public decimal xts_esttotalpartsamount_base { get; set; }

		[AntiXss]
		public Guid xts_workgroupid { get; set; }

		[AntiXss]
		public decimal ktb_grandtotalinformationdetail { get; set; }

		[AntiXss]
		public decimal ktb_grandtotalinformationdetail_base { get; set; }

		[AntiXss]
		public decimal ktb_totalmiscchargeamount { get; set; }

		[AntiXss]
		public decimal ktb_ownriskamount_base { get; set; }

		[AntiXss]
		public decimal ktb_totalmiscchargeamount_base { get; set; }

		[AntiXss]
		public string ktb_spknumber { get; set; }

		[AntiXss]
		public decimal ktb_ownriskamount { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtax2allocated_base { get; set; }

		[AntiXss]
		public Guid ktb_formsprosurveyinvitationid { get; set; }

		[AntiXss]
		public string ktb_approvedcreditlimitreason { get; set; }

		[AntiXss]
		public string ktb_approvedcreditlimitbyname { get; set; }

		[AntiXss]
		public string ktb_approvedcreditlimitbyyominame { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtax2notallocated { get; set; }

		[AntiXss]
		public Guid ktb_approvedcreditlimitby { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtax2allocated { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtax2notallocated_base { get; set; }

		[AntiXss]
		public string ktb_formsprosurveyinvitationidname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
