#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_registrationdocument interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 10:41:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xjp_registrationdocumentBL : IBaseInterface<VWI_CRM_xjp_registrationdocumentParameterDto, VWI_CRM_xjp_registrationdocumentFilterDto, VWI_CRM_xjp_registrationdocumentDto>
    {
        ResponseBase<List<VWI_CRM_xjp_registrationdocumentDto>> ReadList(VWI_CRM_xjp_registrationdocumentFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}