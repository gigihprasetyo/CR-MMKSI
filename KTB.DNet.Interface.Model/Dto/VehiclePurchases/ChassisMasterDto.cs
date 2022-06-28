#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterDto  class
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
    public class ChassisMasterDto : DtoBase
    {
        [IgnoreDataMember]
        public int ID { get; set; }
        [IgnoreDataMember]
        public int EndCustomerID { get; set; }

        public string ChassisNumber { get; set; }

        public int CategoryID { get; set; }
        [IgnoreDataMember]
        public int VehicleColorID { get; set; }

        public string VehicleColorCode { get; set; }

        public string VehicleColorDesc { get; set; }
        [IgnoreDataMember]
        public int VehicleKindID { get; set; }

        public string VehicleKindCode { get; set; }

        public string VehicleKindDesc { get; set; }

        public string VehicleTypeCode { get; set; }

        public string VehicleTypeDesc { get; set; }

        [IgnoreDataMember]
        public int SoldDealerID { get; set; }

        public string SoldDealerCode { get; set; }

        public string SoldDealerName { get; set; }

        public string DONumber { get; set; }

        public string SONumber { get; set; }
        [IgnoreDataMember]
        public int TOPID { get; set; }

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

        [IgnoreDataMember]
        public int ParkingDays { get; set; }

        public decimal ParkingAmount { get; set; }
        [IgnoreDataMember]
        public string FakturStatus { get; set; }
        [IgnoreDataMember]
        public string PendingDesc { get; set; }
        [IgnoreDataMember]
        public string IsSAPDownload { get; set; }
        [IgnoreDataMember]
        public string StockStatus { get; set; }
        [IgnoreDataMember]
        [DateTimeDisplayFormatAttribute]
        public DateTime LastUpdateProfile { get; set; }
        [IgnoreDataMember]
        public byte AlreadySaled { get; set; }
        [IgnoreDataMember]
        [DateTimeDisplayFormatAttribute]
        public DateTime AlreadySaledTime { get; set; }
        [IgnoreDataMember]
        public int StockDealer { get; set; }
        [IgnoreDataMember]
        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime StockDate { get; set; }

        public int ProductionYear { get; set; }

        public string MaterialNumber { get; set; }

        public string MaterialDescription { get; set; }
    }
}
