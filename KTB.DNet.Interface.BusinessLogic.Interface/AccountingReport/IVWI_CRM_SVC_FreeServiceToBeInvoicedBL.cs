#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SVC_FreeServiceToBeInvoiced interface
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
    public interface IVWI_CRM_SVC_FreeServiceToBeInvoicedBL : IBaseInterface<VWI_CRM_SVC_FreeServiceToBeInvoicedParameterDto, VWI_CRM_SVC_FreeServiceToBeInvoicedFilterDto, VWI_CRM_SVC_FreeServiceToBeInvoicedDto>
    {
		ResponseBase<List<VWI_CRM_SVC_FreeServiceToBeInvoicedDto>> ReadList(VWI_CRM_SVC_FreeServiceToBeInvoicedFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}