#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : FreeServiceDto  class
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
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class FreeServiceDto : DtoBase
    {
        [IgnoreDataMember]
        public int ID { get; set; }
        [IgnoreDataMember]
        public string Status { get; set; }
        [IgnoreDataMember]
        public int ChassisMasterID { get; set; }
        [IgnoreDataMember]
        public byte FSKindID { get; set; }
        [IgnoreDataMember]
        public int MileAge { get; set; }
        [IgnoreDataMember]
        public DateTime ServiceDate { get; set; }
        [IgnoreDataMember]
        public int ServiceDealerID { get; set; }
        [IgnoreDataMember]
        public int DealerBranchID { get; set; }
        [IgnoreDataMember]
        public DateTime SoldDate { get; set; }
        [IgnoreDataMember]
        public string NotificationNumber { get; set; }
        [IgnoreDataMember]
        public string NotificationType { get; set; } 
        [IgnoreDataMember]
        public Decimal TotalAmount { get; set; }        
        
        [IgnoreDataMember]
        public Decimal PPNAmount { get; set; }
        [IgnoreDataMember]
        public Decimal PPHAmount { get; set; }
        
        public string Reject { get; set; }
        [IgnoreDataMember]
        public int Reason { get; set; }
        public string ReasonCode { get; set; }
        public string ReasonDesc { get; set; }
        [IgnoreDataMember]
        public string ReleaseBy { get; set; }
        [IgnoreDataMember]
        public DateTime ReleaseDate { get; set; }
        [IgnoreDataMember]
        public int FleetRequestID { get; set; }
        [IgnoreDataMember]
        public string VisitType { get; set; }
        public string DealerCode { get; set; }
        public string DealerBranchCode { get; set; }
        public string ChassisNumber { get; set;}
        public string WorkOrderNumber { get; set; }
        public Decimal LabourAmount { get; set; }
        public Decimal PartAmount { get; set; }
        public Decimal TotalPartBeforeRounding { get; set; }
        public Decimal Rounding { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public List<FreeServicePartDetailDto> FreeServicePartDetails { get; set; }
    }
}

