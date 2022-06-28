#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_servicetemplateproductnongenuine interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 16:08:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_ktb_servicetemplateproductnongenuineBL : IBaseInterface<VWI_CRM_ktb_servicetemplateproductnongenuineParameterDto, VWI_CRM_ktb_servicetemplateproductnongenuineFilterDto, VWI_CRM_ktb_servicetemplateproductnongenuineDto>
    {
        ResponseBase<List<VWI_CRM_ktb_servicetemplateproductnongenuineDto>> ReadList(VWI_CRM_ktb_servicetemplateproductnongenuineFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}