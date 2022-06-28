#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_purchaseorderdetail interface
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
    public interface IVWI_CRM_xts_purchaseorderdetailBL : IBaseInterface<VWI_CRM_xts_purchaseorderdetailParameterDto, VWI_CRM_xts_purchaseorderdetailFilterDto, VWI_CRM_xts_purchaseorderdetailDto>
    {
		ResponseBase<List<VWI_CRM_xts_purchaseorderdetailDto>> ReadList(VWI_CRM_xts_purchaseorderdetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}