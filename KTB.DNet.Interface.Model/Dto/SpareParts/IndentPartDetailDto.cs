#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : IndentPartDetailDto  class
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
    public class IndentPartDetailDto : DtoBase
    {
        #region Public Properties

        public int ID { get; set; }

        public int TotalForecast { get; set; }

        public int Qty { get; set; }

        public string Description { get; set; }

        public int AllocationQty { get; set; }

        public byte IsCompletedAllocation { get; set; }

        public decimal Price { get; set; }

        public SparePartMasterDto SparePartMaster { get; set; }

        public IndentPartHeaderDto IndentPartHeader { get; set; }

        #endregion

        #region Custom Properties

        #endregion
    }
}
