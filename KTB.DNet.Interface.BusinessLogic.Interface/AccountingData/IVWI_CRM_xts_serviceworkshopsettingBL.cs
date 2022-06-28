#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_serviceworkshopsetting interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 09:53:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_serviceworkshopsettingBL : IBaseInterface<VWI_CRM_xts_serviceworkshopsettingParameterDto, VWI_CRM_xts_serviceworkshopsettingFilterDto, VWI_CRM_xts_serviceworkshopsettingDto>
    {
        ResponseBase<List<VWI_CRM_xts_serviceworkshopsettingDto>> ReadList(VWI_CRM_xts_serviceworkshopsettingFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}