#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_servicetemplateactivity interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 15:21:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_servicetemplateactivityBL : IBaseInterface<VWI_CRM_xts_servicetemplateactivityParameterDto, VWI_CRM_xts_servicetemplateactivityFilterDto, VWI_CRM_xts_servicetemplateactivityDto>
    {
        ResponseBase<List<VWI_CRM_xts_servicetemplateactivityDto>> ReadList(VWI_CRM_xts_servicetemplateactivityFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}