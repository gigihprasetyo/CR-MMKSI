#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_vehicletransfer interface
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
    public interface IVWI_CRM_xjp_vehicletransferBL : IBaseInterface<VWI_CRM_xjp_vehicletransferParameterDto, VWI_CRM_xjp_vehicletransferFilterDto, VWI_CRM_xjp_vehicletransferDto>
    {
		ResponseBase<List<VWI_CRM_xjp_vehicletransferDto>> ReadList(VWI_CRM_xjp_vehicletransferFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}