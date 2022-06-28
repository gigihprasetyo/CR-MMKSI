#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistServiceIncomingDto  class
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
using System.Runtime.Serialization;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_ServiceReminderDto : DtoBase
    {
        public int ID { get; set; }
        public int SalesforceID { get; set; }
        public int DealerCode { get; set; }
        public string ChassisNumber { get; set; }
        public string EngineNumber { get; set; }
        public string VehicleType { get; set; }
        public string WONumber { get; set; }
        public DateTime ServiceReminderDate { get; set; }
        public DateTime MaxFUDealerDate { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan BookingTime { get; set; }
        public string CaseNumber { get; set; }
        public int AssistServiceIncomingID { get; set; }
        public DateTime ServiceActualDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonPhoneNumber { get; set; }
        public string ServiceType { get; set; }
        public int TransactionType { get; set; }
        public int ActualKM { get; set; }
        public int Status { get; set; }
        public DateTime LastUpdateTime { get; set; }

    }
}

