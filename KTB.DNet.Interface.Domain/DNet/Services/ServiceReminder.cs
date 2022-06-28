#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : AssistServiceIncomingBP Domain class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 2021-03-23
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class ServiceReminder
    {
        public int ID { get; set; }
        public string SalesforceID { get; set; }
        public int DealerID { get; set; }
        public int DealerBranchID { get; set; }
        public string ChassisNumber { get; set; }
        public string EngineNumber { get; set; }
        public int ChassisMasterID { get; set; }
        public string VehicleType { get; set; }
        public Int16 CategoryID { get; set; }
        public DateTime ServiceReminderDate { get; set; }
        public DateTime MaxFUDealerDate { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? BookingTime { get; set; }
        public string CaseNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonPhoneNumber { get; set; }
        public int PMKindID { get; set; }
        public Int16 TransactionType { get; set; }
        public int AssistServiceIncomingID { get; set; }
        public string WONumber { get; set; }
        public DateTime? ServiceActualDate { get; set; }
        public int ActualKM { get; set; }
        public int ActualServiceDealerID { get; set; }
        public int ActualServiceDealerBranchID { get; set; }
        public DateTime PKTDate { get; set; }
        public Int16 SourceFlag { get; set; }
        public string Remark { get; set; }
        public Int16 Status { get; set; }
        public Int16 RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
