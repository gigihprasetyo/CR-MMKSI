#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ProfileHeaderToGroup interface
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
    public interface IProfileHeaderToGroupBL : IBaseInterface<ProfileHeaderToGroupParameterDto, ProfileHeaderToGroupFilterDto, ProfileHeaderToGroupDto>
    {
        ResponseBase<List<ProfileHeaderToGroupDto>> RetrieveByProfileGroupId(int profileGroupId);
    }
}
