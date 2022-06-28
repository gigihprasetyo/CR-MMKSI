#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_userareaassignment interface
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
    public interface IVWI_CRM_ktb_userareaassignmentBL : IBaseInterface<VWI_CRM_ktb_userareaassignmentParameterDto, VWI_CRM_ktb_userareaassignmentFilterDto, VWI_CRM_ktb_userareaassignmentDto>
    {
		ResponseBase<List<VWI_CRM_ktb_userareaassignmentDto>> ReadList(VWI_CRM_ktb_userareaassignmentFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}