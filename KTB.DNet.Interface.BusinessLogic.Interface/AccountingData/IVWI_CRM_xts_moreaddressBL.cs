#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_moreaddress interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 09:36:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_moreaddressBL : IBaseInterface<VWI_CRM_xts_moreaddressParameterDto, VWI_CRM_xts_moreaddressFilterDto, VWI_CRM_xts_moreaddressDto>
    {
        ResponseBase<List<VWI_CRM_xts_moreaddressDto>> ReadList(VWI_CRM_xts_moreaddressFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}