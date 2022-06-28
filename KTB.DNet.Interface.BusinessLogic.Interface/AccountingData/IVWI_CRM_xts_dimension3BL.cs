#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_dimension3BL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 09:29:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_dimension3BL : IBaseInterface<VWI_CRM_xts_dimension3ParameterDto, VWI_CRM_xts_dimension3FilterDto, VWI_CRM_xts_dimension3Dto>
    {
        ResponseBase<List<VWI_CRM_xts_dimension3Dto>> ReadList(VWI_CRM_xts_dimension3FilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
