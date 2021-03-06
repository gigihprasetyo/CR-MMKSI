
#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_daftardepositb interface
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
    public interface IVWI_CRM_ktb_daftardepositbBL : IBaseInterface<VWI_CRM_ktb_daftardepositbDtoParameter, VWI_CRM_ktb_daftardepositbFilterDto, VWI_CRM_ktb_daftardepositbDto>
    {
        ResponseBase<List<VWI_CRM_ktb_daftardepositbDto>> ReadList(VWI_CRM_ktb_daftardepositbFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
