#region Summary
/*
 ===========================================================================
 AUTHOR        : Ivan
 PURPOSE       : IServiceMMSBL IBL class
 SPECIAL NOTES : DNet WebApi Project
 ---------------------
 Copyright  (c) 2021 
 ---------------------
 $History      : $
 Created on 2021-10-26
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IServiceMMSBL : IBaseInterface<ServiceMMSParameterDto, ServiceMMSFilterDto, ServiceMMSDto>
    {
        ResponseBase<ServiceMMSDto> Create(ServiceMMSCreateParameterDto objCreate);
        ResponseBase<ServiceMMSDto> Update(ServiceMMSUpdateParameterDto objCreate);
    }
}
