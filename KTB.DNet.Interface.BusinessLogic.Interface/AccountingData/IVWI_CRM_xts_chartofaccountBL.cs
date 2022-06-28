#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_chartofaccount interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/12/2019 3:25:51
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_chartofaccountBL : IBaseInterface<VWI_CRM_xts_chartofaccountParameterDto, VWI_CRM_xts_chartofaccountFilterDto, VWI_CRM_xts_chartofaccountDto>
    {
		ResponseBase<List<VWI_CRM_xts_chartofaccountDto>> ReadList(VWI_CRM_xts_chartofaccountFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}