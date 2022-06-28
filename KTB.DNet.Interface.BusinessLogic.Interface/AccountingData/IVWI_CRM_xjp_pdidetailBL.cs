#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_pdidetail interface
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
    public interface IVWI_CRM_xjp_pdidetailBL : IBaseInterface<VWI_CRM_xjp_pdidetailParameterDto, VWI_CRM_xjp_pdidetailFilterDto, VWI_CRM_xjp_pdidetailDto>
    {
		ResponseBase<List<VWI_CRM_xjp_pdidetailDto>> ReadList(VWI_CRM_xjp_pdidetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}