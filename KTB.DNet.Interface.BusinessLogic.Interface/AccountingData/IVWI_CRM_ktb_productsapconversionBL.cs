#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_productsapconversion interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2020 17:06:21
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_ktb_productsapconversionBL : IBaseInterface<VWI_CRM_ktb_productsapconversionParameterDto, VWI_CRM_ktb_productsapconversionFilterDto, VWI_CRM_ktb_productsapconversionDto>
    {
		ResponseBase<List<VWI_CRM_ktb_productsapconversionDto>> ReadList(VWI_CRM_ktb_productsapconversionFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}