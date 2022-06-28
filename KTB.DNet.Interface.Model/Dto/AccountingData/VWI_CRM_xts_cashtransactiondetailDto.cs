#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_cashtransactiondetailDto  class
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
    public class VWI_CRM_xts_cashtransactiondetailDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public Guid xts_cashtransactiondimension1id { get; set; }

		public string ktb_vendoridname { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Int64 versionnumber { get; set; }

		public DateTime createdon { get; set; }

		public string statuscodename { get; set; }

		public Guid xts_reasonid { get; set; }

		public string xts_description { get; set; }

		public string modifiedbyyominame { get; set; }

		public string ktb_arreceiptidname { get; set; }

		public string owneridtype { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string xts_departmentidname { get; set; }

		public string modifiedbyname { get; set; }

		public Guid xts_cashtransactiondimension2id { get; set; }

		public string xts_businessunitidname { get; set; }

		public string owneridname { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal exchangerate { get; set; }

		public decimal xts_transactionamount_base { get; set; }

		public string xts_cashtransactiondetail { get; set; }

		public Guid xts_cashtransactiondimension3id { get; set; }

		public string xts_cashtransactionidname { get; set; }

		public int statecode { get; set; }

		public string xts_externaldocumentnumber { get; set; }

		public string xts_cashtransactiondimension1idname { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string xts_cashtransactiondimension2idname { get; set; }

		public Guid ktb_customerid { get; set; }

		public Guid owninguser { get; set; }

		public Guid ktb_vendorid { get; set; }

		public Guid xts_cashtransactiondimension4id { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string xts_cashtransactiondimension4idname { get; set; }

		public Guid xts_cashtransactiondetailid { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public Guid ownerid { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public string owneridyominame { get; set; }

		public string ktb_customeridname { get; set; }

		public Guid xts_cashtransactiondimension5id { get; set; }

		public string createdbyname { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string xts_accountidname { get; set; }

		public string xts_cashtransactiondimension3idname { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public Guid owningteam { get; set; }

		public Guid createdby { get; set; }

		public Guid modifiedby { get; set; }

		public Guid xts_cashtransactiondimension6id { get; set; }

		public string createdbyyominame { get; set; }

		public string xts_reasonidname { get; set; }

		public string transactioncurrencyidname { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string xts_cashtransactiondimension5idname { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public decimal xts_transactionamount { get; set; }

		public string xts_cashtransactiondimension6idname { get; set; }

		public Guid xts_departmentid { get; set; }

		public Guid ktb_arreceiptid { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public Guid xts_accountid { get; set; }

		public string statecodename { get; set; }

		public Guid xts_cashtransactionid { get; set; }

		public string ktb_customeridyominame { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
