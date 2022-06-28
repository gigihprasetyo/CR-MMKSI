#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKDetail interface
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
using System.Collections;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ISPKDetailBL : IBaseInterface<SPKDetailParameterDto, SPKDetailFilterDto, SPKDetailDto>
    {
        ResponseBase<SPKDetailDto> Cancel(SPKDetailParameterDto spkDetailParam);
        List<DNetValidationResult> SetSPKDetailFromParameterDto(SPKDetailParameterDto objCreate, out SPKDetail spkDetail, int rowStatusActiveCode, List<StandardCodeDto> listOfDBRowStatus, List<StandardCodeDto> listOfSPKStatus, out string msgSPKDetailCantUpdate, bool isNewSPK = false);
        List<DNetValidationResult> SetSPKProfiles(SPKDetailParameterDto spkDetailParam, out ArrayList existingProfiles, out ArrayList newProfiles);
    }
}
