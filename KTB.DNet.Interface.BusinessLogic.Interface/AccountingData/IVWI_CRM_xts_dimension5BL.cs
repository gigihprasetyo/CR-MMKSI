#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_dimension5BL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 10:18:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_dimension5BL : IBaseInterface<VWI_CRM_xts_dimension5ParameterDto, VWI_CRM_xts_dimension5FilterDto, VWI_CRM_xts_dimension5Dto>
    {
        ResponseBase<List<VWI_CRM_xts_dimension5Dto>> ReadList(VWI_CRM_xts_dimension5FilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
