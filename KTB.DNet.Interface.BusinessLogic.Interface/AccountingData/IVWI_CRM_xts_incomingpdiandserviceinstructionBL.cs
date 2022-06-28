#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_incomingpdiandserviceinstruction interface
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
    public interface IVWI_CRM_xts_incomingpdiandserviceinstructionBL : IBaseInterface<VWI_CRM_xts_incomingpdiandserviceinstructionParameterDto, VWI_CRM_xts_incomingpdiandserviceinstructionFilterDto, VWI_CRM_xts_incomingpdiandserviceinstructionDto>
    {
		ResponseBase<List<VWI_CRM_xts_incomingpdiandserviceinstructionDto>> ReadList(VWI_CRM_xts_incomingpdiandserviceinstructionFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}