#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_dimension1BL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 08:45:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_dimension1BL : IBaseInterface<VWI_CRM_xts_dimension1ParameterDto, VWI_CRM_xts_dimension1FilterDto, VWI_CRM_xts_dimension1Dto>
    {
        ResponseBase<List<VWI_CRM_xts_dimension1Dto>> ReadList(VWI_CRM_xts_dimension1FilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
