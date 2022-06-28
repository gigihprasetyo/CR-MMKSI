
#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceBookingDto class
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
using System.Collections.Generic;

namespace KTB.DNet.Interface.Model
{
    public class ServiceBookingDto : DtoBase
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        //public int ChassisMasterID { get; set; }
        public string ServiceBookingCode { get; set; }
        public string ChassisNumber { get; set; }
        public int VechileTypeID { get; set; }
        public string PlateNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public int Odometer { get; set; }
        //public int ServiceTypeID { get; set; }
        //public string KindCode { get; set; }
        public int StallMasterID { get; set; }
        public DateTime IncomingDateStart { get; set; }
        public DateTime IncomingDateEnd { get; set; }
        public string Notes { get; set; }
        public int Status { get; set; }
        public string PickupType { get; set; }
        public string StallServiceType { get; set; }
        public string IsMitsubishi { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public List<ServiceBookingActivityDto> servicebookingactivity { get; set; }


    }
}

