#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_provinceBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/15/2020 16:20:00 AM
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_provinceBL : IBaseInterface<VWI_CRM_xts_provinceParameterDto, VWI_CRM_xts_provinceFilterDto, VWI_CRM_xts_provinceDto>
    {
        ResponseBase<List<VWI_CRM_xts_provinceDto>> ReadList(VWI_CRM_xts_provinceFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
