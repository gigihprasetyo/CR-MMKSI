#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_aptransactiondocument interface
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
    public interface IVWI_CRM_xts_aptransactiondocumentBL : IBaseInterface<VWI_CRM_xts_aptransactiondocumentParameterDto, VWI_CRM_xts_aptransactiondocumentFilterDto, VWI_CRM_xts_aptransactiondocumentDto>
    {
		ResponseBase<List<VWI_CRM_xts_aptransactiondocumentDto>> ReadList(VWI_CRM_xts_aptransactiondocumentFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}