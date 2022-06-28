#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_SparepartPurchase interface
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
    public interface IVWI_CRM_PRT_SparepartPurchaseBL : IBaseInterface<VWI_CRM_PRT_SparepartPurchaseParameterDto, VWI_CRM_PRT_SparepartPurchaseFilterDto, VWI_CRM_PRT_SparepartPurchaseDto>
    {
		ResponseBase<List<VWI_CRM_PRT_SparepartPurchaseDto>> ReadList(VWI_CRM_PRT_SparepartPurchaseFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}