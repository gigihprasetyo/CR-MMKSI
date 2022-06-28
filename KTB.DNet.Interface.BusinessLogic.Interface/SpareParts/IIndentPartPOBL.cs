#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : IndentPartPO interface
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
    public interface IIndentPartPOBL : IBaseInterface<IndentPartPOParameterDto, IndentPartPOFilterDto, IndentPartPODto>
    {
        ResponseBase<List<IndentPartPODto>> GetBySparePartPODetailID(int sparePartPODetailID);
    }
}