#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_termofpaymentBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/29/2020 08:36:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_termofpaymentBL : IBaseInterface<VWI_CRM_xts_termofpaymentParameterDto, VWI_CRM_xts_termofpaymentFilterDto, VWI_CRM_xts_termofpaymentDto>
    {
        ResponseBase<List<VWI_CRM_xts_termofpaymentDto>> ReadList(VWI_CRM_xts_termofpaymentFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
