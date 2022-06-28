#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_department interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/29/2020 09:43:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_departmentBL : IBaseInterface<VWI_CRM_xts_departmentParameterDto, VWI_CRM_xts_departmentFilterDto, VWI_CRM_xts_departmentDto>
    {
        ResponseBase<List<VWI_CRM_xts_departmentDto>> ReadList(VWI_CRM_xts_departmentFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}