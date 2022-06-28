#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ProfileGroup interface
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
    public interface IProfileGroupBL : IBaseInterface<ProfileGroupParameterDto, ProfileGroupFilterDto, ProfileGroupDto>
    {
        ResponseBase<ProfileGroupDto> GetByCode(string code);
        ResponseBase<bool> IsProfileGroupFound(string ProfileGroupCode);
        ResponseBase<int> ValidateCode(int OrganizationID, string ProfileGroupName);
    }
}
