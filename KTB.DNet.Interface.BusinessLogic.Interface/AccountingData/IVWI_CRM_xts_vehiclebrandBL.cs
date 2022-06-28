#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_vehiclebrandBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/18/2020 09:31:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_vehiclebrandBL : IBaseInterface<VWI_CRM_xts_vehiclebrandParameterDto, VWI_CRM_xts_vehiclebrandFilterDto, VWI_CRM_xts_vehiclebrandDto>
    {
        ResponseBase<List<VWI_CRM_xts_vehiclebrandDto>> ReadList(VWI_CRM_xts_vehiclebrandFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
