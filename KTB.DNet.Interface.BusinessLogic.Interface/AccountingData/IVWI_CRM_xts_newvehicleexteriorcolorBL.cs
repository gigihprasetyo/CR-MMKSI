#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_newvehicleexteriorcolor interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/12/2019 3:25:51
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_newvehicleexteriorcolorBL : IBaseInterface<VWI_CRM_xts_newvehicleexteriorcolorParameterDto, VWI_CRM_xts_newvehicleexteriorcolorFilterDto, VWI_CRM_xts_newvehicleexteriorcolorDto>
    {
		ResponseBase<List<VWI_CRM_xts_newvehicleexteriorcolorDto>> ReadList(VWI_CRM_xts_newvehicleexteriorcolorFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}