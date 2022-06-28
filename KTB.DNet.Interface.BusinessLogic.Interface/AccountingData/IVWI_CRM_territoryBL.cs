#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_territoryBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 09:13:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_territoryBL : IBaseInterface<VWI_CRM_territoryParameterDto, VWI_CRM_territoryFilterDto, VWI_CRM_territoryDto>
    {
        ResponseBase<List<VWI_CRM_territoryDto>> ReadList(VWI_CRM_territoryFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
