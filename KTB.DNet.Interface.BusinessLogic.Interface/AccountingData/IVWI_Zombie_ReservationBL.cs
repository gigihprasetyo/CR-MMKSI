#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_Zombie_ReservationBL interface
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
    public interface IVWI_Zombie_ReservationBL : IBaseInterface<VWI_Zombie_ReservationParameterDto, VWI_Zombie_ReservationFilterDto, VWI_Zombie_ReservationDto>
    {
        ResponseBase<List<VWI_Zombie_ReservationDto>> ReadList(VWI_Zombie_ReservationFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
