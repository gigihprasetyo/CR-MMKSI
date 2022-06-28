#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_appointment interface
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
    public interface IVWI_CRM_appointmentBL : IBaseInterface<VWI_CRM_appointmentParameterDto, VWI_CRM_appointmentFilterDto, VWI_CRM_appointmentDto>
    {
        ResponseBase<List<VWI_CRM_appointmentDto>> ReadList(VWI_CRM_appointmentFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}