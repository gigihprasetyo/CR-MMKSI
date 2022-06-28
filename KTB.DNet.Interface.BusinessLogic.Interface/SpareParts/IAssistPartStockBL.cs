#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistPartStock interface
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
using System.Threading.Tasks;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IAssistPartStockBL : IBaseInterface<AssistPartStockParameterDto, AssistPartStockFilterDto, AssistPartStockDto>
    {
        ResponseBase<List<AssistPartStockDto>> Create(List<AssistPartStockParameterDto> objCreate);
        ResponseBase<List<AssistPartStockDto>> Update(List<AssistPartStockParameterDto> objCreate);
        Task<ResponseBase<List<AssistPartStockDto>>> BulkCreateAsync(List<AssistPartStockParameterDto> lstObjCreate);
        ResponseBase<List<AssistPartStockDto>> BulkCreate(List<AssistPartStockParameterDto> lstObjCreate);
    }
}
