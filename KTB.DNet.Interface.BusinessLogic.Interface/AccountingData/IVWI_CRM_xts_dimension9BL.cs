#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_dimension9BL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 11:42:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_dimension9BL : IBaseInterface<VWI_CRM_xts_dimension9ParameterDto, VWI_CRM_xts_dimension9FilterDto, VWI_CRM_xts_dimension9Dto>
    {
        ResponseBase<List<VWI_CRM_xts_dimension9Dto>> ReadList(VWI_CRM_xts_dimension9FilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
