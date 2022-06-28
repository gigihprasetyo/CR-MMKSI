#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_equipmentParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_equipmentParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string bsi_employeeidentificationtype { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xts_vehiclemodelname { get; set; }

		[AntiXss]
		public string xts_vehiclemodelidname { get; set; }

		[AntiXss]
		public Guid equipmentid { get; set; }

		[AntiXss]
		public decimal xts_addlaborrateperhour_base { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_vehiclemodelid { get; set; }

		[AntiXss]
		public bool xts_default { get; set; }

		[AntiXss]
		public string xts_type { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string bsi_employeeidentificationtypename { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public string skills { get; set; }

		[AntiXss]
		public Guid xts_vehiclebrandid { get; set; }

		[AntiXss]
		public decimal xts_addlaborrateperhour { get; set; }

		[AntiXss]
		public Int32 timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid siteid { get; set; }

		[AntiXss]
		public string displayinserviceviewsname { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_equipmentnumber { get; set; }

		[AntiXss]
		public Int32 timezonecode { get; set; }

		[AntiXss]
		public string xts_manufactureridname { get; set; }

		[AntiXss]
		public Guid xts_employeeid { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public Int32 utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_typename { get; set; }

		[AntiXss]
		public string siteidname { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public Int32 importsequencenumber { get; set; }

		[AntiXss]
		public string description { get; set; }

		[AntiXss]
		public string xts_defaultname { get; set; }

		[AntiXss]
		public string xts_objective { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xts_objectivename { get; set; }

		[AntiXss]
		public bool isdisabled { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_pkcombinationkey { get; set; }

		[AntiXss]
		public string isdisabledname { get; set; }

		[AntiXss]
		public string xts_employeeidname { get; set; }

		[AntiXss]
		public string businessunitidname { get; set; }

		[AntiXss]
		public Guid calendarid { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public bool displayinserviceviews { get; set; }

		[AntiXss]
		public string xts_vehicleidentificationnumber { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string emailaddress { get; set; }

		[AntiXss]
		public string xts_vehiclebrandidname { get; set; }

		[AntiXss]
		public Int32 xts_orderdisplay { get; set; }

		[AntiXss]
		public Guid xts_manufacturerid { get; set; }

		[AntiXss]
		public string name { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid organizationid { get; set; }

		[AntiXss]
		public string organizationidname { get; set; }

		[AntiXss]
		public Guid businessunitid { get; set; }


		[AntiXss]
		public Guid msdyn_organizationalunitid { get; set; }

		[AntiXss]
		public string msdyn_organizationalunitidname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

		public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
