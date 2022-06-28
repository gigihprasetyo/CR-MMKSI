#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_kit interface
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
    public interface IVWI_CRM_xts_kitBL : IBaseInterface<VWI_CRM_xts_kitParameterDto, VWI_CRM_xts_kitFilterDto, VWI_CRM_xts_kitDto>
    {
        ResponseBase<List<VWI_CRM_xts_kitDto>> ReadList(VWI_CRM_xts_kitFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}