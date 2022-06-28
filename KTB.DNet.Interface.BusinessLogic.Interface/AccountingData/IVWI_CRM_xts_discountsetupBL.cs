#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_discountsetup interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 14:25:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_discountsetupBL : IBaseInterface<VWI_CRM_xts_discountsetupParameterDto, VWI_CRM_xts_discountsetupFilterDto, VWI_CRM_xts_discountsetupDto>
    {
        ResponseBase<List<VWI_CRM_xts_discountsetupDto>> ReadList(VWI_CRM_xts_discountsetupFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}