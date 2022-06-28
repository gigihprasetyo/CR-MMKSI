#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : CRM_ktb_openfacture interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 17/02/2021 11:49:03
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_ktb_openfactureBL : IBaseInterface<CRM_ktb_openfactureParameterDto, VWI_CRM_ktb_openfactureFilterDto, VWI_CRM_ktb_openfactureDto>
    {
		ResponseBase<List<VWI_CRM_ktb_openfactureDto>> ReadList(VWI_CRM_ktb_openfactureFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}