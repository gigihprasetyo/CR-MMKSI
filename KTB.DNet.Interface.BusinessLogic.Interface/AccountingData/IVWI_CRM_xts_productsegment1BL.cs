#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_productsegment1 interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2020 14:18:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_productsegment1BL : IBaseInterface<VWI_CRM_xts_productsegment1ParameterDto, VWI_CRM_xts_productsegment1FilterDto, VWI_CRM_xts_productsegment1Dto>
    {
        ResponseBase<List<VWI_CRM_xts_productsegment1Dto>> ReadList(VWI_CRM_xts_productsegment1FilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}