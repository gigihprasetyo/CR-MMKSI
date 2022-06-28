#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_predeliveryinspection interface
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
    public interface IVWI_CRM_xjp_predeliveryinspectionBL : IBaseInterface<VWI_CRM_xjp_predeliveryinspectionParameterDto, VWI_CRM_xjp_predeliveryinspectionFilterDto, VWI_CRM_xjp_predeliveryinspectionDto>
    {
		ResponseBase<List<VWI_CRM_xjp_predeliveryinspectionDto>> ReadList(VWI_CRM_xjp_predeliveryinspectionFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}