#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_saleschannel interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 08:25:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_ktb_saleschannelBL : IBaseInterface<VWI_CRM_ktb_saleschannelParameterDto, VWI_CRM_ktb_saleschannelFilterDto, VWI_CRM_ktb_saleschannelDto>
    {
        ResponseBase<List<VWI_CRM_ktb_saleschannelDto>> ReadList(VWI_CRM_ktb_saleschannelFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}