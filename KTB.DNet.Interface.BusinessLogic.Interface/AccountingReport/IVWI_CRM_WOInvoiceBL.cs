#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_WOInvoice interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2021 16:16:28
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_WOInvoiceBL : IBaseInterface<VWI_CRM_WOInvoiceParameterDto, VWI_CRM_WOInvoiceFilterDto, VWI_CRM_WOInvoiceDto>
    {
		ResponseBase<List<VWI_CRM_WOInvoiceDto>> ReadList(VWI_CRM_WOInvoiceFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}