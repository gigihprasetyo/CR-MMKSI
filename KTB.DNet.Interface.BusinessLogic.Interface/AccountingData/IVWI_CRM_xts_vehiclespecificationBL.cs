#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vehiclespecification interface
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
    public interface IVWI_CRM_xts_vehiclespecificationBL : IBaseInterface<VWI_CRM_xts_vehiclespecificationParameterDto, VWI_CRM_xts_vehiclespecificationFilterDto, VWI_CRM_xts_vehiclespecificationDto>
    {
		ResponseBase<List<VWI_CRM_xts_vehiclespecificationDto>> ReadList(VWI_CRM_xts_vehiclespecificationFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}