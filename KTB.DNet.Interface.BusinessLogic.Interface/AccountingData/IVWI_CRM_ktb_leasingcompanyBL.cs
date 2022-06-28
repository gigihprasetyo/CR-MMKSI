#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_leasingcompany interface
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
    public interface IVWI_CRM_ktb_leasingcompanyBL : IBaseInterface<VWI_CRM_ktb_leasingcompanyParameterDto, VWI_CRM_ktb_leasingcompanyFilterDto, VWI_CRM_ktb_leasingcompanyDto>
    {
		ResponseBase<List<VWI_CRM_ktb_leasingcompanyDto>> ReadList(VWI_CRM_ktb_leasingcompanyFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}