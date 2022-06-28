#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_IncomingInventoryTransfer interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2019 4:57:58 PM
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_PRT_IncomingInventoryTransferBL : IBaseInterface<VWI_CRM_PRT_IncomingInventoryTransferParameterDto, VWI_CRM_PRT_IncomingInventoryTransferFilterDto, VWI_CRM_PRT_IncomingInventoryTransferDto>
    {
		ResponseBase<List<VWI_CRM_PRT_IncomingInventoryTransferDto>> ReadList(VWI_CRM_PRT_IncomingInventoryTransferFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}