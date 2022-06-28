#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_reasonBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 10:14:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_reasonBL : IBaseInterface<VWI_CRM_xts_reasonParameterDto, VWI_CRM_xts_reasonFilterDto, VWI_CRM_xts_reasonDto>
    {
        ResponseBase<List<VWI_CRM_xts_reasonDto>> ReadList(VWI_CRM_xts_reasonFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
