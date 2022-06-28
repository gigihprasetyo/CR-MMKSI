#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_globalworkorderhistorydetail interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2020 10:42:01
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_globalworkorderhistorydetailBL : IBaseInterface<VWI_CRM_xts_globalworkorderhistorydetailParameterDto, VWI_CRM_xts_globalworkorderhistorydetailFilterDto, VWI_CRM_xts_globalworkorderhistorydetailDto>
    {
		ResponseBase<List<VWI_CRM_xts_globalworkorderhistorydetailDto>> ReadList(VWI_CRM_xts_globalworkorderhistorydetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}