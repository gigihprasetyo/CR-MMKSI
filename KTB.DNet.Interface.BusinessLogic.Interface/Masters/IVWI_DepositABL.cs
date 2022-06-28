#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : IVWI_DepositABL  Business Logic
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 2021-09-13
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_DepositABL : IBaseInterface<VWI_DepositAParameterDto, VWI_DepositAFilterDto, VWI_DepositADto>
    {
        ResponseBase<List<VWI_DepositADto>> ReadList(VWI_DepositAFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
