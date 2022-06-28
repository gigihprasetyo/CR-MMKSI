#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_incomingoutsourceworkorderDto  class
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
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_incomingoutsourceworkorderDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xjp_requeststampfeeadvancedpaymentname { get; set; }

		public decimal xts_totaltaxamount_base { get; set; }

		public DateTime xts_outsourceworkorderreferencedate { get; set; }

		public Int64 versionnumber { get; set; }

		public string xts_outsrcworefnumberlookupname { get; set; }

		public DateTime createdon { get; set; }

		public decimal xts_totalbillingamountaftertax { get; set; }

		public string statuscodename { get; set; }

		public DateTime xts_scheduledarrivaldateandtime { get; set; }

		public decimal xts_totaltaxamount { get; set; }

		public bool xjp_requestcaliadvancedpayment { get; set; }

		public Guid xts_outsrcworefnumberserviceinstructionid { get; set; }

		public string xts_finishpatternname { get; set; }

		public string xts_status { get; set; }

		public Guid xts_customerid { get; set; }

		public string xts_manufacturerdescription { get; set; }

		public Guid xts_incomingoutsourceworkorderid { get; set; }

		public DateTime xts_scheduledfinishdateandtime { get; set; }

		public string owneridtype { get; set; }

		public DateTime xts_schledoutsourceservicefinishdateandtime { get; set; }

		public string xts_incomingoutsourceworkordermemo { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string xjp_requestcaliadvancedpaymentname { get; set; }

		public string xts_chassisnumber { get; set; }

		public string xts_platenumber { get; set; }

		public Guid xts_outsrcworkorderreferencepersoninchargeid { get; set; }

		public string xts_vehiclemodelname { get; set; }

		public string owneridname { get; set; }

		public decimal xts_totalbillingamountbeforetax_base { get; set; }

		public decimal xts_totalbillingamountbeforetax { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string xts_deliverymemo { get; set; }

		public string xts_originalbusinessunitidname { get; set; }

		public string xts_originalworkorderreferenceidname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string transactioncurrencyidname { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public int xts_outsrcworefnumberlookuptype { get; set; }

		public string xts_manufactureridname { get; set; }

		public string xts_customeridyominame { get; set; }

		public bool xjp_requeststampfeeadvancedpayment { get; set; }

		public Guid owningteam { get; set; }

		public string xts_arrivalmemo { get; set; }

		public bool xjp_weighttax { get; set; }

		public string xts_arrivalpatternname { get; set; }

		public int statecode { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string xjp_requestweighttaxadvancedpaymentname { get; set; }

		public decimal exchangerate { get; set; }

		public string modifiedbyname { get; set; }

		public string xjp_outsourceworefnbrpdiidname { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public string xjp_taxpaymentdocumentname { get; set; }

		public string xts_locking { get; set; }

		public string xts_handling { get; set; }

		public string xts_customernumber { get; set; }

		public string xjp_opticalcharacterreadername { get; set; }

		public bool xjp_requestweighttaxadvancedpayment { get; set; }

		public string xts_modelspecificationnumber { get; set; }

		public bool xjp_taxpaymentdocument { get; set; }

		public string traversedpath { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string xts_chassismodel { get; set; }

		public Guid xjp_outsourceworefnbrpdiid { get; set; }

		public bool xjp_opticalcharacterreader { get; set; }

		public string xts_incomingreservationreferenceidname { get; set; }

		public string xts_classificationnumber { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public Guid xts_vehicleidentificationid { get; set; }

		public string xts_outsrcworkorderreferencepersoninchargeidname { get; set; }

		public string xts_statusname { get; set; }

		public Guid ownerid { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public string owneridyominame { get; set; }

		public string xjp_weighttaxname { get; set; }

		public string xjp_compulsoryautomobileliabilityinsurancename { get; set; }

		public Guid xts_originalworkorderreferenceid { get; set; }

		public string xts_category { get; set; }

		public Guid xts_servicecategoryid { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public string xts_outsrcworefnumberserviceinstructionidname { get; set; }

		public Guid processid { get; set; }

		public string createdbyname { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public DateTime xts_schledarrivalinoriginworkshopdateandtime { get; set; }

		public Guid xts_originalbusinessunitid { get; set; }

		public string xts_vehicleidentificationidname { get; set; }

		public Guid stageid { get; set; }

		public string xts_categoryname { get; set; }

		public string xts_handlingname { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public bool xjp_compulsoryautomobileliabilityinsurance { get; set; }

		public string xts_businessunitidname { get; set; }

		public Guid createdby { get; set; }

		public Guid modifiedby { get; set; }

		public Guid xts_outsrcworefnumberoutsourceworkorderid { get; set; }

		public string createdbyyominame { get; set; }

		public Guid owninguser { get; set; }

		public string xts_incomingoutsourceworkorder { get; set; }

		public string xts_customeridname { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string modifiedbyyominame { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public Guid xts_manufacturerid { get; set; }

		public string xts_finishpattern { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public DateTime xts_scheduledoutsourcearrivaldateandtime { get; set; }

		public string xts_arrivalpattern { get; set; }

		public Guid xts_incomingreservationreferenceid { get; set; }

		public DateTime xts_scheduledoutsourcefinishdateandtime { get; set; }

		public string xts_servicecategoryidname { get; set; }

		public string statecodename { get; set; }

		public decimal xts_totalbillingamountaftertax_base { get; set; }

		public string xts_outsrcworefnumberoutsourceworkorderidname { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
