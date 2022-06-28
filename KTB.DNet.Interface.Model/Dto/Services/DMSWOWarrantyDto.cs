#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DMSWOWarrantyDto  class
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
    public class DMSWOWarrantyDto : DtoBase
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        public int DealerBranchID { get; set; }
        public string ChassisNumber { get; set; }
        public Boolean isBB { get; set; }
        public string WorkOrderNumber { get; set; }
        public DateTime FailureDate { get; set; }
        public DateTime ServiceDate { get; set; }
        public string Owner { get; set; }
        public int Mileage { get; set; }
        public string ServiceBuletin { get; set; }
        public string Symptoms { get; set; }
        public string Causes { get; set; }
        public string Results { get; set; }
        public string Notes { get; set; }
        public string CreateBy { get; set; }
    }
}

