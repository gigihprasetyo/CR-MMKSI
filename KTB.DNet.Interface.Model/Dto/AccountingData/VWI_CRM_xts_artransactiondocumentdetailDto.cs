#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_artransactiondocumentdetailDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 13/01/2020 14:03:27
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_artransactiondocumentdetailDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid owninguser { get; set; }

		public string transactioncurrencyidname { get; set; }

		public int statecode { get; set; }

		public string owneridname { get; set; }

		public string statecodename { get; set; }

		public Guid createdonbehalfby { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string xts_transactiontype { get; set; }

		public string xts_customertransactionnumber { get; set; }

		public string xts_gljournaldetailreference { get; set; }

		public string xts_transactiontypename { get; set; }

		public int importsequencenumber { get; set; }

		public decimal xts_creditamount { get; set; }

		public string modifiedbyyominame { get; set; }

		public string xts_artransactiondocumentidname { get; set; }

		public string xts_gljournalnumber { get; set; }

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

		public decimal xts_creditamount_base { get; set; }

		public string owneridyominame { get; set; }

		public decimal xts_debitamount { get; set; }

		public DateTime modifiedon { get; set; }

		public decimal exchangerate { get; set; }

		public Guid xts_artransactiondocumentdetailid { get; set; }

		public decimal xts_debitamount_base { get; set; }

		public Guid xts_artransactiondocumentid { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public int statuscode { get; set; }

		public string createdbyname { get; set; }

		public DateTime createdon { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string xts_artransactiondocumentdetailnumber { get; set; }

		public Int64 versionnumber { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public Guid ownerid { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
