#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vendorclass interface
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
    public interface IVWI_CRM_xts_vendorclassBL : IBaseInterface<VWI_CRM_xts_vendorclassParameterDto, VWI_CRM_xts_vendorclassFilterDto, VWI_CRM_xts_vendorclassDto>
    {
		ResponseBase<List<VWI_CRM_xts_vendorclassDto>> ReadList(VWI_CRM_xts_vendorclassFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}