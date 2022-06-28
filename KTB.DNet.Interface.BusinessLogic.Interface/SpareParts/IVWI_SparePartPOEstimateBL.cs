#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_SparePartPOEstimate interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_SparePartPOEstimateBL : IBaseViewInterface<SparePartPOEstimateFilterDto, SparePartPOEstimateDto>
    {
        ResponseBase<List<SparePartPOEstimateDto>> Read1(SparePartPOEstimateFilterDto filterDto, int pageSize);

        ResponseBase<List<SparePartPOEstimateDto>> ReadWithCriteria(SparePartPOEstimateFilterDto filterDto, int pageSize);
    }
}
