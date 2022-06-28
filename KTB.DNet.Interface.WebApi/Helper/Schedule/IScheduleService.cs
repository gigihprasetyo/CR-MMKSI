#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : IScheduleService class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.WebApi.Helper
{
    public interface IScheduleService
    {
        List<APISchedule> GetSchedules(string endPointUrl);
    }
}
