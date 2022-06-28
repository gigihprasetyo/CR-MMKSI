#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_landedcostBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 09:38:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_landedcostBL : IBaseInterface<VWI_CRM_xts_landedcostParameterDto, VWI_CRM_xts_landedcostFilterDto, VWI_CRM_xts_landedcostDto>
    {
        ResponseBase<List<VWI_CRM_xts_landedcostDto>> ReadList(VWI_CRM_xts_landedcostFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
