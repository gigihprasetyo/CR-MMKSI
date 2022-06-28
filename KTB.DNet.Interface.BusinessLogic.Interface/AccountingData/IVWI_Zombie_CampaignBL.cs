#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_Zombie_CampaignBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-10-13
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_Zombie_CampaignBL : IBaseInterface<VWI_Zombie_CampaignParameterDto, VWI_Zombie_CampaignFilterDto, VWI_Zombie_CampaignDto>
    {
        ResponseBase<List<VWI_Zombie_CampaignDto>> ReadList(VWI_Zombie_CampaignFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
