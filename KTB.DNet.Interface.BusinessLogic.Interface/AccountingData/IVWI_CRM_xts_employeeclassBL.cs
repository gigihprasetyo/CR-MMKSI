#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_employeeclassBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/29/2020 10:05:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_employeeclassBL : IBaseInterface<VWI_CRM_xts_employeeclassParameterDto, VWI_CRM_xts_employeeclassFilterDto, VWI_CRM_xts_employeeclassDto>
    {
        ResponseBase<List<VWI_CRM_xts_employeeclassDto>> ReadList(VWI_CRM_xts_employeeclassFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
