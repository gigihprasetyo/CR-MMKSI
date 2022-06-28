#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_OutgoingInventoryTransferWarehouse interface
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
    public interface IVWI_CRM_PRT_OutgoingInventoryTransferWarehouseBL : IBaseInterface<VWI_CRM_PRT_OutgoingInventoryTransferWarehouseParameterDto, VWI_CRM_PRT_OutgoingInventoryTransferWarehouseFilterDto, VWI_CRM_PRT_OutgoingInventoryTransferWarehouseDto>
    {
		ResponseBase<List<VWI_CRM_PRT_OutgoingInventoryTransferWarehouseDto>> ReadList(VWI_CRM_PRT_OutgoingInventoryTransferWarehouseFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}