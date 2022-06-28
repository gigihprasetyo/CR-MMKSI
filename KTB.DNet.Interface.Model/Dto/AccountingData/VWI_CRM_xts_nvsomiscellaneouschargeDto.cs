#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_nvsomiscellaneouschargeDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/01/2020 8:22:49
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_nvsomiscellaneouschargeDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Int64 versionnumber { get; set; }

		public Guid xts_consumptiontaxid { get; set; }

		public DateTime createdon { get; set; }

		public decimal xts_amount { get; set; }

		public string statuscodename { get; set; }

		public decimal xts_baseamount_base { get; set; }

		public bool ktb_caroserie { get; set; }

		public string xts_consumptiontaxidname { get; set; }

		public string xts_miscellaneouschargetemplatereferenceidname { get; set; }

		public string owneridtype { get; set; }

		public Guid xts_nvsqmiscellaneouschargeid { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string modifiedbyname { get; set; }

		public Guid xts_nvsomiscellaneouschargeid { get; set; }

		public string ktb_caroseriename { get; set; }

		public string xts_newvehiclesalesorderidname { get; set; }

		public string ktb_caroseriesname { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string xts_eventdata { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal exchangerate { get; set; }

		public string xts_nvsqmiscellaneouschargeidname { get; set; }

		public Guid xts_miscellaneouschargeid { get; set; }

		public Guid xts_newvehiclesalesorderid { get; set; }

		public decimal xts_totalamount { get; set; }

		public int statecode { get; set; }

		public string owneridname { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string xts_taxablename { get; set; }

		public string xts_locking { get; set; }

		public string xts_miscellaneouschargeidname { get; set; }

		public decimal xts_amount_base { get; set; }

		public decimal xts_totalamount_base { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string xts_pluginflag { get; set; }

		public string xts_nvsomiscellaneouscharge { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public decimal xts_estimatedmiscchargescost { get; set; }

		public Guid ownerid { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public decimal xts_consumptiontaxamount_base { get; set; }

		public decimal xts_consumptiontaxamount { get; set; }

		public string createdbyname { get; set; }

		public string ktb_miscchargedescription { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public decimal xts_baseamount { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public Guid owningteam { get; set; }

		public Guid createdby { get; set; }

		public Guid modifiedby { get; set; }

		public string createdbyyominame { get; set; }

		public Guid owninguser { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string modifiedbyyominame { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public decimal xts_estimatedmiscchargescost_base { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public Guid xts_miscellaneouschargetemplatereferenceid { get; set; }

		public string owneridyominame { get; set; }

		public string xts_taxable { get; set; }

		public string statecodename { get; set; }

		public string xts_businessunitidname { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
