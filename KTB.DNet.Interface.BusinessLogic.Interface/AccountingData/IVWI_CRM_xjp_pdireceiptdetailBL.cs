#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_pdireceiptdetail interface
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
    public interface IVWI_CRM_xjp_pdireceiptdetailBL : IBaseInterface<VWI_CRM_xjp_pdireceiptdetailParameterDto, VWI_CRM_xjp_pdireceiptdetailFilterDto, VWI_CRM_xjp_pdireceiptdetailDto>
    {
		ResponseBase<List<VWI_CRM_xjp_pdireceiptdetailDto>> ReadList(VWI_CRM_xjp_pdireceiptdetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}