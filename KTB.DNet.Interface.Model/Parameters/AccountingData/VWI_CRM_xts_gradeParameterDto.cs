#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_gradeParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 16:02:00
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
	public class VWI_CRM_xts_gradeParameterDto : ParameterDtoBase, IValidatableObject
	{
		[AntiXss]
		public Int64 versionnumber { get; set; }
		[AntiXss]
		public DateTime createdon { get; set; }
		[AntiXss]
		public string xts_defaultconfigidname { get; set; }
		[AntiXss]
		public string statuscodename { get; set; }
		[AntiXss]
		public string xts_defaultconfigurationidname { get; set; }
		[AntiXss]
		public string xts_description { get; set; }
		[AntiXss]
		public string modifiedbyyominame { get; set; }
		[AntiXss]
		public Guid xts_defaultproductsegment1id { get; set; }
		[AntiXss]
		public string xts_registrationmodel { get; set; }
		[AntiXss]
		public string xts_gradenumber { get; set; }
		[AntiXss]
		public string modifiedbyname { get; set; }
		[AntiXss]
		public string xts_fuelcategoryname { get; set; }
		[AntiXss]
		public string xts_vehicletransmission { get; set; }
		[AntiXss]
		public Guid xts_defaultproductsegment3id { get; set; }
		[AntiXss]
		public string xts_defaultbrand { get; set; }
		[AntiXss]
		public DateTime overriddencreatedon { get; set; }
		[AntiXss]
		public string xts_vehiclebody { get; set; }
		[AntiXss]
		public string xts_defaultexterioridname { get; set; }
		[AntiXss]
		public Guid xts_gradeid { get; set; }
		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }
		[AntiXss]
		public string xts_defaultstylesidname { get; set; }
		[AntiXss]
		public Guid xts_defaultconfigurationid { get; set; }
		[AntiXss]
		public int statecode { get; set; }
		[AntiXss]
		public Guid xts_defaultstylesid { get; set; }
		[AntiXss]
		public string xts_defaultproductsegment3idname { get; set; }
		[AntiXss]
		public Guid xts_defaultstyleid { get; set; }
		[AntiXss]
		public int timezoneruleversionnumber { get; set; }
		[AntiXss]
		public Guid xts_defaultproductsegment2id { get; set; }
		[AntiXss]
		public string xts_locking { get; set; }
		[AntiXss]
		public string xts_defaultinterioridname { get; set; }
		[AntiXss]
		public string xts_defaultvehiclebrandidname { get; set; }
		[AntiXss]
		public Guid xts_defaultexteriorid { get; set; }
		[AntiXss]
		public string xjp_purposecategory { get; set; }
		[AntiXss]
		public Guid xts_defaultvehiclebrandid { get; set; }
		[AntiXss]
		public string xts_defaultproductsegment4idname { get; set; }
		[AntiXss]
		public string xts_grade { get; set; }
		[AntiXss]
		public int utcconversiontimezonecode { get; set; }
		[AntiXss]
		public DateTime modifiedon { get; set; }
		[AntiXss]
		public int importsequencenumber { get; set; }
		[AntiXss]
		public Guid xts_defaultproductsegment4id { get; set; }
		[AntiXss]
		public string xts_defaultstyleidname { get; set; }
		[AntiXss]
		public string xts_defaultproductsegment2idname { get; set; }
		[AntiXss]
		public string createdbyname { get; set; }
		[AntiXss]
		public string createdonbehalfbyname { get; set; }
		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }
		[AntiXss]
		public string xjp_purposecategoryname { get; set; }
		[AntiXss]
		public string xts_enginedisplacement { get; set; }
		[AntiXss]
		public string xts_vehicleengine { get; set; }
		[AntiXss]
		public Guid createdby { get; set; }
		[AntiXss]
		public Guid modifiedby { get; set; }
		[AntiXss]
		public string createdbyyominame { get; set; }
		[AntiXss]
		public string xts_fuelcategory { get; set; }
		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }
		[AntiXss]
		public Guid xts_defaultconfigid { get; set; }
		[AntiXss]
		public Guid createdonbehalfby { get; set; }
		[AntiXss]
		public int statuscode { get; set; }
		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }
		[AntiXss]
		public Guid organizationid { get; set; }
		[AntiXss]
		public string xts_drivingname { get; set; }
		[AntiXss]
		public string statecodename { get; set; }
		[AntiXss]
		public string organizationidname { get; set; }
		[AntiXss]
		public string xts_defaultbrandgeneration { get; set; }
		[AntiXss]
		public decimal xts_fuelconsumptionrate { get; set; }
		[AntiXss]
		public Guid xts_defaultinteriorid { get; set; }
		[AntiXss]
		public string xts_defaultproductsegment1idname { get; set; }

		public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var results = new List<ValidationResult>();
			return results;
		}
	}
}
