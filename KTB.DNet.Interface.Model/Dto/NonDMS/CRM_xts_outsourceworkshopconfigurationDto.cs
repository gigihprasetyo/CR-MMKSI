#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_outsourceworkshopconfiguration class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 19 Jan 2021 09:25:04
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xts_outsourceworkshopconfigurationDto : DtoBase
    {
        
		public Guid createdby { get; set; }

		public string createdbyname { get; set; }

		public string createdbyyominame { get; set; }

		public DateTime? createdon { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string DealerCode { get; set; }

		public int? importsequencenumber { get; set; }

		public Guid modifiedby { get; set; }

		public string modifiedbyname { get; set; }

		public string modifiedbyyominame { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public DateTime? overriddencreatedon { get; set; }

		public Guid ownerid { get; set; }

		public string owneridname { get; set; }

		public string owneridtype { get; set; }

		public string owneridyominame { get; set; }

		public Guid owningbusinessunit { get; set; }

		public Guid owningteam { get; set; }

		public Guid owninguser { get; set; }

		public short? RowStatus { get; set; }

		public string SourceType { get; set; }

		public int? statecode { get; set; }

		public string statecodename { get; set; }

		public int? statuscode { get; set; }

		public string statuscodename { get; set; }

		public int? timezoneruleversionnumber { get; set; }

		public int? utcconversiontimezonecode { get; set; }

		public long? versionnumber { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xts_businessunitidname { get; set; }

		public string xts_category { get; set; }

		public string xts_categoryname { get; set; }

		public string xts_locking { get; set; }

		public string xts_outsourcebusinessunitdescription { get; set; }

		public Guid xts_outsourcebusinessunitid { get; set; }

		public string xts_outsourcebusinessunitidname { get; set; }

		public string xts_outsourceworkshopconfiguration { get; set; }

		public Guid xts_outsourceworkshopconfigurationid { get; set; }

		public string xts_pkcombinationkey { get; set; }

		public Guid xts_taxzoneid { get; set; }

		public string xts_taxzoneidname { get; set; }

		public string xts_vendordescription { get; set; }

		public Guid xts_vendorid { get; set; }

		public string xts_vendoridname { get; set; }

    }
}
