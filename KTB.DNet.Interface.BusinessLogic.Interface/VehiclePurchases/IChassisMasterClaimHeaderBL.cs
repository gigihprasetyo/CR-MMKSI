#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleClaimHeader interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 10/9/2020 15:13
//
// ===========================================================================	
#endregion


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.Interface.Model;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IChassisMasterClaimHeaderBL : IBaseInterface<ChassisMasterClaimHeaderParameterDto, ChassisMasterClaimHeaderFilterDto, ChassisMastertClaimHeaderDto>
    {
        ResponseBase<ChassisMastertClaimHeaderCreateResponseDto> Create(ChassisMasterClaimHeaderParameterDto param);
        ResponseBase<List<ChassisMastertClaimHeaderDto>> ReadData(ChassisMasterClaimHeaderFilterDto filterDto, int pageSize);
        ResponseBase<ChassisMastertClaimHeaderUpdateResponseDto> Update(ChassisMasterClaimHeaderUpdateParameterDto param);

    }
}
