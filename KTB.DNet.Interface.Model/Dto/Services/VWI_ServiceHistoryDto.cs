#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_ServiceHistoryDto  class
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
    public class VWI_ServiceHistoryDto : ReadDtoBase
    {
        public int ChassisMasterID { get; set; }
        public string KodeChassis { get; set; }
        public DateTime PKTDate { get; set; }
        public DateTime TglBukaTransaksi { get; set; }
        public DateTime TglTutupTransaksi { get; set; }
        public TimeSpan WaktuMasuk { get; set; }
        public TimeSpan WaktuKeluar { get; set; }
        public string DealerCode { get; set; }
        public string DealerBranchCode { get; set; }
        public string WorkOrderType { get; set; }
        public string WorkOrderCategoryCode { get; set; }
        public int KMService { get; set; }
        public string NoWorkOrder { get; set; }
        public string ServicePlaceCode { get; set; }
        public string ServiceTypeCode { get; set; }
    }
}

