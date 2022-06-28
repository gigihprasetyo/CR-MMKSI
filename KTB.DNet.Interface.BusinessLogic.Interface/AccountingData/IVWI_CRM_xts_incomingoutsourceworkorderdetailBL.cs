#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_incomingoutsourceworkorderdetail interface
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
    public interface IVWI_CRM_xts_incomingoutsourceworkorderdetailBL : IBaseInterface<VWI_CRM_xts_incomingoutsourceworkorderdetailParameterDto, VWI_CRM_xts_incomingoutsourceworkorderdetailFilterDto, VWI_CRM_xts_incomingoutsourceworkorderdetailDto>
    {
		ResponseBase<List<VWI_CRM_xts_incomingoutsourceworkorderdetailDto>> ReadList(VWI_CRM_xts_incomingoutsourceworkorderdetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}