#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistServiceIncomingBPDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 2021-03-23
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using System.Runtime.Serialization;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class AssistServiceIncomingBPDto : DtoBase
    {
        [IgnoreDataMember]
        public int? ID { get; set; }

        [IgnoreDataMember]
        public int? AssistUploadLogID { get; set; }
        public DateTime? TglPengajuanEstimasi { get; set; }
        public DateTime? TglPersetujuanEstimasi { get; set; }
        public DateTime? TglBukaTransaksi { get; set; }
        public DateTime? WaktuMasuk { get; set; }
        public DateTime? TglJanjiSelesai { get; set; }
        public DateTime? TglTutupTransaksi { get; set; }
        public DateTime? WaktuKeluar { get; set; }

        [IgnoreDataMember]
        public int? DealerID { get; set; }
        public string DealerCode { get; set; }

        [IgnoreDataMember]
        public int? TrTraineMekanikID { get; set; }
        public string KodeMekanik { get; set; }
        public string NoWorkOrder { get; set; }

        [IgnoreDataMember]
        public int? ChassisMasterID { get; set; }
        public string KodeChassis { get; set; }
        public string VehicleModelDesc { get; set; }
        public string VehicleColorDesc { get; set; }
        public int? KMService { get; set; }

        [IgnoreDataMember]
        public int? WorkOrderCategoryID { get; set; }
        public string WorkOrderCategoryCode { get; set; }

        [IgnoreDataMember]
        public int? ServiceTypeID { get; set; }
        public string ServiceTypeCode { get; set; }
        public string ServiceBooking { get; set; }
        public Decimal? TotalLC { get; set; }
        public Decimal? TotalSubOrder { get; set; }
        public Decimal? TotalCat { get; set; }
        public Decimal? TotalNonCat { get; set; }
        public string DamageCategory { get; set; }
        public string TotalPanel { get; set; }
        public string MethodofPayment { get; set; }
        public string InsuranceName { get; set; }

        [IgnoreDataMember]
        public string RemarksSystem { get; set; }

        [IgnoreDataMember]
        public string RemarksSpecial { get; set; }

        [IgnoreDataMember]
        public string RemarksBM { get; set; }

        [IgnoreDataMember]
        public int? WOStatus { get; set; }

        [IgnoreDataMember]
        public int? StatusAktif { get; set; }

        [IgnoreDataMember]
        public int? ValidateSystemStatus { get; set; }
        public string CustomerOwnerName { get; set; }
        public string CustomerOwnerPhoneNumber { get; set; }
        public string CustomerVisitName { get; set; }
        public string CustomerVisitPhoneNumber { get; set; }
    }
}

