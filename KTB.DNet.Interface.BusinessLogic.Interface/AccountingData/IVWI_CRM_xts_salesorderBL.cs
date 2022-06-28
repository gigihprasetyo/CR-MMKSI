#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_salesorder interface
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
    public interface IVWI_CRM_xts_salesorderBL : IBaseInterface<VWI_CRM_xts_salesorderParameterDto, VWI_CRM_xts_salesorderFilterDto, VWI_CRM_xts_salesorderDto>
    {
		ResponseBase<List<VWI_CRM_xts_salesorderDto>> ReadList(VWI_CRM_xts_salesorderFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}