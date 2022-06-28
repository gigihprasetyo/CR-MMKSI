#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_IncomingTransaction interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2019 5:45:18 PM
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_PRT_IncomingTransactionBL : IBaseInterface<VWI_CRM_PRT_IncomingTransactionParameterDto, VWI_CRM_PRT_IncomingTransactionFilterDto, VWI_CRM_PRT_IncomingTransactionDto>
    {
		ResponseBase<List<VWI_CRM_PRT_IncomingTransactionDto>> ReadList(VWI_CRM_PRT_IncomingTransactionFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}