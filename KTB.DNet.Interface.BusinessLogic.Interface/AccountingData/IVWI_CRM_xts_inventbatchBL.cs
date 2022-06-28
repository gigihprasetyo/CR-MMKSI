#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_inventbatchBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 13:39:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_inventbatchBL : IBaseInterface<VWI_CRM_xts_inventbatchParameterDto, VWI_CRM_xts_inventbatchFilterDto, VWI_CRM_xts_inventbatchDto>
    {
        ResponseBase<List<VWI_CRM_xts_inventbatchDto>> ReadList(VWI_CRM_xts_inventbatchFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
