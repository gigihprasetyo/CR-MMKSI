#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_StockSummary interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2019 5:45:18 PM
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_SLS_StockSummaryBL : IBaseInterface<VWI_CRM_SLS_StockSummaryParameterDto, VWI_CRM_SLS_StockSummaryFilterDto, VWI_CRM_SLS_StockSummaryDto>
    {
		ResponseBase<List<VWI_CRM_SLS_StockSummaryDto>> ReadList(VWI_CRM_SLS_StockSummaryFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}