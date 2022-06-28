#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_equipment class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 18 Jan 2021 16:07:58
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_equipmentDto : DtoBase
    {
        
		public string bsi_employeeidentificationtype { get; set; }

		public string bsi_employeeidentificationtypename { get; set; }

		public Guid businessunitid { get; set; }

		public string businessunitidname { get; set; }

		public Guid calendarid { get; set; }

		public Guid createdby { get; set; }

		public string createdbyname { get; set; }

		public string createdbyyominame { get; set; }

		public DateTime? createdon { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string DealerCode { get; set; }

		public string description { get; set; }

		public bool? displayinserviceviews { get; set; }

		public string displayinserviceviewsname { get; set; }

		public string emailaddress { get; set; }

		public Guid equipmentid { get; set; }

		public decimal? exchangerate { get; set; }

		public int? importsequencenumber { get; set; }

		public bool? isdisabled { get; set; }

		public string isdisabledname { get; set; }

		public Guid modifiedby { get; set; }

		public string modifiedbyname { get; set; }

		public string modifiedbyyominame { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public Guid msdyn_organizationalunitid { get; set; }

		public string msdyn_organizationalunitidname { get; set; }

		public string name { get; set; }

		public Guid organizationid { get; set; }

		public string organizationidname { get; set; }

		public DateTime? overriddencreatedon { get; set; }

		//public string RowStatus { get; set; }

		public Guid siteid { get; set; }

		public string siteidname { get; set; }

		public string skills { get; set; }

		public string SourceType { get; set; }

		public int? timezonecode { get; set; }

		public int? timezoneruleversionnumber { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string transactioncurrencyidname { get; set; }

		public int? utcconversiontimezonecode { get; set; }

		public long? versionnumber { get; set; }

		public decimal? xts_addlaborrateperhour { get; set; }

		public decimal? xts_addlaborrateperhour_base { get; set; }

		public bool? xts_default { get; set; }

		public string xts_defaultname { get; set; }

		public Guid xts_employeeid { get; set; }

		public string xts_employeeidname { get; set; }

		public string xts_equipmentnumber { get; set; }

		public string xts_locking { get; set; }

		public Guid xts_manufacturerid { get; set; }

		public string xts_manufactureridname { get; set; }

		public string xts_objective { get; set; }

		public string xts_objectivename { get; set; }

		public int? xts_orderdisplay { get; set; }

		public string xts_pkcombinationkey { get; set; }

		public Guid xts_productid { get; set; }

		public string xts_productidname { get; set; }

		public string xts_type { get; set; }

		public string xts_typename { get; set; }

		public Guid xts_vehiclebrandid { get; set; }

		public string xts_vehiclebrandidname { get; set; }

		public string xts_vehicleidentificationnumber { get; set; }

		public Guid xts_vehiclemodelid { get; set; }

		public string xts_vehiclemodelidname { get; set; }

		public string xts_vehiclemodelname { get; set; }

    }
}
