#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistSalesChannel interface
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
    public interface IAssistSalesChannelBL : IBaseInterface<AssistSalesChannelParameterDto, AssistSalesChannelFilterDto, AssistSalesChannelDto>
    {
        /// <summary>
        /// Filter Object 
        /// </summary>
        /// <param name="filterDto"></param>
        /// <returns></returns>        
        ResponseBase<List<AssistSalesChannelDto>> ReadMaster(AssistSalesChannelFilterDto filterDto, int pageSize);
    }
}
