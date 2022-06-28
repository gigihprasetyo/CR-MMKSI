#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_servicetemplateparentgroupdetail interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 16:34:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_servicetemplateparentgroupdetailBL : IBaseInterface<VWI_CRM_xts_servicetemplateparentgroupdetailParameterDto, VWI_CRM_xts_servicetemplateparentgroupdetailFilterDto, VWI_CRM_xts_servicetemplateparentgroupdetailDto>
    {
        ResponseBase<List<VWI_CRM_xts_servicetemplateparentgroupdetailDto>> ReadList(VWI_CRM_xts_servicetemplateparentgroupdetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}