#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_outsourceworkshopconfiguration interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/28/2020 08:50:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_outsourceworkshopconfigurationBL : IBaseInterface<VWI_CRM_xts_outsourceworkshopconfigurationParameterDto, VWI_CRM_xts_outsourceworkshopconfigurationFilterDto, VWI_CRM_xts_outsourceworkshopconfigurationDto>
    {
        ResponseBase<List<VWI_CRM_xts_outsourceworkshopconfigurationDto>> ReadList(VWI_CRM_xts_outsourceworkshopconfigurationFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}