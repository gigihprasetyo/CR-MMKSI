#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : IVWI_DepositBBL  Business Logic
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 2021-09-14
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_DepositBBL : IBaseInterface<VWI_DepositBHeaderParameterDto, VWI_DepositBHeaderFilterDto, VWI_DepositBHeaderDto>
    {
        ResponseBase<List<VWI_DepositBHeaderDto>> ReadList(VWI_DepositBHeaderFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
