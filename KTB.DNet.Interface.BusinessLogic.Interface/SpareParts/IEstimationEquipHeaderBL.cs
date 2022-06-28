#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EstimationEquipHeader interface
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
    public interface IEstimationEquipHeaderBL : IBaseInterface<EstimationEquipHeaderParameterDto, EstimationEquipHeaderFilterDto, EstimationEquipHeaderDto>
    {
        ResponseBase<EstimationEquipHeaderResponseDto> Insert(EstimationEquipHeaderParameterDto objCreate);
    }
}
