#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPO interface
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
    public interface ISparePartPOBL : IBaseInterface<SparePartPOParameterDto, SparePartPOFilterDto, SparePartPOResponse>
    {
        ResponseBase<List<SparePartPODto>> ReadData(SparePartPOFilterDto filterDto, int pageSize);
        ResponseBase<List<SparePartPOOtherDto>> ReadPOOthers(SparePartPOFilterDto filterDto, int pageSize);
        ResponseBase<SparePartPOResponse> Update(SparePartPOUpdateParameterDto objUpdate, bool isUpdateDMSPRNoOnly);
    }
}
