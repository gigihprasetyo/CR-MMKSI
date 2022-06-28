#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PMHeaderDto  class
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
    public class PMHeaderDto : DtoBase
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        public int DealerBranchID { get; set; }
        public int ChassisNumberID { get; set; }
        public int PMKindID { get; set; }
        public int StandKM { get; set; }
        public DateTime ServiceDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PMStatus { get; set; }
        public string EntityType { get; set; }
        public string WorkOrderNumber { get; set; }
        public string BookingNo { get; set; }
        public string VisitType { get; set; }
    }
}
