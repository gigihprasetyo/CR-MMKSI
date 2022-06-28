#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vehiclespecificationParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/12/2019 3:26:37
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
    public class VWI_CRM_xts_vehiclespecificationParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string xts_shiftpositioncode { get; set; }

		[AntiXss]
		public string ktb_fueltype { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public int xts_rearrearaxleweight { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public decimal xjp_commercialacquisitiontaxamount_base { get; set; }

		[AntiXss]
		public decimal xjp_privateacquisitiontaxamount { get; set; }

		[AntiXss]
		public DateTime xts_lifecyclebegin { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public int xts_capacity { get; set; }

		[AntiXss]
		public int xts_frontrearaxleweight { get; set; }

		[AntiXss]
		public string ktb_vehicledescription { get; set; }

		[AntiXss]
		public string xts_registrationmodel { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xts_vehiclespecification { get; set; }

		[AntiXss]
		public int xts_frontfrontaxleweight { get; set; }

		[AntiXss]
		public DateTime xts_lifecycleend { get; set; }

		[AntiXss]
		public string xts_shiftpositioncodename { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_vehiclemodelid { get; set; }

		[AntiXss]
		public int xts_rearfrontaxleweight { get; set; }

		[AntiXss]
		public string xts_transmission { get; set; }

		[AntiXss]
		public string xjp_weighttaxidname { get; set; }

		[AntiXss]
		public string xjp_usageidname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xjp_vehicleclassification { get; set; }

		[AntiXss]
		public string xts_numberofgearname { get; set; }

		[AntiXss]
		public Guid xts_shapeid { get; set; }

		[AntiXss]
		public Guid xts_bodyshapeid { get; set; }

		[AntiXss]
		public decimal xjp_commercialacquisitiontaxamount { get; set; }

		[AntiXss]
		public string xjp_steeringwheelpositionname { get; set; }

		[AntiXss]
		public int xts_warrantyperiodmonth { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xjp_grade { get; set; }

		[AntiXss]
		public string ktb_transmissiondnet { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid xjp_compulsoryinsuranceid { get; set; }

		[AntiXss]
		public Guid xjp_automobiletaxid { get; set; }

		[AntiXss]
		public string ktb_segmenttypeidname { get; set; }

		[AntiXss]
		public decimal xts_dimensionlength { get; set; }

		[AntiXss]
		public Guid xjp_usageid { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_manufactureridname { get; set; }

		[AntiXss]
		public decimal xts_vehicleweight { get; set; }

		[AntiXss]
		public int xjp_maximumcapacityload1max { get; set; }

		[AntiXss]
		public int xjp_maximumcapacityload2min { get; set; }

		[AntiXss]
		public string xts_bodyshapeidname { get; set; }

		[AntiXss]
		public decimal xts_dimensionwidth { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_chassismodel { get; set; }

		[AntiXss]
		public Guid ktb_productcategoryid { get; set; }

		[AntiXss]
		public int xts_capacity2min { get; set; }

		[AntiXss]
		public string xts_classificationnumber { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xjp_vehicleorigin { get; set; }

		[AntiXss]
		public Guid xjp_automobilelayoutid { get; set; }

		[AntiXss]
		public string xjp_steeringwheelposition { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public Guid xts_usedvehiclefuelcategoryid { get; set; }

		[AntiXss]
		public string xjp_vehicleclassificationname { get; set; }

		[AntiXss]
		public Guid xts_vehiclespecificationid { get; set; }

		[AntiXss]
		public string xts_pkcombinationkey { get; set; }

		[AntiXss]
		public decimal xjp_privateacquisitiontaxamount_base { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xts_numberofgear { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public decimal xts_enginedisplacement { get; set; }

		[AntiXss]
		public string xjp_automobilelayoutidname { get; set; }

		[AntiXss]
		public string xjp_vehicleoriginname { get; set; }

		[AntiXss]
		public string xts_vehiclemodelidname { get; set; }

		[AntiXss]
		public string xjp_motormodel { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xjp_typename { get; set; }

		[AntiXss]
		public Guid xjp_weighttaxid { get; set; }

		[AntiXss]
		public string ktb_productcategoryidname { get; set; }

		[AntiXss]
		public string ktb_drivesystem { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string xts_usedvehiclefuelcategoryidname { get; set; }

		[AntiXss]
		public string xts_maximumcapacityload { get; set; }

		[AntiXss]
		public string xjp_automobiletaxidname { get; set; }

		[AntiXss]
		public string xts_numberofdoorname { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string xjp_madeinidname { get; set; }

		[AntiXss]
		public Guid xts_manufacturerid { get; set; }

		[AntiXss]
		public Guid organizationid { get; set; }

		[AntiXss]
		public string xts_shapeidname { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public decimal xts_maxkmwarranty { get; set; }

		[AntiXss]
		public decimal xts_dimensionheight { get; set; }

		[AntiXss]
		public string xjp_compulsoryinsuranceidname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string organizationidname { get; set; }

		[AntiXss]
		public Guid ktb_segmenttypeid { get; set; }

		[AntiXss]
		public string xts_transmissionname { get; set; }

		[AntiXss]
		public string xts_numberofdoor { get; set; }

		[AntiXss]
		public Guid xjp_madeinid { get; set; }

		[AntiXss]
		public string xts_modelspecification { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
