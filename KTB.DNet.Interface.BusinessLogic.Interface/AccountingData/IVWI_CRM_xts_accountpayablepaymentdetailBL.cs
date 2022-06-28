#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_accountpayablepaymentdetail interface
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
    public interface IVWI_CRM_xts_accountpayablepaymentdetailBL : IBaseInterface<VWI_CRM_xts_accountpayablepaymentdetailParameterDto, VWI_CRM_xts_accountpayablepaymentdetailFilterDto, VWI_CRM_xts_accountpayablepaymentdetailDto>
    {
		ResponseBase<List<VWI_CRM_xts_accountpayablepaymentdetailDto>> ReadList(VWI_CRM_xts_accountpayablepaymentdetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}