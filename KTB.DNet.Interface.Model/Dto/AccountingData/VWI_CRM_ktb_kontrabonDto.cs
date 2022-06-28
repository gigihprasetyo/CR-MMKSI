#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_kontrabonDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:46
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_ktb_kontrabonDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid owninguser { get; set; }

		public string transactioncurrencyidname { get; set; }

		public int statecode { get; set; }

		public string owneridname { get; set; }

		public string statecodename { get; set; }

		public Guid ktb_vendorid { get; set; }

		public Guid createdonbehalfby { get; set; }

		public DateTime ktb_tanggalkontrabon { get; set; }

		public string ktb_status { get; set; }

		public int importsequencenumber { get; set; }

		public string modifiedbyyominame { get; set; }

		public string ktb_statusname { get; set; }

		public decimal ktb_total_base { get; set; }

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

		public string ktb_vendoridname { get; set; }

		public Guid ktb_businessunitid { get; set; }

		public string ktb_say { get; set; }

		public string owneridyominame { get; set; }

		public string ktb_kontrabonno { get; set; }

		public DateTime modifiedon { get; set; }

		public decimal exchangerate { get; set; }

		public string ktb_handling { get; set; }

		public decimal ktb_total { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public int statuscode { get; set; }

		public string createdbyname { get; set; }

		public DateTime createdon { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public Guid ktb_kontrabonid { get; set; }

		public string ktb_businessunitidname { get; set; }

		public Int64 versionnumber { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string ktb_handlingname { get; set; }

		public Guid ownerid { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
