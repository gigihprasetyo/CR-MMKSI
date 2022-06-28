#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_incomingoutsourceworkorder interface
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
    public interface IVWI_CRM_xts_incomingoutsourceworkorderBL : IBaseInterface<VWI_CRM_xts_incomingoutsourceworkorderParameterDto, VWI_CRM_xts_incomingoutsourceworkorderFilterDto, VWI_CRM_xts_incomingoutsourceworkorderDto>
    {
		ResponseBase<List<VWI_CRM_xts_incomingoutsourceworkorderDto>> ReadList(VWI_CRM_xts_incomingoutsourceworkorderFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}