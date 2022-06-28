#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_purchaserequisitiondetail interface
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
    public interface IVWI_CRM_xts_purchaserequisitiondetailBL : IBaseInterface<VWI_CRM_xts_purchaserequisitiondetailParameterDto, VWI_CRM_xts_purchaserequisitiondetailFilterDto, VWI_CRM_xts_purchaserequisitiondetailDto>
    {
		ResponseBase<List<VWI_CRM_xts_purchaserequisitiondetailDto>> ReadList(VWI_CRM_xts_purchaserequisitiondetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}