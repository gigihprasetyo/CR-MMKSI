#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartForecast interface
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
    public interface ISparePartForecastBL : IBaseInterface<SparePartForecastParameterDto, SparePartForecastFilterDto, SparePartForecastDto>
    {
        ResponseBase<List<SparePartForecastStockManagementDto>> ReadStockManagement(SparePartForecastStockManagementFilterDto filterDto, int pageSize);
        ResponseBase<List<SparePartForecastRejectDto>> ReadReject(SparePartForecastRejectFilterDto filterDto, int pageSize);
        ResponseBase<List<SparePartForecastPOEstimateDto>> ReadPOEstimate(SparePartForecastPOEstimateFilterDto filterDto, int pageSize);
        ResponseBase<List<SparePartForecastGoodReceiptDto>> ReadGoodReceipt(SparePartForecastGoodReceiptFilterDto filterDto, int pageSize);
        ResponseBase<SparePartForecastValidatorDto> Validator(SparePartForecastParameterDto param);
    }
}
