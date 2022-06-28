#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_cashtransactionDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:54
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_cashtransactionDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid owninguser { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string xts_locking { get; set; }

		public int statecode { get; set; }

		public string xts_businessunitidname { get; set; }

		public decimal xts_totaltransactionamount_base { get; set; }

		public string owneridname { get; set; }

		public decimal xts_totalcontrolamount_base { get; set; }

		public string statecodename { get; set; }

		public Guid createdonbehalfby { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string xts_handlingname { get; set; }

		public Guid xts_cashtransactionid { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Guid xts_cashandbankid { get; set; }

		public int importsequencenumber { get; set; }

		public string xts_cashandbankidname { get; set; }

		public string modifiedbyyominame { get; set; }

		public decimal xts_totaltransactionamount { get; set; }

		public DateTime xts_cashtransactiondate { get; set; }

		public string xts_handling { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string createdbyyominame { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string modifiedbyname { get; set; }

		public Guid owningteam { get; set; }

		public Guid modifiedby { get; set; }

		public Guid createdby { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string owneridtype { get; set; }

		public string statuscodename { get; set; }

		public decimal exchangerate { get; set; }

		public decimal xts_totalcontrolamount { get; set; }

		public string xts_hascancelledname { get; set; }

		public string xts_cashtransactiontype { get; set; }

		public string xts_cashtransactiontypename { get; set; }

		public string owneridyominame { get; set; }

		public Guid xts_cashtransactionreferenceid { get; set; }

		public DateTime modifiedon { get; set; }

		public bool xts_hascancelled { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public int statuscode { get; set; }

		public string createdbyname { get; set; }

		public DateTime createdon { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xts_cashtransaction { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string xts_status { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string xts_cashtransactionreferenceidname { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public Int64 versionnumber { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string xts_statusname { get; set; }

		public Guid ownerid { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string xts_description { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
