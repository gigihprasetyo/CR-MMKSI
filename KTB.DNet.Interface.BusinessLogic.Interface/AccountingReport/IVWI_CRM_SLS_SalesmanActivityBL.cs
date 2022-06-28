#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_SalesmanActivity interface
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
    public interface IVWI_CRM_SLS_SalesmanActivityBL : IBaseInterface<VWI_CRM_SLS_SalesmanActivityParameterDto, VWI_CRM_SLS_SalesmanActivityFilterDto, VWI_CRM_SLS_SalesmanActivityDto>
    {
		ResponseBase<List<VWI_CRM_SLS_SalesmanActivityDto>> ReadList(VWI_CRM_SLS_SalesmanActivityFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}