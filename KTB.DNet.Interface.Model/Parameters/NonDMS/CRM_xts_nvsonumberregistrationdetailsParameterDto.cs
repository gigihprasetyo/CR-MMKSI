#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_nvsonumberregistrationdetails class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 05 Feb 2021 15:16:48
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
    public class CRM_xts_nvsonumberregistrationdetailsParameterDto : ParameterDtoBase, IValidatableObject
    {
        
		public Guid createdby { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdbyname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdbyyominame { get; set; }

		public DateTime? createdon { get; set; }

		public Guid createdonbehalfby { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string DealerCode { get; set; }

		public int? importsequencenumber { get; set; }

		public Guid ktb_validspkid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_validspkidname { get; set; }

		public Guid modifiedby { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedbyname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedbyyominame { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		public DateTime? overriddencreatedon { get; set; }

		public Guid ownerid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string owneridname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string owneridtype { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string owneridyominame { get; set; }

		public Guid owningbusinessunit { get; set; }

		public Guid owningteam { get; set; }

		public Guid owninguser { get; set; }

		//[StringLength(255, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		//public string RowStatus { get; set; }

		[StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string SourceType { get; set; }

		public int? statecode { get; set; }

		public string statecodename { get; set; }

		public int? statuscode { get; set; }

		public string statuscodename { get; set; }

		public int? timezoneruleversionnumber { get; set; }

		public int? utcconversiontimezonecode { get; set; }

		public long? versionnumber { get; set; }

		public DateTime? xts_assigndate { get; set; }

		public DateTime? xts_assigntosalesdate { get; set; }

		public Guid xts_businessunitid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_businessunitidname { get; set; }

		public int? xts_lastnumber { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_locking { get; set; }

		public Guid xts_nvsonumberregistration { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_nvsonumberregistrationdetails { get; set; }

		public Guid xts_nvsonumberregistrationdetailsid { get; set; }

		public Guid xts_nvsonumberregistrationid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_nvsonumberregistrationidname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_nvsonumberregistrationname { get; set; }

		public int? xts_nvsoserial { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_pkcombinationkey { get; set; }

		public Guid xts_salespersonid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_salespersonidname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_status { get; set; }

		public string xts_statusname { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }

    public class CRM_xts_nvsonumberregistrationdetailsDeleteParameterDto
	{
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public Guid xts_nvsonumberregistrationdetailsid { get; set; }

		#region ModifiedOn ModifiedBy Delete
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public DateTime? modifiedon { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public Guid modifiedby { get; set; }
		#endregion
	}

	public class CRM_xts_nvsonumberregistrationdetailsUpdateParameterDto : CRM_xts_nvsonumberregistrationdetailsParameterDto
    {
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_nvsonumberregistrationdetailsid { get { return base.xts_nvsonumberregistrationdetailsid; } set { base.xts_nvsonumberregistrationdetailsid = value; } }

		#region ModifiedOn ModifiedBy Update
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? modifiedon { get { return base.modifiedon; } set { base.modifiedon = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid modifiedby { get { return base.modifiedby; } set { base.modifiedby = value; } }
		#endregion
	}

	public class CRM_xts_nvsonumberregistrationdetailsCreateParameterDto : CRM_xts_nvsonumberregistrationdetailsParameterDto
    {

		//[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		//public new Guid xts_salespersonid { get { return base.xts_salespersonid; } set { base.xts_salespersonid = value; } }

		//[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		//public new string xts_status { get { return base.xts_status; } set { base.xts_status = value; } }

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