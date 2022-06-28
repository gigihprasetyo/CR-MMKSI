#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_servicemiscellaneouscharge interface
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
    public interface IVWI_CRM_xts_servicemiscellaneouschargeBL : IBaseInterface<VWI_CRM_xts_servicemiscellaneouschargeParameterDto, VWI_CRM_xts_servicemiscellaneouschargeFilterDto, VWI_CRM_xts_servicemiscellaneouschargeDto>
    {
		ResponseBase<List<VWI_CRM_xts_servicemiscellaneouschargeDto>> ReadList(VWI_CRM_xts_servicemiscellaneouschargeFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}