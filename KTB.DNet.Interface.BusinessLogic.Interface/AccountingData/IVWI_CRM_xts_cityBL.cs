#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_cityBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/15/2020 09:32:00 AM
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_cityBL : IBaseInterface<VWI_CRM_xts_cityParameterDto, VWI_CRM_xts_cityFilterDto, VWI_CRM_xts_cityDto>
    {
        ResponseBase<List<VWI_CRM_xts_cityDto>> ReadList(VWI_CRM_xts_cityFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
