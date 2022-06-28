#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StandardCode interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.Model;
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IStandardCodeBL : IBaseInterface<StandardCodeParameterDto, StandardCodeFilterDto, StandardCodeDto>
    {
        StandardCodeDto GetByCategoryAndValue(string category, string value);

        StandardCodeDto GetByCategoryAndCode(string category, string code);

        List<StandardCodeDto> GetByCategory(string category);

        bool IsExistByCategoryAndValue(string category, string value);

        bool IsExistByCategoryAndCode(string category, string code);
    }
}
