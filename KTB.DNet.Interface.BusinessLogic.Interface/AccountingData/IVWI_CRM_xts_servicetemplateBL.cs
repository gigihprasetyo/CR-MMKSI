#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_servicetemplate interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/12/2019 3:25:51
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_servicetemplateBL : IBaseInterface<VWI_CRM_xts_servicetemplateParameterDto, VWI_CRM_xts_servicetemplateFilterDto, VWI_CRM_xts_servicetemplateDto>
    {
		ResponseBase<List<VWI_CRM_xts_servicetemplateDto>> ReadList(VWI_CRM_xts_servicetemplateFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}