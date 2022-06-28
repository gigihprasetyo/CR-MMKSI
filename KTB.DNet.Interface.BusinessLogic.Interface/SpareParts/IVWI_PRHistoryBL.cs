#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_PRHistory interface
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
    public interface IVWI_PRHistoryBL : IBaseViewInterface<VWI_PRHistorySOFilterDto, VWI_PRHistorySODto>
    {
        ResponseBase<List<VWI_PRHistorySODto>> GetPRHistorySO(VWI_PRHistorySOFilterDto filterDto, int pageSize);
        ResponseBase<List<VWI_PRHistoryDODto>> GetPRHistoryDO(VWI_PRHistoryDOFilterDto filterDto, int pageSize);
    }
}
