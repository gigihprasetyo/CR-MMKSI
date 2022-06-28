#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_accessorieskitgroup interface
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
    public interface IVWI_CRM_xts_accessorieskitgroupBL : IBaseInterface<VWI_CRM_xts_accessorieskitgroupParameterDto, VWI_CRM_xts_accessorieskitgroupFilterDto, VWI_CRM_xts_accessorieskitgroupDto>
    {
        ResponseBase<List<VWI_CRM_xts_accessorieskitgroupDto>> ReadList(VWI_CRM_xts_accessorieskitgroupFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}