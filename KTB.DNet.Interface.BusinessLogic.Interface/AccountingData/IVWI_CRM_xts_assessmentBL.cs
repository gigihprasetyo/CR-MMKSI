#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_assessment interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 15:22:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_assessmentBL : IBaseInterface<VWI_CRM_xts_assessmentParameterDto, VWI_CRM_xts_assessmentFilterDto, VWI_CRM_xts_assessmentDto>
    {
        ResponseBase<List<VWI_CRM_xts_assessmentDto>> ReadList(VWI_CRM_xts_assessmentFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}