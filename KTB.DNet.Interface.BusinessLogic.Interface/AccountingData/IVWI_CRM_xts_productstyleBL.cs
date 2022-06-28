#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_xts_productstyleBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 13:57:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xts_productstyleBL : IBaseInterface<VWI_CRM_xts_productstyleParameterDto, VWI_CRM_xts_productstyleFilterDto, VWI_CRM_xts_productstyleDto>
    {
        ResponseBase<List<VWI_CRM_xts_productstyleDto>> ReadList(VWI_CRM_xts_productstyleFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
