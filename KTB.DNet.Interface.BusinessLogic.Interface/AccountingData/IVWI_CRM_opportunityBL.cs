#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_opportunity interface
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
    public interface IVWI_CRM_opportunityBL : IBaseInterface<VWI_CRM_opportunityParameterDto, VWI_CRM_opportunityFilterDto, VWI_CRM_opportunityDto>
    {
		ResponseBase<List<VWI_CRM_opportunityDto>> ReadList(VWI_CRM_opportunityFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}