#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_gljournaldetailsBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-09-28
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_gljournaldetailsBL : IBaseInterface<VWI_CRM_xts_gljournaldetailsParameterDto, VWI_CRM_xts_gljournaldetailsFilterDto, VWI_CRM_xts_gljournaldetailsDto>
    {
        ResponseBase<List<VWI_CRM_xts_gljournaldetailsDto>> ReadList(VWI_CRM_xts_gljournaldetailsFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
