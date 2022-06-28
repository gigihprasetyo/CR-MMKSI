#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_pricelevel interface
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
    public interface IVWI_CRM_pricelevelBL : IBaseInterface<VWI_CRM_pricelevelParameterDto, VWI_CRM_pricelevelFilterDto, VWI_CRM_pricelevelDto>
    {
        ResponseBase<List<VWI_CRM_pricelevelDto>> ReadList(VWI_CRM_pricelevelFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}