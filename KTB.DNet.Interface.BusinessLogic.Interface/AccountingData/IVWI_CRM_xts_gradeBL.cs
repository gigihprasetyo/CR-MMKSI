#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_grade interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 16:17:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_gradeBL : IBaseInterface<VWI_CRM_xts_gradeParameterDto, VWI_CRM_xts_gradeFilterDto, VWI_CRM_xts_gradeDto>
    {
        ResponseBase<List<VWI_CRM_xts_gradeDto>> ReadList(VWI_CRM_xts_gradeFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}