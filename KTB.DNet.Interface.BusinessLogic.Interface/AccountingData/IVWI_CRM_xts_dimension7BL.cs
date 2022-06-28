#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_dimension7BL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 11:07:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_dimension7BL : IBaseInterface<VWI_CRM_xts_dimension7ParameterDto, VWI_CRM_xts_dimension7FilterDto, VWI_CRM_xts_dimension7Dto>
    {
        ResponseBase<List<VWI_CRM_xts_dimension7Dto>> ReadList(VWI_CRM_xts_dimension7FilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
