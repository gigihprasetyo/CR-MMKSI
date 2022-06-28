#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_inventorytransaction interface
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
    public interface IVWI_CRM_xts_inventorytransactionBL : IBaseInterface<VWI_CRM_xts_inventorytransactionParameterDto, VWI_CRM_xts_inventorytransactionFilterDto, VWI_CRM_xts_inventorytransactionDto>
    {
		ResponseBase<List<VWI_CRM_xts_inventorytransactionDto>> ReadList(VWI_CRM_xts_inventorytransactionFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}