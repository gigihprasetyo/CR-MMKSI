#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : VWI_VehicleInformation_IF class
 GENERATED BY  : Admin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 24 Aug 2021
 ===========================================================================
*/
#endregion

using System;
using System.ComponentModel;
using System.Data.SqlTypes;

namespace KTB.DNet.Interface.Domain
{
    public class VWI_VehicleInformation_IF
    {
        public int ID { get; set; }
        public string ChassisNumber { get; set; }
        public int IsBB { get; set; }
        public string SategoryCode { get; set; }
        public string SategoryDesc { get; set; }
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
        public string speedType { get; set; }
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
        public string PKTDate { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int WSCDuration { get; set; }
        public DateTime WSCStart { get; set; }
        public DateTime WSCEnd { get; set; }
        public string WebModel { get; set; }
        public string WebVariant { get; set; }
        public string ColorWeb { get; set; }
        public string PDIExpiredStatus { get; set; }
        public string PDIExpiredDate { get; set; }
        public string WarrantyActivationStatus { get; set; }
        public string WarrantyActivationDate { get; set; }
    }
}
