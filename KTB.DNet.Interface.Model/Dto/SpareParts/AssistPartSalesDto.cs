#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistPartSalesDto  class
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
    public class AssistPartSalesDto : DtoBase
    {
        public int ID { get; set; }
        public int AssistUploadLogID { get; set; }
        public DateTime TglTransaksi { get; set; }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string KodeCustomer { get; set; }
        public int SalesChannelID { get; set; }
        public string SalesChannelCode { get; set; }
        public int TrTraineeSalesSparepartID { get; set; }
        public int SalesmanHeaderID { get; set; }
        public string KodeSalesman { get; set; }
        public string NoWorkOrder { get; set; }
        public int SparepartMasterID { get; set; }
        public string NoParts { get; set; }
        public Double Qty { get; set; }
        public Decimal HargaBeli { get; set; }
        public Decimal HargaJual { get; set; }
        public Boolean IsCampaign { get; set; }
        public string CampaignNo { get; set; }
        public string CampaignDescription { get; set; }
        public int DealerBranchID { get; set; }
        public string DealerBranchCode { get; set; }
        public string RemarksSystem { get; set; }
        public int StatusAktif { get; set; }
        public int ValidateSystemStatus { get; set; }
    }

    public class AssistPartSalesReadDto : DtoBase
    {
        public int ID { get; set; }
        //public int AssistUploadLogID { get; set; }
        public DateTime TglTransaksi { get; set; }
        //public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string KodeCustomer { get; set; }
        //public int SalesChannelID { get; set; }
        public string SalesChannelCode { get; set; }
        public int TrTraineeSalesSparepartID { get; set; }
        //public int SalesmanHeaderID { get; set; }
        public string KodeSalesman { get; set; }
        public string NoWorkOrder { get; set; }
        //public int SparepartMasterID { get; set; }
        public string NoParts { get; set; }
        public Double Qty { get; set; }
        public Decimal HargaBeli { get; set; }
        public Decimal HargaJual { get; set; }
        public Boolean IsCampaign { get; set; }
        public string CampaignNo { get; set; }
        public string CampaignDescription { get; set; }
        //public int DealerBranchID { get; set; }
        public string DealerBranchCode { get; set; }
        public string RemarksSystem { get; set; }
        public int StatusAktif { get; set; }
        public int ValidateSystemStatus { get; set; }
        public DateTime LastUpdatedTime { get; set; }

    }
}

