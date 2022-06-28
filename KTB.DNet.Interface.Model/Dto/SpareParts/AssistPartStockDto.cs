#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistPartStockDto  class
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
    public class AssistPartStockDto : DtoBase
    {
        public int ID { get; set; }
        public int AssistUploadLogID { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        //public int DealerID { get; set; }
        public string DealerCode { get; set; }
        //public int DealerBranchID { get; set; }
        public string DealerBranchCode { get; set; }
        public int SparepartMasterID { get; set; }
        public string NoParts { get; set; }
        public Double JumlahStokAwal { get; set; }
        public Double JumlahDatang { get; set; }
        public Decimal HargaBeli { get; set; }
        public string RemarksSystem { get; set; }
        public int StatusAktif { get; set; }
        public int ValidateSystemStatus { get; set; }
    }
}