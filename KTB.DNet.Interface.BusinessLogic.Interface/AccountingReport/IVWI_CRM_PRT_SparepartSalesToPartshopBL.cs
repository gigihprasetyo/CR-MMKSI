#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_SparepartSalesToPartshop interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2019 5:45:18 PM
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_PRT_SparepartSalesToPartshopBL : IBaseInterface<VWI_CRM_PRT_SparepartSalesToPartshopParameterDto, VWI_CRM_PRT_SparepartSalesToPartshopFilterDto, VWI_CRM_PRT_SparepartSalesToPartshopDto>
    {
		ResponseBase<List<VWI_CRM_PRT_SparepartSalesToPartshopDto>> ReadList(VWI_CRM_PRT_SparepartSalesToPartshopFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}