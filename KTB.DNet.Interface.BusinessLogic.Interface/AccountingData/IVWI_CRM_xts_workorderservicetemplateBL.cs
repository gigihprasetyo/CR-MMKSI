#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_workorderservicetemplate interface
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
    public interface IVWI_CRM_xts_workorderservicetemplateBL : IBaseInterface<VWI_CRM_xts_workorderservicetemplateParameterDto, VWI_CRM_xts_workorderservicetemplateFilterDto, VWI_CRM_xts_workorderservicetemplateDto>
    {
		ResponseBase<List<VWI_CRM_xts_workorderservicetemplateDto>> ReadList(VWI_CRM_xts_workorderservicetemplateFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}