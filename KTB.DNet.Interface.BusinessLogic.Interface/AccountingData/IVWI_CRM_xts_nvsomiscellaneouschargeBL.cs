#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_nvsomiscellaneouscharge interface
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
    public interface IVWI_CRM_xts_nvsomiscellaneouschargeBL : IBaseInterface<VWI_CRM_xts_nvsomiscellaneouschargeParameterDto, VWI_CRM_xts_nvsomiscellaneouschargeFilterDto, VWI_CRM_xts_nvsomiscellaneouschargeDto>
    {
		ResponseBase<List<VWI_CRM_xts_nvsomiscellaneouschargeDto>> ReadList(VWI_CRM_xts_nvsomiscellaneouschargeFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}