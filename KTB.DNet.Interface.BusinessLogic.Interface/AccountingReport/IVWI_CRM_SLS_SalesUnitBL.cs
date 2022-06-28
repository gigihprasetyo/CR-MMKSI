#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_SalesUnit interface
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
    public interface IVWI_CRM_SLS_SalesUnitBL : IBaseInterface<VWI_CRM_SLS_SalesUnitParameterDto, VWI_CRM_SLS_SalesUnitFilterDto, VWI_CRM_SLS_SalesUnitDto>
    {
		ResponseBase<List<VWI_CRM_SLS_SalesUnitDto>> ReadList(VWI_CRM_SLS_SalesUnitFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}