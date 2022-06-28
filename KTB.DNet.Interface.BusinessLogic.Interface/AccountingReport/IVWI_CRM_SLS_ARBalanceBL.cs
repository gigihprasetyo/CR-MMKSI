#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_ARBalance interface
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
    public interface IVWI_CRM_SLS_ARBalanceBL : IBaseInterface<VWI_CRM_SLS_ARBalanceParameterDto, VWI_CRM_SLS_ARBalanceFilterDto, VWI_CRM_SLS_ARBalanceDto>
    {
		ResponseBase<List<VWI_CRM_SLS_ARBalanceDto>> ReadList(VWI_CRM_SLS_ARBalanceFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}