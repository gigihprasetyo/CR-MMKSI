#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_servicecampaign class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 08 Feb 2021 11:14:57
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xts_servicecampaignDto : DtoBase
    {
        
		public Guid bsi_buoriginal { get; set; }

		public string bsi_buoriginalname { get; set; }

		public string bsi_owneridoriginal_text { get; set; }

		public string bsi_owneridoriginalguid { get; set; }

		public string bsi_owneridoriginallogicalname { get; set; }

		public string bsi_owneridoriginalname { get; set; }

		public Guid bsi_pbuoriginal { get; set; }

		public string bsi_pbuoriginalname { get; set; }

		public Guid createdby { get; set; }

		public string createdbyname { get; set; }

		public string createdbyyominame { get; set; }

		public DateTime? createdon { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string DealerCode { get; set; }

		public int? importsequencenumber { get; set; }

		public string ktb_buletindescription { get; set; }

		public bool? ktb_iscampaign { get; set; }

		public string ktb_iscampaignname { get; set; }

		public bool? ktb_isfieldfix { get; set; }

		public string ktb_isfieldfixname { get; set; }

		public bool? ktb_isinterfaced { get; set; }

		public string ktb_isinterfacedname { get; set; }

		public Guid ktb_servicecategoryid { get; set; }

		public string ktb_servicecategoryidname { get; set; }

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

		//public string RowStatus { get; set; }

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

		public string xts_description { get; set; }

		public DateTime? xts_enddate { get; set; }

		public string xts_locking { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public Guid xts_servicecampaignid { get; set; }

		public string xts_servicecampaignnumber { get; set; }

		public DateTime? xts_startdate { get; set; }

		public string xts_type { get; set; }

		public string xts_typename { get; set; }

    }
}
