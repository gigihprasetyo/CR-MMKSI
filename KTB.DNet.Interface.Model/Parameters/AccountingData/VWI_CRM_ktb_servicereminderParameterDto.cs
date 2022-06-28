#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_ktb_servicereminder  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/03/2022 10:02:47
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
    public class VWI_CRM_ktb_servicereminderParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string ktb_bucompany { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        public Int64 versionnumber { get; set; }
        public DateTime createdon { get; set; }
        public Guid ktb_servicereminderid { get; set; }
        [AntiXss]
        public string owningbusinessunitname { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        public DateTime ktb_maximaltanggalfollowup { get; set; }
        [AntiXss]
        public string ktb_plateno { get; set; }
        public Guid ktb_vehiclepublicid { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string ktb_vehiclepublicidname { get; set; }
        [AntiXss]
        public string ktb_transactiontype { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        public Guid ktb_businessunitid { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        public DateTime overriddencreatedon { get; set; }
        public DateTime ktb_tanggalbooking { get; set; }
        [AntiXss]
        public string ktb_serviceincdnetcode { get; set; }
        [AntiXss]
        public string ktb_notemms { get; set; }
        [AntiXss]
        public string ktb_servicereminderiddnet { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public string ktb_responname { get; set; }
        public Guid owningteam { get; set; }
        [AntiXss]
        public string ktb_respon { get; set; }
        [AntiXss]
        public string ktb_nommsrefidname { get; set; }
        [AntiXss]
        public string ktb_lastkm { get; set; }
        public int statecode { get; set; }
        public Guid ktb_noworefmmsid { get; set; }
        [AntiXss]
        public string ktb_noworefmmsidname { get; set; }
        public int timezoneruleversionnumber { get; set; }
        public bool ktb_isinterface { get; set; }
        public Guid ktb_parentbusinessunitid { get; set; }
        public Guid modifiedby { get; set; }
        public DateTime ktb_followupdate { get; set; }
        [AntiXss]
        public string ktb_businessunitidname { get; set; }
        public Guid ktb_vehicleidentificationnoid { get; set; }
        [AntiXss]
        public string ktb_noworefidname { get; set; }
        [AntiXss]
        public string ktb_customername { get; set; }
        [AntiXss]
        public string ktb_servicereminderdms { get; set; }
        [AntiXss]
        public string ktb_statusname { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public string ktb_chassisnumber { get; set; }
        public Guid createdby { get; set; }
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public string ktb_sourcedata { get; set; }
        [AntiXss]
        public string ktb_engineno { get; set; }
        public Guid ownerid { get; set; }
        public DateTime modifiedon { get; set; }
        public int importsequencenumber { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public string ktb_tipekendaraan { get; set; }
        public Guid ktb_noworefid { get; set; }
        [AntiXss]
        public string ktb_sourcedataname { get; set; }
        [AntiXss]
        public string ktb_isinterfacename { get; set; }
        [AntiXss]
        public string ktb_description { get; set; }
        [AntiXss]
        public string ktb_reinterface { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public string ktb_handling { get; set; }
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public string ktb_customerphonenumber { get; set; }
        [AntiXss]
        public string ktb_interfaceresult { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        public Guid owninguser { get; set; }
        [AntiXss]
        public string ktb_reinterfacename { get; set; }
        public DateTime ktb_reminderservicedate { get; set; }
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public string ktb_handlingname { get; set; }
        [AntiXss]
        public string ktb_contactphonenumber { get; set; }
        public DateTime ktb_dateupdatefrominterface { get; set; }
        public int statuscode { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string ktb_parentbusinessunitidname { get; set; }
        public Guid ktb_nommsrefid { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public string ktb_contactname { get; set; }
        [AntiXss]
        public string ktb_vehicleidentificationnoidname { get; set; }
        [AntiXss]
        public string ktb_status { get; set; }
        [AntiXss]
        public string ktb_casenumber { get; set; }
        [AntiXss]
        public string DealerCode { get; set; }
        [AntiXss]
        public string SourceType { get; set; }
        public DateTime LastSyncDate { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
