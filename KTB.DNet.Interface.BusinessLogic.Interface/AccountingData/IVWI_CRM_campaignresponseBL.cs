#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_campaignresponse interface
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
    public interface IVWI_CRM_campaignresponseBL : IBaseInterface<VWI_CRM_campaignresponseParameterDto, VWI_CRM_campaignresponseFilterDto, VWI_CRM_campaignresponseDto>
    {
		ResponseBase<List<VWI_CRM_campaignresponseDto>> ReadList(VWI_CRM_campaignresponseFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}