#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_recommendedproduct interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 16:55:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_recommendedproductBL : IBaseInterface<VWI_CRM_xts_recommendedproductParameterDto, VWI_CRM_xts_recommendedproductFilterDto, VWI_CRM_xts_recommendedproductDto>
    {
        ResponseBase<List<VWI_CRM_xts_recommendedproductDto>> ReadList(VWI_CRM_xts_recommendedproductFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}