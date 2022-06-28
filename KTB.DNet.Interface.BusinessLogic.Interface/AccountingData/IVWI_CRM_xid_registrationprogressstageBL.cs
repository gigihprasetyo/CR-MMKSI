#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xid_registrationprogressstageBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 08:50:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xid_registrationprogressstageBL : IBaseInterface<VWI_CRM_xid_registrationprogressstageParameterDto, VWI_CRM_xid_registrationprogressstageFilterDto, VWI_CRM_xid_registrationprogressstageDto>
    {
        ResponseBase<List<VWI_CRM_xid_registrationprogressstageDto>> ReadList(VWI_CRM_xid_registrationprogressstageFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
