#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_uomconversion interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 11:15:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_uomconversionBL : IBaseInterface<VWI_CRM_xts_uomconversionParameterDto, VWI_CRM_xts_uomconversionFilterDto, VWI_CRM_xts_uomconversionDto>
    {
        ResponseBase<List<VWI_CRM_xts_uomconversionDto>> ReadList(VWI_CRM_xts_uomconversionFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}