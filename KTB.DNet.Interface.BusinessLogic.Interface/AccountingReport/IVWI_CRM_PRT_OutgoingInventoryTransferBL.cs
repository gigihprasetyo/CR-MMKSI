#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_OutgoingInventoryTransfer interface
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
    public interface IVWI_CRM_PRT_OutgoingInventoryTransferBL : IBaseInterface<VWI_CRM_PRT_OutgoingInventoryTransferParameterDto, VWI_CRM_PRT_OutgoingInventoryTransferFilterDto, VWI_CRM_PRT_OutgoingInventoryTransferDto>
    {
		ResponseBase<List<VWI_CRM_PRT_OutgoingInventoryTransferDto>> ReadList(VWI_CRM_PRT_OutgoingInventoryTransferFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}