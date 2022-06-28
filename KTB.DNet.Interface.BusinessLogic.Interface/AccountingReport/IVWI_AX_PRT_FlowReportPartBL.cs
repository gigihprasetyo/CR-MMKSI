#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_PRT_FlowReportPart interface
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
    public interface IVWI_AX_PRT_FlowReportPartBL : IBaseInterface<VWI_AX_PRT_FlowReportPartParameterDto, VWI_AX_PRT_FlowReportPartFilterDto, VWI_AX_PRT_FlowReportPartDto>
    {
		ResponseBase<List<VWI_AX_PRT_FlowReportPartDto>> ReadList(VWI_AX_PRT_FlowReportPartFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}