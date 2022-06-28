#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_ktb_daerahlogisticBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 11:59:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_ktb_daerahlogisticBL : IBaseInterface<VWI_CRM_ktb_daerahlogisticParameterDto, VWI_CRM_ktb_daerahlogisticFilterDto, VWI_CRM_ktb_daerahlogisticDto>
    {
        ResponseBase<List<VWI_CRM_ktb_daerahlogisticDto>> ReadList(VWI_CRM_ktb_daerahlogisticFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
