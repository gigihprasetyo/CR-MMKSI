#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_campaignactivity interface
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
    public interface IVWI_CRM_campaignactivityBL : IBaseInterface<VWI_CRM_campaignactivityParameterDto, VWI_CRM_campaignactivityFilterDto, VWI_CRM_campaignactivityDto>
    {
		ResponseBase<List<VWI_CRM_campaignactivityDto>> ReadList(VWI_CRM_campaignactivityFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}