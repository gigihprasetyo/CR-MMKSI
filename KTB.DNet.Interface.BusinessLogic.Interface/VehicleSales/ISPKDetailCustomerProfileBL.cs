#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKDetailCustomerProfile interface
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
    public interface ISPKDetailCustomerProfileBL : IBaseInterface<SPKDetailCustomerProfileParameterDto, SPKDetailCustomerProfileFilterDto, SPKDetailCustomerProfileDto>
    {
        ResponseBase<SPKDetailCustomerProfileDto> GetSPKDetailCustomerProfiles(int spkDetailCustomerId, int profGroupId, int profileHeaderId);
    }
}
