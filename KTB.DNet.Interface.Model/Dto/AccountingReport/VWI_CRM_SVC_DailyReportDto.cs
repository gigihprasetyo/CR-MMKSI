#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SVC_DailyReportDto  class
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
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_SVC_DailyReportDto : DtoBase
    {
		public string company { get; set; }

		public string businessunitcode { get; set; }
        
		public string Site { get; set; }

		public string WONumber { get; set; }

		public string OrderType { get; set; }

		public string State { get; set; }

		public string ServiceCategory { get; set; }

		public string Customer { get; set; }

		public string Owner { get; set; }

		public string VIN { get; set; }

		public string EngineNumber { get; set; }

		public string Category { get; set; }

		public string ProductDescription { get; set; }

		public string ProductType { get; set; }

		public string PlateNumber { get; set; }

		public string LeaderName { get; set; }

		public string SAName { get; set; }

		public DateTime DateIn { get; set; }

		public DateTime DateOut { get; set; }

		public int CurrentMileage { get; set; }

		public string CustomerNo { get; set; }

		public string Address { get; set; }

		public string InvoiceNumber { get; set; }

		public string BillToCustomer { get; set; }

		public decimal SubtotalJasa { get; set; }

		public decimal SubtotalPart { get; set; }

		public decimal SubtotalOil { get; set; }

		public decimal SubtotalSO { get; set; }

		public decimal SubtotalSM { get; set; }

		public decimal DiskonJasa { get; set; }

		public decimal DiskonPart { get; set; }

		public decimal DiskonOil { get; set; }

		public decimal DiskonSO { get; set; }

		public decimal DiskonSM { get; set; }

		public decimal HppJasa { get; set; }

		public decimal HppPart { get; set; }

		public decimal HppOil { get; set; }

		public decimal HppSO { get; set; }

		public decimal HppSM { get; set; }

		public decimal DppJasa { get; set; }

		public decimal DppPart { get; set; }

		public decimal DppOil { get; set; }

		public decimal DppSO { get; set; }

		public decimal DppSM { get; set; }

		public decimal DppTotal { get; set; }

		public decimal PpnTotal { get; set; }

		public decimal GrandTotal { get; set; }

		public string Mechanic { get; set; }

		public string MechanicDescription { get; set; }
    }
}
