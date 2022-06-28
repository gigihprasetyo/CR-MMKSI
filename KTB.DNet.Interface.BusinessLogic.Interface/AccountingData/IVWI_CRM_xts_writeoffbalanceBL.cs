#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_writeoffbalance interface
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
    public interface IVWI_CRM_xts_writeoffbalanceBL : IBaseInterface<VWI_CRM_xts_writeoffbalanceParameterDto, VWI_CRM_xts_writeoffbalanceFilterDto, VWI_CRM_xts_writeoffbalanceDto>
    {
		ResponseBase<List<VWI_CRM_xts_writeoffbalanceDto>> ReadList(VWI_CRM_xts_writeoffbalanceFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}