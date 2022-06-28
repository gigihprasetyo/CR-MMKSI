#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_registrationrequest interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 15:13:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xjp_registrationrequestBL : IBaseInterface<VWI_CRM_xjp_registrationrequestParameterDto, VWI_CRM_xjp_registrationrequestFilterDto, VWI_CRM_xjp_registrationrequestDto>
    {
        ResponseBase<List<VWI_CRM_xjp_registrationrequestDto>> ReadList(VWI_CRM_xjp_registrationrequestFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}