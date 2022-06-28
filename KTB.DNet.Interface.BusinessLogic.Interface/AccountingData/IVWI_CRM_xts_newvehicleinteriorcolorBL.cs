#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_newvehicleinteriorcolorBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/29/2020 10:32:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_newvehicleinteriorcolorBL : IBaseInterface<VWI_CRM_xts_newvehicleinteriorcolorParameterDto, VWI_CRM_xts_newvehicleinteriorcolorFilterDto, VWI_CRM_xts_newvehicleinteriorcolorDto>
    {
        ResponseBase<List<VWI_CRM_xts_newvehicleinteriorcolorDto>> ReadList(VWI_CRM_xts_newvehicleinteriorcolorFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
