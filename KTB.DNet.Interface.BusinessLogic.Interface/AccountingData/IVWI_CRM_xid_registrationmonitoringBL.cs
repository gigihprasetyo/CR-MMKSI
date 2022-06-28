#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xid_registrationmonitoring interface
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
    public interface IVWI_CRM_xid_registrationmonitoringBL : IBaseInterface<VWI_CRM_xid_registrationmonitoringParameterDto, VWI_CRM_xid_registrationmonitoringFilterDto, VWI_CRM_xid_registrationmonitoringDto>
    {
		ResponseBase<List<VWI_CRM_xid_registrationmonitoringDto>> ReadList(VWI_CRM_xid_registrationmonitoringFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}