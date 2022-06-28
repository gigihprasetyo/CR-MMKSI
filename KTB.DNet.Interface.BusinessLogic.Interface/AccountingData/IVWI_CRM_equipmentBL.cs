#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_equipment interface
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
    public interface IVWI_CRM_equipmentBL : IBaseInterface<VWI_CRM_equipmentParameterDto, VWI_CRM_equipmentFilterDto, VWI_CRM_equipmentDto>
    {
		ResponseBase<List<VWI_CRM_equipmentDto>> ReadList(VWI_CRM_equipmentFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}