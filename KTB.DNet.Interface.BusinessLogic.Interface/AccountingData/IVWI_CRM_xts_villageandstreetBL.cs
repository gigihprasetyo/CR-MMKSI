#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_villageandstreetBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/29/2020 08:57:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_villageandstreetBL : IBaseInterface<VWI_CRM_xts_villageandstreetParameterDto, VWI_CRM_xts_villageandstreetFilterDto, VWI_CRM_xts_villageandstreetDto>
    {
        ResponseBase<List<VWI_CRM_xts_villageandstreetDto>> ReadList(VWI_CRM_xts_villageandstreetFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
