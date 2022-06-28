#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ScheduleService class
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
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper;
using KTB.DNet.Interface.Repository.Interface;
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.WebApi.Helper
{
    public class ScheduleService : IScheduleService
    {
        private IScheduleRepository<APISchedule, int> _scheduleRepo;
        private IEndpointScheduleRepository<APIEndpointSchedule, int> _endPointScheduleRepo;

        public ScheduleService()
        {
            _scheduleRepo = new ScheduleRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.InterfaceConnection));
            _endPointScheduleRepo = new EndpointScheduleRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.InterfaceConnection));
        }

        public List<APISchedule> GetSchedules(string endPointUrl)
        {
            var schedules = new List<APISchedule>();

            var endPointSchedules = _endPointScheduleRepo.GetByEndpointUrl(endPointUrl);
            if (endPointSchedules != null)
            {
                foreach (var endPointSchedule in endPointSchedules)
                {
                    if (endPointSchedule.Endpoint.IsScheduled)
                    {
                        var schedule = _scheduleRepo.Get(endPointSchedule.ScheduleId);
                        schedules.Add(schedule);
                    }
                }
            }

            return schedules;
        }
    }
}