#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : ServiceTemplate interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2021 16:16:28
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IServiceTemplateStallBL : IBaseInterface<ServiceTemplateHeaderParameterDto, ServiceTemplateHeaderFilterDto, ServiceTemplateHeaderDto>
    {
        ResponseBase<List<ServiceTemplateHeaderDto>> ReadList(ServiceTemplateHeaderFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}