#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SVC_ARReceipt interface
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
    public interface IVWI_CRM_SVC_ARReceiptBL : IBaseInterface<VWI_CRM_SVC_ARReceiptParameterDto, VWI_CRM_SVC_ARReceiptFilterDto, VWI_CRM_SVC_ARReceiptDto>
    {
		ResponseBase<List<VWI_CRM_SVC_ARReceiptDto>> ReadList(VWI_CRM_SVC_ARReceiptFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}