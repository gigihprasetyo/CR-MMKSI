#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_dimension10BL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 11:58:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_dimension10BL : IBaseInterface<VWI_CRM_xts_dimension10ParameterDto, VWI_CRM_xts_dimension10FilterDto, VWI_CRM_xts_dimension10Dto>
    {
        ResponseBase<List<VWI_CRM_xts_dimension10Dto>> ReadList(VWI_CRM_xts_dimension10FilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
