#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_serviceworkshopsettingParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 09:35:00
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
    public class VWI_CRM_xts_serviceworkshopsettingParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public string xts_startsundayworkingdays { get; set; }
        [AntiXss]
        public Guid xts_defaultmmstemplateid { get; set; }
        [AntiXss]
        public string xts_ownerbusinessname { get; set; }
        [AntiXss]
        public string xts_tuesdayworkingdaysname { get; set; }
        [AntiXss]
        public Guid xts_provinceid { get; set; }
        [AntiXss]
        public string xts_startmondaybreakdays { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public string xjp_certificatedbusinessnumber { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public string xts_endthursdaybreakdays { get; set; }
        [AntiXss]
        public string xjp_reportformatname { get; set; }
        [AntiXss]
        public string xjp_designatedbusinessnumber { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public string xjp_maintenanceworkshop { get; set; }
        [AntiXss]
        public string xts_provinceidname { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public Guid xjp_securitystandardissuerid { get; set; }
        [AntiXss]
        public string xts_businessunitidname { get; set; }
        [AntiXss]
        public bool xts_tuesdayworkingdays { get; set; }
        [AntiXss]
        public string xts_postalcode { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public string xts_phone { get; set; }
        [AntiXss]
        public Guid xts_serviceworkshopsettingid { get; set; }
        [AntiXss]
        public string xts_sundayworkingdaysname { get; set; }
        [AntiXss]
        public bool xts_sundayworkingdays { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public bool xts_fridayworkingdays { get; set; }
        [AntiXss]
        public string xts_defaultcirclechecktemplateidname { get; set; }
        [AntiXss]
        public string xts_cityidname { get; set; }
        [AntiXss]
        public string xts_endsaturdayworkingdays { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public string xts_starttuesdaybreakdays { get; set; }
        [AntiXss]
        public string xts_startfridaybreakdays { get; set; }
        [AntiXss]
        public string xts_startwednesdayworkingdays { get; set; }
        [AntiXss]
        public Guid xjp_registrationofficeid { get; set; }
        [AntiXss]
        public string xts_address2 { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public string xts_endwednesdaybreakdays { get; set; }
        [AntiXss]
        public string xts_wednesdayworkingdaysname { get; set; }
        [AntiXss]
        public string xts_address1 { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public string xts_startsaturdayworkingdays { get; set; }
        [AntiXss]
        public string xts_endfridayworkingdays { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public bool xts_mondayworkingdays { get; set; }
        [AntiXss]
        public string xts_startsaturdaybreakdays { get; set; }
        [AntiXss]
        public Guid xts_businessunitid { get; set; }
        [AntiXss]
        public string xjp_securitystandardissueridname { get; set; }
        [AntiXss]
        public string xts_thursdayworkingdaysname { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public string xts_endtuesdayworkingdays { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public string xts_startthursdayworkingdays { get; set; }
        [AntiXss]
        public string xts_endwednesdayworkingdays { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public string xjp_maintenanceworkshopsegment2 { get; set; }
        [AntiXss]
        public string xts_endtuesdaybreakdays { get; set; }
        [AntiXss]
        public string xts_startfridayworkingdays { get; set; }
        [AntiXss]
        public string xts_endthursdayworkingdays { get; set; }
        [AntiXss]
        public string xts_townorvillageandstreetidname { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public string xts_endsaturdaybreakdays { get; set; }
        [AntiXss]
        public Guid xts_cityid { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public Guid xts_countryid { get; set; }
        [AntiXss]
        public string xts_countryidname { get; set; }
        [AntiXss]
        public string xts_endsundaybreakdays { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public string xts_startmondayworkingdays { get; set; }
        [AntiXss]
        public string xts_address3 { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public string xts_endmondaybreakdays { get; set; }
        [AntiXss]
        public string xts_endsundayworkingdays { get; set; }
        [AntiXss]
        public string xjp_businessname2 { get; set; }
        [AntiXss]
        public string xts_startsundaybreakdays { get; set; }
        [AntiXss]
        public string xjp_reportformat { get; set; }
        [AntiXss]
        public string xjp_registrationregional { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public string xts_endmondayworkingdays { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public Guid xts_defaultcirclechecktemplateid { get; set; }
        [AntiXss]
        public string xts_startthursdaybreakdays { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public string xjp_registrationofficeidname { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public string xts_startwednesdaybreakdays { get; set; }
        [AntiXss]
        public bool xts_saturdayworkingdays { get; set; }
        [AntiXss]
        public string xts_defaultmmstemplateidname { get; set; }
        [AntiXss]
        public bool xts_wednesdayworkingdays { get; set; }
        [AntiXss]
        public string xjp_inspectoridname { get; set; }
        [AntiXss]
        public Guid xjp_inspectorid { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public bool xts_thursdayworkingdays { get; set; }
        [AntiXss]
        public string xts_saturdayworkingdaysname { get; set; }
        [AntiXss]
        public string xts_starttuesdayworkingdays { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public Guid xts_townorvillageandstreetid { get; set; }
        [AntiXss]
        public string xts_fridayworkingdaysname { get; set; }
        [AntiXss]
        public string xts_serviceworkshopsetting { get; set; }
        [AntiXss]
        public string xts_address4 { get; set; }
        [AntiXss]
        public string xts_mondayworkingdaysname { get; set; }
        [AntiXss]
        public string xts_endfridaybreakdays { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
