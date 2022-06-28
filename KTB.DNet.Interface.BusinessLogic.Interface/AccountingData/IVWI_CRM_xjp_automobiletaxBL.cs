#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_automobiletax interface
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
    public interface IVWI_CRM_xjp_automobiletaxBL : IBaseInterface<VWI_CRM_xjp_automobiletaxParameterDto, VWI_CRM_xjp_automobiletaxFilterDto, VWI_CRM_xjp_automobiletaxDto>
    {
        ResponseBase<List<VWI_CRM_xjp_automobiletaxDto>> ReadList(VWI_CRM_xjp_automobiletaxFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}