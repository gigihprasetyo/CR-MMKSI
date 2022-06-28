#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDODetailDto  class
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
    public class SparePartDODetailDto : DtoBase
    {
        #region Public Properties

        public int ID { get; set; }

        public int ItemNoDO { get; set; }

        public int ItemNoSO { get; set; }

        public int Qty { get; set; }

        public SparePartDODto SparePartDO { get; set; }

        public SparePartPOEstimateDto SparePartPOEstimate { get; set; }

        public SparePartMasterDto SparePartMaster { get; set; }

        #endregion

        #region Custom Properties

        #endregion
    }

    public class DeliveryOrderDetailResponseDto : DtoBase
    {
        public string SONumber { get; set; }

        public Decimal Discount { get; set; }

        public string PartNumber { get; set; }

        public string PartName { get; set; }

        public int Qty { get; set; }

        public decimal Tax { get; set; }

        public Decimal RetailPrice { get; set; }
    }
}
