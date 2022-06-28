#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_daftardepositbdetail interface
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
    public interface IVWI_CRM_ktb_daftardepositbdetailBL : IBaseInterface<VWI_CRM_ktb_daftardepositbdetailDtoParameter, VWI_CRM_ktb_daftardepositbdetailFilterDto, VWI_CRM_ktb_daftardepositbdetailDto>
    {
        ResponseBase<List<VWI_CRM_ktb_daftardepositbdetailDto>> ReadList(VWI_CRM_ktb_daftardepositbdetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}