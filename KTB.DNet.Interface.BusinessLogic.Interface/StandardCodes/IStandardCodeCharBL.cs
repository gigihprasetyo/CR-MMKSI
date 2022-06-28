#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StandardCodeChar interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namescape Imports
using KTB.DNet.Interface.Model;
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IStandardCodeCharBL : IBaseInterface<StandardCodeCharParameterDto, StandardCodeCharFilterDto, StandardCodeCharDto>
    {
        StandardCodeCharDto GetByCategoryAndCode(string category, string code);
        List<StandardCodeCharDto> GetByCategory(string category);
    }
}
