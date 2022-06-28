#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_servicemms interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-05 10:01:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_servicemmsBL : IBaseInterface<VWI_CRM_xts_servicemmsParameterDto, VWI_CRM_xts_servicemmsFilterDto, VWI_CRM_xts_servicemmsDto>
    {
        ResponseBase<List<VWI_CRM_xts_servicemmsDto>> ReadList(VWI_CRM_xts_servicemmsFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}