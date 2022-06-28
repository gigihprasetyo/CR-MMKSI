#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_reservationtransactionDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:16:02
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_reservationtransactionDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xts_reservationtransaction { get; set; }

		public decimal ktb_esttotalworktaxamount { get; set; }

		public DateTime modifiedon { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string xts_finishpattern { get; set; }

		public decimal ktb_esttotalmiscchargebaseamount_base { get; set; }

		public string statuscodename { get; set; }

		public string xts_status { get; set; }

		public DateTime xts_scheduledarrivaldateandtime { get; set; }

		public string xts_deliveryaddress2 { get; set; }

		public string xts_workorderidname { get; set; }

		public string xts_estimatebayidname { get; set; }

		public decimal ktb_esttotalbaseamount_base { get; set; }

		public decimal ktb_esttotalothersalestaxamount_base { get; set; }

		public decimal ktb_esttotalothersalestaxamount { get; set; }

		public string xts_pickupaddress1 { get; set; }

		public decimal ktb_estgrandtotalamount { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string xts_customeridyominame { get; set; }

		public string xts_chassismodel { get; set; }

		public bool xts_vehicleincludedintherecall { get; set; }

		public Guid createdonbehalfby { get; set; }

		public DateTime xts_scheduledservicestartdate { get; set; }

		public string xts_pickupaddress2 { get; set; }

		public Guid ktb_productsegment2id { get; set; }

		public string ktb_productsegment4idname { get; set; }

		public decimal ktb_esttotalworkamount_base { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string ktb_productidname { get; set; }

		public string xts_deliverypostalcode { get; set; }

		public string xts_customernumber { get; set; }

		public string ktb_productsegment3idname { get; set; }

		public decimal ktb_exchangerateamount { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string xts_customeridname { get; set; }

		public string owneridyominame { get; set; }

		public int statuscode { get; set; }

		public Guid xts_estimatebayid { get; set; }

		public decimal ktb_esttotalmiscchargeamount_base { get; set; }

		public decimal ktb_esttotalothersalesbaseamount { get; set; }

		public string xts_defaultoutsourceworkshopidname { get; set; }

		public string xts_maintenancepackageinformation { get; set; }

		public decimal ktb_esttotalworkamount { get; set; }

		public string statecodename { get; set; }

		public string xts_deliveryaddress1 { get; set; }

		public DateTime xts_scheduledfinishdateandtime { get; set; }

		public decimal ktb_esttotalmiscchargetaxamount { get; set; }

		public string ktb_productsegment1idname { get; set; }

		public string xts_contactpersonidname { get; set; }

		public Guid xts_vehicleidentificationid { get; set; }

		public Guid xts_contactpersonid { get; set; }

		public Guid xts_reservationpersoninchargeid { get; set; }

		public Guid xts_defaultoutsourceworkshopid { get; set; }

		public Guid xts_servicecategoryid { get; set; }

		public DateTime createdon { get; set; }

		public string ktb_productsegment2idname { get; set; }

		public string xts_handling { get; set; }

		public Guid xts_customerid { get; set; }

		public string xts_statusname { get; set; }

		public decimal ktb_esttotalmiscchargeamount { get; set; }

		public string xts_contactpersonidyominame { get; set; }

		public Guid xts_previousreservationid { get; set; }

		public decimal ktb_esttotalworkbaseamount_base { get; set; }

		public string ktb_productdescription { get; set; }

		public string xts_vehicleidentificationidname { get; set; }

		public decimal ktb_esttotaltaxamount_base { get; set; }

		public string xts_personinchargefordeliveryidname { get; set; }

		public Guid modifiedby { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public decimal ktb_esttotalothersalesbaseamount_base { get; set; }

		public Guid owninguser { get; set; }

		public Guid xts_incomingoutsourcebusinessunitid { get; set; }

		public string xts_vehicleplatenumber { get; set; }

		public string xts_vehiclemodelname { get; set; }

		public decimal ktb_esttotalworkbaseamount { get; set; }

		public decimal ktb_exchangerate { get; set; }

		public DateTime ktb_exchangeratedate { get; set; }

		public string xts_globalserviceidname { get; set; }

		public string traversedpath { get; set; }

		public string xts_aliasname { get; set; }

		public decimal ktb_esttotalpartstaxamount { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string xts_reservationclassidname { get; set; }

		public decimal ktb_esttotaltaxamount { get; set; }

		public string xts_servicecategoryidname { get; set; }

		public Guid xts_reservationclassid { get; set; }

		public Guid xts_salespersonid { get; set; }

		public string owneridtype { get; set; }

		public decimal ktb_esttotalpartsamount_base { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public decimal ktb_esttotalpartstaxamount_base { get; set; }

		public string xts_personinchargeforarrivalidname { get; set; }

		public Guid xts_reservationtransactionid { get; set; }

		public string xts_chassisnumber { get; set; }

		public string owneridname { get; set; }

		public string xts_vehicleincludedintherecallname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid xts_personinchargeforarrivalid { get; set; }

		public string xts_loadinggroupidname { get; set; }

		public string ktb_dnetticketno { get; set; }

		public Guid xts_globalserviceid { get; set; }

		public Guid owningteam { get; set; }

		public string xts_reservationmemo { get; set; }

		public Guid ktb_productsegment4id { get; set; }

		public string xts_arrivalpatternname { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public Guid processid { get; set; }

		public decimal exchangerate { get; set; }

		public string xts_insurancecontract { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string xts_reservationpersoninchargeidname { get; set; }

		public string xts_finishpatternname { get; set; }

		public string modifiedbyyominame { get; set; }

		public Guid ownerid { get; set; }

		public decimal ktb_esttotalpartsamount { get; set; }

		public string xts_ordertypeidname { get; set; }

		public string xts_salespersonidname { get; set; }

		public string createdbyname { get; set; }

		public string xts_businessunitidname { get; set; }

		public int statecode { get; set; }

		public Guid xts_personinchargefordeliveryid { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public Guid createdby { get; set; }

		public string xts_platenumber { get; set; }

		public string xts_arrivalpattern { get; set; }

		public Guid ktb_productsegment1id { get; set; }

		public string xts_deliveryaddress4 { get; set; }

		public Guid xts_ordertypeid { get; set; }

		public string xts_arrivalpostalcode { get; set; }

		public string xts_deliveryaddress3 { get; set; }

		public Guid stageid { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string xts_incomingoutsourcebusinessunitidname { get; set; }

		public decimal ktb_esttotalothersalesamount_base { get; set; }

		public decimal ktb_estgrandtotalamount_base { get; set; }

		public int importsequencenumber { get; set; }

		public string xts_locking { get; set; }

		public Guid ktb_productid { get; set; }

		public decimal xts_reservationmanhour { get; set; }

		public string xts_pickupaddress3 { get; set; }

		public string xts_servicepackagecontract { get; set; }

		public Int64 versionnumber { get; set; }

		public decimal ktb_esttotalothersalesamount { get; set; }

		public Guid xts_businessunitid { get; set; }

		public decimal ktb_esttotalbaseamount { get; set; }

		public decimal ktb_esttotalworktaxamount_base { get; set; }

		public decimal ktb_esttotalpartsbaseamount_base { get; set; }

		public string xts_handlingname { get; set; }

		public decimal ktb_esttotalpartsbaseamount { get; set; }

		public decimal ktb_esttotalmiscchargetaxamount_base { get; set; }

		public string xts_pickupaddress4 { get; set; }

		public string xts_contactpersonphone { get; set; }

		public string modifiedbyname { get; set; }

		public Guid ktb_productsegment3id { get; set; }

		public string createdbyyominame { get; set; }

		public decimal ktb_esttotalmiscchargebaseamount { get; set; }

		public Guid xts_loadinggroupid { get; set; }

		public string xts_previousreservationidname { get; set; }

		public Guid xts_workorderid { get; set; }

		public string xts_cancelreason { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
