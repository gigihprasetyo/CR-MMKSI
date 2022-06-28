#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_serviceworkshopsettingferenceDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 09:29:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_serviceworkshopsettingDto : DtoBase
    {
        public string company { get; set; }
        public string businessunitcode { get; set; }
        public DateTime modifiedon { get; set; }
        public int timezoneruleversionnumber { get; set; }
        public string xts_startsundayworkingdays { get; set; }
        public Guid xts_defaultmmstemplateid { get; set; }
        public string xts_ownerbusinessname { get; set; }
        public string xts_tuesdayworkingdaysname { get; set; }
        public Guid xts_provinceid { get; set; }
        public string xts_startmondaybreakdays { get; set; }
        public string createdbyname { get; set; }
        public string createdonbehalfbyyominame { get; set; }
        public string xjp_certificatedbusinessnumber { get; set; }
        public Guid createdby { get; set; }
        public string xts_endthursdaybreakdays { get; set; }
        public string xjp_reportformatname { get; set; }
        public string xjp_designatedbusinessnumber { get; set; }
        public Guid owningteam { get; set; }
        public Guid owningbusinessunit { get; set; }
        public string xjp_maintenanceworkshop { get; set; }
        public string xts_provinceidname { get; set; }
        public string modifiedbyname { get; set; }
        public Guid xjp_securitystandardissuerid { get; set; }
        public string xts_businessunitidname { get; set; }
        public bool xts_tuesdayworkingdays { get; set; }
        public string xts_postalcode { get; set; }
        public string modifiedonbehalfbyyominame { get; set; }
        public Guid ownerid { get; set; }
        public string xts_phone { get; set; }
        public Guid xts_serviceworkshopsettingid { get; set; }
        public string xts_sundayworkingdaysname { get; set; }
        public bool xts_sundayworkingdays { get; set; }
        public string modifiedonbehalfbyname { get; set; }
        public bool xts_fridayworkingdays { get; set; }
        public string xts_defaultcirclechecktemplateidname { get; set; }
        public string xts_cityidname { get; set; }
        public string xts_endsaturdayworkingdays { get; set; }
        public string owneridyominame { get; set; }
        public string xts_starttuesdaybreakdays { get; set; }
        public string xts_startfridaybreakdays { get; set; }
        public string xts_startwednesdayworkingdays { get; set; }
        public Guid xjp_registrationofficeid { get; set; }
        public string xts_address2 { get; set; }
        public int utcconversiontimezonecode { get; set; }
        public string xts_endwednesdaybreakdays { get; set; }
        public string xts_wednesdayworkingdaysname { get; set; }
        public string xts_address1 { get; set; }
        public string modifiedbyyominame { get; set; }
        public string xts_locking { get; set; }
        public Guid createdonbehalfby { get; set; }
        public string xts_startsaturdayworkingdays { get; set; }
        public string xts_endfridayworkingdays { get; set; }
        public Guid owninguser { get; set; }
        public bool xts_mondayworkingdays { get; set; }
        public string xts_startsaturdaybreakdays { get; set; }
        public Guid xts_businessunitid { get; set; }
        public string xjp_securitystandardissueridname { get; set; }
        public string xts_thursdayworkingdaysname { get; set; }
        public Guid modifiedonbehalfby { get; set; }
        public string xts_endtuesdayworkingdays { get; set; }
        public string statuscodename { get; set; }
        public string xts_startthursdayworkingdays { get; set; }
        public string xts_endwednesdayworkingdays { get; set; }
        public string createdonbehalfbyname { get; set; }
        public string xjp_maintenanceworkshopsegment2 { get; set; }
        public string xts_endtuesdaybreakdays { get; set; }
        public string xts_startfridayworkingdays { get; set; }
        public string xts_endthursdayworkingdays { get; set; }
        public string xts_townorvillageandstreetidname { get; set; }
        public int importsequencenumber { get; set; }
        public string xts_endsaturdaybreakdays { get; set; }
        public Guid xts_cityid { get; set; }
        public int statuscode { get; set; }
        public Guid xts_countryid { get; set; }
        public string xts_countryidname { get; set; }
        public string xts_endsundaybreakdays { get; set; }
        public string statecodename { get; set; }
        public string xts_startmondayworkingdays { get; set; }
        public string xts_address3 { get; set; }
        public Guid modifiedby { get; set; }
        public string xts_endmondaybreakdays { get; set; }
        public string xts_endsundayworkingdays { get; set; }
        public string xjp_businessname2 { get; set; }
        public string xts_startsundaybreakdays { get; set; }
        public string xjp_reportformat { get; set; }
        public string xjp_registrationregional { get; set; }
        public string owneridname { get; set; }
        public string xts_endmondayworkingdays { get; set; }
        public DateTime overriddencreatedon { get; set; }
        public string createdbyyominame { get; set; }
        public Guid xts_defaultcirclechecktemplateid { get; set; }
        public string xts_startthursdaybreakdays { get; set; }
        public DateTime createdon { get; set; }
        public string xjp_registrationofficeidname { get; set; }
        public Int64 versionnumber { get; set; }
        public string xts_startwednesdaybreakdays { get; set; }
        public bool xts_saturdayworkingdays { get; set; }
        public string xts_defaultmmstemplateidname { get; set; }
        public bool xts_wednesdayworkingdays { get; set; }
        public string xjp_inspectoridname { get; set; }
        public Guid xjp_inspectorid { get; set; }
        public string owneridtype { get; set; }
        public bool xts_thursdayworkingdays { get; set; }
        public string xts_saturdayworkingdaysname { get; set; }
        public string xts_starttuesdayworkingdays { get; set; }
        public int statecode { get; set; }
        public Guid xts_townorvillageandstreetid { get; set; }
        public string xts_fridayworkingdaysname { get; set; }
        public string xts_serviceworkshopsetting { get; set; }
        public string xts_address4 { get; set; }
        public string xts_mondayworkingdaysname { get; set; }
        public string xts_endfridaybreakdays { get; set; }
        public string msdyn_companycode { get; set; }
    }
}
