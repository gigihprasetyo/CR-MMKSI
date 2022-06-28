#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : IndentPartHeader interface
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
    public interface IIndentPartHeaderBL : IBaseInterface<IndentPartHeaderParameterDto, IndentPartHeaderFilterDto, IndentPartHeaderDto>
    {
        ResponseBase<IndentPartHeaderCreateResponseDto> Create(IndentPartHeaderCreateParameterDto param);
        ResponseBase<IndentPartHeaderDto> Update(IndentPartHeaderUpdateParameterDto param);
    }
}
