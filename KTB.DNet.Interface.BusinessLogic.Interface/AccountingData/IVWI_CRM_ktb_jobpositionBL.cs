#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_ktb_jobpositionBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/29/2020 09:19:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_ktb_jobpositionBL : IBaseInterface<VWI_CRM_ktb_jobpositionParameterDto, VWI_CRM_ktb_jobpositionFilterDto, VWI_CRM_ktb_jobpositionDto>
    {
        ResponseBase<List<VWI_CRM_ktb_jobpositionDto>> ReadList(VWI_CRM_ktb_jobpositionFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
