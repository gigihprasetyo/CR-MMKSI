#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_productsapconversionDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2020 17:06:22
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_ktb_productsapconversionDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string ktb_dnetid { get; set; }

		public string ktb_productsapdesc { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public Guid owningteam { get; set; }

		public int statecode { get; set; }

		public string owneridname { get; set; }

		public string statecodename { get; set; }

		public Guid owninguser { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string ktb_uomname { get; set; }

		public int importsequencenumber { get; set; }

		public Guid ktb_uom { get; set; }

		public Guid ktb_productsapconversionid { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string createdbyyominame { get; set; }

		public string ktb_typecode { get; set; }

		public string ktb_productreferenceidname { get; set; }

		public string modifiedbyname { get; set; }

		public Int64 versionnumber { get; set; }

		public Guid modifiedby { get; set; }

		public string modifiedbyyominame { get; set; }

		public Guid createdby { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string owneridtype { get; set; }

		public Guid ktb_productreferenceid { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public bool ktb_isinterfaced { get; set; }

		public string owneridyominame { get; set; }

		public string ktb_isinterfacedname { get; set; }

		public DateTime modifiedon { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public int statuscode { get; set; }

		public string createdbyname { get; set; }

		public DateTime createdon { get; set; }

		public string ktb_productsap { get; set; }

		public string ktb_productsapconversion { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string statuscodename { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string createdonbehalfbyname { get; set; }

		public Guid ownerid { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
