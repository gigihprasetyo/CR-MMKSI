#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ScheduleAuthorizeAttribute class
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
using KTB.DNet.Interface.Framework.Enums;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using KTB.DNet.Interface.WebApi.Helper.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
#endregion

namespace KTB.DNet.Interface.WebApi.Helper.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ScheduleAuthorizeAttribute : ActionFilterAttribute
    {
        public string ControllerMethodName { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                DateTime startDate = DateTime.Now;
                DateTime endDate = DateTime.Now;

                if (IsOnSheduled(out startDate, out endDate)) return;
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    actionContext.Response.Headers.Add("X-Scheduler", "enabled");
                    actionContext.Response.Content = GetResponseMessage(startDate, endDate);
                }
            }
            catch (Exception ex)
            {
                // do nothing
                // actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }

        /// <summary>
        /// Return Dealer Code from Login 
        /// </summary>
        public string DealerCode
        {
            get
            {
                var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var dealerCode = claims.FirstOrDefault(x => x.Type == "DealerCode");
                    if (dealerCode != null)
                        return dealerCode.Value;
                }

                return null;
            }
        }


        /// <summary>
        /// Validate Schedule compare to the request time 
        /// </summary>
        /// <returns></returns>
        protected bool IsOnSheduled(out DateTime dateStart, out DateTime dateEnd)
        {
            var result = true;
            var allowScheduleConfig = ConvertToBoolean(ConfigurationManager.AppSettings["EnableScheduler"]);
            var service = new ScheduleService();

            dateStart = DateTime.Now;
            dateEnd = DateTime.Now;

            //if config applies scheduler
            if (allowScheduleConfig)
            {
                //find schedules 
                var schedules = service.GetSchedules(ControllerMethodName);
                var scheduleUser = schedules.FirstOrDefault(x => x.DealerCode == DealerCode);
                if (scheduleUser != null)
                {
                    ConstructDateStartAndDateEnd(scheduleUser, out dateStart, out dateEnd);
                    var requestTime = HttpContext.Current.Timestamp;
                    if ((requestTime > dateStart) && (requestTime < dateEnd))
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        #region Private Methods
        /// <summary>
        /// Construct Start and End Date 
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        private void ConstructDateStartAndDateEnd(APISchedule schedule, out DateTime dateStart, out DateTime dateEnd)
        {
            //Default out put 
            dateStart = DateTime.Now;
            dateEnd = DateTime.Now;
            var dateStartOfWeek = DateTime.Now;
            var dateStartOfMonth = DateTime.Now;

            if (schedule != null)
            {
                switch (schedule.ScheduleType)
                {
                    case ScheduleType.Daily:
                        dateStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                            schedule.ScheduleTime.Hours, schedule.ScheduleTime.Minutes, schedule.ScheduleTime.Seconds);
                        dateEnd = dateStart.AddMinutes(schedule.Interval);
                        break;
                    case ScheduleType.Monthly:
                        dateStartOfMonth = DateTime.Now.FirstDayOfMonth().AddDays((int)schedule.MonthDay - 1);
                        dateStart = new DateTime(dateStartOfMonth.Year, dateStartOfMonth.Month, dateStartOfMonth.Day,
                                    schedule.ScheduleTime.Hours, schedule.ScheduleTime.Minutes, schedule.ScheduleTime.Seconds);
                        dateEnd = dateStart.AddMinutes(schedule.Interval);
                        break;
                    case ScheduleType.Weekly:
                        dateStartOfWeek = DayNumberInAWeek(DateTime.Now, (ScheduleDay)schedule.ScheduleDay);
                        var totalAddtionalDays = ConvertDayToInt((ScheduleDay)schedule.ScheduleDay);
                        dateStart = new DateTime(dateStartOfWeek.Year, dateStartOfWeek.Month, dateStartOfWeek.AddDays(totalAddtionalDays).Day,
                            schedule.ScheduleTime.Hours, schedule.ScheduleTime.Minutes, schedule.ScheduleTime.Seconds);
                        dateEnd = dateStart.AddMinutes(schedule.Interval);
                        break;
                    default:
                        break;
                }
            }
        }

        private DateTime DayNumberInAWeek(DateTime dt, ScheduleDay day)
        {
            var result = dt.FirstDayOfWeek(DayOfWeek.Sunday);
            var increment = 0;

            switch (day)
            {
                case ScheduleDay.Sunday:
                    increment = 0;
                    break;
                case ScheduleDay.Monday:
                    increment = 1;
                    break;
                case ScheduleDay.Tuesday:
                    increment = 2;
                    break;
                case ScheduleDay.Wednesday:
                    increment = 3;
                    break;
                case ScheduleDay.Thursday:
                    increment = 4;
                    break;
                case ScheduleDay.Friday:
                    increment = 5;
                    break;
                case ScheduleDay.Saturday:
                    increment = 6;
                    break;
            }
            result.AddDays(increment);
            return result;
        }

        /// <summary>
        /// Convert Day to Int 
        /// </summary>
        /// <param name="scheduleDay"></param>
        /// <returns></returns>
        private int ConvertDayToInt(ScheduleDay scheduleDay)
        {
            var result = 0;
            switch (scheduleDay)
            {
                case ScheduleDay.Sunday:
                    result = 0;
                    break;
                case ScheduleDay.Monday:
                    result = 1;
                    break;
                case ScheduleDay.Tuesday:
                    result = 2;
                    break;
                case ScheduleDay.Wednesday:
                    result = 3;
                    break;
                case ScheduleDay.Thursday:
                    result = 4;
                    break;
                case ScheduleDay.Friday:
                    result = 5;
                    break;
                case ScheduleDay.Saturday:
                    result = 6;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Convert to Boolean
        /// </summary>
        /// <param name="boolean"></param>
        /// <returns></returns>
        private bool ConvertToBoolean(string boolean)
        {
            if (boolean == "true") return true;
            return false;
        }

        private StringContent GetResponseMessage(DateTime startDate, DateTime endDate)
        {
            MessageBase message = new MessageBase();
            message.ErrorCode = ErrorCode.AuthNoPrivilege;

            message.ErrorMessage = string.Format(MessageResource.ErrorMsgNotOnSchedule, startDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss"));

            List<MessageBase> messages = new List<MessageBase>();
            messages.Add(message);

            var errorResult = HttpCodeMessage.BuildErrorResult(messages);
            HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(messages);

            var json = JsonConvert.SerializeObject(errorResult);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        #endregion
    }
}