
#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_vehiclecostinputdetail interface
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
    public interface IVWI_CRM_xjp_vehiclecostinputdetailBL : IBaseInterface<VWI_CRM_xjp_vehiclecostinputdetailDtoParameter, VWI_CRM_xjp_vehiclecostinputdetailFilterDto, VWI_CRM_xjp_vehiclecostinputdetailDto>
    {
        ResponseBase<List<VWI_CRM_xjp_vehiclecostinputdetailDto>> ReadList(VWI_CRM_xjp_vehiclecostinputdetailFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}