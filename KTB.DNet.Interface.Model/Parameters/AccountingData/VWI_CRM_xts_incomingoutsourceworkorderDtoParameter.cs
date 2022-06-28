#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_incomingoutsourceworkorderParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:55
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
    public class VWI_CRM_xts_incomingoutsourceworkorderParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xjp_requeststampfeeadvancedpaymentname { get; set; }

		[AntiXss]
		public decimal xts_totaltaxamount_base { get; set; }

		[AntiXss]
		public DateTime xts_outsourceworkorderreferencedate { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public string xts_outsrcworefnumberlookupname { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public decimal xts_totalbillingamountaftertax { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public DateTime xts_scheduledarrivaldateandtime { get; set; }

		[AntiXss]
		public decimal xts_totaltaxamount { get; set; }

		[AntiXss]
		public bool xjp_requestcaliadvancedpayment { get; set; }

		[AntiXss]
		public Guid xts_outsrcworefnumberserviceinstructionid { get; set; }

		[AntiXss]
		public string xts_finishpatternname { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public Guid xts_customerid { get; set; }

		[AntiXss]
		public string xts_manufacturerdescription { get; set; }

		[AntiXss]
		public Guid xts_incomingoutsourceworkorderid { get; set; }

		[AntiXss]
		public DateTime xts_scheduledfinishdateandtime { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public DateTime xts_schledoutsourceservicefinishdateandtime { get; set; }

		[AntiXss]
		public string xts_incomingoutsourceworkordermemo { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xjp_requestcaliadvancedpaymentname { get; set; }

		[AntiXss]
		public string xts_chassisnumber { get; set; }

		[AntiXss]
		public string xts_platenumber { get; set; }

		[AntiXss]
		public Guid xts_outsrcworkorderreferencepersoninchargeid { get; set; }

		[AntiXss]
		public string xts_vehiclemodelname { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public decimal xts_totalbillingamountbeforetax_base { get; set; }

		[AntiXss]
		public decimal xts_totalbillingamountbeforetax { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_deliverymemo { get; set; }

		[AntiXss]
		public string xts_originalbusinessunitidname { get; set; }

		[AntiXss]
		public string xts_originalworkorderreferenceidname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public int xts_outsrcworefnumberlookuptype { get; set; }

		[AntiXss]
		public string xts_manufactureridname { get; set; }

		[AntiXss]
		public string xts_customeridyominame { get; set; }

		[AntiXss]
		public bool xjp_requeststampfeeadvancedpayment { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_arrivalmemo { get; set; }

		[AntiXss]
		public bool xjp_weighttax { get; set; }

		[AntiXss]
		public string xts_arrivalpatternname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string xjp_requestweighttaxadvancedpaymentname { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xjp_outsourceworefnbrpdiidname { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public string xjp_taxpaymentdocumentname { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public string xts_customernumber { get; set; }

		[AntiXss]
		public string xjp_opticalcharacterreadername { get; set; }

		[AntiXss]
		public bool xjp_requestweighttaxadvancedpayment { get; set; }

		[AntiXss]
		public string xts_modelspecificationnumber { get; set; }

		[AntiXss]
		public bool xjp_taxpaymentdocument { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_chassismodel { get; set; }

		[AntiXss]
		public Guid xjp_outsourceworefnbrpdiid { get; set; }

		[AntiXss]
		public bool xjp_opticalcharacterreader { get; set; }

		[AntiXss]
		public string xts_incomingreservationreferenceidname { get; set; }

		[AntiXss]
		public string xts_classificationnumber { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public Guid xts_vehicleidentificationid { get; set; }

		[AntiXss]
		public string xts_outsrcworkorderreferencepersoninchargeidname { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string xjp_weighttaxname { get; set; }

		[AntiXss]
		public string xjp_compulsoryautomobileliabilityinsurancename { get; set; }

		[AntiXss]
		public Guid xts_originalworkorderreferenceid { get; set; }

		[AntiXss]
		public string xts_category { get; set; }

		[AntiXss]
		public Guid xts_servicecategoryid { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public string xts_outsrcworefnumberserviceinstructionidname { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public DateTime xts_schledarrivalinoriginworkshopdateandtime { get; set; }

		[AntiXss]
		public Guid xts_originalbusinessunitid { get; set; }

		[AntiXss]
		public string xts_vehicleidentificationidname { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public string xts_categoryname { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public bool xjp_compulsoryautomobileliabilityinsurance { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid xts_outsrcworefnumberoutsourceworkorderid { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string xts_incomingoutsourceworkorder { get; set; }

		[AntiXss]
		public string xts_customeridname { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public Guid xts_manufacturerid { get; set; }

		[AntiXss]
		public string xts_finishpattern { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public DateTime xts_scheduledoutsourcearrivaldateandtime { get; set; }

		[AntiXss]
		public string xts_arrivalpattern { get; set; }

		[AntiXss]
		public Guid xts_incomingreservationreferenceid { get; set; }

		[AntiXss]
		public DateTime xts_scheduledoutsourcefinishdateandtime { get; set; }

		[AntiXss]
		public string xts_servicecategoryidname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public decimal xts_totalbillingamountaftertax_base { get; set; }

		[AntiXss]
		public string xts_outsrcworefnumberoutsourceworkorderidname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
