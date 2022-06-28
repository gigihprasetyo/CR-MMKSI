#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_invalidcustomer interface
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
    public interface IVWI_CRM_invalidcustomerBL : IBaseInterface<VWI_CRM_invalidcustomerDtoParameter, VWI_CRM_invalidcustomerFilterDto, VWI_CRM_invalidcustomerDto>
    {
        ResponseBase<List<VWI_CRM_invalidcustomerDto>> ReadList(VWI_CRM_invalidcustomerFilterDto filterDto, int pageSize);
    }
}
