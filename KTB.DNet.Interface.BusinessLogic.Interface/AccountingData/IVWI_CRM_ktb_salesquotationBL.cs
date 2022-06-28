#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_ktb_salesquotationBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/22/2020 17:01:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_ktb_salesquotationBL : IBaseInterface<VWI_CRM_ktb_salesquotationParameterDto, VWI_CRM_ktb_salesquotationFilterDto, VWI_CRM_ktb_salesquotationDto>
    {
        ResponseBase<List<VWI_CRM_ktb_salesquotationDto>> ReadList(VWI_CRM_ktb_salesquotationFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
