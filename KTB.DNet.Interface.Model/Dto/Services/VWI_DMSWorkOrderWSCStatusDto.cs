#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_DMSWorkOrderWSCStatusDto  class
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
    public class VWI_DMSWorkOrderWSCStatusDto : ReadDtoBase
    {
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string ChassisNumber { get; set; }
        public string WorkOrderNumber { get; set; }
        public string PQRNo { get; set; }
        public int PQRType { get; set; }
        public string PQRTypeText { get; set; }
        public DateTime PQRDate { get; set; }
        public int PQRStatus { get; set; }
        public string PQRStatusText { get; set; }
        public string ClaimType { get; set; }
        public string ClaimNumber { get; set; }
        public string Description { get; set; }
        public string ClaimStatus { get; set; }
        public string WSCStatus { get; set; }
        public string WSCStatusText { get; set; }
        public Decimal LaborAmount { get; set; }
        public Decimal PartAmount { get; set; }
    }
}

