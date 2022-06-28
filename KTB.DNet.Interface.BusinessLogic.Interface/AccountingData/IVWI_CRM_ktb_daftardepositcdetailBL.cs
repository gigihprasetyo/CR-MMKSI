#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_daftardepositcdetail interface
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
    public interface IVWI_CRM_ktb_daftardepositcdetailBL : IBaseInterface<VWI_CRM_ktb_daftardepositcdetailDtoParameter, VWI_CRM_ktb_daftardepositcdetailFilterDto, VWI_CRM_ktb_daftardepositcdetailDto>
    {
        ResponseBase<List<VWI_CRM_ktb_daftardepositcdetailDto>> ReadList(VWI_CRM_ktb_daftardepositcdetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}