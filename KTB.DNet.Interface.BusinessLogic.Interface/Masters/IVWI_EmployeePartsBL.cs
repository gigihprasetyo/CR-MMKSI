#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_EmployeeParts interface
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
    public interface IVWI_EmployeePartsBL : IBaseViewInterface<VWI_EmployeePartsFilterDto, VWI_EmployeePartsDto>
    {
        ResponseBase<List<VWI_EmployeePartsDto>> ReadWithProfileCriteria(VWI_EmployeePartsFilterDto filterDto, int pageSize);

        ResponseBase<List<VWI_EmployeeResignDto>> ReadResignEmployee(VWI_EmployeeResignFilterDto filterDto, int pageSize);

        ResponseBase<List<VWI_EmployeePartsDto>> ReadDataResign(string salesmanCode);
    }
}
