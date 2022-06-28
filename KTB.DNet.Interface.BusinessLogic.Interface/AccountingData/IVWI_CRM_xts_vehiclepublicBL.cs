#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_vehiclepublicBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-05 10:23:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_vehiclepublicBL : IBaseInterface<VWI_CRM_xts_vehiclepublicParameterDto, VWI_CRM_xts_vehiclepublicFilterDto, VWI_CRM_xts_vehiclepublicDto>
    {
        ResponseBase<List<VWI_CRM_xts_vehiclepublicDto>> ReadList(VWI_CRM_xts_vehiclepublicFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
