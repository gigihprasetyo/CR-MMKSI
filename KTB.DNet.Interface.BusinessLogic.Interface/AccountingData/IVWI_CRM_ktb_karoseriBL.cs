#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_karoseri interface
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
    public interface IVWI_CRM_ktb_karoseriBL : IBaseInterface<VWI_CRM_ktb_karoseriParameterDto, VWI_CRM_ktb_karoseriFilterDto, VWI_CRM_ktb_karoseriDto>
    {
        ResponseBase<List<VWI_CRM_ktb_karoseriDto>> ReadList(VWI_CRM_ktb_karoseriFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}