#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_lkpp interface
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
    public interface IVWI_CRM_ktb_lkppBL : IBaseInterface<VWI_CRM_ktb_lkppParameterDto, VWI_CRM_ktb_lkppFilterDto, VWI_CRM_ktb_lkppDto>
    {
        ResponseBase<List<VWI_CRM_ktb_lkppDto>> ReadList(VWI_CRM_ktb_lkppFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}