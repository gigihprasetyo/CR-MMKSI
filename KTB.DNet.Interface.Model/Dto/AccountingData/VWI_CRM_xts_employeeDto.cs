#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_employeeDto  class
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
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_employeeDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }
		public string ktb_externalcode { get; set; }		

		public string ktb_salesmanarea { get; set; }

		public string xts_salutation { get; set; }

		public string xts_pkcombinationkey { get; set; }

		public string ktb_salespersoncode { get; set; }

		public DateTime modifiedon { get; set; }

		public string ktb_ukuranbaju { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string statuscodename { get; set; }

		public string ktb_salesmanlevelname { get; set; }

		public Int64 entityimage_timestamp { get; set; }

		public string modifiedbyyominame { get; set; }

		public string ktb_superiorsname { get; set; }

		public DateTime xts_resigndate { get; set; }

		public string xts_mobilephone { get; set; }

		public string xts_regardinguseridyominame { get; set; }

		public string ktb_positionname { get; set; }

		public string ktb_interfacehandling { get; set; }

		public string bsi_resigntype { get; set; }

		public string bsi_resigntypename { get; set; }

		public string xts_employeeclassidname { get; set; }

		public string xts_regardinguseridname { get; set; }

		public string xts_lastname { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string modifiedbyname { get; set; }

		public string bsi_resignstatusdnet { get; set; }

		public string ktb_educationname { get; set; }

		public string ktb_kategoriteamname { get; set; }

		public bool ktb_isinterfaced { get; set; }

		public string xts_employeeclasstypename { get; set; }

		public Guid xts_employeeclassid { get; set; }

		public string ktb_interfaceexceptionmessage { get; set; }

		public string ktb_education { get; set; }

		public int statuscode { get; set; }

		public string bsi_resignreason { get; set; }

		public string ktb_religion { get; set; }

		public Int64 versionnumber { get; set; }

		public string statecodename { get; set; }

		public DateTime xts_joindate { get; set; }

		public string xts_departmentidname { get; set; }

		public string xts_taxregistrationnumber { get; set; }

		public Guid ktb_jobpositionid { get; set; }

		public DateTime xts_birthdate { get; set; }

		public DateTime createdon { get; set; }

		public string ktb_maritalstatusname { get; set; }

		public string xts_workingstatus { get; set; }

		public Guid xts_employeeid { get; set; }

		public int bsi_nomorsepatu { get; set; }

		public string ktb_shirtsize { get; set; }

		public string ktb_salesmanareaname { get; set; }

		public string bsi_resignreasonname { get; set; }

		public string owneridyominame { get; set; }

		public string entityimage_url { get; set; }

		public Guid xts_countryid { get; set; }

		public Guid modifiedby { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string xts_address1 { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public Guid ktb_superiors { get; set; }

		public string ktb_resignreason { get; set; }

		public string ktb_outsourcename { get; set; }

		public string ktb_jobpositionidname { get; set; }

		public string ktb_kategoriteam { get; set; }

		public string ktb_position { get; set; }

		public string xts_workingstatusname { get; set; }

		public bool ktb_outsource { get; set; }

		public string ktb_salesmanlevel { get; set; }

		public string ktb_categoryname { get; set; }

		public string xts_aliasname { get; set; }

		public string createdonbehalfbyname { get; set; }

		public Guid owninguser { get; set; }

		public Guid xts_departmentid { get; set; }

		public string ktb_interfacehandlingname { get; set; }

		public string owneridtype { get; set; }

		public string xts_employee { get; set; }

		public string ktb_interfacestatusname { get; set; }

		public Guid entityimageid { get; set; }

		public string ktb_birthplacename { get; set; }

		public string ktb_resigntype { get; set; }

		public string owneridname { get; set; }

		public string entityimage { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string xts_fax { get; set; }

		public string ktb_maritalstatus { get; set; }

		public Guid owningteam { get; set; }

		public string xts_firstname { get; set; }

		public string xts_address3 { get; set; }

		public string xts_parentemployeeidname { get; set; }

		public string xts_businessphone { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string ktb_resigntypename { get; set; }

		public string xts_postalcode { get; set; }

		public decimal bsi_nomorsepatukoma { get; set; }

		public string ktb_category { get; set; }

		public Guid xts_cityid { get; set; }

		public string xts_description { get; set; }

		public string bsi_rejectedreason { get; set; }

		public string ktb_isinterfacedname { get; set; }

		public string xts_position { get; set; }

		public Guid ownerid { get; set; }

		public string ktb_identificationtypename { get; set; }

		public string xts_permanentname { get; set; }

		public string xts_provinceidname { get; set; }

		public Guid ktb_birthplace { get; set; }

		public string createdbyname { get; set; }

		public string ktb_identificationno { get; set; }

		public int xts_newvehiclereservationbalance { get; set; }

		public string xts_businessunitidname { get; set; }

		public int statecode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public Guid createdby { get; set; }

		public string xts_villageandstreetidname { get; set; }

		public string ktb_id { get; set; }

		public string bsi_resignstatusdnetname { get; set; }

		public string xts_permanent { get; set; }

		public string xts_homephone { get; set; }

		public string xts_countryidname { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string xts_shortname { get; set; }

		public Guid xts_provinceid { get; set; }

		public int ktb_salary { get; set; }

		public Guid xts_regardinguserid { get; set; }

		public int importsequencenumber { get; set; }

		public string xts_locking { get; set; }

		public string ktb_interfacestatus { get; set; }

		public string xts_email { get; set; }

		public string xts_gendername { get; set; }

		public string xts_address2 { get; set; }

		public Guid xts_businessunitid { get; set; }

		public Guid xts_parentemployeeid { get; set; }

		public string xts_employeeclasstype { get; set; }

		public string xts_gender { get; set; }

		public string ktb_ukuranbajuname { get; set; }

		public string createdbyyominame { get; set; }

		public string ktb_salesmancodereff { get; set; }

		public string xts_positionname { get; set; }

		public string xts_address4 { get; set; }

		public string xts_cityidname { get; set; }

		public string ktb_identificationtype { get; set; }

		public string ktb_employeecode { get; set; }

		public string ktb_religionname { get; set; }

		public string ktb_superiorscode { get; set; }

		public Guid xts_villageandstreetid { get; set; }

		public string xts_otherphone { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
