#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_cashtransactiondetail interface
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
    public interface IVWI_CRM_xts_cashtransactiondetailBL : IBaseInterface<VWI_CRM_xts_cashtransactiondetailParameterDto, VWI_CRM_xts_cashtransactiondetailFilterDto, VWI_CRM_xts_cashtransactiondetailDto>
    {
		ResponseBase<List<VWI_CRM_xts_cashtransactiondetailDto>> ReadList(VWI_CRM_xts_cashtransactiondetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}