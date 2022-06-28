#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_pdireceipt interface
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
    public interface IVWI_CRM_xjp_pdireceiptBL : IBaseInterface<VWI_CRM_xjp_pdireceiptParameterDto, VWI_CRM_xjp_pdireceiptFilterDto, VWI_CRM_xjp_pdireceiptDto>
    {
		ResponseBase<List<VWI_CRM_xjp_pdireceiptDto>> ReadList(VWI_CRM_xjp_pdireceiptFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}