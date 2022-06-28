#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_equipmentDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:44
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_equipmentDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public Int64 versionnumber { get; set; }

		public DateTime createdon { get; set; }

		public string modifiedbyyominame { get; set; }

		public string bsi_employeeidentificationtype { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string modifiedbyname { get; set; }

		public string xts_vehiclemodelname { get; set; }

		public string xts_vehiclemodelidname { get; set; }

		public Guid equipmentid { get; set; }

		public decimal xts_addlaborrateperhour_base { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public Guid xts_vehiclemodelid { get; set; }

		public bool xts_default { get; set; }

		public string xts_type { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string bsi_employeeidentificationtypename { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal exchangerate { get; set; }

		public string xts_productidname { get; set; }

		public string skills { get; set; }

		public Guid xts_vehiclebrandid { get; set; }

		public decimal xts_addlaborrateperhour { get; set; }

		public Int32 timezoneruleversionnumber { get; set; }

		public Guid siteid { get; set; }

		public string displayinserviceviewsname { get; set; }

		public string xts_locking { get; set; }

		public string xts_equipmentnumber { get; set; }

		public Int32 timezonecode { get; set; }

		public string xts_manufactureridname { get; set; }

		public Guid xts_employeeid { get; set; }

		public string createdonbehalfbyname { get; set; }

		public Int32 utcconversiontimezonecode { get; set; }

		public string xts_typename { get; set; }

		public string siteidname { get; set; }

		public DateTime modifiedon { get; set; }

		public Int32 importsequencenumber { get; set; }

		public string description { get; set; }

		public string xts_defaultname { get; set; }

		public string xts_objective { get; set; }

		public string createdbyname { get; set; }

		public string xts_objectivename { get; set; }

		public bool isdisabled { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string xts_pkcombinationkey { get; set; }

		public string isdisabledname { get; set; }

		public string xts_employeeidname { get; set; }

		public string businessunitidname { get; set; }

		public Guid calendarid { get; set; }

		public Guid xts_productid { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public Guid createdby { get; set; }

		public Guid modifiedby { get; set; }

		public bool displayinserviceviews { get; set; }

		public string xts_vehicleidentificationnumber { get; set; }

		public string createdbyyominame { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string emailaddress { get; set; }

		public string xts_vehiclebrandidname { get; set; }

		public Int32 xts_orderdisplay { get; set; }

		public Guid xts_manufacturerid { get; set; }

		public string name { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public Guid organizationid { get; set; }

		public string organizationidname { get; set; }

		public Guid businessunitid { get; set; }

		public Guid msdyn_organizationalunitid { get; set; }

		public string msdyn_organizationalunitidname { get; set; }

		public string msdyn_companycode { get; set; }

	}
}
