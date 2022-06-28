#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_BusinessUnit interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/12/2019 3:25:51
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_BusinessUnitBL : IBaseInterface<VWI_CRM_BusinessUnitParameterDto, VWI_CRM_BusinessUnitFilterDto, VWI_CRM_BusinessUnitDto>
    {
		ResponseBase<List<VWI_CRM_BusinessUnitDto>> ReadList(VWI_CRM_BusinessUnitFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}