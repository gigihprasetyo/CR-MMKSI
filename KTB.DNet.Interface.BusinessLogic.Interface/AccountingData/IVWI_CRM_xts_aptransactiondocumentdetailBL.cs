#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_aptransactiondocumentdetail interface
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
    public interface IVWI_CRM_xts_aptransactiondocumentdetailBL : IBaseInterface<VWI_CRM_xts_aptransactiondocumentdetailParameterDto, VWI_CRM_xts_aptransactiondocumentdetailFilterDto, VWI_CRM_xts_aptransactiondocumentdetailDto>
    {
		ResponseBase<List<VWI_CRM_xts_aptransactiondocumentdetailDto>> ReadList(VWI_CRM_xts_aptransactiondocumentdetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}