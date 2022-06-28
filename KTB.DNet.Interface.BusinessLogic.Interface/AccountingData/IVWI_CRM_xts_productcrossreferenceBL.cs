#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_productcrossreference interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/18/2020 10:34:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_productcrossreferenceBL : IBaseInterface<VWI_CRM_xts_productcrossreferenceParameterDto, VWI_CRM_xts_productcrossreferenceFilterDto, VWI_CRM_xts_productcrossreferenceDto>
    {
        ResponseBase<List<VWI_CRM_xts_productcrossreferenceDto>> ReadList(VWI_CRM_xts_productcrossreferenceFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}