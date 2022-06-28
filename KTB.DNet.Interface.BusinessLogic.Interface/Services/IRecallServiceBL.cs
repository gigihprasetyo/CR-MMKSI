#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : RecallService interface
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
    public interface IRecallServiceBL : IBaseInterface<RecallServiceParameterDto, RecallServiceFilterDto, RecallServiceDto>
    {
        ResponseBase<List<FieldFixListDto>> Get(RecallServiceFilterDto filterDto, int pageSize);
    }
}
