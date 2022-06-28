#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_employeeParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/01/2020 8:22:48
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
    public class VWI_CRM_xts_employeeParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string ktb_salesmanarea { get; set; }

		[AntiXss]
		public string xts_salutation { get; set; }

		[AntiXss]
		public string xts_pkcombinationkey { get; set; }

		[AntiXss]
		public string ktb_salespersoncode { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public string ktb_ukuranbaju { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string ktb_salesmanlevelname { get; set; }

		[AntiXss]
		public Int64 entityimage_timestamp { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string ktb_superiorsname { get; set; }

		[AntiXss]
		public DateTime xts_resigndate { get; set; }

		[AntiXss]
		public string xts_mobilephone { get; set; }

		[AntiXss]
		public string xts_regardinguseridyominame { get; set; }

		[AntiXss]
		public string ktb_positionname { get; set; }

		[AntiXss]
		public string ktb_interfacehandling { get; set; }

		[AntiXss]
		public string bsi_resigntype { get; set; }

		[AntiXss]
		public string bsi_resigntypename { get; set; }

		[AntiXss]
		public string xts_employeeclassidname { get; set; }

		[AntiXss]
		public string xts_regardinguseridname { get; set; }

		[AntiXss]
		public string xts_lastname { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string bsi_resignstatusdnet { get; set; }

		[AntiXss]
		public string ktb_educationname { get; set; }

		[AntiXss]
		public string ktb_kategoriteamname { get; set; }

		[AntiXss]
		public bool ktb_isinterfaced { get; set; }

		[AntiXss]
		public string xts_employeeclasstypename { get; set; }

		[AntiXss]
		public Guid xts_employeeclassid { get; set; }

		[AntiXss]
		public string ktb_interfaceexceptionmessage { get; set; }

		[AntiXss]
		public string ktb_education { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string bsi_resignreason { get; set; }

		[AntiXss]
		public string ktb_religion { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public DateTime xts_joindate { get; set; }

		[AntiXss]
		public string xts_departmentidname { get; set; }

		[AntiXss]
		public string xts_taxregistrationnumber { get; set; }

		[AntiXss]
		public Guid ktb_jobpositionid { get; set; }

		[AntiXss]
		public DateTime xts_birthdate { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string ktb_maritalstatusname { get; set; }

		[AntiXss]
		public string xts_workingstatus { get; set; }

		[AntiXss]
		public Guid xts_employeeid { get; set; }

		[AntiXss]
		public int bsi_nomorsepatu { get; set; }

		[AntiXss]
		public string ktb_shirtsize { get; set; }

		[AntiXss]
		public string ktb_salesmanareaname { get; set; }

		[AntiXss]
		public string bsi_resignreasonname { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string entityimage_url { get; set; }

		[AntiXss]
		public Guid xts_countryid { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string xts_address1 { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public Guid ktb_superiors { get; set; }

		[AntiXss]
		public string ktb_resignreason { get; set; }

		[AntiXss]
		public string ktb_outsourcename { get; set; }

		[AntiXss]
		public string ktb_jobpositionidname { get; set; }

		[AntiXss]
		public string ktb_kategoriteam { get; set; }

		[AntiXss]
		public string ktb_position { get; set; }

		[AntiXss]
		public string xts_workingstatusname { get; set; }

		[AntiXss]
		public bool ktb_outsource { get; set; }

		[AntiXss]
		public string ktb_salesmanlevel { get; set; }

		[AntiXss]
		public string ktb_categoryname { get; set; }

		[AntiXss]
		public string xts_aliasname { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public Guid xts_departmentid { get; set; }

		[AntiXss]
		public string ktb_interfacehandlingname { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xts_employee { get; set; }

		[AntiXss]
		public string ktb_interfacestatusname { get; set; }

		[AntiXss]
		public Guid entityimageid { get; set; }

		[AntiXss]
		public string ktb_birthplacename { get; set; }

		[AntiXss]
		public string ktb_resigntype { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string entityimage { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_fax { get; set; }

		[AntiXss]
		public string ktb_maritalstatus { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_firstname { get; set; }

		[AntiXss]
		public string xts_address3 { get; set; }

		[AntiXss]
		public string xts_parentemployeeidname { get; set; }

		[AntiXss]
		public string xts_businessphone { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string ktb_resigntypename { get; set; }

		[AntiXss]
		public string xts_postalcode { get; set; }

		[AntiXss]
		public decimal bsi_nomorsepatukoma { get; set; }

		[AntiXss]
		public string ktb_category { get; set; }

		[AntiXss]
		public Guid xts_cityid { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public string bsi_rejectedreason { get; set; }

		[AntiXss]
		public string ktb_isinterfacedname { get; set; }

		[AntiXss]
		public string xts_position { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public string ktb_identificationtypename { get; set; }

		[AntiXss]
		public string xts_permanentname { get; set; }

		[AntiXss]
		public string xts_provinceidname { get; set; }

		[AntiXss]
		public Guid ktb_birthplace { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string ktb_identificationno { get; set; }

		[AntiXss]
		public int xts_newvehiclereservationbalance { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string xts_villageandstreetidname { get; set; }

		[AntiXss]
		public string ktb_id { get; set; }

		[AntiXss]
		public string bsi_resignstatusdnetname { get; set; }

		[AntiXss]
		public string xts_permanent { get; set; }

		[AntiXss]
		public string xts_homephone { get; set; }

		[AntiXss]
		public string xts_countryidname { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_shortname { get; set; }

		[AntiXss]
		public Guid xts_provinceid { get; set; }

		[AntiXss]
		public int ktb_salary { get; set; }

		[AntiXss]
		public Guid xts_regardinguserid { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string ktb_interfacestatus { get; set; }

		[AntiXss]
		public string xts_email { get; set; }

		[AntiXss]
		public string xts_gendername { get; set; }

		[AntiXss]
		public string xts_address2 { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public Guid xts_parentemployeeid { get; set; }

		[AntiXss]
		public string xts_employeeclasstype { get; set; }

		[AntiXss]
		public string xts_gender { get; set; }

		[AntiXss]
		public string ktb_ukuranbajuname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string ktb_salesmancodereff { get; set; }

		[AntiXss]
		public string xts_positionname { get; set; }

		[AntiXss]
		public string xts_address4 { get; set; }

		[AntiXss]
		public string xts_cityidname { get; set; }

		[AntiXss]
		public string ktb_identificationtype { get; set; }

		[AntiXss]
		public string ktb_employeecode { get; set; }

		[AntiXss]
		public string ktb_religionname { get; set; }

		[AntiXss]
		public string ktb_superiorscode { get; set; }

		[AntiXss]
		public Guid xts_villageandstreetid { get; set; }

		[AntiXss]
		public string xts_otherphone { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
