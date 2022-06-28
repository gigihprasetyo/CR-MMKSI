#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_EmployeeSales interface
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
    public interface IVWI_EmployeeSalesBL : IBaseViewInterface<VWI_EmployeeSalesFilterDto, VWI_EmployeeSalesDto>
    {
        ResponseBase<List<VWI_EmployeeSalesDto>> ReadWithProfileCriteria(VWI_EmployeeSalesFilterDto filterDto, int pageSize);

        ResponseBase<List<VWI_EmployeeResignDto>> ReadResignEmployee(VWI_EmployeeResignFilterDto filterDto, int pageSize);

        ResponseBase<List<VWI_EmployeeSalesDto>> ReadDataResign(string salesmanCode, string NoKTP);
    }
}
