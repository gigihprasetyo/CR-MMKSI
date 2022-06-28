#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_inventtrans interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 22/03/2022 13:51:21
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_inventtransBL : IBaseInterface<VWI_CRM_xts_inventtransParameterDto, VWI_CRM_xts_inventtransFilterDto, VWI_CRM_xts_inventtransDto>
    {
		ResponseBase<List<VWI_CRM_xts_inventtransDto>> ReadList(VWI_CRM_xts_inventtransFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}