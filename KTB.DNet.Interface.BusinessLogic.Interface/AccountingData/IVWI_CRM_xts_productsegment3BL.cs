#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_productsegment3 interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2020 14:46:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_productsegment3BL : IBaseInterface<VWI_CRM_xts_productsegment3ParameterDto, VWI_CRM_xts_productsegment3FilterDto, VWI_CRM_xts_productsegment3Dto>
    {
        ResponseBase<List<VWI_CRM_xts_productsegment3Dto>> ReadList(VWI_CRM_xts_productsegment3FilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}