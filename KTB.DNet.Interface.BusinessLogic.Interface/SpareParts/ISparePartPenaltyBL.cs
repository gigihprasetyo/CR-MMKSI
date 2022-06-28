#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : SparePartPenalty interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2021 16:16:28
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ISparePartPenaltyBL : IBaseInterface<SparePartPenaltyHeaderParameterDto, SparePartPenaltyHeaderFilterDto, SparePartPenaltyHeaderDto>
    {
        ResponseBase<List<SparePartPenaltyHeaderDto>> ReadList(SparePartPenaltyHeaderFilterDto filterDto, int pageSize);
    }
}
