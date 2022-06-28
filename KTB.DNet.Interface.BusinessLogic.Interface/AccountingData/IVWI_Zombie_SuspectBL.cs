#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_Zombie_SuspectBL interface
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
    public interface IVWI_Zombie_SuspectBL : IBaseInterface<VWI_Zombie_SuspectParameterDto, VWI_Zombie_SuspectFilterDto, VWI_Zombie_SuspectDto>
    {
        ResponseBase<List<VWI_Zombie_SuspectDto>> ReadList(VWI_Zombie_SuspectFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
