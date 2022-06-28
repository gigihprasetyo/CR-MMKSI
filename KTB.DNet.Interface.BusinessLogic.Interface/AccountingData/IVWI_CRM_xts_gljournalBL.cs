#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_gljournalBL interface
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
    public interface IVWI_CRM_xts_gljournalBL : IBaseInterface<VWI_CRM_xts_gljournalParameterDto, VWI_CRM_xts_gljournalFilterDto, VWI_CRM_xts_gljournalDto>
    {
        ResponseBase<List<VWI_CRM_xts_gljournalDto>> ReadList(VWI_CRM_xts_gljournalFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}