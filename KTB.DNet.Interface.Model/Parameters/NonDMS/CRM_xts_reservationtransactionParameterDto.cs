#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_reservationtransaction class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Gugun (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 15:32:20
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KTB.DNet.Interface.Resources;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xts_reservationtransactionParameterDto : ParameterDtoBase, IValidatableObject
    {
        
		public Guid createdby { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdbyname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdbyyominame { get; set; }

		public DateTime? createdon { get; set; }

		public Guid createdonbehalfby { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string DealerCode { get; set; }

		public decimal? exchangerate { get; set; }

		public int? importsequencenumber { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_dnetticketno { get; set; }

		public decimal? ktb_estgrandtotalamount { get; set; }

		public decimal? ktb_estgrandtotalamount_base { get; set; }

		public decimal? ktb_esttotalbaseamount { get; set; }

		public decimal? ktb_esttotalbaseamount_base { get; set; }

		public decimal? ktb_esttotalmiscchargeamount { get; set; }

		public decimal? ktb_esttotalmiscchargeamount_base { get; set; }

		public decimal? ktb_esttotalmiscchargebaseamount { get; set; }

		public decimal? ktb_esttotalmiscchargebaseamount_base { get; set; }

		public decimal? ktb_esttotalmiscchargetaxamount { get; set; }

		public decimal? ktb_esttotalmiscchargetaxamount_base { get; set; }

		public decimal? ktb_esttotalothersalesamount { get; set; }

		public decimal? ktb_esttotalothersalesamount_base { get; set; }

		public decimal? ktb_esttotalothersalesbaseamount { get; set; }

		public decimal? ktb_esttotalothersalesbaseamount_base { get; set; }

		public decimal? ktb_esttotalothersalestaxamount { get; set; }

		public decimal? ktb_esttotalothersalestaxamount_base { get; set; }

		public decimal? ktb_esttotalpartsamount { get; set; }

		public decimal? ktb_esttotalpartsamount_base { get; set; }

		public decimal? ktb_esttotalpartsbaseamount { get; set; }

		public decimal? ktb_esttotalpartsbaseamount_base { get; set; }

		public decimal? ktb_esttotalpartstaxamount { get; set; }

		public decimal? ktb_esttotalpartstaxamount_base { get; set; }

		public decimal? ktb_esttotaltaxamount { get; set; }

		public decimal? ktb_esttotaltaxamount_base { get; set; }

		public decimal? ktb_esttotalworkamount { get; set; }

		public decimal? ktb_esttotalworkamount_base { get; set; }

		public decimal? ktb_esttotalworkbaseamount { get; set; }

		public decimal? ktb_esttotalworkbaseamount_base { get; set; }

		public decimal? ktb_esttotalworktaxamount { get; set; }

		public decimal? ktb_esttotalworktaxamount_base { get; set; }

		public decimal? ktb_exchangerate { get; set; }

		public decimal? ktb_exchangerateamount { get; set; }

		public DateTime? ktb_exchangeratedate { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_productdescription { get; set; }

		public Guid ktb_productid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_productidname { get; set; }

		public Guid ktb_productsegment1id { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_productsegment1idname { get; set; }

		public Guid ktb_productsegment2id { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_productsegment2idname { get; set; }

		public Guid ktb_productsegment3id { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_productsegment3idname { get; set; }

		public Guid ktb_productsegment4id { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_productsegment4idname { get; set; }

		public Guid modifiedby { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedbyname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedbyyominame { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		public DateTime? overriddencreatedon { get; set; }

		public Guid ownerid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string owneridname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string owneridtype { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string owneridyominame { get; set; }

		public Guid owningbusinessunit { get; set; }

		public Guid owningteam { get; set; }

		public Guid owninguser { get; set; }

		public Guid processid { get; set; }

		//[StringLength(255, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		//public string RowStatus { get; set; }

		[StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string SourceType { get; set; }

		public Guid stageid { get; set; }

		public int? statecode { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string statecodename { get; set; }

		public int? statuscode { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string statuscodename { get; set; }

		public int? timezoneruleversionnumber { get; set; }

		public Guid transactioncurrencyid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string traversedpath { get; set; }

		public int? utcconversiontimezonecode { get; set; }

		public long? versionnumber { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_aliasname { get; set; }

		//[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public int? xts_arrivalpattern { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_arrivalpatternname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_arrivalpostalcode { get; set; }

		public Guid xts_businessunitid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_cancelreason { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_chassismodel { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_chassisnumber { get; set; }

		public Guid xts_contactpersonid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_contactpersonidname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_contactpersonidyominame { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_contactpersonphone { get; set; }

		public Guid xts_customerid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_customeridname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_customeridyominame { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_customernumber { get; set; }

		public Guid xts_defaultoutsourceworkshopid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_defaultoutsourceworkshopidname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_deliveryaddress1 { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_deliveryaddress2 { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_deliveryaddress3 { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_deliveryaddress4 { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_deliverypostalcode { get; set; }

		public Guid xts_estimatebayid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_estimatebayidname { get; set; }

		//[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public int? xts_finishpattern { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_finishpatternname { get; set; }

		public Guid xts_globalserviceid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_globalserviceidname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_handling { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_handlingname { get; set; }

		public Guid xts_incomingoutsourcebusinessunitid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_incomingoutsourcebusinessunitidname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_insurancecontract { get; set; }

		public Guid xts_loadinggroupid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_loadinggroupidname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_locking { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_maintenancepackageinformation { get; set; }

		public Guid xts_ordertypeid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_ordertypeidname { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		public Guid xts_personinchargeforarrivalid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_personinchargeforarrivalidname { get; set; }

		public Guid xts_personinchargefordeliveryid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_personinchargefordeliveryidname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_pickupaddress1 { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_pickupaddress2 { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_pickupaddress3 { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_pickupaddress4 { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_platenumber { get; set; }

		public Guid xts_previousreservationid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_previousreservationidname { get; set; }

		public Guid xts_reservationclassid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_reservationclassidname { get; set; }

		public decimal? xts_reservationmanhour { get; set; }

		public string xts_reservationmemo { get; set; }

		public Guid xts_reservationpersoninchargeid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_reservationpersoninchargeidname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_reservationtransaction { get; set; }

		public Guid xts_reservationtransactionid { get; set; }

		public Guid xts_salespersonid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_salespersonidname { get; set; }

		public DateTime? xts_scheduledarrivaldateandtime { get; set; }

		public DateTime? xts_scheduledfinishdateandtime { get; set; }

		public DateTime? xts_scheduledservicestartdate { get; set; }

		public Guid xts_servicecategoryid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_servicecategoryidname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_servicepackagecontract { get; set; }

		//[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public int? xts_status { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_statusname { get; set; }

		public DateTime? xts_transactiondate { get; set; }

		public Guid xts_vehicleidentificationid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_vehicleidentificationidname { get; set; }

		public bool? xts_vehicleincludedintherecall { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_vehicleincludedintherecallname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_vehiclemodelname { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_vehicleplatenumber { get; set; }

		public Guid xts_workorderid { get; set; }

		[StringLength(1000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_workorderidname { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }

    public class CRM_xts_reservationtransactionDeleteParameterDto
	{
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public Guid xts_reservationtransactionid { get; set; }

		#region ModifiedOn ModifiedBy Delete
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public DateTime? modifiedon { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public Guid modifiedby { get; set; }
		#endregion
	}

	public class CRM_xts_reservationtransactionUpdateParameterDto : CRM_xts_reservationtransactionParameterDto
    {
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_reservationtransactionid { get { return base.xts_reservationtransactionid; } set { base.xts_reservationtransactionid = value; } }

		#region ModifiedOn ModifiedBy Update
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? modifiedon { get { return base.modifiedon; } set { base.modifiedon = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid modifiedby { get { return base.modifiedby; } set { base.modifiedby = value; } }
		#endregion
	}

	public class CRM_xts_reservationtransactionCreateParameterDto : CRM_xts_reservationtransactionParameterDto
    {
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid ownerid { get { return base.ownerid; } set { base.ownerid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new int? statecode { get { return base.statecode; } set { base.statecode = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new int? xts_arrivalpattern { get { return base.xts_arrivalpattern; } set { base.xts_arrivalpattern = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_businessunitid { get { return base.xts_businessunitid; } set { base.xts_businessunitid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_customerid { get { return base.xts_customerid; } set { base.xts_customerid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new int? xts_finishpattern { get { return base.xts_finishpattern; } set { base.xts_finishpattern = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_ordertypeid { get { return base.xts_ordertypeid; } set { base.xts_ordertypeid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_reservationclassid { get { return base.xts_reservationclassid; } set { base.xts_reservationclassid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new decimal? xts_reservationmanhour { get { return base.xts_reservationmanhour; } set { base.xts_reservationmanhour = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? xts_scheduledarrivaldateandtime { get { return base.xts_scheduledarrivaldateandtime; } set { base.xts_scheduledarrivaldateandtime = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? xts_scheduledfinishdateandtime { get { return base.xts_scheduledfinishdateandtime; } set { base.xts_scheduledfinishdateandtime = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? xts_scheduledservicestartdate { get { return base.xts_scheduledservicestartdate; } set { base.xts_scheduledservicestartdate = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? xts_transactiondate { get { return base.xts_transactiondate; } set { base.xts_transactiondate = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_vehicleidentificationid { get { return base.xts_vehicleidentificationid; } set { base.xts_vehicleidentificationid = value; } }

		#region Createdon CreatedBy ModifiedOn ModifiedBy
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? createdon { get { return base.createdon; } set { base.createdon = value; } }

		//[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid createdby { get { return base.createdby; } set { base.createdby = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? modifiedon { get { return base.modifiedon; } set { base.modifiedon = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid modifiedby { get { return base.modifiedby; } set { base.modifiedby = value; } }
		#endregion
	}

}
