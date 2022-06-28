#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_POReceiptDealerDto  class
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
using KTB.DNet.Interface.Model.CustomAttribute;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_POReceiptDealerDto : DtoBase
    {
        public int ID { get; set; }
        [IgnoreDataMember]
        public int EndCustomerID { get; set; }

        public string ChassisNumber { get; set; }

        public int CategoryID { get; set; }

        public string VehicleColorCode { get; set; }

        public string VehicleColorDesc { get; set; }

        public string VehicleKindCode { get; set; }

        public string VehicleKindDesc { get; set; }

        public string VehicleTypeCode { get; set; }

        public string VehicleTypeDesc { get; set; }

        public string SoldDealerCode { get; set; }

        public string SoldDealerName { get; set; }

        public string DONumber { get; set; }

        public string SONumber { get; set; }

        public decimal DiscountAmount { get; set; }

        public string PONumber { get; set; }

        public string EngineNumber { get; set; }
        [IgnoreDataMember]
        public string SerialNumber { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime DODate { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime GIDate { get; set; }

        public decimal ParkingAmount { get; set; }

        public int ProductionYear { get; set; }

        public string MaterialNumber { get; set; }

        public string MaterialDescription { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime ATDDate
        {
            get; set;
        }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime ETADate
        {
            get; set;
        }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime ATADate
        {
            get; set;
        }

        public string SegmentType { get; set; }
        public string FuelType { get; set; }
        public string TransmitType { get; set; }
        public string DriveSystemType { get; set; }
        public string VariantType { get; set; }
        public string SpeedType { get; set; }
    }
}
