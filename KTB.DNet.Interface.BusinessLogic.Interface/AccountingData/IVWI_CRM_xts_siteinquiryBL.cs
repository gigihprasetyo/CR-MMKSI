#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_siteinquiryBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/22/2020 17:35:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_siteinquiryBL : IBaseInterface<VWI_CRM_xts_siteinquiryParameterDto, VWI_CRM_xts_siteinquiryFilterDto, VWI_CRM_xts_siteinquiryDto>
    {
        ResponseBase<List<VWI_CRM_xts_siteinquiryDto>> ReadList(VWI_CRM_xts_siteinquiryFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
