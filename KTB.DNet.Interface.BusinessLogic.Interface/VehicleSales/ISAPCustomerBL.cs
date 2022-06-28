#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SAPCustomer interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ISAPCustomerBL : IBaseInterface<SAPCustomerParameterDto, SAPCustomerFilterDto, SAPCustomerDto>
    {
        ResponseBase<SAPCustomerDto> GetByName(string name);
        ResponseBase<List<VWI_LeadCustomerSalesForceDto>> GetLeadCustomerSalesForce(VWI_LeadCustomerSalesForceFilterDto filterDto, int pageSize);
        ResponseBase<List<VWI_LeadCustomerSalesForceDto>> GetLeadCustomerSalesForceDapper(VWI_LeadCustomerSalesForceFilterDto filterDto, int pageSize);
    }
}
