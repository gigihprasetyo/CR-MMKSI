#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKHeader interface
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
    public interface ISPKHeaderBL : IBaseInterface<SPKHeaderParameterDto, SPKHeaderFilterDto, SPKHeaderDto>
    {
        ResponseBase<List<SPKHeaderStatusDto>> GetCompleteOrCanceledSPKHeader(SPKHeaderFilterDto filterDto, int pageSize);
    }
}
