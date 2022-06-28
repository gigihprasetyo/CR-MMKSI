#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_warehouse interface
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
    public interface IVWI_CRM_xts_warehouseBL : IBaseInterface<VWI_CRM_xts_warehouseParameterDto, VWI_CRM_xts_warehouseFilterDto, VWI_CRM_xts_warehouseDto>
    {
		ResponseBase<List<VWI_CRM_xts_warehouseDto>> ReadList(VWI_CRM_xts_warehouseFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}