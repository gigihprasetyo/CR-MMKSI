#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_outsourceworkorderDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/01/2020 14:19:26
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_outsourceworkorderDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xjp_requeststampfeeadvancedpaymentname { get; set; }

		public decimal xts_totaltaxamount_base { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Int64 versionnumber { get; set; }

		public string xjp_compulsoryautomobileliabilityinsurancename { get; set; }

		public DateTime createdon { get; set; }

		public string statuscodename { get; set; }

		public DateTime xts_scheduledarrivaldateandtime { get; set; }

		public decimal xts_totaltaxamount { get; set; }

		public bool xjp_requestcaliadvancedpayment { get; set; }

		public bool xjp_requestweighttaxadvancedpayment { get; set; }

		public string xts_finishpatternname { get; set; }

		public string xts_status { get; set; }

		public Guid xts_customerid { get; set; }

		public string modifiedbyyominame { get; set; }

		public string xjp_requestweighttaxadvancedpaymentname { get; set; }

		public DateTime xts_scheduledfinishdateandtime { get; set; }

		public string owneridtype { get; set; }

		public DateTime xts_schledoutsourceservicefinishdateandtime { get; set; }

		public Guid processid { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string xjp_requestcaliadvancedpaymentname { get; set; }

		public string modifiedbyname { get; set; }

		public string xts_platenumber { get; set; }

		public string xts_outsourcereservationidname { get; set; }

		public string xjp_taxpaymentdocumentname { get; set; }

		public Guid xts_outsourceworkorderid { get; set; }

		public string xjp_ocrname { get; set; }

		public string owneridname { get; set; }

		public decimal xts_totalbillingamountbeforetax_base { get; set; }

		public decimal xts_totalbillingamountbeforetax { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string xts_deliverymemo { get; set; }

		public Guid xts_outsourcereservationid { get; set; }

		public decimal xts_downpaymentamount { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal exchangerate { get; set; }

		public string xts_workorderidname { get; set; }

		public DateTime xts_scheduledoustourcefinishdateandtime { get; set; }

		public decimal xts_downpaymentamount_base { get; set; }

		public bool xjp_requeststampfeeadvancedpayment { get; set; }

		public Guid owningteam { get; set; }

		public string xts_arrivalmemo { get; set; }

		public string xts_tempotherids { get; set; }

		public string xts_arrivalpatternname { get; set; }

		public string xts_downpaymentispaidname { get; set; }

		public int statecode { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public decimal xts_downpaymentamountreceived_base { get; set; }

		public string xts_prpotypeidname { get; set; }

		public string xts_locking { get; set; }

		public string xts_handling { get; set; }

		public string xts_customernumber { get; set; }

		public bool xjp_taxpaymentdocument { get; set; }

		public string traversedpath { get; set; }

		public string createdonbehalfbyname { get; set; }

		public Guid xts_prpotypeid { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public Guid xts_vehicleidentificationid { get; set; }

		public bool xjp_weighttax { get; set; }

		public string xts_statusname { get; set; }

		public string xts_outsourceworkshopidname { get; set; }

		public string xts_customeridyominame { get; set; }

		public Guid ownerid { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public string owneridyominame { get; set; }

		public string xjp_weighttaxname { get; set; }

		public string xts_category { get; set; }

		public Guid xts_servicecategoryid { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public Guid xts_outsourceworkshopid { get; set; }

		public bool xts_downpaymentispaid { get; set; }

		public Guid xts_workorderid { get; set; }

		public string createdbyname { get; set; }

		public Guid xts_businessunitid { get; set; }

		public Guid xts_outsourceworkorderpersoninchargeid { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public DateTime xts_schledarrivalinoriginworkshopdateandtime { get; set; }

		public string xts_vehicleidentificationidname { get; set; }

		public Guid stageid { get; set; }

		public string xts_outsourceworkorderpersoninchargeidname { get; set; }

		public string xts_categoryname { get; set; }

		public string xts_handlingname { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public bool xjp_compulsoryautomobileliabilityinsurance { get; set; }

		public string xts_businessunitidname { get; set; }

		public Guid createdby { get; set; }

		public string xts_outsourceworkorder { get; set; }

		public Guid modifiedby { get; set; }

		public string xts_vehiclemodelname { get; set; }

		public decimal xts_totalbillingamountaftertax { get; set; }

		public string createdbyyominame { get; set; }

		public Guid owninguser { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string xts_customeridname { get; set; }

		public Guid owningbusinessunit { get; set; }

		public decimal xts_downpaymentamountreceived { get; set; }

		public bool xjp_ocr { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public string xts_finishpattern { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public DateTime xts_scheduledoutsourcearrivaldateandtime { get; set; }

		public string xts_arrivalpattern { get; set; }

		public string xts_outsourceworkordermemo { get; set; }

		public string xts_servicecategoryidname { get; set; }

		public string statecodename { get; set; }

		public decimal xts_totalbillingamountaftertax_base { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
