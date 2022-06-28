#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_EmployeeService interface
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
    public interface IVWI_EmployeeServiceBL : IBaseViewInterface<VWI_EmployeeServiceFilterDto, VWI_EmployeeServiceDto>
    {
        ResponseBase<List<VWI_EmployeeServiceDto>> ReadWithProfile(VWI_EmployeeServiceFilterDto filterDto, int pageSize);

        ResponseBase<List<VWI_EmployeeServiceResignDto>> ReadResignEmployee(VWI_EmployeeResignFilterDto filterDto, int pageSize);

        ResponseBase<List<VWI_EmployeeServiceDto>> ReadDataResign(int salesmanID);
    }
}
