#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_ktb_vendorlogisticBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 13:31:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_ktb_vendorlogisticBL : IBaseInterface<VWI_CRM_ktb_vendorlogisticParameterDto, VWI_CRM_ktb_vendorlogisticFilterDto, VWI_CRM_ktb_vendorlogisticDto>
    {
        ResponseBase<List<VWI_CRM_ktb_vendorlogisticDto>> ReadList(VWI_CRM_ktb_vendorlogisticFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
