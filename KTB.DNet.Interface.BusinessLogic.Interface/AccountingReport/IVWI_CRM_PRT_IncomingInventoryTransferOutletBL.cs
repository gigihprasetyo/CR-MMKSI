#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_IncomingInventoryTransferOutlet interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/01/2020 10:25:21
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_PRT_IncomingInventoryTransferOutletBL : IBaseInterface<VWI_CRM_PRT_IncomingInventoryTransferOutletParameterDto, VWI_CRM_PRT_IncomingInventoryTransferOutletFilterDto, VWI_CRM_PRT_IncomingInventoryTransferOutletDto>
    {
		ResponseBase<List<VWI_CRM_PRT_IncomingInventoryTransferOutletDto>> ReadList(VWI_CRM_PRT_IncomingInventoryTransferOutletFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}