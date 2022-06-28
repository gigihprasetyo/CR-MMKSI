#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_serviceappointment class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 19 Jan 2021 09:35:16
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
    public class CRM_serviceappointmentParameterDto : ParameterDtoBase, IValidatableObject
    {
        
		public string activityadditionalparams { get; set; }

		public Guid activityid { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string activitytypecode { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string activitytypecodename { get; set; }

		public int? actualdurationminutes { get; set; }

		public DateTime? actualend { get; set; }

		public DateTime? actualstart { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string bcc { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string category { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string cc { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string community { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string communityname { get; set; }

		public Guid createdby { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdbyname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdbyyominame { get; set; }

		public DateTime? createdon { get; set; }

		public Guid createdonbehalfby { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string customers { get; set; }

		[StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string DealerCode { get; set; }

		public DateTime? deliverylastattemptedon { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string deliveryprioritycode { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string deliveryprioritycodename { get; set; }

		public string description { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string exchangeitemid { get; set; }

		public decimal? exchangerate { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string exchangeweblink { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string from { get; set; }

		public int? importsequencenumber { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string instancetypecode { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string instancetypecodename { get; set; }

		public bool? isalldayevent { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string isalldayeventname { get; set; }

		public bool? isbilled { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string isbilledname { get; set; }

		public bool? ismapiprivate { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ismapiprivatename { get; set; }

		public bool? isregularactivity { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string isregularactivityname { get; set; }

		public bool? isworkflowcreated { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string isworkflowcreatedname { get; set; }

		public DateTime? lastonholdtime { get; set; }

		public bool? leftvoicemail { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string leftvoicemailname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string location { get; set; }

		public Guid modifiedby { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedbyname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedbyyominame { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		public Guid msdyn_organizationalunitid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string msdyn_organizationalunitidname { get; set; }

		public int? onholdtime { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string optionalattendees { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string organizer { get; set; }

		public DateTime? overriddencreatedon { get; set; }

		public Guid ownerid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string owneridname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string owneridtype { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string owneridyominame { get; set; }

		public Guid owningbusinessunit { get; set; }

		public Guid owningteam { get; set; }

		public Guid owninguser { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string partners { get; set; }

		public DateTime? postponeactivityprocessinguntil { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string prioritycode { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string prioritycodename { get; set; }

		public Guid processid { get; set; }

		public Guid regardingobjectid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string regardingobjectidname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string regardingobjectidyominame { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string regardingobjecttypecode { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string requiredattendees { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string resources { get; set; }

		//[StringLength(255, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		//public string RowStatus { get; set; }

		public int? scheduleddurationminutes { get; set; }

		public DateTime? scheduledend { get; set; }

		public DateTime? scheduledstart { get; set; }

		public Guid sendermailboxid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string sendermailboxidname { get; set; }

		public DateTime? senton { get; set; }

		public Guid seriesid { get; set; }

		public Guid serviceid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string serviceidname { get; set; }

		public Guid siteid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string siteidname { get; set; }

		public Guid slaid { get; set; }

		public Guid slainvokedid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string slainvokedidname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string slaname { get; set; }

		public DateTime? sortdate { get; set; }

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

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string subcategory { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string subject { get; set; }

		public Guid subscriptionid { get; set; }

		public int? timezoneruleversionnumber { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string to { get; set; }

		public Guid transactioncurrencyid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string traversedpath { get; set; }

		public int? utcconversiontimezonecode { get; set; }

		public long? versionnumber { get; set; }

		public bool? xts_billable { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_billablename { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_billabletype { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_billabletypename { get; set; }

		public Guid xts_businessunitid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_businessunitidname { get; set; }

		public Guid xts_costcenterid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_costcenteridname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_freecouponnumber { get; set; }

		public bool? xts_isfromwoquote { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_isfromwoquotename { get; set; }

		public Guid xts_locationid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_locationidname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_locking { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_maindealerinvoicegroup { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_maindealerinvoicegroupname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_nonbillabletype { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_nonbillabletypename { get; set; }

		public Guid xts_reasonid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_reasonidname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_servicetype { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_servicetypename { get; set; }

		public decimal? xts_totallaborcost { get; set; }

		public decimal? xts_totallaborcost_base { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }

    public class CRM_serviceappointmentDeleteParameterDto
	{
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public Guid activityid { get; set; }

		#region ModifiedOn ModifiedBy Delete
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public DateTime? modifiedon { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public Guid modifiedby { get; set; }
		#endregion
	}

	public class CRM_serviceappointmentUpdateParameterDto : CRM_serviceappointmentParameterDto
    {
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid activityid { get { return base.activityid; } set { base.activityid = value; } }

		#region ModifiedOn ModifiedBy Update
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? modifiedon { get { return base.modifiedon; } set { base.modifiedon = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid modifiedby { get { return base.modifiedby; } set { base.modifiedby = value; } }
		#endregion
	}

	public class CRM_serviceappointmentCreateParameterDto : CRM_serviceappointmentParameterDto
    {
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid ownerid { get { return base.ownerid; } set { base.ownerid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_businessunitid { get { return base.xts_businessunitid; } set { base.xts_businessunitid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? scheduledend { get { return base.scheduledend; } set { base.scheduledend = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? scheduledstart { get { return base.scheduledstart; } set { base.scheduledstart = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid serviceid { get { return base.serviceid; } set { base.serviceid = value; } }

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