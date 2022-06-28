#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_VehicleInvoice interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 02/03/2021 6:32:17
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_VehicleInvoiceBL : IBaseInterface<VWI_CRM_VehicleInvoiceParameterDto, VWI_CRM_VehicleInvoiceFilterDto, VWI_CRM_VehicleInvoiceDto>
    {
		ResponseBase<List<VWI_CRM_VehicleInvoiceDto>> ReadList(VWI_CRM_VehicleInvoiceFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}