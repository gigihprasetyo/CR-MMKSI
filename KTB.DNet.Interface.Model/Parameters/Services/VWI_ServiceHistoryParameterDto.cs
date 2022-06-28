#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_ServiceHistoryParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_ServiceHistoryParameterDto : DtoBase, IValidatableObject
    {
        public int ChassisMasterID { get; set; }
        public string KodeChassis { get; set; }
        public int SoldDealerID { get; set; }
        public int VehicleKindID { get; set; }
        public int VechileColorID { get; set; }
        public string ColorEngName { get; set; }
        public string MaterialDescription { get; set; }
        public string Description { get; set; }
        public string ChassisNumber { get; set; }
        public string EngineNumber { get; set; }
        public string SerialNumber { get; set; }
        public DateTime DODate { get; set; }
        public DateTime GIDate { get; set; }
        public DateTime FakturDate { get; set; }
        public DateTime OpenFakturDate { get; set; }
        public string FakturNumber { get; set; }
        public DateTime PKTDate { get; set; }
        public Object TglBukaTransaksi { get; set; }
        public Object TglTutupTransaksi { get; set; }
        public Object WaktuMasuk { get; set; }
        public Object WaktuKeluar { get; set; }
        public string DealerCode { get; set; }
        public string DealerBranchCode { get; set; }
        public string WorkOrderCategoryCode { get; set; }
        public int KMService { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}