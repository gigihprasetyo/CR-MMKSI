#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_contact interface
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
    public interface IVWI_CRM_contactBL : IBaseInterface<VWI_CRM_contactParameterDto, VWI_CRM_contactFilterDto, VWI_CRM_contactDto>
    {
		ResponseBase<List<VWI_CRM_contactDto>> ReadList(VWI_CRM_contactFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}