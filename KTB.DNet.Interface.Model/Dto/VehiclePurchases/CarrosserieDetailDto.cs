#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CarrosserieDetailDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion


namespace KTB.DNet.Interface.Model
{
    public class CarrosserieDetailDto : DtoBase
    {
        public int ID { get; set; }
        public short PDIStateCode { get; set; }
        public short PDIStatusCode { get; set; }
        public string AccessorriesDescription { get; set; }
        public string AccessorriesName { get; set; }
        public string BUCode { get; set; }
        public string BUName { get; set; }
        public string KITName { get; set; }
        public string PBUCode { get; set; }
        public string PBUName { get; set; }
        public string PDIDetailName { get; set; }
        public string PDIReceiptDetailNo { get; set; }
        public string PDIReceiptName { get; set; }
        public double ReceiveQuantity { get; set; }

        public CarrosserieHeaderDto CarrosserieHeader { get; set; }
    }
}

