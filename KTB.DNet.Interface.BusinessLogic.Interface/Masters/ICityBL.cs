#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : City interface
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
    public interface ICityBL : IBaseInterface<CityParameterDto, CityFilterDto, CityDto>
    {
        ResponseBase<List<VWI_CityDto>> Get(CityFilterDto filterDto, int pageSize);
    }
}
