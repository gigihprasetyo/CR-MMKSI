#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : CRM_ktb_openfactureDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 17/02/2021 11:49:03
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_ktb_openfactureDto : DtoBase
    {
        public Guid owningteam { get; set; }

		public string statuscodename { get; set; }
		public string businessunitcode { get; set; }
		
		public int statecode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public string statecodename { get; set; }

		public Guid owninguser { get; set; }

		public Guid modifiedby { get; set; }

		public Int64 versionnumber { get; set; }

		public Guid ownerid { get; set; }

		public Guid ktb_openfactureid { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string ktb_name { get; set; }

		public string owneridname { get; set; }

		public string ktb_nomorchassis { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public int statuscode { get; set; }

		public string ktb_nomorfaktur { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string modifiedbyname { get; set; }

		public DateTime ktb_tanggalfaktur { get; set; }

		public DateTime modifiedon { get; set; }

		public string createdbyname { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string modifiedbyyominame { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public DateTime createdon { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string owneridtype { get; set; }

		public int importsequencenumber { get; set; }

		public Guid createdby { get; set; }

		public string createdbyyominame { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string owneridyominame { get; set; }

		public Guid createdonbehalfby { get; set; }
    }
}
