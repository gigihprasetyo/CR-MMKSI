#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_VehicleInformationParameterDto  class
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
    public class VWI_VehicleInformationParameterDto : DtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public string ChassisNumber { get; set; }
        public int IsBB { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryDesc { get; set; }
        public string ColorCode { get; set; }
        public string ColorIndName { get; set; }
        public string ColorEngName { get; set; }
        public string MaterialDescription { get; set; }
        public string VehicleTypeCode { get; set; }
        public string VehicleTypeDesc { get; set; }
        public string ModelSearchTerm1 { get; set; }
        public string ModelSearchTerm2 { get; set; }
        public string SegmentType { get; set; }
        public string FuelType { get; set; }
        public string TransmitType { get; set; }
        public string DriveSystemType { get; set; }
        public string VariantType { get; set; }
        public string VehicleBrand { get; set; }
        public string SpeedType { get; set; }
        public int VehicleKindID { get; set; }
        public string Code { get; set; }
        public string VehicleKindDesc { get; set; }
        public int SoldDealerID { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string EngineNumber { get; set; }
        public string SerialNumber { get; set; }
        public int ProductionYear { get; set; }
        public string FleetCode { get; set; }
        public DateTime OpenFakturDate { get; set; }
        public DateTime FakturDate { get; set; }
        public string FSExtended { get; set; }
        public string FSProgram { get; set; }
        public DateTime PKTDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

