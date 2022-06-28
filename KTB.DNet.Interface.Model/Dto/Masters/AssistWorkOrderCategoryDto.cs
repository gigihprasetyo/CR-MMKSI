#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Area1Dto  class
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
    public class AssistWorkOrderCategoryDto : DtoBase
    {
        public int ID { get; set; }

        public string WorkOrderCategory { get; set; }

        public int WorkOrderTypeID { get; set; }

        public string Description { get; set; }

        public short Status { get; set; }

    }
}
