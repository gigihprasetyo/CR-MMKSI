#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_dimension4BL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 09:53:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_dimension4BL : IBaseInterface<VWI_CRM_xts_dimension4ParameterDto, VWI_CRM_xts_dimension4FilterDto, VWI_CRM_xts_dimension4Dto>
    {
        ResponseBase<List<VWI_CRM_xts_dimension4Dto>> ReadList(VWI_CRM_xts_dimension4FilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
