#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xjp_registrationcolorBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 13:20:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xjp_registrationcolorBL : IBaseInterface<VWI_CRM_xjp_registrationcolorParameterDto, VWI_CRM_xjp_registrationcolorFilterDto, VWI_CRM_xjp_registrationcolorDto>
    {
        ResponseBase<List<VWI_CRM_xjp_registrationcolorDto>> ReadList(VWI_CRM_xjp_registrationcolorFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
