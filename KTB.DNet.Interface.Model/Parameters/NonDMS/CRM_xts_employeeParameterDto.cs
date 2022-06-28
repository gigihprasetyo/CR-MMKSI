#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_employee class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 13:42:55
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
    public class CRM_xts_employeeParameterDto : ParameterDtoBase, IValidatableObject
    {
        
		public int? bsi_nomorsepatu { get; set; }

		public decimal? bsi_nomorsepatukoma { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string bsi_rejectedreason { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string bsi_resignreason { get; set; }

		public string bsi_resignreasonname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string bsi_resignstatusdnet { get; set; }

		public string bsi_resignstatusdnetname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string bsi_resigntype { get; set; }

		public string bsi_resigntypename { get; set; }

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

		public string entityimage { get; set; }

		public long? entityimage_timestamp { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string entityimage_url { get; set; }

		public Guid entityimageid { get; set; }

		public int? importsequencenumber { get; set; }

		public Guid ktb_birthplace { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_birthplacename { get; set; }

		//[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public int? ktb_category { get; set; }

		public string ktb_categoryname { get; set; }

		//[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public int? ktb_education { get; set; }

		public string ktb_educationname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_employeecode { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_id { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_identificationno { get; set; }

		//[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public int? ktb_identificationtype { get; set; }

		public string ktb_identificationtypename { get; set; }

		public string ktb_interfaceexceptionmessage { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_interfacehandling { get; set; }

		public string ktb_interfacehandlingname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_interfacestatus { get; set; }

		public string ktb_interfacestatusname { get; set; }

		public bool? ktb_isinterfaced { get; set; }

		public string ktb_isinterfacedname { get; set; }

		public Guid ktb_jobpositionid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_jobpositionidname { get; set; }

		//[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public int? ktb_kategoriteam { get; set; }

		public string ktb_kategoriteamname { get; set; }

		//[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public int? ktb_maritalstatus { get; set; }

		public string ktb_maritalstatusname { get; set; }

		public bool? ktb_outsource { get; set; }

		public string ktb_outsourcename { get; set; }

		//[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public int? ktb_position { get; set; }

		public string ktb_positionname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_religion { get; set; }

		public string ktb_religionname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_resignreason { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_resigntype { get; set; }

		public string ktb_resigntypename { get; set; }

		public int? ktb_salary { get; set; }

		//[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public int? ktb_salesmanarea { get; set; }

		public string ktb_salesmanareaname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_salesmancodereff { get; set; }

		//[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public int? ktb_salesmanlevel { get; set; }

		public string ktb_salesmanlevelname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_salespersoncode { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_shirtsize { get; set; }

		public Guid ktb_superiors { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_superiorscode { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_superiorsname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_ukuranbaju { get; set; }

		public string ktb_ukuranbajuname { get; set; }

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

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public string xts_address1 { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public string xts_address2 { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public string xts_address3 { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public string xts_address4 { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public string xts_aliasname { get; set; }

		public DateTime? xts_birthdate { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_businessphone { get; set; }

		public Guid xts_businessunitid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_businessunitidname { get; set; }

		public Guid xts_cityid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_cityidname { get; set; }

		public Guid xts_countryid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_countryidname { get; set; }

		public Guid xts_departmentid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_departmentidname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_description { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_email { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_employee { get; set; }

		public Guid xts_employeeclassid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_employeeclassidname { get; set; }

		//[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public int? xts_employeeclasstype { get; set; }

		public string xts_employeeclasstypename { get; set; }

		public Guid xts_employeeid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_fax { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public string xts_firstname { get; set; }

		//[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public int? xts_gender { get; set; }

		public string xts_gendername { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_homephone { get; set; }

		public DateTime? xts_joindate { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public string xts_lastname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_locking { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_mobilephone { get; set; }

		public int? xts_newvehiclereservationbalance { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_otherphone { get; set; }

		public Guid xts_parentemployeeid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_parentemployeeidname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_permanent { get; set; }

		public string xts_permanentname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_pkcombinationkey { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_position { get; set; }

		public string xts_positionname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_postalcode { get; set; }

		public Guid xts_provinceid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_provinceidname { get; set; }

		public Guid xts_regardinguserid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_regardinguseridname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_regardinguseridyominame { get; set; }

		public DateTime? xts_resigndate { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_salutation { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_shortname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_taxregistrationnumber { get; set; }

		public Guid xts_villageandstreetid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_villageandstreetidname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_workingstatus { get; set; }

		public string xts_workingstatusname { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }

    public class CRM_xts_employeeDeleteParameterDto
	{
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public Guid xts_employeeid { get; set; }

		#region ModifiedOn ModifiedBy Delete
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public DateTime? modifiedon { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public Guid modifiedby { get; set; }
		#endregion
	}

	public class CRM_xts_employeeUpdateParameterDto : CRM_xts_employeeParameterDto
    {
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_employeeid { get { return base.xts_employeeid; } set { base.xts_employeeid = value; } }

		#region ModifiedOn ModifiedBy Update
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? modifiedon { get { return base.modifiedon; } set { base.modifiedon = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid modifiedby { get { return base.modifiedby; } set { base.modifiedby = value; } }
		#endregion
	}

	public class CRM_xts_employeeCreateParameterDto : CRM_xts_employeeParameterDto
    {
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new string ktb_employeecode { get { return base.ktb_employeecode; } set { base.ktb_employeecode = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new string ktb_identificationno { get { return base.ktb_identificationno; } set { base.ktb_identificationno = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new int? ktb_identificationtype { get { return base.ktb_identificationtype; } set { base.ktb_identificationtype = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new int? ktb_position { get { return base.ktb_position; } set { base.ktb_position = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid ownerid { get { return base.ownerid; } set { base.ownerid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new int? statecode { get { return base.statecode; } set { base.statecode = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? xts_birthdate { get { return base.xts_birthdate; } set { base.xts_birthdate = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_businessunitid { get { return base.xts_businessunitid; } set { base.xts_businessunitid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new string xts_email { get { return base.xts_email; } set { base.xts_email = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new string xts_firstname { get { return base.xts_firstname; } set { base.xts_firstname = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new int? xts_gender { get { return base.xts_gender; } set { base.xts_gender = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? xts_joindate { get { return base.xts_joindate; } set { base.xts_joindate = value; } }

		//[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		//public new string ktb_category { get { return base.ktb_category; } set { base.ktb_category = value; } }

		//[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		//public new string ktb_kategoriteam { get { return base.ktb_kategoriteam; } set { base.ktb_kategoriteam = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new int? ktb_maritalstatus { get { return base.ktb_maritalstatus; } set { base.ktb_maritalstatus = value; } }

		//[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		//public new string ktb_salesmanarea { get { return base.ktb_salesmanarea; } set { base.ktb_salesmanarea = value; } }

		//[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		//public new string ktb_salesmanlevel { get { return base.ktb_salesmanlevel; } set { base.ktb_salesmanlevel = value; } }

		//[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		//public new Guid ktb_superiors { get { return base.ktb_superiors; } set { base.ktb_superiors = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new string xts_address1 { get { return base.xts_address1; } set { base.xts_address1 = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_cityid { get { return base.xts_cityid; } set { base.xts_cityid = value; } }

		//[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		//public new string xts_employeeclasstype { get { return base.xts_employeeclasstype; } set { base.xts_employeeclasstype = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_provinceid { get { return base.xts_provinceid; } set { base.xts_provinceid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new string xts_mobilephone { get { return base.xts_mobilephone; } set { base.xts_mobilephone = value; } }

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