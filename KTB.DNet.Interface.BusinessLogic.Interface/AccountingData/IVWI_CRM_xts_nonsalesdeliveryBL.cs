#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_nonsalesdelivery interface
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
    public interface IVWI_CRM_xts_nonsalesdeliveryBL : IBaseInterface<VWI_CRM_xts_nonsalesdeliveryParameterDto, VWI_CRM_xts_nonsalesdeliveryFilterDto, VWI_CRM_xts_nonsalesdeliveryDto>
    {
		ResponseBase<List<VWI_CRM_xts_nonsalesdeliveryDto>> ReadList(VWI_CRM_xts_nonsalesdeliveryFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}