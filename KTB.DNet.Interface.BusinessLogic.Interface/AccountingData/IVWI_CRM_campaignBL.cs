#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_campaign interface
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
    public interface IVWI_CRM_campaignBL : IBaseInterface<VWI_CRM_campaignParameterDto, VWI_CRM_campaignFilterDto, VWI_CRM_campaignDto>
    {
		ResponseBase<List<VWI_CRM_campaignDto>> ReadList(VWI_CRM_campaignFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}