#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_predeliveryinspectionParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:51
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
    public class VWI_CRM_xjp_predeliveryinspectionParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string ktb_isinterfacedname { get; set; }

		[AntiXss]
		public string xjp_customernumber { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public decimal xjp_actualcost_base { get; set; }

		[AntiXss]
		public string xjp_stockidname { get; set; }

		[AntiXss]
		public string xjp_pdistatusname { get; set; }

		[AntiXss]
		public Guid xjp_productid { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xjp_parentbusinessunitidname { get; set; }

		[AntiXss]
		public DateTime xjp_arrivaldate { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xjp_deliverybusinessunitidname { get; set; }

		[AntiXss]
		public bool xjp_autoreceipt { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xjp_handlingname { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xjp_vendoridname { get; set; }

		[AntiXss]
		public Guid xjp_departmentid { get; set; }

		[AntiXss]
		public Guid xjp_exteriorcolorid { get; set; }

		[AntiXss]
		public string xjp_styleidname { get; set; }

		[AntiXss]
		public string xjp_productidname { get; set; }

		[AntiXss]
		public Guid xjp_parentbusinessunitid { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime xjp_vehiclesuggestedarrivaldateandtime { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public DateTime xjp_desireddeliverydate { get; set; }

		[AntiXss]
		public decimal xjp_actualcost { get; set; }

		[AntiXss]
		public string xjp_pdicategory { get; set; }

		[AntiXss]
		public string xjp_customeridname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public Guid xjp_vendorid { get; set; }

		[AntiXss]
		public string xjp_pdistatus { get; set; }

		[AntiXss]
		public Guid xjp_newvehiclesalesorderid { get; set; }

		[AntiXss]
		public decimal xjp_costdifference_base { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public Guid xjp_interiorcolorid { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public bool ktb_iskaroseri { get; set; }

		[AntiXss]
		public Guid xjp_servicecategoryid { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid xjp_predeliveryinspectionid { get; set; }

		[AntiXss]
		public DateTime xjp_schedulearrivaldate { get; set; }

		[AntiXss]
		public string xjp_customeridyominame { get; set; }

		[AntiXss]
		public string xjp_servicecategoryidname { get; set; }

		[AntiXss]
		public string xjp_handling { get; set; }

		[AntiXss]
		public string xjp_newvehiclesalesorderidname { get; set; }

		[AntiXss]
		public string xjp_exteriorcoloridname { get; set; }

		[AntiXss]
		public string xjp_interiorcoloridname { get; set; }

		[AntiXss]
		public Guid xjp_businessunitid { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string ktb_interfaceexceptionmessage { get; set; }

		[AntiXss]
		public DateTime xjp_completepdidate { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xjp_matchunmatchidname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public string xjp_autoreceiptname { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xjp_chassisnumber { get; set; }

		[AntiXss]
		public string xjp_configurationidname { get; set; }

		[AntiXss]
		public Guid xjp_customerid { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public Guid xjp_vehicleidentificationid { get; set; }

		[AntiXss]
		public DateTime xjp_scheduledcompletepdidate { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xjp_type { get; set; }

		[AntiXss]
		public DateTime xjp_deliverydate { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xjp_departmentidname { get; set; }

		[AntiXss]
		public DateTime xjp_transactiondate { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public bool xjp_deliverytostore { get; set; }

		[AntiXss]
		public string xjp_eventdata { get; set; }

		[AntiXss]
		public string xjp_deliverytostorename { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public decimal xjp_costdifference { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid xjp_deliverybusinessunitid { get; set; }

		[AntiXss]
		public decimal xjp_estimatedcost { get; set; }

		[AntiXss]
		public Guid xjp_configurationid { get; set; }

		[AntiXss]
		public Guid xjp_salespersonid { get; set; }

		[AntiXss]
		public string xjp_vehicleidentificationidname { get; set; }

		[AntiXss]
		public string xjp_businessunitidname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public Guid xjp_matchunmatchid { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xjp_productdescription { get; set; }

		[AntiXss]
		public string ktb_iskaroseriname { get; set; }

		[AntiXss]
		public string xjp_pdicategoryname { get; set; }

		[AntiXss]
		public string xjp_salespersonidname { get; set; }

		[AntiXss]
		public string xjp_typename { get; set; }

		[AntiXss]
		public string xjp_platenumber { get; set; }

		[AntiXss]
		public string xjp_predeliveryinspectionnumber { get; set; }

		[AntiXss]
		public Guid xjp_styleid { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public DateTime xjp_pdicancellationdate { get; set; }

		[AntiXss]
		public Guid xjp_stockid { get; set; }

		[AntiXss]
		public bool ktb_isinterfaced { get; set; }

		[AntiXss]
		public decimal xjp_estimatedcost_base { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xjp_locking { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
