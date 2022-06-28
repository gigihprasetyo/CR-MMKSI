#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_accountreceivablereceiptdetail interface
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
    public interface IVWI_CRM_xts_accountreceivablereceiptdetailBL : IBaseInterface<VWI_CRM_xts_accountreceivablereceiptdetailParameterDto, VWI_CRM_xts_accountreceivablereceiptdetailFilterDto, VWI_CRM_xts_accountreceivablereceiptdetailDto>
    {
		ResponseBase<List<VWI_CRM_xts_accountreceivablereceiptdetailDto>> ReadList(VWI_CRM_xts_accountreceivablereceiptdetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}