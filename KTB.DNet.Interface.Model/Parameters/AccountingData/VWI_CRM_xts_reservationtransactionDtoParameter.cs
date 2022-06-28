#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_reservationtransactionParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_reservationtransactionParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_reservationtransaction { get; set; }

		[AntiXss]
		public decimal ktb_esttotalworktaxamount { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_finishpattern { get; set; }

		[AntiXss]
		public decimal ktb_esttotalmiscchargebaseamount_base { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public DateTime xts_scheduledarrivaldateandtime { get; set; }

		[AntiXss]
		public string xts_deliveryaddress2 { get; set; }

		[AntiXss]
		public string xts_workorderidname { get; set; }

		[AntiXss]
		public string xts_estimatebayidname { get; set; }

		[AntiXss]
		public decimal ktb_esttotalbaseamount_base { get; set; }

		[AntiXss]
		public decimal ktb_esttotalothersalestaxamount_base { get; set; }

		[AntiXss]
		public decimal ktb_esttotalothersalestaxamount { get; set; }

		[AntiXss]
		public string xts_pickupaddress1 { get; set; }

		[AntiXss]
		public decimal ktb_estgrandtotalamount { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_customeridyominame { get; set; }

		[AntiXss]
		public string xts_chassismodel { get; set; }

		[AntiXss]
		public bool xts_vehicleincludedintherecall { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public DateTime xts_scheduledservicestartdate { get; set; }

		[AntiXss]
		public string xts_pickupaddress2 { get; set; }

		[AntiXss]
		public Guid ktb_productsegment2id { get; set; }

		[AntiXss]
		public string ktb_productsegment4idname { get; set; }

		[AntiXss]
		public decimal ktb_esttotalworkamount_base { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string ktb_productidname { get; set; }

		[AntiXss]
		public string xts_deliverypostalcode { get; set; }

		[AntiXss]
		public string xts_customernumber { get; set; }

		[AntiXss]
		public string ktb_productsegment3idname { get; set; }

		[AntiXss]
		public decimal ktb_exchangerateamount { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xts_customeridname { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public Guid xts_estimatebayid { get; set; }

		[AntiXss]
		public decimal ktb_esttotalmiscchargeamount_base { get; set; }

		[AntiXss]
		public decimal ktb_esttotalothersalesbaseamount { get; set; }

		[AntiXss]
		public string xts_defaultoutsourceworkshopidname { get; set; }

		[AntiXss]
		public string xts_maintenancepackageinformation { get; set; }

		[AntiXss]
		public decimal ktb_esttotalworkamount { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_deliveryaddress1 { get; set; }

		[AntiXss]
		public DateTime xts_scheduledfinishdateandtime { get; set; }

		[AntiXss]
		public decimal ktb_esttotalmiscchargetaxamount { get; set; }

		[AntiXss]
		public string ktb_productsegment1idname { get; set; }

		[AntiXss]
		public string xts_contactpersonidname { get; set; }

		[AntiXss]
		public Guid xts_vehicleidentificationid { get; set; }

		[AntiXss]
		public Guid xts_contactpersonid { get; set; }

		[AntiXss]
		public Guid xts_reservationpersoninchargeid { get; set; }

		[AntiXss]
		public Guid xts_defaultoutsourceworkshopid { get; set; }

		[AntiXss]
		public Guid xts_servicecategoryid { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string ktb_productsegment2idname { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public Guid xts_customerid { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public decimal ktb_esttotalmiscchargeamount { get; set; }

		[AntiXss]
		public string xts_contactpersonidyominame { get; set; }

		[AntiXss]
		public Guid xts_previousreservationid { get; set; }

		[AntiXss]
		public decimal ktb_esttotalworkbaseamount_base { get; set; }

		[AntiXss]
		public string ktb_productdescription { get; set; }

		[AntiXss]
		public string xts_vehicleidentificationidname { get; set; }

		[AntiXss]
		public decimal ktb_esttotaltaxamount_base { get; set; }

		[AntiXss]
		public string xts_personinchargefordeliveryidname { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public decimal ktb_esttotalothersalesbaseamount_base { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public Guid xts_incomingoutsourcebusinessunitid { get; set; }

		[AntiXss]
		public string xts_vehicleplatenumber { get; set; }

		[AntiXss]
		public string xts_vehiclemodelname { get; set; }

		[AntiXss]
		public decimal ktb_esttotalworkbaseamount { get; set; }

		[AntiXss]
		public decimal ktb_exchangerate { get; set; }

		[AntiXss]
		public DateTime ktb_exchangeratedate { get; set; }

		[AntiXss]
		public string xts_globalserviceidname { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public string xts_aliasname { get; set; }

		[AntiXss]
		public decimal ktb_esttotalpartstaxamount { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_reservationclassidname { get; set; }

		[AntiXss]
		public decimal ktb_esttotaltaxamount { get; set; }

		[AntiXss]
		public string xts_servicecategoryidname { get; set; }

		[AntiXss]
		public Guid xts_reservationclassid { get; set; }

		[AntiXss]
		public Guid xts_salespersonid { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public decimal ktb_esttotalpartsamount_base { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public decimal ktb_esttotalpartstaxamount_base { get; set; }

		[AntiXss]
		public string xts_personinchargeforarrivalidname { get; set; }

		[AntiXss]
		public Guid xts_reservationtransactionid { get; set; }

		[AntiXss]
		public string xts_chassisnumber { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string xts_vehicleincludedintherecallname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid xts_personinchargeforarrivalid { get; set; }

		[AntiXss]
		public string xts_loadinggroupidname { get; set; }

		[AntiXss]
		public string ktb_dnetticketno { get; set; }

		[AntiXss]
		public Guid xts_globalserviceid { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_reservationmemo { get; set; }

		[AntiXss]
		public Guid ktb_productsegment4id { get; set; }

		[AntiXss]
		public string xts_arrivalpatternname { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xts_insurancecontract { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string xts_reservationpersoninchargeidname { get; set; }

		[AntiXss]
		public string xts_finishpatternname { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public decimal ktb_esttotalpartsamount { get; set; }

		[AntiXss]
		public string xts_ordertypeidname { get; set; }

		[AntiXss]
		public string xts_salespersonidname { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public Guid xts_personinchargefordeliveryid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string xts_platenumber { get; set; }

		[AntiXss]
		public string xts_arrivalpattern { get; set; }

		[AntiXss]
		public Guid ktb_productsegment1id { get; set; }

		[AntiXss]
		public string xts_deliveryaddress4 { get; set; }

		[AntiXss]
		public Guid xts_ordertypeid { get; set; }

		[AntiXss]
		public string xts_arrivalpostalcode { get; set; }

		[AntiXss]
		public string xts_deliveryaddress3 { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_incomingoutsourcebusinessunitidname { get; set; }

		[AntiXss]
		public decimal ktb_esttotalothersalesamount_base { get; set; }

		[AntiXss]
		public decimal ktb_estgrandtotalamount_base { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public Guid ktb_productid { get; set; }

		[AntiXss]
		public decimal xts_reservationmanhour { get; set; }

		[AntiXss]
		public string xts_pickupaddress3 { get; set; }

		[AntiXss]
		public string xts_servicepackagecontract { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public decimal ktb_esttotalothersalesamount { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public decimal ktb_esttotalbaseamount { get; set; }

		[AntiXss]
		public decimal ktb_esttotalworktaxamount_base { get; set; }

		[AntiXss]
		public decimal ktb_esttotalpartsbaseamount_base { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public decimal ktb_esttotalpartsbaseamount { get; set; }

		[AntiXss]
		public decimal ktb_esttotalmiscchargetaxamount_base { get; set; }

		[AntiXss]
		public string xts_pickupaddress4 { get; set; }

		[AntiXss]
		public string xts_contactpersonphone { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid ktb_productsegment3id { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public decimal ktb_esttotalmiscchargebaseamount { get; set; }

		[AntiXss]
		public Guid xts_loadinggroupid { get; set; }

		[AntiXss]
		public string xts_previousreservationidname { get; set; }

		[AntiXss]
		public Guid xts_workorderid { get; set; }

		[AntiXss]
		public string xts_cancelreason { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
