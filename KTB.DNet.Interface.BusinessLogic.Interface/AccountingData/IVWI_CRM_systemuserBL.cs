#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_systemuser interface
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
    public interface IVWI_CRM_systemuserBL : IBaseInterface<VWI_CRM_systemuserParameterDto, VWI_CRM_systemuserFilterDto, VWI_CRM_systemuserDto>
    {
        ResponseBase<List<VWI_CRM_systemuserDto>> ReadList(VWI_CRM_systemuserFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}