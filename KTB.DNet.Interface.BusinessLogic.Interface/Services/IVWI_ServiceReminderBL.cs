#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceReminder interface
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
    public interface IVWI_ServiceReminderBL : IBaseInterface<ServiceReminderFollowUpParameterDto, VWI_ServiceReminderFilterDto, VWI_ServiceReminderDto>
    {
        ResponseBase<ServiceReminderFollowUpDto> FollowUp(ServiceReminderFollowUpParameterDto param);
    }
}
