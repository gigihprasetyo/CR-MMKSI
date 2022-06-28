#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_bookingstatusBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-10-08
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_bookingstatusBL : IBaseInterface<VWI_CRM_bookingstatusParameterDto, VWI_CRM_bookingstatusFilterDto, VWI_CRM_bookingstatusDto>
    {
        ResponseBase<List<VWI_CRM_bookingstatusDto>> ReadList(VWI_CRM_bookingstatusFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
