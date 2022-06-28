#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_MSPMembershipDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.Model
{
    public class VWI_MSPMembershipDto : ReadDtoBase
    {
        public DateTime MSPExRegistrationDate { get; set; }
        public string MSPExDealerCode { get; set; }
        public string MSPExCode { get; set; }
        public string MSPExDescription { get; set; }
        public int MSPExKm { get; set; }
        public DateTime MSPExValidUntil { get; set; }
        public int MSPExDuration { get; set; }
        public int MSPCustomerID { get; set; }
        public int DealerId { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public int ChassisMasterID { get; set; }
        public string MSPCode { get; set; }
        public string ChassisNumber { get; set; }
        public string ColorCode { get; set; }
        public string VehicleTypeCode { get; set; }
        public string VehicleTypeDesc { get; set; }
        public int MSPKm { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public DateTime ValidUntil { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}

