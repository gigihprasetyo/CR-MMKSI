#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_outsourceworkorderreceiptdetail interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2020 10:42:01
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_outsourceworkorderreceiptdetailBL : IBaseInterface<VWI_CRM_xts_outsourceworkorderreceiptdetailParameterDto, VWI_CRM_xts_outsourceworkorderreceiptdetailFilterDto, VWI_CRM_xts_outsourceworkorderreceiptdetailDto>
    {
		ResponseBase<List<VWI_CRM_xts_outsourceworkorderreceiptdetailDto>> ReadList(VWI_CRM_xts_outsourceworkorderreceiptdetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}