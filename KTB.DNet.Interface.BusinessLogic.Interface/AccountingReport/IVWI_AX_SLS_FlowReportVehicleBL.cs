#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_SLS_FlowReportVehicle interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 05/02/2020 9:46:08
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_AX_SLS_FlowReportVehicleBL : IBaseInterface<VWI_AX_SLS_FlowReportVehicleParameterDto, VWI_AX_SLS_FlowReportVehicleFilterDto, VWI_AX_SLS_FlowReportVehicleDto>
    {
		ResponseBase<List<VWI_AX_SLS_FlowReportVehicleDto>> ReadList(VWI_AX_SLS_FlowReportVehicleFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}