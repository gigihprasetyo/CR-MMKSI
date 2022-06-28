#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_transferorderrequesting interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 14:23:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_transferorderrequestingBL : IBaseInterface<VWI_CRM_xts_transferorderrequestingParameterDto, VWI_CRM_xts_transferorderrequestingFilterDto, VWI_CRM_xts_transferorderrequestingDto>
    {
        ResponseBase<List<VWI_CRM_xts_transferorderrequestingDto>> ReadList(VWI_CRM_xts_transferorderrequestingFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}