#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_ktb_servicereminder  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/03/2022 10:02:47
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_ktb_servicereminderBL : IBaseInterface<VWI_CRM_ktb_servicereminderParameterDto, VWI_CRM_ktb_servicereminderFilterDto, VWI_CRM_ktb_servicereminderDto>
    {
        ResponseBase<List<VWI_CRM_ktb_servicereminderDto>> ReadList(VWI_CRM_ktb_servicereminderFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}