#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SOInvoice interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 01/03/2021 0:35:59
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_SOInvoiceBL : IBaseInterface<VWI_CRM_SOInvoiceParameterDto, VWI_CRM_SOInvoiceFilterDto, VWI_CRM_SOInvoiceDto>
    {
		ResponseBase<List<VWI_CRM_SOInvoiceDto>> ReadList(VWI_CRM_SOInvoiceFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}