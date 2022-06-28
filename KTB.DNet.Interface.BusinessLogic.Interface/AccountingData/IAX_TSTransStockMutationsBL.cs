#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : AX_TSTransStockMutations interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 29/03/2022 9:17:19
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IAX_TSTransStockMutationsBL : IBaseInterface<AX_TSTransStockMutationsParameterDto, AX_TSTransStockMutationsFilterDto, AX_TSTransStockMutationsDto>
    {
		ResponseBase<List<AX_TSTransStockMutationsDto>> ReadList(AX_TSTransStockMutationsFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}