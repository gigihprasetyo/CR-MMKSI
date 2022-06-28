#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SVC_DailyReportParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2020 10:45:39
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
    public class VWI_CRM_SVC_DailyReportParameterDto : ParameterDtoBase, IValidatableObject
    {
		[AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string Site { get; set; }

		[AntiXss]
		public string WONumber { get; set; }

		[AntiXss]
		public string OrderType { get; set; }

		[AntiXss]
		public string State { get; set; }

		[AntiXss]
		public string ServiceCategory { get; set; }

		[AntiXss]
		public string Customer { get; set; }

		[AntiXss]
		public string Owner { get; set; }

		[AntiXss]
		public string VIN { get; set; }

		[AntiXss]
		public string EngineNumber { get; set; }

		[AntiXss]
		public string Category { get; set; }

		[AntiXss]
		public string ProductDescription { get; set; }

		[AntiXss]
		public string ProductType { get; set; }

		[AntiXss]
		public string PlateNumber { get; set; }

		[AntiXss]
		public string LeaderName { get; set; }

		[AntiXss]
		public string SAName { get; set; }

		[AntiXss]
		public DateTime DateIn { get; set; }

		[AntiXss]
		public DateTime DateOut { get; set; }

		[AntiXss]
		public int CurrentMileage { get; set; }

		[AntiXss]
		public string CustomerNo { get; set; }

		[AntiXss]
		public string Address { get; set; }

		[AntiXss]
		public string InvoiceNumber { get; set; }

		[AntiXss]
		public string BillToCustomer { get; set; }

		[AntiXss]
		public decimal SubtotalJasa { get; set; }

		[AntiXss]
		public decimal SubtotalPart { get; set; }

		[AntiXss]
		public decimal SubtotalOil { get; set; }

		[AntiXss]
		public decimal SubtotalSO { get; set; }

		[AntiXss]
		public decimal SubtotalSM { get; set; }

		[AntiXss]
		public decimal DiskonJasa { get; set; }

		[AntiXss]
		public decimal DiskonPart { get; set; }

		[AntiXss]
		public decimal DiskonOil { get; set; }

		[AntiXss]
		public decimal DiskonSO { get; set; }

		[AntiXss]
		public decimal DiskonSM { get; set; }

		[AntiXss]
		public decimal HppJasa { get; set; }

		[AntiXss]
		public decimal HppPart { get; set; }

		[AntiXss]
		public decimal HppOil { get; set; }

		[AntiXss]
		public decimal HppSO { get; set; }

		[AntiXss]
		public decimal HppSM { get; set; }

		[AntiXss]
		public decimal DppJasa { get; set; }

		[AntiXss]
		public decimal DppPart { get; set; }

		[AntiXss]
		public decimal DppOil { get; set; }

		[AntiXss]
		public decimal DppSO { get; set; }

		[AntiXss]
		public decimal DppSM { get; set; }

		[AntiXss]
		public decimal DppTotal { get; set; }

		[AntiXss]
		public decimal PpnTotal { get; set; }

		[AntiXss]
		public decimal GrandTotal { get; set; }

		[AntiXss]
		public string Mechanic { get; set; }

		[AntiXss]
		public string MechanicDescription { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
