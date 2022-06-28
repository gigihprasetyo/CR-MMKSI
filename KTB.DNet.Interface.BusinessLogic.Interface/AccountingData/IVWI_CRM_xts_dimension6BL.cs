#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_dimension6BL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 10:38:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_dimension6BL : IBaseInterface<VWI_CRM_xts_dimension6ParameterDto, VWI_CRM_xts_dimension6FilterDto, VWI_CRM_xts_dimension6Dto>
    {
        ResponseBase<List<VWI_CRM_xts_dimension6Dto>> ReadList(VWI_CRM_xts_dimension6FilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
