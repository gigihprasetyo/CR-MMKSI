#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_daftardepositadetail interface
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
    public interface IVWI_CRM_ktb_daftardepositadetailBL : IBaseInterface<VWI_CRM_ktb_daftardepositadetailDtoParameter, VWI_CRM_ktb_daftardepositadetailFilterDto, VWI_CRM_ktb_daftardepositadetailDto>
    {
        ResponseBase<List<VWI_CRM_ktb_daftardepositadetailDto>> ReadList(VWI_CRM_ktb_daftardepositadetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}