#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_SLS_LedgerTransaction interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/9/2019 1:53:51 PM
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_AX_SLS_LedgerTransactionBL : IBaseInterface<VWI_AX_SLS_LedgerTransactionParameterDto, VWI_AX_SLS_LedgerTransactionFilterDto, VWI_AX_SLS_LedgerTransactionDto>
    {
		ResponseBase<List<VWI_AX_SLS_LedgerTransactionDto>> ReadList(VWI_AX_SLS_LedgerTransactionFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}