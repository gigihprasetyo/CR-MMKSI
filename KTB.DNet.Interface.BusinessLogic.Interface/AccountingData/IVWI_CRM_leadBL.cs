#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_lead interface
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
    public interface IVWI_CRM_leadBL : IBaseInterface<VWI_CRM_leadParameterDto, VWI_CRM_leadFilterDto, VWI_CRM_leadDto>
    {
		ResponseBase<List<VWI_CRM_leadDto>> ReadList(VWI_CRM_leadFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}