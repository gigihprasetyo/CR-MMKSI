#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_nvsoaccessories interface
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
    public interface IVWI_CRM_xts_nvsoaccessoriesBL : IBaseInterface<VWI_CRM_xts_nvsoaccessoriesParameterDto, VWI_CRM_xts_nvsoaccessoriesFilterDto, VWI_CRM_xts_nvsoaccessoriesDto>
    {
		ResponseBase<List<VWI_CRM_xts_nvsoaccessoriesDto>> ReadList(VWI_CRM_xts_nvsoaccessoriesFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}