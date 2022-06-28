#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_outsourceworkorderdetail interface
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
    public interface IVWI_CRM_xts_outsourceworkorderdetailBL : IBaseInterface<VWI_CRM_xts_outsourceworkorderdetailParameterDto, VWI_CRM_xts_outsourceworkorderdetailFilterDto, VWI_CRM_xts_outsourceworkorderdetailDto>
    {
		ResponseBase<List<VWI_CRM_xts_outsourceworkorderdetailDto>> ReadList(VWI_CRM_xts_outsourceworkorderdetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}