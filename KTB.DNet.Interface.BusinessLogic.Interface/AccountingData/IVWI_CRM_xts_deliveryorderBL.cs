#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_deliveryorder interface
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
    public interface IVWI_CRM_xts_deliveryorderBL : IBaseInterface<VWI_CRM_xts_deliveryorderParameterDto, VWI_CRM_xts_deliveryorderFilterDto, VWI_CRM_xts_deliveryorderDto>
    {
		ResponseBase<List<VWI_CRM_xts_deliveryorderDto>> ReadList(VWI_CRM_xts_deliveryorderFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}