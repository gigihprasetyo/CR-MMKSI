#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_commonbusinessunit interface
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
    public interface IVWI_CRM_xts_commonbusinessunitBL : IBaseInterface<VWI_CRM_xts_commonbusinessunitParameterDto, VWI_CRM_xts_commonbusinessunitFilterDto, VWI_CRM_xts_commonbusinessunitDto>
    {
		ResponseBase<List<VWI_CRM_xts_commonbusinessunitDto>> ReadList(VWI_CRM_xts_commonbusinessunitFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}