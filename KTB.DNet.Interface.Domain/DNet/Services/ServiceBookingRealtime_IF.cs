#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : ServiceMMS_IF Domain class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 2021-10-26
//
// ===========================================================================	
#endregion

using System;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Domain
{
    public class ServiceBookingRealtimeAll_IF
    {
        public int ID { get; set; }
        public string ServiceBookingCode { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string ChassisNumber { get; set; }
        public int StallMasterID { get; set; }
        public string StallCode { get; set; }
        public string VehicleTypeCode { get; set; }
        public string VechileModelDescription { get; set; }
        public string VechileTypeDescription { get; set; }
        public string PlateNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public int Odometer { get; set; }
        public int ServiceAdvisorID { get; set; }
        public string ServiceAdvisorName { get; set; }
        public string IncomingPlan { get; set; }
        public string StallServiceType { get; set; }
        public decimal StandardTime { get; set; }
        public DateTime IncomingDateStart { get; set; }
        public DateTime IncomingDateEnd { get; set; }
        public DateTime WorkingTimeStart { get; set; }
        public DateTime WorkingTimeEnd { get; set; }
        public string IsMitsubishi { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public decimal Total { get; set; }
        public string ServiceBookingActivities { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }

}
