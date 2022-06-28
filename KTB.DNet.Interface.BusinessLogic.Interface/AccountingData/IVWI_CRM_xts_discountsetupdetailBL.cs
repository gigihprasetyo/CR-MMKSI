#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_discountsetupdetail interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 14:55:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_discountsetupdetailBL : IBaseInterface<VWI_CRM_xts_discountsetupdetailParameterDto, VWI_CRM_xts_discountsetupdetailFilterDto, VWI_CRM_xts_discountsetupdetailDto>
    {
        ResponseBase<List<VWI_CRM_xts_discountsetupdetailDto>> ReadList(VWI_CRM_xts_discountsetupdetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}