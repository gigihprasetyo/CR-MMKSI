#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistPartSales interface
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
    public interface IAssistPartSalesBL : IBaseInterface<AssistPartSalesParameterDto, AssistPartSalesFilterDto, AssistPartSalesDto>
    {
        ResponseBase<List<AssistPartSalesDto>> Create(List<AssistPartSalesParameterDto> objCreate);
        ResponseBase<List<AssistPartSalesReadDto>> ReadData(AssistPartSalesFilterDto filterDto, int pageSize);
        Task<ResponseBase<List<AssistPartSalesDto>>> BulkCreateAsync(List<AssistPartSalesParameterDto> lstObjCreate);
        ResponseBase<List<AssistPartSalesDto>> BulkCreate(List<AssistPartSalesParameterDto> lstObjCreate);
    }
}
