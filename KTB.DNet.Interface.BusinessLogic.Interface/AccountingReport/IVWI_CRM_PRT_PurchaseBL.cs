#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_Purchase interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/21/2019 2:25:32 PM
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_PRT_PurchaseBL : IBaseInterface<VWI_CRM_PRT_PurchaseParameterDto, VWI_CRM_PRT_PurchaseFilterDto, VWI_CRM_PRT_PurchaseDto>
    {
		ResponseBase<List<VWI_CRM_PRT_PurchaseDto>> ReadList(VWI_CRM_PRT_PurchaseFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}