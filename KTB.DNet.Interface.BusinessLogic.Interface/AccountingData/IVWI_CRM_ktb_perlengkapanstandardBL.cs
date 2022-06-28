#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_perlengkapanstandard interface
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
    public interface IVWI_CRM_ktb_perlengkapanstandardBL : IBaseInterface<VWI_CRM_ktb_perlengkapanstandardParameterDto, VWI_CRM_ktb_perlengkapanstandardFilterDto, VWI_CRM_ktb_perlengkapanstandardDto>
    {
		ResponseBase<List<VWI_CRM_ktb_perlengkapanstandardDto>> ReadList(VWI_CRM_ktb_perlengkapanstandardFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}