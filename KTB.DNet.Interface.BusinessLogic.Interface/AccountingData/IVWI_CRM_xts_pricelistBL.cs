#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_pricelist interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 27/01/2021 08:45:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_pricelistBL : IBaseInterface<VWI_CRM_xts_pricelistParameterDto, VWI_CRM_xts_pricelistFilterDto, VWI_CRM_xts_pricelistDto>
    {
        ResponseBase<List<VWI_CRM_xts_pricelistDto>> ReadList(VWI_CRM_xts_pricelistFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}