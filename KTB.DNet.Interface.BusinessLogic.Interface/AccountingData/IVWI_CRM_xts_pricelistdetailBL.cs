#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_pricelistdetail interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 14:02:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_pricelistdetailBL : IBaseInterface<VWI_CRM_xts_pricelistdetailParameterDto, VWI_CRM_xts_pricelistdetailFilterDto, VWI_CRM_xts_pricelistdetailDto>
    {
        ResponseBase<List<VWI_CRM_xts_pricelistdetailDto>> ReadList(VWI_CRM_xts_pricelistdetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}