#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_reservationtransaction interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/12/2019 3:25:51
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_reservationtransactionBL : IBaseInterface<VWI_CRM_xts_reservationtransactionParameterDto, VWI_CRM_xts_reservationtransactionFilterDto, VWI_CRM_xts_reservationtransactionDto>
    {
		ResponseBase<List<VWI_CRM_xts_reservationtransactionDto>> ReadList(VWI_CRM_xts_reservationtransactionFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}