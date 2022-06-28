#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_customerpublicParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/01/2020 14:19:26
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
    public class VWI_CRM_xts_customerpublicParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string xts_shortname { get; set; }

		[AntiXss]
		public string xjp_ownershippositionname { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public Guid xts_villageandstreetid { get; set; }

		[AntiXss]
		public Guid xts_provinceid { get; set; }

		[AntiXss]
		public string xts_categoryidname { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public bool xts_sharepersonalinformation { get; set; }

		[AntiXss]
		public Guid xts_customerclassid { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public Guid xts_customerpublicid { get; set; }

		[AntiXss]
		public Guid xts_ownershipid { get; set; }

		[AntiXss]
		public string xts_mobilephone { get; set; }

		[AntiXss]
		public string xts_salutation { get; set; }

		[AntiXss]
		public string ktb_fleetcode { get; set; }

		[AntiXss]
		public string xts_provinceidname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_customerclassidname { get; set; }

		[AntiXss]
		public string xts_fax { get; set; }

		[AntiXss]
		public string xts_address2 { get; set; }

		[AntiXss]
		public string xts_sharepersonalinformationname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xts_countryidname { get; set; }

		[AntiXss]
		public string xts_email3 { get; set; }

		[AntiXss]
		public string xts_address4 { get; set; }

		[AntiXss]
		public string xts_ownershipidname { get; set; }

		[AntiXss]
		public string xts_phonepriority { get; set; }

		[AntiXss]
		public Guid xts_jobtitleid { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_identificationtypename { get; set; }

		[AntiXss]
		public DateTime xts_birthdate { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string xts_email1 { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xts_homephone { get; set; }

		[AntiXss]
		public string xts_customerpublicnumber { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_villageandstreetidname { get; set; }

		[AntiXss]
		public string xts_otherphone { get; set; }

		[AntiXss]
		public Guid xts_countryid { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public Guid xts_cityid { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string ktb_customercodesap { get; set; }

		[AntiXss]
		public string xts_companyname { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_businessphone { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xts_jobtitleidname { get; set; }

		[AntiXss]
		public string xts_identificationnumber { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xts_firstname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_address3 { get; set; }

		[AntiXss]
		public string xts_cityidname { get; set; }

		[AntiXss]
		public string xts_aliasname { get; set; }

		[AntiXss]
		public string xts_gender { get; set; }

		[AntiXss]
		public string xjp_companycode { get; set; }

		[AntiXss]
		public string xts_postalcode { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_address1 { get; set; }

		[AntiXss]
		public string xts_phonepriorityname { get; set; }

		[AntiXss]
		public string xts_email4 { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string xts_customer { get; set; }

		[AntiXss]
		public string xts_gendername { get; set; }

		[AntiXss]
		public string xts_lastname { get; set; }

		[AntiXss]
		public string xts_integrationnumber { get; set; }

		[AntiXss]
		public string xts_industryidname { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid organizationid { get; set; }

		[AntiXss]
		public Guid xts_industryid { get; set; }

		[AntiXss]
		public string xjp_ownershipposition { get; set; }

		[AntiXss]
		public Guid xts_categoryid { get; set; }

		[AntiXss]
		public string organizationidname { get; set; }

		[AntiXss]
		public string xts_email2 { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_identificationtype { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
