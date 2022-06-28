#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_OutgoingInventoryTransferOutlet interface
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
    public interface IVWI_CRM_PRT_OutgoingInventoryTransferOutletBL : IBaseInterface<VWI_CRM_PRT_OutgoingInventoryTransferOutletParameterDto, VWI_CRM_PRT_OutgoingInventoryTransferOutletFilterDto, VWI_CRM_PRT_OutgoingInventoryTransferOutletDto>
    {
		ResponseBase<List<VWI_CRM_PRT_OutgoingInventoryTransferOutletDto>> ReadList(VWI_CRM_PRT_OutgoingInventoryTransferOutletFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}