#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_standardplate interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 08:57:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xjp_standardplateBL : IBaseInterface<VWI_CRM_xjp_standardplateParameterDto, VWI_CRM_xjp_standardplateFilterDto, VWI_CRM_xjp_standardplateDto>
    {
        ResponseBase<List<VWI_CRM_xjp_standardplateDto>> ReadList(VWI_CRM_xjp_standardplateFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}