#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_SparepartSales interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/18/2019 9:51:45 AM
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_PRT_SparepartSalesBL : IBaseInterface<VWI_CRM_PRT_SparepartSalesParameterDto, VWI_CRM_PRT_SparepartSalesFilterDto, VWI_CRM_PRT_SparepartSalesDto>
    {
		ResponseBase<List<VWI_CRM_PRT_SparepartSalesDto>> ReadList(VWI_CRM_PRT_SparepartSalesFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}