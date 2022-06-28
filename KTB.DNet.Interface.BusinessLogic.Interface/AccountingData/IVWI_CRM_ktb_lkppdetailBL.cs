#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_lkppdetail interface
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
    public interface IVWI_CRM_ktb_lkppdetailBL : IBaseInterface<VWI_CRM_ktb_lkppdetailParameterDto, VWI_CRM_ktb_lkppdetailFilterDto, VWI_CRM_ktb_lkppdetailDto>
    {
        ResponseBase<List<VWI_CRM_ktb_lkppdetailDto>> ReadList(VWI_CRM_ktb_lkppdetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}