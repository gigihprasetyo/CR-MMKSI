#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_businessunitinquiryBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/22/2020 17:58:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_businessunitinquiryBL : IBaseInterface<VWI_CRM_xts_businessunitinquiryParameterDto, VWI_CRM_xts_businessunitinquiryFilterDto, VWI_CRM_xts_businessunitinquiryDto>
    {
        ResponseBase<List<VWI_CRM_xts_businessunitinquiryDto>> ReadList(VWI_CRM_xts_businessunitinquiryFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
