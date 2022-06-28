#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : IVWI_DepositC2BL  Business Logic
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
    public interface IVWI_DepositC2BL : IBaseInterface<VWI_DepositC2ParameterDto, VWI_DepositC2FilterDto, VWI_DepositC2Dto>
    {
        ResponseBase<List<VWI_DepositC2Dto>> ReadList(VWI_DepositC2FilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
