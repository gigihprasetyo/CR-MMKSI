#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vehiclerecognizedmodel interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-05 09:40:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_vehiclerecognizedmodelBL : IBaseInterface<VWI_CRM_xts_vehiclerecognizedmodelParameterDto, VWI_CRM_xts_vehiclerecognizedmodelFilterDto, VWI_CRM_xts_vehiclerecognizedmodelDto>
    {
        ResponseBase<List<VWI_CRM_xts_vehiclerecognizedmodelDto>> ReadList(VWI_CRM_xts_vehiclerecognizedmodelFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}