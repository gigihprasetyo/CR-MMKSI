#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_equipmentParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:44
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
    public class VWI_CRM_systemuserParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public decimal exchangerate { get; set; }
        [AntiXss]
        public string address1_postofficebox { get; set; }
        [AntiXss]
        public string personalemailaddress { get; set; }
        [AntiXss]
        public string emailrouteraccessapprovalname { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public bool isemailaddressapprovedbyo365admin { get; set; }
        [AntiXss]
        public string address2_stateorprovince { get; set; }
        [AntiXss]
        public Guid queueid { get; set; }
        [AntiXss]
        public string incomingemaildeliverymethodname { get; set; }
        [AntiXss]
        public string islicensedname { get; set; }
        [AntiXss]
        public string address1_line3 { get; set; }
        [AntiXss]
        public string siteidname { get; set; }
        [AntiXss]
        public string address1_stateorprovince { get; set; }
        [AntiXss]
        public string isdisabledname { get; set; }
        [AntiXss]
        public string address2_telephone3 { get; set; }
        [AntiXss]
        public Guid organizationid { get; set; }
        [AntiXss]
        public string address2_upszone { get; set; }
        [AntiXss]
        public string preferredemailcodename { get; set; }
        [AntiXss]
        public string outgoingemaildeliverymethodname { get; set; }
        [AntiXss]
        public string entityimage_url { get; set; }
        [AntiXss]
        public string preferredaddresscodename { get; set; }
        [AntiXss]
        public Guid calendarid { get; set; }
        [AntiXss]
        public string address2_addresstypecode { get; set; }
        [AntiXss]
        public string address2_shippingmethodcode { get; set; }
        [AntiXss]
        public string address1_shippingmethodcodename { get; set; }
        [AntiXss]
        public string accessmode { get; set; }
        [AntiXss]
        public string sharepointemailaddress { get; set; }
        [AntiXss]
        public string address1_postalcode { get; set; }
        [AntiXss]
        public Int32 utcconversiontimezonecode { get; set; }
        [AntiXss]
        public string address2_fax { get; set; }
        [AntiXss]
        public string organizationidname { get; set; }
        [AntiXss]
        public string address2_postofficebox { get; set; }
        [AntiXss]
        public Int32 address1_utcoffset { get; set; }
        [AntiXss]
        public string ktb_isenableprintservicename { get; set; }
        [AntiXss]
        public string address1_shippingmethodcode { get; set; }
        [AntiXss]
        public bool msdyn_gdproptout { get; set; }
        [AntiXss]
        public string yomifirstname { get; set; }
        [AntiXss]
        public string address2_line1 { get; set; }
        [AntiXss]
        public Int32 passporthi { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string accessmodename { get; set; }
        [AntiXss]
        public string address1_addresstypecode { get; set; }
        [AntiXss]
        public string salutation { get; set; }
        [AntiXss]
        public string employeeid { get; set; }
        [AntiXss]
        public string queueidname { get; set; }
        [AntiXss]
        public string preferredemailcode { get; set; }
        [AntiXss]
        public Int32 userlicensetype { get; set; }
        [AntiXss]
        public decimal address2_latitude { get; set; }
        [AntiXss]
        public string governmentid { get; set; }
        [AntiXss]
        public string windowsliveid { get; set; }
        [AntiXss]
        public string address2_telephone1 { get; set; }
        [AntiXss]
        public string mobilephone { get; set; }
        [AntiXss]
        public string nickname { get; set; }
        [AntiXss]
        public string address1_telephone3 { get; set; }
        [AntiXss]
        public bool defaultfilterspopulated { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public Int32 address2_utcoffset { get; set; }
        [AntiXss]
        public Int32 identityid { get; set; }
        [AntiXss]
        public string caltype { get; set; }
        [AntiXss]
        public string address1_county { get; set; }
        [AntiXss]
        public string address2_composite { get; set; }
        [AntiXss]
        public bool isintegrationuser { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public string address2_addresstypecodename { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public string homephone { get; set; }
        [AntiXss]
        public string positionidname { get; set; }
        [AntiXss]
        public bool issyncwithdirectory { get; set; }
        [AntiXss]
        public Guid siteid { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public decimal address2_longitude { get; set; }
        [AntiXss]
        public Guid defaultmailbox { get; set; }
        [AntiXss]
        public string address1_line1 { get; set; }
        [AntiXss]
        public string jobtitle { get; set; }
        [AntiXss]
        public string address1_telephone1 { get; set; }
        [AntiXss]
        public string displayinserviceviewsname { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public string mobileofflineprofileidname { get; set; }
        [AntiXss]
        public Int64 entityimage_timestamp { get; set; }
        [AntiXss]
        public Guid businessunitid { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public decimal address1_latitude { get; set; }
        [AntiXss]
        public string domainname { get; set; }
        [AntiXss]
        public Guid stageid { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public string preferredaddresscode { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public string address1_upszone { get; set; }
        [AntiXss]
        public string fullname { get; set; }
        [AntiXss]
        public Guid positionid { get; set; }
        [AntiXss]
        public string address2_line2 { get; set; }
        [AntiXss]
        public bool isactivedirectoryuser { get; set; }
        [AntiXss]
        public string setupusername { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public string entityimage { get; set; }
        [AntiXss]
        public string yomilastname { get; set; }
        [AntiXss]
        public string firstname { get; set; }
        [AntiXss]
        public string parentsystemuseridyominame { get; set; }
        [AntiXss]
        public string photourl { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid mobileofflineprofileid { get; set; }
        [AntiXss]
        public string userpuid { get; set; }
        [AntiXss]
        public string address2_telephone2 { get; set; }
        [AntiXss]
        public string incomingemaildeliverymethod { get; set; }
        [AntiXss]
        public Guid transactioncurrencyid { get; set; }
        [AntiXss]
        public string caltypename { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public string address2_name { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public bool ktb_isenableprintservice { get; set; }
        [AntiXss]
        public Guid processid { get; set; }
        [AntiXss]
        public string title { get; set; }
        [AntiXss]
        public Guid parentsystemuserid { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public string address2_country { get; set; }
        [AntiXss]
        public string yammeremailaddress { get; set; }
        [AntiXss]
        public string address1_composite { get; set; }
        [AntiXss]
        public string mobilealertemail { get; set; }
        [AntiXss]
        public string address1_line2 { get; set; }
        [AntiXss]
        public Int32 passportlo { get; set; }
        [AntiXss]
        public string address1_city { get; set; }
        [AntiXss]
        public string address2_shippingmethodcodename { get; set; }
        [AntiXss]
        public Guid entityimageid { get; set; }
        [AntiXss]
        public string middlename { get; set; }
        [AntiXss]
        public string address2_postalcode { get; set; }
        [AntiXss]
        public Guid systemuserid { get; set; }
        [AntiXss]
        public Int32 importsequencenumber { get; set; }
        [AntiXss]
        public string outgoingemaildeliverymethod { get; set; }
        [AntiXss]
        public string isintegrationusername { get; set; }
        [AntiXss]
        public string invitestatuscode { get; set; }
        [AntiXss]
        public Guid ktb_employeeid { get; set; }
        [AntiXss]
        public string defaultodbfoldername { get; set; }
        [AntiXss]
        public string disabledreason { get; set; }
        [AntiXss]
        public Guid applicationid { get; set; }
        [AntiXss]
        public string address2_city { get; set; }
        [AntiXss]
        public string yomifullname { get; set; }
        [AntiXss]
        public string parentsystemuseridname { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public string territoryidname { get; set; }
        [AntiXss]
        public string address1_telephone2 { get; set; }
        [AntiXss]
        public DateTime latestupdatetime { get; set; }
        [AntiXss]
        public string address1_addresstypecodename { get; set; }
        [AntiXss]
        public string invitestatuscodename { get; set; }
        [AntiXss]
        public string address2_county { get; set; }
        [AntiXss]
        public string ktb_employeeidname { get; set; }
        [AntiXss]
        public string businessunitidname { get; set; }
        [AntiXss]
        public Guid azureactivedirectoryobjectid { get; set; }
        [AntiXss]
        public Guid address2_addressid { get; set; }
        [AntiXss]
        public string address1_country { get; set; }
        [AntiXss]
        public string msdyn_gdproptoutname { get; set; }
        [AntiXss]
        public bool islicensed { get; set; }
        [AntiXss]
        public bool displayinserviceviews { get; set; }
        [AntiXss]
        public string internalemailaddress { get; set; }
        [AntiXss]
        public string defaultmailboxname { get; set; }
        [AntiXss]
        public string address2_line3 { get; set; }
        [AntiXss]
        public string address1_fax { get; set; }
        [AntiXss]
        public string preferredphonecodename { get; set; }
        [AntiXss]
        public string emailrouteraccessapproval { get; set; }
        [AntiXss]
        public decimal address1_longitude { get; set; }
        [AntiXss]
        public string applicationiduri { get; set; }
        [AntiXss]
        public Guid territoryid { get; set; }
        [AntiXss]
        public string yammeruserid { get; set; }
        [AntiXss]
        public string lastname { get; set; }
        [AntiXss]
        public Guid activedirectoryguid { get; set; }
        [AntiXss]
        public Guid address1_addressid { get; set; }
        [AntiXss]
        public Int32 timezoneruleversionnumber { get; set; }
        [AntiXss]
        public bool isdisabled { get; set; }
        [AntiXss]
        public string preferredphonecode { get; set; }
        [AntiXss]
        public string traversedpath { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public bool setupuser { get; set; }
        [AntiXss]
        public string yomimiddlename { get; set; }
        [AntiXss]
        public string address1_name { get; set; }
        [AntiXss]
        public string skills { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }


        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
