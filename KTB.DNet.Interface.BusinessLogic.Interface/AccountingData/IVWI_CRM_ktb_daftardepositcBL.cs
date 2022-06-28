
#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_daftardepositc interface
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
    public interface IVWI_CRM_ktb_daftardepositcBL : IBaseInterface<VWI_CRM_ktb_daftardepositcDtoParameter, VWI_CRM_ktb_daftardepositcFilterDto, VWI_CRM_ktb_daftardepositcDto>
    {
        ResponseBase<List<VWI_CRM_ktb_daftardepositcDto>> ReadList(VWI_CRM_ktb_daftardepositcFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}