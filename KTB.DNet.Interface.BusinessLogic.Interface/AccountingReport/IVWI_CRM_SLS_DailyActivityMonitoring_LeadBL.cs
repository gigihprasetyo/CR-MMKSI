#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_DailyActivityMonitoring_Lead interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/21/2019 1:53:33 PM
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_SLS_DailyActivityMonitoring_LeadBL : IBaseInterface<VWI_CRM_SLS_DailyActivityMonitoring_LeadParameterDto, VWI_CRM_SLS_DailyActivityMonitoring_LeadFilterDto, VWI_CRM_SLS_DailyActivityMonitoring_LeadDto>
    {
		ResponseBase<List<VWI_CRM_SLS_DailyActivityMonitoring_LeadDto>> ReadList(VWI_CRM_SLS_DailyActivityMonitoring_LeadFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}