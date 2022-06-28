#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SVC_DailyReport interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2020 10:45:39
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_SVC_DailyReportBL : IBaseInterface<VWI_CRM_SVC_DailyReportParameterDto, VWI_CRM_SVC_DailyReportFilterDto, VWI_CRM_SVC_DailyReportDto>
    {
		ResponseBase<List<VWI_CRM_SVC_DailyReportDto>> ReadList(VWI_CRM_SVC_DailyReportFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}