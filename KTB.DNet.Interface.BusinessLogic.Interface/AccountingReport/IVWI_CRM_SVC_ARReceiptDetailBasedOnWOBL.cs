#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SVC_ARReceiptDetailBasedOnWO interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2019 5:45:18 PM
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_SVC_ARReceiptDetailBasedOnWOBL : IBaseInterface<VWI_CRM_SVC_ARReceiptDetailBasedOnWOParameterDto, VWI_CRM_SVC_ARReceiptDetailBasedOnWOFilterDto, VWI_CRM_SVC_ARReceiptDetailBasedOnWODto>
    {
		ResponseBase<List<VWI_CRM_SVC_ARReceiptDetailBasedOnWODto>> ReadList(VWI_CRM_SVC_ARReceiptDetailBasedOnWOFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}