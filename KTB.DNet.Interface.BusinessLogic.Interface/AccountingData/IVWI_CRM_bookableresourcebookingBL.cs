#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : IVWI_CRM_bookableresourcebookingBL interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-10-08
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_bookableresourcebookingBL : IBaseInterface<VWI_CRM_bookableresourcebookingParameterDto, VWI_CRM_bookableresourcebookingFilterDto, VWI_CRM_bookableresourcebookingDto>
    {
        ResponseBase<List<VWI_CRM_bookableresourcebookingDto>> ReadList(VWI_CRM_bookableresourcebookingFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
