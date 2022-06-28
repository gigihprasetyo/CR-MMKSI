#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_serviceinstructionferenceDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 08:15:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_serviceinstructionDto : DtoBase
    {
        public string company { get; set; }
        public string businessunitcode { get; set; }
        public string xts_businessunitidname { get; set; }
        public decimal xjp_weighttax { get; set; }
        public string xts_servicetypename { get; set; }
        public DateTime overriddencreatedon { get; set; }
        public decimal xts_estimatedpartsfee_base { get; set; }
        public string xts_costnonprint { get; set; }
        public decimal xjp_automobiletax { get; set; }
        public string xts_costnonprintname { get; set; }
        public string createdonbehalfbyyominame { get; set; }
        public Guid modifiedonbehalfby { get; set; }
        public decimal exchangerate { get; set; }
        public string xts_workorderidname { get; set; }
        public Guid xts_usedvehiclesalesorderid { get; set; }
        public decimal xts_actualtechnicalfee { get; set; }
        public string xts_reconditionidname { get; set; }
        public decimal xts_totalestimatedfee_base { get; set; }
        public decimal xts_actualpartsfee { get; set; }
        public decimal xts_estimatedpartsfee { get; set; }
        public Guid xts_servicereceiptid { get; set; }
        public string xts_requestto { get; set; }
        public decimal xts_actualsubcontractfee_base { get; set; }
        public decimal xjp_actualautomobiletax { get; set; }
        public string owneridname { get; set; }
        public int timezoneruleversionnumber { get; set; }
        public DateTime xts_acceptancedate { get; set; }
        public int statecode { get; set; }
        public decimal xjp_actualcali { get; set; }
        public bool xjp_vehicleinspection { get; set; }
        public Guid xts_parentbusinessunitid { get; set; }
        public int xts_servicedestinationlookuptype { get; set; }
        public string xts_locking { get; set; }
        public string xts_handling { get; set; }
        public DateTime xts_plannedreceiptdate { get; set; }
        public string xts_servicecategoryidname { get; set; }
        public string modifiedbyname { get; set; }
        public decimal xts_estimatedsubcontractfee_base { get; set; }
        public Guid createdonbehalfby { get; set; }
        public Guid xts_personinchargeid { get; set; }
        public decimal xts_estimatedsubcontractfee { get; set; }
        public string owneridtype { get; set; }
        public decimal xjp_variouscost_base { get; set; }
        public Guid ownerid { get; set; }
        public string xts_status { get; set; }
        public decimal xjp_actualacquisitiontax { get; set; }
        public string statuscodename { get; set; }
        public Guid xts_servicedestinationbusinessunitid { get; set; }
        public string xts_mainworkdescription { get; set; }
        public Guid xts_serviceinstructionid { get; set; }
        public decimal xjp_actualweighttax { get; set; }
        public string xts_servicecategorynewidname { get; set; }
        public DateTime createdon { get; set; }
        public string xts_servicereceiptidname { get; set; }
        public Int64 versionnumber { get; set; }
        public decimal xjp_actualacquisitiontax_base { get; set; }
        public decimal xjp_compulsoryinsurance_base { get; set; }
        public Guid xts_servicecategoryid { get; set; }
        public string xts_servicedestinationlookupname { get; set; }
        public decimal xts_totalestimatedfee { get; set; }
        public decimal xjp_compulsoryinsurance { get; set; }
        public DateTime xts_desireddeliverydate { get; set; }
        public DateTime xts_deliverydate { get; set; }
        public DateTime xts_actualcompletiondate { get; set; }
        public Guid xts_servicecategorynewid { get; set; }
        public string createdonbehalfbyname { get; set; }
        public Guid xts_servicedestinationid { get; set; }
        public decimal xjp_weighttax_base { get; set; }
        public int utcconversiontimezonecode { get; set; }
        public Guid xts_businessunitid { get; set; }
        public string xts_statusname { get; set; }
        public int xts_standardworkinghours { get; set; }
        public DateTime modifiedon { get; set; }
        public int importsequencenumber { get; set; }
        public string owneridyominame { get; set; }
        public decimal xts_actualsubcontractfee { get; set; }
        public string xts_requesttoname { get; set; }
        public string xts_personinchargeidname { get; set; }
        public decimal xjp_actualcali_base { get; set; }
        public decimal xjp_variouscost { get; set; }
        public DateTime xts_transactiondate { get; set; }
        public decimal xjp_actualserviceinstructioncost_base { get; set; }
        public string xts_serviceinstructionnumber { get; set; }
        public Guid xts_workorderid { get; set; }
        public string xjp_vehicleinspectionname { get; set; }
        public string createdbyname { get; set; }
        public Guid owningteam { get; set; }
        public decimal xjp_automobiletax_base { get; set; }
        public DateTime xts_schledarrivalinoriginworkshopdateandtime { get; set; }
        public decimal xts_actualpartsfee_base { get; set; }
        public decimal xjp_acquisitiontax { get; set; }
        public string xts_servicestatus { get; set; }
        public decimal xts_estimatedtechnicalfee_base { get; set; }
        public int statuscode { get; set; }
        public string xts_servicedestinationidname { get; set; }
        public string xts_servicedestinationbusinessunitidname { get; set; }
        public decimal xjp_acquisitiontax_base { get; set; }
        public decimal xts_estimatedtechnicalfee { get; set; }
        public Guid transactioncurrencyid { get; set; }
        public string xts_newmainworkdescription { get; set; }
        public decimal xjp_actualserviceinstructioncost { get; set; }
        public Guid createdby { get; set; }
        public decimal xjp_actualweighttax_base { get; set; }
        public Guid modifiedby { get; set; }
        public string xts_vehicleidentificationnumber { get; set; }
        public string createdbyyominame { get; set; }
        public Guid owninguser { get; set; }
        public string transactioncurrencyidname { get; set; }
        public string xts_servicestatusname { get; set; }
        public string xts_servicedestinationdescription { get; set; }
        public Guid xts_stockid { get; set; }
        public string xts_stockidname { get; set; }
        public string modifiedbyyominame { get; set; }
        public decimal xts_actualtechnicalfee_base { get; set; }
        public string xts_parentbusinessunitidname { get; set; }
        public string modifiedonbehalfbyname { get; set; }
        public string xts_servicetype { get; set; }
        public string xts_usedvehiclesalesorderidname { get; set; }
        public string modifiedonbehalfbyyominame { get; set; }
        public DateTime xts_scheduledoutsourcearrivaldateandtime { get; set; }
        public Guid xts_reconditionid { get; set; }
        public decimal xjp_actualautomobiletax_base { get; set; }
        public string xts_handlingname { get; set; }
        public DateTime xts_scheduledoutsourcefinishdateandtime { get; set; }
        public decimal xjp_actualvariouscost_base { get; set; }
        public string statecodename { get; set; }
        public decimal xjp_actualvariouscost { get; set; }
        public Guid owningbusinessunit { get; set; }
        public DateTime xts_schledoutsourceservicefinishdateandtime { get; set; }
        public string msdyn_companycode { get; set; }
    }
}
