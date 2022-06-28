#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_benefit interface
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
    public interface IVWI_CRM_ktb_benefitBL : IBaseInterface<VWI_CRM_ktb_benefitParameterDto, VWI_CRM_ktb_benefitFilterDto, VWI_CRM_ktb_benefitDto>
    {
        ResponseBase<List<VWI_CRM_ktb_benefitDto>> ReadList(VWI_CRM_ktb_benefitFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}