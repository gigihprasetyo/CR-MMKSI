#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_weighttax interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 15:44:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xjp_weighttaxBL : IBaseInterface<VWI_CRM_xjp_weighttaxParameterDto, VWI_CRM_xjp_weighttaxFilterDto, VWI_CRM_xjp_weighttaxDto>
    {
        ResponseBase<List<VWI_CRM_xjp_weighttaxDto>> ReadList(VWI_CRM_xjp_weighttaxFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}