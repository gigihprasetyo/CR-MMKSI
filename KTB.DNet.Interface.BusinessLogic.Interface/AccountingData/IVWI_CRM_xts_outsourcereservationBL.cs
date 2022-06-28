#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_outsourcereservation interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/28/2020 08:27:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_outsourcereservationBL : IBaseInterface<VWI_CRM_xts_outsourcereservationParameterDto, VWI_CRM_xts_outsourcereservationFilterDto, VWI_CRM_xts_outsourcereservationDto>
    {
        ResponseBase<List<VWI_CRM_xts_outsourcereservationDto>> ReadList(VWI_CRM_xts_outsourcereservationFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}