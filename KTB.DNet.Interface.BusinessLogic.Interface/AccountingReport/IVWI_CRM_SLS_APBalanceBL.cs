#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_APBalance interface
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
    public interface IVWI_CRM_SLS_APBalanceBL : IBaseInterface<VWI_CRM_SLS_APBalanceParameterDto, VWI_CRM_SLS_APBalanceFilterDto, VWI_CRM_SLS_APBalanceDto>
    {
		ResponseBase<List<VWI_CRM_SLS_APBalanceDto>> ReadList(VWI_CRM_SLS_APBalanceFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}