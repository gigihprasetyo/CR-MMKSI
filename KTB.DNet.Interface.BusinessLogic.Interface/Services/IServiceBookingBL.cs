

#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : IServiceBookingBL interface
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
    public interface IServiceBookingBL : IBaseInterface<ServiceBookingParameterDto, ServiceBookingFilterDto, ServiceBookingDto>
    {
        ResponseBase<ServiceBookingDto> Update(ServiceBookingUpdateParameterDto objUpdate);
        ResponseBase<VWI_ServiceCostEstimationDto> EstimationCost(VWI_ServiceCostEstimationParameterDto objEstimationCost);
        Task<ResponseBase<List<DealerSuggestionServiceDto>>> DealerSuggestion(DealerSuggestionServiceParameterDto objDealerSuggestionService);
        ResponseBase<ServiceBookingRealtimeDto> RealtimeCreate(ServiceBookingRealtimeParameterDto objCreate);
        ResponseBase<ServiceBookingRealtimeDto> RealtimeUpdate(ServiceBookingRealtimeParameterDto objUpdate);
        ResponseBase<List<ServiceBookingRealtimeReadDto>> RealtimeRead(ServiceBookingFilterDto filterDto, int pageSize);
        ResponseBase<List<GetServiceTypeDto>> GetServiceType(GetServiceTypeParameterDto objServiceType);
        ResponseBase<List<ServiceRecommendationDto>> ServiceRecommendation(ServiceRecommendationParameterDto objServiceRecommendation);
        ResponseBase<List<ServiceBookingRealtimeReadDto>> RealtimeAll(ServiceBookingFilterDto filterDto, int pageSize);
    }
}

