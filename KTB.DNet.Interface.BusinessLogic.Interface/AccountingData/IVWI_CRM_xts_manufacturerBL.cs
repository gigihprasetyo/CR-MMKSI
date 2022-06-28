#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_manufacturerBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2020 16:18:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_manufacturerBL : IBaseInterface<VWI_CRM_xts_manufacturerParameterDto, VWI_CRM_xts_manufacturerFilterDto, VWI_CRM_xts_manufacturerDto>
    {
        ResponseBase<List<VWI_CRM_xts_manufacturerDto>> ReadList(VWI_CRM_xts_manufacturerFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
