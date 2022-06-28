#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_SLS_StockMutation interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/01/2020 10:25:21
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_AX_SLS_StockMutationBL : IBaseInterface<VWI_AX_SLS_StockMutationParameterDto, VWI_AX_SLS_StockMutationFilterDto, VWI_AX_SLS_StockMutationDto>
    {
		ResponseBase<List<VWI_AX_SLS_StockMutationDto>> ReadList(VWI_AX_SLS_StockMutationFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}