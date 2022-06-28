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
    public class AssistServiceIncomingDto : DtoBase
    {
        [IgnoreDataMember]
        public int ID { get; set; }
        [IgnoreDataMember]
        public int AssistUploadLogID { get; set; }
        public Object TglBukaTransaksi { get; set; }
        public Object WaktuMasuk { get; set; }
        public Object TglTutupTransaksi { get; set; }
        public Object WaktuKeluar { get; set; }
        [IgnoreDataMember]
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public int DealerBranchID { get; set; }
        public string DealerBranchCode { get; set; }
        [IgnoreDataMember]
        public int TrTraineMekanikID { get; set; }
        public string KodeMekanik { get; set; }
        public string NoWorkOrder { get; set; }
        [IgnoreDataMember]
        public int ChassisMasterID { get; set; }
        public string KodeChassis { get; set; }
        [IgnoreDataMember]
        public int WorkOrderCategoryID { get; set; }
        public string WorkOrderCategoryCode { get; set; }
        public int KMService { get; set; }
        [IgnoreDataMember]
        public int ServicePlaceID { get; set; }
        public string ServicePlaceCode { get; set; }
        [IgnoreDataMember]
        public int ServiceTypeID { get; set; }
        public string ServiceTypeCode { get; set; }
        public Decimal TotalLC { get; set; }
        public string MetodePembayaran { get; set; }
        public string Model { get; set; }
        public string Transmition { get; set; }
        public string DriveSystem { get; set; }
        [IgnoreDataMember]
        public string RemarksSystem { get; set; }
        [IgnoreDataMember]
        public string RemarksSpecial { get; set; }
        [IgnoreDataMember]
        public string RemarksBM { get; set; }
        [IgnoreDataMember]
        public int StatusAktif { get; set; }
        [IgnoreDataMember]
        public int WOStatus { get; set; }
        [IgnoreDataMember]
        public int ValidateSystemStatus { get; set; }

        // 20200702 : CR Global Service Reminder - Restia,Benny
        public string CustomerOwnerName { get; set; }
        public string CustomerOwnerPhoneNumber { get; set; }
        public string CustomerVisitName { get; set; }
        public string CustomerVisitPhoneNumber { get; set; }
        // end CR Global Service Reminder
    }
}

