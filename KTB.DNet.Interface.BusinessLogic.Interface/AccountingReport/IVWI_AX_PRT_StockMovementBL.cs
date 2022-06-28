#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_PRT_StockMovement interface
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
    public interface IVWI_AX_PRT_StockMovementBL : IBaseInterface<VWI_AX_PRT_StockMovementParameterDto, VWI_AX_PRT_StockMovementFilterDto, VWI_AX_PRT_StockMovementDto>
    {
		ResponseBase<List<VWI_AX_PRT_StockMovementDto>> ReadList(VWI_AX_PRT_StockMovementFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}