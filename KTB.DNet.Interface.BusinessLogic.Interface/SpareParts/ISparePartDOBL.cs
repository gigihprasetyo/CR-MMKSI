#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDO interface
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
    public interface ISparePartDOBL : IBaseInterface<SparePartDOParameterDto, SparePartDOFilterDto, SparePartDODto>
    {
        /// <summary>
        /// Filter Object 
        /// </summary>
        /// <param name="filterDto"></param>
        /// <returns></returns>
        ResponseBase<List<DeliveryOrderBillingResponseDto>> ReadDeliveryOrderBilling(SparePartDOFilterDto filterDto, int pageSize);

        ResponseBase<List<DeliveryOrderBillingResponseDto>> ReadDeliveryOrderBilling1(SparePartDOFilterDto filterDto, int pageSize);

        ResponseBase<List<DeliveryOrderBillingResponseDto>> ReadDeliveryOrderBilling2(SparePartDOFilterDto filterDto, int pageSize);

        ResponseBase<List<DeliveryOrderBillingResponseDto>> ReadDeliveryOrderBillingWithCriteria(SparePartDOFilterDto filterDto, int pageSize);

    }
}
