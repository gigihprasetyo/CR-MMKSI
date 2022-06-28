#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_warehouseinquiryBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/18/2020 13:19:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_warehouseinquiryBL : IBaseInterface<VWI_CRM_xts_warehouseinquiryParameterDto, VWI_CRM_xts_warehouseinquiryFilterDto, VWI_CRM_xts_warehouseinquiryDto>
    {
        ResponseBase<List<VWI_CRM_xts_warehouseinquiryDto>> ReadList(VWI_CRM_xts_warehouseinquiryFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
