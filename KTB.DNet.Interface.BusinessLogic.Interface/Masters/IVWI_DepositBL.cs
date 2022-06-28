#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : IVWI_DepositBL  Business Logic
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
    public interface IVWI_DepositBL : IBaseInterface<VWI_DepositParameterDto, VWI_DepositFilterDto, VWI_DepositDto>
    {
        ResponseBase<List<VWI_DepositDto>> ReadList(VWI_DepositFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}

