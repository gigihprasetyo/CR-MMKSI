#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_productexteriorcolor interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/18/2020 08:27:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_productexteriorcolorBL : IBaseInterface<VWI_CRM_xts_productexteriorcolorParameterDto, VWI_CRM_xts_productexteriorcolorFilterDto, VWI_CRM_xts_productexteriorcolorDto>
    {
        ResponseBase<List<VWI_CRM_xts_productexteriorcolorDto>> ReadList(VWI_CRM_xts_productexteriorcolorFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}