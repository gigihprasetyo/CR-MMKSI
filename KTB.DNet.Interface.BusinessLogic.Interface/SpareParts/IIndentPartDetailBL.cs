#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : IndentPartDetail interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

using KTB.DNet.Domain;
using KTB.DNet.Interface.Model;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IIndentPartDetailBL : IBaseInterface<IndentPartDetailParameterDto, IndentPartDetailFilterDto, IndentPartDetailDto>
    {
        List<DNetValidationResult> ValidateCreateParameterDto(IndentPartDetailParameterDto objCreate, out IndentPartDetail indentPartDetail);
    }
}
