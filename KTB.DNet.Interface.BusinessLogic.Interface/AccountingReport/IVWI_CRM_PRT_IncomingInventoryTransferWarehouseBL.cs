#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_IncomingInventoryTransferWarehouse interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 27/01/2020 17:07:57
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_PRT_IncomingInventoryTransferWarehouseBL : IBaseInterface<VWI_CRM_PRT_IncomingInventoryTransferWarehouseParameterDto, VWI_CRM_PRT_IncomingInventoryTransferWarehouseFilterDto, VWI_CRM_PRT_IncomingInventoryTransferWarehouseDto>
    {
		ResponseBase<List<VWI_CRM_PRT_IncomingInventoryTransferWarehouseDto>> ReadList(VWI_CRM_PRT_IncomingInventoryTransferWarehouseFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}