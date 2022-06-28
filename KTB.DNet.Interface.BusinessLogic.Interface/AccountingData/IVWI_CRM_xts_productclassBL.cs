#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_productclass interface
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
    public interface IVWI_CRM_xts_productclassBL : IBaseInterface<VWI_CRM_xts_productclassParameterDto, VWI_CRM_xts_productclassFilterDto, VWI_CRM_xts_productclassDto>
    {
		ResponseBase<List<VWI_CRM_xts_productclassDto>> ReadList(VWI_CRM_xts_productclassFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}