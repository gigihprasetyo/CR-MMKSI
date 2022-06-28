#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_VehicleStock interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2019 5:45:18 PM
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_SLS_VehicleStockBL : IBaseInterface<VWI_CRM_SLS_VehicleStockParameterDto, VWI_CRM_SLS_VehicleStockFilterDto, VWI_CRM_SLS_VehicleStockDto>
    {
		ResponseBase<List<VWI_CRM_SLS_VehicleStockDto>> ReadList(VWI_CRM_SLS_VehicleStockFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}