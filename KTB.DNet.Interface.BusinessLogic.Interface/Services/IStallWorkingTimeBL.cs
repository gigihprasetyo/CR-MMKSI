
#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StallWorkingTime interface
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
    public interface IStallWorkingTimeBL : IBaseInterface<StallWorkingTimeParameterDto, StallWorkingTimeFilterDto, StallWorkingTimeDto>
    {
        ResponseBase<StallWorkingTimeDto> Update(StallWorkingTimeUpdateParameterDto objUpdate);

        ResponseBase<List<StallWorkingTimeDto>> BulkCreate(List<StallWorkingTimeCreateListParameterDto> lstObjCreate);
        ResponseBase<List<StallWorkingTimeDto>> BulkUpdate(List<StallWorkingTimeUpdateListParameterDto> lstObjUpdate);
    }
}

