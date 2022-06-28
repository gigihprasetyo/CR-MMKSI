#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_servicetemplatedetail interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 15:45:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_servicetemplatedetailBL : IBaseInterface<VWI_CRM_xts_servicetemplatedetailParameterDto, VWI_CRM_xts_servicetemplatedetailFilterDto, VWI_CRM_xts_servicetemplatedetailDto>
    {
        ResponseBase<List<VWI_CRM_xts_servicetemplatedetailDto>> ReadList(VWI_CRM_xts_servicetemplatedetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}