#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_goodsreceipt interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-05 08:42:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_goodsreceiptBL : IBaseInterface<VWI_CRM_xts_goodsreceiptParameterDto, VWI_CRM_xts_goodsreceiptFilterDto, VWI_CRM_xts_goodsreceiptDto>
    {
        ResponseBase<List<VWI_CRM_xts_goodsreceiptDto>> ReadList(VWI_CRM_xts_goodsreceiptFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}