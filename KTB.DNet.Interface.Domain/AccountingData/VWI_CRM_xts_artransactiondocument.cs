#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_xts_artransactiondocument  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 13/01/2020 14:03:26
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_xts_artransactiondocument
    {
        public Int64 IDRow { get; set; }

		public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xts_sourcetypename { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid owninguser { get; set; }

		public string transactioncurrencyidname { get; set; }

		public decimal xts_transactionamount_base { get; set; }

		public int statecode { get; set; }

		public string owneridname { get; set; }

		public string statecodename { get; set; }

		public decimal xts_receiptamount_base { get; set; }

		public Guid createdonbehalfby { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public int importsequencenumber { get; set; }

		public string modifiedbyyominame { get; set; }

		public string xts_company { get; set; }

		public string xts_transactiondocumentnumber { get; set; }

		public decimal xts_receiptamount { get; set; }

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

		public string owneridyominame { get; set; }

		public DateTime modifiedon { get; set; }

		public decimal exchangerate { get; set; }

		public Guid xts_artransactiondocumentid { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public int statuscode { get; set; }

		public string createdbyname { get; set; }

		public DateTime createdon { get; set; }

		public string xts_customer { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string xts_artransactionnumber { get; set; }

		public decimal xts_transactionamount { get; set; }

		public Int64 versionnumber { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string xts_sourcetype { get; set; }

		public Guid ownerid { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string RowStatus { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
