#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_serviceinstruction interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 08:28:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_serviceinstructionBL : IBaseInterface<VWI_CRM_xts_serviceinstructionParameterDto, VWI_CRM_xts_serviceinstructionFilterDto, VWI_CRM_xts_serviceinstructionDto>
    {
        ResponseBase<List<VWI_CRM_xts_serviceinstructionDto>> ReadList(VWI_CRM_xts_serviceinstructionFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}