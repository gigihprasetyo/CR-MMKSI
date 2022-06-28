#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_PurchaseReturn interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/21/2019 1:53:33 PM
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_PRT_PurchaseReturnBL : IBaseInterface<VWI_CRM_PRT_PurchaseReturnParameterDto, VWI_CRM_PRT_PurchaseReturnFilterDto, VWI_CRM_PRT_PurchaseReturnDto>
    {
		ResponseBase<List<VWI_CRM_PRT_PurchaseReturnDto>> ReadList(VWI_CRM_PRT_PurchaseReturnFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}