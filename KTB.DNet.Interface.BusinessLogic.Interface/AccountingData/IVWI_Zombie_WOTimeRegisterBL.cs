#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_Zombie_WOTimeRegisterBL interface
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
    public interface IVWI_Zombie_WOTimeRegisterBL : IBaseInterface<VWI_Zombie_WOTimeRegisterParameterDto, VWI_Zombie_WOTimeRegisterFilterDto, VWI_Zombie_WOTimeRegisterDto>
    {
        ResponseBase<List<VWI_Zombie_WOTimeRegisterDto>> ReadList(VWI_Zombie_WOTimeRegisterFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
