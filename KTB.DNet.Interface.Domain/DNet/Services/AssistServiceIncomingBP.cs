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
    public class AssistServiceIncomingBPIF
    {
        public int ID { get; set; }
        public int AssistUploadLogID { get; set; }
        public DateTime? TglPengajuanEstimasi { get; set; }
        public DateTime? TglPersetujuanEstimasi { get; set; }
        public DateTime? TglBukaTransaksi { get; set; }
        public DateTime? WaktuMasuk { get; set; }
        public DateTime? TglJanjiSelesai { get; set; }
        public DateTime? TglTutupTransaksi { get; set; }
        public DateTime? WaktuKeluar { get; set; }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public int TrTraineMekanikID { get; set; }
        public string KodeMekanik { get; set; }
        public string NoWorkOrder { get; set; }
        public int ChassisMasterID { get; set; }
        public string KodeChassis { get; set; }
        public string VehicleModelDesc { get; set; }
        public string VehicleColorDesc { get; set; }
        public int KMService { get; set; }
        public int WorkOrderCategoryID { get; set; }
        public string WorkOrderCategoryCode { get; set; }
        public int ServiceTypeID { get; set; }
        public string ServiceTypeCode { get; set; }
        public string ServiceBooking { get; set; }
        public decimal TotalLC { get; set; }
        public decimal TotalSubOrder { get; set; }
        public decimal TotalCat { get; set; }
        public decimal TotalNonCat { get; set; }
        public string DamageCategory { get; set; }
        public string TotalPanel { get; set; }
        public string MethodofPayment { get; set; }
        public string InsuranceName { get; set; }
        public string RemarksSystem { get; set; }
        public string RemarksSpecial { get; set; }
        public string RemarksBM { get; set; }
        public int WOStatus { get; set; }
        public int StatusAktif { get; set; }
        public int ValidateSystemStatus { get; set; }
        public string CustomerOwnerName { get; set; }
        public string CustomerOwnerPhoneNumber { get; set; }
        public string CustomerVisitName { get; set; }
        public string CustomerVisitPhoneNumber { get; set; }
        public int RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime? LastUpdateTime { get; set; }
    }
}
