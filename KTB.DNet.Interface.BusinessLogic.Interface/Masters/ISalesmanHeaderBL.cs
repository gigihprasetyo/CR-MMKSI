#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SalesmanHeader interface
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

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ISalesmanHeaderBL : IBaseInterface<SalesmanHeaderParameterDto, SalesmanHeaderFilterDto, SalesmanHeaderDto>
    {
        ResponseBase<EmployeePartDto> CreateEmployeePart(EmployeePartParameterDto param);
        ResponseBase<EmployeePartDto> UpdateEmployeePart(EmployeePartParameterDto param);
        ResponseBase<EmployeeSalesDto> CreateEmployeeSales(EmployeeSalesParameterDto paramDto);
        ResponseBase<EmployeeSalesDto> UpdateEmployeeSales(EmployeeSalesParameterDto paramDto);
    }
}
