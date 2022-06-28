#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_registrationdocumentParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 09:15:00
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
    public class VWI_CRM_xjp_registrationdocumentParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public int xjp_parkingspaceownershipdocument { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public string xjp_calipartyinchargename { get; set; }
        [AntiXss]
        public int xjp_livingsamecertificate { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public string xjp_parentbusinessunitidname { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public int xjp_caliterminatedocument { get; set; }
        [AntiXss]
        public string xjp_garageapplicationpartyinchargename { get; set; }
        [AntiXss]
        public int xjp_tradeinrecyclingticket { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public DateTime xjp_calisenddatetohq { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public DateTime xjp_garageapplicationresultdate { get; set; }
        [AntiXss]
        public int xjp_tradeinproxystamping { get; set; }
        [AntiXss]
        public Guid xjp_parentbusinessunitid { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public int xjp_parkingspaceownershipacceptancecert { get; set; }
        [AntiXss]
        public int xjp_tradeinlightmotorvhcinspeccertform1 { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public int xjp_driverslicensecopy { get; set; }
        [AntiXss]
        public int xjp_tradeinsealcertificate { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public DateTime xjp_garageapplicationschedulecollectiondate { get; set; }
        [AntiXss]
        public string xjp_insurancecompanyidname { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public int xjp_sealregistrationcertificate { get; set; }
        [AntiXss]
        public int xjp_certificateofresident { get; set; }
        [AntiXss]
        public int xjp_proxy { get; set; }
        [AntiXss]
        public DateTime xjp_tradeincollectiondate { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public string xjp_policestation { get; set; }
        [AntiXss]
        public DateTime xjp_requestedplatenoreceivedate { get; set; }
        [AntiXss]
        public int xjp_tradeinlightmotorvhcinspeccertform2 { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public int xjp_autmblacquisitionandautomobiletaxreport { get; set; }
        [AntiXss]
        public string xjp_reservedplatenumberpartyincharge { get; set; }
        [AntiXss]
        public string xjp_vehiclespecificnumber { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public DateTime xjp_registrationdocumentsentdate { get; set; }
        [AntiXss]
        public DateTime xjp_registrationdocumentcollectiondate { get; set; }
        [AntiXss]
        public DateTime xjp_garageapplicationcollectiondate { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public DateTime xjp_garageapplicationsenddate { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public string xjp_reservedplatenumberpartyinchargename { get; set; }
        [AntiXss]
        public DateTime xjp_reservedplatenosenddatetohq { get; set; }
        [AntiXss]
        public DateTime xjp_electronicproxyrequestdate { get; set; }
        [AntiXss]
        public Guid xjp_insurancecompanyid { get; set; }
        [AntiXss]
        public string xjp_registrationdocumentnumber { get; set; }
        [AntiXss]
        public int xjp_tradeincertificateofresident { get; set; }
        [AntiXss]
        public string xjp_calipartyincharge { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public Guid xjp_registrationdocumentid { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public int xjp_companycertificate { get; set; }
        [AntiXss]
        public int xjp_parkingspacecertificate { get; set; }
        [AntiXss]
        public string xjp_calinumber { get; set; }
        [AntiXss]
        public int xjp_applicationrequestlightvehicle { get; set; }
        [AntiXss]
        public Guid xjp_businessunitid { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public string xjp_businessunitidname { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public string xjp_garageapplicationpartyincharge { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public int xjp_handicappedhandbook { get; set; }
        [AntiXss]
        public DateTime xjp_regdocumentschedulecollectiondate { get; set; }
        [AntiXss]
        public int xjp_tradeinautombltaxcertpaymentdocument { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public DateTime xjp_garageapplicationexpectedresultdate { get; set; }
        [AntiXss]
        public string xjp_vehicleidentificationnumber { get; set; }
        [AntiXss]
        public DateTime xjp_tradeinschedulecollectiondate { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public string xjp_locking { get; set; }
        [AntiXss]
        public int xjp_tradeintransfercertificate { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
