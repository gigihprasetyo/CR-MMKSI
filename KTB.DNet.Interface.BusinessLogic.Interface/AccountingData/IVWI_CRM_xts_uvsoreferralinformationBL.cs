#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_uvsoreferralinformation interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-05 09:07:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_uvsoreferralinformationBL : IBaseInterface<VWI_CRM_xts_uvsoreferralinformationParameterDto, VWI_CRM_xts_uvsoreferralinformationFilterDto, VWI_CRM_xts_uvsoreferralinformationDto>
    {
        ResponseBase<List<VWI_CRM_xts_uvsoreferralinformationDto>> ReadList(VWI_CRM_xts_uvsoreferralinformationFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}