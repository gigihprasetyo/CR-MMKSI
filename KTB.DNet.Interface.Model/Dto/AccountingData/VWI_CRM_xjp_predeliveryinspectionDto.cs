#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_predeliveryinspectionDto  class
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
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xjp_predeliveryinspectionDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string ktb_isinterfacedname { get; set; }

		public string xjp_customernumber { get; set; }

		public Int64 versionnumber { get; set; }

		public decimal xjp_actualcost_base { get; set; }

		public string xjp_stockidname { get; set; }

		public string xjp_pdistatusname { get; set; }

		public Guid xjp_productid { get; set; }

		public string statuscodename { get; set; }

		public string xjp_parentbusinessunitidname { get; set; }

		public DateTime xjp_arrivaldate { get; set; }

		public DateTime modifiedon { get; set; }

		public DateTime createdon { get; set; }

		public string owneridtype { get; set; }

		public string xjp_deliverybusinessunitidname { get; set; }

		public bool xjp_autoreceipt { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string xjp_handlingname { get; set; }

		public string modifiedbyname { get; set; }

		public string xjp_vendoridname { get; set; }

		public Guid xjp_departmentid { get; set; }

		public Guid xjp_exteriorcolorid { get; set; }

		public string xjp_styleidname { get; set; }

		public string xjp_productidname { get; set; }

		public Guid xjp_parentbusinessunitid { get; set; }

		public string owneridname { get; set; }

		public DateTime xjp_vehiclesuggestedarrivaldateandtime { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public DateTime xjp_desireddeliverydate { get; set; }

		public decimal xjp_actualcost { get; set; }

		public string xjp_pdicategory { get; set; }

		public string xjp_customeridname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal exchangerate { get; set; }

		public Guid xjp_vendorid { get; set; }

		public string xjp_pdistatus { get; set; }

		public Guid xjp_newvehiclesalesorderid { get; set; }

		public decimal xjp_costdifference_base { get; set; }

		public Guid owningteam { get; set; }

		public Guid xjp_interiorcolorid { get; set; }

		public int statecode { get; set; }

		public bool ktb_iskaroseri { get; set; }

		public Guid xjp_servicecategoryid { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public Guid xjp_predeliveryinspectionid { get; set; }

		public DateTime xjp_schedulearrivaldate { get; set; }

		public string xjp_customeridyominame { get; set; }

		public string xjp_servicecategoryidname { get; set; }

		public string xjp_handling { get; set; }

		public string xjp_newvehiclesalesorderidname { get; set; }

		public string xjp_exteriorcoloridname { get; set; }

		public string xjp_interiorcoloridname { get; set; }

		public Guid xjp_businessunitid { get; set; }

		public string traversedpath { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string ktb_interfaceexceptionmessage { get; set; }

		public DateTime xjp_completepdidate { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string xjp_matchunmatchidname { get; set; }

		public Guid ownerid { get; set; }

		public string xjp_autoreceiptname { get; set; }

		public int importsequencenumber { get; set; }

		public string owneridyominame { get; set; }

		public Guid modifiedby { get; set; }

		public string xjp_chassisnumber { get; set; }

		public string xjp_configurationidname { get; set; }

		public Guid xjp_customerid { get; set; }

		public Guid processid { get; set; }

		public Guid xjp_vehicleidentificationid { get; set; }

		public DateTime xjp_scheduledcompletepdidate { get; set; }

		public string createdbyname { get; set; }

		public string xjp_type { get; set; }

		public DateTime xjp_deliverydate { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string xjp_departmentidname { get; set; }

		public DateTime xjp_transactiondate { get; set; }

		public Guid stageid { get; set; }

		public bool xjp_deliverytostore { get; set; }

		public string xjp_eventdata { get; set; }

		public string xjp_deliverytostorename { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public decimal xjp_costdifference { get; set; }

		public Guid createdby { get; set; }

		public Guid xjp_deliverybusinessunitid { get; set; }

		public decimal xjp_estimatedcost { get; set; }

		public Guid xjp_configurationid { get; set; }

		public Guid xjp_salespersonid { get; set; }

		public string xjp_vehicleidentificationidname { get; set; }

		public string xjp_businessunitidname { get; set; }

		public string createdbyyominame { get; set; }

		public Guid owninguser { get; set; }

		public string transactioncurrencyidname { get; set; }

		public Guid xjp_matchunmatchid { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string modifiedbyyominame { get; set; }

		public string xjp_productdescription { get; set; }

		public string ktb_iskaroseriname { get; set; }

		public string xjp_pdicategoryname { get; set; }

		public string xjp_salespersonidname { get; set; }

		public string xjp_typename { get; set; }

		public string xjp_platenumber { get; set; }

		public string xjp_predeliveryinspectionnumber { get; set; }

		public Guid xjp_styleid { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public DateTime xjp_pdicancellationdate { get; set; }

		public Guid xjp_stockid { get; set; }

		public bool ktb_isinterfaced { get; set; }

		public decimal xjp_estimatedcost_base { get; set; }

		public string statecodename { get; set; }

		public string xjp_locking { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
