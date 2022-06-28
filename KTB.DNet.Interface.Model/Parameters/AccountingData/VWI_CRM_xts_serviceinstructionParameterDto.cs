#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_serviceinstructionParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 08:18:00
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
    public class VWI_CRM_xts_serviceinstructionParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public string xts_businessunitidname { get; set; }
        [AntiXss]
        public decimal xjp_weighttax { get; set; }
        [AntiXss]
        public string xts_servicetypename { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public decimal xts_estimatedpartsfee_base { get; set; }
        [AntiXss]
        public string xts_costnonprint { get; set; }
        [AntiXss]
        public decimal xjp_automobiletax { get; set; }
        [AntiXss]
        public string xts_costnonprintname { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public decimal exchangerate { get; set; }
        [AntiXss]
        public string xts_workorderidname { get; set; }
        [AntiXss]
        public Guid xts_usedvehiclesalesorderid { get; set; }
        [AntiXss]
        public decimal xts_actualtechnicalfee { get; set; }
        [AntiXss]
        public string xts_reconditionidname { get; set; }
        [AntiXss]
        public decimal xts_totalestimatedfee_base { get; set; }
        [AntiXss]
        public decimal xts_actualpartsfee { get; set; }
        [AntiXss]
        public decimal xts_estimatedpartsfee { get; set; }
        [AntiXss]
        public Guid xts_servicereceiptid { get; set; }
        [AntiXss]
        public string xts_requestto { get; set; }
        [AntiXss]
        public decimal xts_actualsubcontractfee_base { get; set; }
        [AntiXss]
        public decimal xjp_actualautomobiletax { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public DateTime xts_acceptancedate { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public decimal xjp_actualcali { get; set; }
        [AntiXss]
        public bool xjp_vehicleinspection { get; set; }
        [AntiXss]
        public Guid xts_parentbusinessunitid { get; set; }
        [AntiXss]
        public int xts_servicedestinationlookuptype { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public string xts_handling { get; set; }
        [AntiXss]
        public DateTime xts_plannedreceiptdate { get; set; }
        [AntiXss]
        public string xts_servicecategoryidname { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public decimal xts_estimatedsubcontractfee_base { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public Guid xts_personinchargeid { get; set; }
        [AntiXss]
        public decimal xts_estimatedsubcontractfee { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public decimal xjp_variouscost_base { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public string xts_status { get; set; }
        [AntiXss]
        public decimal xjp_actualacquisitiontax { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public Guid xts_servicedestinationbusinessunitid { get; set; }
        [AntiXss]
        public string xts_mainworkdescription { get; set; }
        [AntiXss]
        public Guid xts_serviceinstructionid { get; set; }
        [AntiXss]
        public decimal xjp_actualweighttax { get; set; }
        [AntiXss]
        public string xts_servicecategorynewidname { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public string xts_servicereceiptidname { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public decimal xjp_actualacquisitiontax_base { get; set; }
        [AntiXss]
        public decimal xjp_compulsoryinsurance_base { get; set; }
        [AntiXss]
        public Guid xts_servicecategoryid { get; set; }
        [AntiXss]
        public string xts_servicedestinationlookupname { get; set; }
        [AntiXss]
        public decimal xts_totalestimatedfee { get; set; }
        [AntiXss]
        public decimal xjp_compulsoryinsurance { get; set; }
        [AntiXss]
        public DateTime xts_desireddeliverydate { get; set; }
        [AntiXss]
        public DateTime xts_deliverydate { get; set; }
        [AntiXss]
        public DateTime xts_actualcompletiondate { get; set; }
        [AntiXss]
        public Guid xts_servicecategorynewid { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public Guid xts_servicedestinationid { get; set; }
        [AntiXss]
        public decimal xjp_weighttax_base { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public Guid xts_businessunitid { get; set; }
        [AntiXss]
        public string xts_statusname { get; set; }
        [AntiXss]
        public int xts_standardworkinghours { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public decimal xts_actualsubcontractfee { get; set; }
        [AntiXss]
        public string xts_requesttoname { get; set; }
        [AntiXss]
        public string xts_personinchargeidname { get; set; }
        [AntiXss]
        public decimal xjp_actualcali_base { get; set; }
        [AntiXss]
        public decimal xjp_variouscost { get; set; }
        [AntiXss]
        public DateTime xts_transactiondate { get; set; }
        [AntiXss]
        public decimal xjp_actualserviceinstructioncost_base { get; set; }
        [AntiXss]
        public string xts_serviceinstructionnumber { get; set; }
        [AntiXss]
        public Guid xts_workorderid { get; set; }
        [AntiXss]
        public string xjp_vehicleinspectionname { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public decimal xjp_automobiletax_base { get; set; }
        [AntiXss]
        public DateTime xts_schledarrivalinoriginworkshopdateandtime { get; set; }
        [AntiXss]
        public decimal xts_actualpartsfee_base { get; set; }
        [AntiXss]
        public decimal xjp_acquisitiontax { get; set; }
        [AntiXss]
        public string xts_servicestatus { get; set; }
        [AntiXss]
        public decimal xts_estimatedtechnicalfee_base { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public string xts_servicedestinationidname { get; set; }
        [AntiXss]
        public string xts_servicedestinationbusinessunitidname { get; set; }
        [AntiXss]
        public decimal xjp_acquisitiontax_base { get; set; }
        [AntiXss]
        public decimal xts_estimatedtechnicalfee { get; set; }
        [AntiXss]
        public Guid transactioncurrencyid { get; set; }
        [AntiXss]
        public string xts_newmainworkdescription { get; set; }
        [AntiXss]
        public decimal xjp_actualserviceinstructioncost { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public decimal xjp_actualweighttax_base { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public string xts_vehicleidentificationnumber { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public string xts_servicestatusname { get; set; }
        [AntiXss]
        public string xts_servicedestinationdescription { get; set; }
        [AntiXss]
        public Guid xts_stockid { get; set; }
        [AntiXss]
        public string xts_stockidname { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public decimal xts_actualtechnicalfee_base { get; set; }
        [AntiXss]
        public string xts_parentbusinessunitidname { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public string xts_servicetype { get; set; }
        [AntiXss]
        public string xts_usedvehiclesalesorderidname { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public DateTime xts_scheduledoutsourcearrivaldateandtime { get; set; }
        [AntiXss]
        public Guid xts_reconditionid { get; set; }
        [AntiXss]
        public decimal xjp_actualautomobiletax_base { get; set; }
        [AntiXss]
        public string xts_handlingname { get; set; }
        [AntiXss]
        public DateTime xts_scheduledoutsourcefinishdateandtime { get; set; }
        [AntiXss]
        public decimal xjp_actualvariouscost_base { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public decimal xjp_actualvariouscost { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public DateTime xts_schledoutsourceservicefinishdateandtime { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
