#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_SLS_BankTransaction interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/9/2019 1:55:37 PM
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_AX_SLS_BankTransactionBL : IBaseInterface<VWI_AX_SLS_BankTransactionParameterDto, VWI_AX_SLS_BankTransactionFilterDto, VWI_AX_SLS_BankTransactionDto>
    {
		ResponseBase<List<VWI_AX_SLS_BankTransactionDto>> ReadList(VWI_AX_SLS_BankTransactionFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}